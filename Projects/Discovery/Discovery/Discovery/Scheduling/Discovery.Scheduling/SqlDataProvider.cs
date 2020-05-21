
using System; 
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web; 
using System.Collections;
using System.Diagnostics;
using Discovery.Utility.Configuration;
using Discovery.Utility;
using Discovery.Utility.DataAccess.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ForeignKeyConstraint = Discovery.Utility.DataAccess.Exceptions.ForeignKeyConstraint;

namespace Discovery.Scheduling 
{
        public class SqlDataProvider : Scheduling.DataProvider
        {
            #region Provider details

            /*************************************************************************************************
			** PRIVATE MEMBER VARIABLES
			************************************************************************************************/
            private const int ERRORVALUE = -1;
            private const string ProviderType = "schedulingdata"; 

			private ProviderConfiguration m_providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
			private string m_strConnectionString;
			private string m_strProviderPath;
			private string m_strObjectQualifier;
			private string m_strDatabaseOwner;
						
            
			/*************************************************************************************************
			** PUBLIC PROPERTIES
			************************************************************************************************/
			public string ConnectionString
			{
				get
				{
					return m_strConnectionString;
				}
			}

			public string ProviderPath
			{
				get
				{
					return m_strProviderPath;
				}
			}
		
			public string ObjectQualifier
			{
				get
				{
					return m_strObjectQualifier;
				}
			}

			public string DatabaseOwner
			{
				get
				{
					return m_strDatabaseOwner;
				}
			}

            /// <summary>
            /// Initialises the specified provider name.
            /// </summary>
            /// <param name="providerName">Name of the provider.</param>
            protected override void Initialise(string providerName)
            {
                // Read the configuration specific information for this provider
                Provider objProvider = (Provider)m_providerConfiguration.Providers[providerName];

                // Read the attributes for this provider
                m_strConnectionString = objProvider.Attributes["connectionString"];
                m_strProviderPath = objProvider.Attributes["providerPath"];
                m_strObjectQualifier = objProvider.Attributes["objectQualifier"];
                m_strDatabaseOwner = objProvider.Attributes["databaseOwner"];

                if (m_strObjectQualifier != "" && !m_strObjectQualifier.EndsWith("_"))
                {
                    // Append "_" to qualifier name if qualifier specified and name does not end with "_"
                    m_strObjectQualifier += "_";
                }

                if (m_strDatabaseOwner != "" && !m_strDatabaseOwner.EndsWith("."))
                {
                    // Append "." to database owner if owner specified and does not end with "."
                    m_strDatabaseOwner += ".";
                }
            }

