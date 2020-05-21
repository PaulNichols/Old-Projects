﻿/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:10
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

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

namespace Bob.DataClasses.Parameters {

	/// <summary>
	/// This class allows a developer to specify the parameters expected by the
	/// stored procedure 'spU_Customers'. It allows also to specify specific connection
	/// information such as the ConnectionString to be use, the command time-out and so forth.
	/// </summary>
	[Serializable()]
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:10", SqlObjectDependancyName="spU_Customers", SqlObjectDependancyRevision=112)]
#endif
	public class spU_Customers : MarshalByRefObject, IDisposable, IParameter {

		private ErrorSource errorSource = ErrorSource.NotAvailable;
		private System.Data.SqlClient.SqlException sqlException = null;
		private System.Exception otherException = null;
		private string connectionString = String.Empty;
		private System.Data.SqlClient.SqlConnection sqlConnection = null;
		private System.Data.SqlClient.SqlTransaction sqlTransaction = null;
		private ConnectionType lastKnownConnectionType = ConnectionType.None;
		private int commandTimeOut = 30;

		internal void internal_UpdateExceptionInformation(System.Data.SqlClient.SqlException sqlException) {

			this.sqlException = sqlException;
		}

		internal void internal_UpdateExceptionInformation(System.Exception otherException) {

			this.otherException = otherException;
		}

		internal void internal_SetErrorSource(ErrorSource errorSource) {

			this.errorSource = errorSource;
		}

		private bool useDefaultValue = true;

		/// <summary>
		/// Initializes a new instance of the spU_Customers class. If you use this constructor version,
		/// not assigning parameter values implies using the parameter default values.
		/// </summary>
		public spU_Customers() : this(true) {

		}

		/// <summary>
		/// Initializes a new instance of the spU_Customers class.
		/// </summary>
		/// <param name="useDefaultValue">If True, this parameter indicates that "not assigning parameter values" implies "using the parameter default values". If False, this parameter indicates that "not assigning parameter values" implies "using the SQL Server Null value".</param>
		public spU_Customers(bool useDefaultValue) {

			this.useDefaultValue = useDefaultValue;

			this.internal_Param_CustomerID_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_CompanyName_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_CompanyName_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ContactName_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_ContactName_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_TitleId_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_TitleId_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_Address_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_Address_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_City_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_City_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_PostalCode_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_PostalCode_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_Phone_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_Phone_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_Email_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_Email_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_WebAddress_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_WebAddress_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_Fax_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_Fax_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_Active_UseDefaultValue = this.useDefaultValue;
			this.internal_Param_ConsiderNull_Active_UseDefaultValue = this.useDefaultValue;
		}


		/// <summary>
		/// Sets the connection string to be used against the 
		/// SQL Server database.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public void SetUpConnection(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.lastKnownConnectionType = ConnectionType.ConnectionString;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spU_Customers'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spU_Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif
		}

		/// <summary>
		/// Sets the System.Data.SqlClient.SqlConnection to be used
		/// against the SQL Server database.
		/// </summary>
		/// <param name="sqlConnection">A valid System.Data.SqlClient.SqlConnection object. It can be opened or not. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		public void SetUpConnection(System.Data.SqlClient.SqlConnection sqlConnection) {

			if (sqlConnection == null) {
				throw new ArgumentNullException("sqlConnection", "Invalid connection!");
			}

