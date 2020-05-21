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
using System.Data.SqlTypes;
using Params = Bob.DataClasses.Parameters;
using SPs = Bob.DataClasses.StoredProcedures;

namespace Bob.BusinessComponents {

	/// <summary>
	/// This class is an abstract class that represents the [JobPart] table. With this class
	/// you can load or update a record from the database. If you need to add or delete a record,
	/// you must use the <see cref="Bob.BusinessComponents.JobPart_Collection"/> class to do so.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:40:04", SqlObjectDependancyName="JobPart", SqlObjectDependancyRevision=656)]
#endif
	public class JobPart_Record : IBusinessComponentRecord {

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

		internal bool recordWasLoadedFromDB = false;
		private bool recordIsLoaded = false;

		internal string connectionString = String.Empty;
		internal SqlConnection sqlConnection = null;
		internal SqlTransaction sqlTransaction = null;
		private Bob.DataClasses.ConnectionType lastKnownConnectionType = Bob.DataClasses.ConnectionType.None;
		private int updateCommandTimeOut = 30;
		private int selectCommandTimeOut = 30;

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
		
		/// <summary>
		/// Initializes a new instance of the JobPart_Record class. Use this contructor to add
		/// a new record. Then call the Add method of the Bob.BusinessComponents.JobPart_Collection class to actually
		/// add the record in the database.
		/// </summary>
		public JobPart_Record() {
		
			this.recordWasLoadedFromDB = false;
			this.recordIsLoaded = false;
		}
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		/// <param name="col_JobPartId">[To be supplied.]</param>
		public JobPart_Record(string connectionString, System.Data.SqlTypes.SqlInt32 col_JobPartId) {

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

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.connectionString = connectionString;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

			this.col_JobPartId = col_JobPartId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_JobPartId">[To be supplied.]</param>
		public JobPart_Record(SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 col_JobPartId) {

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

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

			this.col_JobPartId = col_JobPartId;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_JobPartId">[To be supplied.]</param>
		public JobPart_Record(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlInt32 col_JobPartId) {

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

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlTransaction;

			this.col_JobPartId = col_JobPartId;
		}

		internal System.Data.SqlTypes.SqlInt32 col_JobPartId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobPartId {
		
			get {
			
				return(this.col_JobPartId);
			}

			set {

				if (this.recordWasLoadedFromDB) {

					throw new System.Exception("You cannot affect this primary key since the record was loaded from the database.");
				}
				else {

					this.col_JobPartId = value;
				}
			}
		}
		
		internal bool col_JobIdWasSet = false;
		private bool col_JobIdWasUpdated = false;
		internal System.Data.SqlTypes.SqlInt32 col_JobId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobId {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_JobId);
			}
			set {
			
				this.col_JobIdWasUpdated = true;
				this.col_JobIdWasSet = true;
				this.col_JobId_Record = null;
				this.col_JobId = value;
			}
		}

		
		private Job_Record col_JobId_Record = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Job_Record Col_JobId_Job_Record {
		
			get {

				if (!recordIsLoaded) {

					Refresh();
				}

				if (this.col_JobId_Record == null && !this.col_JobId.IsNull) {

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							this.col_JobId_Record = new Job_Record(this.connectionString, this.col_JobId);
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							this.col_JobId_Record = new Job_Record(this.sqlConnection, this.col_JobId);
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							this.col_JobId_Record = new Job_Record(this.sqlTransaction, this.col_JobId);
							break;
					}
				}
				
				return(this.col_JobId_Record);
			}
		}

