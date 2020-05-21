/*************************************************************************************************
 ** FILE:	global.asax.cs
 ** DATE:	01/08/2004
 ** AUTHOR:	Lee Spring
 **
 ** COPYRIGHT:
 ** Lee Spring
 ** LAS Solutions Ltd - www.las-solutions.co.uk
 ** Copyright (c) 2004 LAS Solutions Ltd
 **
 ** THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 ** TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 ** THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 ** CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 ** DEALINGS IN THE SOFTWARE.
 **
 ** OVERVIEW:
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:	Who:	Change:
 ** 1/8/04		1.0			LAS		Initial Version
 ************************************************************************************************/

using System.Threading; 
using System.Xml; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Principal;
using Discovery;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Discovery.Utility;

namespace Discovery.Scheduling 
{
    sealed public class Scheduler 
	{ 
        public class CoreScheduler  
		{ 
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is the heart of the scheduler mechanism.
            // This class manages running new events according
            // to the schedule.
            // 
            // This class can also react to the three
            // scheduler events (Started, Progressing and Completed)
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            
            private static bool ThreadPoolInitialized = false; 
            
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
            // The MaxThreadCount establishes the maximum
            // threads you want running simultaneously
            // for spawning SchedulerClient processes
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static int MaxThreadCount; 
            private static int ActiveThreadCount; 
            
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
            // If KeepRunning gets switched to false, 
            // the scheduler stops running.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static bool KeepRunning = true; 
            private static bool ForceReloadSchedule = false; 
            private static bool Debug = false; 
            

			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			// Do we need to impersonate a user on the scheduling threads?
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			private static bool _Impersonate;
			private static string _UserName;
			private static string _UserPassword;

            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // FreeThreads tracks how many threads we have
            // free to work with at any given time.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static int FreeThreads 
			{ 
                get 
				{ 
                    return MaxThreadCount - ActiveThreadCount; 
                } 
            } 
            
            public static int GetActiveThreadCount() 
			{ 
                return ActiveThreadCount; 
            } 
            
            public static int GetFreeThreadCount() 
			{ 
                return FreeThreads; 
            } 
            
            public static int GetMaxThreadCount() 
			{ 
                return MaxThreadCount; 
            } 

            private static int NumberOfProcessGroups; 
            
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // The ScheduleQueue collection contains the current
            // queue of scheduler clients that need to run
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static ArrayList ScheduleQueue = new ArrayList(); 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // The ScheduleInProgress collection contains the 
            // collection of tasks that are currently in progress
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static ArrayList ScheduleInProgress = new ArrayList(); 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is our array that holds the process group
            // where our threads will be kicked off.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static ArrayList arrProcessGroup; 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // A ReaderWriterLock will protect our objects
            // in memory from being corrupted by simultaneous
            // thread operations.  This block of code below
            // establishes variables to help keep track
            // of the ReaderWriter locks.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static ReaderWriterLock objInProgressReadWriteLock = new ReaderWriterLock(); 
            private static ReaderWriterLock objQueueReadWriteLock = new ReaderWriterLock(); 
            private static int ReaderTimeouts = 0; 
            private static int WriterTimeouts = 0; 
            private static int Reads = 0; 
            private static int Writes = 0; 
            private static int ReadTimeout = 45000;		// wait 45 seconds
            private static int WriteTimeout = 45000;	// wait 45 seconds
            
            private static ReaderWriterLock objStatusReadWriteLock = new ReaderWriterLock(); 
            
            private static ScheduleStatus Status = ScheduleStatus.STOPPED; 

			public CoreScheduler()
			{
				// See if we're in debug mode
				Debug = SchedulingProvider.Debug; 

				// Initialise the thread pool
				if (!ThreadPoolInitialized) 
				{ 
					InitializeThreadPool(SchedulingProvider.MaxThreads); 
				} 
			}

            private void InitializeThreadPool(int MaxThreads) 
			{ 
				arrProcessGroup = new ArrayList();

				// If thread count -1, defualt to 5
                if (MaxThreads == -1) 
				{ 
					MaxThreads = 5; 
				}
 
                NumberOfProcessGroups = MaxThreads; 
                MaxThreadCount = MaxThreads; 

                for (int i = 0; i < NumberOfProcessGroups; i++) 
				{ 
					arrProcessGroup.Add(new ProcessGroup());
                }
 
                ThreadPoolInitialized = true; 
            } 
            
            
            public static void ReloadSchedule() 
			{ 
                ForceReloadSchedule = true; 
            } 
            
