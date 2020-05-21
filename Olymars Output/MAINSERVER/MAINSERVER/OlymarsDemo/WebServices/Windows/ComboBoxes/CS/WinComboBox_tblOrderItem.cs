/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 28/12/2004 11:53:17
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

namespace OlymarsDemo.WebServices.Windows.ComboBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.ComboBox class and was built to display the 'tblOrderItem' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinComboBox_tblOrderItem), "WinComboBox.bmp")]
	public sealed class WinComboBox_tblOrderItem : System.Windows.Forms.ComboBox {

		private WS_tblOrderItem_Record.tblOrderItem_Record tblOrderItem_RecordWebService = null;
		private System.Net.NetworkCredential networkCredential = null;

		private WS_tblOrderItem_Record.WSGuid FK_Oit_GuidOrderID = null;
		private WS_tblOrderItem_Record.WSGuid FK_Oit_GuidProductID = null;

		private bool bindingInProgress = false;

		/// <summary>
		/// Returns true if binding operation is in progress.
		/// </summary>
		public bool BindingInProgress {

			get {

				return(this.bindingInProgress);
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
		/// Create a new instance of the OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblOrderItem class.
		/// </summary>
		public WinComboBox_tblOrderItem() : base() {

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

			WS_tblOrderItem_Record.WSGuid nullValue = new WS_tblOrderItem_Record.WSGuid();
			nullValue.UseNull = true;

			this.RefreshData(nullValue, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. In order to successfully call this method, you need to call first
		/// the Initialize method.
		/// </summary>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(int startRecord, int maxRecords) {

			WS_tblOrderItem_Record.WSGuid nullValue = new WS_tblOrderItem_Record.WSGuid();
			nullValue.UseNull = true;

			this.RefreshData(nullValue, startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which record you want to be selected by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Oit_GuidID">Primary key of the record you want to be selected by default.</param>
		public void RefreshData(WS_tblOrderItem_Record.WSGuid PK_Oit_GuidID) {

			this.RefreshData(PK_Oit_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be selected
		/// by default. In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Oit_GuidID">Primary key of the record you want to be selected by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_tblOrderItem_Record.WSGuid PK_Oit_GuidID, int startRecord, int maxRecords) {


			System.Data.DataSet dataSet = null;
			dataSet = tblOrderItem_RecordWebService.GetAllDisplay_tblOrderItem_Collection_DataSet(this.FK_Oit_GuidOrderID, this.FK_Oit_GuidProductID);
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblOrderItem_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			if (!PK_Oit_GuidID.UseNull) {

				this.SelectedValue = PK_Oit_GuidID.Value;
			}
			else {

				base.OnSelectedIndexChanged(EventArgs.Empty);
			}

			this.EndUpdate();
		}
	}
}
