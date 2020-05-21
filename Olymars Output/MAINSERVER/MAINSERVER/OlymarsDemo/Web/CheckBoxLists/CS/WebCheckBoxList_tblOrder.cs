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

namespace OlymarsDemo.Web.CheckBoxLists {

	/// <summary>
	/// This class derives from System.Web.UI.WebControls.CheckBoxList class and was built to display the 'tblOrder' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:22", SqlObjectDependancyName="tblOrder", SqlObjectDependancyRevision=800)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WebCheckBoxList_tblOrder), "WebCheckBoxList.bmp")]
	public sealed class WebCheckBoxList_tblOrder : System.Web.UI.WebControls.CheckBoxList {

		private Params.spS_tblOrder_Display param;
		private OlymarsDemo.AbstractClasses.Abstract_tblOrder oAbstract_tblOrder;
		private System.Data.SqlTypes.SqlInt32 FK_Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
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
		public Params.spS_tblOrder_Display SP_Parameter {

			get {

				return(this.param);
			}
		}

		/// <summary>
		/// Returns the 'tblOrder' abstract class so that you can access any table
		/// column for the current record selection. When the selection has changed, you need to call the
		/// RefreshCurrentRecord method first before accessing this property.
		/// </summary>
		public OlymarsDemo.AbstractClasses.Abstract_tblOrder Abstract_tblOrder {

			get {

				return(this.oAbstract_tblOrder);
			}
		}

		/// <summary>
		/// Returns an array of the currently checked records primary keys.
		/// </summary>
		public System.Collections.ArrayList CheckedRecordsID {

			get {
						
				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(System.Web.UI.WebControls.ListItem currentItem in this.Items) {
				
					if (currentItem.Selected) {
					
						arrayList.Add(new Guid(currentItem.Value));
					}
				}
				
				return(arrayList);
			}
		}

		/// <summary>
		/// Disposes the current instance of this object.
		/// </summary>
		public override void Dispose() {

			this.oAbstract_tblOrder = null;

			this.FK_Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;

			if (this.param != null) {

				this.param.Dispose();
			}

			base.Dispose();
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Web.CheckBoxLists.WebCheckBoxList_tblOrder class.
		/// </summary>
		public WebCheckBoxList_tblOrder() : base() {

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
		/// <param name="FK_Ord_LngCustomerID">Value for this foreign key.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlInt32 FK_Ord_LngCustomerID) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrder'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrder", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.FK_Ord_LngCustomerID = FK_Ord_LngCustomerID;
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
		/// <param name="FK_Ord_LngCustomerID">Value for this foreign key.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 FK_Ord_LngCustomerID) {

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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrder'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrder", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.FK_Ord_LngCustomerID = FK_Ord_LngCustomerID;
		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new System.Data.SqlTypes.SqlGuid[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new System.Data.SqlTypes.SqlGuid[0], startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Ord_GuidID) {

			this.RefreshData(ArrayOf_PK_Ord_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Ord_GuidID, int startRecord, int maxRecords) {

			if (ArrayOf_PK_Ord_GuidID != null && ArrayOf_PK_Ord_GuidID.Count > 0) {

				System.Data.SqlTypes.SqlGuid[] typedArray = new System.Data.SqlTypes.SqlGuid[ArrayOf_PK_Ord_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Ord_GuidID) {

					if (item is System.Data.SqlTypes.SqlGuid) {

						typedArray[index] = (System.Data.SqlTypes.SqlGuid)item;
						index++;
					}
					else if (item is System.Guid) {

						typedArray[index] = (System.Guid)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Ord_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid or System.Guid elements.");
					}
				}
				this.RefreshData(typedArray, startRecord, maxRecords);
			}
			else {

				this.RefreshData(startRecord, maxRecords);
			}
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Ord_GuidID) {

			this.RefreshData(ArrayOf_PK_Ord_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Ord_GuidID, int startRecord, int maxRecords) {

			this.param = new Params.spS_tblOrder_Display(true);

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

			this.param.Param_Ord_LngCustomerID = this.FK_Ord_LngCustomerID;

			System.Data.DataSet DS = null;
			
			SPs.spS_tblOrder_Display SP = new SPs.spS_tblOrder_Display(false);

			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.DataSource = DS.Tables["spS_tblOrder_Display"].DefaultView;
				this.DataValueField = SPs.spS_tblOrder_Display.Resultset1.Fields.Column_ID1.ColumnName;
				this.DataTextField = SPs.spS_tblOrder_Display.Resultset1.Fields.Column_Display.ColumnName;
				if (this.doDataBindAfterRefreshData) {

					this.DataBind();
				}

				if (ArrayOf_PK_Ord_GuidID != null) {

					this.SetRecordsChecked(ArrayOf_PK_Ord_GuidID, true);
				}
			}
			else {

				SP.Dispose();
				throw new OlymarsDemo.DataClasses.CustomException(this.param, "WebCheckBoxList_tblOrder : System.Web.UI.WebControls.CheckBoxList", "RefreshData");
			}

		}
		/// <summary>
		/// Sets the supplied collection records to a given selected status.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">True if the records must be checked. Otherwise False.</param>
		public void SetRecordsChecked(System.Collections.ArrayList ArrayOf_PK_Ord_GuidID, bool value) {
		
			if (ArrayOf_PK_Ord_GuidID != null && ArrayOf_PK_Ord_GuidID.Count > 0) {

				System.Data.SqlTypes.SqlGuid[] typedArray = new System.Data.SqlTypes.SqlGuid[ArrayOf_PK_Ord_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Ord_GuidID) {

					if (item is System.Data.SqlTypes.SqlGuid) {

						typedArray[index] = (System.Data.SqlTypes.SqlGuid)item;
						index++;
					}
					else if (item is System.Guid) {

						typedArray[index] = (System.Guid)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Ord_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid or System.Guid elements.");
					}
				}
				this.SetRecordsChecked(typedArray, value);
			}
		}

		/// <summary>
		/// Sets the supplied collection records to a given selected status.
		/// </summary>
		/// <param name="ArrayOf_PK_Ord_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">True if the records must be checked. Otherwise False.</param>
		public void SetRecordsChecked(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Ord_GuidID, bool value) {

			this.ClearSelection();

			if (ArrayOf_PK_Ord_GuidID != null) {

				foreach (System.Data.SqlTypes.SqlGuid PK_Ord_GuidID in ArrayOf_PK_Ord_GuidID) {

					System.Web.UI.WebControls.ListItem listItem = this.Items.FindByValue(PK_Ord_GuidID.Value.ToString());
					if (listItem != null) {

						listItem.Selected = value;
					}
				}
			}
		}


		/// <summary>
		/// Loads the tblOrder abstract class for the current selected record primary key.
		/// </summary>
		/// <returns>true if the call succeeded; false, otherwise.</returns>
		public bool RefreshCurrentRecord() {

			if (this.SelectedIndex == -1) {

				if (oAbstract_tblOrder != null) {

					oAbstract_tblOrder.Reset();
				}
				return(false);
			}

			System.Data.SqlTypes.SqlGuid PK_Ord_GuidID = new Guid(this.SelectedItem.Value);

			if (this.oAbstract_tblOrder == null) {

				switch (this.LastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.oAbstract_tblOrder = new OlymarsDemo.AbstractClasses.Abstract_tblOrder(this.connectionString);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.oAbstract_tblOrder = new OlymarsDemo.AbstractClasses.Abstract_tblOrder(this.sqlConnection);
						break;
				}
			}

			return(this.oAbstract_tblOrder.Refresh(PK_Ord_GuidID));
		}
	}
}
