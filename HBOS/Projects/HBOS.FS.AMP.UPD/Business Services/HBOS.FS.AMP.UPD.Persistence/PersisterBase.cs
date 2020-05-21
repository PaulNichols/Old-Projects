using System;
using System.Data.SqlClient;
using HBOS.FS.AMP.UPD.Exceptions;
using HBOS.FS.Support.Tex;

namespace HBOS.FS.AMP.UPD.Persistence
{
	/// <summary>
	/// Base class for all persisters
	/// </summary>
	public abstract class PersisterBase
	{
		#region Constructors

		/// <summary>
		/// Creates a new <see cref="PersisterBase"/> instance.
		/// </summary>
		protected PersisterBase()
		{
			T.E();
			T.X();
		}

		/// <summary>
		/// Creates a new <see cref="PersisterBase"/> instance.
		/// </summary>
		/// <param name="connectionString">Connection string to use for database connections.</param>
		protected PersisterBase(string connectionString)
		{
			T.E();
			m_connectionString = connectionString;
			T.X();
		}

		#endregion
		
		#region ConnectionString
		/// <summary>
		/// The member variable for the ConnectionString property.
		/// </summary>
		private string m_connectionString;

		/// <summary>
		/// The connection string to be used for the data access layer.
		/// </summary>
		public string ConnectionString
		{
			get { return m_connectionString; }
			set { m_connectionString = value; }
		}
		#endregion

