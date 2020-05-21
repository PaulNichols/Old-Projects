/*
	This C# source code was automatically generated using:

		SQL Server Centric .NET Code Generator
			v 1.0.1697.21165

			Generation Date: 27/12/2004 16:08:33
			Generator name: MAINSERVER\Administrator
			Template last update: 13/10/2003 04:51:40
			Template revision: 56177501

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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace OlymarsDemo.Web.Forms
{
	/// <summary>
	/// Summary description for AdminTop.
	/// </summary>
	public class WebFormInternal_AdminTop : System.Web.UI.Page {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label labTables;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList comDatabaseTablesList;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label labRecords;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCategory com_tblCategory;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCustomer com_tblCustomer;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrder com_tblOrder;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrderItem com_tblOrderItem;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblProduct com_tblProduct;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblSupplier com_tblSupplier;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Button cmdAddNewRecord;
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebFormInternal_AdminTop() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString;
		private System.Globalization.CultureInfo CurrentUserCulture;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public string FrameEditURL;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public string FrameAddURL;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public string ComboBoxName;

		private void Page_Load(object sender, System.EventArgs e) {

			string Language = "en-US";
			if (Request.UserLanguages.Length != 0) {

				Language = Request.UserLanguages[0];
			}

			CurrentUserCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);
			System.Threading.Thread.CurrentThread.CurrentUICulture = CurrentUserCulture;
			System.Threading.Thread.CurrentThread.CurrentCulture = CurrentUserCulture;

			/*
				Add the following in your web.config file
				<appSettings>
					<add key="OlymarsDemo ConnectionString" value="Data Source=localhost; Initial Catalog=OlymarsDemo; Integrated Security=SSPI" />
				</appSettings>
			*/
			if (ConfigurationSettings.AppSettings["OlymarsDemo ConnectionString"] != null) {

				ConnectionString = ConfigurationSettings.AppSettings["OlymarsDemo ConnectionString"];
			}
			else if (Application["ConnectionString"] != null) {

				ConnectionString = Application["ConnectionString"].ToString().Trim();
			}
			else {

				ConnectionString = String.Empty;
			}

			if (!Page.IsPostBack) {

				UpdateRecords();
				com_tblCategory_SelectedIndexChanged(null, null);
			}
		}

		private void UpdateRecords() {

			com_tblCategory.EnableViewState = false;
			com_tblCategory.Visible = false;
			com_tblCustomer.EnableViewState = false;
			com_tblCustomer.Visible = false;
			com_tblOrder.EnableViewState = false;
			com_tblOrder.Visible = false;
			com_tblOrderItem.EnableViewState = false;
			com_tblOrderItem.Visible = false;
			com_tblProduct.EnableViewState = false;
			com_tblProduct.Visible = false;
			com_tblSupplier.EnableViewState = false;
			com_tblSupplier.Visible = false;

			switch (comDatabaseTablesList.SelectedItem.Value) {

				case "tblCategory":
					com_tblCategory.EnableViewState = true;
					com_tblCategory.Visible = true;
					com_tblCategory.Initialize(ConnectionString);
					com_tblCategory.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
					com_tblCategory.Items.Insert(0, "");
					if (com_tblCategory.Items.Count > 1) {

						com_tblCategory.SelectedIndex = 1;
					}
					com_tblCategory_SelectedIndexChanged(null, null);
					break;

				case "tblCustomer":
					com_tblCustomer.EnableViewState = true;
					com_tblCustomer.Visible = true;
					com_tblCustomer.Initialize(ConnectionString);
					com_tblCustomer.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
					com_tblCustomer.Items.Insert(0, "");
					if (com_tblCustomer.Items.Count > 1) {

						com_tblCustomer.SelectedIndex = 1;
					}
					com_tblCustomer_SelectedIndexChanged(null, null);
					break;

				case "tblOrder":
					com_tblOrder.EnableViewState = true;
					com_tblOrder.Visible = true;
					com_tblOrder.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
					com_tblOrder.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
					com_tblOrder.Items.Insert(0, "");
					if (com_tblOrder.Items.Count > 1) {

						com_tblOrder.SelectedIndex = 1;
					}
					com_tblOrder_SelectedIndexChanged(null, null);
					break;

				case "tblOrderItem":
					com_tblOrderItem.EnableViewState = true;
					com_tblOrderItem.Visible = true;
					com_tblOrderItem.Initialize(ConnectionString, System.Data.SqlTypes.SqlGuid.Null, System.Data.SqlTypes.SqlGuid.Null);
					com_tblOrderItem.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
					com_tblOrderItem.Items.Insert(0, "");
					if (com_tblOrderItem.Items.Count > 1) {

						com_tblOrderItem.SelectedIndex = 1;
					}
					com_tblOrderItem_SelectedIndexChanged(null, null);
					break;

				case "tblProduct":
					com_tblProduct.EnableViewState = true;
					com_tblProduct.Visible = true;
					com_tblProduct.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
					com_tblProduct.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
					com_tblProduct.Items.Insert(0, "");
					if (com_tblProduct.Items.Count > 1) {

						com_tblProduct.SelectedIndex = 1;
					}
					com_tblProduct_SelectedIndexChanged(null, null);
					break;

				case "tblSupplier":
					com_tblSupplier.EnableViewState = true;
					com_tblSupplier.Visible = true;
					com_tblSupplier.Initialize(ConnectionString);
					com_tblSupplier.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
					com_tblSupplier.Items.Insert(0, "");
					if (com_tblSupplier.Items.Count > 1) {

						com_tblSupplier.SelectedIndex = 1;
					}
					com_tblSupplier_SelectedIndexChanged(null, null);
					break;

				default:
					break;
			}
		}

		private void Page_Init(object sender, EventArgs e) {

			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		private void comDatabaseTablesList_SelectedIndexChanged(object sender, System.EventArgs e) {

			UpdateRecords();
		}

		private void com_tblCategory_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblCategory.SelectedIndex == -1 || com_tblCategory.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblCategory.aspx?Action=Edit&ID=" + com_tblCategory.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblCategory.aspx?Action=Add";
			ComboBoxName = "com_tblCategory";
		}
		private void com_tblCustomer_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblCustomer.SelectedIndex == -1 || com_tblCustomer.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblCustomer.aspx?Action=Edit&ID=" + com_tblCustomer.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblCustomer.aspx?Action=Add";
			ComboBoxName = "com_tblCustomer";
		}
		private void com_tblOrder_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblOrder.SelectedIndex == -1 || com_tblOrder.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblOrder.aspx?Action=Edit&ID=" + com_tblOrder.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblOrder.aspx?Action=Add";
			ComboBoxName = "com_tblOrder";
		}
		private void com_tblOrderItem_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblOrderItem.SelectedIndex == -1 || com_tblOrderItem.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblOrderItem.aspx?Action=Edit&ID=" + com_tblOrderItem.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblOrderItem.aspx?Action=Add";
			ComboBoxName = "com_tblOrderItem";
		}
		private void com_tblProduct_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblProduct.SelectedIndex == -1 || com_tblProduct.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblProduct.aspx?Action=Edit&ID=" + com_tblProduct.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblProduct.aspx?Action=Add";
			ComboBoxName = "com_tblProduct";
		}
		private void com_tblSupplier_SelectedIndexChanged(object sender, System.EventArgs e) {

			if (com_tblSupplier.SelectedIndex == -1 || com_tblSupplier.SelectedIndex == 0) {

				FrameEditURL = "about:blank";
			}
			else {

				FrameEditURL = "WebForm_tblSupplier.aspx?Action=Edit&ID=" + com_tblSupplier.SelectedItem.Value;
			}
			FrameAddURL = "WebForm_tblSupplier.aspx?Action=Add";
			ComboBoxName = "com_tblSupplier";
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

			this.comDatabaseTablesList.SelectedIndexChanged += new System.EventHandler(this.comDatabaseTablesList_SelectedIndexChanged);
			this.com_tblCategory.SelectedIndexChanged += new System.EventHandler(this.com_tblCategory_SelectedIndexChanged);
			this.com_tblCustomer.SelectedIndexChanged += new System.EventHandler(this.com_tblCustomer_SelectedIndexChanged);
			this.com_tblOrder.SelectedIndexChanged += new System.EventHandler(this.com_tblOrder_SelectedIndexChanged);
			this.com_tblOrderItem.SelectedIndexChanged += new System.EventHandler(this.com_tblOrderItem_SelectedIndexChanged);
			this.com_tblProduct.SelectedIndexChanged += new System.EventHandler(this.com_tblProduct_SelectedIndexChanged);
			this.com_tblSupplier.SelectedIndexChanged += new System.EventHandler(this.com_tblSupplier_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
