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

namespace OlymarsDemo.Windows.CheckedListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.CheckedListBox class and was built to display the 'tblOrderItem' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[OlymarsDemo.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2004/11/28 15:05:22", SqlObjectDependancyName="tblOrderItem", SqlObjectDependancyRevision=848)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBox_tblOrderItem), "WinCheckedListBox.bmp")]
	public sealed class WinCheckedListBox_tblOrderItem : System.Windows.Forms.CheckedListBox {

		private Params.spS_tblOrderItem_Display param;
		private OlymarsDemo.AbstractClasses.Abstract_tblOrderItem oAbstract_tblOrderItem;
		private System.Data.SqlTypes.SqlGuid FK_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
		private System.Data.SqlTypes.SqlGuid FK_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
		private bool bindingInProgress = false;
		private bool internalItemCheckStateUpdateInProgress = false;
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
		/// Returns True if an internal item CheckState update operation is in progress.
		/// </summary>
		public bool InternalItemCheckStateUpdateInProgress {

			get {

				return(this.internalItemCheckStateUpdateInProgress);
			}
		}

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
		public Params.spS_tblOrderItem_Display SP_Parameter {

			get {

				return(this.param);
			}
		}

		/// <summary>
		/// Returns the 'tblOrderItem' abstract class so that you can access any table
		/// column for the current record selection. When the selection has changed, you need to call the
		/// RefreshCurrentRecord method first before accessing this property.
		/// </summary>
		public OlymarsDemo.AbstractClasses.Abstract_tblOrderItem Abstract_tblOrderItem {

			get {

				return(this.oAbstract_tblOrderItem);
			}
		}

		/// <summary>
		/// Returns the currently selected record primary key.
		/// </summary>
		public System.Data.SqlTypes.SqlGuid SelectedRecordID {

			get {

				if (base.SelectedIndex == -1) {

					return(System.Data.SqlTypes.SqlGuid.Null);
				}

				return(new System.Data.SqlTypes.SqlGuid((System.Guid)((System.Data.DataRowView)this.SelectedItem).Row[0]));
			}
		}

		/// <summary>
		/// Returns an array of the currently checked records primary keys
		/// </summary>
		public System.Collections.ArrayList CheckedRecordsID {

			get {
						
				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(object currentItem in this.CheckedItems) {
				
					arrayList.Add(new System.Data.SqlTypes.SqlGuid((System.Guid)((System.Data.DataRowView)currentItem).Row[0]));
				}
				
				return(arrayList);
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

				this.oAbstract_tblOrderItem = null;

				this.FK_Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
				this.FK_Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;

				if (this.param != null) {

					this.param.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblOrderItem class.
		/// </summary>
		public WinCheckedListBox_tblOrderItem() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="FK_Oit_GuidOrderID">Value for this foreign key.</param>
		/// <param name="FK_Oit_GuidProductID">Value for this foreign key.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlGuid FK_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid FK_Oit_GuidProductID) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'tblOrderItem'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((OlymarsDemo.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(OlymarsDemo.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "tblOrderItem", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.FK_Oit_GuidOrderID = FK_Oit_GuidOrderID;
			this.FK_Oit_GuidProductID = FK_Oit_GuidProductID;
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
		/// <param name="FK_Oit_GuidOrderID">Value for this foreign key.</param>
		/// <param name="FK_Oit_GuidProductID">Value for this foreign key.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlGuid FK_Oit_GuidOrderID, System.Data.SqlTypes.SqlGuid FK_Oit_GuidProductID) {

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

			this.FK_Oit_GuidOrderID = FK_Oit_GuidOrderID;
			this.FK_Oit_GuidProductID = FK_Oit_GuidProductID;
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
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Oit_GuidID) {

			this.RefreshData(ArrayOf_PK_Oit_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Oit_GuidID, int startRecord, int maxRecords) {

			if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Count > 0) {

				System.Data.SqlTypes.SqlGuid[] typedArray = new System.Data.SqlTypes.SqlGuid[ArrayOf_PK_Oit_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Oit_GuidID) {

					if (item is System.Data.SqlTypes.SqlGuid) {

						typedArray[index] = (System.Data.SqlTypes.SqlGuid)item;
						index++;
					}
					else if (item is System.Guid) {

						typedArray[index] = (System.Guid)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Oit_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid or System.Guid elements.");
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
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Oit_GuidID) {

			this.RefreshData(ArrayOf_PK_Oit_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Oit_GuidID, int startRecord, int maxRecords) {

			this.param = new Params.spS_tblOrderItem_Display(true);

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

			this.param.Param_Oit_GuidOrderID = this.FK_Oit_GuidOrderID;
			this.param.Param_Oit_GuidProductID = this.FK_Oit_GuidProductID;

			System.Data.DataSet DS = null;
			
			SPs.spS_tblOrderItem_Display SP = new SPs.spS_tblOrderItem_Display(false);

			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.BeginUpdate();
				this.bindingInProgress = true;
				this.DataSource = DS.Tables["spS_tblOrderItem_Display"].DefaultView;
				this.ValueMember = SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_ID1.ColumnName;
				this.DisplayMember = SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_Display.ColumnName;
				this.bindingInProgress = false;

				System.Data.DataView dataView = DS.Tables["spS_tblOrderItem_Display"].DefaultView;

				if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Length > 0) {

					this.SetRecordsCheckState(ArrayOf_PK_Oit_GuidID, System.Windows.Forms.CheckState.Checked);
				}
				else {

					base.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.EndUpdate();
				SP.Dispose();
			}
			else {

				SP.Dispose();
				throw new OlymarsDemo.DataClasses.CustomException(this.param, "WinCheckedListBox_tblOrderItem : System.Windows.Forms.CheckedListBox", "RefreshData");
			}

		}

		/// <summary>
		/// Sets all records to a given System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetAllRecordsCheckState(System.Windows.Forms.CheckState value) {

			for (int index=0; index < this.Items.Count; index++) {
			
				this.SetItemCheckState(index, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Collections.ArrayList ArrayOf_PK_Oit_GuidID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Count > 0) {

				System.Data.SqlTypes.SqlGuid[] typedArray = new System.Data.SqlTypes.SqlGuid[ArrayOf_PK_Oit_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Oit_GuidID) {

					if (item is System.Data.SqlTypes.SqlGuid) {

						typedArray[index] = (System.Data.SqlTypes.SqlGuid)item;
						index++;
					}
					else if (item is System.Guid) {

						typedArray[index] = (System.Guid)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Oit_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid or System.Guid elements.");
					}
				}
				this.SetRecordsCheckState(typedArray, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Data.SqlTypes.SqlGuid[] ArrayOf_PK_Oit_GuidID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Length > 0) {

				int PrimaryKeysWereFound = 0;
				int TotalPrimaryKeysNumber = ArrayOf_PK_Oit_GuidID.Length;
				int TotalDataRowViewsNumber = this.Items.Count;

				this.internalItemCheckStateUpdateInProgress = true;

				for (int Index = 0; Index < TotalDataRowViewsNumber; Index++) {

					System.Data.DataRowView dataRowView = (System.Data.DataRowView)this.Items[Index];
					System.Data.SqlTypes.SqlGuid CurrentPrimaryKey = new System.Data.SqlTypes.SqlGuid((Guid)dataRowView.Row[SPs.spS_tblOrderItem_Display.Resultset1.Fields.Column_ID1.ColumnName]);

					if (System.Array.IndexOf(ArrayOf_PK_Oit_GuidID, CurrentPrimaryKey) != -1) {

						this.SetItemChecked(Index, true);
						PrimaryKeysWereFound ++;

						if (TotalPrimaryKeysNumber == PrimaryKeysWereFound) {

							break;
						}
					}
				}

				this.internalItemCheckStateUpdateInProgress = false;
			}
		}


		/// <summary>
		/// Loads the tblOrderItem abstract class for the current selected record primary key.
		/// </summary>
		/// <returns>true if the call succeeded; false, otherwise.</returns>
		public bool RefreshCurrentRecord() {

			if (this.SelectedIndex == -1) {

				if (oAbstract_tblOrderItem != null) {

					oAbstract_tblOrderItem.Reset();
				}
				return(false);
			}

			System.Data.SqlTypes.SqlGuid PK_Oit_GuidID = (System.Guid)this.SelectedValue;

			if (this.oAbstract_tblOrderItem == null) {

				switch (this.LastKnownConnectionType) {

					case OlymarsDemo.DataClasses.ConnectionType.ConnectionString:
							this.oAbstract_tblOrderItem = new OlymarsDemo.AbstractClasses.Abstract_tblOrderItem(this.connectionString);
						break;

					case OlymarsDemo.DataClasses.ConnectionType.SqlConnection:
							this.oAbstract_tblOrderItem = new OlymarsDemo.AbstractClasses.Abstract_tblOrderItem(this.sqlConnection);
						break;
				}
			}

			return(this.oAbstract_tblOrderItem.Refresh(PK_Oit_GuidID));
		}
	}
}
