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

using System.Reflection;

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Discovery.BusinessObjects;
using Discovery.Utility;

namespace Discovery.Scheduling
{
    public enum EventName
    {
        // do not add APPLICATION_END
        // it will not reliably complete
        APPLICATION_START,
    }


    public enum ScheduleSource
    {
        STARTED_FROM_EVENT,
        STARTED_FROM_TIMER,
    }


    public enum ScheduleStatus
    {
        WAITING_FOR_OPEN_THREAD,
        RUNNING_EVENT_SCHEDULE,
        RUNNING_TIMER_SCHEDULE,
        SHUTTING_DOWN,
        STOPPED,
    }

    public class ScheduleItem : PersistableBusinessObject
    {
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // This custom business object represents
        // a single item on the schedule.
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        //private int _ScheduleId
        private string _TypeFullName;
        private int _TimeLapse;
        private string _TimeLapseMeasurement;
        private int _RetryTimeLapse;
        private string _RetryTimeLapseMeasurement;
        private string _ObjectDependencies;
        private int _RetainHistoryNum;
        private DateTime _NextStart;
        private bool _CatchUpEnabled;
        private bool _Enabled;
        private string _AttachToEvent;
        private int _ThreadID;
        private int _ProcessGroup;
        private ScheduleSource _ScheduleSource;
        private Hashtable _ScheduleItemSettings;

        public ScheduleItem()
        {
            _ScheduleItemSettings = new Hashtable();
            //_ScheduleId = Null.NullInteger;
            _TypeFullName = Null.NullString;
            _TimeLapse = Null.NullInteger;
            _TimeLapseMeasurement = Null.NullString;
            _RetryTimeLapse = Null.NullInteger;
            _RetryTimeLapseMeasurement = Null.NullString;
            _ObjectDependencies = Null.NullString;
            _RetainHistoryNum = Null.NullInteger;
            _NextStart = Null.NullDate;
            _CatchUpEnabled = Null.NullBoolean;
            _Enabled = Null.NullBoolean;
            _AttachToEvent = Null.NullString;
            _ThreadID = Null.NullInteger;
            _ProcessGroup = Null.NullInteger;
        }

        //public int ScheduleId
        //{
        //    get
        //    {
        //        return _ScheduleId;
        //    } 
        //    set
        //    {
        //        _ScheduleId = value;
        //    }
        //}

        public string TypeFullName
        {
            get
            {
                return _TypeFullName;
            }
            set
            {
                _TypeFullName = value;
            }
        }

        public int TimeLapse
        {
            get
            {
                return _TimeLapse;
            }
            set
            {
                _TimeLapse = value;
            }
        }
        public string TimeLapseMeasurement
        {
            get
            {
                return _TimeLapseMeasurement;
            }
            set
            {
                _TimeLapseMeasurement = value;
            }
        }
        public int RetryTimeLapse
        {
            get
            {
                return _RetryTimeLapse;
            }
            set
            {
                _RetryTimeLapse = value;
            }
        }
        public string RetryTimeLapseMeasurement
        {
            get
            {
                return _RetryTimeLapseMeasurement;
            }
            set
            {
                _RetryTimeLapseMeasurement = value;
            }
        }
        public string ObjectDependencies
        {
            get
            {
                return _ObjectDependencies;
            }
            set
            {
                _ObjectDependencies = value;
            }
        }

        public int RetainHistoryNum
        {
            get
            {
                return _RetainHistoryNum;
            }
            set
            {
                _RetainHistoryNum = value;
            }
        }

        public DateTime NextStart
        {
            get
            {
                return _NextStart;
            }
            set
            {
                _NextStart = value;
            }
        }
        public ScheduleSource ScheduleSource
        {
            get
            {
                return _ScheduleSource;
            }
            set
            {
                _ScheduleSource = value;
            }
        }

        public bool CatchUpEnabled
        {
            get
            {
                return _CatchUpEnabled;
            }
            set
            {
                _CatchUpEnabled = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }

        public string AttachToEvent
        {
            get
            {
                return _AttachToEvent;
            }
            set
            {
                _AttachToEvent = value;
            }
        }

        public int ThreadID
        {
            get
            {
                return _ThreadID;
            }
            set
            {
                _ThreadID = value;
            }
        }

        public int ProcessGroup
        {
            get
            {
                return _ProcessGroup;
            }
            set
            {
                _ProcessGroup = value;
            }
        }

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


        public void AddSetting(string Key, string Value)
        {
            _ScheduleItemSettings.Add(Key, Value);
        }

        public string GetSetting(string Key)
        {
            if (_ScheduleItemSettings.ContainsKey(Key))
            {
                return Convert.ToString(_ScheduleItemSettings[Key]);
            }
            else
            {
                return "";
            }
        }

        public Hashtable GetSettings()
        {
            return _ScheduleItemSettings;
        }

        public void SetSettings(Hashtable Settings)
        {
            _ScheduleItemSettings = Settings;
        }

    }

    public class ScheduleQueueItem : Scheduling.ScheduleItem
    {

    }

    public class ScheduleHistoryItem : Scheduling.ScheduleItem
    {
        //private int _ScheduleHistoryID;
        private int _ScheduleId;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private bool _Succeeded;
        private System.Text.StringBuilder _LogNotes;

