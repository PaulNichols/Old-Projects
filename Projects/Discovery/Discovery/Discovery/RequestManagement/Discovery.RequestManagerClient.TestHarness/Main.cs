using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Messaging;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.IO;
using Discovery.RequestManagerClient;

namespace Discovery.RequestManagerClient.TestHarness
{
    public partial class Main : Form
    {
        private RequestManagerClientMSMQ requestMSMQ;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            requestMSMQ = new RequestManagerClientMSMQ();
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                // Specify the queue name
                requestMSMQ.QueueName = txtQueueName.Text;

                // Create the request message
                RequestMessage requestMessage = new RequestMessage();

                // How many of each message do we send
                int msgCount = Convert.ToInt32(txtNumMessages.Text);

                // Set the size of the progress bar
                progressBar.Maximum = dlgFileOpen.FileNames.Length * msgCount;
                progressBar.Value = 0;

                // send the specified files
                foreach (string fileName in dlgFileOpen.FileNames)
                {
                    // Read the file contents
                    ReadFileContents(fileName);

                    // Specify request properties
                    requestMessage.SourceSystem = txtSource.Text;
                    requestMessage.DestinationSystem = txtDestination.Text;
                    requestMessage.Name = txtName.Text;
                    requestMessage.Type = txtType.Text;
                    requestMessage.Body = txtBody.Text;

                    for (int i = 0; i < msgCount; i++)
                    {
                        requestMessage.Sequence = (i + 1);
                        txtSequence.Text = (i + 1).ToString();

                        try
                        {
                            // Send the request
                            requestMSMQ.Send(requestMessage);
                        }
                        catch (MessageQueueException ex)
                        {
                            MessageQueue[] messageQueues = MessageQueue.GetPublicQueuesByMachine("robins");
                            // Display the exception, give the user a chance to stop
                            if (System.Windows.Forms.DialogResult.No == MessageBox.Show(String.Concat(ex.Message, "\n\nWould you like to continue sending messages?"), "Error sending message.", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                            {
                                break;
                            }
                        };

                        // Update the progress bar
                        progressBar.Value++;

                        // Clear message pump
                        Application.DoEvents();
                    }
                }
            }
        }

        private void ReadFileContents(string fileName)
        {
            // Read file contents
            StreamReader streamReader = new StreamReader(File.Open(fileName, FileMode.Open));
            txtBody.Text = streamReader.ReadToEnd();
            streamReader.Close();
            // Set file name
            txtName.Text = Path.GetFileName(fileName);
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (dlgFolderSelect.ShowDialog() == DialogResult.OK)
            {
                // Specify the queue name
                requestMSMQ.QueueName = txtQueueName.Text;

                // Create the request message
                RequestMessage requestMessage = new RequestMessage();

                // Get all the files in the folder
                string[] requestFiles = Directory.GetFiles(dlgFolderSelect.SelectedPath, txtWildCard.Text);

                // How many of each message do we send
                int msgCount = Convert.ToInt32(txtNumMessages.Text);

                // Set the size of the progress bar
                progressBar.Maximum = requestFiles.Length * msgCount;
                progressBar.Value = 0;

                // send the specified files
                foreach (string fileName in requestFiles)
                {
                    // Read the file contents
                    ReadFileContents(fileName);

                    // Specify request properties
                    requestMessage.SourceSystem = txtSource.Text;
                    requestMessage.DestinationSystem = txtDestination.Text;
                    requestMessage.Name = txtName.Text;
                    requestMessage.Type = txtType.Text;
                    requestMessage.Body = txtBody.Text;

                    for (int i = 0; i < msgCount; i++)
                    {
                        requestMessage.Sequence = (i + 1);
                        txtSequence.Text = (i + 1).ToString();

                        try
                        {
                            // Send the request
                            requestMSMQ.Send(requestMessage);
                        }
                        catch (MessageQueueException ex)
                        {
                            MessageQueue[] messageQueues = MessageQueue.GetPublicQueuesByMachine("robins");
                            // Display the exception, give the user a chance to stop
                            if (System.Windows.Forms.DialogResult.No == MessageBox.Show(String.Concat(ex.Message, "\n\nWould you like to continue sending messages?"), "Error sending message.", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                            {
                                break;
                            }
                        };

                        // Update the progress bar
                        progressBar.Value++;

                        // Clear message pump
                        Application.DoEvents();
                    }
                }
            }
        }
    }
}