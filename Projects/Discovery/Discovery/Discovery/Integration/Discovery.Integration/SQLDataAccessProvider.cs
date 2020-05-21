using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discovery.Utility.Configuration;
using Discovery.Utility.DataAccess.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.Integration.DataAccess
{
    /// <summary>
    /// A class to provide the SQL data access provider which is in integration folder
    /// with namespace Discovery.Integration.DataAccess
    /// </summary>
    public class SQLDataAccessProvider : Integration.DataAccess.DataAccessProvider
    {
        private const int ERRORVALUE = -1;
        private string connectionString;
        private string upgradeConnectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        //private ProviderConfiguration m_providerConfiguration =
        //    ProviderConfiguration.GetProviderConfiguration(ProviderType);


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get { return connectionString; }
        }

        /// <summary>
        /// Gets the provider path.
        /// </summary>
        /// <value>The provider path.</value>
        public string ProviderPath
        {
            get { return providerPath; }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        /// <value>The object qualifier.</value>
        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        /// <value>The database owner.</value>
        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <returns></returns>
        private IDataReader ExecuteReader(string spNameWithoutQualifier)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), new object[] {});
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <returns></returns>
        private IDataReader ExecuteReader(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <returns></returns>
        private bool ExecuteNonQuery(string spNameWithoutQualifier, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            return
                (database.ExecuteNonQuery(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters) > 0)
                    ? true
                    : false;
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="errorValue">The error value.</param>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <returns></returns>
        private T ExecuteScalar<T>(string spNameWithoutQualifier, T errorValue, params object[] sqlParameters)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            object returnValue =
                database.ExecuteScalar(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
            T identity = default(T);

            if (returnValue is T && !returnValue.Equals(errorValue))
            {
                identity = (T) returnValue;
            }
            else
            {
                throw new ConcurrencyException("No Records were Updated, please reload your data..");
            }

            return identity;
        }


        /// <summary>
        /// Gets the command with parameters.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="openConnection">if set to <c>true</c> [open connection].</param>
        /// <returns></returns>
        private DbCommand GetCommandWithParameters(string spNameWithoutQualifier, bool openConnection)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
            database.DiscoverParameters(command);
            if (openConnection)
            {
                command.Connection = database.CreateConnection();
                command.Connection.Open();
            }

            return command;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <param name="openConnection">if set to <c>true</c> [open connection].</param>
        /// <returns></returns>
        private DbCommand GetCommand(string spNameWithoutQualifier, bool openConnection)
        {
            Database database = DatabaseFactory.CreateDatabase(ConnectionString);
            DbCommand command = database.GetStoredProcCommand(String.Concat(ObjectQualifier, spNameWithoutQualifier));
            // database.DiscoverParameters(command);
            if (openConnection)
            {
                command.Connection = database.CreateConnection();
                command.Connection.Open();
            }

            return command;
        }

        /// <summary>
        /// Gets the command with parameters.
        /// </summary>
        /// <param name="spNameWithoutQualifier">The sp name without qualifier.</param>
        /// <returns></returns>
        private DbCommand GetCommandWithParameters(string spNameWithoutQualifier)
        {
            return GetCommandWithParameters(spNameWithoutQualifier, false);
        }

        /// <summary>
        /// Initialises the specified provider name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        protected override void Initialise(Provider provider)
        {
            // Read the attributes for this provider
            connectionString = provider.Attributes["connectionString"];
            upgradeConnectionString = provider.Attributes["upgradeConnectionString"];
            providerPath = provider.Attributes["provIderPath"];
            objectQualifier = provider.Attributes["objectQualifier"];
            databaseOwner = provider.Attributes["databaseOwner"];

            if (objectQualifier != "" && !objectQualifier.EndsWith("_"))
            {
                // Append "_" to qualifier name if qualifier specified and name does not end with "_"
                objectQualifier += "_";
            }

            if (databaseOwner != "" && !databaseOwner.EndsWith("."))
            {
                // Append "." to database owner if owner specified and does not end with "."
                databaseOwner += ".";
            }

            // See if we have an upgrade connection string
            if ("" == upgradeConnectionString)
            {
                upgradeConnectionString = connectionString;
            }
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetTasks()
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetTasks", new object[] { });
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetTask(int taskId)
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetTask", new object[] { taskId});
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <returns></returns>
        public override bool DeleteTask(Task taskToDelete)
        {
            bool returnValue = false;
            try
            {
                returnValue = ExecuteNonQuery("DeleteTask",
                                              new object[]
                                                  {
                                                      taskToDelete.Id
                                                  });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {

                    //ExceptionPolicy.HandleException(new ForeignKeyConstraint(ex.Message, ex),
                    //                                "DataProvider Policy"))
                    throw ex;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the task.
        /// </summary>
        /// <returns></returns>
        public override int SaveTask(Task taskToSave)
        {
            int returnValue = ERRORVALUE;
            try
            {
                returnValue = ExecuteScalar<int>("SaveTask", -1,
                                                 new object[]
                                                     {
                                                        taskToSave.Id,
                                                        taskToSave.Name,
				                                        taskToSave.RemoveDataFile,
				                                        taskToSave.RemoveFlagFile ,
				                                        taskToSave.DataFileExtension ,
				                                        taskToSave.FlagFileExtension ,
				                                        taskToSave.MonitorSequenceNumber ,
				                                        taskToSave.SequenceNumber ,
				                                        taskToSave.SourceDirectory ,
				                                        taskToSave.DestinationDirectory ,
				                                        taskToSave.SourceConnectionId ,
				                                        taskToSave.SourceConnectionIdentifier	,
				                                        taskToSave.DestinationConnectionId ,
				                                        taskToSave.DestinationConnectionIdentifier ,
				                                        taskToSave.Frequency,
                                                        taskToSave.UpdatedBy,
                                                        taskToSave.CheckSum
                                                     });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    //if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider Policy"))
                    //    throw;
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetConnections()
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetConnections");
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }
        
          /// <summary>
        /// Gets the connections.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetConnections(Connection.ConnectionTypeEnum connectionType, Connection.ChannelTypeEnum channelType)
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetConnectionsByConnectionAndChannelType", new object[] { connectionType, channelType });
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetConnection(int connectionId)
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetConnection", new object[] {connectionId});
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Removes the connection.
        /// </summary>
        /// <param name="connectionToRemove">The connection to remove.</param>
        /// <returns></returns>
        public override bool DeleteConnection(Connection connectionToRemove)
        {
            bool returnValue = false;
            try
            {
                returnValue = ExecuteNonQuery("DeleteConnection",
                                              new object[]
                                                  {
                                                      connectionToRemove.Id
                                                  });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                   
                        //ExceptionPolicy.HandleException(new ForeignKeyConstraint(ex.Message, ex),
                        //                                "DataProvider Policy"))
                        throw ex;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
               // if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the connection.
        /// </summary>
        /// <param name="connectionToSave">The connection to save.</param>
        /// <returns></returns>
        public override int SaveConnection(Connection connectionToSave)
        {
            int returnValue = ERRORVALUE;
            try
            {
                returnValue = ExecuteScalar<int>("SaveConnection", -1,
                                                 new object[]
                                                     {
                                                         connectionToSave.Id,
                                                         connectionToSave.Name,
                                                         connectionToSave.ScheduleId,
                                                         connectionToSave.Active,
                                                         connectionToSave.ConnectionType,
                                                         connectionToSave.ChannelType,
                                                         connectionToSave.SettingsSerialised,
                                                         connectionToSave.UpdatedBy,
                                                         connectionToSave.CheckSum
                                                     });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    //if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider Policy"))
                    //    throw;
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public override IDataReader GetSchedule(int scheduleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the day details for schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public override IDataReader GetDayDetailsForSchedule(int scheduleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the schedule day.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetScheduleDay()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the schedules.
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetSchedules()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the down times.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns></returns>
        public override IDataReader GetDownTimes(int? connectionId)
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetDownTimes", new object[] { connectionId ==null|| connectionId==0?DBNull.Value:(object)connectionId.Value });
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Gets the down time.
        /// </summary>
        /// <param name="downTimeId">The down time id.</param>
        /// <returns></returns>
        public override IDataReader GetDownTime(int downTimeId)
        {
            IDataReader reader = null;

            try
            {
                reader = ExecuteReader("GetDownTime", new object[] { downTimeId });
            }
            catch (Exception ex)
            {
                //if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return reader;
        }

        /// <summary>
        /// Saves the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public override int SaveDownTime(ConnectionDownTime connectionDownTime)
        {
            int returnValue = ERRORVALUE;
            try
            {
                returnValue = ExecuteScalar<int>("SaveDownTime", -1,
                                                 new object[]
                                                     {
                                                        connectionDownTime.Id,
                                                        connectionDownTime.DayOfWeek,
                                                        connectionDownTime.StartTime,
                                                        connectionDownTime.EndTime,
                                                        connectionDownTime.ConnectionId,
                                                        connectionDownTime.UpdatedBy,
                                                        connectionDownTime.CheckSum
                                                     });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2627)
                {
                    //if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider Policy"))
                    //    throw;
                }
                else
                {
                    throw;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Deletes the down time.
        /// </summary>
        /// <param name="connectionDownTime">The connection down time.</param>
        /// <returns></returns>
        public override bool DeleteDownTime(ConnectionDownTime connectionDownTime)
        {
            bool returnValue = false;
            try
            {
                returnValue = ExecuteNonQuery("DeleteDownTime",
                                              new object[]
                                                  {
                                                      connectionDownTime.Id
                                                  });
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {

                    //ExceptionPolicy.HandleException(new ForeignKeyConstraint(ex.Message, ex),
                    //                                "DataProvider Policy"))
                    throw ex;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
            }
            return returnValue;
        }

    }
}