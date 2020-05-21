/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 27/12/2004 14:12:01
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

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
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using Params = OlymarsDemo.DataClasses.Parameters;

[assembly:System.Web.UI.TagPrefix("OlymarsDemo.Web.DataGrids", "OlymarsDemoDataGrids")]

namespace OlymarsDemo.Web.DataGrids {

	/// <summary>
	/// This class is based on a System.Web.UI.WebControls.DataGrid class and was built to display
	/// one of the resultset returned back by the
	/// 'spS_xSearchCustomer' stored procedure.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/12/27 14:12:01", SqlObjectDependancyName="spS_xSearchCustomer", SqlObjectDependancyRevision=64)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WebDataGridCustom_spS_xSearchCustomer), "WebDataGrid.bmp")]
	public class WebDataGridCustom_spS_xSearchCustomer : System.Web.UI.WebControls.DataGrid {

		private string tableName;
		private Params.spS_xSearchCustomer param;
		private System.Data.SqlTypes.SqlString param_LastName = System.Data.SqlTypes.SqlString.Null;
		private System.Data.SqlTypes.SqlString param_FirstName = System.Data.SqlTypes.SqlString.Null;
		private string primaryKey = String.Empty;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private OlymarsDemo.DataClasses.ConnectionType LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
		private bool doDataBindAfterRefreshData = true;

		/// <summary>
		/// Returns the current type of connection that is going or
		/// has been used against the Sql Server database.
		/// </summary>
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.LastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.ConnectionString) {

					throw new InvalidOperationException("The connection string was not set in the class constructor.");
				}

				return(this.connectionString);
			}
		}

		/// <summary>
		/// Returns the SqlConnection used to access the Sql Server database.
		/// </summary>
		public System.Data.SqlClient.SqlConnection SqlConnection {

			get {

				if (this.LastKnownConnectionType != OlymarsDemo.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the Parameter class that was used to populate this control.
		/// </summary>
		public Params.spS_xSearchCustomer SP_Parameter {

			get {

				return(this.param);
			}
		}

		private int commandTimeOut = 30;
		/// <summary>
		/// Gets or sets the command timeout for the underlying stored procedure execution (default is 30 seconds)
		/// </summary>
		public int CommandTimeOut {

			get {

				return(this.commandTimeOut);
			}
			set {

				this.commandTimeOut = value;
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public override void Dispose() {

			this.param_LastName = System.Data.SqlTypes.SqlString.Null;
			this.param_FirstName = System.Data.SqlTypes.SqlString.Null;

			if (this.param != null) {

				this.param.Dispose();
			}

			base.Dispose();
		}

		/// <summary>
		/// Create a new instance of the
		/// WebDataGridCustom_spS_xSearchCustomer class.
		/// </summary>
		public WebDataGridCustom_spS_xSearchCustomer() : base() {

		}

		/// <summary>
		/// Gets or sets the databinding behavior after the RefreshData method is called. True if DataBind has to be called after
		/// RefreshData call, False if not.
		/// </summary>
		public bool DoDataBindAfterRefreshData {

			get {

				return(this.doDataBindAfterRefreshData);
			}

			set {

				this.doDataBindAfterRefreshData = value;
			}
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="primaryKey">The field that represents a primary key.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(string connectionString, string primaryKey, string tableName, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
			this.primaryKey = primaryKey;
			this.tableName = tableName;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="primaryKey">The field that represents a primary key.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(string connectionString, string primaryKey, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
			this.primaryKey = primaryKey;
			this.tableName = "spS_xSearchCustomer";

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}


		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;
			this.tableName = "spS_xSearchCustomer";
			this.primaryKey = String.Empty;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = OlymarsDemo.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="primaryKey">The field that represents a primary key.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, string primaryKey, string tableName, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
			this.primaryKey = primaryKey;
			this.tableName = tableName;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="primaryKey">The field that represents a primary key.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, string primaryKey, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
			this.primaryKey = primaryKey;
			this.tableName = "spS_xSearchCustomer";

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_xSearchCustomer stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="param_LastName">Value for the parameter @LastName.</param>
		/// <param name="param_FirstName">Value for the parameter @FirstName.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlString param_LastName, System.Data.SqlTypes.SqlString param_FirstName) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;
			this.tableName = "spS_xSearchCustomer";
			this.primaryKey = String.Empty;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_xSearchCustomer'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_xSearchCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.param_LastName = param_LastName;
			this.param_FirstName = param_FirstName;
		}

		/// <summary>
		/// Load or reloads the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(-1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the chosen resultset returned by the stored procedure.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			if (this.LastKnownConnectionType == OlymarsDemo.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}

			this.param = new Params.spS_xSearchCustomer();

			switch (this.LastKnownConnectionType) {

 				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;
			}

			this.param.CommandTimeOut = this.commandTimeOut;


			if (!this.param_LastName.IsNull) {

				this.param.Param_LastName = this.param_LastName;
			}

			if (!this.param_FirstName.IsNull) {

				this.param.Param_FirstName = this.param_FirstName;
			}

			System.Data.DataSet DS = null;
			
			SPs.spS_xSearchCustomer SP = new SPs.spS_xSearchCustomer();
			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.DataSource = DS.Tables[this.tableName].DefaultView;
				if (this.primaryKey.Length != 0) {

					this.DataKeyField = this.primaryKey;
				}
				if (this.doDataBindAfterRefreshData) {

					this.DataBind();
				}
				SP.Dispose();
			}

			else {

				SP.Dispose();
				throw new OlymarsDemo.DataClasses.CustomException(this.param, "WebDataGridCustom_spS_xSearchCustomer", "RefreshData");
			}
		}
	}
}
