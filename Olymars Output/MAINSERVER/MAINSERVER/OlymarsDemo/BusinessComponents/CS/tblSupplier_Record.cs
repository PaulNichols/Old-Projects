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
	/// This class is an abstract class that represents the [tblSupplier] table. With this class
	/// you can load or update a record from the database. If you need to add or delete a record,
	/// you must use the <see cref="OlymarsDemo.BusinessComponents.Suppliers"/> class to do so.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/12/28 11:52:11", SqlObjectDependancyName="tblSupplier", SqlObjectDependancyRevision=496)]
#endif
	public class Supplier : IBusinessComponentRecord {

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
		/// Initializes a new instance of the Supplier class. Use this contructor to add
		/// a new record. Then call the Add method of the OlymarsDemo.BusinessComponents.Suppliers class to actually
		/// add the record in the database.
		/// </summary>
		public Supplier() {
		
			this.recordWasLoadedFromDB = false;
			this.recordIsLoaded = false;
		}
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		/// <param name="col_Sup_GuidID">[To be supplied.]</param>
		public Supplier(string connectionString, System.Data.SqlTypes.SqlGuid col_Sup_GuidID) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplier'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "tblSupplier", CurrentRevision, OriginalRevision));
					}
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.connectionString = connectionString;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;

			this.col_Sup_GuidID = col_Sup_GuidID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_Sup_GuidID">[To be supplied.]</param>
		public Supplier(SqlConnection sqlConnection, System.Data.SqlTypes.SqlGuid col_Sup_GuidID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplier'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblSupplier", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;

			this.col_Sup_GuidID = col_Sup_GuidID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_Sup_GuidID">[To be supplied.]</param>
		public Supplier(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlGuid col_Sup_GuidID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblSupplier'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblSupplier", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlTransaction;

			this.col_Sup_GuidID = col_Sup_GuidID;
		}

		internal System.Data.SqlTypes.SqlGuid col_Sup_GuidID = System.Data.SqlTypes.SqlGuid.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlGuid Col_Sup_GuidID {
		
			get {
			
				return(this.col_Sup_GuidID);
			}

			set {

				if (this.recordWasLoadedFromDB) {

					throw new System.Exception("You cannot affect this primary key since the record was loaded from the database.");
				}
				else {

					this.col_Sup_GuidID = value;
				}
			}
		}
		
		internal bool col_Sup_StrNameWasSet = false;
		private bool col_Sup_StrNameWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Sup_StrName = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Col_Sup_StrName {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Sup_StrName);
			}
			set {
			
				this.col_Sup_StrNameWasUpdated = true;
				this.col_Sup_StrNameWasSet = true;
				this.col_Sup_StrName = value;
			}
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public bool Refresh() {

			this.displayName = null;

			this.col_Sup_StrNameWasUpdated = false;
			this.col_Sup_StrNameWasSet = false;
			this.col_Sup_StrName = System.Data.SqlTypes.SqlString.Null;

			bool alreadyOpened = false;

			Params.spS_tblSupplier Param = new Params.spS_tblSupplier(true);
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

			if (!this.col_Sup_GuidID.IsNull) {

				Param.Param_Sup_GuidID = this.col_Sup_GuidID;
			}


			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_tblSupplier Sp = new SPs.spS_tblSupplier(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				if (sqlDataReader.Read()) {

					if (!sqlDataReader.IsDBNull(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_StrName.ColumnIndex)) {

						this.col_Sup_StrName = sqlDataReader.GetSqlString(SPs.spS_tblSupplier.Resultset1.Fields.Column_Sup_StrName.ColumnIndex);
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

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.Supplier", "Refresh");
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

			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_Sup_StrNameWasUpdated);

			if (!ChangesHaveBeenMade) {

				return;
			}

			bool alreadyOpened = false;

			Params.spU_tblSupplier Param = new Params.spU_tblSupplier(true);
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

			Param.Param_Sup_GuidID = this.col_Sup_GuidID;

			if (this.col_Sup_StrNameWasUpdated) {

				Param.Param_Sup_StrName = this.col_Sup_StrName;
				Param.Param_ConsiderNull_Sup_StrName = true;
			}
			else {

				Param.Param_Sup_StrName = SqlString.Null;
				Param.Param_ConsiderNull_Sup_StrName = false;
			}

			SPs.spU_tblSupplier Sp = new SPs.spU_tblSupplier(false);
			if (Sp.Execute(ref Param)) {

				this.col_Sup_StrNameWasUpdated = false;
			}
			else {

				throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.Supplier", "Update");
			}
			CloseConnection(Sp.Connection, alreadyOpened);

		}


		private tblSupplierProduct_Collection internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public tblSupplierProduct_Collection tblSupplierProduct_Col_Spr_GuidSupplierID_Collection {

			get {

				if (this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection == null) {

					switch (this.lastKnownConnectionType) {

						case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection = new tblSupplierProduct_Collection(this.connectionString);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection = new tblSupplierProduct_Collection(this.sqlConnection);
							break;

						case OlymarsDemo.DataClasses.ConnectionType.SqlTransaction:
							this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection = new tblSupplierProduct_Collection(this.sqlTransaction);
							break;

					}
					this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection.LoadFrom_Spr_GuidSupplierID(this.col_Sup_GuidID, this);
				}

				return(this.internal_tblSupplierProduct_Col_Spr_GuidSupplierID_Collection);
			}
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

				Params.spS_tblSupplier_Display Param = new Params.spS_tblSupplier_Display(true);

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

				Param.Param_Sup_GuidID = this.col_Sup_GuidID;

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				SPs.spS_tblSupplier_Display Sp = new SPs.spS_tblSupplier_Display(false);
				if (Sp.Execute(ref Param, out sqlDataReader)) {

					if (sqlDataReader.Read()) {

						if (!sqlDataReader.IsDBNull(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_Display.ColumnIndex)) {

							if (sqlDataReader.GetFieldType(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

								this.displayName = sqlDataReader.GetString(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_Display.ColumnIndex);
							}
							else {

								this.displayName = sqlDataReader.GetValue(SPs.spS_tblSupplier_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
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

					throw new OlymarsDemo.DataClasses.CustomException(Param, "OlymarsDemo.BusinessComponents.Supplier", "ToString");
				}				
			}
			
			return(this.displayName);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new Supplier MemberwiseClone() {
		
			Supplier newRecord = new Supplier();
			
			newRecord.recordWasLoadedFromDB = this.recordWasLoadedFromDB;
			newRecord.recordIsLoaded = this.recordIsLoaded;
			newRecord.connectionString = this.connectionString;

			newRecord.col_Sup_GuidID = this.col_Sup_GuidID;


			newRecord.col_Sup_StrNameWasSet = this.col_Sup_StrNameWasSet;
			newRecord.col_Sup_StrNameWasUpdated = this.col_Sup_StrNameWasUpdated;
			newRecord.col_Sup_StrName = this.col_Sup_StrName;

			newRecord.displayName = this.displayName;
			
			return(newRecord);
		}
	}
}
