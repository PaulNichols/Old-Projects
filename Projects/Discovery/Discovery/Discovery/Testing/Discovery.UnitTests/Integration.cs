using System;
using System.Collections.Generic;
using System.Transactions;
using Discovery.BusinessObjects.Controllers;
using Discovery.Integration;
using NUnit.Framework;

namespace Discovery.UnitTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private static Connection PopulateMSMQConnection()
        {
            Connection connection = new Connection();
            connection.Active = false;
            connection.ChannelType = Connection.ChannelTypeEnum.MSMQ;
            connection.ConnectionType = Connection.ConnectionTypeEnum.Shipment;
            connection.ErrorCount = 3;
            connection.IsDown = false;
            connection.Name = "TestConnection";
            connection.UpdatedBy = "UnitTest";
            connection.Settings = new MSMQConnectionSettings();
            ((MSMQConnectionSettings) connection.Settings).QueueName = "QueueName";
            return connection;
        }

         private static Connection PopulateFTPConnection()
        {
            Connection connection = new Connection();
            connection.Active = false;
            connection.ChannelType = Connection.ChannelTypeEnum.FTP;
            connection.ConnectionType = Connection.ConnectionTypeEnum.Shipment;
            connection.ErrorCount = 3;
            connection.IsDown = false;
            connection.Name = "TestConnection";
            connection.UpdatedBy = "UnitTest";
            connection.Settings = new FTPConnectionSettings();
            ((FTPConnectionSettings) connection.Settings).Username = "Username";
             ((FTPConnectionSettings) connection.Settings).Port = 21;
             ((FTPConnectionSettings) connection.Settings).IpAddress = "1.1.1.1";
             ((FTPConnectionSettings) connection.Settings).Password = "Password";
             ((FTPConnectionSettings) connection.Settings).ErrorCount= 3;
            return connection;
        }

        [Test]
        public void GetConnections()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                int id = IntegrationController.SaveConnection(PopulateMSMQConnection());
                if (id > -1)
                {
                    //retrieve all connections and the one we saved should return at least
                    List<Connection> connections = IntegrationController.GetConnections();
                    //so the count should be >0
                    Assert.IsTrue(connections.Count > 0);
                    //check for our new id
                    Assert.IsTrue(connections.Find(delegate(Connection currentItem)
                                                       {
                                                           return currentItem.Id == id;
                                                       }) != null);
                }
            }
        }

        [Test]
        public void SaveConnection()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                int id = IntegrationController.SaveConnection(PopulateMSMQConnection());

                //check the id!=-1
                Assert.IsTrue(id != -1);
            }
        }

          [Test]
        public void SaveConnectionTestPassword_NoChange()
        {
              //if the password is not altered then the old password should be reused
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                Connection connection = PopulateFTPConnection();
                connection.Id = IntegrationController.SaveConnection(connection);
                
                //check the id!=-1
                Assert.IsTrue(connection.Id != -1);
                connection=IntegrationController.GetConnection(connection.Id);

                string oldPassword = ((FTPConnectionSettings) connection.Settings).Password;
                ((FTPConnectionSettings) connection.Settings).Password = "";
                connection.Id = IntegrationController.SaveConnection(connection);

                 //check the id!=-1
                Assert.IsTrue(connection.Id != -1);
                
                //retrieve the saved connection
                connection=IntegrationController.GetConnection(connection.Id);

                //check that the password on the save object is the same as the original 
                //despite clearing the password before saving
                ((FTPConnectionSettings) connection.Settings).Password = oldPassword;
            }
        }

          [Test]
        public void SaveConnectionTestPassword_Change()
        {
              //if the password is not altered then the old password should be reused
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                Connection connection = PopulateFTPConnection();
                connection.Id = IntegrationController.SaveConnection(connection);
                
                //check the id!=-1
                Assert.IsTrue(connection.Id != -1);
                connection=IntegrationController.GetConnection(connection.Id);

                string newPassword = "password2";
                ((FTPConnectionSettings) connection.Settings).Password = newPassword;
                connection.Id = IntegrationController.SaveConnection(connection);

                 //check the id!=-1
                Assert.IsTrue(connection.Id != -1);
                
                //retrieve the saved connection
                connection=IntegrationController.GetConnection(connection.Id);

                //check that the password on the save object is the same as the original 
                //despite clearing the password before saving
                ((FTPConnectionSettings) connection.Settings).Password = newPassword;
            }
        }


        [Test]
        public void GetConnectionsByType()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                Connection connection = PopulateMSMQConnection();
                int id = IntegrationController.SaveConnection(connection);
                if (id > -1)
                {
                    //retrieve all connections and the one we saved should return at least
                    List<Connection> connections =
                        IntegrationController.GetConnections(connection.ConnectionType, connection.ChannelType);
                    //so the count should be >0
                    Assert.IsTrue(connections.Count > 0);
                    //check for our new id
                    Assert.IsTrue(connections.Find(delegate(Connection currentItem)
                                                       {
                                                           return
                                                               (currentItem.Id == id &&
                                                                currentItem.ConnectionType == connection.ConnectionType &&
                                                                currentItem.ChannelType == connection.ChannelType);
                                                       }) != null);
                }
            }
        }

        [Test]
        public void GetConnection()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                Connection connection = PopulateMSMQConnection();
                int id = IntegrationController.SaveConnection(connection);
                if (id > -1)
                {
                    Assert.IsNotNull(IntegrationController.GetConnection(id));
                }
            }
        }

        [Test]
        public void DeleteConnection()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                Connection connection = PopulateMSMQConnection();
                connection.Id = IntegrationController.SaveConnection(connection);
                if (connection.Id > -1)
                {
                    Assert.IsTrue(IntegrationController.DeleteConnection(connection));
                }
            }
        }

        [Test]
        public void GetTasks()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IntegrationController integrationController = new IntegrationController();
                //add a task
                Task task = PopulateTask();
                int id = integrationController.SaveTask(task);
                if (id > -1)
                {
                    //retrieve all connections and the one we saved should return at least
                    List<Task> tasks = integrationController.GetTasks();
                    //so the count should be >0
                    Assert.IsTrue(tasks.Count > 0);
                    //check for our new id
                    Assert.IsTrue(tasks.Find(delegate(Task currentItem)
                                                 {
                                                     return currentItem.Id == id;
                                                 }) != null);
                }
            }
        }

        private Task PopulateTask()
        {
            Task task = new Task();

            //set up and save a destination connection
            Connection destinationConnection = PopulateMSMQConnection();
            Connection sourceConnection = destinationConnection.DeepClone<Connection>();

            task.DestinationConnectionId = IntegrationController.SaveConnection(destinationConnection);
            destinationConnection.Id = task.DestinationConnectionId;
            task.DestinationConnection = destinationConnection;
            task.DestinationConnectionIdentifier = "DestId";

            //set up and save a source connection
            task.SourceConnectionId = IntegrationController.SaveConnection(sourceConnection);
            sourceConnection.Id = task.SourceConnectionId;
            task.SourceConnection = sourceConnection;
            task.SourceConnectionIdentifier = "SourceId";

            task.DataFileExtension = "DAT";
            task.DestinationDirectory = "DestDir";
            task.FlagFileExtension = "FLG";
            task.Frequency = 60;
            task.MonitorSequenceNumber = false;
            task.Name = "Task";
            task.RemoveDataFile = false;
            task.RemoveFlagFile = false;
            task.SequenceNumber = 1;
            task.SourceDirectory = "";
            task.UpdatedBy = "UnitTest";

            return task;
        }

        [Test]
        public void GetDownTasks()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a task
                ConnectionDownTime connectionDownTime = PopulateDownTime();
                int id = IntegrationController.SaveDownTime(connectionDownTime);
                if (id > -1)
                {
                    //retrieve all connection down times and the one we saved should return at least
                    List<ConnectionDownTime> connectionDownTimes =
                        IntegrationController.GetDownTimes(connectionDownTime.ConnectionId);
                    //so the count should be >0
                    Assert.IsTrue(connectionDownTimes.Count > 0);
                    //check for our new id also
                    Assert.IsTrue(connectionDownTimes.Find(delegate(ConnectionDownTime currentItem)
                                                               {
                                                                   return (currentItem.Id == id && currentItem.ConnectionId==connectionDownTime.ConnectionId);
                                                               }) != null);
                }
            }
        }
        
        [Test]
        public void DeleteTask()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IntegrationController integrationController = new IntegrationController();
                //add a task
                Task task = PopulateTask();
                task.Id = integrationController.SaveTask(task);
                if (task.Id > -1)
                {
                    Assert.IsTrue(integrationController.DeleteTask(task));
                }
            }
        }

        [Test]
        public void SaveTask()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IntegrationController integrationController = new IntegrationController();
                //add a task
                Task task = PopulateTask();
                Assert.IsTrue(integrationController.SaveTask(task) != -1);
            }
        }


        [Test]
        public void DeleteDownTime()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a downtime
                ConnectionDownTime connectionDownTime = PopulateDownTime();
                connectionDownTime.Id = IntegrationController.SaveDownTime(connectionDownTime);
                if (connectionDownTime.Id > -1)
                {
                    Assert.IsTrue(IntegrationController.DeleteDownTime(connectionDownTime));
                }
            }
        }

        private static ConnectionDownTime PopulateDownTime()
        {
            ConnectionDownTime connectionDownTime = new ConnectionDownTime();
            //set up and save a destination connection
            connectionDownTime.Connection = PopulateMSMQConnection();
            connectionDownTime.ConnectionId = IntegrationController.SaveConnection(connectionDownTime.Connection);
            connectionDownTime.Connection.Id = connectionDownTime.ConnectionId;

            connectionDownTime.DayOfWeek =DayOfWeek.Monday;
            connectionDownTime.EndTime =DateTime.Now;
            connectionDownTime.StartTime =DateTime.Now;
            connectionDownTime.UpdatedBy = "UnitTest";

            return connectionDownTime;
        }


        [Test]
        public void SaveDownTime()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a task
                ConnectionDownTime connectionDownTime = PopulateDownTime();
                Assert.IsTrue(IntegrationController.SaveDownTime(connectionDownTime) != -1);
            }
        }

        [Test]
        public void GetDownTime()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a connection
                ConnectionDownTime connectionDownTime = PopulateDownTime();
                int id = IntegrationController.SaveDownTime(connectionDownTime);
                if (id > -1)
                {
                    Assert.IsNotNull(IntegrationController.GetDownTime(id));
                }
            }
        }

        [Test]
        public void GetDownTimes()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //add a task
                ConnectionDownTime connectionDownTime = PopulateDownTime();
                int id = IntegrationController.SaveDownTime(connectionDownTime);
                if (id > -1)
                {
                    //retrieve all connection down times and the one we saved should return at least
                    List<ConnectionDownTime> connectionDownTimes =
                        IntegrationController.GetDownTimes(connectionDownTime.ConnectionId);
                    //so the count should be >0
                    Assert.IsTrue(connectionDownTimes.Count > 0);
                    //check for our new id also
                    Assert.IsTrue(connectionDownTimes.Find(delegate(ConnectionDownTime currentItem)
                                                               {
                                                                   return currentItem.Id == id;
                                                               }) != null);
                }
            }
        }
    }
}