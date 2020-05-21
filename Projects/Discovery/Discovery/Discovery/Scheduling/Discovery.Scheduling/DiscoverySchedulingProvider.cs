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

using System; 
using System.IO; 
using System.Web; 
using System.Xml; 
using System.Reflection; 
using System.Threading; 
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;

namespace Discovery.Scheduling 
{
    // This is the provider specified via web.config, when Invoke is called on the constructor, execution continues here
    public class DiscoverySchedulingProvider : Discovery.Scheduling.SchedulingProvider 
	{ 
        //private static Process p = new Process(); 

		public DiscoverySchedulingProvider()
		{
		}
        
        private const string ProviderType = "schedulingprovider"; 
        
        public override string GetProviderPath() 
		{ 
            return ProviderPath; 
        } 
        
        public override void Start() 
		{ 
            if (Enabled) 
			{ 
                Scheduler.CoreScheduler s = new Scheduler.CoreScheduler(); 
                Scheduler.CoreScheduler.KeepRunning = true; 
                Scheduler.CoreScheduler.Start(); 
            } 
        } 
        
        public override void ReStart(string SourceOfRestart) 
		{ 
            Halt(SourceOfRestart); 
            StartAndWaitForResponse(); 
        } 
        
        public override void StartAndWaitForResponse() 
		{ 
            if (Enabled) 
			{ 
				Scheduler.CoreScheduler s = new Scheduler.CoreScheduler(); 
				Scheduler.CoreScheduler.KeepRunning = true; 
                Scheduler.CoreScheduler.StartAndWaitForResponse(); 
            } 
        } 
        
        
        public override void Halt(string SourceOfHalt) 
		{ 
			Scheduler.CoreScheduler s = new Scheduler.CoreScheduler(); 
			Scheduler.CoreScheduler.KeepRunning = false; 
            Scheduler.CoreScheduler.Halt(SourceOfHalt); 
            Scheduler.CoreScheduler.KeepRunning = false; 
        } 
        
        public override void PurgeScheduleHistory() 
		{ 
            Scheduler.CoreScheduler s = new Scheduler.CoreScheduler(); 
            Scheduler.CoreScheduler.PurgeScheduleHistory(); 
        } 

        /*
		public override void RunApplicationStartEventSchedule() 
		{
			RunEventSchedule(Scheduling.EventName.APPLICATION_START);
		}
		*/

        public override void RunEventSchedule(Discovery.Scheduling.EventName objEventName) 
		{ 
            if (Enabled) 
			{ 
				Scheduler.CoreScheduler s = new Scheduler.CoreScheduler(); 
				Scheduler.CoreScheduler.RunEventSchedule(objEventName); 
            } 
        } 
        
        public override List<ScheduleItem> GetSchedule() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetSchedule(); 
        } 
        
        public override ScheduleItem GetSchedule(int ScheduleID) 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetSchedule(ScheduleID); 
        } 
        
        public override ScheduleHistoryItem GetScheduleHistory(int ScheduleID) 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetScheduleHistory(ScheduleID); 
        } 
        
        public override ArrayList GetScheduleQueue() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetScheduleQueue(); 
        } 
        
        public override ArrayList GetScheduleProcessing() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetScheduleProcessing(); 
        } 
        
        public override ScheduleStatus GetScheduleStatus() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetScheduleStatus(); 
        }

        public override int SaveSchedule(ScheduleItem scheduleItem)
        {
            return Discovery.Scheduling.SchedulingController.SaveSchedule(scheduleItem);
        } 
        
        //public override int AddSchedule(ScheduleItem objScheduleItem) 
        //{ 
        //    Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
        //    return s.AddSchedule(objScheduleItem.TypeFullName, objScheduleItem.TimeLapse, objScheduleItem.TimeLapseMeasurement, objScheduleItem.RetryTimeLapse, objScheduleItem.RetryTimeLapseMeasurement, objScheduleItem.RetainHistoryNum, objScheduleItem.AttachToEvent, objScheduleItem.CatchUpEnabled, objScheduleItem.Enabled, objScheduleItem.ObjectDependencies); 
        //} 
        
        
        //public override void UpdateSchedule(ScheduleItem objScheduleItem) 
        //{ 
        //    Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
        //    s.UpdateSchedule(objScheduleItem.Id, objScheduleItem.TypeFullName, objScheduleItem.TimeLapse, objScheduleItem.TimeLapseMeasurement, objScheduleItem.RetryTimeLapse, objScheduleItem.RetryTimeLapseMeasurement, objScheduleItem.RetainHistoryNum, objScheduleItem.AttachToEvent, objScheduleItem.CatchUpEnabled, objScheduleItem.Enabled, objScheduleItem.ObjectDependencies); 
        //} 
        
        public override bool DeleteSchedule(ScheduleItem objScheduleItem) 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.DeleteSchedule(objScheduleItem); 
        } 
        
        public override int GetFreeThreadCount() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetFreeThreadCount(); 
        } 
        
        public override int GetActiveThreadCount() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetActiveThreadCount(); 
        } 
        
        public override int GetMaxThreadCount() 
		{ 
            Discovery.Scheduling.SchedulingController s = new Discovery.Scheduling.SchedulingController(); 
            return s.GetMaxThreadCount(); 
        }
 
    } 
} 

