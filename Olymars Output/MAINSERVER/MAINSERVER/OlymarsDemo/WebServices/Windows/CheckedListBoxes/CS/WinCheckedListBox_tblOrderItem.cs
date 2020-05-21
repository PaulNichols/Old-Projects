/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:18
			Generator name: MAINSERVER\Administrator
			Template last update: 27/12/2004 16:39:29
			Template revision: 324

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
using System.Configuration;
using WS_tblOrderItem_Record = OlymarsDemo_WebServicesClient.WS_tblOrderItem_Record;

namespace OlymarsDemo.WebServices.Windows.CheckedListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.CheckedListBox class and was built to display the 'tblOrderItem' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBox_tblOrderItem), "WinCheckedListBox.bmp")]
	public sealed class WinCheckedListBox_tblOrderItem : System.Windows.Forms.CheckedListBox {

		private WS_tblOrderItem_Record.tblOrderItem_Record tblOrderItem_RecordWebService = null;
		private System.Net.NetworkCredential networkCredential = null;

		private WS_tblOrderItem_Record.WSGuid FK_Oit_GuidOrderID = null;
		private WS_tblOrderItem_Record.WSGuid FK_Oit_GuidProductID = null;

		private bool bindingInProgress = false;
		private bool internalItemCheckStateUpdateInProgress = false;

		/// <summary>
		/// Returns true if binding operation is in progress.
		/// </summary>
		public bool BindingInProgress {

			get {

				return(this.bindingInProgress);
			}
		}

		/// <summary>
		/// Returns true if an internal item CheckState update operation is in progress.
		/// </summary>
		public bool InternalItemCheckStateUpdateInProgress {

			get {

				return(this.internalItemCheckStateUpdateInProgress);
			}
		}

		/// <summary>
		/// Returns the currently selected record primary key.
		/// </summary>
		public WS_tblOrderItem_Record.WSGuid SelectedRecordID {

			get {

				WS_tblOrderItem_Record.WSGuid value = new WS_tblOrderItem_Record.WSGuid();

				if (base.SelectedIndex == -1) {

					value.UseNull = true;

					return(value);
				}

				value.UseNull = false;
				value.Value = (System.Guid)((System.Data.DataRowView)this.SelectedItem).Row[0];

				return(value);
			}
		}

		/// <summary>
		/// Returns an array of the currently checked records primary keys
		/// </summary>
		public System.Collections.ArrayList CheckedRecordsID {

			get {
						
				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(object currentItem in this.CheckedItems) {
				
					WS_tblOrderItem_Record.WSGuid record = new WS_tblOrderItem_Record.WSGuid();
					record.UseNull = false;
					record.Value = (System.Guid)((System.Data.DataRowView)currentItem).Row[0];

					arrayList.Add(record);
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

				this.FK_Oit_GuidOrderID = null;
				this.FK_Oit_GuidProductID = null;
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
		/// <param name="FK_Oit_GuidOrderID">Value for this foreign key.</param>
		/// <param name="FK_Oit_GuidProductID">Value for this foreign key.</param>
		public void Initialize(System.Net.NetworkCredential networkCredential, WS_tblOrderItem_Record.WSGuid FK_Oit_GuidOrderID, WS_tblOrderItem_Record.WSGuid FK_Oit_GuidProductID) {

			this.networkCredential = networkCredential;

			tblOrderItem_RecordWebService = new WS_tblOrderItem_Record.tblOrderItem_Record();
			tblOrderItem_RecordWebService.Credentials = this.networkCredential;

			string url = ConfigurationSettings.AppSettings["WS_tblOrderItem_Record"];
			if (url != null && url .Length != 0) {

				tblOrderItem_RecordWebService.Url = url;
			}

			this.FK_Oit_GuidOrderID = FK_Oit_GuidOrderID;
			this.FK_Oit_GuidProductID = FK_Oit_GuidProductID;
		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new WS_tblOrderItem_Record.WSGuid[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new WS_tblOrderItem_Record.WSGuid[0], startRecord, maxRecords);
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

				WS_tblOrderItem_Record.WSGuid[] typedArray = new WS_tblOrderItem_Record.WSGuid[ArrayOf_PK_Oit_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Oit_GuidID) {

					if (item is WS_tblOrderItem_Record.WSGuid) {

						typedArray[index] = (WS_tblOrderItem_Record.WSGuid)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlGuid) {

						WS_tblOrderItem_Record.WSGuid value = new WS_tblOrderItem_Record.WSGuid();
						value.UseNull = false;
						value.Value = ((System.Data.SqlTypes.SqlGuid)item).Value;
						
						typedArray[index] = value;
						index++;
					}
					else if (item is System.Guid) {

						WS_tblOrderItem_Record.WSGuid value = new WS_tblOrderItem_Record.WSGuid();
						value.UseNull = false;
						value.Value = (System.Guid)item;

						typedArray[index] = value;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Oit_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid, WS_tblOrderItem_Record.WSGuid or System.Guid elements.");
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
		public void RefreshData(WS_tblOrderItem_Record.WSGuid[] ArrayOf_PK_Oit_GuidID) {

			this.RefreshData(ArrayOf_PK_Oit_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Oit_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_tblOrderItem_Record.WSGuid[] ArrayOf_PK_Oit_GuidID, int startRecord, int maxRecords) {


			System.Data.DataSet dataSet = null;
			dataSet = tblOrderItem_RecordWebService.GetAllDisplay_tblOrderItem_Collection_DataSet(this.FK_Oit_GuidOrderID, this.FK_Oit_GuidProductID);
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblOrderItem_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			System.Data.DataView dataView = dataSet.Tables["spS_tblOrderItem_Display"].DefaultView;

			if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Length > 0) {

				this.SetRecordsCheckState(ArrayOf_PK_Oit_GuidID, System.Windows.Forms.CheckState.Checked);
			}
			else {

				base.OnSelectedIndexChanged(EventArgs.Empty);
			}
			this.EndUpdate();
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

				WS_tblOrderItem_Record.WSGuid[] typedArray = new WS_tblOrderItem_Record.WSGuid[ArrayOf_PK_Oit_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Oit_GuidID) {

					if (item is WS_tblOrderItem_Record.WSGuid) {

						typedArray[index] = (WS_tblOrderItem_Record.WSGuid)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlGuid) {

						WS_tblOrderItem_Record.WSGuid record = new WS_tblOrderItem_Record.WSGuid();
						record.UseNull = false;
						record.Value = ((System.Data.SqlTypes.SqlGuid)item).Value;
						
						typedArray[index] = record;
						index++;
					}
					else if (item is System.Guid) {

						WS_tblOrderItem_Record.WSGuid record = new WS_tblOrderItem_Record.WSGuid();
						record.UseNull = false;
						record.Value = (System.Guid)item;

						typedArray[index] = record;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Oit_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid, WS_tblOrderItem_Record.WSGuid or System.Guid elements.");
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
		public void SetRecordsCheckState(WS_tblOrderItem_Record.WSGuid[] ArrayOf_PK_Oit_GuidID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Oit_GuidID != null && ArrayOf_PK_Oit_GuidID.Length > 0) {

				int primaryKeysWereFound = 0;
				int totalPrimaryKeysNumber = ArrayOf_PK_Oit_GuidID.Length;
				int totalDataRowViewsNumber = this.Items.Count;

				this.internalItemCheckStateUpdateInProgress = true;

				for (int index = 0; index < totalDataRowViewsNumber; index++) {

					System.Data.DataRowView dataRowView = (System.Data.DataRowView)this.Items[index];
					System.Guid currentPrimaryKey = (System.Guid)dataRowView.Row[0];

					foreach (WS_tblOrderItem_Record.WSGuid record in ArrayOf_PK_Oit_GuidID) {

						if (record.Value == currentPrimaryKey) {

							this.SetItemChecked(index, true);
							primaryKeysWereFound ++;

							break;
						}
					}

					if (totalPrimaryKeysNumber == primaryKeysWereFound) break;
				}

				this.internalItemCheckStateUpdateInProgress = false;
			}
		}
	}
}