        public ScheduleHistoryItem()
        {
            //_ScheduleHistoryID = Null.NullInteger;
            _ScheduleId = Null.NullInteger;
            _StartDate = Null.NullDate;
            _EndDate = Null.NullDate;
            _Succeeded = Null.NullBoolean;
            _LogNotes = new System.Text.StringBuilder();
        }

        //public int ScheduleHistoryID
        //{
        //    get
        //    {
        //        return _ScheduleHistoryID;
        //    }
        //    set
        //    {
        //        _ScheduleHistoryID = value;
        //    }
        //}

        public int ScheduleId
        {
            get
            {
                return _ScheduleId;
            }
            set
            {
                _ScheduleId = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        public bool Overdue
        {
            get
            {
                if (NextStart < DateTime.Now && EndDate == Null.NullDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public double OverdueBy
        {
            get
            {
                double overdueByReturn = 0;
                try
                {
                    if (NextStart <= DateTime.Now && EndDate == Null.NullDate)
                    {
                        return DateTime.Now.Subtract(NextStart).TotalSeconds;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    overdueByReturn = 0;
                }
                return overdueByReturn;
            }
        }

        public double RemainingTime
        {
            get
            {
                double remainingTimeReturn = 0;
                try
                {
                    if ((NextStart > DateTime.Now) && EndDate == Null.NullDate)
                    {
                        return NextStart.Subtract(DateTime.Now).TotalSeconds;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    remainingTimeReturn = 0;
                }
                return remainingTimeReturn;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }
        public double ElapsedTime
        {
            get
            {
                double elapsedTimeReturn = 0;
                try
                {
                    if (_EndDate == Null.NullDate && _StartDate != Null.NullDate)
                    {
                        return DateTime.Now.Subtract(_StartDate).TotalSeconds;
                    }
                    else if (_StartDate != Null.NullDate)
                    {
                        return _EndDate.Subtract(_StartDate).TotalSeconds;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    elapsedTimeReturn = 0;
                }
                return elapsedTimeReturn;
            }
        }
        public bool Succeeded
        {
            get
            {
                return _Succeeded;
            }
            set
            {
                _Succeeded = value;
            }
        }
        public string LogNotes
        {
            get
            {
                return _LogNotes.ToString();
            }
            set
            {
                _LogNotes = new System.Text.StringBuilder(value);
            }
        }


        public void AddLogNote(string Notes)
        {
            _LogNotes.Append(Notes + Environment.NewLine);
        }


    }

    public class SchedulerClient
    {

        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // This class is inherited by any class that wants
        // to run tasks in the scheduler.
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        public event WorkStarted ProcessStarted;
        public event WorkProgressing ProcessProgressing;
        public event WorkCompleted ProcessCompleted;
        public event WorkErrored ProcessErrored;

        public void Started()
        {
            if (null != ProcessStarted) ProcessStarted(this);
        }

        public void Progressing()
        {
            if (null != ProcessProgressing) ProcessProgressing(this);
        }

        public void Completed()
        {
            if (null != ProcessCompleted) ProcessCompleted(this);
        }

        public void Errored(ref Exception objException)
        {
            if (null != ProcessErrored) ProcessErrored(this, objException);
        }

        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        // This is the sub that kicks off the actual
        // work within the SchedulerClient's subclass
        // ''''''''''''''''''''''''''''''''''''''''''''''''''
        public virtual void DoWork()
        {
        }

        private string _SchedulerEventGUID;
        private string _ProcessMethod;
        private string _Status;
        private ScheduleHistoryItem _ScheduleHistoryItem;

        public SchedulerClient()
        {
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            // Assign the event a unique ID for tracking purposes.
            // ''''''''''''''''''''''''''''''''''''''''''''''''''
            _SchedulerEventGUID = Null.NullString;
            _ProcessMethod = Null.NullString;
            _Status = Null.NullString;
            _ScheduleHistoryItem = new ScheduleHistoryItem();
        }

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

        public int ThreadID
        {
            get
            {
                return AppDomain.GetCurrentThreadId();
            }
        }
    }

    public class PurgeScheduleHistory : Scheduling.SchedulerClient
    {
        public PurgeScheduleHistory(Scheduling.ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {

            this.ScheduleHistoryItem = objScheduleHistoryItem;
        }

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
                //Exceptions.LogException(exc);
                throw exc;
            }
        }
    }

    public class SampleScheduleTask : Scheduling.SchedulerClient
    {

        public SampleScheduleTask(Scheduling.ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            this.ScheduleHistoryItem = objScheduleHistoryItem;
        }

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

                this.ScheduleHistoryItem.AddLogNote("Test done.  Waited " + SleepFor.ToString() + " milliseconds."); // OPTIONAL
            }
            catch (Exception exc)
            {
                // REQUIRED
                this.ScheduleHistoryItem.Succeeded = false;

                this.ScheduleHistoryItem.AddLogNote("EXCEPTION: " + exc.ToString()); // OPTIONAL

                // notification that we have errored
                this.Errored(ref exc); // REQUIRED

                // log the exception
                //Exceptions.LogException(exc); // OPTIONAL
                throw exc;
            }
        }
    }
}

