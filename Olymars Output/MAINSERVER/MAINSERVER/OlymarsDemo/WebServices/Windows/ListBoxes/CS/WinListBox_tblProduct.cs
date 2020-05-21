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
using WS_Product = OlymarsDemo_WebServicesClient.WS_Product;

namespace OlymarsDemo.WebServices.Windows.ListBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.ListBox class and was built to display the 'tblProduct' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinListBox_tblProduct), "WinListBox.bmp")]
	public sealed class WinListBox_tblProduct : System.Windows.Forms.ListBox {

		private WS_Product.Product productWebService = null;
		private System.Net.NetworkCredential networkCredential = null;

		private WS_Product.WSInt32 FK_Pro_LngCategoryID = null;

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
		public WS_Product.WSGuid SelectedRecordID {

			get {

				WS_Product.WSGuid value = new WS_Product.WSGuid();

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
		/// Returns an array of the currently selected records primary keys. This is only
		/// available when SelectionMode=MultiSimple or SelectionMode=MultiExtended
		/// </summary>
		public System.Collections.ArrayList SelectedRecordsID {

			get {
						
				if (this.SelectionMode == System.Windows.Forms.SelectionMode.None || this.SelectionMode == System.Windows.Forms.SelectionMode.One) {

					throw new InvalidOperationException("Not available given the current selection behavior (SelectionMode property) of this control.");
				}

				System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
				
				foreach(object currentItem in this.SelectedItems) {
				
					WS_Product.WSGuid currentID = new WS_Product.WSGuid();
					currentID.UseNull = false;
					currentID.Value = (System.Guid)((System.Data.DataRowView)currentItem).Row[0];

					arrayList.Add(currentID);
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

				this.FK_Pro_LngCategoryID = null;
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Create a new instance of the OlymarsDemo.Windows.ListBoxes.WinListBox_tblProduct class.
		/// </summary>
		public WinListBox_tblProduct() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		/// <param name="FK_Pro_LngCategoryID">Value for this foreign key.</param>
		public void Initialize(System.Net.NetworkCredential networkCredential, WS_Product.WSInt32 FK_Pro_LngCategoryID) {

			this.networkCredential = networkCredential;

			productWebService = new WS_Product.Product();
			productWebService.Credentials = this.networkCredential;

			string url = ConfigurationSettings.AppSettings["WS_Product"];
			if (url != null && url .Length != 0) {

				productWebService.Url = url;
			}

			this.FK_Pro_LngCategoryID = FK_Pro_LngCategoryID;
		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			WS_Product.WSGuid nullValue = new WS_Product.WSGuid();
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

			WS_Product.WSGuid nullValue = new WS_Product.WSGuid();
			nullValue.UseNull = true;

			this.RefreshData(nullValue, startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which record you want to be selected by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Pro_GuidID">Primary key of the record you want to be selected by default.</param>
		public void RefreshData(WS_Product.WSGuid PK_Pro_GuidID) {

			this.RefreshData(PK_Pro_GuidID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be selected
		/// by default. In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Pro_GuidID">Primary key of the record you want to be selected by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_Product.WSGuid PK_Pro_GuidID, int startRecord, int maxRecords) {

			if (!PK_Pro_GuidID.UseNull && this.SelectionMode == System.Windows.Forms.SelectionMode.None) {

				throw new ArgumentException("You cannot supply a value to this parameter when SelectionMode property is set to None.", "PK_Pro_GuidID");
			}

			if (this.networkCredential == null) {

				throw new InvalidOperationException("You must call the 'Initialize' method before calling this method.");
			}


			System.Data.DataSet dataSet = null;
			dataSet = productWebService.GetAllDisplay_Products_DataSet(this.FK_Pro_LngCategoryID);
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblProduct_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			if (!PK_Pro_GuidID.UseNull) {

				this.SelectedValue = PK_Pro_GuidID.Value;
			}
			else {

				base.OnSelectedIndexChanged(EventArgs.Empty);
			}

			this.EndUpdate();
		}
	}
}