		internal bool col_DescriptionWasSet = false;
		private bool col_DescriptionWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Description = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Description {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Description);
			}
			set {
			
				this.col_DescriptionWasUpdated = true;
				this.col_DescriptionWasSet = true;
				this.col_Description = value;
			}
		}

		internal bool col_JobPartTypeIdWasSet = false;
		private bool col_JobPartTypeIdWasUpdated = false;
		internal System.Data.SqlTypes.SqlInt32 col_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_JobPartTypeId {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_JobPartTypeId);
			}
			set {
			
				this.col_JobPartTypeIdWasUpdated = true;
				this.col_JobPartTypeIdWasSet = true;
				this.col_JobPartTypeId_Record = null;
				this.col_JobPartTypeId = value;
			}
		}

		
		private JobPartType_Record col_JobPartTypeId_Record = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public JobPartType_Record Col_JobPartTypeId_JobPartType_Record {
		
			get {

				if (!recordIsLoaded) {

					Refresh();
				}

				if (this.col_JobPartTypeId_Record == null && !this.col_JobPartTypeId.IsNull) {

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							this.col_JobPartTypeId_Record = new JobPartType_Record(this.connectionString, this.col_JobPartTypeId);
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							this.col_JobPartTypeId_Record = new JobPartType_Record(this.sqlConnection, this.col_JobPartTypeId);
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							this.col_JobPartTypeId_Record = new JobPartType_Record(this.sqlTransaction, this.col_JobPartTypeId);
							break;
					}
				}
				
				return(this.col_JobPartTypeId_Record);
			}
		}

		internal bool col_UnitsWasSet = false;
		private bool col_UnitsWasUpdated = false;
		internal System.Data.SqlTypes.SqlDecimal col_Units = System.Data.SqlTypes.SqlDecimal.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlDecimal Col_Units {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Units);
			}
			set {
			
				this.col_UnitsWasUpdated = true;
				this.col_UnitsWasSet = true;
				this.col_Units = value;
			}
		}

		internal bool col_PricePerUnitWasSet = false;
		private bool col_PricePerUnitWasUpdated = false;
		internal System.Data.SqlTypes.SqlMoney col_PricePerUnit = System.Data.SqlTypes.SqlMoney.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlMoney Col_PricePerUnit {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_PricePerUnit);
			}
			set {
			
				this.col_PricePerUnitWasUpdated = true;
				this.col_PricePerUnitWasSet = true;
				this.col_PricePerUnit = value;
			}
		}

		internal bool col_TotalPriceWasSet = false;
		private bool col_TotalPriceWasUpdated = false;
		internal System.Data.SqlTypes.SqlMoney col_TotalPrice = System.Data.SqlTypes.SqlMoney.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlMoney Col_TotalPrice {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_TotalPrice);
			}
			set {
			
				this.col_TotalPriceWasUpdated = true;
				this.col_TotalPriceWasSet = true;
				this.col_TotalPrice = value;
			}
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public bool Refresh() {

			this.displayName = null;

			this.col_JobIdWasUpdated = false;
			this.col_JobIdWasSet = false;
			this.col_JobId = System.Data.SqlTypes.SqlInt32.Null;

			this.col_DescriptionWasUpdated = false;
			this.col_DescriptionWasSet = false;
			this.col_Description = System.Data.SqlTypes.SqlString.Null;

			this.col_JobPartTypeIdWasUpdated = false;
			this.col_JobPartTypeIdWasSet = false;
			this.col_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;

			this.col_UnitsWasUpdated = false;
			this.col_UnitsWasSet = false;
			this.col_Units = System.Data.SqlTypes.SqlDecimal.Null;

			this.col_PricePerUnitWasUpdated = false;
			this.col_PricePerUnitWasSet = false;
			this.col_PricePerUnit = System.Data.SqlTypes.SqlMoney.Null;

			this.col_TotalPriceWasUpdated = false;
			this.col_TotalPriceWasSet = false;
			this.col_TotalPrice = System.Data.SqlTypes.SqlMoney.Null;

			bool alreadyOpened = false;

			Params.spS_JobPart Param = new Params.spS_JobPart(true);
			Param.CommandTimeOut = this.selectCommandTimeOut;
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

			if (!this.col_JobPartId.IsNull) {

				Param.Param_JobPartId = this.col_JobPartId;
			}


			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_JobPart Sp = new SPs.spS_JobPart(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				if (sqlDataReader.Read()) {

					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_JobId.ColumnIndex)) {

						this.col_JobId = sqlDataReader.GetSqlInt32(SPs.spS_JobPart.Resultset1.Fields.Column_JobId.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_Description.ColumnIndex)) {

						this.col_Description = sqlDataReader.GetSqlString(SPs.spS_JobPart.Resultset1.Fields.Column_Description.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_JobPartTypeId.ColumnIndex)) {

						this.col_JobPartTypeId = sqlDataReader.GetSqlInt32(SPs.spS_JobPart.Resultset1.Fields.Column_JobPartTypeId.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_Units.ColumnIndex)) {

						this.col_Units = sqlDataReader.GetSqlDecimal(SPs.spS_JobPart.Resultset1.Fields.Column_Units.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_PricePerUnit.ColumnIndex)) {

						this.col_PricePerUnit = sqlDataReader.GetSqlMoney(SPs.spS_JobPart.Resultset1.Fields.Column_PricePerUnit.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_JobPart.Resultset1.Fields.Column_TotalPrice.ColumnIndex)) {

						this.col_TotalPrice = sqlDataReader.GetSqlMoney(SPs.spS_JobPart.Resultset1.Fields.Column_TotalPrice.ColumnIndex);
					}

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					CloseConnection(Sp.Connection, alreadyOpened);

					this.recordIsLoaded = true;

					return(true);
				}
				else {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					CloseConnection(Sp.Connection, alreadyOpened);

					this.recordIsLoaded = false;

					return(false);
				}
			}
			else {

				if (sqlDataReader != null && !sqlDataReader.IsClosed) {

					sqlDataReader.Close();
				}

				CloseConnection(Sp.Connection, alreadyOpened);

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.JobPart_Record", "Refresh");
			}
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public void Update() {

			if (!this.recordWasLoadedFromDB) {

				throw new ArgumentException("No record was loaded from the database. No update is possible.");
			}

			bool ChangesHaveBeenMade = false;

			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_JobIdWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_DescriptionWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_JobPartTypeIdWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_UnitsWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_PricePerUnitWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_TotalPriceWasUpdated);

			if (!ChangesHaveBeenMade) {

				return;
			}

			bool alreadyOpened = false;

			Params.spU_JobPart Param = new Params.spU_JobPart(true);
			Param.CommandTimeOut = this.updateCommandTimeOut;
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

			Param.Param_JobPartId = this.col_JobPartId;

			if (this.col_JobIdWasUpdated) {

				Param.Param_JobId = this.col_JobId;
				Param.Param_ConsiderNull_JobId = true;
			}
			else {

				Param.Param_JobId = SqlInt32.Null;
				Param.Param_ConsiderNull_JobId = false;
			}

			if (this.col_DescriptionWasUpdated) {

				Param.Param_Description = this.col_Description;
				Param.Param_ConsiderNull_Description = true;
			}
			else {

				Param.Param_Description = SqlString.Null;
				Param.Param_ConsiderNull_Description = false;
			}

			if (this.col_JobPartTypeIdWasUpdated) {

				Param.Param_JobPartTypeId = this.col_JobPartTypeId;
				Param.Param_ConsiderNull_JobPartTypeId = true;
			}
			else {

				Param.Param_JobPartTypeId = SqlInt32.Null;
				Param.Param_ConsiderNull_JobPartTypeId = false;
			}

			if (this.col_UnitsWasUpdated) {

				Param.Param_Units = this.col_Units;
				Param.Param_ConsiderNull_Units = true;
			}
			else {

				Param.Param_Units = SqlDecimal.Null;
				Param.Param_ConsiderNull_Units = false;
			}

			if (this.col_PricePerUnitWasUpdated) {

				Param.Param_PricePerUnit = this.col_PricePerUnit;
				Param.Param_ConsiderNull_PricePerUnit = true;
			}
			else {

				Param.Param_PricePerUnit = SqlMoney.Null;
				Param.Param_ConsiderNull_PricePerUnit = false;
			}

			if (this.col_TotalPriceWasUpdated) {

				Param.Param_TotalPrice = this.col_TotalPrice;
				Param.Param_ConsiderNull_TotalPrice = true;
			}
			else {

				Param.Param_TotalPrice = SqlMoney.Null;
				Param.Param_ConsiderNull_TotalPrice = false;
			}

			SPs.spU_JobPart Sp = new SPs.spU_JobPart(false);
			if (Sp.Execute(ref Param)) {

				this.col_JobIdWasUpdated = false;
				this.col_DescriptionWasUpdated = false;
				this.col_JobPartTypeIdWasUpdated = false;
				this.col_UnitsWasUpdated = false;
				this.col_PricePerUnitWasUpdated = false;
				this.col_TotalPriceWasUpdated = false;
			}
			else {

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.JobPart_Record", "Update");
			}
			CloseConnection(Sp.Connection, alreadyOpened);

		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public string ObjectDisplay {

			get {

				return(this.ToString());
			}
		}

		internal string displayName = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public override string ToString() {

			if (!this.recordWasLoadedFromDB) {

				throw new ArgumentException("No record was loaded from the database. The DisplayName is not available.");
			}
		
			if (this.displayName == null) {

				bool alreadyOpened = false;

				Params.spS_JobPart_Display Param = new Params.spS_JobPart_Display(true);

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

				Param.Param_JobPartId = this.col_JobPartId;

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				SPs.spS_JobPart_Display Sp = new SPs.spS_JobPart_Display(false);
				if (Sp.Execute(ref Param, out sqlDataReader)) {

					if (sqlDataReader.Read()) {

						if (!sqlDataReader.IsDBNull(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex)) {

							if (sqlDataReader.GetFieldType(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

								this.displayName = sqlDataReader.GetString(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex);
							}
							else {

								this.displayName = sqlDataReader.GetValue(SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
							}
						}
					}

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}
					
					CloseConnection(Sp.Connection, alreadyOpened);
				}
				else {

					if (sqlDataReader != null && !sqlDataReader.IsClosed) {

						sqlDataReader.Close();
					}

					CloseConnection(Sp.Connection, alreadyOpened);

					throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.JobPart_Record", "ToString");
				}				
			}
			
			return(this.displayName);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new JobPart_Record MemberwiseClone() {
		
			JobPart_Record newRecord = new JobPart_Record();
			
			newRecord.recordWasLoadedFromDB = this.recordWasLoadedFromDB;
			newRecord.recordIsLoaded = this.recordIsLoaded;
			newRecord.connectionString = this.connectionString;

			newRecord.col_JobPartId = this.col_JobPartId;


			newRecord.col_JobIdWasSet = this.col_JobIdWasSet;
			newRecord.col_JobIdWasUpdated = this.col_JobIdWasUpdated;
			newRecord.col_JobId = this.col_JobId;

			newRecord.col_JobId_Record = this.col_JobId_Record.MemberwiseClone();

			newRecord.col_DescriptionWasSet = this.col_DescriptionWasSet;
			newRecord.col_DescriptionWasUpdated = this.col_DescriptionWasUpdated;
			newRecord.col_Description = this.col_Description;

			newRecord.col_JobPartTypeIdWasSet = this.col_JobPartTypeIdWasSet;
			newRecord.col_JobPartTypeIdWasUpdated = this.col_JobPartTypeIdWasUpdated;
			newRecord.col_JobPartTypeId = this.col_JobPartTypeId;

			newRecord.col_JobPartTypeId_Record = this.col_JobPartTypeId_Record.MemberwiseClone();

			newRecord.col_UnitsWasSet = this.col_UnitsWasSet;
			newRecord.col_UnitsWasUpdated = this.col_UnitsWasUpdated;
			newRecord.col_Units = this.col_Units;

			newRecord.col_PricePerUnitWasSet = this.col_PricePerUnitWasSet;
			newRecord.col_PricePerUnitWasUpdated = this.col_PricePerUnitWasUpdated;
			newRecord.col_PricePerUnit = this.col_PricePerUnit;

			newRecord.col_TotalPriceWasSet = this.col_TotalPriceWasSet;
			newRecord.col_TotalPriceWasUpdated = this.col_TotalPriceWasUpdated;
			newRecord.col_TotalPrice = this.col_TotalPrice;

			newRecord.displayName = this.displayName;
			
			return(newRecord);
		}
	}
}
