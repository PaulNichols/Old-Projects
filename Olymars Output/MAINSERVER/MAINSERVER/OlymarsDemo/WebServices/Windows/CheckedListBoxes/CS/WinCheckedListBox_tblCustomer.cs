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
using WS_tblCustomer_Record = OlymarsDemo_WebServicesClient.WS_tblCustomer_Record;

namespace OlymarsDemo.WebServices.Windows.CheckedListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.CheckedListBox class and was built to display the 'tblCustomer' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBox_tblCustomer), "WinCheckedListBox.bmp")]
	public sealed class WinCheckedListBox_tblCustomer : System.Windows.Forms.CheckedListBox {

		private WS_tblCustomer_Record.tblCustomer_Record tblCustomer_RecordWebService = null;
		private System.Net.NetworkCredential networkCredential = null;


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
		public WS_tblCustomer_Record.WSInt32 SelectedRecordID {

			get {

				WS_tblCustomer_Record.WSInt32 value = new WS_tblCustomer_Record.WSInt32();

				if (base.SelectedIndex == -1) {

					value.UseNull = true;

					return(value);
				}

				value.UseNull = false;
				value.Value = (System.Int32)((System.Data.DataRowView)this.SelectedItem).Row[0];

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
				
					WS_tblCustomer_Record.WSInt32 record = new WS_tblCustomer_Record.WSInt32();
					record.UseNull = false;
					record.Value = (System.Int32)((System.Data.DataRowView)currentItem).Row[0];

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

			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblCustomer class.
		/// </summary>
		public WinCheckedListBox_tblCustomer() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		public void Initialize(System.Net.NetworkCredential networkCredential) {

			this.networkCredential = networkCredential;

			tblCustomer_RecordWebService = new WS_tblCustomer_Record.tblCustomer_Record();
			tblCustomer_RecordWebService.Credentials = this.networkCredential;

			string url = ConfigurationSettings.AppSettings["WS_tblCustomer_Record"];
			if (url != null && url .Length != 0) {

				tblCustomer_RecordWebService.Url = url;
			}

		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new WS_tblCustomer_Record.WSInt32[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new WS_tblCustomer_Record.WSInt32[0], startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Cus_LngID) {

			this.RefreshData(ArrayOf_PK_Cus_LngID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Cus_LngID, int startRecord, int maxRecords) {

			if (ArrayOf_PK_Cus_LngID != null && ArrayOf_PK_Cus_LngID.Count > 0) {

				WS_tblCustomer_Record.WSInt32[] typedArray = new WS_tblCustomer_Record.WSInt32[ArrayOf_PK_Cus_LngID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Cus_LngID) {

					if (item is WS_tblCustomer_Record.WSInt32) {

						typedArray[index] = (WS_tblCustomer_Record.WSInt32)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlInt32) {

						WS_tblCustomer_Record.WSInt32 value = new WS_tblCustomer_Record.WSInt32();
						value.UseNull = false;
						value.Value = ((System.Data.SqlTypes.SqlInt32)item).Value;
						
						typedArray[index] = value;
						index++;
					}
					else if (item is System.Int32) {

						WS_tblCustomer_Record.WSInt32 value = new WS_tblCustomer_Record.WSInt32();
						value.UseNull = false;
						value.Value = (System.Int32)item;

						typedArray[index] = value;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Cus_LngID does not contain ONLY System.Data.SqlTypes.SqlInt32, WS_tblCustomer_Record.WSInt32 or System.Int32 elements.");
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
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(WS_tblCustomer_Record.WSInt32[] ArrayOf_PK_Cus_LngID) {

			this.RefreshData(ArrayOf_PK_Cus_LngID, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_tblCustomer_Record.WSInt32[] ArrayOf_PK_Cus_LngID, int startRecord, int maxRecords) {


			System.Data.DataSet dataSet = null;
			dataSet = tblCustomer_RecordWebService.GetAllDisplay_tblCustomer_Collection_DataSet();
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblCustomer_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			System.Data.DataView dataView = dataSet.Tables["spS_tblCustomer_Display"].DefaultView;

			if (ArrayOf_PK_Cus_LngID != null && ArrayOf_PK_Cus_LngID.Length > 0) {

				this.SetRecordsCheckState(ArrayOf_PK_Cus_LngID, System.Windows.Forms.CheckState.Checked);
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
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Collections.ArrayList ArrayOf_PK_Cus_LngID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Cus_LngID != null && ArrayOf_PK_Cus_LngID.Count > 0) {

				WS_tblCustomer_Record.WSInt32[] typedArray = new WS_tblCustomer_Record.WSInt32[ArrayOf_PK_Cus_LngID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Cus_LngID) {

					if (item is WS_tblCustomer_Record.WSInt32) {

						typedArray[index] = (WS_tblCustomer_Record.WSInt32)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlInt32) {

						WS_tblCustomer_Record.WSInt32 record = new WS_tblCustomer_Record.WSInt32();
						record.UseNull = false;
						record.Value = ((System.Data.SqlTypes.SqlInt32)item).Value;
						
						typedArray[index] = record;
						index++;
					}
					else if (item is System.Int32) {

						WS_tblCustomer_Record.WSInt32 record = new WS_tblCustomer_Record.WSInt32();
						record.UseNull = false;
						record.Value = (System.Int32)item;

						typedArray[index] = record;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Cus_LngID does not contain ONLY System.Data.SqlTypes.SqlInt32, WS_tblCustomer_Record.WSInt32 or System.Int32 elements.");
					}
				}
				this.SetRecordsCheckState(typedArray, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PK_Cus_LngID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(WS_tblCustomer_Record.WSInt32[] ArrayOf_PK_Cus_LngID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Cus_LngID != null && ArrayOf_PK_Cus_LngID.Length > 0) {

				int primaryKeysWereFound = 0;
				int totalPrimaryKeysNumber = ArrayOf_PK_Cus_LngID.Length;
				int totalDataRowViewsNumber = this.Items.Count;

				this.internalItemCheckStateUpdateInProgress = true;

				for (int index = 0; index < totalDataRowViewsNumber; index++) {

					System.Data.DataRowView dataRowView = (System.Data.DataRowView)this.Items[index];
					System.Int32 currentPrimaryKey = (System.Int32)dataRowView.Row[0];

					foreach (WS_tblCustomer_Record.WSInt32 record in ArrayOf_PK_Cus_LngID) {

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
