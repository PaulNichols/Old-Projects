/*
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
using SPs = OlymarsDemo.DataClasses.StoredProcedures;
using Params = OlymarsDemo.DataClasses.Parameters;
using Abstracts = OlymarsDemo.AbstractClasses;

namespace OlymarsDemo.Web.ListBoxes {

	/// <summary>
	/// This class derives from System.Web.UI.WebControls.ListBox class and was built to display the 'tblCustomer' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:22", SqlObjectDependancyName="tblCustomer", SqlObjectDependancyRevision=736)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WebListBox_tblCustomer), "WebListBox.bmp")]
	public sealed class WebListBox_tblCustomer : System.Web.UI.WebControls.ListBox {

		private Params.spS_tblCustomer_Display param;
		private OlymarsDemo.AbstractClasses.Abstract_tblCustomer oAbstract_tblCustomer;
		private string connectionString;
		private System.Data.SqlClient.SqlConnection sqlConnection;
		private OlymarsDemo.DataClasses.ConnectionType LastKnownConnectionType = OlymarsDemo.DataClasses.ConnectionType.None;
		private bool doDataBindAfterRefreshData = true;

		/// <summary>
		/// Returns the current type of connection that is going or has been used against the Sql Server database.
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
		public Params.spS_tblCustomer_Display SP_Parameter {

			get {

				return(this.param);
			}
		}

		/// <summary>
		/// Returns the 'tblCustomer' abstract class so that you can access any table
		/// column for the current record selection. When the selection has changed, you need to call the
		/// RefreshCurrentRecord method first before accessing this property.
		/// </summary>
		public OlymarsDemo.AbstractClasses.Abstract_tblCustomer Abstract_tblCustomer {

			get {

				return(this.oAbstract_tblCustomer);
			}
		}

		/// <summary>
		/// Returns the currently selected record primary key.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 SelectedRecordID {

			get {

				return(Convert.ToInt32(this.SelectedItem.Value));
			}
		}

		/// <summary>
		/// Returns an array of the currently selected records primary keys. This is only
		/// available when SelectionMode=Multiple
		/// </summary>
		public System.Collections.ArrayList SelectedRecordsID {

			get {
						
				if (this.SelectionMode != System.Web.UI.WebControls.ListSelectionMode.Multiple) {

					throw new InvalidOperationException("Not available given the current selection behavior (SelectionMode property) of this control.");
				}

				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(System.Web.UI.WebControls.ListItem currentItem in this.Items) {
				
					if (currentItem.Selected) {
					
						arrayList.Add(Convert.ToInt32(currentItem.Value));
					}
				}
				
				return(arrayList);
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public override void Dispose() {

			this.oAbstract_tblCustomer = null;


			if (this.param != null) {

				this.param.Dispose();
			}

			base.Dispose();
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Web.ListBoxes.WebListBox_tblCustomer class.
		/// </summary>
		public WebListBox_tblCustomer() : base() {

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
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		public void Initialize(string connectionString) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblCustomer'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		/// <param name="sqlConnection">
		/// A valid System.Data.SqlClient.SqlConnection object. If it is not opened, it will be
		/// opened when used then closed again after the job is done.
		/// </param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection) {

			if (sqlConnection == null) {

				throw new ArgumentNullException("sqlConnection", "Invalid connection!");
			}

			this.sqlConnection = sqlConnection;
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblCustomer'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblCustomer", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(System.Data.SqlTypes.SqlInt32.Null, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(System.Data.SqlTypes.SqlInt32.Null, startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which record you want to be selected by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Cus_LngID">Primary key of the record you want to be selected by default.</param>
		public void RefreshData(System.Data.SqlTypes.SqlInt32 PK_Cus_LngID) {

			this.RefreshData(PK_Cus_LngID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be selected
		/// by default. In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Cus_LngID">Primary key of the record you want to be selected by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Data.SqlTypes.SqlInt32 PK_Cus_LngID, int startRecord, int maxRecords) {

			this.param = new Params.spS_tblCustomer_Display(true);

			switch (this.LastKnownConnectionType) {

 				case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;

				default:
					throw new InvalidOperationException("This control has not been initialized. You must call the Initialize method before calling this method.");
			}


			System.Data.DataSet DS = null;
			
			SPs.spS_tblCustomer_Display SP = new SPs.spS_tblCustomer_Display(false);

			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.DataSource = DS.Tables["spS_tblCustomer_Display"].DefaultView;
				this.DataValueField = SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_ID1.ColumnName;
				this.DataTextField = SPs.spS_tblCustomer_Display.Resultset1.Fields.Column_Display.ColumnName;
				if (this.doDataBindAfterRefreshData) {

					this.DataBind();
				}

				if (!PK_Cus_LngID.IsNull) {

					System.Web.UI.WebControls.ListItem listItem = this.Items.FindByValue(PK_Cus_LngID.Value.ToString());
					if (listItem != null) {

						listItem.Selected = true;
					}
				}

			}
			else {

				SP.Dispose();
				throw new OlymarsDemo.DataClasses.CustomException(this.param, "WebListBox_tblCustomer : System.Web.UI.WebControls.ListBox", "RefreshData");
			}

		}


		/// <summary>
		/// Loads the tblCustomer abstract class for the current selected record primary key.
		/// </summary>
		/// <returns>true if the call succeeded; false, otherwise.</returns>
		public bool RefreshCurrentRecord() {

			if (this.SelectedIndex == -1) {

				if (oAbstract_tblCustomer != null) {

					oAbstract_tblCustomer.Reset();
				}
				return(false);
			}

			System.Data.SqlTypes.SqlInt32 PK_Cus_LngID = Convert.ToInt32(this.SelectedItem.Value);

			if (this.oAbstract_tblCustomer == null) {

				switch (this.LastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.oAbstract_tblCustomer = new OlymarsDemo.AbstractClasses.Abstract_tblCustomer(this.connectionString);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.oAbstract_tblCustomer = new OlymarsDemo.AbstractClasses.Abstract_tblCustomer(this.sqlConnection);
						break;
				}
			}

			return(this.oAbstract_tblCustomer.Refresh(PK_Cus_LngID));
		}
	}
}
