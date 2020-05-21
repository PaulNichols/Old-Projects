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
using SPs = Bob.DataClasses.StoredProcedures;
using Params = Bob.DataClasses.Parameters;
using Abstracts = Bob.AbstractClasses;

namespace Bob.Windows.CheckedListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.CheckedListBox class and was built to display the 'JobPart' table content.
	/// </summary>
#if OLYMARS_ATTRIBUTE || OLYMARS_DEBUG
	[Bob.DataClasses.OlymarsInformation(DeveloperName="<Developer Name Here>", GeneratedOn="2005/01/15 18:39:12", SqlObjectDependancyName="JobPart", SqlObjectDependancyRevision=656)]
#endif
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBox_JobPart), "WinCheckedListBox.bmp")]
	public class WinCheckedListBox_JobPart : System.Windows.Forms.CheckedListBox {

		private Params.spS_JobPart_Display param;
		private Bob.AbstractClasses.Abstract_JobPart oAbstract_JobPart;
		private System.Data.SqlTypes.SqlInt32 FK_JobId = System.Data.SqlTypes.SqlInt32.Null;
		private System.Data.SqlTypes.SqlInt32 FK_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;
		private bool bindingInProgress = false;
		private bool internalItemCheckStateUpdateInProgress = false;
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
		public Params.spS_JobPart_Display SP_Parameter {

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
		/// Returns the 'JobPart' abstract class so that you can access any table
		/// column for the current record selection. When the selection has changed, you need to call the
		/// RefreshCurrentRecord method first before accessing this property.
		/// </summary>
		public Bob.AbstractClasses.Abstract_JobPart Abstract_JobPart {

			get {

				return(this.oAbstract_JobPart);
			}
		}

		/// <summary>
		/// Returns the currently selected record primary key.
		/// </summary>
		public System.Data.SqlTypes.SqlInt32 SelectedRecordID {

			get {

				if (base.SelectedIndex == -1) {

					return(System.Data.SqlTypes.SqlInt32.Null);
				}

				return(new System.Data.SqlTypes.SqlInt32((System.Int32)((System.Data.DataRowView)this.SelectedItem).Row[0]));
			}
		}

		/// <summary>
		/// Returns an array of the currently checked records primary keys
		/// </summary>
		public System.Collections.ArrayList CheckedRecordsID {

			get {
						
				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(object currentItem in this.CheckedItems) {
				
					arrayList.Add(new System.Data.SqlTypes.SqlInt32((System.Int32)((System.Data.DataRowView)currentItem).Row[0]));
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

				this.oAbstract_JobPart = null;

				this.FK_JobId = System.Data.SqlTypes.SqlInt32.Null;
				this.FK_JobPartTypeId = System.Data.SqlTypes.SqlInt32.Null;

				if (this.param != null) {

					this.param.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the Bob.Windows.CheckedListBoxes.WinCheckedListBox_JobPart class.
		/// </summary>
		public WinCheckedListBox_JobPart() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		/// <param name="connectionString">A valid connection string to the database.</param>
		/// <param name="FK_JobId">Value for this foreign key.</param>
		/// <param name="FK_JobPartTypeId">Value for this foreign key.</param>
		public void Initialize(string connectionString, System.Data.SqlTypes.SqlInt32 FK_JobId, System.Data.SqlTypes.SqlInt32 FK_JobPartTypeId) {

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
					sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

					int CurrentRevision = (int)sqlCommand.ExecuteScalar();

					sqlConnection.Close();

					int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
					if (CurrentRevision != OriginalRevision) {

						throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
					}
				}
			}
#endif

			this.FK_JobId = FK_JobId;
			this.FK_JobPartTypeId = FK_JobPartTypeId;
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
		/// <param name="FK_JobId">Value for this foreign key.</param>
		/// <param name="FK_JobPartTypeId">Value for this foreign key.</param>
		public void Initialize(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlTypes.SqlInt32 FK_JobId, System.Data.SqlTypes.SqlInt32 FK_JobPartTypeId) {

			if (sqlConnection == null) {

				throw new ArgumentNullException("sqlConnection", "Invalid connection!");
			}

			this.sqlConnection = sqlConnection;
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
				sqlCommand.CommandText = "Select sysobjects.schema_ver from sysobjects where sysobjects.name = 'JobPart'";

				int CurrentRevision = (int)sqlCommand.ExecuteScalar();

				if (NotAlreadyOpened) {

					sqlConnection.Close();
				}

				int OriginalRevision = ((Bob.DataClasses.OlymarsInformationAttribute)System.Attribute.GetCustomAttribute(this.GetType(), typeof(Bob.DataClasses.OlymarsInformationAttribute), false)).SqlObjectDependancyRevision;
				if (CurrentRevision != OriginalRevision) {

					throw new System.InvalidOperationException(System.String.Format("OLYMARS: This code is not in sync anymore with [{0}]. It was generated when [{0}] version was: {2}. Current [{0}] version is: {1}{3}{3}You can either regenerate the code for this class so that it will be based on the new version or edit the configuration file of the class caller application and paste the following code:{3}{3}<?xml version=\"1.0\" encoding=\"utf-8\" ?>{3}<configuration>{3}\t<appSettings>{3}\t\t<add key=\"OlymarsDebugCheck\" value=\"False\" />{3}\t</appSettings>{3}</configuration>{3}{3}You will need to reload the caller application if it is a Windows Forms based application.", "JobPart", CurrentRevision, OriginalRevision, System.Environment.NewLine));
				}
			}
#endif

			this.FK_JobId = FK_JobId;
			this.FK_JobPartTypeId = FK_JobPartTypeId;
		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new System.Data.SqlTypes.SqlInt32[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new System.Data.SqlTypes.SqlInt32[0], startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_JobPartId) {

			this.RefreshData(ArrayOf_PK_JobPartId, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_JobPartId, int startRecord, int maxRecords) {

			if (ArrayOf_PK_JobPartId != null && ArrayOf_PK_JobPartId.Count > 0) {

				System.Data.SqlTypes.SqlInt32[] typedArray = new System.Data.SqlTypes.SqlInt32[ArrayOf_PK_JobPartId.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_JobPartId) {

					if (item is System.Data.SqlTypes.SqlInt32) {

						typedArray[index] = (System.Data.SqlTypes.SqlInt32)item;
						index++;
					}
					else if (item is System.Int32) {

						typedArray[index] = (System.Int32)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_JobPartId does not contain ONLY System.Data.SqlTypes.SqlInt32 or System.Int32 elements.");
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
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Data.SqlTypes.SqlInt32[] ArrayOf_PK_JobPartId) {

			this.RefreshData(ArrayOf_PK_JobPartId, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Data.SqlTypes.SqlInt32[] ArrayOf_PK_JobPartId, int startRecord, int maxRecords) {

		this.CreateControl();

			this.param = new Params.spS_JobPart_Display(true);

			switch (this.LastKnownConnectionType) {

 				case Bob.DataClasses.ConnectionType.ConnectionString:
					this.param.SetUpConnection(this.connectionString);
					break;

 				case Bob.DataClasses.ConnectionType.SqlConnection:
					this.param.SetUpConnection(this.sqlConnection);
					break;

				default:
					throw new InvalidOperationException("This control has not been initialized. You must call the Initialize method before calling this method.");
			}

			this.param.CommandTimeOut = this.commandTimeOut;
			this.param.Param_JobId = this.FK_JobId;
			this.param.Param_JobPartTypeId = this.FK_JobPartTypeId;

			System.Data.DataSet DS = null;
			
			SPs.spS_JobPart_Display SP = new SPs.spS_JobPart_Display(false);

			if (SP.Execute(ref this.param, ref DS, startRecord, maxRecords)) {

				this.BeginUpdate();
				this.bindingInProgress = true;
				this.DataSource = DS.Tables["spS_JobPart_Display"].DefaultView;
				this.ValueMember = SPs.spS_JobPart_Display.Resultset1.Fields.Column_ID1.ColumnName;
				this.DisplayMember = SPs.spS_JobPart_Display.Resultset1.Fields.Column_Display.ColumnName;
				this.bindingInProgress = false;

				System.Data.DataView dataView = DS.Tables["spS_JobPart_Display"].DefaultView;

				if (ArrayOf_PK_JobPartId != null && ArrayOf_PK_JobPartId.Length > 0) {

					this.SetRecordsCheckState(ArrayOf_PK_JobPartId, System.Windows.Forms.CheckState.Checked);
				}
				else {

					base.OnSelectedIndexChanged(EventArgs.Empty);
				}
				this.EndUpdate();
				SP.Dispose();
			}
			else {

				SP.Dispose();
				throw new Bob.DataClasses.CustomException(this.param, "WinCheckedListBox_JobPart : System.Windows.Forms.CheckedListBox", "RefreshData");
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
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Collections.ArrayList ArrayOf_PK_JobPartId, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_JobPartId != null && ArrayOf_PK_JobPartId.Count > 0) {

				System.Data.SqlTypes.SqlInt32[] typedArray = new System.Data.SqlTypes.SqlInt32[ArrayOf_PK_JobPartId.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_JobPartId) {

					if (item is System.Data.SqlTypes.SqlInt32) {

						typedArray[index] = (System.Data.SqlTypes.SqlInt32)item;
						index++;
					}
					else if (item is System.Int32) {

						typedArray[index] = (System.Int32)item;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_JobPartId does not contain ONLY System.Data.SqlTypes.SqlInt32 or System.Int32 elements.");
					}
				}
				this.SetRecordsCheckState(typedArray, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PK_JobPartId">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Data.SqlTypes.SqlInt32[] ArrayOf_PK_JobPartId, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_JobPartId != null && ArrayOf_PK_JobPartId.Length > 0) {

				int PrimaryKeysWereFound = 0;
				int TotalPrimaryKeysNumber = ArrayOf_PK_JobPartId.Length;
				int TotalDataRowViewsNumber = this.Items.Count;

				this.internalItemCheckStateUpdateInProgress = true;

				for (int Index = 0; Index < TotalDataRowViewsNumber; Index++) {

					System.Data.DataRowView dataRowView = (System.Data.DataRowView)this.Items[Index];
					System.Data.SqlTypes.SqlInt32 CurrentPrimaryKey = new System.Data.SqlTypes.SqlInt32((Int32)dataRowView.Row[SPs.spS_JobPart_Display.Resultset1.Fields.Column_ID1.ColumnName]);

					if (System.Array.IndexOf(ArrayOf_PK_JobPartId, CurrentPrimaryKey) != -1) {

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
		/// Loads the JobPart abstract class for the current selected record primary key.
		/// </summary>
		/// <returns>true if the call succeeded; false, otherwise.</returns>
		public bool RefreshCurrentRecord() {

			if (this.SelectedIndex == -1) {

				if (oAbstract_JobPart != null) {

					oAbstract_JobPart.Reset();
				}
				return(false);
			}

			System.Data.SqlTypes.SqlInt32 PK_JobPartId = (System.Int32)this.SelectedValue;

			if (this.oAbstract_JobPart == null) {

				switch (this.LastKnownConnectionType) {

					case Bob.DataClasses.ConnectionType.ConnectionString:
							this.oAbstract_JobPart = new Bob.AbstractClasses.Abstract_JobPart(this.connectionString);
						break;

					case Bob.DataClasses.ConnectionType.SqlConnection:
							this.oAbstract_JobPart = new Bob.AbstractClasses.Abstract_JobPart(this.sqlConnection);
						break;
				}
			}

			return(this.oAbstract_JobPart.Refresh(PK_JobPartId));
		}
	}
}
