/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:40:04
			Generator name: MAINSERVER\Administrator
			Template last update: 15/01/2005 18:36:57
			Template revision: 1247

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [Bob]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.

	    More information: http://www.microsoft.com/france/msdn/olymars
	Latest interim build: http://www.olymars.net/latest.zip
	       Author's blog: http://blogs.msdn.com/olymars
*/

using System;
using System.Data.SqlClient;
using Params = Bob.DataClasses.Parameters;
using SPs = Bob.DataClasses.StoredProcedures;

namespace Bob.BusinessComponents {

	/// <summary>
	/// [To be supplied.]
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:40:04", SqlObjectDependancyName="Job", SqlObjectDependancyRevision=512)]
#endif
	public class Job_Collection : IBusinessComponentCollection, System.Collections.IEnumerable, System.ComponentModel.IListSource {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public enum Job_CollectionParentType {

			/// <summary>
			/// [To be supplied.]
			/// </summary>
			None,
			/// <summary>
			/// [To be supplied.]
			/// </summary>
			Col_CustomerId
		}

		private System.Collections.ArrayList internalRecords = null;
		private bool recordsAreLoaded = false;
		private IBusinessComponentRecord parent = null;
		private Job_CollectionParentType parentType = Job_CollectionParentType.None;
		internal string connectionString = String.Empty;
		internal SqlConnection sqlConnection = null;
		internal SqlTransaction sqlTransaction = null;
		private Bob.DataClasses.ConnectionType lastKnownConnectionType = Bob.DataClasses.ConnectionType.None;
		private int insertCommandTimeOut = 30;
		private int deleteCommandTimeOut = 30;
		private int selectCollectionCommandTimeOut = 30;
		private int updateCommandTimeOut = 30;
		private int selectCommandTimeOut = 30;
		private System.Collections.ArrayList addedRecords = null;

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Collections.ArrayList AddedRecords {

			get {

				if (this.addedRecords == null) this.addedRecords = new System.Collections.ArrayList();
				return(this.addedRecords);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public IBusinessComponentRecord Parent {

			get {

				return(parent);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Job_CollectionParentType ParentType {

			get {

				return(this.parentType);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public bool ContainsListCollection {

			get {

				return(true);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public System.Collections.IList GetList() {

			return(internalRecords);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public int Count {

			get {

				if (!this.recordsAreLoaded) {

					Refresh();
				}

				return(internalRecords.Count);
			}
		}

		/// <summary>
		/// Returns the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		public System.String ConnectionString {

			get {

				return(this.connectionString);
			}
		}
		/// <summary>
		/// Returns the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection {

			get {

				return(this.sqlConnection);
			}
		}

		/// <summary>
		/// Returns the System.Data.SqlClient.SqlTransaction to be used
		/// against the SQL Server database.
		/// </summary>
		public System.Data.SqlClient.SqlTransaction SqlTransaction {

			get {

				return(this.sqlTransaction);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString, SqlConnection
		/// or SqlTransaction
		/// </summary>
		public Bob.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.lastKnownConnectionType );
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Insert operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int InsertCommandTimeOut {

			get {

				return(this.insertCommandTimeOut);
			}

			set {

				this.insertCommandTimeOut = value;
				if (this.insertCommandTimeOut <= 0) {

					this.insertCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Delete operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int DeleteCommandTimeOut {

			get {

				return(this.deleteCommandTimeOut);
			}

			set {

				this.deleteCommandTimeOut = value;
				if (this.deleteCommandTimeOut <= 0) {

					this.deleteCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Select operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int SelectCollectionCommandTimeOut {

			get {

				return(this.selectCollectionCommandTimeOut);
			}

			set {

				this.selectCollectionCommandTimeOut = value;
				if (this.selectCollectionCommandTimeOut <= 0) {

					this.selectCollectionCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Update operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int UpdateCommandTimeOut {

			get {

				return(this.updateCommandTimeOut);
			}

			set {

				this.updateCommandTimeOut = value;
				if (this.updateCommandTimeOut <= 0) {

					this.updateCommandTimeOut = 30;
				}
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand) for the Select operation.
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int SelectCommandTimeOut {

			get {

				return(this.selectCommandTimeOut);
			}

			set {

				this.selectCommandTimeOut = value;
				if (this.selectCommandTimeOut <= 0) {

					this.selectCommandTimeOut = 30;
				}
			}
		}

		private System.Data.SqlTypes.SqlInt32 col_CustomerId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_CustomerId {

			get {

				return(this.col_CustomerId);
			}
			set {

				this.col_CustomerId = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_CustomerId">[To be supplied.]</param>
		/// <param name="parent">[To be supplied.]</param>
		internal void LoadFrom_CustomerId(System.Data.SqlTypes.SqlInt32 col_CustomerId, Customer parent) {
		
			if (col_CustomerId.IsNull) {

				throw new ArgumentException("Parameter can not be null", "col_CustomerId");
			}

			if (parent == null) {

				throw new ArgumentException("Parameter can not be null", "parent");
			}

			this.col_CustomerId = col_CustomerId;
			this.parent = parent;
			this.parentType = Job_CollectionParentType.Col_CustomerId;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		public Job_Collection(string connectionString) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString != String.Empty) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "Job", CurrentRevision, OriginalRevision));
					}
				}
			}
#endif

			this.connectionString = connectionString;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		public Job_Collection(SqlConnection sqlConnection) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlConnection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlConnection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Job", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		public Job_Collection(SqlTransaction sqlTransaction) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlTransaction.Connection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlTransaction.Connection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlTransaction.Connection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Job", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlTransaction;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">[To be supplied.]</param>
		/// <param name="col_CustomerId">[To be supplied.]</param>
		public Job_Collection(string connectionString, System.Data.SqlTypes.SqlInt32 col_CustomerId) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

			string DebugConnectionString = connectionString;

			if (DebugConnectionString == System.String.Empty) {

				DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
			}

			if (DebugConnectionString == System.String.Empty) {

				DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
			}

			if (DebugConnectionString != String.Empty) {

				System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

				sqlConnection.Open();

				System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				sqlConnection.Close();

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "Job", CurrentRevision, OriginalRevision));
				}
			}
			}
#endif

			this.connectionString = connectionString;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

			this.col_CustomerId = col_CustomerId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_CustomerId">[To be supplied.]</param>
		public Job_Collection(SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 col_CustomerId) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlConnection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlConnection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Job", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

			this.col_CustomerId = col_CustomerId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_CustomerId">[To be supplied.]</param>
		public Job_Collection(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlInt32 col_CustomerId) {
			
#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				bool NotAlreadyOpened = false;
				if (sqlTransaction.Connection.State == System.Data.ConnectionState.Closed) {

					NotAlreadyOpened = true;
					sqlTransaction.Connection.Open();
				}

				System.Data.SqlClient.SqlCommand sqlCommand = sqlTransaction.Connection.CreateCommand();

				sqlCommand.CommandType = System.Data.CommandType.Text;
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Job'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Job", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlTransaction;

			this.col_CustomerId = col_CustomerId;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public IBusinessComponentRecord Add(IBusinessComponentRecord record) {
		
			Job_Record recordToAdd = record as Job_Record;

			if (recordToAdd == null) {

				throw new ArgumentException("Invalid record type. Must be 'Bob.BusinessComponents.Job_Record'.", "record");
			}

			switch (this.parentType) {

				case Job_CollectionParentType.Col_CustomerId:
					recordToAdd.Col_CustomerId = ((Customer)this.parent).CustomerID;
					break;

			}

			bool alreadyOpened = false;

			Params.spI_Job Param = new Params.spI_Job(true);
			Param.CommandTimeOut = this.insertCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_JobId = recordToAdd.Col_JobId;
			Param.Param_Description = recordToAdd.Col_Description;
			Param.Param_Price = recordToAdd.Col_Price;
			Param.Param_StartDate = recordToAdd.Col_StartDate;
			Param.Param_EndDate = recordToAdd.Col_EndDate;
			Param.Param_CustomerId = recordToAdd.Col_CustomerId;

			SPs.spI_Job Sp = new SPs.spI_Job(false);
			if (Sp.Execute(ref Param)) {

				Job_Record newRecord = null;

				switch (this.lastKnownConnectionType) {

					case Bob.DataClasses.ConnectionType.ConnectionString:
						newRecord = new Job_Record(this.connectionString, Param.Param_JobId);
						break;

					case Bob.DataClasses.ConnectionType.SqlConnection:
						newRecord = new Job_Record(this.sqlConnection, Param.Param_JobId);
						break;

					case Bob.DataClasses.ConnectionType.SqlTransaction:
						newRecord = new Job_Record(this.sqlTransaction, Param.Param_JobId);
						break;
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				if (internalRecords != null) {

					internalRecords.Add(newRecord);
				}

				if (this.addedRecords == null) this.addedRecords = new System.Collections.ArrayList();
				this.addedRecords.Add(newRecord);

				return(newRecord);
			}
			else {

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.Job_Collection", "Add");
			}	
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="Col_JobId">[To be supplied.]</param>
		public void Remove(System.Data.SqlTypes.SqlInt32 Col_JobId) {

			bool alreadyOpened = false;

			Params.spD_Job Param = new Params.spD_Job(true);
			Param.CommandTimeOut = this.deleteCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_JobId = Col_JobId;

			SPs.spD_Job Sp = new SPs.spD_Job(true);

			Sp.Execute(ref Param);

			CloseConnection(Sp.Connection, alreadyOpened);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		public void Remove(IBusinessComponentRecord record) {
		
			Job_Record recordToDelete = record as Job_Record;

			if (recordToDelete == null) {

				throw new ArgumentException("Invalid record type. Must be 'Bob.BusinessComponents.Job_Record'.", "record");
			}

			bool alreadyOpened = false;

			Params.spD_Job Param = new Params.spD_Job(true);
			Param.CommandTimeOut = this.deleteCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_JobId = recordToDelete.Col_JobId;

			SPs.spD_Job Sp = new SPs.spD_Job(true);

			Sp.Execute(ref Param);
			CloseConnection(Sp.Connection, alreadyOpened);

			if (internalRecords != null) {

				internalRecords.Remove(recordToDelete);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public void Refresh() {

			internalRecords = new System.Collections.ArrayList();

			bool alreadyOpened = false;

			Params.spS_Job_Display Param = new Params.spS_Job_Display(true);
			Param.CommandTimeOut = this.selectCollectionCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			if (!this.col_CustomerId.IsNull) {

				Param.Param_CustomerId = this.col_CustomerId;
			}

			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_Job_Display Sp = new SPs.spS_Job_Display(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				while (sqlDataReader.Read()) {

					Job_Record record = null;

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							record = new Job_Record(this.connectionString, sqlDataReader.GetSqlInt32(SPs.spS_Job_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							record = new Job_Record(this.sqlConnection, sqlDataReader.GetSqlInt32(SPs.spS_Job_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							record = new Job_Record(this.sqlTransaction, sqlDataReader.GetSqlInt32(SPs.spS_Job_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;
					}


					record.UpdateCommandTimeOut = this.updateCommandTimeOut;
					record.SelectCommandTimeOut = this.selectCommandTimeOut;

					if (sqlDataReader.GetFieldType(SPs.spS_Job_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

						record.displayName = sqlDataReader.GetString(SPs.spS_Job_Display.Resultset1.Fields.Column_Display.ColumnIndex);
					}
					else {

						record.displayName = sqlDataReader.GetValue(SPs.spS_Job_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
					}

					internalRecords.Add(record);
				}

				if (sqlDataReader != null && !sqlDataReader.IsClosed) {

					sqlDataReader.Close();
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				this.recordsAreLoaded = true;
			}
			else {

				if (sqlDataReader != null && !sqlDataReader.IsClosed) {

					sqlDataReader.Close();
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				this.recordsAreLoaded = false;
				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.Job_Collection", "Refresh");
			}
		}

		private void CloseConnection(SqlConnection connection, bool alreadyOpened) {

			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					if (connection != null && connection.State == System.Data.ConnectionState.Open) {

						connection.Close();
						connection.Dispose();
					}
					break;

				case Bob.DataClasses.ConnectionType.SqlConnection:
					if (!alreadyOpened && this.sqlConnection.State == System.Data.ConnectionState.Open) this.sqlConnection.Close();
					break;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public System.Collections.IEnumerator GetEnumerator() {

			if (!this.recordsAreLoaded) {

				Refresh();
			}

			return(internalRecords.GetEnumerator());
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="index">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public Job_Record this[int index] {

			get {
				
				return((Job_Record)internalRecords[index]);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_JobId">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public Job_Record this[System.Data.SqlTypes.SqlInt32 col_JobId] {

			get {

				foreach(Job_Record record in internalRecords) {

					bool equality = true;

					equality = equality && (record.Col_JobId == col_JobId).Value;

					if (equality) return(record);
				}
				return(null);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new Job_Collection MemberwiseClone() {

			Job_Collection newCollection = null;

			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					newCollection = new Job_Collection(this.connectionString);
					break;
					
				case Bob.DataClasses.ConnectionType.SqlConnection:
					newCollection = new Job_Collection(this.sqlConnection);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					newCollection = new Job_Collection(this.sqlTransaction);
					break;
			}
	
			newCollection.recordsAreLoaded = this.recordsAreLoaded;
			newCollection.parent = null;

			newCollection.internalRecords = new System.Collections.ArrayList();
			foreach (Job_Record record in this.internalRecords) {

				newCollection.internalRecords.Add(record);
			}

			return(newCollection);
		}
	}
}
