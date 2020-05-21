/*************************************************************************************************
 ** FILE:	Processgroup.cs
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
using System.Data;
using System.Diagnostics;
using System.Security.Principal;


namespace Discovery.Scheduling 
{
    public class ProcessGroup  
	{ 
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // This class represents a process group for
        // our threads to run in.
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        private static int numberOfProcessesInQueue = 0; 
        private static int numberOfProcesses = 0; 
        private static int processesCompleted = 0; 
        private static int ticksElapsed = 0;
 
		// History item if running via a normal thread
		private ScheduleHistoryItem historyItem = null;

        public static int GetTicksElapsed 
		{ 
            get 
			{ 
                return ticksElapsed; 
            } 
        } 
        
        public static int GetProcessesCompleted 
		{ 
            get 
			{ 
                return processesCompleted; 
            } 
        } 
        
        public static int GetProcessesInQueue 
		{ 
            get 
			{ 
                return numberOfProcessesInQueue; 
            } 
        } 
        
        public delegate void CompletedEventHandler();

		// Our completed event
        public event CompletedEventHandler Completed; 
        
        public void Run(ScheduleHistoryItem objScheduleHistoryItem) 
		{ 
			try 
			{ 
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
                // This is called from RunPooledThread()
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                ticksElapsed = Environment.TickCount - ticksElapsed; 
				// This is what we're running
				SchedulerClient Process = null; 
				// Create an instance of our SchedulerClient derived class using Reflection
				Process = GetSchedulerClient(objScheduleHistoryItem.TypeFullName, objScheduleHistoryItem); 
				// Store the schedule info/item in the instance                
				Process.ScheduleHistoryItem = objScheduleHistoryItem; 

				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				// Set up the handlers to the CoreScheduler from our instance
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				Process.ProcessStarted		+= new WorkStarted(Scheduler.CoreScheduler.WorkStarted); 
				Process.ProcessProgressing	+= new WorkProgressing(Scheduler.CoreScheduler.WorkProgressing); 
				Process.ProcessCompleted	+= new WorkCompleted(Scheduler.CoreScheduler.WorkCompleted); 
				Process.ProcessErrored		+= new WorkErrored(Scheduler.CoreScheduler.WorkErrored); 
                
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				// This kicks off the DoWork method of the class
				// type specified in the configuration.
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				Process.Started(); 
                
				// Do the actual work
				try 
				{
					// Do the actual work
					Process.DoWork(); 
				} 
				catch (Exception exc) 
				{ 
					// In case the scheduler client
					// didn't have proper exception handling
					// make sure we fire the Errored event
					Process.ScheduleHistoryItem.Succeeded = false; 
					Process.Errored(ref exc); 
				}
                
				if (Process.ScheduleHistoryItem.Succeeded) 
				{ 
                    Process.Completed(); 
                } 

				// ''''''''''''''''''''''''''''''''''''''''''''''''''
                // If all processes in this ProcessGroup have
                // completed, set the ticksElapsed and raise
                // the Completed event.
                // I don't think this is necessary with the
                // other events.  I'll leave it for now and
                // will probably take it out later.
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                if (processesCompleted == numberOfProcesses) 
				{ 
                    ticksElapsed = (Environment.TickCount - ticksElapsed); 
					if (null != Completed) 
					{
						Completed(); 
					}
                } 
            } 
            catch (Exception exc) 
			{ 
                //Exceptions.ProcessSchedulerException(exc); 
                throw exc;
            } 
            finally 
			{ 
				// See if we need to undo impersonation
                //if (null != impersonationContext)
                //{
                //    Globals.UndoImpersonation(impersonationContext);
                //}

				// ''''''''''''''''''''''''''''''''''''''''''''''''''
                // Track how many processes have completed for
                // this instanciation of the ProcessGroup
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                numberOfProcessesInQueue -= 1; 
                processesCompleted++;
            } 
        } 
        
        private SchedulerClient GetSchedulerClient(string strProcess, ScheduleHistoryItem objScheduleHistoryItem) 
		{ 
            try 
			{ 
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                // This is a method to encapsulate returning
                // an object whose class inherits SchedulerClient.
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
				
				// Return value
				SchedulerClient objRetClient = null;
				// The constructor we've found
				System.Reflection.ConstructorInfo objConstructor = null; 

				// The type we need 
				Type t = Type.GetType(strProcess, true); 

				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				// Get the default constructor for the class
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				objConstructor = t.GetConstructor(System.Type.EmptyTypes);
				if (null != objConstructor)
				{
					// Found the default constructor
					objRetClient = (SchedulerClient)objConstructor.Invoke(null);
				}
				else
				{
					// ''''''''''''''''''''''''''''''''''''''''''''''''''
					// Get the parameterised constructor for the class
					// ''''''''''''''''''''''''''''''''''''''''''''''''''

					// Parameters we pass to the constructor
					ScheduleHistoryItem[] param = new ScheduleHistoryItem[1]; 
					param[0] = objScheduleHistoryItem; 
					// Parameter types we pass to the constructor
					Type[] types = new Type[1]; 
					types[0] = typeof(ScheduleHistoryItem); 
					// Get the parameterised contructor
					objConstructor = t.GetConstructor(types);
					// Call the constructor
					objRetClient = (SchedulerClient)objConstructor.Invoke(param); 
				}

                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                // Return an instance of the class as an object
                // ''''''''''''''''''''''''''''''''''''''''''''''''''
                return objRetClient;
            } 
            catch (Exception exc) 
			{ 
                //Exceptions.ProcessSchedulerException(exc); 
                throw exc;
            } 
			return null;
        } 
        
		// ''''''''''''''''''''''''''''''''''''''''''''''''''
        //  This subroutine is callback for Threadpool.QueueWorkItem.  This is the necessary
        //  subroutine signature for QueueWorkItem, and Run() is proper for creating a Thread
        //  so the two subroutines cannot be combined, so instead just call Run from here.
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        private void RunPooledThread(Object objScheduleHistoryItem) 
		{ 
            Run((ScheduleHistoryItem)objScheduleHistoryItem); 
        }
 
		private void RunStandardThread()
		{
			Run(this.historyItem);
		}
        
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // Add a queue request to Threadpool with a 
        // callback to RunPooledThread which calls Run()
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        public void AddQueueUserWorkItem(ScheduleItem s) 
		{ 
            numberOfProcessesInQueue++;
            numberOfProcesses++;
            
			// Create the history item
			ScheduleHistoryItem objHist = new ScheduleHistoryItem(); 
            objHist.TypeFullName = s.TypeFullName;
            objHist.Id = s.Id;
            objHist.TimeLapse = s.TimeLapse; 
            objHist.TimeLapseMeasurement = s.TimeLapseMeasurement; 
            objHist.RetryTimeLapse = s.RetryTimeLapse; 
            objHist.RetryTimeLapseMeasurement = s.RetryTimeLapseMeasurement; 
            objHist.ObjectDependencies = s.ObjectDependencies; 
            objHist.CatchUpEnabled = s.CatchUpEnabled; 
            objHist.Enabled = s.Enabled; 
            objHist.NextStart = s.NextStart; 
            objHist.ScheduleSource = s.ScheduleSource; 
            objHist.SetSettings(s.GetSettings()); 
            objHist.ThreadID = s.ThreadID; 
            objHist.ProcessGroup = s.ProcessGroup; 
            
            try 
			{
				// Use a standard thread to run the scheduled item iinstead of a pool thread
				// Store the history item
				this.historyItem = objHist;
				System.Threading.Thread threadItem = new System.Threading.Thread(new ThreadStart(this.RunStandardThread)); 
				threadItem.IsBackground = true; 
				threadItem.Start(); 

				/*
                // Create a callback to subroutine RunPooledThread, this will be called
				// by our thread pool thread
                System.Threading.WaitCallback callback = new System.Threading.WaitCallback(RunPooledThread); 
                //  And put in a request to ThreadPool to run the process.
                System.Threading.ThreadPool.QueueUserWorkItem(callback, (Object)objHist); 
				*/

				// Suspend this threads quantum for a second
				Thread.Sleep(1000); 
            } 
            catch (Exception exc) 
			{ 
                //Exceptions.ProcessSchedulerException(exc); 
                throw exc;
            } 
        } 
        
        public void RunSingleTask(ScheduleItem s) 
		{ 
            numberOfProcessesInQueue++;
            numberOfProcesses++;
            ScheduleHistoryItem obj = new ScheduleHistoryItem(); 
            
			obj.TypeFullName = s.TypeFullName;
            obj.Id = s.Id; 
            obj.TimeLapse = s.TimeLapse; 
            obj.TimeLapseMeasurement = s.TimeLapseMeasurement; 
            obj.RetryTimeLapse = s.RetryTimeLapse; 
            obj.RetryTimeLapseMeasurement = s.RetryTimeLapseMeasurement; 
            obj.ObjectDependencies = s.ObjectDependencies; 
            obj.CatchUpEnabled = s.CatchUpEnabled; 
            obj.Enabled = s.Enabled; 
            obj.NextStart = s.NextStart; 
            obj.ScheduleSource = s.ScheduleSource; 
            obj.SetSettings(s.GetSettings()); 
            obj.ThreadID = s.ThreadID; 
            obj.ProcessGroup = s.ProcessGroup; 
            
            try 
			{ 
                Run(obj); 
                Thread.Sleep(1000); 
            } 
            catch (Exception exc) 
			{ 
                //Exceptions.ProcessSchedulerException(exc); 
                throw exc;
            } 
        } 
    } 
} 