            private IDataReader ExecuteReader(string spNameWithoutQualifier)
            {
                Database database = DatabaseFactory.CreateDatabase(ConnectionString);
                return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), new object[] { });
            }

            private IDataReader ExecuteReader(string spNameWithoutQualifier, params object[] sqlParameters)
            {
                Database database = DatabaseFactory.CreateDatabase(ConnectionString);
                return database.ExecuteReader(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
            }

            private bool ExecuteNonQuery(string spNameWithoutQualifier)
            {
                Database database = DatabaseFactory.CreateDatabase(ConnectionString);
                return
                    (database.ExecuteNonQuery(String.Concat(ObjectQualifier, spNameWithoutQualifier), new object[] { }) > 0)
                        ? true
                        : false;
            }

            private bool ExecuteNonQuery(string spNameWithoutQualifier, params object[] sqlParameters)
            {
                Database database = DatabaseFactory.CreateDatabase(ConnectionString);
                return
                    (database.ExecuteNonQuery(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters) > 0)
                        ? true
                        : false;
            }

            private T ExecuteScalar<T>(string spNameWithoutQualifier, T errorValue, params object[] sqlParameters)
            {
                Database database = DatabaseFactory.CreateDatabase(ConnectionString);
                object returnValue =
                    database.ExecuteScalar(String.Concat(ObjectQualifier, spNameWithoutQualifier), sqlParameters);
                T identity = default(T);

                if (returnValue is T && !returnValue.Equals(errorValue))
                {
                    identity = (T)returnValue;
                }
                else
                {
                    throw new ConcurrencyException(String.Format("Sequence {0} does not exist.", sqlParameters[0].ToString()));
                }

                return identity;
            }


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

            private DbCommand GetCommandWithParameters(string spNameWithoutQualifier)
            {
                return GetCommandWithParameters(spNameWithoutQualifier, false);
            }

            #endregion

            //  general
            /// <summary>
            /// Gets the null.
            /// </summary>
            /// <param name="Field">The field.</param>
            /// <returns></returns>
            private Object GetNull(Object Field) 
			{ 
                return Null.GetNull(Field, DBNull.Value);
            }

            #region Schedule methods

            /// <summary>
            /// Gets the schedule.
            /// </summary>
            /// <returns></returns>
            public override IDataReader GetSchedule() 
			{
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetSchedules");
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            public override IDataReader GetSchedules(string sortExpression, int startRowIndex,
                                                     int maximumRows, out int totalRows)
            {
                totalRows = 0;
                IDataReader reader = null;
                try
                {
                    reader = ExecuteReader("GetSchedules",
                                           new object[]
                                           {
                                               sortExpression,
                                               startRowIndex,
                                               maximumRows
                                           });

                    totalRows = ExecuteScalar<int>("GetSchedulesCount", -1,
                                                        new object[]
                                           {
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            /// <summary>
            /// Gets the schedule.
            /// </summary>
            /// <param name="ScheduleID">The schedule ID.</param>
            /// <returns></returns>
            public override IDataReader GetSchedule(int scheduleID) 
			{
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetSchedule",
                                           new object[]
                                           {
                                               scheduleID
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            /// <summary>
            /// Gets the schedule.
            /// </summary>
            /// <param name="eventName">Name of the event.</param>
            /// <returns></returns>
            public override IDataReader GetSchedule(string eventName) 
			{
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetScheduleByEventName",
                                           new object[]
                                           {
                                               eventName
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            /// <summary>
            /// Gets the schedule history.
            /// </summary>
            /// <param name="scheduleID">The schedule ID.</param>
            /// <returns></returns>
            public override IDataReader GetScheduleHistory(int scheduleId)
			{
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetScheduleHistory",
                                           new object[]
                                           {
                                               scheduleId
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            /// <summary>
            /// Gets the schedule histories.
            /// </summary>
            /// <param name="scheduleId">The schedule id.</param>
            /// <param name="sortExpression">The sort expression.</param>
            /// <param name="startRowIndex">Start index of the row.</param>
            /// <param name="maximumRows">The maximum rows.</param>
            /// <param name="totalRows">The total rows.</param>
            /// <returns></returns>
            public override IDataReader GetScheduleHistories(int scheduleId,
                                                             string sortExpression,
                                                             int startRowIndex,
                                                             int maximumRows,
                                                             out int totalRows)
            {
                totalRows = 0;
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetScheduleHistories",
                                           new object[]
                                           {
                                               scheduleId,
                                               sortExpression,
                                               startRowIndex,
                                               maximumRows
                                           });
                    totalRows = ExecuteScalar<int>("GetScheduleHistoryCount", -1,
                                                        new object[]
                                           {
                                               Null.GetNull(scheduleId, DBNull.Value)
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            /// <summary>
            /// Saves the schedule.
            /// </summary>
            /// <param name="scheduleItem">The schedule item.</param>
            /// <returns></returns>
            public override int SaveSchedule(ScheduleItem scheduleItem)
            {
                int returnValue = ERRORVALUE;

                try
                {
                    returnValue = ExecuteScalar<int>("SaveSchedule", ERRORVALUE,
                                                     new object[]
                                                         {
                                                            scheduleItem.Id,
                                                            scheduleItem.TypeFullName,
                                                            scheduleItem.TimeLapse,
                                                            scheduleItem.TimeLapseMeasurement,
                                                            scheduleItem.RetryTimeLapse,
                                                            scheduleItem.RetryTimeLapseMeasurement,
                                                            scheduleItem.RetainHistoryNum,
                                                            scheduleItem.AttachToEvent,
                                                            scheduleItem.CatchUpEnabled,
                                                            scheduleItem.Enabled,
                                                            scheduleItem.ObjectDependencies,
                                                            scheduleItem.UpdatedBy,
                                                            scheduleItem.CheckSum 
                                                         });
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2627)
                    {
                        if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider Policy"))
                            throw;
                    }
                    else
                    {
                        throw;
                    }
                }
                return returnValue;
            }
            
            //public override int AddSchedule(string TypeFullName, int TimeLapse, string TimeLapseMeasurement, int RetryTimeLapse, string RetryTimeLapseMeasurement, int RetainHistoryNum, string AttachToEvent, bool CatchUpEnabled, bool Enabled, string ObjectDependencies) 
            //{ 
            //    return  Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner + ObjectQualifier + "AddSchedule", 
            //                TypeFullName, 
            //                TimeLapse, 
            //                TimeLapseMeasurement, 
            //                RetryTimeLapse, 
            //                RetryTimeLapseMeasurement, 
            //                RetainHistoryNum, 
            //                AttachToEvent, 
            //                CatchUpEnabled, 
            //                Enabled, 
            //                ObjectDependencies)); 
            //} 
            
            //public override void UpdateSchedule(int ScheduleID, string TypeFullName, int TimeLapse, string TimeLapseMeasurement, int RetryTimeLapse, string RetryTimeLapseMeasurement, int RetainHistoryNum, string AttachToEvent, bool CatchUpEnabled, bool Enabled, string ObjectDependencies) 
            //{ 
            //    SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "UpdateSchedule", 
            //                ScheduleID, 
            //                TypeFullName, 
            //                TimeLapse, 
            //                TimeLapseMeasurement, 
            //                RetryTimeLapse, 
            //                RetryTimeLapseMeasurement, 
            //                RetainHistoryNum, 
            //                AttachToEvent, 
            //                CatchUpEnabled, 
            //                Enabled, 
            //                ObjectDependencies); 
            //} 

            /// <summary>
            /// Deletes the schedule.
            /// </summary>
            /// <param name="scheduleID">The schedule ID.</param>
            public override bool DeleteSchedule(int scheduleId) 
			{
                bool returnValue = false;

                try
                {
                    returnValue = ExecuteNonQuery("DeleteSchedule",
                                                  new object[]
                                                  {
                                                      scheduleId
                                                  });
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        if (
                            ExceptionPolicy.HandleException(new ForeignKeyConstraint(ex.Message, ex),
                                                            "DataProvider Policy"))
                            throw;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return returnValue;
            }

            /// <summary>
            /// Gets the schedule item settings.
            /// </summary>
            /// <param name="ScheduleID">The schedule ID.</param>
            /// <returns></returns>
            public override IDataReader GetScheduleItemSettings(int scheduleID) 
			{ 
                IDataReader reader = null;

                try
                {
                    reader = ExecuteReader("GetScheduleItemSettings",
                                           new object[]
                                           {
                                               scheduleID
                                           });
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return reader;
            }

            public override int SaveScheduleHistory(ScheduleHistoryItem scheduleHistoryItem)
            {
                int returnValue = ERRORVALUE;

                try
                {
                    returnValue = ExecuteScalar<int>("SaveScheduleHistory", ERRORVALUE,
                                                     new object[]
                                                         {
                                                            scheduleHistoryItem.Id,
                                                            scheduleHistoryItem.ScheduleId,
                                                            scheduleHistoryItem.StartDate,
                                                            scheduleHistoryItem.EndDate,
                                                            scheduleHistoryItem.Succeeded,
                                                            scheduleHistoryItem.LogNotes,
                                                            scheduleHistoryItem.NextStart
                                                         });
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2627)
                    {
                        if (ExceptionPolicy.HandleException(new ConstraintException(ex.Message, ex), "DataProvider Policy"))
                            throw;
                    }
                    else
                    {
                        throw;
                    }
                }
                return returnValue;
            }
            
            //public override int AddScheduleHistory(int scheduleID, DateTime startDate) 
            //{ 
            //    return Convert.ToInt32(SqlHelper.ExecuteScalar(
            //                ConnectionString, 
            //                DatabaseOwner + ObjectQualifier + "AddScheduleHistory", 
            //                ScheduleID, 
            //                StartDate));
            //} 
            
            //public override void UpdateScheduleHistory(int ScheduleHistoryID, DateTime EndDate, bool Succeeded, string LogNotes, DateTime NextStart) 
            //{ 
            //    SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "UpdateScheduleHistory", 
            //                ScheduleHistoryID, 
            //                GetNull(EndDate), 
            //                GetNull(Succeeded), 
            //                LogNotes, 
            //                GetNull(NextStart));
            //} 
            
            public override bool PurgeScheduleHistory() 
			{ 
                bool returnValue = false;

                try
                {
                    returnValue = ExecuteNonQuery("PurgeScheduleHistory");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        if (
                            ExceptionPolicy.HandleException(new ForeignKeyConstraint(ex.Message, ex),
                                                            "DataProvider Policy"))
                            throw;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "DataProvider Policy")) throw;
                }
                return returnValue;
            }

            #endregion
        } 
} 
