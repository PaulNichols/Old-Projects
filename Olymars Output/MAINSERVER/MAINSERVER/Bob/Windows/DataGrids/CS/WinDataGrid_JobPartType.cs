﻿/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 15/01/2005 18:39:12
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

namespace Bob.Windows.DataGrids {

	/// <summary>
	/// This class is based on a System.Windows.Forms.DataGrid class and was built to display the 'JobPartType' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:12", SqlObjectDependancyName="JobPartType", SqlObjectDependancyRevision=272)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinDataGrid_JobPartType), "WinDataGrid.bmp")]
	public class WinDataGrid_JobPartType : System.Windows.Forms.DataGrid {

			private System.Data.DataSet dataSet = null;
		private Bob.SqlDataAdapters.SqlDataAdapter_JobPartType sqlDataAdapter = null;
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
		/// Returns the System.Data.SqlClient.SqlDataAdapter used to build the System.Data.DataSet that was used to fill this System.Windows.Forms.DataGrid.
		/// </summary>
		public Bob.SqlDataAdapters.SqlDataAdapter_JobPartType SqlDataAdapter {

			get {

				return(this.sqlDataAdapter);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString or SqlConnection.
		/// </summary>
		public Bob.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Commit all the changes that have been made in this System.Windows.Forms.DataGrid.
		/// </summary>
		public void CommitChanges() {

			this.sqlDataAdapter.SqlDataAdapter.Update(this.dataSet, "JobPartType");
		}

		/// <summary>
		/// Returns the System.Data.DataSet used to fill this System.Windows.Forms.DataGrid.
		/// </summary>
		public System.Data.DataSet DataSet {

			get {

				return(this.dataSet);
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
		/// Releases this System.Windows.Forms.DataGrid.
		/// </summary>
		protected override void Dispose(bool disposing) {

			if (disposing) {

				if (this.sqlDataAdapter != null) {

					this.sqlDataAdapter = null;
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Initializes a new instance of the Bob.Windows.DataGrids.WinDataGrid_JobPartType class.
		/// </summary>
		public WinDataGrid_JobPartType() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public void Initialize(string connectionString) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.ConnectionString;

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPartType'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPartType", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened,
		/// it will be opened when used then closed again after the job is done. </param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection) {

			this.sqlConnection= sqlConnection;
			this.LastKnownConnectionType = Bob.DataClasses.ConnectionType.SqlConnection;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPartType'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPartType", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(-1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully
		/// call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.CreateControl();

			if (this.LastKnownConnectionType == Bob.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}


			switch (this.LastKnownConnectionType) {

 				case Bob.DataClasses.ConnectionType.ConnectionString:
					this.sqlDataAdapter = new Bob.SqlDataAdapters.SqlDataAdapter_JobPartType(this.connectionString, "JobPartType");
					break;

 				case Bob.DataClasses.ConnectionType.SqlConnection:
					this.sqlDataAdapter = new Bob.SqlDataAdapters.SqlDataAdapter_JobPartType(this.sqlConnection, "JobPartType");
					break;
			}

			this.dataSet = null;

			if (startRecord == -1 && maxRecords == -1) {

				this.sqlDataAdapter.FillDataSet(ref this.dataSet);
			}
			else {

				this.sqlDataAdapter.FillDataSet(ref this.dataSet, startRecord, maxRecords);
			}

			this.dataSet.Tables["JobPartType"].Columns["JobPartTypeId"].Caption = "JobPartTypeId (update this label in the \"Olymars/ColumnLabel\" extended property of the \"JobPartTypeId\" column)";
			this.dataSet.Tables["JobPartType"].Columns["Description"].Caption = "Description (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Description\" column)";
			this.dataSet.Tables["JobPartType"].Columns["GeneralUnitCost"].Caption = "GeneralUnitCost (update this label in the \"Olymars/ColumnLabel\" extended property of the \"GeneralUnitCost\" column)";

			this.bindingInProgress = true;
			this.DataSource = this.dataSet .Tables["JobPartType"].DefaultView;
			this.bindingInProgress = false;
		}
	}
}
