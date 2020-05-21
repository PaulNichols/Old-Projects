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
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:40:04", SqlObjectDependancyName="JobPart", SqlObjectDependancyRevision=656)]
#endif
	public class JobPart_Collection : IBusinessComponentCollection, System.Collections.IEnumerable, System.ComponentModel.IListSource {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public enum JobPart_CollectionParentType {

			/// <summary>
			/// [To be supplied.]
			/// </summary>
			None,
			/// <summary>
			/// [To be supplied.]
			/// </summary>
			Col_JobId,
			/// <summary>
			/// [To be supplied.]
			/// </summary>
			Col_JobPartTypeId,
		}

		private System.Collections.ArrayList internalRecords = null;
		private bool recordsAreLoaded = false;
		private IBusinessComponentRecord parent = null;
		private JobPart_CollectionParentType parentType = JobPart_CollectionParentType.None;
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
		public JobPart_CollectionParentType ParentType {

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

		private System.Data.SqlTypes.SqlInt32 col_JobId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobId {

			get {

				return(this.col_JobId);
			}
			set {

				this.col_JobId = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_JobId">[To be supplied.]</param>
		/// <param name="parent">[To be supplied.]</param>
		internal void LoadFrom_JobId(System.Data.SqlTypes.SqlInt32 col_JobId, Job_Record parent) {
		
			if (col_JobId.IsNull) {

				throw new ArgumentException("Parameter can not be null", "col_JobId");
			}

			if (parent == null) {

				throw new ArgumentException("Parameter can not be null", "parent");
			}

			this.col_JobId = col_JobId;
			this.parent = parent;
			this.parentType = JobPart_CollectionParentType.Col_JobId;
		}

		private System.Data.SqlTypes.SqlInt32 col_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId {

			get {

				return(this.col_JobPartTypeId);
			}
			set {

				this.col_JobPartTypeId = value;
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_JobPartTypeId">[To be supplied.]</param>
		/// <param name="parent">[To be supplied.]</param>
		internal void LoadFrom_JobPartTypeId(System.Data.SqlTypes.SqlInt32 col_JobPartTypeId, JobPartType_Record parent) {
		
			if (col_JobPartTypeId.IsNull) {

				throw new ArgumentException("Parameter can not be null", "col_JobPartTypeId");
			}

			if (parent == null) {

				throw new ArgumentException("Parameter can not be null", "parent");
			}

			this.col_JobPartTypeId = col_JobPartTypeId;
			this.parent = parent;
			this.parentType = JobPart_CollectionParentType.Col_JobPartTypeId;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		public JobPart_Collection(string connectionString) {
			
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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "JobPart", CurrentRevision, OriginalRevision));
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
		public JobPart_Collection(SqlConnection sqlConnection) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
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
		public JobPart_Collection(SqlTransaction sqlTransaction) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
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
		/// <param name="col_JobId">[To be supplied.]</param>
		/// <param name="col_JobPartTypeId">[To be supplied.]</param>
		public JobPart_Collection(string connectionString, System.Data.SqlTypes.SqlInt32 col_JobId, System.Data.SqlTypes.SqlInt32 col_JobPartTypeId) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				sqlConnection.Close();

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "JobPart", CurrentRevision, OriginalRevision));
				}
			}
			}
#endif

			this.connectionString = connectionString;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

			this.col_JobId = col_JobId;
			this.col_JobPartTypeId = col_JobPartTypeId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_JobId">[To be supplied.]</param>
		/// <param name="col_JobPartTypeId">[To be supplied.]</param>
		public JobPart_Collection(SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 col_JobId, System.Data.SqlTypes.SqlInt32 col_JobPartTypeId) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

			this.col_JobId = col_JobId;
			this.col_JobPartTypeId = col_JobPartTypeId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_JobId">[To be supplied.]</param>
		/// <param name="col_JobPartTypeId">[To be supplied.]</param>
		public JobPart_Collection(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlInt32 col_JobId, System.Data.SqlTypes.SqlInt32 col_JobPartTypeId) {
			
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlTransaction;

			this.col_JobId = col_JobId;
			this.col_JobPartTypeId = col_JobPartTypeId;
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public IBusinessComponentRecord Add(IBusinessComponentRecord record) {
		
			JobPart_Record recordToAdd = record as JobPart_Record;

			if (recordToAdd == null) {

				throw new ArgumentException("Invalid record type. Must be 'Bob.BusinessComponents.JobPart_Record'.", "record");
			}

			switch (this.parentType) {

				case JobPart_CollectionParentType.Col_JobId:
					recordToAdd.Col_JobId = ((Job_Record)this.parent).Col_JobId;
					break;

				case JobPart_CollectionParentType.Col_JobPartTypeId:
					recordToAdd.Col_JobPartTypeId = ((JobPartType_Record)this.parent).Col_JobPartTypeId;
					break;

			}

			bool alreadyOpened = false;

			Params.spI_JobPart Param = new Params.spI_JobPart(true);
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

			Param.Param_JobPartId = recordToAdd.Col_JobPartId;
			Param.Param_JobId = recordToAdd.Col_JobId;
			Param.Param_Description = recordToAdd.Col_Description;
			Param.Param_JobPartTypeId = recordToAdd.Col_JobPartTypeId;
			Param.Param_Units = recordToAdd.Col_Units;
			Param.Param_PricePerUnit = recordToAdd.Col_PricePerUnit;
			Param.Param_TotalPrice = recordToAdd.Col_TotalPrice;

			SPs.spI_JobPart Sp = new SPs.spI_JobPart(false);
			if (Sp.Execute(ref Param)) {

				JobPart_Record newRecord = null;

				switch (this.lastKnownConnectionType) {

					case Bob.DataClasses.ConnectionType.ConnectionString:
						newRecord = new JobPart_Record(this.connectionString, Param.Param_JobPartId);
						break;

					case Bob.DataClasses.ConnectionType.SqlConnection:
						newRecord = new JobPart_Record(this.sqlConnection, Param.Param_JobPartId);
						break;

					case Bob.DataClasses.ConnectionType.SqlTransaction:
						newRecord = new JobPart_Record(this.sqlTransaction, Param.Param_JobPartId);
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

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.JobPart_Collection", "Add");
			}	
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="Col_JobPartId">[To be supplied.]</param>
		public void Remove(System.Data.SqlTypes.SqlInt32 Col_JobPartId) {

			bool alreadyOpened = false;

			Params.spD_JobPart Param = new Params.spD_JobPart(true);
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

			Param.Param_JobPartId = Col_JobPartId;

			SPs.spD_JobPart Sp = new SPs.spD_JobPart(true);

			Sp.Execute(ref Param);

			CloseConnection(Sp.Connection, alreadyOpened);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="record">[To be supplied.]</param>
		public void Remove(IBusinessComponentRecord record) {
		
			JobPart_Record recordToDelete = record as JobPart_Record;

			if (recordToDelete == null) {

				throw new ArgumentException("Invalid record type. Must be 'Bob.BusinessComponents.JobPart_Record'.", "record");
			}

			bool alreadyOpened = false;

			Params.spD_JobPart Param = new Params.spD_JobPart(true);
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

			Param.Param_JobPartId = recordToDelete.Col_JobPartId;

			SPs.spD_JobPart Sp = new SPs.spD_JobPart(true);

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

			Params.spS_JobPart_Display Param = new Params.spS_JobPart_Display(true);
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

			if (!this.col_JobId.IsNull) {

				Param.Param_JobId = this.col_JobId;
			}

			if (!this.col_JobPartTypeId.IsNull) {

				Param.Param_JobPartTypeId = this.col_JobPartTypeId;
			}

			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_JobPart_Display Sp = new SPs.spS_JobPart_Display(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				while (sqlDataReader.Read()) {

					JobPart_Record record = null;

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							record = new JobPart_Record(this.connectionString, sqlDataReader.GetSqlInt32(SPs.spS_JobPart_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							record = new JobPart_Record(this.sqlConnection, sqlDataReader.GetSqlInt32(SPs.spS_JobPart_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							record = new JobPart_Record(this.sqlTransaction, sqlDataReader.GetSqlInt32(SPs.spS_JobPart_Display.Resultset1.Fields.Column_ID1.ColumnIndex));
							break;
					}


					record.UpdateCommandTimeOut = this.updateCommandTimeOut;
					record.SelectCommandTimeOut = this.selectCommandTimeOut;

					if (sqlDataReader.GetFieldType(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

						record.displayName = sqlDataReader.GetString(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex);
					}
					else {

						record.displayName = sqlDataReader.GetValue(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
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
				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.JobPart_Collection", "Refresh");
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
		public JobPart_Record this[int index] {

			get {
				
				return((JobPart_Record)internalRecords[index]);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="col_JobPartId">[To be supplied.]</param>
		/// <returns>[To be supplied.]</returns>
		public JobPart_Record this[System.Data.SqlTypes.SqlInt32 col_JobPartId] {

			get {

				foreach(JobPart_Record record in internalRecords) {

					bool equality = true;

					equality = equality && (record.Col_JobPartId == col_JobPartId).Value;

					if (equality) return(record);
				}
				return(null);
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new JobPart_Collection MemberwiseClone() {

			JobPart_Collection newCollection = null;

			switch (this.lastKnownConnectionType) {

				case Bob.DataClasses.ConnectionType.ConnectionString:
					newCollection = new JobPart_Collection(this.connectionString);
					break;
					
				case Bob.DataClasses.ConnectionType.SqlConnection:
					newCollection = new JobPart_Collection(this.sqlConnection);
					break;

				case Bob.DataClasses.ConnectionType.SqlTransaction:
					newCollection = new JobPart_Collection(this.sqlTransaction);
					break;
			}
	
			newCollection.recordsAreLoaded = this.recordsAreLoaded;
			newCollection.parent = null;

			newCollection.internalRecords = new System.Collections.ArrayList();
			foreach (JobPart_Record record in this.internalRecords) {

				newCollection.internalRecords.Add(record);
			}

			return(newCollection);
		}
	}
}
