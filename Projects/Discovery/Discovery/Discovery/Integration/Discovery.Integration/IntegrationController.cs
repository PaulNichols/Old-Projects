using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Messaging;
using System.Runtime.Remoting;
using System.Threading;
using Discovery.Integration.DataAccess;
using Discovery.Utility;
using Discovery.Utility.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Discovery.Integration
{
    /// <summary>
    /// A class to provide the integration controller which is in integration folder
    /// It provides the connection, FTP, MSMQ or any other tasks running
    /// with namespace Discovery.integration
    /// </summary>
    public class IntegrationController : MarshalByRefObject
    {
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public List<Task> GetTasks(string sortExpression)
        {
            List<Task> tasks = GetTasks();
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            tasks.Sort(new UniversalComparer<Task>(sortExpression));
            return tasks;
        }


        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                tasks =
                    CBO<Task>.FillCollection(DataAccessProvider.Instance().GetTasks(), TaskCustomFill, true);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return tasks;
        }


        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <returns></returns>
        public Task GetTask(int taskId)
        {
            Task task = null;
            try
            {
                task =
                    CBO<Task>.FillObject(DataAccessProvider.Instance().GetTask(taskId), TaskCustomFill, true);

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return task;
        }

        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskToDelete">The task to delete.</param>
        /// <returns></returns>
        public bool DeleteTask(Task taskToDelete)
        {
            bool success = false;
            try
            {
                if (taskToDelete != null)
                {
                    success = DataAccessProvider.Instance().DeleteTask(taskToDelete);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return success;
        }

        /// <summary>
        /// Saves the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public int SaveTask(Task task)
        {
            try
            {
                if (task.IsValid)
                {
                    // Save entity
                    task.Id = DataAccessProvider.Instance().SaveTask(task);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(task);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }

            // Done
            return task.Id;
        }

        /// <summary>
        /// Gets a list of queue names .
        /// </summary>
        /// <returns></returns>
        public List<string> GetQueues()
        {
            //initialise the retuen list of queue names
            List<string> queuesNames = new List<string>();

            try
            {
                //create a list of MessageQueue objects which are currently public on the network, then convert that list into a list
                //of strings which contain the queue name
                MessageQueue[] queues;
                try
                {
                    queues = MessageQueue.GetPublicQueues();
                    queuesNames = new List<MessageQueue>(queues).ConvertAll(new Converter<MessageQueue, string>(ConvertQueue));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                try
                {
                    queues = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName);
                    queuesNames.AddRange(new List<MessageQueue>(queues).ConvertAll(new Converter<MessageQueue, string>(ConvertQueue)));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            //sort the queue name using the default string comparer
            queuesNames.Sort();
            return queuesNames;
        }

        private static string ConvertQueue(MessageQueue queue)
        {
            //method to convert the MessageQueue object to a string of queue names
            return string.Concat(queue.MachineName, "\\", queue.QueueName);
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public static List<Connection> GetConnections()
        {
            List<Connection> connections = new List<Connection>();
            try
            {
                connections = CBO<Connection>.FillCollection(DataAccessProvider.Instance().GetConnections());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return connections;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <param name="connectionType">Type of the connection.</param>
        /// <param name="channelType">Type of the channel.</param>
        /// <returns></returns>
        public static List<Connection> GetConnections(Connection.ConnectionTypeEnum connectionType,
                                               Connection.ChannelTypeEnum channelType)
        {
            List<Connection> connections = null;
            try
            {
                connections =
                    CBO<Connection>.FillCollection(
                        DataAccessProvider.Instance().GetConnections(connectionType, channelType));
            }
            catch (Exception ex)
            {

                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return connections;
        }

        /// <summary>
        /// Tasks the custom fill.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        private static void TaskCustomFill(Task task, IDataReader dataReader, bool fullyPopulate)
        {
            if (task != null)
            {
                if (fullyPopulate)
                {
                    task.SourceConnection = GetConnection(task.SourceConnectionId);
                    task.DestinationConnection = GetConnection(task.DestinationConnectionId);
                }
            }
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<Connection> GetConnections(string sortExpression)
        {
            List<Connection> connections = GetConnections();
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Connection.Description,DayOfWeek";
            }
            connections.Sort(new UniversalComparer<Connection>(sortExpression));
            return connections;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        public static Connection GetConnection(int connectionId)
        {
            Connection connection = null;
            try
            {
                connection =
                    CBO<Connection>.FillObject(DataAccessProvider.Instance().GetConnection(connectionId));
                return connection;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return connection;
        }

        /// <summary>
        /// Deletes the connection.
        /// </summary>
        /// <param name="connectionToRemove">The connection to remove.</param>
        /// <returns></returns>
        public static bool DeleteConnection(Connection connectionToRemove)
        {
            bool success = false;
            try
            {
                if (connectionToRemove != null)
                {
                    success = DataAccessProvider.Instance().DeleteConnection(connectionToRemove);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return success;
        }

        /// <summary>
        /// Deletes the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public static bool DeleteDownTime(ConnectionDownTime connectionDownTime)
        {
            bool success = false;
            try
            {
                if (connectionDownTime != null)
                {
                    success = DataAccessProvider.Instance().DeleteDownTime(connectionDownTime);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return success;
        }

        /// <summary>
        /// Saves the connection.
        /// </summary>
        /// <param name="connectionToSave">The connection to save.</param>
        /// <returns></returns>
        public static int SaveConnection(Connection connectionToSave)
        {
            try
            {
                if (connectionToSave.IsValid)
                {
                    if (connectionToSave.ChannelType == Connection.ChannelTypeEnum.FTP && connectionToSave.Id != -1)
                    {
                        //we are editing an FTP connection
                        //check if the user entered a new password
                        string currentPassword = ((FTPConnectionSettings)connectionToSave.Settings).Password;
                        //if they didn't enter a new password then we'll retrieve the previous one from the database and attempt
                        //to save using that password
                        if (string.IsNullOrEmpty(currentPassword))
                        {
                            Connection retrievedConnection = GetConnection(connectionToSave.Id);
                            if (retrievedConnection != null && retrievedConnection.Id != -1)
                            {
                                //we've successfully retrieved the current data for this connection from the database
                                //so this will be the password we try and save
                                ((FTPConnectionSettings)connectionToSave.Settings).Password =
                                    ((FTPConnectionSettings)retrievedConnection.Settings).Password;
                            }
                        }
                    }
                    connectionToSave.Id = DataAccessProvider.Instance().SaveConnection(connectionToSave);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(connectionToSave);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }

            // Done
            return connectionToSave.Id;
        }

        /// <summary>
        /// Saves the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public static int SaveDownTime(ConnectionDownTime connectionDownTime)
        {
            try
            {
                if (connectionDownTime.IsValid)
                {
                    // Save entity
                    connectionDownTime.Id = DataAccessProvider.Instance().SaveDownTime(connectionDownTime);
                }
                else
                {
                    // Entity is not valid
                    throw new InValidBusinessObjectException(connectionDownTime);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }

            // Done
            return connectionDownTime.Id;
        }

        /// <summary>
        /// Gets the down time.
        /// </summary>
        /// <param name="downTimeId">The down time id.</param>
        /// <returns></returns>
        public static ConnectionDownTime GetDownTime(int downTimeId)
        {
            ConnectionDownTime downTime = null;
            try
            {
                downTime =
                    CBO<ConnectionDownTime>.FillObject(
                        DataAccessProvider.Instance().GetDownTime(downTimeId),
                        DownTimeCustomFill, true);
                return downTime;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return downTime;
        }

        /// <summary>
        /// Downs the time custom fill.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="fullyPopulate">if set to <c>true</c> [fully populate].</param>
        private static void DownTimeCustomFill(ConnectionDownTime connectionDownTime, IDataReader dataReader,
                                        bool fullyPopulate)
        {
            if (connectionDownTime != null)
            {
                if (fullyPopulate)
                {
                    connectionDownTime.Connection = GetConnection(connectionDownTime.ConnectionId);
                }
            }
        }

        /// <summary>
        /// Gets the down times.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns></returns>
        public static List<ConnectionDownTime> GetDownTimes(int? connectionId, string sortExpression)
        {
            List<ConnectionDownTime> connectionDownTimes = GetDownTimes(connectionId);
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = "Name";
            }
            connectionDownTimes.Sort(new UniversalComparer<ConnectionDownTime>(sortExpression));
            return connectionDownTimes;
        }

        /// <summary>
        /// Gets the down times.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        public static List<ConnectionDownTime> GetDownTimes(int? connectionId)
        {
            List<ConnectionDownTime> connectionDownTimes = null;
            try
            {
                connectionDownTimes =
                    CBO<ConnectionDownTime>.FillCollection(
                        DataAccessProvider.Instance().GetDownTimes(connectionId),
                        DownTimeCustomFill, true);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
            return connectionDownTimes;
        }

        /// <summary>
        /// Define a timer task
        /// </summary>
        public static Dictionary<string, Timer> TaskTimers = new Dictionary<string, Timer>();
        /// <summary>
        /// Define all tasks
        /// </summary>
        public static Dictionary<string, Task> AllTasks = new Dictionary<string, Task>();
        /// <summary>
        /// Define all connections
        /// </summary>
        public static Dictionary<string, Connection> AllConnections = new Dictionary<string, Connection>();
        public static List<String> TasksInProgress = new List<string>();

        /// <summary>
        /// Executes the tasks.
        /// </summary>
        /// <param name="state">The state of the thread.</param>
        public static void ExecuteTasks(object state)
        {
            try
            {
                IntegrationController integrationController = new IntegrationController();
                List<Task> tasks = integrationController.GetTasks();

#if DEBUG
                Logger.Write(new LogEntry(
                                 string.Format("A list of {0} tasks have been retrieved.", tasks.Count),
                                 "Integration Trace",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
#endif

                foreach (Task task in tasks)
                {
                    AddConnection(task.SourceConnection);
                    AddConnection(task.DestinationConnection);

                    if (TaskTimers.ContainsKey(task.Name))
                    {
                        if (AllTasks[task.Name].Frequency != task.Frequency)
                        {
                            //the execution interval has changed in the database so change it on the timer
                            TaskTimers[task.Name].Change(0, task.Frequency * 1000);
                        }
#if DEBUG
                        Logger.Write(new LogEntry(
                                         string.Format("The frequency of Task '{0}' has changed from {1} to {2}.",
                                                       task.Name, AllTasks[task.Name].Frequency, task.Frequency),
                                         "Integration Trace",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
#endif
                        AllTasks[task.Name] = task;

                    }
                    else
                    {
                        TimerCallback taskDelegate = null;
                        if (task.SourceConnection.ChannelType == Connection.ChannelTypeEnum.FTP)
                        {
                            taskDelegate = new TimerCallback(Task.ExecuteFTPTask);
                        }
                        else if (task.SourceConnection.ChannelType == Connection.ChannelTypeEnum.MSMQ)
                        {
                            taskDelegate = new TimerCallback(Task.ExecuteMSMQTask);
                        }

                        AllTasks.Add(task.Name, task);

#if DEBUG
                        Logger.Write(new LogEntry(
                                         string.Format(
                                             "A new Task '{0}' has been added with a frequency of {1} seconds.",
                                             task.Name, task.Frequency),
                                         "Integration Trace",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
#endif

                        Timer taskTimer = new Timer(taskDelegate, task.Name, 0, task.Frequency * 1000);
                        TaskTimers.Add(task.Name, taskTimer);
                    }
                }

                //check all tasks still exist in database, ortherwise remove them
                foreach (KeyValuePair<string, Timer> taskTimer in TaskTimers)
                {
                    if (!AllTasks.ContainsKey(taskTimer.Key))
                    {
                        TaskTimers[taskTimer.Key].Dispose();
                        TaskTimers.Remove(taskTimer.Key);

#if DEBUG
                        Logger.Write(new LogEntry(
                                         string.Format("The redundant Task '{0}' has been removed.", taskTimer.Key),
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
                //log error

                if (ExceptionPolicy.HandleException(ex, "Integration")) throw;
            }
        }

        /// <summary>
        /// Adds the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private static void AddConnection(Connection connection)
        {
            connection.IsDown = ConnectionSheduledForDownTime(connection.Id);
            if (!AllConnections.ContainsKey(connection.Name))
            {
                AllConnections.Add(connection.Name, connection);
            }
            else if (AllConnections[connection.Name].Active != connection.Active)
            {
                AllConnections[connection.Name] = connection;
            }
        }

        /// <summary>
        /// Connections the sheduled for down time.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        private static bool ConnectionSheduledForDownTime(int connectionId)
        {
            List<ConnectionDownTime> downTimes = GetDownTimes(connectionId);
            bool isDown = false;

            foreach (ConnectionDownTime downTime in downTimes)
            {
                if (downTime.DayOfWeek == DateTime.Today.DayOfWeek)
                {
                    if (downTime.StartTime.TimeOfDay <= DateTime.Now.TimeOfDay &&
                        downTime.EndTime.TimeOfDay > DateTime.Now.TimeOfDay)
                    {
                        isDown = true;
                        break;
                    }
                }
            }
            return isDown;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
#if DEBUG
            Logger.Write(new LogEntry(
                             "Integration has started.",
                             "Integration Trace",
                             0,
                             0,
                             TraceEventType.Information,
                             null,
                             null));
#endif


//            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            RemotingConfiguration.Configure(configuration.FilePath, true);

//#if DEBUG
//            Logger.Write(new LogEntry(
//                             string.Format("Remoting has been configured. Config path was '{0}.", configuration.FilePath),
//                             "Integration Trace",
//                             0,
//                             0,
//                             TraceEventType.Information,
//                             null,
//                             null));
//#endif

            TimerCallback tasksDelegate = new TimerCallback(ExecuteTasks);
            //set how often we check the tasks in the database
            int checkTasksInterval = Convert.ToInt32(ConfigurationManager.AppSettings["checkTasksInterval"]) * 1000;

#if DEBUG
            Logger.Write(new LogEntry(
                             string.Format("Tasks table will be checked every {0} seconds.",
                                           checkTasksInterval),
                             "Integration Trace",
                             0,
                             0,
                             TraceEventType.Information,
                             null,
                             null));
#endif
            executeTasksTimer = new Timer(tasksDelegate, null, 0, checkTasksInterval);

#if DEBUG
            Logger.Write(new LogEntry(
                             "Timer to get tasks has begun.",
                             "Integration Trace",
                             0,
                             0,
                             TraceEventType.Information,
                             null,
                             null));
#endif
        }

        private static Timer executeTasksTimer;

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public static void Stop()
        {
            executeTasksTimer.Dispose();
        }
    }
}