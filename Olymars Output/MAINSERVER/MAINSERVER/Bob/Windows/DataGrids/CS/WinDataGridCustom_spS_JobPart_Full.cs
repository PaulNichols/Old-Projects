/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:13
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
using SPs = Bob.DataClasses.StoredProcedures;
using Params = Bob.DataClasses.Parameters;

namespace Bob.Windows.DataGrids {

	/// <summary>
	/// This class is based on a System.Windows.Forms.DataGrid class and was built to display
	/// the resultset returned back by the
	/// 'spS_JobPart_Full' stored procedure.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:13", SqlObjectDependancyName="spS_JobPart_Full", SqlObjectDependancyRevision=16)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinDataGridCustom_spS_JobPart_Full), "WinDataGrid.bmp")]
	public class WinDataGridCustom_spS_JobPart_Full : System.Windows.Forms.DataGrid {

		private string tableName;
		private Params.spS_JobPart_Full param;
		private System.Data.SqlTypes.SqlInt32 param_JobPartId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Data.SqlTypes.SqlInt32 param_JobId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Data.SqlTypes.SqlInt32 param_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		private bool bindingInProgress = false;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private Bob.DataClasses.ConnectionType LastKnownConnectionType = Bob.DataClasses.ConnectionType.None;

		/// <summary>
		/// Returns True if binding operation is in progress.
		/// </summary>
		public bool BindingInProgress {

			get {

				return(this.bindingInProgress);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going or
		/// has been used against the Sql Server database.
		/// </summary>
		public Bob.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Returns the connection string used to access the Sql Server database.
		/// </summary>
		public string ConnectionString {

			get {

				if (this.LastKnownConnectionType != Bob.DataClasses.ConnectionType.ConnectionString) {

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

				if (this.LastKnownConnectionType != Bob.DataClasses.ConnectionType.SqlConnection) {

					throw new InvalidOperationException("The SqlConnection was not set in the class constructor.");
				}

				return(this.sqlConnection );
			}
		}

		/// <summary>
		/// Returns the Parameter class that was used to populate this control.
		/// </summary>
		public Params.spS_JobPart_Full SP_Parameter {

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
		/// <param name="disposing">
		/// true to release both managed and unmanaged resources;
		/// false to release only unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing) {

			if (disposing) {

				this.param_JobPartId = System.Data.SqlTypes.SqlInt32.Null;
				this.param_JobId = System.Data.SqlTypes.SqlInt32.Null;
				this.param_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;

				if (this.param != null) {

					this.param.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the
		/// WinDataGridCustom_spS_JobPart_Full class.
		/// </summary>
		public WinDataGridCustom_spS_JobPart_Full() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_JobPart_Full stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_JobPartId">Value for the parameter @JobPartId.</param>
		/// <param name="param_JobId">Value for the parameter @JobId.</param>
		/// <param name="param_JobPartTypeId">Value for the parameter @JobPartTypeId.</param>
		public void Initialize(string connectionString, string tableName, System.Data.SqlTypes.SqlInt32 param_JobPartId, System.Data.SqlTypes.SqlInt32 param_JobId, System.Data.SqlTypes.SqlInt32 param_JobPartTypeId) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;
			this.tableName = tableName;

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_JobPart_Full'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_JobPart_Full", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.param_JobPartId = param_JobPartId;
			this.param_JobId = param_JobId;
			this.param_JobPartTypeId = param_JobPartTypeId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_JobPart_Full stored procedure parameters.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="param_JobPartId">Value for the parameter @JobPartId.</param>
		/// <param name="param_JobId">Value for the parameter @JobId.</param>
		/// <param name="param_JobPartTypeId">Value for the parameter @JobPartTypeId.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlInt32 param_JobPartId, System.Data.SqlTypes.SqlInt32 param_JobId, System.Data.SqlTypes.SqlInt32 param_JobPartTypeId) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;
			this.tableName = "spS_JobPart_Full";

#if OLYMARS_DEBUG
			object olymarsDebugCheck = System.Configuration.ConfigurationSettings.AppSettings["OlymarsDebugCheck"];
			if (olymarsDebugCheck == null || (string)olymarsDebugCheck == "True") {

				string DebugConnectionString = connectionString;

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromConfigurationFile;
				}

				if (DebugConnectionString.Length == 0) {

					DebugConnectionString = Bob.DataClasses.Information.GetConnectionStringFromRegistry;
				}

				if (DebugConnectionString.Length != 0) {

					System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(DebugConnectionString);

					sqlConnection.Open();

					System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = System.Data.CommandType.Text;
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_JobPart_Full'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_JobPart_Full", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.param_JobPartId = param_JobPartId;
			this.param_JobId = param_JobId;
			this.param_JobPartTypeId = param_JobPartTypeId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_JobPart_Full stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="tableName">name of the table to use to populate the control.</param>
		/// <param name="param_JobPartId">Value for the parameter @JobPartId.</param>
		/// <param name="param_JobId">Value for the parameter @JobId.</param>
		/// <param name="param_JobPartTypeId">Value for the parameter @JobPartTypeId.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, string tableName, System.Data.SqlTypes.SqlInt32 param_JobPartId, System.Data.SqlTypes.SqlInt32 param_JobId, System.Data.SqlTypes.SqlInt32 param_JobPartTypeId) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_JobPart_Full'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_JobPart_Full", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.param_JobPartId = param_JobPartId;
			this.param_JobId = param_JobId;
			this.param_JobPartTypeId = param_JobPartTypeId;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database.
		/// You also need to supply the spS_JobPart_Full stored procedure parameters.
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened, it will be opened when used then closed again after the job is done.</param>
		/// <param name="param_JobPartId">Value for the parameter @JobPartId.</param>
		/// <param name="param_JobId">Value for the parameter @JobId.</param>
		/// <param name="param_JobPartTypeId">Value for the parameter @JobPartTypeId.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 param_JobPartId, System.Data.SqlTypes.SqlInt32 param_JobId, System.Data.SqlTypes.SqlInt32 param_JobPartTypeId) {

			this.sqlConnection = sqlConnection;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;
			this.tableName = "spS_JobPart_Full";

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'spS_JobPart_Full'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "spS_JobPart_Full", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.param_JobPartId = param_JobPartId;
			this.param_JobId = param_JobId;
			this.param_JobPartTypeId = param_JobPartTypeId;
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

			this.CreateControl();

			if (this.LastKnownConnectionType == Bob.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}

			this.param = new Params.spS_JobPart_Full();

			switch (this.LastKnownConnectionType) {

 				case Bob.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case Bob.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;
			}

			this.param.CommandTimeOut = this.commandTimeOut;


			if (!this.param_JobPartId.IsNull) {

				this.param.Param_JobPartId = this.param_JobPartId;
			}

			if (!this.param_JobId.IsNull) {

				this.param.Param_JobId = this.param_JobId;
			}

			if (!this.param_JobPartTypeId.IsNull) {

				this.param.Param_JobPartTypeId = this.param_JobPartTypeId;
			}

			System.Data.DataSet DS = null;
			
			SPs.spS_JobPart_Full SP = new SPs.spS_JobPart_Full();
			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.bindingInProgress = true;
				this.DataSource = DS.Tables[this.tableName].DefaultView;
				this.bindingInProgress = false;
			}

			else {

				SP.Dispose();
				throw new Bob.DataClasses.CustomException(this.param, "WinDataGridCustom_spS_JobPart_Full", "RefreshData");
			}
		}
	}
}