			this.sqlConnection = sqlConnection;
			this.lastKnownConnectionType = ConnectionType.SqlConnection;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spU_Customers'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spU_Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif
		}

		/// <summary>
		/// Sets the System.Data.SqlClient.SqlTransaction to be used
		/// against the SQL Server database.
		/// </summary>
		/// <param name="sqlTransaction">A valid System.Data.SqlClient.SqlTransaction object.</param>
		public void SetUpConnection(System.Data.SqlClient.SqlTransaction sqlTransaction) {

			if (sqlTransaction == null || sqlTransaction.Connection == null) {
				throw new ArgumentNullException("sqlTransaction", "Invalid connection!");
			}

			this.sqlTransaction= sqlTransaction;
			this.lastKnownConnectionType = ConnectionType.SqlTransaction;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spU_Customers'";
				sqlCommand.Transaction = sqlTransaction;

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlTransaction.Connection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spU_Customers", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public void Dispose() {

			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {

			if (disposing) {

				this.internal_Param_RETURN_VALUE = System.Data.SqlTypes.SqlInt32.Null;
				this.internal_Param_CustomerID = System.Data.SqlTypes.SqlInt32.Null;
				this.internal_Param_CompanyName = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_CompanyName = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_ContactName = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_ContactName = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_TitleId = System.Data.SqlTypes.SqlInt32.Null;
				this.internal_Param_ConsiderNull_TitleId = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_Address = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_Address = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_City = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_City = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_PostalCode = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_PostalCode = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_Phone = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_Phone = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_Email = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_Email = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_WebAddress = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_WebAddress = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_Fax = System.Data.SqlTypes.SqlString.Null;
				this.internal_Param_ConsiderNull_Fax = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_Active = System.Data.SqlTypes.SqlBoolean.Null;
				this.internal_Param_ConsiderNull_Active = System.Data.SqlTypes.SqlBoolean.Null;

				this.sqlException = null;
				this.otherException = null;
				this.connectionString = null;
				this.sqlConnection = null;
				this.sqlTransaction = null;

			}
		}

		/// <summary>
		/// This member overrides 'Object.Finalize'.
		/// </summary>
		~spU_Customers() {

			Dispose(false);
		}

		/// <summary>
		/// Returns the stored procedure name for which this class was built, i.e. 'spU_Customers'.
		/// </summary>
		public string StoredProcedureName {

			get {

				return("spU_Customers");
			}
		}

		private System.Data.SqlTypes.SqlInt32 internal_Param_RETURN_VALUE;

		private System.Data.SqlTypes.SqlInt32 internal_Param_CustomerID;
		internal bool internal_Param_CustomerID_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_CompanyName;
		internal bool internal_Param_CompanyName_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_CompanyName;
		internal bool internal_Param_ConsiderNull_CompanyName_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_ContactName;
		internal bool internal_Param_ContactName_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_ContactName;
		internal bool internal_Param_ConsiderNull_ContactName_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlInt32 internal_Param_TitleId;
		internal bool internal_Param_TitleId_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_TitleId;
		internal bool internal_Param_ConsiderNull_TitleId_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_Address;
		internal bool internal_Param_Address_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_Address;
		internal bool internal_Param_ConsiderNull_Address_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_City;
		internal bool internal_Param_City_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_City;
		internal bool internal_Param_ConsiderNull_City_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_PostalCode;
		internal bool internal_Param_PostalCode_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_PostalCode;
		internal bool internal_Param_ConsiderNull_PostalCode_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_Phone;
		internal bool internal_Param_Phone_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_Phone;
		internal bool internal_Param_ConsiderNull_Phone_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_Email;
		internal bool internal_Param_Email_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_Email;
		internal bool internal_Param_ConsiderNull_Email_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_WebAddress;
		internal bool internal_Param_WebAddress_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_WebAddress;
		internal bool internal_Param_ConsiderNull_WebAddress_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlString internal_Param_Fax;
		internal bool internal_Param_Fax_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_Fax;
		internal bool internal_Param_ConsiderNull_Fax_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_Active;
		internal bool internal_Param_Active_UseDefaultValue = true;

		private System.Data.SqlTypes.SqlBoolean internal_Param_ConsiderNull_Active;
		internal bool internal_Param_ConsiderNull_Active_UseDefaultValue = true;


		/// <summary>
		/// Allows you to know at which step did the error occured if one occured. See <see cref="ErrorSource" />
		/// for more information.
		/// </summary>
		public ErrorSource ErrorSource {

			get {

				return(this.errorSource);
			}
		}

		/// <summary>
		/// This method allows you to reset the parameter object. Please note that this
		/// method resets all the parameters members except the connection information and
		/// the command time-out which are left in their current state.
		/// </summary>
		public void Reset() {


			this.internal_Param_RETURN_VALUE = System.Data.SqlTypes.SqlInt32.Null;

			this.internal_Param_CustomerID = System.Data.SqlTypes.SqlInt32.Null;
			this.internal_Param_CustomerID_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_CompanyName = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_CompanyName_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_CompanyName = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_CompanyName_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ContactName = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_ContactName_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_ContactName = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_ContactName_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_TitleId = System.Data.SqlTypes.SqlInt32.Null;
			this.internal_Param_TitleId_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_TitleId = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_TitleId_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_Address = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_Address_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_Address = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_Address_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_City = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_City_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_City = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_City_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_PostalCode = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_PostalCode_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_PostalCode = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_PostalCode_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_Phone = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_Phone_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_Phone = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_Phone_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_Email = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_Email_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_Email = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_Email_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_WebAddress = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_WebAddress_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_WebAddress = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_WebAddress_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_Fax = System.Data.SqlTypes.SqlString.Null;
			this.internal_Param_Fax_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_Fax = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_Fax_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_Active = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_Active_UseDefaultValue = this.useDefaultValue;

			this.internal_Param_ConsiderNull_Active = System.Data.SqlTypes.SqlBoolean.Null;
			this.internal_Param_ConsiderNull_Active_UseDefaultValue = this.useDefaultValue;

			this.errorSource = ErrorSource.NotAvailable;
			this.sqlException = null;
			this.otherException = null;
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
		public ConnectionType ConnectionType {

			get {

				return(this.lastKnownConnectionType );
			}
		}

		/// <summary>
		/// In case of an ADO exception, returns the SqlException exception (System.Data.SqlClient.SqlException)
		/// that has occured.
		/// </summary>
		public System.Data.SqlClient.SqlException SqlException {

			get {

				return(this.sqlException);
			}
		}

		/// <summary>
		/// In case of a System exception, returns the standard exception (System.Exception) that 
		/// has occured.
		/// </summary>
		public System.Exception OtherException {

			get {

				return(this.otherException);
			}
		}

		/// <summary>
		/// Sets or returns the time-out (in seconds) to be use by the ADO command object
		/// (System.Data.SqlClient.SqlCommand).
		/// <remarks>
		/// Default value is 30 seconds.
		/// </remarks>
		/// </summary>
		public int CommandTimeOut {

			get {

				return(this.commandTimeOut);
			}

			set {

				this.commandTimeOut = value;
				if (this.commandTimeOut <= 0) {

					this.commandTimeOut = 30;
				}
			}
		}


		/// <summary>
		/// Returns the value returned back by the stored procedure execution.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 Param_RETURN_VALUE {

			get {

				return(this.internal_Param_RETURN_VALUE);
			}
		}
            
		internal void internal_Set_RETURN_VALUE(System.Data.SqlTypes.SqlInt32 value) {

			this.internal_Param_RETURN_VALUE = value;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@CustomerID'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [int]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_CustomerID_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlInt32 Param_CustomerID {

			get {

				if (this.internal_Param_CustomerID_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_CustomerID);
			}

			set {

				this.internal_Param_CustomerID_UseDefaultValue = false;
				this.internal_Param_CustomerID = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@CustomerID' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_CustomerID_UseDefaultValue() {

			this.internal_Param_CustomerID_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@CompanyName'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](80)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_CompanyName_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_CompanyName {

			get {

				if (this.internal_Param_CompanyName_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_CompanyName);
			}

			set {

				this.internal_Param_CompanyName_UseDefaultValue = false;
				this.internal_Param_CompanyName = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@CompanyName' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_CompanyName_UseDefaultValue() {

			this.internal_Param_CompanyName_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_CompanyName'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_CompanyName_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_CompanyName {

			get {

				if (this.internal_Param_ConsiderNull_CompanyName_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_CompanyName);
			}

			set {

				this.internal_Param_ConsiderNull_CompanyName_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_CompanyName = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_CompanyName' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_CompanyName_UseDefaultValue() {

			this.internal_Param_ConsiderNull_CompanyName_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ContactName'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](60)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ContactName_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_ContactName {

			get {

				if (this.internal_Param_ContactName_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ContactName);
			}

			set {

				this.internal_Param_ContactName_UseDefaultValue = false;
				this.internal_Param_ContactName = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ContactName' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ContactName_UseDefaultValue() {

			this.internal_Param_ContactName_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_ContactName'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_ContactName_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_ContactName {

			get {

				if (this.internal_Param_ConsiderNull_ContactName_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_ContactName);
			}

			set {

				this.internal_Param_ConsiderNull_ContactName_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_ContactName = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_ContactName' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_ContactName_UseDefaultValue() {

			this.internal_Param_ConsiderNull_ContactName_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@TitleId'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [int]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_TitleId_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlInt32 Param_TitleId {

			get {

				if (this.internal_Param_TitleId_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_TitleId);
			}

			set {

				this.internal_Param_TitleId_UseDefaultValue = false;
				this.internal_Param_TitleId = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@TitleId' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_TitleId_UseDefaultValue() {

			this.internal_Param_TitleId_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_TitleId'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_TitleId_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_TitleId {

			get {

				if (this.internal_Param_ConsiderNull_TitleId_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_TitleId);
			}

			set {

				this.internal_Param_ConsiderNull_TitleId_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_TitleId = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_TitleId' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_TitleId_UseDefaultValue() {

			this.internal_Param_ConsiderNull_TitleId_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@Address'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](120)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_Address_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_Address {

			get {

				if (this.internal_Param_Address_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_Address);
			}

			set {

				this.internal_Param_Address_UseDefaultValue = false;
				this.internal_Param_Address = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@Address' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_Address_UseDefaultValue() {

			this.internal_Param_Address_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_Address'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_Address_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_Address {

			get {

				if (this.internal_Param_ConsiderNull_Address_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_Address);
			}

			set {

				this.internal_Param_ConsiderNull_Address_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_Address = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_Address' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_Address_UseDefaultValue() {

			this.internal_Param_ConsiderNull_Address_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@City'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](30)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_City_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_City {

			get {

				if (this.internal_Param_City_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_City);
			}

			set {

				this.internal_Param_City_UseDefaultValue = false;
				this.internal_Param_City = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@City' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_City_UseDefaultValue() {

			this.internal_Param_City_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_City'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_City_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_City {

			get {

				if (this.internal_Param_ConsiderNull_City_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_City);
			}

			set {

				this.internal_Param_ConsiderNull_City_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_City = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_City' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_City_UseDefaultValue() {

			this.internal_Param_ConsiderNull_City_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@PostalCode'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](20)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_PostalCode_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_PostalCode {

			get {

				if (this.internal_Param_PostalCode_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_PostalCode);
			}

			set {

				this.internal_Param_PostalCode_UseDefaultValue = false;
				this.internal_Param_PostalCode = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@PostalCode' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_PostalCode_UseDefaultValue() {

			this.internal_Param_PostalCode_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_PostalCode'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_PostalCode_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_PostalCode {

			get {

				if (this.internal_Param_ConsiderNull_PostalCode_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_PostalCode);
			}

			set {

				this.internal_Param_ConsiderNull_PostalCode_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_PostalCode = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_PostalCode' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_PostalCode_UseDefaultValue() {

			this.internal_Param_ConsiderNull_PostalCode_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@Phone'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](48)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_Phone_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_Phone {

			get {

				if (this.internal_Param_Phone_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_Phone);
			}

			set {

				this.internal_Param_Phone_UseDefaultValue = false;
				this.internal_Param_Phone = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@Phone' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_Phone_UseDefaultValue() {

			this.internal_Param_Phone_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_Phone'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_Phone_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_Phone {

			get {

				if (this.internal_Param_ConsiderNull_Phone_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_Phone);
			}

			set {

				this.internal_Param_ConsiderNull_Phone_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_Phone = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_Phone' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_Phone_UseDefaultValue() {

			this.internal_Param_ConsiderNull_Phone_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@Email'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](200)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_Email_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_Email {

			get {

				if (this.internal_Param_Email_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_Email);
			}

			set {

				this.internal_Param_Email_UseDefaultValue = false;
				this.internal_Param_Email = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@Email' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_Email_UseDefaultValue() {

			this.internal_Param_Email_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_Email'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_Email_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_Email {

			get {

				if (this.internal_Param_ConsiderNull_Email_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_Email);
			}

			set {

				this.internal_Param_ConsiderNull_Email_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_Email = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_Email' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_Email_UseDefaultValue() {

			this.internal_Param_ConsiderNull_Email_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@WebAddress'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](48)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_WebAddress_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_WebAddress {

			get {

				if (this.internal_Param_WebAddress_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_WebAddress);
			}

			set {

				this.internal_Param_WebAddress_UseDefaultValue = false;
				this.internal_Param_WebAddress = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@WebAddress' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_WebAddress_UseDefaultValue() {

			this.internal_Param_WebAddress_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_WebAddress'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_WebAddress_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_WebAddress {

			get {

				if (this.internal_Param_ConsiderNull_WebAddress_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_WebAddress);
			}

			set {

				this.internal_Param_ConsiderNull_WebAddress_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_WebAddress = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_WebAddress' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_WebAddress_UseDefaultValue() {

			this.internal_Param_ConsiderNull_WebAddress_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@Fax'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [nvarchar](48)
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_Fax_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlString Param_Fax {

			get {

				if (this.internal_Param_Fax_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_Fax);
			}

			set {

				this.internal_Param_Fax_UseDefaultValue = false;
				this.internal_Param_Fax = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@Fax' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_Fax_UseDefaultValue() {

			this.internal_Param_Fax_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_Fax'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_Fax_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_Fax {

			get {

				if (this.internal_Param_ConsiderNull_Fax_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_Fax);
			}

			set {

				this.internal_Param_ConsiderNull_Fax_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_Fax = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_Fax' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_Fax_UseDefaultValue() {

			this.internal_Param_ConsiderNull_Fax_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@Active'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_Active_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_Active {

			get {

				if (this.internal_Param_Active_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_Active);
			}

			set {

				this.internal_Param_Active_UseDefaultValue = false;
				this.internal_Param_Active = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@Active' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_Active_UseDefaultValue() {

			this.internal_Param_Active_UseDefaultValue = true;
		}

		/// <summary>
		/// Sets the value of the stored procedure INPUT parameter '@ConsiderNull_Active'.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>
		/// <description>
		/// In SQL Server, this parameter is of type: [bit]
		/// </description>
		/// </item>
		/// <item>
		/// <description>
		/// If this parameter is not set before the stored procedure call occurs, a Null (SQL Server meaning) value
		/// will be supplied to the corresponding parameter when the call is made. If you wish to use
		/// the parameter default value, consider calling the Param_ConsiderNull_Active_UseDefaultValue() method.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public System.Data.SqlTypes.SqlBoolean Param_ConsiderNull_Active {

			get {

				if (this.internal_Param_ConsiderNull_Active_UseDefaultValue) {
				
					throw new InvalidOperationException("This parameter is not assigned and maps to the stored procedure parameter default value.");
				}
				return(this.internal_Param_ConsiderNull_Active);
			}

			set {

				this.internal_Param_ConsiderNull_Active_UseDefaultValue = false;
				this.internal_Param_ConsiderNull_Active = value;
			}
		}            

		/// <summary>
		/// Indicates that the '@ConsiderNull_Active' parameter value is not supplied and that the default value should be used.
		/// </summary>
		public void Param_ConsiderNull_Active_UseDefaultValue() {

			this.internal_Param_ConsiderNull_Active_UseDefaultValue = true;
		}

  	}
}

namespace Bob.DataClasses.StoredProcedures {

	/// <summary>
	/// This class allows you to execute the 'spU_Customers' stored procedure;
	/// it gives you the ability to:
	/// <list type="bullet">
	/// <item><description>Set all the necessary parameters to execute the stored procedure</description></item>
	/// <item><description>To execute the stored procedure</description></item>
	/// <item><description>To get back after the execution the return value and all the output parameters value</description></item>
	/// </list>
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:10", SqlObjectDependancyName="spU_Customers", SqlObjectDependancyRevision=112)]
#endif
	public class spU_Customers : MarshalByRefObject, IDisposable {


		private bool throwExceptionOnExecute = false;

		/// <summary>
		/// Initializes a new instance of the spU_Customers class.
		/// By default, no exception will be thrown when you call the Execute method. Instead, a Boolean return status will be returned.
		/// </summary>
		public spU_Customers() : this(false) {

		}

		/// <summary>
		/// Initializes a new instance of the spU_Customers class.
		/// </summary>
		/// <param name="throwExceptionOnExecute">True if an exception has to be thrown if the Execute call fails.</param>
		public spU_Customers(bool throwExceptionOnExecute) {

			this.throwExceptionOnExecute = throwExceptionOnExecute;
		}

		private System.Data.SqlClient.SqlConnection sqlConnection;
		/// <summary>
		/// The <see cref="System.Data.SqlClient.SqlConnection">System.Data.SqlClient.SqlConnection</see> that was actually used by this class.
		/// </summary>
		public System.Data.SqlClient.SqlConnection Connection {

			get {

				return(this.sqlConnection);
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public void Dispose() {

			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.
		/// </summary>
		private void Dispose(bool disposing) {

			if (disposing) {

			}
		}

		/// <summary>
		/// This member overrides 'Object.Finalize'.
		/// </summary>
		~spU_Customers() {

			Dispose(false);
		}

		private void ResetParameter(ref Bob.DataClasses.Parameters.spU_Customers parameters) {

			parameters.internal_Set_RETURN_VALUE (System.Data.SqlTypes.SqlInt32.Null);
		}

		private bool InitializeConnection(ref Bob.DataClasses.Parameters.spU_Customers parameters, out System.Data.SqlClient.SqlCommand sqlCommand, ref bool connectionMustBeClosed) {

			try {

				this.sqlConnection = null;
				sqlCommand = null;
				connectionMustBeClosed = true;

				if (parameters.ConnectionType == ConnectionType.None) {

					throw new InvalidOperationException("No connection information was supplied. Consider calling the 'SetUpConnection' method of the Bob.DataClasses.Parameters.spU_Customers object before doing this call.");
				}

				if (parameters.ConnectionType == ConnectionType.SqlConnection && parameters.SqlConnection == null) {

					throw new InvalidOperationException("No connection information was supplied (SqlConnection == null). Consider calling the 'SetUpConnection' method of the Bob.DataClasses.Parameters.spU_Customers object before doing this call.");
				}

				if (parameters.ConnectionType == ConnectionType.SqlTransaction && parameters.SqlTransaction== null) {

					throw new InvalidOperationException("No connection information was supplied (SqlTransaction == null). Consider calling the 'SetUpConnection' method of the Bob.DataClasses.Parameters.spU_Customers object before doing this call.");
				}

				switch (parameters.ConnectionType) {

					case ConnectionType.ConnectionString:

						string connectionString;
				
						if (parameters.ConnectionString.Length == 0) {

							connectionString = Information.GetConnectionStringFromConfigurationFile;
							if (connectionString.Length == 0) {

								connectionString = Information.GetConnectionStringFromRegistry;
							}
						}

						else {

							connectionString = parameters.ConnectionString;
						}

						if (connectionString.Length == 0) {

							throw new System.InvalidOperationException("No connection information was supplied (ConnectionString == \"\")! (Bob.DataClasses.Parameters.spU_Customers)");
						}

						parameters.internal_SetErrorSource(ErrorSource.ConnectionOpening);
						this.sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
						this.sqlConnection.Open();

						sqlCommand = sqlConnection.CreateCommand();
						break;

					case ConnectionType.SqlConnection:

						sqlConnection = parameters.SqlConnection;

						if (this.sqlConnection.State != System.Data.ConnectionState.Open) {

							this.sqlConnection.Open();
						}
						else {

							connectionMustBeClosed = false;
						}
						sqlCommand = sqlConnection.CreateCommand();
						break;

					case ConnectionType.SqlTransaction:

						sqlCommand = new System.Data.SqlClient.SqlCommand();
						this.sqlConnection = parameters.SqlTransaction.Connection;
						if (this.sqlConnection == null) {

							throw new InvalidOperationException("The transaction is no longer valid.");
						}

						if (this.sqlConnection.State != System.Data.ConnectionState.Open) {
						
							this.sqlConnection.Open();
						}
						else {

							connectionMustBeClosed = false;
						}
						sqlCommand.Connection = sqlConnection;
						sqlCommand.Transaction = parameters.SqlTransaction;						break;
				}

				sqlCommand.CommandTimeout = parameters.CommandTimeOut;
				sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
				sqlCommand.CommandText = "spU_Customers";

				return(true);
			}

			catch (System.Data.SqlClient.SqlException sqlException) {

				sqlConnection = null;
				sqlCommand = null;
				parameters.internal_UpdateExceptionInformation(sqlException);
				return(false);
			}

			catch (System.Exception exception) {

				sqlConnection = null;
				sqlCommand = null;
				parameters.internal_UpdateExceptionInformation(exception);
				return(false);
			}
		}

		private bool DeclareParameters(ref Bob.DataClasses.Parameters.spU_Customers parameters, ref System.Data.SqlClient.SqlCommand sqlCommand) {

			try {

				System.Data.SqlClient.SqlParameter sqlParameter;

				sqlParameter = new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4);
				sqlParameter.Direction = System.Data.ParameterDirection.ReturnValue;
				sqlParameter.IsNullable = true;
				sqlParameter.Value = System.DBNull.Value;
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.Int, 4);
				sqlParameter.SourceColumn = "CustomerID";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_CustomerID_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_CustomerID.IsNull) {

					sqlParameter.Value = parameters.Param_CustomerID;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 80);
				sqlParameter.SourceColumn = "CompanyName";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_CompanyName_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_CompanyName.IsNull) {

					sqlParameter.Value = parameters.Param_CompanyName;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_CompanyName", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_CompanyName_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_CompanyName.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_CompanyName;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ContactName", System.Data.SqlDbType.NVarChar, 60);
				sqlParameter.SourceColumn = "ContactName";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ContactName_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ContactName.IsNull) {

					sqlParameter.Value = parameters.Param_ContactName;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_ContactName", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_ContactName_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_ContactName.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_ContactName;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@TitleId", System.Data.SqlDbType.Int, 4);
				sqlParameter.SourceColumn = "TitleId";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_TitleId_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_TitleId.IsNull) {

					sqlParameter.Value = parameters.Param_TitleId;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_TitleId", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_TitleId_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_TitleId.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_TitleId;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 120);
				sqlParameter.SourceColumn = "Address";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_Address_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_Address.IsNull) {

					sqlParameter.Value = parameters.Param_Address;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_Address", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_Address_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_Address.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_Address;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 30);
				sqlParameter.SourceColumn = "City";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_City_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_City.IsNull) {

					sqlParameter.Value = parameters.Param_City;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_City", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_City_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_City.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_City;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar, 20);
				sqlParameter.SourceColumn = "PostalCode";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_PostalCode_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_PostalCode.IsNull) {

					sqlParameter.Value = parameters.Param_PostalCode;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_PostalCode", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_PostalCode_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_PostalCode.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_PostalCode;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 48);
				sqlParameter.SourceColumn = "Phone";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_Phone_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_Phone.IsNull) {

					sqlParameter.Value = parameters.Param_Phone;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_Phone", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_Phone_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_Phone.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_Phone;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.NVarChar, 200);
				sqlParameter.SourceColumn = "Email";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_Email_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_Email.IsNull) {

					sqlParameter.Value = parameters.Param_Email;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_Email", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_Email_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_Email.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_Email;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@WebAddress", System.Data.SqlDbType.NVarChar, 48);
				sqlParameter.SourceColumn = "WebAddress";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_WebAddress_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_WebAddress.IsNull) {

					sqlParameter.Value = parameters.Param_WebAddress;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_WebAddress", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_WebAddress_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_WebAddress.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_WebAddress;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.NVarChar, 48);
				sqlParameter.SourceColumn = "Fax";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_Fax_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_Fax.IsNull) {

					sqlParameter.Value = parameters.Param_Fax;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_Fax", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_Fax_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_Fax.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_Fax;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1);
				sqlParameter.SourceColumn = "Active";
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_Active_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_Active.IsNull) {

					sqlParameter.Value = parameters.Param_Active;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);

				sqlParameter = new System.Data.SqlClient.SqlParameter("@ConsiderNull_Active", System.Data.SqlDbType.Bit, 1);
				sqlParameter.Direction = System.Data.ParameterDirection.Input;
				if (parameters.internal_Param_ConsiderNull_Active_UseDefaultValue) {

					sqlParameter.Value = null;				
				}
				else if (!parameters.Param_ConsiderNull_Active.IsNull) {

					sqlParameter.Value = parameters.Param_ConsiderNull_Active;
				}
				else {

					sqlParameter.IsNullable = true;
					sqlParameter.Value = System.DBNull.Value;
				}
				sqlCommand.Parameters.Add(sqlParameter);


				return(true);

			}

			catch (System.Data.SqlClient.SqlException sqlException) {

				parameters.internal_UpdateExceptionInformation(sqlException);
				return(false);
			}

			catch (System.Exception exception) {

				parameters.internal_UpdateExceptionInformation(exception);
				return(false);
			}
		}

		private bool RetrieveParameters(ref Bob.DataClasses.Parameters.spU_Customers parameters, ref System.Data.SqlClient.SqlCommand sqlCommand) {

			try {

				if (sqlCommand.Parameters["@RETURN_VALUE"].Value != System.DBNull.Value) {

					parameters.internal_Set_RETURN_VALUE ((System.Int32)sqlCommand.Parameters["@RETURN_VALUE"].Value);
				}
				else {

					parameters.internal_Set_RETURN_VALUE (System.Data.SqlTypes.SqlInt32.Null);
				}

				return(true);
			}

			catch (System.Data.SqlClient.SqlException sqlException) {

				parameters.internal_UpdateExceptionInformation(sqlException);
				return(false);
			}

			catch (System.Exception exception) {

				parameters.internal_UpdateExceptionInformation(exception);
				return(false);
			}
		}

		/// <summary>
		/// This method allows you to execute the [spU_Customers] stored procedure.
		/// </summary>
		/// <param name="parameters">
		/// Contains all the necessary information to execute correctly the stored procedure, i.e. 
		/// the database connection to use and all the necessary input parameters to be supplied
		/// for this stored procedure execution. After the execution, this object will allow you
		/// to retrieve back the stored procedure return value and all the output parameters.
		/// </param>
		/// <returns>True if the call was successful. Otherwise, it returns False.</returns>
		public bool Execute(ref Bob.DataClasses.Parameters.spU_Customers parameters) {

			System.Data.SqlClient.SqlCommand sqlCommand = null;
			System.Boolean returnStatus = false;
			System.Boolean connectionMustBeClosed = true;

			try {
				ResetParameter(ref parameters);

				parameters.internal_SetErrorSource(ErrorSource.ConnectionInitialization);
				returnStatus = InitializeConnection(ref parameters, out sqlCommand, ref connectionMustBeClosed);
				if (!returnStatus) {

					return(false);
				}

				parameters.internal_SetErrorSource(ErrorSource.ParametersSetting);
				returnStatus = DeclareParameters(ref parameters, ref sqlCommand);
				if (!returnStatus) {

					return(false);
				}

				parameters.internal_SetErrorSource(ErrorSource.QueryExecution);
				sqlCommand.ExecuteNonQuery();
                
				parameters.internal_SetErrorSource(ErrorSource.ParametersRetrieval);
				returnStatus = RetrieveParameters(ref parameters, ref sqlCommand);
			}

			catch (System.Data.SqlClient.SqlException sqlException) {

				parameters.internal_UpdateExceptionInformation(sqlException);
				returnStatus = false;

				if (this.throwExceptionOnExecute) {

					throw sqlException;
				}
			}

			catch (System.Exception exception) {

				parameters.internal_UpdateExceptionInformation(exception);
				returnStatus = false;
				parameters.internal_SetErrorSource(ErrorSource.Other);

				if (this.throwExceptionOnExecute) {

					throw exception;
				}
			}

			finally {

				if (sqlCommand != null) {

					sqlCommand.Dispose();
				}

				if (parameters.SqlTransaction == null) {

					if (this.sqlConnection != null && connectionMustBeClosed && this.sqlConnection.State == System.Data.ConnectionState.Open) {

						this.sqlConnection.Close();
						this.sqlConnection.Dispose();
					}
				}

				if (returnStatus) {

					parameters.internal_SetErrorSource(ErrorSource.NoError);
				}
				else {

					if (this.throwExceptionOnExecute) {

						if (parameters.SqlException != null) {

							throw parameters.SqlException;
						}
						else {

							throw parameters.OtherException;
						}
					}
				}
			}
			return(returnStatus);
       
		}

	}

}

