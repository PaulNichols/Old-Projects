using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using Discovery.BusinessObjects;
using Discovery.RequestManagerClient;
using FtpLib;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.Integration
{
    /// <summary>
    /// A Class 'Task' which is an entity with namespace Discovery.Integration
    /// It is inherited from PersistableBusinessObject
    /// The class holds the task details such as connection, FTP, MSMQ, etc...
    /// </summary>
    [Serializable]
    public class Task : PersistableBusinessObject
    {
        #region Private fields

        private string name;
        private bool removeDataFile;
        private bool removeFlagFile;
        private string dataFileExtension;
        private string flagFileExtension;
        private bool monitorSequenceNumber;
        private int sequenceNumber;
        private string sourceDirectory;
        private string destinationDirectory;
        private Connection sourceConnection;
        private int sourceConnectionId;
        private Connection destinationConnection;
        private int destinationConnectionId;
        private int frequency; //mins
        //backup settings

        private string sourceConnectionIdentifier;
        private string destinationConnectionIdentifier;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the source connection identifier. 
        /// This is used to place information such as an opco's name or a 
        /// warehouse name which will be required when monitoring an msmq in order
        /// to pull the correct message from that queue
        /// </summary>
        /// <value>The source connection identifier.</value>
        public string SourceConnectionIdentifier
        {
            get { return sourceConnectionIdentifier; }
            set { sourceConnectionIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the destination connection identifier.
        /// This is used to place information such as an opco's name or a 
        /// warehouse name which will be required when putting a message on an msmq and forming the label
        /// </summary>
        /// <value>The destination connection identifier.</value>
        public string DestinationConnectionIdentifier
        {
            get { return destinationConnectionIdentifier; }
            set { destinationConnectionIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the tasks identifying name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether data files should be removed after being transferred
        /// </summary>
        /// <value><c>true</c> if [remove data file]; otherwise, <c>false</c>.</value>
        public bool RemoveDataFile
        {
            get { return removeDataFile; }
            set { removeDataFile = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether flag files should be removed after the data file has bee transferred
        /// </summary>
        /// <value><c>true</c> if [remove flag file]; otherwise, <c>false</c>.</value>
        public bool RemoveFlagFile
        {
            get { return removeFlagFile; }
            set { removeFlagFile = value; }
        }

        /// <summary>
        /// Gets or sets the data file extension.
        /// </summary>
        /// <value>The data file extension.</value>
        public string DataFileExtension
        {
            get { return dataFileExtension; }
            set { dataFileExtension = value; }
        }

        /// <summary>
        /// Gets or sets the flag file extension.
        /// </summary>
        /// <value>The flag file extension.</value>
        public string FlagFileExtension
        {
            get { return flagFileExtension; }
            set { flagFileExtension = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to monitor sequence numbers.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [monitor sequence number]; otherwise, <c>false</c>.
        /// </value>
        public bool MonitorSequenceNumber
        {
            get { return monitorSequenceNumber; }
            set { monitorSequenceNumber = value; }
        }

        /// <summary>
        /// Gets or sets the last sequence number.
        /// </summary>
        /// <value>The sequence number.</value>
        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        /// <summary>
        /// Gets or sets the source ftp directory.
        /// </summary>
        /// <value>The source directory.</value>
        public string SourceDirectory
        {
            get { return sourceDirectory; }
            set { sourceDirectory = value; }
        }

        /// <summary>
        /// Gets or sets the destination ftp directory.
        /// </summary>
        /// <value>The destination directory.</value>
        public string DestinationDirectory
        {
            get { return destinationDirectory; }
            set { destinationDirectory = value; }
        }


        /// <summary>
        /// Gets or sets the source connection.
        /// </summary>
        /// <value>The source connection.</value>
        public Connection SourceConnection
        {
            get { return sourceConnection; }
            set { sourceConnection = value; }
        }

        /// <summary>
        /// Gets or sets the source connection unique identifier.
        /// </summary>
        /// <value>The source connection id.</value>
        public int SourceConnectionId
        {
            get { return sourceConnectionId; }
            set { sourceConnectionId = value; }
        }

        /// <summary>
        /// Gets or sets the destination connection.
        /// </summary>
        /// <value>The destination connection.</value>
        public Connection DestinationConnection
        {
            get { return destinationConnection; }
            set { destinationConnection = value; }
        }

        /// <summary>
        /// Gets or sets the destination connections unique identifier.
        /// </summary>
        /// <value>The destination connection id.</value>
        public int DestinationConnectionId
        {
            get { return destinationConnectionId; }
            set { destinationConnectionId = value; }
        }

        /// <summary>
        /// Gets or sets the frequency this task should be executed.
        /// </summary>
        /// <value>The frequency.</value>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the FTP task. The controller or caller of this method will decide that an FTP  task requires execution as the 
        /// source connection of the task is FTP. This method then goes on to execute the destination part of the task, either FTP again
        /// or MSMQ
        /// </summary>
        /// <param name="ftpTaskName">Name of the FTP task.</param>
        public static void ExecuteFTPTask(object ftpTaskName)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                             string.Format("The FTP Task '{0}' is being executed.", ftpTaskName),
                             "Integration Trace",
                             0,
                             0,
                             TraceEventType.Information,
                             null,
                             null));
#endif
            Task task = IntegrationController.AllTasks[ftpTaskName.ToString()];
            if (!IntegrationController.TasksInProgress.Contains(ftpTaskName.ToString()))
            {
                string workingDirectory = "";
                FTPFactory sourceFTPClient = null;

                try
                {
                    //retieve the latest details from the collection of tasks held in mememory which is kept up to date
                    // on a seperate thread at regular intervals

                    IntegrationController.TasksInProgress.Add(ftpTaskName.ToString());


                    try
                    {
                        //only execute this task if the source connection is FTP
                        if (task != null && task.SourceConnection.Settings is FTPConnectionSettings)
                        {
                            //only continue if the source and destination connnections are active and not down
                            if ((IntegrationController.AllConnections[task.SourceConnection.Name].Active &&
                                 !IntegrationController.AllConnections[task.SourceConnection.Name].IsDown) &&
                                (IntegrationController.AllConnections[task.DestinationConnection.Name].Active &&
                                 !IntegrationController.AllConnections[task.DestinationConnection.Name].IsDown))
                            {
#if DEBUG
                                Logger.Write(new LogEntry(
                                                 string.Format(
                                                     "The FTP Task '{0}' is still being executed as both connections are active and not down.",
                                                     ftpTaskName),
                                                 "Integration Trace",
                                                 0,
                                                 0,
                                                 TraceEventType.Information,
                                                 null,
                                                 null));
#endif
                                //get an instance of the FTP helper class using the source connection details, including logging in and
                                //setting the remote path
                                sourceFTPClient = GetSourceFTPClient(task);

                                //if (task.TaskType == TaskTypeEnum.Shipment || task.TaskType == TaskTypeEnum.Commander)
                                //{

                                //has this task been identified as once which looks for flag files before looking for the data file (i.e. Shipment tasks)
                                bool useFlag = !string.IsNullOrEmpty(task.FlagFileExtension);
                                string searchExtension;

#if DEBUG
                                if (useFlag)
                                {
                                    Logger.Write(new LogEntry(
                                                     string.Format("Flag files will not be ignored on task '{0}'.",
                                                                   ftpTaskName),
                                                     "Integration Trace",
                                                     0,
                                                     0,
                                                     TraceEventType.Information,
                                                     null,
                                                     null));
                                }
                                else
                                {
                                    Logger.Write(new LogEntry(
                                                     string.Format("Flag files will be ignored on task '{0}'.",
                                                                   ftpTaskName),
                                                     "Integration Trace",
                                                     0,
                                                     0,
                                                     TraceEventType.Information,
                                                     null,
                                                     null));
                                }
#endif
                                //set the file extension to search for as that of the flag or the data file setting
                                if (useFlag)
                                {
                                    searchExtension = task.FlagFileExtension;
                                }
                                else
                                {
                                    searchExtension = task.DataFileExtension;
                                }


                                string[] filteredFileList = new string[] {};
                                //get all files with a matching file extension

                                string mask = string.Concat("*.", searchExtension, "*");
                                try
                                {
#if DEBUG
                                    Logger.Write(new LogEntry(
                                                     string.Format(
                                                         "A list of files from the FTP connection '{1}' with the extension '{0}' will be retrieved.",
                                                         mask, ftpTaskName),
                                                     "Integration Trace",
                                                     0,
                                                     0,
                                                     TraceEventType.Information,
                                                     null,
                                                     null));
#endif
                                    //see if there are any files to retrieve
                                    filteredFileList =
                                        sourceFTPClient.GetFileList(mask);
                                }
                                catch (Exception e)
                                {
                                    if (sourceFTPClient.RetValue == 552)
                                    {
                                        //this error occurs when there's simply no files matching the wild card
                                        //on the ftp site
                                    }
                                    else
                                    {
                                        throw e;
                                    }
                                }


#if DEBUG
                                Logger.Write(new LogEntry(
                                                 string.Format("Found {0} files to download.", filteredFileList.Length),
                                                 "Integration Trace",
                                                 0,
                                                 0,
                                                 TraceEventType.Information,
                                                 null,
                                                 null));
#endif
                                if (filteredFileList.Length > 0)
                                    //create a temporary working local directory to place the files in
                                {
                                    workingDirectory = Path.GetRandomFileName();
#if DEBUG
                                    Logger.Write(new LogEntry(
                                                     string.Format("A working directory called '{0}' is being created.",
                                                                   workingDirectory),
                                                     "Integration Trace",
                                                     0,
                                                     0,
                                                     TraceEventType.Information,
                                                     null,
                                                     null));
#endif

                                    Directory.CreateDirectory(workingDirectory);

#if DEBUG
                                    Logger.Write(new LogEntry(
                                                     string.Format("The working directory was created."),
                                                     "Integration Trace",
                                                     0,
                                                     0,
                                                     TraceEventType.Information,
                                                     null,
                                                     null));
#endif


                                    //sort filtered list by name in order that the sequence number check is more likely to work
                                    List<string> sortedList = new List<string>(filteredFileList);
                                    sortedList.Sort();
                                    filteredFileList = sortedList.ToArray();

                                    //iterate through the retrieved files
                                    foreach (string file in filteredFileList)
                                    {
                                        if (!string.IsNullOrEmpty(file))
                                        {
                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                string fileNameToDownLoad;
                                                if (useFlag)
                                                {
                                                    //find data file from flag file
                                                    fileNameToDownLoad =
                                                        file.ToLower().Replace("." + task.FlagFileExtension.ToLower(),
                                                                               "." + task.DataFileExtension.ToLower());
                                                }
                                                else
                                                {
                                                    //just Download the data file don't worry about flag files
                                                    fileNameToDownLoad = file;
                                                }

                                                if (task.MonitorSequenceNumber)
                                                {
                                                    CheckSequenceNumber(task, fileNameToDownLoad);
                                                }

                                                if (!sourceFTPClient.FileExists(fileNameToDownLoad))
                                                {
                                                    //error
                                                    Logger.Write(new LogEntry(
                                                                     string.Format(
                                                                         "The FTP Task '{0}' could not find the file '{1}' to download.",
                                                                         ftpTaskName, fileNameToDownLoad),
                                                                     "Integration Trace",
                                                                     0,
                                                                     0,
                                                                     TraceEventType.Information,
                                                                     null,
                                                                     null));

                                                    ExceptionPolicy.HandleException(
                                                        new MissingFileException(task.SourceConnectionIdentifier,
                                                                                 string.Format(
                                                                                     "The FTP Task '{0}' could not find the file '{1}' to download.",
                                                                                     ftpTaskName, fileNameToDownLoad)),
                                                        "Integration");

                                                    //If no error has been raised yet the persumable download was succesfull 
                                                    //so remove the flag file if this is what should happen
                                                    if (useFlag && task.RemoveFlagFile)
                                                    {
                                                        //remove remote flag file
                                                        sourceFTPClient.DeleteRemoteFile(file);
                                                    }
                                                }
                                                else
                                                {
                                                    bool resume = true;
                                                    //the name of the directory and file to down load the data file to locally
                                                    string localFileNameAndPath =
                                                        string.Concat(workingDirectory, Path.DirectorySeparatorChar,
                                                                      fileNameToDownLoad);

                                                    sourceFTPClient.Download(fileNameToDownLoad, localFileNameAndPath,
                                                                             resume);

                                                    //we now should have the file based on the source connection setting, the second half of the 
                                                    //task is to deliver the file somewhere based on the destination settings, so this is what happens next
                                                    if (task.DestinationConnection.ChannelType ==
                                                        Connection.ChannelTypeEnum.MSMQ)
                                                    {
                                                        SendMSMQMessageToDestination(fileNameToDownLoad,
                                                                                     localFileNameAndPath,
                                                                                     task);
                                                    }
                                                    else if (task.DestinationConnection.ChannelType ==
                                                             Connection.ChannelTypeEnum.FTP)
                                                    {
                                                        SendFTPMessageToDestination(localFileNameAndPath, resume, task);
                                                    }

                                                    if (task.RemoveDataFile)
                                                    {
                                                        //remove remote data file
                                                        sourceFTPClient.DeleteRemoteFile(fileNameToDownLoad);
                                                    }

                                                    //If no error has been raised yet the persumable download was succesfull 
                                                    //so remove the flag file if this is what should happen
                                                    if (useFlag && task.RemoveFlagFile)
                                                    {
                                                        //remove remote flag file
                                                        sourceFTPClient.DeleteRemoteFile(file);
                                                    }
                                                }

                                                if (task.MonitorSequenceNumber)
                                                {
                                                    //save the latest sequence number
                                                    UpdateSequenceNumber(task);
                                                }

                                                //commit the transaction
                                                if (Transaction.Current != null &&
                                                    Transaction.Current.TransactionInformation.Status ==
                                                    TransactionStatus.Active)
                                                {
                                                    scope.Complete();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
#if DEBUG
                                string reason = "";
                                if (!IntegrationController.AllConnections[task.SourceConnection.Name].Active)
                                {
                                    reason = "The Source Connection is inactive.";
                                }
                                if (IntegrationController.AllConnections[task.SourceConnection.Name].IsDown)
                                {
                                    reason = "The Source Connection is scheduled for downtime.";
                                }
                                if (!IntegrationController.AllConnections[task.DestinationConnection.Name].Active)
                                {
                                    reason = "The Destination Connection is inactive.";
                                }
                                if (IntegrationController.AllConnections[task.DestinationConnection.Name].IsDown)
                                {
                                    reason = "The Destination Connection is scheduled for downtime.";
                                }
                                Logger.Write(new LogEntry(
                                                 string.Format(
                                                     "The FTP Task '{0}' will not be exectuted because - {1}.",
                                                     ftpTaskName, reason),
                                                 "Integration Trace",
                                                 0,
                                                 0,
                                                 TraceEventType.Information,
                                                 null,
                                                 null));
#endif
                            }
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Logger.Write(new LogEntry(
                                         ex.Message,
                                         "Integration Trace",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
#endif
                        //log, do not rethrow but instead make the task sleep for a period of time before trying again
                        int TaskFailureSleep =
                            Convert.ToInt32(ConfigurationManager.AppSettings["TaskFailureSleep"])*1000;
                        IntegrationController.TaskTimers[task.Name].Change(TaskFailureSleep, task.Frequency*1000);
                        Exception exceptionToLog = new Exception(task.Name, ex);
                        if (ExceptionPolicy.HandleException(exceptionToLog, "Integration")) throw;
                    }

                    IntegrationController.TasksInProgress.Remove(ftpTaskName.ToString());
                
            }
            catch(Exception ex)
            {
                //log, do not rethrow

#if DEBUG
                Logger.Write(new LogEntry(
                                 ex.Message,
                                 "Integration Trace",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
#endif
            }
        finally
            {
                try
                {
                    if (sourceFTPClient != null)
                    {
                        sourceFTPClient.Close();
                    }
                }
                catch
                {
                }

                try
                {
                    if (!string.IsNullOrEmpty(workingDirectory))
                    {
#if DEBUG
                        Logger.Write(new LogEntry(
                                         "Deleting working directory.",
                                         "Integration Trace",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
#endif
                        Directory.Delete(workingDirectory, true);
#if DEBUG
                        Logger.Write(new LogEntry(
                                         "Working directory deleted.",
                                         "Integration Trace",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
#endif
                    }
                }
                catch
                {
                }
            }
        }
    }

        /// <summary>
        /// Executes the MSMQ task.
        /// </summary>
        /// <param name="msmqTaskName">The FTP task.</param>
        public static void ExecuteMSMQTask(object msmqTaskName)
        {
            bool resume = true;
            Task task = IntegrationController.AllTasks[msmqTaskName.ToString()];
            if (task != null && task.SourceConnection.Settings is MSMQConnectionSettings)
            {
                if (task.SourceConnection.Active && task.DestinationConnection.Active)
                {
                    if ((IntegrationController.AllConnections[task.SourceConnection.Name].Active &&
                         !IntegrationController.AllConnections[task.SourceConnection.Name].IsDown) &&
                        (IntegrationController.AllConnections[task.DestinationConnection.Name].Active &&
                         !IntegrationController.AllConnections[task.DestinationConnection.Name].IsDown))
                    {
                        MSMQConnectionSettings sourceConnectionSettings =
                            (MSMQConnectionSettings)task.SourceConnection.Settings;

                        RequestManagerClientMSMQ requestManagerClient = new RequestManagerClientMSMQ();


                        // create an instance of the queue
                        requestManagerClient.QueueName = sourceConnectionSettings.QueueName;

                        //get a snapshot of all current messages on the queue in order to check them and recieve 
                        //them is they match the criteria defined in the source connection settings
                        MessageEnumerator messageEnumerator = requestManagerClient.MessageQueue.GetMessageEnumerator2();


                        while (messageEnumerator.MoveNext())
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                MSMQMessageLabel msmqMessageLabel = new MSMQMessageLabel(messageEnumerator.Current.Label);


                                if (msmqMessageLabel.IsMatch(task.DestinationConnection.ConnectionType,
                                                             task.SourceConnectionIdentifier,
                                                             task.DestinationConnectionIdentifier))
                                {
                                    //this message is one we are looking for so remove from queue
                                    //if anything goes wrong then the transaction should pleace it back


                                    RequestMessage recievedMessage = requestManagerClient.ReceiveById(messageEnumerator.Current.Id);

                                    //handle the delivery half of the task
                                    if (task.DestinationConnection.ChannelType == Connection.ChannelTypeEnum.MSMQ)
                                    {
                                        //create a new message and send it using the destination connection settings
                                        //and the current message property

                                        requestManagerClient.QueueName =
                                            ((MSMQConnectionSettings)task.DestinationConnection.Settings).
                                                QueueName;

                                        recievedMessage.DestinationSystem = task.DestinationConnectionIdentifier;
                                        recievedMessage.SourceSystem = task.SourceConnectionIdentifier;
                                        recievedMessage.Type = task.SourceConnection.ConnectionType.ToString();
                                        //recievedMessage.Body = recievedMessage.Body;
                                        recievedMessage.Name = msmqMessageLabel.FileName;
                                        recievedMessage.Sequence = task.SequenceNumber;

                                        requestManagerClient.Send(recievedMessage);
                                    }
                                    else if (task.DestinationConnection.ChannelType ==
                                             Connection.ChannelTypeEnum.FTP)
                                    {
                                        //get and create a temporary directory to place the file into
                                        string workingDirectory = Path.GetRandomFileName();
                                      

                                        Directory.CreateDirectory(workingDirectory);
                                          string localFileNameAndPath =
                                          String.Concat(workingDirectory, '\\',
                                                        msmqMessageLabel.FileName);
                                        
                                        File.WriteAllText(localFileNameAndPath, recievedMessage.Body);
                                      
                                        SendFTPMessageToDestination(localFileNameAndPath, resume, task);

                                        try
                                        {
                                            Directory.Delete(workingDirectory,true);
                                        }
                                        catch 
                                        {
                                          
                                        }
                                    }

                                    if (Transaction.Current != null && Transaction.Current.TransactionInformation.Status == TransactionStatus.Active)
                                    {
                                        scope.Complete();
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        #endregion

        //this class is used to parse a message label which will allow the caller to see if the message matches 
        //the type of messages they are monitoring the queue for
        private class MSMQMessageLabel
        {
            private readonly string messageLabel;
            private string type;
            private string src;
            private string dest;
            private string fileName;

            public MSMQMessageLabel(string messageLabel)
            {
                this.messageLabel = messageLabel;
                SplitMSMQMessageLabel();
            }

            public string MessageLabel
            {
                get { return messageLabel; }
            }

            public string Type
            {
                get { return type; }
                set { type = value; }
            }

            public string Src
            {
                get { return src; }
                set { src = value; }
            }

            public string Dest
            {
                get { return dest; }
                set { dest = value; }
            }

            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }

            /// <summary>
            /// Splits the MSMQ message label using a regular expression.
            /// </summary>
            private void SplitMSMQMessageLabel()
            {
                //example: SRC:MS;DEST:Northampton;TYPE:OPTRAK;NAME:rhgshipment0000000012.dat;1
                string pattern =
                    "SRC:(?<Source>[a-zA-Z]+);DEST:(?<Destination>[a-zA-Z]+);TYPE:(?<Type>[a-zA-Z]+);NAME:(?<Name>.+)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(MessageLabel);
                Src = match.Groups["Source"].Value;
                Dest = match.Groups["Destination"].Value;
                Type = match.Groups["Type"].Value;
                FileName = match.Groups["Name"].Value;
            }

            public bool IsMatch(Connection.ConnectionTypeEnum messageType, string sourceIdentifier,
                                string destinationIdentifier)
            {

                //look for messages where the lable includes (for example)
                //TYPE=OPTRAK
                //SRC=sourceconnectionidetifier 
                //DEST=destinationconnectionidetifier
                return (Type.ToLower() ==
                        messageType.ToString().ToLower() &&
                        sourceIdentifier.ToLower() == Src.ToLower() &&
                        destinationIdentifier.ToLower() == Dest.ToLower());
            }
        }

        #region Private Methods

        /// <summary>
        /// FTPs a file to a destination based on the tasks detination connection settings.
        /// </summary>
        /// <param name="localFileNameAndPath">The local file name and path.</param>
        /// <param name="resume">if set to <c>true</c> [resume].</param>
        /// <param name="task">The task.</param>
        private static void SendFTPMessageToDestination(string localFileNameAndPath, bool resume, Task task)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                     "SendFTPMessageToDestination.",
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
            FTPFactory destinationFTPClient = null;
            try
            {
                FTPConnectionSettings ftpDestinationConnectionSettings =
                    ((FTPConnectionSettings)task.DestinationConnection.Settings);


                destinationFTPClient =
                    new FTPFactory(ftpDestinationConnectionSettings.IpAddress,
                                   ftpDestinationConnectionSettings.Password,
                                   ftpDestinationConnectionSettings.Username,
                                   ftpDestinationConnectionSettings.Port,
                                   ftpDestinationConnectionSettings.ErrorCount);
#if DEBUG
                destinationFTPClient.Debug = true;
#else
                          destinationFTPClient.Debug = false;
#endif
                destinationFTPClient.RemotePath = task.DestinationDirectory;
                destinationFTPClient.Login();
                destinationFTPClient.SetBinaryMode(true);

                destinationFTPClient.Upload(localFileNameAndPath, resume);
            }
            finally
            {
                destinationFTPClient.Close();
            }
        }

        /// <summary>
        /// Places a files content on an MSMQ based on the tasks detination connection settings.
        /// </summary>
        /// <param name="fileNameToDownLoad">The file name to down load.</param>
        /// <param name="localFileNameAndPath">The local file name and path.</param>
        /// <param name="task">The task.</param>
        private static void SendMSMQMessageToDestination(string fileNameToDownLoad, string localFileNameAndPath,
                                                         Task task)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                     "SendMSMQMessageToDestination.",
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
            RequestManagerClientMSMQ requestManagerClient = new RequestManagerClientMSMQ();

            requestManagerClient.QueueName =
                ((MSMQConnectionSettings)task.DestinationConnection.Settings).
                    QueueName;

            RequestMessage requestMessage = new RequestMessage();
            requestMessage.DestinationSystem = task.DestinationConnectionIdentifier;
            requestMessage.SourceSystem = task.SourceConnectionIdentifier;
            requestMessage.Type = task.SourceConnection.ConnectionType.ToString();


            requestMessage.Body = File.ReadAllText(localFileNameAndPath);
            requestMessage.Name = fileNameToDownLoad;
            requestMessage.Sequence = task.SequenceNumber;

#if DEBUG
            Logger.Write(new LogEntry(
                     string.Format("About to send message to queue '{0}.", requestManagerClient.QueueName),
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
            requestManagerClient.Send(requestMessage);

#if DEBUG
            Logger.Write(new LogEntry(
                     string.Format("Sent message to queue '{0}'. ", requestManagerClient.QueueName),
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
        }

        /// <summary>
        /// Gets the source FTP client instatce and login to the remote site.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        private static FTPFactory GetSourceFTPClient(Task task)
        {
            FTPFactory sourceFTPClient;
            FTPConnectionSettings ftpConnectionSettings =
                ((FTPConnectionSettings)task.SourceConnection.Settings);


            sourceFTPClient = new FTPFactory(ftpConnectionSettings.IpAddress, ftpConnectionSettings.Password,
                                             ftpConnectionSettings.Username, ftpConnectionSettings.Port,
                                             ftpConnectionSettings.ErrorCount);

#if DEBUG
            sourceFTPClient.Debug = true;
#else
                          sourceFTPClient.Debug = false;
#endif

            try
            {
                sourceFTPClient.RemotePath = task.SourceDirectory;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to set remote path");
            }

            try
            {
                sourceFTPClient.Login();
            }
            catch (Exception e)
            {
#if DEBUG
                Logger.Write(new LogEntry(
                         string.Format("failed login"),
                          "Integration Trace",
                          0,
                          0,
                          TraceEventType.Information,
                          null,
                          null));
#endif
            }
            try
            {
                sourceFTPClient.SetBinaryMode(true);
            }
            catch (Exception e)
            {
                //throw new Exception("Failed to set binary mode");
            }
            return sourceFTPClient;
        }


        /// <summary>
        /// Updates the sequence number. Persist the lastest sequnce number for the task, the CheckSequenceNumber() method
        /// should have incremented this number
        /// </summary>
        /// <param name="task">The task.</param>
        private static void UpdateSequenceNumber(Task task)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                     "Updating the Sequence number and persisting the altered task.",
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
            IntegrationController integrationController = new IntegrationController();
            if (integrationController.SaveTask(task) == -1)
            {

            }
            else
            {
                //get new checksum
            }
        }

        /// <summary>
        /// Checks the sequenece number. If the number of this file is out of sequence then log the problem and continue.
        /// If the number was as expected then set the task objects sequence number property so that the next time the 
        /// task is executed the sequence number will be incremented
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="dataFileName">Name of the data file.</param>
        private static void CheckSequenceNumber(Task task, string dataFileName)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                     "Checking sequence number.",
                      "Integration Trace",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));
#endif
            string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(dataFileName);
            int fileNumber = 0;
            if (fileNameWithoutExtention.Length > 11)
            {
                try
                {
                    fileNumber = Convert.ToInt32(fileNameWithoutExtention.Substring(11));
                }
                catch
                {
                }
            }


            try
            {
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                if (fileNumber < (task.SequenceNumber + 1) )
                {

                    ExceptionPolicy.HandleException(new SequenceNumberException(task.SourceConnectionIdentifier,
                                                                                string.Format(
                                                                                    "Found missing sequence number '{0}'.",
                                                                                    fileNumber)), "Integration");


                }
                else if (fileNumber > (task.SequenceNumber + 1) )
                {
                    string endOfString = (fileNumber - 1 == task.SequenceNumber + 1) ? "." : ", numbers {1} - {3} are missing.";
                    ExceptionPolicy.HandleException(new SequenceNumberException(task.SourceConnectionIdentifier,
                                                                                string.Format("File from Opco '{2}' was out of sequence. Sequence Number '{1}' was expected" + endOfString,
                                                                                    fileNumber, task.SequenceNumber + 1,
                                                                                    task.sourceConnectionIdentifier,
                                                                                    fileNumber - 1)),
                                                                                   "Integration");

                    task.SequenceNumber = fileNumber;
                }
                else if (fileNumber == (task.SequenceNumber + 1) )
                {
                    task.SequenceNumber = fileNumber;
                }
                //}
            }
            catch (Exception e)
            {
                if (ExceptionPolicy.HandleException(e, "Integration")) throw;

            }
        }

        #endregion
    }
}