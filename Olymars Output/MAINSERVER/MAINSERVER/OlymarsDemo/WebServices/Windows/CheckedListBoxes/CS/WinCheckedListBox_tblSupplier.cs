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
using WS_Supplier = OlymarsDemo_WebServicesClient.WS_Supplier;

namespace OlymarsDemo.WebServices.Windows.CheckedListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.CheckedListBox class and was built to display the 'tblSupplier' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinCheckedListBox_tblSupplier), "WinCheckedListBox.bmp")]
	public sealed class WinCheckedListBox_tblSupplier : System.Windows.Forms.CheckedListBox {

		private WS_Supplier.Supplier supplierWebService = null;
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
		public WS_Supplier.WSGuid SelectedRecordID {

			get {

				WS_Supplier.WSGuid value = new WS_Supplier.WSGuid();

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
				
					WS_Supplier.WSGuid record = new WS_Supplier.WSGuid();
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

			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.CheckedListBoxes.WinCheckedListBox_tblSupplier class.
		/// </summary>
		public WinCheckedListBox_tblSupplier() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		public void Initialize(System.Net.NetworkCredential networkCredential) {

			this.networkCredential = networkCredential;

			supplierWebService = new WS_Supplier.Supplier();
			supplierWebService.Credentials = this.networkCredential;

			string url = ConfigurationSettings.AppSettings["WS_Supplier"];
			if (url != null && url .Length != 0) {

				supplierWebService.Url = url;
			}

		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			this.RefreshData(new WS_Supplier.WSGuid[0], -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			this.RefreshData(new WS_Supplier.WSGuid[0], startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Sup_GuidID) {

			this.RefreshData(ArrayOf_PK_Sup_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(System.Collections.ArrayList ArrayOf_PK_Sup_GuidID, int startRecord, int maxRecords) {

			if (ArrayOf_PK_Sup_GuidID != null && ArrayOf_PK_Sup_GuidID.Count > 0) {

				WS_Supplier.WSGuid[] typedArray = new WS_Supplier.WSGuid[ArrayOf_PK_Sup_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Sup_GuidID) {

					if (item is WS_Supplier.WSGuid) {

						typedArray[index] = (WS_Supplier.WSGuid)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlGuid) {

						WS_Supplier.WSGuid value = new WS_Supplier.WSGuid();
						value.UseNull = false;
						value.Value = ((System.Data.SqlTypes.SqlGuid)item).Value;
						
						typedArray[index] = value;
						index++;
					}
					else if (item is System.Guid) {

						WS_Supplier.WSGuid value = new WS_Supplier.WSGuid();
						value.UseNull = false;
						value.Value = (System.Guid)item;

						typedArray[index] = value;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Sup_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid, WS_Supplier.WSGuid or System.Guid elements.");
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
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records you want to be checked by default.</param>
		public void RefreshData(WS_Supplier.WSGuid[] ArrayOf_PK_Sup_GuidID) {

			this.RefreshData(ArrayOf_PK_Sup_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which records you want to be checked by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records you want to be checked by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_Supplier.WSGuid[] ArrayOf_PK_Sup_GuidID, int startRecord, int maxRecords) {


			System.Data.DataSet dataSet = null;
			dataSet = supplierWebService.GetAllDisplay_Suppliers_DataSet();
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblSupplier_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			System.Data.DataView dataView = dataSet.Tables["spS_tblSupplier_Display"].DefaultView;

			if (ArrayOf_PK_Sup_GuidID != null && ArrayOf_PK_Sup_GuidID.Length > 0) {

				this.SetRecordsCheckState(ArrayOf_PK_Sup_GuidID, System.Windows.Forms.CheckState.Checked);
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
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(System.Collections.ArrayList ArrayOf_PK_Sup_GuidID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Sup_GuidID != null && ArrayOf_PK_Sup_GuidID.Count > 0) {

				WS_Supplier.WSGuid[] typedArray = new WS_Supplier.WSGuid[ArrayOf_PK_Sup_GuidID.Count];
				int index = 0;
				foreach(object item in ArrayOf_PK_Sup_GuidID) {

					if (item is WS_Supplier.WSGuid) {

						typedArray[index] = (WS_Supplier.WSGuid)item;
						index++;
					}
					else if (item is System.Data.SqlTypes.SqlGuid) {

						WS_Supplier.WSGuid record = new WS_Supplier.WSGuid();
						record.UseNull = false;
						record.Value = ((System.Data.SqlTypes.SqlGuid)item).Value;
						
						typedArray[index] = record;
						index++;
					}
					else if (item is System.Guid) {

						WS_Supplier.WSGuid record = new WS_Supplier.WSGuid();
						record.UseNull = false;
						record.Value = (System.Guid)item;

						typedArray[index] = record;
						index++;
					}
					else {

						throw new InvalidOperationException("ArrayOf_PK_Sup_GuidID does not contain ONLY System.Data.SqlTypes.SqlGuid, WS_Supplier.WSGuid or System.Guid elements.");
					}
				}
				this.SetRecordsCheckState(typedArray, value);
			}
		}
		
		/// <summary>
		/// Sets the supplied collection records to a System.Windows.Forms.CheckState value.
		/// </summary>
		/// <param name="ArrayOf_PK_Sup_GuidID">Primary keys of the records to set the check state for.</param>
		/// <param name="value">One of the System.Windows.Forms.CheckState values.</param>
		public void SetRecordsCheckState(WS_Supplier.WSGuid[] ArrayOf_PK_Sup_GuidID, System.Windows.Forms.CheckState value) {
		
			if (ArrayOf_PK_Sup_GuidID != null && ArrayOf_PK_Sup_GuidID.Length > 0) {

				int primaryKeysWereFound = 0;
				int totalPrimaryKeysNumber = ArrayOf_PK_Sup_GuidID.Length;
				int totalDataRowViewsNumber = this.Items.Count;

				this.internalItemCheckStateUpdateInProgress = true;

				for (int index = 0; index < totalDataRowViewsNumber; index++) {

					System.Data.DataRowView dataRowView = (System.Data.DataRowView)this.Items[index];
					System.Guid currentPrimaryKey = (System.Guid)dataRowView.Row[0];

					foreach (WS_Supplier.WSGuid record in ArrayOf_PK_Sup_GuidID) {

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
