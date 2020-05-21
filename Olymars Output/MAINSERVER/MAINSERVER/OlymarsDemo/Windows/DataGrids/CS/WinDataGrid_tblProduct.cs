﻿/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1287.20792

			Generation Date: 28/11/2004 15:05:22
			Generator name: <Developer Name Here>
			Template last update: 24/06/2002 01:53:57
			Template revision: 15083637

			SQL Server version: 08.00.0194
			Server: MAINSERVER\MAINSERVER
			Database: [OlymarsDemo]

	WARNING: This source is provided "AS IS" without warranty of any kind.
	The author disclaims all warranties, either express or implied, including
	the warranties of merchantability and fitness for a particular purpose.
	In no event shall the author or its suppliers be liable for any damages
	whatsoever including direct, indirect, incidental, consequential, loss of
	business profits or special damages, even if the author or its suppliers
	have been advised of the possibility of such damages.
*/

using System;

namespace OlymarsDemo.Windows.DataGrids {

	/// <summary>
	/// This class is based on a System.Windows.Forms.DataGrid class and was built to display the 'tblProduct' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:22", SqlObjectDependancyName="tblProduct", SqlObjectDependancyRevision=816)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinDataGrid_tblProduct), "WinDataGrid.bmp")]
	public sealed class WinDataGrid_tblProduct : System.Windows.Forms.DataGrid {

			private System.Data.DataSet dataSet = null;
		private OlymarsDemo.SqlDataAdapters.SqlDataAdapter_tblProduct sqlDataAdapter = null;
		private System.Data.SqlTypes.SqlInt32 FK_Pro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
		private bool bindingInProgress = false;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private OlymarsDemo.DataClasses.ConnectionType LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;

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
		public OlymarsDemo.SqlDataAdapters.SqlDataAdapter_tblProduct SqlDataAdapter {

			get {

				return(this.sqlDataAdapter);
			}
		}

		/// <summary>
		/// Returns the current type of connection that is going or has been used
		/// against the Sql Server database. It can be: ConnectionString or SqlConnection.
		/// </summary>
		public OlymarsDemo.DataClasses.ConnectionType ConnectionType {

			get {

				return(this.LastKnownConnectionType);
			}
		}

		/// <summary>
		/// Commit all the changes that have been made in this System.Windows.Forms.DataGrid.
		/// </summary>
		public void CommitChanges() {

			this.sqlDataAdapter.SqlDataAdapter.Update(this.dataSet, "tblProduct");
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
		/// Releases this System.Windows.Forms.DataGrid.
		/// </summary>
		protected override void Dispose(bool disposing) {

			if (disposing) {

				if (this.sqlDataAdapter != null) {

					this.sqlDataAdapter = null;
				}
				this.FK_Pro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Initializes a new instance of the OlymarsDemo.Windows.DataGrids.WinDataGrid_tblProduct class.
		/// </summary>
		public WinDataGrid_tblProduct() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// or only a subset (based on its foreign keys).
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="FK_Pro_LngCategoryID">Value for this foreign key.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlInt32 FK_Pro_LngCategoryID) {

			if (connectionString == null) {

				throw new ArgumentNullException("connectionString", "connectionString can be an empty string but can not be null.");
			}

			this.connectionString = connectionString;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.ConnectionString;

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblProduct'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblProduct", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.FK_Pro_LngCategoryID = FK_Pro_LngCategoryID;
		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the SQL Server database and if you want to populate the whole table content
		/// or only a subset (based on its foreign keys).
		/// </summary>
		/// <param name="sqlConnection">A valid SqlConnection object. If it is not opened,
		/// it will be opened when used then closed again after the job is done. </param>
		/// <param name="FK_Pro_LngCategoryID">Value for this foreign key.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 FK_Pro_LngCategoryID) {

			this.sqlConnection= sqlConnection;
			this.LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.SqlConnection;

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblProduct'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblProduct", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.FK_Pro_LngCategoryID = FK_Pro_LngCategoryID;
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

			if (this.LastKnownConnectionType == OlymarsDemo.DataClasses.ConnectionType.None) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}


			switch (this.LastKnownConnectionType) {

 				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					this.sqlDataAdapter = new OlymarsDemo.SqlDataAdapters.SqlDataAdapter_tblProduct(this.connectionString, this.FK_Pro_LngCategoryID, "tblProduct");
					break;

 				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					this.sqlDataAdapter = new OlymarsDemo.SqlDataAdapters.SqlDataAdapter_tblProduct(this.sqlConnection, this.FK_Pro_LngCategoryID, "tblProduct");
					break;
			}

			this.dataSet = null;

			if (startRecord == -1 && maxRecords == -1) {

				this.sqlDataAdapter.FillDataSet(ref this.dataSet);
			}
			else {

				this.sqlDataAdapter.FillDataSet(ref this.dataSet, startRecord, maxRecords);
			}

			this.dataSet.Tables["tblProduct"].Columns["Pro_GuidID"].Caption = "Pro_GuidID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_GuidID\" column)";
			this.dataSet.Tables["tblProduct"].Columns["Pro_StrName"].Caption = "Pro_StrName (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_StrName\" column)";
			this.dataSet.Tables["tblProduct"].Columns["Pro_CurPrice"].Caption = "Pro_CurPrice (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_CurPrice\" column)";
			this.dataSet.Tables["tblProduct"].Columns["Pro_LngCategoryID"].Caption = "Pro_LngCategoryID (update this label in the \"Olymars/ColumnLabel\" extended property of the \"Pro_LngCategoryID\" column)";

			this.bindingInProgress = true;
			this.DataSource = this.dataSet .Tables["tblProduct"].DefaultView;
			this.bindingInProgress = false;
		}
	}
}
