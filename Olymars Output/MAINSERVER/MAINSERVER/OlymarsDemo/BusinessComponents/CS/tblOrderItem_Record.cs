/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:52:11
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:52:27
			Template revision: 1247

			SQL Server version: 08.00.0760
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

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
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.BusinessComponents {

	/// <summary>
	/// This class is an abstract class that represents the [tblOrderItem] table. With this class
	/// you can load or update a record from the database. If you need to add or delete a record,
	/// you must use the <see cref="OlymarsDemo.BusinessComponents.tblOrderItem_Collection"/> class to do so.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/12/28 11:52:11", SqlObjectDependancyName="tblOrderItem", SqlObjectDependancyRevision=896)]
#endif
	public class tblOrderItem_Record : IBusinessComponentRecord {

		private void CloseConnection(SqlConnection connection, bool alreadyOpened) {

			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					if (connection != null && connection.State == System.Data.ConnectionState.Open) {

						connection.Close();
						connection.Dispose();
					}
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					if (!alreadyOpened && this.sqlConnection.State == System.Data.ConnectionState.Open) this.sqlConnection.Close();
					break;
			}
		}

		internal bool recordWasLoadedFromDB = false;
		private bool recordIsLoaded = false;

		internal string connectionString = String.Empty;
		internal SqlConnection sqlConnection = null;
		internal SqlTransaction sqlTransaction = null;
		private OlymarsDemo.DataClasses.ConnectionType lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
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
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

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
		/// Initializes a new instance of the tblOrderItem_Record class. Use this contructor to add
		/// a new record. Then call the Add method of the OlymarsDemo.BusinessComponents.tblOrderItem_Collection class to actually
		/// add the record in the database.
		/// </summary>
		public tblOrderItem_Record() {
		
			this.recordWasLoadedFromDB = false;
			this.recordIsLoaded = false;
		}
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		/// <param name="col_Oit_GuidID">[To be supplied.]</param>
		public tblOrderItem_Record(string connectionString, System.Data.SqlTypes.SqlGuid col_Oit_GuidID) {

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString == System.String.Empty) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString != String.Empty) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrderItem'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "tblOrderItem", CurrentRevision, OriginalRevision));
					}
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.connectionString = connectionString;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;

			this.col_Oit_GuidID = col_Oit_GuidID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_Oit_GuidID">[To be supplied.]</param>
		public tblOrderItem_Record(SqlConnection sqlConnection, System.Data.SqlTypes.SqlGuid col_Oit_GuidID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrderItem'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrderItem", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;

			this.col_Oit_GuidID = col_Oit_GuidID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_Oit_GuidID">[To be supplied.]</param>
		public tblOrderItem_Record(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlGuid col_Oit_GuidID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrderItem'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrderItem", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlTransaction;

			this.col_Oit_GuidID = col_Oit_GuidID;
		}

		internal System.Data.SqlTypes.SqlGuid col_Oit_GuidID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Oit_GuidID {
		
			get {
			
				return(this.col_Oit_GuidID);
			}

			set {

				if (this.recordWasLoadedFromDB) {

					throw new System.Exception("You cannot affect this primary key since the record was loaded from the database.");
				}
				else {

					this.col_Oit_GuidID = value;
				}
			}
		}
		
		internal bool col_Oit_GuidOrderIDWasSet = false;
		private bool col_Oit_GuidOrderIDWasUpdated = false;
		internal System.Data.SqlTypes.SqlGuid col_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Oit_GuidOrderID {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Oit_GuidOrderID);
			}
			set {
			
				this.col_Oit_GuidOrderIDWasUpdated = true;
				this.col_Oit_GuidOrderIDWasSet = true;
				this.col_Oit_GuidOrderID_Record = null;
				this.col_Oit_GuidOrderID = value;
			}
		}

		
		private tblOrder_Record col_Oit_GuidOrderID_Record = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblOrder_Record Col_Oit_GuidOrderID_tblOrder_Record {
		
			get {

				if (!recordIsLoaded) {

					Refresh();
				}

				if (this.col_Oit_GuidOrderID_Record == null && !this.col_Oit_GuidOrderID.IsNull) {

					switch (this.lastKnownConnectionType) {

						case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.col_Oit_GuidOrderID_Record = new tblOrder_Record(this.connectionString, this.col_Oit_GuidOrderID);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.col_Oit_GuidOrderID_Record = new tblOrder_Record(this.sqlConnection, this.col_Oit_GuidOrderID);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
							this.col_Oit_GuidOrderID_Record = new tblOrder_Record(this.sqlTransaction, this.col_Oit_GuidOrderID);
							break;
					}
				}
				
				return(this.col_Oit_GuidOrderID_Record);
			}
		}

		internal bool col_Oit_GuidProductIDWasSet = false;
		private bool col_Oit_GuidProductIDWasUpdated = false;
		internal System.Data.SqlTypes.SqlGuid col_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Oit_GuidProductID {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Oit_GuidProductID);
			}
			set {
			
				this.col_Oit_GuidProductIDWasUpdated = true;
				this.col_Oit_GuidProductIDWasSet = true;
				this.col_Oit_GuidProductID_Record = null;
				this.col_Oit_GuidProductID = value;
			}
		}

		
		private Product col_Oit_GuidProductID_Record = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Product Col_Oit_GuidProductID_tblProduct_Record {
		
			get {

				if (!recordIsLoaded) {

					Refresh();
				}

				if (this.col_Oit_GuidProductID_Record == null && !this.col_Oit_GuidProductID.IsNull) {

					switch (this.lastKnownConnectionType) {

						case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.col_Oit_GuidProductID_Record = new Product(this.connectionString, this.col_Oit_GuidProductID);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.col_Oit_GuidProductID_Record = new Product(this.sqlConnection, this.col_Oit_GuidProductID);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
							this.col_Oit_GuidProductID_Record = new Product(this.sqlTransaction, this.col_Oit_GuidProductID);
							break;
					}
				}
				
				return(this.col_Oit_GuidProductID_Record);
			}
		}

		internal bool col_Oit_LngAmountWasSet = false;
		private bool col_Oit_LngAmountWasUpdated = false;
		internal System.Data.SqlTypes.SqlInt32 col_Oit_LngAmount = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Col_Oit_LngAmount {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Oit_LngAmount);
			}
			set {
			
				this.col_Oit_LngAmountWasUpdated = true;
				this.col_Oit_LngAmountWasSet = true;
				this.col_Oit_LngAmount = value;
			}
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public bool Refresh() {

			this.displayName = null;

			this.col_Oit_GuidOrderIDWasUpdated = false;
			this.col_Oit_GuidOrderIDWasSet = false;
			this.col_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;

			this.col_Oit_GuidProductIDWasUpdated = false;
			this.col_Oit_GuidProductIDWasSet = false;
			this.col_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;

			this.col_Oit_LngAmountWasUpdated = false;
			this.col_Oit_LngAmountWasSet = false;
			this.col_Oit_LngAmount = System.Data.SqlTypes.SqlInt32.Null;

			bool alreadyOpened = false;

			Params.spS_tblOrderItem Param = new Params.spS_tblOrderItem(true);
			Param.CommandTimeOut = this.selectCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			if (!this.col_Oit_GuidID.IsNull) {

				Param.Param_Oit_GuidID = this.col_Oit_GuidID;
			}


			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_tblOrderItem Sp = new SPs.spS_tblOrderItem(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				if (sqlDataReader.Read()) {

					if (!sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidOrderID.ColumnIndex)) {

						this.col_Oit_GuidOrderID = sqlDataReader.GetSqlGuid(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidOrderID.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidProductID.ColumnIndex)) {

						this.col_Oit_GuidProductID = sqlDataReader.GetSqlGuid(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_GuidProductID.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_LngAmount.ColumnIndex)) {

						this.col_Oit_LngAmount = sqlDataReader.GetSqlInt32(SPs.spS_tblOrderItem.Resultset1.Fields.Column_Oit_LngAmount.ColumnIndex);
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

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblOrderItem_Record", "Refresh");
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

			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_Oit_GuidOrderIDWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_Oit_GuidProductIDWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_Oit_LngAmountWasUpdated);

			if (!ChangesHaveBeenMade) {

				return;
			}

			bool alreadyOpened = false;

			Params.spU_tblOrderItem Param = new Params.spU_tblOrderItem(true);
			Param.CommandTimeOut = this.updateCommandTimeOut;
			switch (this.lastKnownConnectionType) {

				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					Param.SetUpConnection(this.connectionString);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					Param.SetUpConnection(this.sqlConnection);
					alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
					break;

				case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
					Param.SetUpConnection(this.sqlTransaction);
					break;
			}

			Param.Param_Oit_GuidID = this.col_Oit_GuidID;

			if (this.col_Oit_GuidOrderIDWasUpdated) {

				Param.Param_Oit_GuidOrderID = this.col_Oit_GuidOrderID;
				Param.Param_ConsiderNull_Oit_GuidOrderID = true;
			}
			else {

				Param.Param_Oit_GuidOrderID = SqlGuid.Null;
				Param.Param_ConsiderNull_Oit_GuidOrderID = false;
			}

			if (this.col_Oit_GuidProductIDWasUpdated) {

				Param.Param_Oit_GuidProductID = this.col_Oit_GuidProductID;
				Param.Param_ConsiderNull_Oit_GuidProductID = true;
			}
			else {

				Param.Param_Oit_GuidProductID = SqlGuid.Null;
				Param.Param_ConsiderNull_Oit_GuidProductID = false;
			}

			if (this.col_Oit_LngAmountWasUpdated) {

				Param.Param_Oit_LngAmount = this.col_Oit_LngAmount;
				Param.Param_ConsiderNull_Oit_LngAmount = true;
			}
			else {

				Param.Param_Oit_LngAmount = SqlInt32.Null;
				Param.Param_ConsiderNull_Oit_LngAmount = false;
			}

			SPs.spU_tblOrderItem Sp = new SPs.spU_tblOrderItem(false);
			if (Sp.Execute(ref Param)) {

				this.col_Oit_GuidOrderIDWasUpdated = false;
				this.col_Oit_GuidProductIDWasUpdated = false;
				this.col_Oit_LngAmountWasUpdated = false;
			}
			else {

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblOrderItem_Record", "Update");
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

				Params.spS_tblOrderItem_Display Param = new Params.spS_tblOrderItem_Display(true);

				switch (this.lastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
						Param.SetUpConnection(this.connectionString);
						break;
					
					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
						Param.SetUpConnection(this.sqlConnection);
						alreadyOpened = (this.sqlConnection.State == System.Data.ConnectionState.Open);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
						Param.SetUpConnection(this.sqlTransaction);
						break;
				}

				Param.Param_Oit_GuidID = this.col_Oit_GuidID;

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				SPs.spS_tblOrderItem_Display Sp = new SPs.spS_tblOrderItem_Display(false);
				if (Sp.Execute(ref Param, out sqlDataReader)) {

					if (sqlDataReader.Read()) {

						if (!sqlDataReader.IsDBNull(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnIndex)) {

							if (sqlDataReader.GetFieldType(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

								this.displayName = sqlDataReader.GetString(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnIndex);
							}
							else {

								this.displayName = sqlDataReader.GetValue(SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.tblOrderItem_Record", "ToString");
				}				
			}
			
			return(this.displayName);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new tblOrderItem_Record MemberwiseClone() {
		
			tblOrderItem_Record newRecord = new tblOrderItem_Record();
			
			newRecord.recordWasLoadedFromDB = this.recordWasLoadedFromDB;
			newRecord.recordIsLoaded = this.recordIsLoaded;
			newRecord.connectionString = this.connectionString;

			newRecord.col_Oit_GuidID = this.col_Oit_GuidID;


			newRecord.col_Oit_GuidOrderIDWasSet = this.col_Oit_GuidOrderIDWasSet;
			newRecord.col_Oit_GuidOrderIDWasUpdated = this.col_Oit_GuidOrderIDWasUpdated;
			newRecord.col_Oit_GuidOrderID = this.col_Oit_GuidOrderID;

			newRecord.col_Oit_GuidOrderID_Record = this.col_Oit_GuidOrderID_Record.MemberwiseClone();

			newRecord.col_Oit_GuidProductIDWasSet = this.col_Oit_GuidProductIDWasSet;
			newRecord.col_Oit_GuidProductIDWasUpdated = this.col_Oit_GuidProductIDWasUpdated;
			newRecord.col_Oit_GuidProductID = this.col_Oit_GuidProductID;

			newRecord.col_Oit_GuidProductID_Record = this.col_Oit_GuidProductID_Record.MemberwiseClone();

			newRecord.col_Oit_LngAmountWasSet = this.col_Oit_LngAmountWasSet;
			newRecord.col_Oit_LngAmountWasUpdated = this.col_Oit_LngAmountWasUpdated;
			newRecord.col_Oit_LngAmount = this.col_Oit_LngAmount;

			newRecord.displayName = this.displayName;
			
			return(newRecord);
		}
	}
}