		#region Exception Handing Helper Methods
		/// <summary>
		/// Wraps and throws an existing SqlException.
		/// Exception message can be customised by overriding one or more of the Get...ExceptionMessage() methods.
		/// </summary>
		/// <example>
		/// string storedProc = "spGetSomeData"
		/// 
		/// sqlParameters[] spParams = new sqlParameters[1]'
		/// spParams[0] = new SqlParameter("@sCompanyCode", SqlDbType.VarChar, 10 ); 
		/// spParams[0].Value = companyCode;
		/// try
		/// {
		/// 	using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString,
		///            CommandType.StoredProcedure, storedProc, spParams))
		///     {
		///       // do something with the reader
		///     }
		///		
		///	}
		///	catch(SqlException ex)
		///	{
		///		this.ThrowDBException(ex,ConnectionString,storedProc,spParams);
		///	}
		/// </example>
		/// <param name="ex">The original SqlException thrown. This is wrapped as the InnerException of the exception thrown</param>
		/// <param name="commandText">The sql command text that failed.</param>
		/// <param name="parms">Array of parameters sent to the command.</param>
		/// <exception cref="ConstraintViolationException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to insert item as an existing item already exists with unique key OR some other constraint violation has occurred</exception>
		/// <exception cref="DatabaseException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="InvalidSqlParameterException">Unable to create/update/delete/load items</exception>
		protected void ThrowDBException(SqlException ex, string commandText, SqlParameter[] parms)
		{
			T.E();
			try
			{
				string exMessage = string.Empty;

				switch (ex.Number)
				{
					case (int) DatabaseError.NullParameter:
						exMessage = GetNullParameterExceptionMessage();
						throw new InvalidSqlParameterException(exMessage, DatabaseInfo, commandText, parms, ex);

					case (int) DatabaseError.ConstraintViolation:
						exMessage = GetConstraintViolationExceptionMessage();
						throw new ConstraintViolationException(exMessage, DatabaseInfo, commandText, parms, ex);

					case (int) DatabaseError.ConstraintViolationDuplicateKey:
						exMessage = GetConstraintViolationDuplicateKeyExceptionMessage();
						throw new ConstraintViolationException(exMessage, DatabaseInfo, commandText, parms, ex);

					case (int) DatabaseError.CustomError: // Custom error thrown.
						switch (ex.Message)
						{
							case "Concurrency violation":
								exMessage = GetConcurrencyViolationExceptionMessage();
								throw new ConcurrencyViolationException(exMessage, DatabaseInfo, commandText, parms, ex);

							default:
								throwDefaultDatabaseException(ex, DatabaseInfo);
								break;

						}
						break;

					default:
						throwDefaultDatabaseException(ex, DatabaseInfo);
						break;
				}
			}
			catch (DatabaseException)
			{
				throw;
			}
			catch (Exception)
			{
				throw ex; // if we haven't thrown a DatabaseException, throw the SqlException			
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Wraps and throws an existing SqlException.
		/// Exception message can be customised by overriding one or more of the Get...ExceptionMessage() methods.
		/// </summary>
		/// <example>
		/// SqlCommand cmd = new SqlCommand(connection,"spGetSomeData");
		/// try
		/// {
		///		cmd.ExecuteScalar();
		///	}
		///	catch(SqlException ex)
		///	{
		///		this.ThrowDBException(ex,cmd);
		///	}
		/// </example>
		/// <param name="ex">The original SqlException thrown. This is wrapped as the InnerException of the exception thrown</param>
		/// <param name="cmd">The SqlCommand that failed.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key OR some other constraint violation has occurred</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update/delete/load items</exception>
		protected void ThrowDBException(SqlException ex, SqlCommand cmd)
		{
			T.E();

			try
			{
				ThrowDBException(ex, cmd.CommandText, cmd.Parameters);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Wraps and throws an existing SqlException.
		/// Exception message can be customised by overriding one or more of the Get...ExceptionMessage() methods.
		/// </summary>
		/// <example>
		/// string storedProc = "spGetSomeData"
		/// 
		/// sqlParameterCollection spParams = new sqlParametersCollection()
		/// spParams.Add("@sCompanyCode", this.companyCode); 
		/// try
		/// {
		/// 	using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString,
		///            CommandType.StoredProcedure, storedProc, ConvertParameterCollectionToArray(spParams)))
		///     {
		///       // do something with the reader
		///     }
		///		
		///	}
		///	catch(SqlException ex)
		///	{
		///		this.ThrowDBException(ex,ConnectionString,storedProc,spParams);
		///	}
		/// </example>
		/// <param name="ex">The original SqlException thrown. This is wrapped as the InnerException of the exception thrown</param>
		/// <param name="commandText">The Sql that failed.</param>
		/// <param name="parameters">Parameter collection used with the query</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key OR some other constraint violation has occurred</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update/delete/load items</exception>
		protected void ThrowDBException(SqlException ex, string commandText, SqlParameterCollection parameters)
		{
			T.E();
			SqlParameter[] parms;
			try
			{
				try
				{
					parms = ConvertParameterCollectionToArray(parameters);
				}
				catch
				{
					throw ex;
				}

				ThrowDBException(ex, commandText, parms);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Wraps and throws an existing SqlException.
		/// Exception message can be customised by overriding one or more of the Get...ExceptionMessage() methods.
		/// </summary>
		/// <example>
		/// string storedProc = "spGetSomeData"
		/// try
		/// {
		/// 	using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString,
		///            CommandType.StoredProcedure, storedProc))
		///     {
		///       // do something with the reader
		///     }
		///		
		///	}
		///	catch(SqlException ex)
		///	{
		///		this.ThrowDBException(ex,ConnectionString,storedProc);
		///	}
		/// </example>
		/// <param name="ex">The original SqlException thrown. This is wrapped as the InnerException of the exception thrown</param>
		/// <param name="commandText">The Sql that failed.</param>
		/// <exception cref="InvalidSqlParameterException">Attempt to call stored proc with an invalid parameter</exception>
		/// <exception cref="ConstraintViolationException">Unable to insert item as an existing item already exists with unique key OR some other constraint violation has occurred</exception>
		/// <exception cref="ConcurrencyViolationException">Unable to update item as item has been modified since last load</exception>
		/// <exception cref="DatabaseException">Unable to create/update/delete/load items</exception>
		protected void ThrowDBException(SqlException ex, string commandText)
		{
			T.E();
			try
			{
				ThrowDBException(ex, commandText, new SqlParameter[0]);
			}
			finally
			{
				T.X();
			}
		}




		/// <summary>
		/// Gets the database info string that is used in DatabaseExceptions.
		/// </summary>
		/// <value></value>
		protected string DatabaseInfo
		{
			get
			{
				T.E();
				string result = string.Empty;
				SqlConnection cn = new SqlConnection(ConnectionString);

				try
				{
					result = cn.DataSource + " " + cn.Database;
				}
				finally
				{
					cn.Dispose();
				}

				T.X();
				return result;
			}
		}

		private void throwDefaultDatabaseException(SqlException ex, string connectionString)
		{
			T.E();
			try
			{
				string exMessage = GetDatabaseExceptionMessage();
				throw new DatabaseException(exMessage, connectionString, ex);
			}
			finally
			{
				T.X();
			}
		}

		/// <summary>
		/// Gets the exception message for a null parameter condition.
		/// Override to provide a customised message for your class.
		/// </summary>
		/// <returns>The message for the exception</returns>
		protected virtual string GetNullParameterExceptionMessage()
		{
			return "The query cannot be run because of a null parameter";
		}

		/// <summary>
		/// Gets the exception message for a constraint violation condition.
		/// Override to provide a customised message for your class.
		/// </summary>
		/// <returns>The message for the exception</returns>
		protected virtual string GetConstraintViolationExceptionMessage()
		{
			return "A constraint violation occured during the database operation";
		}

		/// <summary>
		/// Gets the exception message for a duplicate key condition.
		/// Override to provide a customised message for your class.
		/// </summary>
		/// <returns>The message for the exception</returns>
		protected virtual string GetConstraintViolationDuplicateKeyExceptionMessage()
		{
			return "A duplicate key constraint violation occured during the database operation";
		}

		/// <summary>
		/// Gets the exception message for a currency violation condition.
		/// Override to provide a customised message for your class.
		/// </summary>
		/// <returns>The message for the exception</returns>
		protected virtual string GetConcurrencyViolationExceptionMessage()
		{
			return "A concurrency violation occured during the database operation";
		}

		/// <summary>
		/// Gets the exception message for an unspecified database condition.
		/// Override to provide a customised message for your class.
		/// </summary>
		/// <returns>The message for the exception</returns>
		protected virtual string GetDatabaseExceptionMessage()
		{
			return "An error occured during a database operation";
		}

		#endregion

		#region Parameter Helper Methods

		/// <summary>
		/// Converts the parameter collection to array for use with Microsoft Data Application Block.
		/// </summary>
		/// <param name="collection">Collection.</param>
		protected SqlParameter[] ConvertParameterCollectionToArray(SqlParameterCollection collection)
		{
			T.E();
			SqlParameter[] array = null;
			if (collection == null || collection.Count == 0)
			{
				array = new SqlParameter[0];
			}
			else
			{
				array = new SqlParameter[collection.Count];
				collection.CopyTo(array,0);
			}

			T.X();
			return array;
		}

		#endregion
	}
}