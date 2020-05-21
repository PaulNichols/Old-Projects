using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartWorkflowRuntime();
        }

        private void StartWorkflowRuntime()
        {
            // Create a new Workflow Runtime for this application
            _WFRuntime = new WorkflowRuntime();

            // Create EventHandlers for the WorkflowRuntime
            _WFRuntime.WorkflowTerminated += new EventHandler<WorkflowTerminatedEventArgs>(WorkflowRuntime_WorkflowTerminated);
            _WFRuntime.WorkflowCompleted += new EventHandler<WorkflowCompletedEventArgs>(WorkflowRuntime_WorkflowCompleted);



            // Add a new instance of the OrderService to the runtime
            ExternalDataExchangeService dataService = new ExternalDataExchangeService();
            _WFRuntime.AddService(dataService);
            _OrderService = new OrderLocalServices.OrderService();
            dataService.AddService(_OrderService);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}