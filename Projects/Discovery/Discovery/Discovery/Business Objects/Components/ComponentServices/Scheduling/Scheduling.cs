/*************************************************************************************************
 ** FILE:	Scheduling.cs
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

using System.Reflection; 

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LASPortal
{
	namespace Scheduling 
	{
        /// <summary>
        /// An enumeration to include event name APPLICATION_START
        /// </summary>
        public enum EventName 
		{ 
            // do not add APPLICATION_END
			// it will not reliably complete
            /// <summary>
            /// APPLICATION_START of EventName
            /// </summary>
			APPLICATION_START,
		}


        /// <summary>
        /// An enumeration to include the schedule source STARTED_FROM_EVENT and STARTED_FROM_TIMER
        /// </summary>
        public enum ScheduleSource 
		{
            /// <summary>
            /// STARTED_FROM_EVENT of ScheduleSource
            /// </summary>
            STARTED_FROM_EVENT,
            /// <summary>
            /// STARTED_FROM_TIMER of ScheduleSource
            /// </summary>
            STARTED_FROM_TIMER,
		}


        /// <summary>
        /// An enumeration to include the schedule status
        /// </summary>
        public enum ScheduleStatus 
		{
            /// <summary>
            /// WAITING_FOR_OPEN_THREAD of ScheduleStatus
            /// </summary>
            WAITING_FOR_OPEN_THREAD,
            /// <summary>
            /// RUNNING_EVENT_SCHEDULE of ScheduleStatus
            /// </summary>
            RUNNING_EVENT_SCHEDULE,
            /// <summary>
            /// RUNNING_TIMER_SCHEDULE of ScheduleStatus
            /// </summary>
            RUNNING_TIMER_SCHEDULE,
            /// <summary>
            /// SHUTTING_DOWN of ScheduleStatus
            /// </summary>
            SHUTTING_DOWN,
            /// <summary>
            /// STOPPED of ScheduleStatus
            /// </summary>
            STOPPED,
		} 
        
        /// <summary>
        /// the class holds the schedule item details
        /// </summary>
		public class ScheduleItem  
		{ 
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			// This custom business object represents
			// a single item on the schedule.
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			private System.Nullable<int> scheduleID; 
			private string typeFullName; 
			private System.Nullable<int> timeLapse; 
			private string timeLapseMeasurement; 
			private System.Nullable<int> retryTimeLapse; 
			private string retryTimeLapseMeasurement; 
			private string objectDependencies; 
			private System.Nullable<int> retainHistoryNum; 
			private System.Nullable<DateTime> nextStart; 
			private System.Nullable<bool> catchUpEnabled; 
			private System.Nullable<bool> enabled; 
			private string attachToEvent; 
			private System.Nullable<int> threadID; 
			private System.Nullable<int> processGroup; 
			private ScheduleSource scheduleSource; 
			private Hashtable scheduleItemSettings;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:ScheduleItem"/> class.
            /// </summary>
			public ScheduleItem() 
			{ 
				scheduleItemSettings = new Hashtable(); 
				scheduleID = null; 
				typeFullName = null;
                timeLapse = null;
                timeLapseMeasurement = null;
                retryTimeLapse = null;
                retryTimeLapseMeasurement = null;
                objectDependencies = null;
                retainHistoryNum = null;
                nextStart = null;
                catchUpEnabled = null;
                enabled = null;
                attachToEvent = null;
                threadID = null;
                processGroup = null; 
			}

            /// <summary>
            /// Gets or sets the schedule ID.
            /// </summary>
            /// <value>The schedule ID.</value>
			public System.Nullable<int> ScheduleID 
			{ 
				get 
				{ 
					return scheduleID; 
				} 
				set 
				{ 
					scheduleID = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the full name of the type.
            /// </summary>
            /// <value>The full name of the type.</value>
			public string TypeFullName 
			{ 
				get 
				{ 
					return typeFullName; 
				} 
				set 
				{ 
					typeFullName = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the time lapse.
            /// </summary>
            /// <value>The time lapse.</value>
			public System.Nullable<int> TimeLapse 
			{ 
				get 
				{ 
					return timeLapse; 
				} 
				set 
				{ 
					timeLapse = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the time lapse measurement.
            /// </summary>
            /// <value>The time lapse measurement.</value>
			public string TimeLapseMeasurement 
			{ 
				get 
				{ 
					return timeLapseMeasurement; 
				} 
				set 
				{ 
					timeLapseMeasurement = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the retry time lapse.
            /// </summary>
            /// <value>The retry time lapse.</value>
			public System.Nullable<int> RetryTimeLapse 
			{ 
				get 
				{ 
					return retryTimeLapse; 
				} 
				set 
				{ 
					retryTimeLapse = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the retry time lapse measurement.
            /// </summary>
            /// <value>The retry time lapse measurement.</value>
			public string RetryTimeLapseMeasurement 
			{ 
				get 
				{ 
					return retryTimeLapseMeasurement; 
				} 
				set 
				{ 
					retryTimeLapseMeasurement = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the object dependencies.
            /// </summary>
            /// <value>The object dependencies.</value>
			public string ObjectDependencies 
			{ 
				get 
				{ 
					return objectDependencies; 
				} 
				set 
				{ 
					objectDependencies = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the retain history num.
            /// </summary>
            /// <value>The retain history num.</value>
			public System.Nullable<int> RetainHistoryNum 
			{ 
				get 
				{ 
					return retainHistoryNum; 
				} 
				set 
				{ 
					retainHistoryNum = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the next start.
            /// </summary>
            /// <value>The next start.</value>
			public System.Nullable<DateTime> NextStart 
			{ 
				get 
				{ 
					return nextStart; 
				} 
				set 
				{ 
					nextStart = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the schedule source.
            /// </summary>
            /// <value>The schedule source.</value>
			public ScheduleSource ScheduleSource 
			{ 
				get 
				{ 
					return scheduleSource; 
				} 
				set 
				{ 
					scheduleSource = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the catch up enabled.
            /// </summary>
            /// <value>The catch up enabled.</value>
			public System.Nullable<bool> CatchUpEnabled 
			{ 
				get 
				{ 
					return catchUpEnabled; 
				} 
				set 
				{ 
					catchUpEnabled = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the enabled.
            /// </summary>
            /// <value>The enabled.</value>
			public System.Nullable<bool> Enabled 
			{ 
				get 
				{ 
					return enabled; 
				} 
				set 
				{ 
					enabled = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the attach to event.
            /// </summary>
            /// <value>The attach to event.</value>
			public string AttachToEvent 
			{ 
				get 
				{ 
					return attachToEvent; 
				} 
				set 
				{ 
					attachToEvent = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the thread ID.
            /// </summary>
            /// <value>The thread ID.</value>
			public System.Nullable<int> ThreadID 
			{ 
				get 
				{ 
					return threadID; 
				} 
				set 
				{ 
					threadID = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the process group.
            /// </summary>
            /// <value>The process group.</value>
			public System.Nullable<int> ProcessGroup 
			{ 
				get 
				{ 
					return processGroup; 
				} 
				set 
				{ 
					processGroup = value; 
				} 
			}

            /// <summary>
            /// Determines whether [has object dependencies] [the specified STR object dependencies].
            /// </summary>
            /// <param name="strObjectDependencies">The STR object dependencies.</param>
            /// <returns>
            /// 	<c>true</c> if [has object dependencies] [the specified STR object dependencies]; otherwise, <c>false</c>.
            /// </returns>
			public bool HasObjectDependencies(string strObjectDependencies) 
			{ 
				// See if we need to test against multiple dependancies (passed in)
				if (strObjectDependencies.IndexOf(",") > -1) 
				{ 
					string[] a = null; 
					a = strObjectDependencies.ToLower().Split(','); 
					for (int i = 0; i < a.Length; i++) 
					{ 
						// See if the passed dependancy clashes with this item's dependancies
						if (ObjectDependencies.ToLower().IndexOf(a[i]) > -1)
						{ 
							return true; 
						} 
					} 
				} 
				else 
				{ 
					if (ObjectDependencies.ToLower().IndexOf(strObjectDependencies) > -1)
					{ 
						return true; 
					} 
				} 
				return false; 
			}


            /// <summary>
            /// Adds the setting.
            /// </summary>
            /// <param name="Key">The key.</param>
            /// <param name="Value">The value.</param>
			public void AddSetting(string Key, string Value) 
			{ 
				scheduleItemSettings.Add(Key, Value); 
			}

            /// <summary>
            /// Gets the setting.
            /// </summary>
            /// <param name="Key">The key.</param>
            /// <returns></returns>
			public string GetSetting(string Key) 
			{ 
				if (scheduleItemSettings.ContainsKey(Key)) 
				{ 
					return Convert.ToString(scheduleItemSettings[Key]); 
				} 
				else 
				{ 
					return ""; 
				} 
			}

            /// <summary>
            /// Gets the settings.
            /// </summary>
            /// <returns></returns>
			public Hashtable GetSettings() 
			{ 
				return scheduleItemSettings; 
			}

            /// <summary>
            /// Sets the settings.
            /// </summary>
            /// <param name="Settings">The settings.</param>
			public void SetSettings(Hashtable Settings) 
			{ 
				scheduleItemSettings = Settings; 
			} 
            
		} 
        
        /// <summary>
        /// The class is for the schedule queue item
        /// </summary>
		public class ScheduleQueueItem : Scheduling.ScheduleItem
		{ 
            
		} 
        
        /// <summary>
        /// The class holds the history of schedule item
        /// </summary>
		public class ScheduleHistoryItem : Scheduling.ScheduleItem
		{ 
			private System.Nullable<int> scheduleHistoryID; 
			private System.Nullable<DateTime> startDate; 
			private System.Nullable<DateTime> endDate; 
			private System.Nullable<bool> succeeded; 
			private System.Text.StringBuilder logNotes;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:ScheduleHistoryItem"/> class.
            /// </summary>
			public ScheduleHistoryItem() 
			{ 
				scheduleHistoryID = null;
                startDate = null;
                endDate = null;
                succeeded = null; 
				logNotes = new System.Text.StringBuilder(); 
			}

            /// <summary>
            /// Gets or sets the schedule history ID.
            /// </summary>
            /// <value>The schedule history ID.</value>
			public System.Nullable<int> ScheduleHistoryID 
			{ 
				get 
				{
                    return scheduleHistoryID; 
				} 
				set 
				{ 
					scheduleHistoryID = value; 
				} 
			}

            /// <summary>
            /// Gets or sets the start date.
            /// </summary>
            /// <value>The start date.</value>
			public System.Nullable<DateTime> StartDate 
			{ 
				get 
				{ 
					return startDate; 
				} 
				set 
				{ 
					startDate = value; 
				} 
			}

            /// <summary>
            /// Gets a value indicating whether this <see cref="T:ScheduleHistoryItem"/> is overdue.
            /// </summary>
            /// <value><c>true</c> if overdue; otherwise, <c>false</c>.</value>
			public bool Overdue 
			{ 
				get 
				{ 
					if (NextStart < DateTime.Now && EndDate == null) 
					{ 
						return true; 
					} 
					else 
					{ 
						return false; 
					} 
				} 
			}

            /// <summary>
            /// Gets the overdue by.
            /// </summary>
            /// <value>The overdue by.</value>
			public double OverdueBy 
			{ 
				get 
				{ 
					double overdueByReturn = 0;
					
					if (NextStart!=null && (NextStart <= DateTime.Now && EndDate == null) )
					{ 
						overdueByReturn= DateTime.Now.Subtract(NextStart.Value).TotalSeconds; 
					} 
											
					return overdueByReturn;
				} 
			}

            /// <summary>
            /// Gets the remaining time.
            /// </summary>
            /// <value>The remaining time.</value>
			public double RemainingTime 
			{ 
				get 
				{
                    double remainingTimeReturn = 0;

                    if (NextStart!=null && (NextStart > DateTime.Now) && EndDate == null) 
					{
                        remainingTimeReturn = NextStart.Value.Subtract(DateTime.Now).TotalSeconds; 
					} 
						
					
					return remainingTimeReturn;
				} 
			}

            /// <summary>
            /// Gets or sets the end date.
            /// </summary>
            /// <value>The end date.</value>
			public System.Nullable<DateTime> EndDate 
			{ 
				get 
				{ 
					return endDate; 
				} 
				set 
				{ 
					endDate = value; 
				} 
			}

            /// <summary>
            /// Gets the elapsed time.
            /// </summary>
            /// <value>The elapsed time.</value>
			public double ElapsedTime 
			{ 
				get 
				{ 
					double elapsedTimeReturn = 0;
					
					if (endDate == null && startDate != null) 
					{ 
						elapsedTimeReturn= DateTime.Now.Subtract(startDate.Value).TotalSeconds; 
					}
                    else if (endDate != null && startDate != null) 
					{ 
						elapsedTimeReturn= endDate.Value.Subtract(startDate.Value).TotalSeconds; 
					} 						
				
					return elapsedTimeReturn;
				} 
			}

            /// <summary>
            /// Gets or sets the succeeded.
            /// </summary>
            /// <value>The succeeded.</value>
			public System.Nullable<bool> Succeeded 
			{ 
				get 
				{ 
					return succeeded; 
				} 
				set 
				{ 
					succeeded = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the log notes.
            /// </summary>
            /// <value>The log notes.</value>
			public string LogNotes 
			{ 
				get 
				{ 
					return logNotes.ToString(); 
				} 
				set 
				{ 
					logNotes = new System.Text.StringBuilder(value); 
				} 
			}


            /// <summary>
            /// Adds the log note.
            /// </summary>
            /// <param name="Notes">The notes.</param>
			public void AddLogNote(string Notes) 
			{ 
				logNotes.Append(Notes + Environment.NewLine); 
			} 
            
            
		} 
        
        /// <summary>
        /// The class hold the client scheduler details
        /// </summary>
		public class SchedulerClient  
		{ 
            
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			// This class is inherited by any class that wants
			// to run tasks in the scheduler.
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
            /// <summary>
            /// An event to indicate that the process has started
            /// </summary>
			public event WorkStarted ProcessStarted;
            /// <summary>
            /// An event to indicate that the process is being processed
            /// </summary>
            public event WorkProgressing ProcessProgressing;
            /// <summary>
            /// An event to indicate that the process has completed
            /// </summary>
            public event WorkCompleted ProcessCompleted;
            /// <summary>
            /// An event to indicate that the process is failure
            /// </summary>
            public event WorkErrored ProcessErrored;

            /// <summary>
            /// Starteds this instance.
            /// </summary>
			public void Started() 
			{ 
				if (null != ProcessStarted) ProcessStarted(this); 
			}

            /// <summary>
            /// Progressings this instance.
            /// </summary>
			public void Progressing() 
			{ 
				if (null != ProcessProgressing) ProcessProgressing(this); 
			}

            /// <summary>
            /// Completeds this instance.
            /// </summary>
			public void Completed() 
			{ 
				if (null != ProcessCompleted) ProcessCompleted(this); 
			}

            /// <summary>
            /// Erroreds the specified obj exception.
            /// </summary>
            /// <param name="objException">The obj exception.</param>
			public void Errored(ref Exception objException) 
			{ 
				if (null != ProcessErrored) ProcessErrored(this, objException); 
			} 
            
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
			// This is the sub that kicks off the actual
			// work within the SchedulerClient's subclass
			// ''''''''''''''''''''''''''''''''''''''''''''''''''
            /// <summary>
            /// Does the work.
            /// </summary>
			public virtual void DoWork() 
			{ 
			} 
            
			private string _SchedulerEventGUID; 
			private string _ProcessMethod; 
			private string _Status; 
			private ScheduleHistoryItem _ScheduleHistoryItem;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:SchedulerClient"/> class.
            /// </summary>
			public SchedulerClient() 
			{ 
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				// Assign the event a unique ID for tracking purposes.
				// ''''''''''''''''''''''''''''''''''''''''''''''''''
				_SchedulerEventGUID = null; 
				_ProcessMethod = null; 
				_Status = null; 
				_ScheduleHistoryItem = new ScheduleHistoryItem(); 
			}

            /// <summary>
            /// Gets or sets the schedule history item.
            /// </summary>
            /// <value>The schedule history item.</value>
			public ScheduleHistoryItem ScheduleHistoryItem 
			{ 
				get 
				{ 
					return _ScheduleHistoryItem; 
				} 
				set 
				{ 
					_ScheduleHistoryItem = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the scheduler event GUID.
            /// </summary>
            /// <value>The scheduler event GUID.</value>
			public string SchedulerEventGUID 
			{ 
				get 
				{ 
					return _SchedulerEventGUID; 
				} 
				set 
				{ 
					_SchedulerEventGUID = value; 
				} 
			}
            /// <summary>
            /// Gets or sets a process method.
            /// </summary>
            /// <value>A process method.</value>
			public string aProcessMethod 
			{ 
				get 
				{ 
					return _ProcessMethod; 
				} 
				set 
				{ 
					_ProcessMethod = value; 
				} 
			}
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
			public string Status 
			{ 
				get 
				{ 
					return _Status; 
				} 
				set 
				{ 
					_Status = value; 
				} 
			}

            /// <summary>
            /// Gets the thread ID.
            /// </summary>
            /// <value>The thread ID.</value>
			public int ThreadID 
			{ 
				get 
				{ 
					return AppDomain.GetCurrentThreadId(); 
				} 
			} 
		} 
        
        /// <summary>
        /// The class is to purge the schedule history
        /// </summary>
		public class PurgeScheduleHistory : Scheduling.SchedulerClient 
		{
            /// <summary>
            /// Initializes a new instance of the <see cref="T:PurgeScheduleHistory"/> class.
            /// </summary>
            /// <param name="objScheduleHistoryItem">The obj schedule history item.</param>
			public PurgeScheduleHistory(Scheduling.ScheduleHistoryItem objScheduleHistoryItem) : base() 
			{ 
                
				this.ScheduleHistoryItem = objScheduleHistoryItem; 
			}

            /// <summary>
            /// Does the work.
            /// </summary>
			public override void DoWork() 
			{ 
				try 
				{ 
					// Notification that the event is progressing
					this.Progressing();
	
					// Purge schedule history
					SchedulingProvider.Instance().PurgeScheduleHistory(); 
                    
					// Update the result to success since no exception was thrown
					this.ScheduleHistoryItem.Succeeded = true; 
					this.ScheduleHistoryItem.AddLogNote("Purged Schedule History"); 
				} 
				catch (Exception exc) 
				{ 
					this.ScheduleHistoryItem.Succeeded = false; 
					this.ScheduleHistoryItem.AddLogNote("EXCEPTION: " + exc.ToString()); 
                    
					// Notification that we have errored
					this.Errored(ref exc); 
                    
					// Log the exception
                    //TODO: Throw exception
				//	Exceptions.LogException(exc); 
				} 
			} 
		} 
        
        /// <summary>
        /// The class is for a sample schedule task
        /// </summary>
		public class SampleScheduleTask : Scheduling.SchedulerClient 
		{

            /// <summary>
            /// Initializes a new instance of the <see cref="T:SampleScheduleTask"/> class.
            /// </summary>
            /// <param name="objScheduleHistoryItem">The obj schedule history item.</param>
			public SampleScheduleTask(Scheduling.ScheduleHistoryItem objScheduleHistoryItem) : base() 
			{ 
				this.ScheduleHistoryItem = objScheduleHistoryItem;
			}

            /// <summary>
            /// Does the work.
            /// </summary>
			public override void DoWork() 
			{ 
				try 
				{ 
					// Notification that the event is progressing
					this.Progressing();
                    
					// optional settings are stored in the
					// ScheduleSettings Table
					int SleepFor = Convert.ToInt32(this.ScheduleHistoryItem.GetSetting("Milliseconds")); 
					System.Threading.Thread.Sleep(SleepFor); 
                    
					this.ScheduleHistoryItem.Succeeded = true;
                    
					this.ScheduleHistoryItem.AddLogNote("ExceptionMessage done.  Waited " + SleepFor.ToString() + " milliseconds."); // OPTIONAL
				} 
				catch (Exception exc) 
				{ 
					// REQUIRED
					this.ScheduleHistoryItem.Succeeded = false;
                    
					this.ScheduleHistoryItem.AddLogNote("EXCEPTION: " + exc.ToString()); // OPTIONAL
                    
					// notification that we have errored
					this.Errored(ref exc); // REQUIRED
                    
					// TODO: log the exception
				//	Exceptions.LogException(exc); // OPTIONAL
				} 
			} 
		} 
	} 
} 

