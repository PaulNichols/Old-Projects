using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace WorkflowConsoleApplication2
{
    public sealed partial class Workflow1
    {
		#region Designer generated code
        
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.NotValid = new System.Workflow.Activities.StateActivity();
            this.Complete = new System.Workflow.Activities.StateActivity();
            this.Valid = new System.Workflow.Activities.StateActivity();
            this.Live = new System.Workflow.Activities.StateActivity();
            this.Printed = new System.Workflow.Activities.StateActivity();
            this.Held = new System.Workflow.Activities.StateActivity();
            this.Cancelled = new System.Workflow.Activities.StateActivity();
            this.WaitingForOrderState = new System.Workflow.Activities.StateActivity();
            this.eventDrivenActivity1 = new System.Workflow.Activities.EventDrivenActivity();
            // 
            // NotValid
            // 
            this.NotValid.Name = "NotValid";
            // 
            // Complete
            // 
            this.Complete.Name = "Complete";
            // 
            // Valid
            // 
            this.Valid.Name = "Valid";
            // 
            // Live
            // 
            this.Live.Name = "Live";
            // 
            // Printed
            // 
            this.Printed.Name = "Printed";
            // 
            // Held
            // 
            this.Held.Name = "Held";
            // 
            // Cancelled
            // 
            this.Cancelled.Name = "Cancelled";
            // 
            // WaitingForOrderState
            // 
            this.WaitingForOrderState.Activities.Add(this.eventDrivenActivity1);
            this.WaitingForOrderState.Name = "WaitingForOrderState";
            // 
            // eventDrivenActivity1
            // 
            this.eventDrivenActivity1.Name = "eventDrivenActivity1";
            // 
            // Workflow1
            // 
            this.Activities.Add(this.NotValid);
            this.Activities.Add(this.Complete);
            this.Activities.Add(this.Valid);
            this.Activities.Add(this.Live);
            this.Activities.Add(this.Printed);
            this.Activities.Add(this.Held);
            this.Activities.Add(this.Cancelled);
            this.Activities.Add(this.WaitingForOrderState);
            this.CompletedStateName = "stateActivity1";
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "WaitingForOrderState";
            this.Name = "Workflow1";
            this.CanModifyActivities = false;

        }

        #endregion

        private HandleExternalEventActivity handleExternalEventActivity1;
        private StateActivity Complete;
        private StateActivity Valid;
        private StateActivity Live;
        private StateActivity Printed;
        private StateActivity Held;
        private StateActivity Cancelled;
        private StateActivity WaitingForOrderState;
        private EventDrivenActivity eventDrivenActivity1;
        private StateActivity NotValid;






    }
}
