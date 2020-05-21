using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Discovery.RequestManagement;

namespace Discovery.RequestManager.TestHarness
{
    public delegate void DiaplyMessageDelegate(string message);

    public partial class frmMain : Form
    {
        private RequestManagement.RequestController RequestController;

        public frmMain()
        {
            InitializeComponent();
        }

        public void DisplayMessage(string message)
        {
            lblStatus.Text = message;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Create an instance of the request manager controller
            RequestController = new Discovery.RequestManagement.RequestController();
            // Wire up events
            RequestController.StatusChanged += new Discovery.RequestManagement.RequestControllerStatusDelegate(RequestController_StatusChanged);
            // Display red light
            DisplayTrafficLight("red");
        }

        void RequestController_StatusChanged(Discovery.RequestManagement.RequestControllerStatus status, Exception lastError)
        {
            switch (status)
            {
                case RequestControllerStatus.Started:
                    {
                        if (null != lastError)
                        {
                            this.Invoke(new DiaplyMessageDelegate(DisplayMessage), "Started, " + lastError.Message);
                        }
                        else
                        {
                            this.Invoke(new DiaplyMessageDelegate(DisplayMessage), "Started.");
                        }
                        break;
                    }
                case RequestControllerStatus.Stopped:
                    {
                        if (null != lastError)
                        {
                            this.Invoke(new DiaplyMessageDelegate(DisplayMessage), "Stopped, " + lastError.Message);
                        }
                        else
                        {
                            this.Invoke(new DiaplyMessageDelegate(DisplayMessage), "Stopped.");
                        }
                        break;
                    }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Display off yellow
                DisplayTrafficLight("yellow");

                // Start the controller
                RequestController.Start();

                // Display appropriate light colour
                DisplayTrafficLight((RequestController.IsRunning)?"green":"red");

                // Start the is running timer
                isRunningTimer.Enabled = true;
            }
            catch
            {
                // Display red yellow
                DisplayTrafficLight("red");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop the is running timer
                isRunningTimer.Enabled = false;

                // Display off yellow
                DisplayTrafficLight("yellow");

                // Stop the controller
                RequestController.Stop();
            }
            finally
            {
                // Display red yellow
                DisplayTrafficLight("red");
            }
        }

        private void DisplayTrafficLight(string colour)
        {
            // Load the image from the resource
            Bitmap img = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Discovery.RequestManager.TestHarness.trafficlight_" + colour + ".png"));
            
            // Load the image into the picturebix
            imgLights.Width = img.Width;
            imgLights.Height = img.Height;
            imgLights.Image = img;

            // Update display
            Application.DoEvents();
        }

        private void isRunningTimer_Tick(object sender, EventArgs e)
        {
            // Display red yellow
            DisplayTrafficLight((RequestController.IsRunning)?"green":"red");
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}