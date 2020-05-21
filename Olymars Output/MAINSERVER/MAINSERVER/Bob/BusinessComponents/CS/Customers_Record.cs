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
	/// This class is an abstract class that represents the [Customers] table. With this class
	/// you can load or update a record from the database. If you need to add or delete a record,
	/// you must use the <see cref="Bob.BusinessComponents.Customers"/> class to do so.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:40:04", SqlObjectDependancyName="Customers", SqlObjectDependancyRevision=960)]
#endif
	public class Customer : IBusinessComponentRecord {

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
		/// Initializes a new instance of the Customer class. Use this contructor to add
		/// a new record. Then call the Add method of the Bob.BusinessComponents.Customers class to actually
		/// add the record in the database.
		/// </summary>
		public Customer() {
		
			this.recordWasLoadedFromDB = false;
			this.recordIsLoaded = false;
		}
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="connectionString">Connection string to be used when accessing the database.</param>
		/// <param name="col_CustomerID">[To be supplied.]</param>
		public Customer(string connectionString, System.Data.SqlTypes.SqlInt32 col_CustomerID) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}", "Customers", CurrentRevision, OriginalRevision));
					}
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.connectionString = connectionString;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

			this.col_CustomerID = col_CustomerID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="col_CustomerID">[To be supplied.]</param>
		public Customer(SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 col_CustomerID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

			this.col_CustomerID = col_CustomerID;
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		/// <param name="col_CustomerID">[To be supplied.]</param>
		public Customer(SqlTransaction sqlTransaction, System.Data.SqlTypes.SqlInt32 col_CustomerID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'Customers'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.recordWasLoadedFromDB = true;
			this.recordIsLoaded = false;

			this.sqlTransaction = sqlTransaction;
			this.lastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlTransaction;

			this.col_CustomerID = col_CustomerID;
		}

		internal System.Data.SqlTypes.SqlInt32 col_CustomerID = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 CustomerID {
		
			get {
			
				return(this.col_CustomerID);
			}

			set {

				if (this.recordWasLoadedFromDB) {

					throw new System.Exception("You cannot affect this primary key since the record was loaded from the database.");
				}
				else {

					this.col_CustomerID = value;
				}
			}
		}
		
		internal bool col_CompanyNameWasSet = false;
		private bool col_CompanyNameWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_CompanyName = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString CompanyName {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_CompanyName);
			}
			set {
			
				this.col_CompanyNameWasUpdated = true;
				this.col_CompanyNameWasSet = true;
				this.col_CompanyName = value;
			}
		}

		internal bool col_ContactNameWasSet = false;
		private bool col_ContactNameWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_ContactName = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString ContactName {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_ContactName);
			}
			set {
			
				this.col_ContactNameWasUpdated = true;
				this.col_ContactNameWasSet = true;
				this.col_ContactName = value;
			}
		}

		internal bool col_TitleIdWasSet = false;
		private bool col_TitleIdWasUpdated = false;
		internal System.Data.SqlTypes.SqlInt32 col_TitleId = System.Data.SqlTypes.SqlInt32.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 TitleId {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_TitleId);
			}
			set {
			
				this.col_TitleIdWasUpdated = true;
				this.col_TitleIdWasSet = true;
				this.col_TitleId_Record = null;
				this.col_TitleId = value;
			}
		}

		
		private Title col_TitleId_Record = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Title Title {
		
			get {

				if (!recordIsLoaded) {

					Refresh();
				}

				if (this.col_TitleId_Record == null && !this.col_TitleId.IsNull) {

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							this.col_TitleId_Record = new Title(this.connectionString, this.col_TitleId);
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							this.col_TitleId_Record = new Title(this.sqlConnection, this.col_TitleId);
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							this.col_TitleId_Record = new Title(this.sqlTransaction, this.col_TitleId);
							break;
					}
				}
				
				return(this.col_TitleId_Record);
			}
		}

		internal bool col_AddressWasSet = false;
		private bool col_AddressWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Address = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Address {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Address);
			}
			set {
			
				this.col_AddressWasUpdated = true;
				this.col_AddressWasSet = true;
				this.col_Address = value;
			}
		}

		internal bool col_CityWasSet = false;
		private bool col_CityWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_City = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString City {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_City);
			}
			set {
			
				this.col_CityWasUpdated = true;
				this.col_CityWasSet = true;
				this.col_City = value;
			}
		}

		internal bool col_PostalCodeWasSet = false;
		private bool col_PostalCodeWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_PostalCode = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString PostalCode {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_PostalCode);
			}
			set {
			
				this.col_PostalCodeWasUpdated = true;
				this.col_PostalCodeWasSet = true;
				this.col_PostalCode = value;
			}
		}

		internal bool col_PhoneWasSet = false;
		private bool col_PhoneWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Phone = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Phone {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Phone);
			}
			set {
			
				this.col_PhoneWasUpdated = true;
				this.col_PhoneWasSet = true;
				this.col_Phone = value;
			}
		}

		internal bool col_EmailWasSet = false;
		private bool col_EmailWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Email = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Email {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Email);
			}
			set {
			
				this.col_EmailWasUpdated = true;
				this.col_EmailWasSet = true;
				this.col_Email = value;
			}
		}

		internal bool col_WebAddressWasSet = false;
		private bool col_WebAddressWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_WebAddress = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString WebAddress {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_WebAddress);
			}
			set {
			
				this.col_WebAddressWasUpdated = true;
				this.col_WebAddressWasSet = true;
				this.col_WebAddress = value;
			}
		}

		internal bool col_FaxWasSet = false;
		private bool col_FaxWasUpdated = false;
		internal System.Data.SqlTypes.SqlString col_Fax = System.Data.SqlTypes.SqlString.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlString Fax {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Fax);
			}
			set {
			
				this.col_FaxWasUpdated = true;
				this.col_FaxWasSet = true;
				this.col_Fax = value;
			}
		}

		internal bool col_ActiveWasSet = false;
		private bool col_ActiveWasUpdated = false;
		internal System.Data.SqlTypes.SqlBoolean col_Active = System.Data.SqlTypes.SqlBoolean.Null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public System.Data.SqlTypes.SqlBoolean Active {
		
			get {
			

				if (this.recordWasLoadedFromDB && !this.recordIsLoaded) {

					Refresh();
				}
				return(this.col_Active);
			}
			set {
			
				this.col_ActiveWasUpdated = true;
				this.col_ActiveWasSet = true;
				this.col_Active = value;
			}
		}


		/// <summary>
		/// [To be supplied.]
		/// </summary>
		/// <returns>[To be supplied.]</returns>
		public bool Refresh() {

			this.displayName = null;

			this.col_CompanyNameWasUpdated = false;
			this.col_CompanyNameWasSet = false;
			this.col_CompanyName = System.Data.SqlTypes.SqlString.Null;

			this.col_ContactNameWasUpdated = false;
			this.col_ContactNameWasSet = false;
			this.col_ContactName = System.Data.SqlTypes.SqlString.Null;

			this.col_TitleIdWasUpdated = false;
			this.col_TitleIdWasSet = false;
			this.col_TitleId = System.Data.SqlTypes.SqlInt32.Null;

			this.col_AddressWasUpdated = false;
			this.col_AddressWasSet = false;
			this.col_Address = System.Data.SqlTypes.SqlString.Null;

			this.col_CityWasUpdated = false;
			this.col_CityWasSet = false;
			this.col_City = System.Data.SqlTypes.SqlString.Null;

			this.col_PostalCodeWasUpdated = false;
			this.col_PostalCodeWasSet = false;
			this.col_PostalCode = System.Data.SqlTypes.SqlString.Null;

			this.col_PhoneWasUpdated = false;
			this.col_PhoneWasSet = false;
			this.col_Phone = System.Data.SqlTypes.SqlString.Null;

			this.col_EmailWasUpdated = false;
			this.col_EmailWasSet = false;
			this.col_Email = System.Data.SqlTypes.SqlString.Null;

			this.col_WebAddressWasUpdated = false;
			this.col_WebAddressWasSet = false;
			this.col_WebAddress = System.Data.SqlTypes.SqlString.Null;

			this.col_FaxWasUpdated = false;
			this.col_FaxWasSet = false;
			this.col_Fax = System.Data.SqlTypes.SqlString.Null;

			this.col_ActiveWasUpdated = false;
			this.col_ActiveWasSet = false;
			this.col_Active = System.Data.SqlTypes.SqlBoolean.Null;

			bool alreadyOpened = false;

			Params.spS_Customers Param = new Params.spS_Customers(true);
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

			if (!this.col_CustomerID.IsNull) {

				Param.Param_CustomerID = this.col_CustomerID;
			}


			System.Data.SqlClient.SqlDataReader sqlDataReader = null;
			SPs.spS_Customers Sp = new SPs.spS_Customers(false);
			if (Sp.Execute(ref Param, out sqlDataReader)) {

				if (sqlDataReader.Read()) {

					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_CompanyName.ColumnIndex)) {

						this.col_CompanyName = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_CompanyName.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_ContactName.ColumnIndex)) {

						this.col_ContactName = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_ContactName.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_TitleId.ColumnIndex)) {

						this.col_TitleId = sqlDataReader.GetSqlInt32(SPs.spS_Customers.Resultset1.Fields.Column_TitleId.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Address.ColumnIndex)) {

						this.col_Address = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Address.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_City.ColumnIndex)) {

						this.col_City = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_City.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_PostalCode.ColumnIndex)) {

						this.col_PostalCode = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_PostalCode.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Phone.ColumnIndex)) {

						this.col_Phone = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Phone.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Email.ColumnIndex)) {

						this.col_Email = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Email.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_WebAddress.ColumnIndex)) {

						this.col_WebAddress = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_WebAddress.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Fax.ColumnIndex)) {

						this.col_Fax = sqlDataReader.GetSqlString(SPs.spS_Customers.Resultset1.Fields.Column_Fax.ColumnIndex);
					}
					if (!sqlDataReader.IsDBNull(SPs.spS_Customers.Resultset1.Fields.Column_Active.ColumnIndex)) {

						this.col_Active = sqlDataReader.GetSqlBoolean(SPs.spS_Customers.Resultset1.Fields.Column_Active.ColumnIndex);
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

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.Customer", "Refresh");
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

			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_CompanyNameWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_ContactNameWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_TitleIdWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_AddressWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_CityWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_PostalCodeWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_PhoneWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_EmailWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_WebAddressWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_FaxWasUpdated);
			ChangesHaveBeenMade = (ChangesHaveBeenMade || col_ActiveWasUpdated);

			if (!ChangesHaveBeenMade) {

				return;
			}

			bool alreadyOpened = false;

			Params.spU_Customers Param = new Params.spU_Customers(true);
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

			Param.Param_CustomerID = this.col_CustomerID;

			if (this.col_CompanyNameWasUpdated) {

				Param.Param_CompanyName = this.col_CompanyName;
				Param.Param_ConsiderNull_CompanyName = true;
			}
			else {

				Param.Param_CompanyName = SqlString.Null;
				Param.Param_ConsiderNull_CompanyName = false;
			}

			if (this.col_ContactNameWasUpdated) {

				Param.Param_ContactName = this.col_ContactName;
				Param.Param_ConsiderNull_ContactName = true;
			}
			else {

				Param.Param_ContactName = SqlString.Null;
				Param.Param_ConsiderNull_ContactName = false;
			}

			if (this.col_TitleIdWasUpdated) {

				Param.Param_TitleId = this.col_TitleId;
				Param.Param_ConsiderNull_TitleId = true;
			}
			else {

				Param.Param_TitleId = SqlInt32.Null;
				Param.Param_ConsiderNull_TitleId = false;
			}

			if (this.col_AddressWasUpdated) {

				Param.Param_Address = this.col_Address;
				Param.Param_ConsiderNull_Address = true;
			}
			else {

				Param.Param_Address = SqlString.Null;
				Param.Param_ConsiderNull_Address = false;
			}

			if (this.col_CityWasUpdated) {

				Param.Param_City = this.col_City;
				Param.Param_ConsiderNull_City = true;
			}
			else {

				Param.Param_City = SqlString.Null;
				Param.Param_ConsiderNull_City = false;
			}

			if (this.col_PostalCodeWasUpdated) {

				Param.Param_PostalCode = this.col_PostalCode;
				Param.Param_ConsiderNull_PostalCode = true;
			}
			else {

				Param.Param_PostalCode = SqlString.Null;
				Param.Param_ConsiderNull_PostalCode = false;
			}

			if (this.col_PhoneWasUpdated) {

				Param.Param_Phone = this.col_Phone;
				Param.Param_ConsiderNull_Phone = true;
			}
			else {

				Param.Param_Phone = SqlString.Null;
				Param.Param_ConsiderNull_Phone = false;
			}

			if (this.col_EmailWasUpdated) {

				Param.Param_Email = this.col_Email;
				Param.Param_ConsiderNull_Email = true;
			}
			else {

				Param.Param_Email = SqlString.Null;
				Param.Param_ConsiderNull_Email = false;
			}

			if (this.col_WebAddressWasUpdated) {

				Param.Param_WebAddress = this.col_WebAddress;
				Param.Param_ConsiderNull_WebAddress = true;
			}
			else {

				Param.Param_WebAddress = SqlString.Null;
				Param.Param_ConsiderNull_WebAddress = false;
			}

			if (this.col_FaxWasUpdated) {

				Param.Param_Fax = this.col_Fax;
				Param.Param_ConsiderNull_Fax = true;
			}
			else {

				Param.Param_Fax = SqlString.Null;
				Param.Param_ConsiderNull_Fax = false;
			}

			if (this.col_ActiveWasUpdated) {

				Param.Param_Active = this.col_Active;
				Param.Param_ConsiderNull_Active = true;
			}
			else {

				Param.Param_Active = SqlBoolean.Null;
				Param.Param_ConsiderNull_Active = false;
			}

			SPs.spU_Customers Sp = new SPs.spU_Customers(false);
			if (Sp.Execute(ref Param)) {

				this.col_CompanyNameWasUpdated = false;
				this.col_ContactNameWasUpdated = false;
				this.col_TitleIdWasUpdated = false;
				this.col_AddressWasUpdated = false;
				this.col_CityWasUpdated = false;
				this.col_PostalCodeWasUpdated = false;
				this.col_PhoneWasUpdated = false;
				this.col_EmailWasUpdated = false;
				this.col_WebAddressWasUpdated = false;
				this.col_FaxWasUpdated = false;
				this.col_ActiveWasUpdated = false;
			}
			else {

				throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.Customer", "Update");
			}
			CloseConnection(Sp.Connection, alreadyOpened);

		}


		private Job_Collection internal_Job_Col_CustomerId_Collection = null;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public Job_Collection Job_Col_CustomerId_Collection {

			get {

				if (this.internal_Job_Col_CustomerId_Collection == null) {

					switch (this.lastKnownConnectionType) {

						case Bob.DataClasses.ConnectionType.ConnectionString:
							this.internal_Job_Col_CustomerId_Collection = new Job_Collection(this.connectionString);
							break;

						case Bob.DataClasses.ConnectionType.SqlConnection:
							this.internal_Job_Col_CustomerId_Collection = new Job_Collection(this.sqlConnection);
							break;

						case Bob.DataClasses.ConnectionType.SqlTransaction:
							this.internal_Job_Col_CustomerId_Collection = new Job_Collection(this.sqlTransaction);
							break;

					}
					this.internal_Job_Col_CustomerId_Collection.LoadFrom_CustomerId(this.col_CustomerID, this);
				}

				return(this.internal_Job_Col_CustomerId_Collection);
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

				Params.spS_Customers_Display Param = new Params.spS_Customers_Display(true);

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

				Param.Param_CustomerID = this.col_CustomerID;

				System.Data.SqlClient.SqlDataReader sqlDataReader = null;
				SPs.spS_Customers_Display Sp = new SPs.spS_Customers_Display(false);
				if (Sp.Execute(ref Param, out sqlDataReader)) {

					if (sqlDataReader.Read()) {

						if (!sqlDataReader.IsDBNull(SPs.spS_Customers_Display.Resultset1.Fields.Column_Display.ColumnIndex)) {

							if (sqlDataReader.GetFieldType(SPs.spS_Customers_Display.Resultset1.Fields.Column_Display.ColumnIndex) == typeof(string)) {

								this.displayName = sqlDataReader.GetString(SPs.spS_Customers_Display.Resultset1.Fields.Column_Display.ColumnIndex);
							}
							else {

								this.displayName = sqlDataReader.GetValue(SPs.spS_Customers_Display.Resultset1.Fields.Column_Display.ColumnIndex).ToString();
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

					throw new Bob.DataClasses.CustomException(Param, "Bob.BusinessComponents.Customer", "ToString");
				}				
			}
			
			return(this.displayName);
		}

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public new Customer MemberwiseClone() {
		
			Customer newRecord = new Customer();
			
			newRecord.recordWasLoadedFromDB = this.recordWasLoadedFromDB;
			newRecord.recordIsLoaded = this.recordIsLoaded;
			newRecord.connectionString = this.connectionString;

			newRecord.col_CustomerID = this.col_CustomerID;


			newRecord.col_CompanyNameWasSet = this.col_CompanyNameWasSet;
			newRecord.col_CompanyNameWasUpdated = this.col_CompanyNameWasUpdated;
			newRecord.col_CompanyName = this.col_CompanyName;

			newRecord.col_ContactNameWasSet = this.col_ContactNameWasSet;
			newRecord.col_ContactNameWasUpdated = this.col_ContactNameWasUpdated;
			newRecord.col_ContactName = this.col_ContactName;

			newRecord.col_TitleIdWasSet = this.col_TitleIdWasSet;
			newRecord.col_TitleIdWasUpdated = this.col_TitleIdWasUpdated;
			newRecord.col_TitleId = this.col_TitleId;

			newRecord.col_TitleId_Record = this.col_TitleId_Record.MemberwiseClone();

			newRecord.col_AddressWasSet = this.col_AddressWasSet;
			newRecord.col_AddressWasUpdated = this.col_AddressWasUpdated;
			newRecord.col_Address = this.col_Address;

			newRecord.col_CityWasSet = this.col_CityWasSet;
			newRecord.col_CityWasUpdated = this.col_CityWasUpdated;
			newRecord.col_City = this.col_City;

			newRecord.col_PostalCodeWasSet = this.col_PostalCodeWasSet;
			newRecord.col_PostalCodeWasUpdated = this.col_PostalCodeWasUpdated;
			newRecord.col_PostalCode = this.col_PostalCode;

			newRecord.col_PhoneWasSet = this.col_PhoneWasSet;
			newRecord.col_PhoneWasUpdated = this.col_PhoneWasUpdated;
			newRecord.col_Phone = this.col_Phone;

			newRecord.col_EmailWasSet = this.col_EmailWasSet;
			newRecord.col_EmailWasUpdated = this.col_EmailWasUpdated;
			newRecord.col_Email = this.col_Email;

			newRecord.col_WebAddressWasSet = this.col_WebAddressWasSet;
			newRecord.col_WebAddressWasUpdated = this.col_WebAddressWasUpdated;
			newRecord.col_WebAddress = this.col_WebAddress;

			newRecord.col_FaxWasSet = this.col_FaxWasSet;
			newRecord.col_FaxWasUpdated = this.col_FaxWasUpdated;
			newRecord.col_Fax = this.col_Fax;

			newRecord.col_ActiveWasSet = this.col_ActiveWasSet;
			newRecord.col_ActiveWasUpdated = this.col_ActiveWasUpdated;
			newRecord.col_Active = this.col_Active;

			newRecord.displayName = this.displayName;
			
			return(newRecord);
		}
	}
}