            public static ScheduleStatus GetScheduleStatus() 
			{ 
                ScheduleStatus s = 0; 
                try 
				{ 
                    objStatusReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        s = Status; 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objStatusReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return s; 
            } 
            
            
            public static void SetScheduleStatus(ScheduleStatus objScheduleStatus) 
			{ 
                try 
				{ 
                    objStatusReadWriteLock.AcquireWriterLock(WriteTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read or write
                        //  from the shared resource.
                        Status = objScheduleStatus; 
                        Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objStatusReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts); 
                    //Exception.LogException(ex);
                    throw ex;
                } 
            } 
            
            public static void Halt(string SourceOfHalt) 
			{ 
				// Stop running
                KeepRunning = false; 

				// Update status
                SetScheduleStatus(ScheduleStatus.SHUTTING_DOWN);
                
                LogEntry logEntry = new LogEntry(
                            "The Scheduler is shutting down.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                // Write to the log
                Logger.Write(logEntry);
                
                //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                //objEventLogInfo.AddProperty("Initiator", SourceOfHalt); 
                //objEventLogInfo.LogTypeKey = "SCHEDULER_SHUTTING_DOWN"; 
                //objEventLog.AddLog(objEventLogInfo); 
                
                // Wait for up to 30 seconds for thread
                // to shut down
                for (int i = 0; i <= 30; i++) 
				{ 
                    if (Status == ScheduleStatus.STOPPED) 
					{ 
						return; 
					} 
					// Release thread quantum
                    Thread.Sleep(1000); 
                } 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that adds
            // an item to the collection of schedule items in 
            // progress.  It first obtains a write lock
            // on the ScheduleInProgress object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static void AddToScheduleInProgress(ScheduleHistoryItem objScheduleHistoryItem) 
			{ 
                try 
				{ 
                    objInProgressReadWriteLock.AcquireWriterLock(WriteTimeout); 
                    try 
					{ 
                        // It is safe for this thread to read or write
                        // from the shared resource.
						ScheduleInProgress.Add(objScheduleHistoryItem);
                        Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        // Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    // The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts); 
                    //Exception.LogException(ex);
                    throw ex;
                } 
            } 
            
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that removes
            // an item from the collection of schedule items in 
            // progress.  It first obtains a write lock
            // on the ScheduleInProgress object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static void RemoveFromScheduleInProgress(ScheduleItem objScheduleItem) 
			{ 
                try 
				{ 
                    objInProgressReadWriteLock.AcquireWriterLock(WriteTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read or write
                        //  from the shared resource.

						foreach(Object objItem in ScheduleInProgress)
						{
							if (((ScheduleItem)objItem).Id == objScheduleItem.Id)
							{
								ScheduleInProgress.Remove(objItem);
								break;
							}
						}
                        Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts);
                    throw ex;
                    //Exception.LogException(ex); 
                } 
            } 
            
            private static bool ScheduleQueueContains(ScheduleItem objScheduleItem) 
			{ 
				bool bRetVal = false;

				foreach(Object objItem in ScheduleQueue)
				{
					if (((ScheduleItem)objItem).Id == objScheduleItem.Id)
					{
						bRetVal = true;
						break;
					}
				}
				return bRetVal;
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that adds
            // an item to the collection of schedule items in 
            // queue.  It first obtains a write lock
            // on the ScheduleQueue object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static void AddToScheduleQueue(ScheduleHistoryItem objScheduleHistoryItem) 
			{ 
                try 
				{ 
                    objQueueReadWriteLock.AcquireWriterLock(WriteTimeout); 
					try 
					{ 
                        //  It is safe for this thread to read or write
                        //  from the shared resource.
                        if (!ScheduleQueueContains(objScheduleHistoryItem)) 
						{ 
                            ScheduleQueue.Add(objScheduleHistoryItem); 
                        } 
                        Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts);
                    throw ex;
                    //Exception.LogException(ex); 
                } 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that clears
            // the collection of schedule items in 
            // queue.  It first obtains a write lock
            // on the ScheduleQueue object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static void ClearScheduleQueue() 
			{ 
                try 
				{ 
                    objQueueReadWriteLock.AcquireWriterLock(WriteTimeout); 
                    try 
					{ 
						// Clear the queue
						ScheduleQueue.Clear();
						Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts);
                    throw ex;
                    //Exception.LogException(ex); 
                } 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that removes
            // an item from the collection of schedule items in 
            // queue.  It first obtains a write lock
            // on the ScheduleQueue object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            private static void RemoveFromScheduleQueue(ScheduleItem objScheduleItem) 
			{ 
                try 
				{ 
                    objQueueReadWriteLock.AcquireWriterLock(WriteTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read or write
                        //  from the shared resource.
						foreach(Object objItem in ScheduleQueue)
						{
							if (((ScheduleItem)objItem).Id == objScheduleItem.Id)
							{
								ScheduleQueue.Remove(objItem);
								break;
							}
						}
                        Interlocked.Increment(ref Writes); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseWriterLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The writer lock request timed out.
                    Interlocked.Increment(ref WriterTimeouts);
                    throw ex;
                    //Exception.LogException(ex); 
                } 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that returns
            // the number of items in the collection of 
            // schedule items in queue.  It first obtains a
            // read lock on the ScheduleQueue object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static ArrayList GetScheduleQueue() 
			{ 
                ArrayList tmpScheduleQueue = null; 
                try 
				{ 
                    objQueueReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        tmpScheduleQueue = ScheduleQueue; 
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return tmpScheduleQueue; 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that returns
            // the number of items in the collection of 
            // schedule items in progress.  It first obtains a
            // read lock on the ScheduleProgress object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static ArrayList GetScheduleInProgress() 
			{ 
                ArrayList arrScheduleInProgress = null; 
                try 
				{ 
                    objInProgressReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        arrScheduleInProgress = ScheduleInProgress; 
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return arrScheduleInProgress; 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that returns
            // the number of items in the collection of 
            // schedule items in queue.  It first obtains a
            // read lock on the ScheduleQueue object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static int GetScheduleQueueCount() 
			{ 
                int intCount = 0; 
                try 
				{ 
                    objQueueReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        intCount = ScheduleQueue.Count; 
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return intCount; 
            } 
            
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // This is a multi-thread safe method that returns
            // the number of items in the collection of 
            // schedule items in progress.  It first obtains a
            // read lock on the ScheduleProgress object.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            public static int GetScheduleInProgressCount() 
			{ 
                int intCount = 0; 
                try 
				{ 
                    objInProgressReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        intCount = ScheduleInProgress.Count; 
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return intCount; 
            } 
            
            public static void WorkStarted(SchedulerClient objSchedulerClient) 
			{ 
                try 
				{ 
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // A SchedulerClient is notifying us that their
                    // process has started.  Increase our ActiveThreadCount
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    Interlocked.Increment(ref ActiveThreadCount); 
                    
                    objSchedulerClient.ScheduleHistoryItem.ThreadID = Thread.CurrentThread.GetHashCode(); 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Put the object in the ScheduleInProgress collection
                    // and remove it from the ScheduleQueue
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    RemoveFromScheduleQueue(objSchedulerClient.ScheduleHistoryItem); 
                    AddToScheduleInProgress(objSchedulerClient.ScheduleHistoryItem); 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Update the schedule item
                    // object property to note the start time.
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    objSchedulerClient.ScheduleHistoryItem.StartDate = DateTime.Now; 
                    CoreScheduler.SaveScheduleHistory(objSchedulerClient.ScheduleHistoryItem); 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler event has started.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                    // Write to the log
                    Logger.Write(logEntry);
                    
                    //Discovery.Logging.EventLogController objEventLog = new Discovery.Logging.EventLogController(); 
                    //Discovery.Logging.EventLogInfo objEventLogInfo = new Discovery.Logging.EventLogInfo(); 
                    //objEventLogInfo.AddProperty("THREAD ID", Thread.CurrentThread.GetHashCode().ToString()); 
                    //objEventLogInfo.AddProperty("TYPE", objSchedulerClient.GetType().FullName); 
                    //objEventLogInfo.AddProperty("SOURCE", objSchedulerClient.ScheduleHistoryItem.ScheduleSource.ToString()); 
                    //objEventLogInfo.AddProperty("ACTIVE THREADS", ActiveThreadCount.ToString()); 
                    //objEventLogInfo.AddProperty("FREE THREADS", FreeThreads.ToString()); 
                    //objEventLogInfo.AddProperty("READER TIMEOUTS", ReaderTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("WRITER TIMEOUTS", WriterTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("IN PROGRESS", GetScheduleInProgressCount().ToString()); 
                    //objEventLogInfo.AddProperty("IN QUEUE", GetScheduleQueueCount().ToString()); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULER_EVENT_STARTED"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                } 
                catch (Exception exc) 
				{ 
                    //Exception.ProcessSchedulerException(exc);
                    throw exc;
                } 
            } 
            
            public static void WorkProgressing(SchedulerClient objSchedulerClient) 
			{ 
                try 
				{ 
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // A SchedulerClient is notifying us that their
                    // process is in progress.  Informational only.
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''

                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler event is processing.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                    // Write to the log
                    Logger.Write(logEntry);
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    //Discovery.Logging.EventLogController objEventLog = new Discovery.Logging.EventLogController(); 
                    //Discovery.Logging.EventLogInfo objEventLogInfo = new Discovery.Logging.EventLogInfo(); 
                    //objEventLogInfo.AddProperty("THREAD ID", Thread.CurrentThread.GetHashCode().ToString()); 
                    //objEventLogInfo.AddProperty("TYPE", objSchedulerClient.GetType().FullName); 
                    //objEventLogInfo.AddProperty("SOURCE", objSchedulerClient.ScheduleHistoryItem.ScheduleSource.ToString()); 
                    //objEventLogInfo.AddProperty("ACTIVE THREADS", ActiveThreadCount.ToString()); 
                    //objEventLogInfo.AddProperty("FREE THREADS", FreeThreads.ToString()); 
                    //objEventLogInfo.AddProperty("READER TIMEOUTS", ReaderTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("WRITER TIMEOUTS", WriterTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("IN PROGRESS", GetScheduleInProgressCount().ToString()); 
                    //objEventLogInfo.AddProperty("IN QUEUE", GetScheduleQueueCount().ToString()); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULER_EVENT_PROGRESSING"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                } 
                catch (Exception exc) 
				{ 
                    //Exceptions.ProcessSchedulerException(exc);
                    throw exc;
                } 
            } 
            
            public static void WorkCompleted(SchedulerClient objSchedulerClient) 
			{ 
                try 
				{ 
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // A SchedulerClient is notifying us that their
                    // process has completed.  Decrease our ActiveThreadCount
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    Interlocked.Decrement(ref ActiveThreadCount); 
                    
                    ScheduleHistoryItem objScheduleHistoryItem = null; 
                    objScheduleHistoryItem = objSchedulerClient.ScheduleHistoryItem; 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Remove the object in the ScheduleInProgress collection
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    RemoveFromScheduleInProgress(objScheduleHistoryItem); 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Update the schedule item object property
                    // to note the end time and next start
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    objScheduleHistoryItem.EndDate = DateTime.Now; 
                    
                    if (objScheduleHistoryItem.ScheduleSource == ScheduleSource.STARTED_FROM_EVENT) 
					{ 
                        objScheduleHistoryItem.NextStart = Null.NullDate; 
                    } 
                    else 
					{ 
                        if (objScheduleHistoryItem.CatchUpEnabled) 
						{ 
                            switch (objScheduleHistoryItem.TimeLapseMeasurement) 
							{
								case "m":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.NextStart.AddMinutes(objScheduleHistoryItem.TimeLapse); 
									break;
								}
								case "h":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.NextStart.AddHours(objScheduleHistoryItem.TimeLapse); 
									break;
								}
								case "d":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.NextStart.AddDays(objScheduleHistoryItem.TimeLapse); 
									break;
								}
                            }
                            
                        } 
                        else 
						{ 
                            switch (objScheduleHistoryItem.TimeLapseMeasurement) 
							{
								case "m":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddMinutes(objScheduleHistoryItem.TimeLapse); 
									break;
								}
								case "h":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddHours(objScheduleHistoryItem.TimeLapse); 
									break;
								}
								case "d":
								{
									objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddDays(objScheduleHistoryItem.TimeLapse); 
									break;
								}
                            }
                        } 
                    } 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Update the ScheduleHistory in the database
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    CoreScheduler.SaveScheduleHistory(objScheduleHistoryItem); 
                    
                    //Discovery.Logging.EventLogInfo objEventLogInfo = new Discovery.Logging.EventLogInfo(); 
                    
                    if (objScheduleHistoryItem.NextStart != Null.NullDate) 
					{ 
                        // ''''''''''''''''''''''''''''''''''''''''''''''''''
                        // Put the object back into the ScheduleQueue
                        // collection with the new NextStart date.
                        // ''''''''''''''''''''''''''''''''''''''''''''''''''
                        objScheduleHistoryItem.StartDate = Null.NullDate; 
                        objScheduleHistoryItem.EndDate = Null.NullDate; 
                        objScheduleHistoryItem.LogNotes = ""; 
                        objScheduleHistoryItem.ProcessGroup = -1; 
                        AddToScheduleQueue(objScheduleHistoryItem); 
                    }

                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler event has completed.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                    // Write to the log
                    Logger.Write(logEntry);

                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    //Discovery.Logging.EventLogController objEventLog = new Discovery.Logging.EventLogController(); 
                    
                    //objEventLogInfo.AddProperty("TYPE", objSchedulerClient.GetType().FullName); 
                    //objEventLogInfo.AddProperty("THREAD ID", Thread.CurrentThread.GetHashCode().ToString()); 
                    //objEventLogInfo.AddProperty("NEXT START", Convert.ToString(objScheduleHistoryItem.NextStart)); 
                    //objEventLogInfo.AddProperty("SOURCE", objSchedulerClient.ScheduleHistoryItem.ScheduleSource.ToString()); 
                    //objEventLogInfo.AddProperty("ACTIVE THREADS", ActiveThreadCount.ToString()); 
                    //objEventLogInfo.AddProperty("FREE THREADS", FreeThreads.ToString()); 
                    //objEventLogInfo.AddProperty("READER TIMEOUTS", ReaderTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("WRITER TIMEOUTS", WriterTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("IN PROGRESS", GetScheduleInProgressCount().ToString()); 
                    //objEventLogInfo.AddProperty("IN QUEUE", GetScheduleQueueCount().ToString()); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULER_EVENT_COMPLETED"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                } 
                catch (Exception exc) 
				{ 
                    //Exception.ProcessSchedulerException(exc);
                    throw exc;
                } 
            } 
            
            public static void WorkErrored(SchedulerClient objSchedulerClient, Exception objException) 
			{ 
                try 
				{ 
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // A SchedulerClient is notifying us that their
                    // process has errored.  Decrease our ActiveThreadCount
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    Interlocked.Decrement(ref ActiveThreadCount); 
                    
                    ScheduleHistoryItem objScheduleHistoryItem = null; 
                    objScheduleHistoryItem = objSchedulerClient.ScheduleHistoryItem; 
                    
					// '''''''''''''''''''''''''''''''''''''''''''''''''
                    // Remove the object in the ScheduleInProgress collection
                    // '''''''''''''''''''''''''''''''''''''''''''''''''
                    RemoveFromScheduleInProgress(objScheduleHistoryItem); 
                    
                    // '''''''''''''''''''''''''''''''''''''''''''''''''
                    // Update the schedule item object property
                    // to note the end time and next start
                    // '''''''''''''''''''''''''''''''''''''''''''''''''
                    objScheduleHistoryItem.EndDate = DateTime.Now; 
                    if (objScheduleHistoryItem.ScheduleSource == ScheduleSource.STARTED_FROM_EVENT) 
					{ 
                        objScheduleHistoryItem.NextStart = Null.NullDate; 
                    } 
                    else if (objScheduleHistoryItem.RetryTimeLapse != Null.NullInteger) 
					{ 
                        switch (objScheduleHistoryItem.RetryTimeLapseMeasurement) 
						{
							case "m":
							{
								objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddMinutes(objScheduleHistoryItem.RetryTimeLapse); 
								break;
							}
							case "h":
							{
								objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddHours(objScheduleHistoryItem.RetryTimeLapse); 
								break;
							}
							case "d":
							{
								objScheduleHistoryItem.NextStart = objScheduleHistoryItem.StartDate.AddDays(objScheduleHistoryItem.RetryTimeLapse); 
								break;
							}
                        }
                    } 
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Update the ScheduleHistory in the database
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    CoreScheduler.SaveScheduleHistory(objScheduleHistoryItem); 
                    
                    if (objScheduleHistoryItem.NextStart != Null.NullDate && objScheduleHistoryItem.RetryTimeLapse != Null.NullInteger) 
					{ 
                        // ''''''''''''''''''''''''''''''''''''''''''''''''''
                        // Put the object back into the ScheduleQueue
                        // collection with the new NextStart date.
                        // ''''''''''''''''''''''''''''''''''''''''''''''''''
                        objScheduleHistoryItem.StartDate = Null.NullDate; 
                        objScheduleHistoryItem.EndDate = Null.NullDate; 
                        objScheduleHistoryItem.LogNotes = ""; 
                        objScheduleHistoryItem.ProcessGroup = -1; 
                        AddToScheduleQueue(objScheduleHistoryItem); 
                    }

                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler event is failure.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                    // Write to the log
                    Logger.Write(logEntry);

                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    //Discovery.Logging.EventLogController objEventLog = new Discovery.Logging.EventLogController(); 
                    //Discovery.Logging.EventLogInfo objEventLogInfo = new Discovery.Logging.EventLogInfo(); 
                    //objEventLogInfo.AddProperty("USER IDENTITY", WindowsIdentity.GetCurrent().Name);
                    //objEventLogInfo.AddProperty("ASYNCHRONOUS", SchedulingProvider.Asynchronous.ToString());
                    //objEventLogInfo.AddProperty("THREAD ID", Thread.CurrentThread.GetHashCode().ToString()); 
                    //objEventLogInfo.AddProperty("TYPE", objSchedulerClient.GetType().FullName); 
                    
                    //if (objException != null) 
                    //{ 
                    //    objEventLogInfo.AddProperty("EXCEPTION", objException.Message); 
                    //} 
                    
                    //objEventLogInfo.AddProperty("RESCHEDULED FOR", Convert.ToString(objScheduleHistoryItem.NextStart)); 
                    //objEventLogInfo.AddProperty("SOURCE", objSchedulerClient.ScheduleHistoryItem.ScheduleSource.ToString()); 
                    //objEventLogInfo.AddProperty("ACTIVE THREADS", ActiveThreadCount.ToString()); 
                    //objEventLogInfo.AddProperty("FREE THREADS", FreeThreads.ToString()); 
                    //objEventLogInfo.AddProperty("READER TIMEOUTS", ReaderTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("WRITER TIMEOUTS", WriterTimeouts.ToString()); 
                    //objEventLogInfo.AddProperty("IN PROGRESS", GetScheduleInProgressCount().ToString()); 
                    //objEventLogInfo.AddProperty("IN QUEUE", GetScheduleQueueCount().ToString()); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULER_EVENT_FAILURE"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                } 
                catch (Exception exc) 
				{ 
                    //Exception.ProcessSchedulerException(exc);
                    throw exc;
                } 
            } 
            
            public static bool PurgeScheduleHistory() 
			{ 
                SchedulingController objSchedulingController = new SchedulingController(); 
                return(objSchedulingController.PurgeScheduleHistory()); 
            } 

            public static void RunEventSchedule(EventName EventName) 
			{ 
				try 
				{
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler fired from event.",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);

                    // Write to the log
                    Logger.Write(logEntry);
                    
                    //// Event log controller
                    //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                    //// Event log info
                    //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                    //// Set event properties
                    //objEventLogInfo.AddProperty("EVENT", EventName.ToString()); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULE_FIRED_FROM_EVENT"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                    
					// ''''''''''''''''''''''''''''''''''''''''''''''''''
					// We allow for threads to run simultaneously.
					// As long as we have an open thread, continue.
					// ''''''''''''''''''''''''''''''''''''''''''''''''''

					// ''''''''''''''''''''''''''''''''''''''''''''''''''
					// Load the queue to determine which schedule
					// items need to be run. 
					// ''''''''''''''''''''''''''''''''''''''''''''''''''
					LoadQueueFromEvent(EventName); 
                    
					// We call GetScheduleQueueCount to lock before the read
					while (GetScheduleQueueCount() > 0) 
					{ 
						SetScheduleStatus(ScheduleStatus.RUNNING_EVENT_SCHEDULE); 
                        
						// ''''''''''''''''''''''''''''''''''''''''''''''''''
						// Fire off the events that need running.
						// ''''''''''''''''''''''''''''''''''''''''''''''''''

                        try
                        {
                            objQueueReadWriteLock.AcquireReaderLock(ReadTimeout);
                            try
                            {
                                //  It is safe for this thread to read from
                                //  the shared resource.
                                if (ScheduleQueue.Count > 0)
                                {
                                    // Fire events asynchronous
                                    FireEvents(SchedulingProvider.Asynchronous);
                                }
                                Interlocked.Increment(ref Reads);
                            }
                            finally
                            {
                                //  Ensure that the lock is released.
                                objQueueReadWriteLock.ReleaseReaderLock();
                            }
                        }
                        catch (ApplicationException ex)
                        {
                            //  The reader lock request timed out.
                            Interlocked.Increment(ref ReaderTimeouts);
                            throw ex;
                        }
                        
						if (WriterTimeouts > 20 || ReaderTimeouts > 20) 
						{ 
							// ''''''''''''''''''''''''''''''''''''''''''''''''''
							// Wait for 10 minutes so we don't fill up the logs
							// ''''''''''''''''''''''''''''''''''''''''''''''''''
							Thread.Sleep(600000); 
						} 
						else 
						{ 
							// ''''''''''''''''''''''''''''''''''''''''''''''''''
							// Wait for 10 seconds to avoid cpu overutilization
							// ''''''''''''''''''''''''''''''''''''''''''''''''''
							Thread.Sleep(10000); 
						} 
                        
						if (GetScheduleQueueCount() == 0) 
						{ 
							return; 
						} 
					} 
				}
                catch (Exception exc) 
				{ 
                    //Exception.ProcessSchedulerException(exc);
                    throw exc;
                } 
                //finally
                //{
                //    // See if we need to undo impersonation
                //    if (null != impersonationContext)
                //    {
                //        Globals.UndoImpersonation(impersonationContext);
                //    }
                //}
			} 
            
            public static void StartAndWaitForResponse() 
			{ 
                Thread newThread = new Thread(new ThreadStart(Start)); 
                // newThread.IsBackground = True
                newThread.Start(); 
                // wait for up to 30 seconds for thread
                // to start up
                for (int i = 0; i <= 30; i++) 
				{ 
                    if (Status != ScheduleStatus.STOPPED) 
					{ 
						return; 
					} 
                    Thread.Sleep(1000); 
                } 
            } 
            
            public static void Start() 
			{ 
				try 
				{ 
				
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // This is where the action begins.
                    // Loop until KeepRunning = false
					// We have a ProcessGroup for each thread we need to run
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    while (KeepRunning) 
					{ 
                        try 
						{ 
							// Write entry to the event log
                            SetScheduleStatus(ScheduleStatus.RUNNING_TIMER_SCHEDULE);
                            
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // Write out the log entry for this event
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            LogEntry logEntry = new LogEntry(
                                    "The Scheduler has started and running under \"" + WindowsIdentity.GetCurrent().Name + "\"",
                                    "Scheduling.Information",
                                    0,
                                    0,
                                    TraceEventType.Start,
                                    null,
                                    null);


                            // Write to the log
                            Logger.Write(logEntry);

                            //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                            //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                            //objEventLogInfo.LogTypeKey = "SCHEDULER_STARTED"; 

                            //// Add an entry for the user we're running as 
                            //objEventLogInfo.AddProperty("Running under", WindowsIdentity.GetCurrent().Name);
                            //objEventLog.AddLog(objEventLogInfo); 
                            
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // Load the queue to determine which schedule
                            // items need to be run. 
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            LoadQueueFromTimer(); 
                            
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // Keep track of when the queue was last refreshed
                            // so we can perform a refresh periodically
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            DateTime LastQueueRefresh = DateTime.Now; 

							// We don't need to refresh the queue schedule
							bool RefreshQueueSchedule = false; 
                            
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // We allow for [MaxThreadCount] threads to run 
                            // simultaneously.  As long as we have an open thread
                            // and we don't have to refresh the queue, continue
                            // to loop.
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            while (
									FreeThreads > 0 && 
									!RefreshQueueSchedule && 
									KeepRunning && 
									!ForceReloadSchedule) 
							{ 
                                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                // Fire off the events that need running.
                                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                try 
								{ 
									// Set the scheduler statsus
                                    SetScheduleStatus(ScheduleStatus.RUNNING_TIMER_SCHEDULE); 

									// Obtain read lock on queue from this thread
                                    objQueueReadWriteLock.AcquireReaderLock(ReadTimeout); 
                                    try 
									{ 
                                        //  It is safe for this thread to read from
                                        //  the shared resource.
                                        if (ScheduleQueue.Count > 0) 
										{ 
                                            FireEvents(SchedulingProvider.Asynchronous); 
                                        } 
                                        Interlocked.Increment(ref Reads); 
                                    } 
                                    finally 
									{ 
                                        //  Ensure that the lock is released.
                                        objQueueReadWriteLock.ReleaseReaderLock(); 
                                    } 
                                } 
                                catch (ApplicationException ex) 
								{ 
                                    //  The reader lock request timed out.
                                    Interlocked.Increment(ref ReaderTimeouts);
                                    throw ex;
                                } 
                                
                                if (WriterTimeouts > 20 | ReaderTimeouts > 20) 
								{ 
                                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                    // Some kind of deadlock on a resource.
                                    // Wait for 10 minutes so we don't fill up the logs
                                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                    if (KeepRunning) 
									{ 
                                        Thread.Sleep(600000); // sleep for 10 minutes
                                    } 
                                    else 
									{ 
                                        return; 
                                    } 
                                } 
                                else 
								{ 
                                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                    // Wait for 10 seconds to avoid cpu overutilization
                                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                                    if (KeepRunning) 
									{ 
                                        Thread.Sleep(10000); // sleep for 10 seconds
                                    } 
                                    else 
									{ 
                                        return; 
                                    } 
                                    
                                    // Refresh queue from database every 10 minutes
                                    // if there are no items currently in progress
                                    if ((LastQueueRefresh.AddMinutes(10) <= DateTime.Now || ForceReloadSchedule) && FreeThreads == MaxThreadCount) 
									{ 
                                        RefreshQueueSchedule = true; 
                                        break;
                                    } 
                                } 
                            } 
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // There are no available threads, all are being
                            // used.  Wait a minute until one is available
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            if (KeepRunning) 
							{ 
                                if (RefreshQueueSchedule) 
								{ 
                                    SetScheduleStatus(ScheduleStatus.WAITING_FOR_OPEN_THREAD); 
									// sleep for 10 seconds
                                    Thread.Sleep(10000); 
                                } 
                            } 
                            else 
							{ 
                                return; 
                            } 
                        } 
                        catch (Exception exc) 
						{ 
                            //Exception.ProcessSchedulerException(exc); 
                            //throw exc;
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // sleep for 10 minutes
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            Thread.Sleep(600000); 
                        } 
                    } 
                } 
                finally 
				{ 
					// See if we need to undo impersonation
                    //if (null != impersonationContext)
                    //{
                    //    Globals.UndoImpersonation(impersonationContext);
                    //}

                    SetScheduleStatus(ScheduleStatus.STOPPED); 
                    
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    // Write out the log entry for this event
                    // ''''''''''''''''''''''''''''''''''''''''''''''''''
                    LogEntry logEntry = new LogEntry(
                            "The Scheduler has stopped",
                            "Scheduling.Information",
                            0,
                            0,
                            TraceEventType.Start,
                            null,
                            null);


                    // Write to the log
                    Logger.Write(logEntry);

                    //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                    //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                    //objEventLogInfo.LogTypeKey = "SCHEDULER_STOPPED"; 
                    //objEventLog.AddLog(objEventLogInfo); 
                } 
            } 
            
            public static void FireEvents(bool Asynchronous) 
			{ 
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                // This method uses a thread pool to
                // call the SchedulerClient methods that need
                // to be called.
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                // For each item in the queue that there
                // is an open thread for, set the object
                // in the array to a new ProcessGroup object.
                // Pass in the ScheduleItem to the ProcessGroup
                // so the ProcessGroup can pass it around for
                // logging and notifications.
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                int numToRun = ScheduleQueue.Count; 
				
				// We can't run more than we have threads for
				if (numToRun > FreeThreads) 
				{ 
                    numToRun = FreeThreads; 
                } 
    
				// Iterate over each of the queue items
				for (int i = 0; i < ScheduleQueue.Count && numToRun > 0 && KeepRunning; i++)
				{
					// This gets a random process group id
					int ProcessGroup = GetProcessGroup(); 

					// Retrieve the next item from the queue
					ScheduleItem objScheduleItem = (ScheduleItem)ScheduleQueue[i];
					
					// Make sure the item can run                    
					if (objScheduleItem.NextStart <= DateTime.Now &&	// Start is not in the future
						objScheduleItem.Enabled &&						// The task is enabled
						!IsInProgress(objScheduleItem) &&				// It's not already running (match ID's)
						!HasDependenciesConflict(objScheduleItem))		// It does't conflic with anything running **LAS** fixed this
					{ 
						// Assign it the random process group id
						objScheduleItem.ProcessGroup = ProcessGroup; 
						// See if we run asynchrounously
						if (Asynchronous) 
						{ 
							// Add to queue
							// This components Run() method will get called from a thread in the thread pool of this process
							((ProcessGroup)arrProcessGroup[ProcessGroup]).AddQueueUserWorkItem(objScheduleItem); 
						} 
						else 
						{ 
							// Run now
							((ProcessGroup)arrProcessGroup[ProcessGroup]).RunSingleTask(objScheduleItem); 
						} 

						// See if we need to write to the log
						if (Debug) 
						{
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // Write out the log entry for this event
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            LogEntry logEntry = new LogEntry(
                                    "The Scheduler event is added to process group",
                                    "Scheduling.Information",
                                    0,
                                    0,
                                    TraceEventType.Start,
                                    null,
                                    null);


                            // Write to the log
                            Logger.Write(logEntry);

                            //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                            //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                            //objEventLogInfo.AddProperty("EVENT ADDED TO PROCESS GROUP " + objScheduleItem.ProcessGroup.ToString(), objScheduleItem.TypeFullName); 
                            //objEventLogInfo.AddProperty("SCHEDULE ID", objScheduleItem.ScheduleID.ToString()); 
                            //objEventLogInfo.LogTypeKey = "DEBUG"; 
                            //objEventLog.AddLog(objEventLogInfo); 
						} 
					} 
					else 
					{ 
						// Item wasn't required to run
						if (Debug) 
						{ 
							bool appended = false; 
							// Build string for debug output
							System.Text.StringBuilder strDebug = new System.Text.StringBuilder("Task not run because "); 
							// Did not run because date is in the future
							if (! (objScheduleItem.NextStart <= DateTime.Now)) 
							{ 
								strDebug.Append(" task is scheduled for " + objScheduleItem.NextStart.ToString()); 
								appended = true; 
							} 
							// Item is not enabled
							if (! objScheduleItem.Enabled) 
							{ 
								if (appended) 
								{ 
									strDebug.Append(" and"); 
								} 
								strDebug.Append(" task is not enabled"); 
								appended = true; 
							}
							// Task is already running
							if (IsInProgress(objScheduleItem)) 
							{ 
								if (appended) 
								{ 
									strDebug.Append(" and"); 
								} 
								strDebug.Append(" task is already in progress"); 
								appended = true; 
							} 
							// Conflicting dependancy
							if (HasDependenciesConflict(objScheduleItem)) 
							{ 
								if (appended) 
								{ 
									strDebug.Append(" and"); 
								} 
								strDebug.Append(" task has conflicting dependency"); 
								appended = true; 
							}

                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            // Write out the log entry for this event
                            // ''''''''''''''''''''''''''''''''''''''''''''''''''
                            LogEntry logEntry = new LogEntry(
                                    "The Scheduler event is not run",
                                    "Scheduling.Information",
                                    0,
                                    0,
                                    TraceEventType.Start,
                                    null,
                                    null);


                            // Write to the log
                            Logger.Write(logEntry);
                            
                            // Write to the log
                            //Logging.EventLogController objEventLog = new Logging.EventLogController(); 
                            //Logging.EventLogInfo objEventLogInfo = new Logging.EventLogInfo(); 
                            //objEventLogInfo.AddProperty("EVENT NOT RUN REASON", strDebug.ToString()); 
                            //objEventLogInfo.AddProperty("SCHEDULE ID", objScheduleItem.ScheduleID.ToString()); 
                            //objEventLogInfo.AddProperty("TYPE FULL NAME", objScheduleItem.TypeFullName); 
                            //objEventLogInfo.LogTypeKey = "DEBUG"; 
                            //objEventLog.AddLog(objEventLogInfo); 
						} 
					} 
					// Decrement number to run
					numToRun--;
				}
            } 
            
            public static void LoadQueueFromTimer() 
			{ 
				// We don't reload the schedule by default
                ForceReloadSchedule = false; 

				// Load schedule from DB
                SchedulingController s = new SchedulingController(); 
                List<ScheduleItem> a = s.GetSchedule(); 
                
				// Add items to queue
                for (int i = 0; i < a.Count; i++) 
				{
					// Schedule item to add
                    ScheduleHistoryItem objScheduleHistoryItem = null; 
                    objScheduleHistoryItem = (ScheduleHistoryItem)a[i]; 
                    
					// Add the item to the queue if it's not in the queue, enabled and has TimeLapse values
					// IsInQueue simply compares the ID of the item and the ID's in the ScheduleQueue
                    if (!IsInQueue(objScheduleHistoryItem) &&								// Not already in the queue
						objScheduleHistoryItem.TimeLapse != Null.NullInteger &&				// Has a time lapse
						objScheduleHistoryItem.TimeLapseMeasurement != Null.NullString &&	// Has a time lapse measurement
						objScheduleHistoryItem.Enabled)										// It's enabled
					{
						// Add item to queue and specify source as STARTED_FROM_TIMER
                        objScheduleHistoryItem.ScheduleSource = ScheduleSource.STARTED_FROM_TIMER; 
                        AddToScheduleQueue(objScheduleHistoryItem); 
                    } 
                } 
            } 
            
            public static void LoadQueueFromEvent(EventName EventName) 
			{ 
                SchedulingController s = new SchedulingController(); 
                List<ScheduleItem> a = s.GetSchedule(EventName.ToString()); 
                
                for (int i = 0; i < a.Count; i++) 
				{ 
                    ScheduleHistoryItem objScheduleHistoryItem = null; 
                    objScheduleHistoryItem = (ScheduleHistoryItem)a[i]; 
                    
                    if (!IsInQueue(objScheduleHistoryItem) && 
						!IsInProgress(objScheduleHistoryItem) && 
						!HasDependenciesConflict(objScheduleHistoryItem) && 
						objScheduleHistoryItem.Enabled) 
					{ 
                        objScheduleHistoryItem.ScheduleSource = ScheduleSource.STARTED_FROM_EVENT; 
                        AddToScheduleQueue(objScheduleHistoryItem); 
                    } 
                } 
            } 
            
            private static int GetProcessGroup() 
			{ 
                // Return a random process group
                Random r = new Random(); 
                return r.Next(0, NumberOfProcessGroups - 1); 
            } 
            
            private static bool IsInQueue(ScheduleItem objScheduleItem) 
			{ 
                bool objReturn = false; 
                try 
				{ 
                    objQueueReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
						foreach(Object objItem in ScheduleQueue)
						{
							if (((ScheduleItem)objItem).Id == objScheduleItem.Id)
							{
								objReturn = true;
								break;
							}
						}
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objQueueReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return objReturn; 
            } 
            
            private static bool IsInProgress(ScheduleItem objScheduleItem) 
			{ 
                bool objReturn = false; 
                try 
				{ 
                    objInProgressReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
						foreach(Object objItem in ScheduleInProgress)
						{
							if (((ScheduleItem)objItem).Id == objScheduleItem.Id) 
							{ 
								objReturn = true; 
								break;
							} 
						}
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return objReturn; 
            } 
            
            public static bool HasDependenciesConflict(ScheduleItem objScheduleItem) 
			{
				// This method checks for any dependancy, not a specific one
                bool objReturn = false; 
                try 
				{ 
					// Acquire reader lock
                    objInProgressReadWriteLock.AcquireReaderLock(ReadTimeout); 
                    try 
					{ 
                        //  It is safe for this thread to read from
                        //  the shared resource.
                        if (null != ScheduleInProgress && ScheduleInProgress.Count > 0 && objScheduleItem.ObjectDependencies.Length > 0) 
						{
							// We need only check against what is already running
							foreach(Object objItem in ScheduleInProgress)
							{
								// See if the running item has a dependancy string
								if (((ScheduleItem)objItem).ObjectDependencies.Length > 0) 
								{ 
									// Running item has a dependancy string, see if it clashes with the item we want to add
									if (((ScheduleItem)objItem).HasObjectDependencies(objScheduleItem.ObjectDependencies)) 
									{ 
										// Dependancy clash
										objReturn = true; 
										break;
									} 
								} 
							}
                        } 
                        Interlocked.Increment(ref Reads); 
                    } 
                    finally 
					{ 
                        //  Ensure that the lock is released.
                        objInProgressReadWriteLock.ReleaseReaderLock(); 
                    } 
                } 
                catch (ApplicationException ex) 
				{ 
                    //  The reader lock request timed out.
                    Interlocked.Increment(ref ReaderTimeouts);
                    throw ex;
                } 
                return objReturn; 
            }

            public static ScheduleHistoryItem SaveScheduleHistory(ScheduleHistoryItem scheduleHistoryItem)
            {
                try
                {
                    // Add a history schedule to the log
                    SchedulingController objSchedulingController = new SchedulingController();
                    int intScheduleHistoryID = 0;
                    intScheduleHistoryID = objSchedulingController.SaveScheduleHistory(scheduleHistoryItem);
                    scheduleHistoryItem.Id = intScheduleHistoryID;
                    // Return the schedule history with new ID                    
                    return scheduleHistoryItem;
                }
                catch (Exception exc)
                {
                    //Exception.ProcessSchedulerException(exc); 
                    throw exc;
                }
                //return null;
 
            }

            //public static ScheduleHistoryItem AddScheduleHistory(ScheduleHistoryItem objScheduleHistoryItem) 
            //{ 
            //    try 
            //    { 
            //        // Add a history schedule to the log
            //        SchedulingController objSchedulingController = new SchedulingController(); 
            //        int intScheduleHistoryID = 0; 
            //        intScheduleHistoryID = objSchedulingController.AddScheduleHistory(objScheduleHistoryItem); 
            //        objScheduleHistoryItem.Id = intScheduleHistoryID; 
            //        // Return the schedule history with new ID                    
            //        return objScheduleHistoryItem; 
            //    } 
            //    catch (Exception exc) 
            //    { 
            //        //Exception.ProcessSchedulerException(exc); 
            //        throw exc;
            //    } 
            //    //return null;
            //} 
            
            //public static void UpdateScheduleHistory(ScheduleHistoryItem objScheduleHistoryItem) 
            //{ 
            //    try 
            //    {
            //        // Update history item
            //        SchedulingController objSchedulingController = new SchedulingController(); 
            //        objSchedulingController.UpdateScheduleHistory(objScheduleHistoryItem); 
            //    } 
            //    catch (Exception exc) 
            //    { 
            //        //Exception.ProcessSchedulerException(exc); 
            //        throw exc;
            //    } 
            //} 
        } 
    } 
} 
