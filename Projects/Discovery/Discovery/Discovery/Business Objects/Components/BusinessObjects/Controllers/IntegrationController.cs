
using System.Collections.Generic;
using System.Net.Sockets;
using Discovery.Integration;
using Discovery.Utility;

namespace Discovery.BusinessObjects.Controllers
{
    
    /// <summary>
    /// A class to provide the integration controller which is a business object controller
    /// with namespace Discovery.BusinessObjects.Controllers
    /// </summary>
    public class IntegrationController 
    {
        //TcpClientChannel chan = new TcpClientChannel();
        //ChannelServices.RegisterChannel(chan, true);

        // RemotingConfiguration.Configure("web.config", true);
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetQueues()
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            List<string> queues;

            try
            {
                queues = controller.GetQueues();
            }
            catch (SocketException)
            {
                throw;
            }
            return queues;
        }
        
        // RemotingConfiguration.Configure("web.config", true);
        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public static List<Task> GetTasks()
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            List<Task> tasks;

            try
            {
                tasks = controller.GetTasks();
            }
            catch (SocketException)
            {
                throw;
            }
            return tasks;
        }

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
        /// Gets the task.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <returns></returns>
        public static Task GetTask(int taskId)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            Task task;

            try
            {
                task = controller.GetTask(taskId);
            }
            catch (SocketException)
            {
                throw;
            }
            return task;
        }



        /// <summary>
        /// Deletes the task.
        /// </summary>
        /// <param name="taskToDelete">The task to delete.</param>
        /// <returns></returns>
        public static bool DeleteTask(Task taskToDelete)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();
            bool success;
            try
            {
                success = controller.DeleteTask(taskToDelete);
            }
            catch (SocketException)
            {
                throw;
            }
            return success;
        }

        /// <summary>
        /// Saves the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public static int SaveTask(Task task)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            int returnValue;

            try
            {
                returnValue = controller.SaveTask(task);
            }
            catch (SocketException)
            {
                throw;
            }
            return returnValue;
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
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public static  List<Connection> GetConnections()
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            List<Connection> connections;

            try
            {
                connections = controller.GetConnections();
            }
            catch (SocketException)
            {
                throw;
            }
            return connections;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <param name="connectionType">Type of the connection.</param>
        /// <param name="channelType">Type of the channel.</param>
        /// <returns></returns>
        public static List<Connection> GetConnections(Connection.ConnectionTypeEnum? connectionType,Connection.ChannelTypeEnum? channelType)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            List<Connection> connections;

            try
            {
                connections = controller.GetConnections(connectionType.Value, channelType.Value);
            }
            catch (SocketException)
            {
                throw;
            }
            return connections;
        }


        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        public static Connection GetConnection(int connectionId)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            Connection connection;

            try
            {
                connection = controller.GetConnection(connectionId);
            }
            catch (SocketException)
            {
                throw;
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
            Integration.IntegrationController controller = new Integration.IntegrationController();
            bool success;
            try
            {
                success = controller.DeleteConnection(connectionToRemove);
            }
            catch (SocketException)
            {
                throw;
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
            Integration.IntegrationController controller = new Integration.IntegrationController();
            bool success;
            try
            {
                success = controller.DeleteDownTime(connectionDownTime);
            }
            catch (SocketException)
            {
                throw;
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
            Integration.IntegrationController controller = new Integration.IntegrationController();

            int returnValue;

            try
            {
                returnValue = controller.SaveConnection(connectionToSave);
            }
            catch (SocketException)
            {
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public static int SaveDownTime(ConnectionDownTime connectionDownTime)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            int returnValue;

            try
            {
                returnValue = controller.SaveDownTime(connectionDownTime);
            }
            catch (SocketException)
            {
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the down time.
        /// </summary>
        /// <param name="downTimeId">The down time id.</param>
        /// <returns></returns>
        public static ConnectionDownTime GetDownTime(int downTimeId)
        {
            Integration.IntegrationController controller = new Integration.IntegrationController();

            ConnectionDownTime downTime;

            try
            {
                downTime = controller.GetDownTime(downTimeId);
            }
            catch (SocketException)
            {
                throw;
            }
            return downTime;
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
            Integration.IntegrationController controller = new Integration.IntegrationController();

            List<ConnectionDownTime> connectionDownTimes;

            try
            {
                connectionDownTimes = controller.GetDownTimes(connectionId==0?null:connectionId);
            }
            catch (SocketException)
            {
                throw;
            }
            return connectionDownTimes;
        }
    }
}