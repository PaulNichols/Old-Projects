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
using WS_Category = OlymarsDemo_WebServicesClient.WS_Category;

namespace OlymarsDemo.WebServices.Windows.ComboBoxes {

	/// <summary>
	/// This class derives from System.Windows.Forms.ComboBox class and was built to display the 'tblCategory' table content.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(WinComboBox_tblCategory), "WinComboBox.bmp")]
	public sealed class WinComboBox_tblCategory : System.Windows.Forms.ComboBox {

		private WS_Category.Category categoryWebService = null;
		private System.Net.NetworkCredential networkCredential = null;


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
		public WS_Category.WSInt32 SelectedRecordID {

			get {

				WS_Category.WSInt32 value = new WS_Category.WSInt32();

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
		/// Create a new instance of the OlymarsDemo.Windows.ComboBoxes.WinComboBox_tblCategory class.
		/// </summary>
		public WinComboBox_tblCategory() : base() {

		}

		/// <summary>
		/// Initializes the control. You need to specify how to connect to the
		/// SQL Server database and if you want to populate the whole table content or
		/// only a subset (based on its foreign keys). The data will only be populated once
		/// you have called the RefreshData method.
		/// </summary>
		public void Initialize(System.Net.NetworkCredential networkCredential) {

			this.networkCredential = networkCredential;

			categoryWebService = new WS_Category.Category();
			categoryWebService.Credentials = this.networkCredential;

			string url = ConfigurationSettings.AppSettings["WS_Category"];
			if (url != null && url .Length != 0) {

				categoryWebService.Url = url;
			}

		}

		/// <summary>
		/// Load or reloads all the table content. In order to successfully call this method,
		/// you need to call first the Initialize method.
		/// </summary>
		public void RefreshData() {

			WS_Category.WSInt32 nullValue = new WS_Category.WSInt32();
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

			WS_Category.WSInt32 nullValue = new WS_Category.WSInt32();
			nullValue.UseNull = true;

			this.RefreshData(nullValue, startRecord, maxRecords);
		}

		/// <summary>
		/// Load or reloads all the table content. You can specify which record you want to be selected by default.
		/// In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Cat_LngID">Primary key of the record you want to be selected by default.</param>
		public void RefreshData(WS_Category.WSInt32 PK_Cat_LngID) {

			this.RefreshData(PK_Cat_LngID, -1, -1);
		}

		/// <summary>
		/// Load or reloads a subset of the table content. You can specify which record you want to be selected
		/// by default. In order to successfully call this method, you need to call first the Initialize method.
		/// </summary>
		/// <param name="PK_Cat_LngID">Primary key of the record you want to be selected by default.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		public void RefreshData(WS_Category.WSInt32 PK_Cat_LngID, int startRecord, int maxRecords) {


			System.Data.DataSet dataSet = null;
			dataSet = categoryWebService.GetAllDisplay_Categories_DataSet();
			
			this.BeginUpdate();
			this.bindingInProgress = true;

			this.DataSource = dataSet.Tables["spS_tblCategory_Display"].DefaultView;
			this.ValueMember = "ID1";
			this.DisplayMember = "Display";

			this.bindingInProgress = false;

			if (!PK_Cat_LngID.UseNull) {

				this.SelectedValue = PK_Cat_LngID.Value;
			}
			else {

				base.OnSelectedIndexChanged(EventArgs.Empty);
			}

			this.EndUpdate();
		}
	}
}
