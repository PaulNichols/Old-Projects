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
using Params = OlymarsDemo.DataClasses.Parameters;
using SPs = OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.Web.Forms {

	/// <summary>
	/// Summary description for WebForm_tblOrderItem.
	/// </summary>
	public class WebForm_tblOrderItem : System.Web.UI.Page {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Oit_GuidOrderID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrder com_Oit_GuidOrderID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Oit_GuidProductID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblProduct com_Oit_GuidProductID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Oit_LngAmount;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.TextBox txt_Oit_LngAmount;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label labError_Oit_LngAmount;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Button cmdRefresh;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Button cmdDelete;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Panel MainDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Error;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Panel ErrorDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Button cmdUpdate;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.HyperLink ReturnURL;
	
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebForm_tblOrderItem() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString;
		private ActionEnum Action = ActionEnum.None;
		private System.Data.SqlTypes.SqlGuid CurrentID = System.Data.SqlTypes.SqlGuid.Null;

		private enum ActionEnum {

			None, Add, Edit, Delete
		}

		private System.Globalization.CultureInfo CurrentUserCulture;
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

				if (Request.Params["ReturnToUrl"] != null) {

					ReturnURL.NavigateUrl = Request.Params["ReturnToUrl"].ToString();
					if (Request.Params["ReturnToDisplay"] != null) {

						ReturnURL.Text = Request.Params["ReturnToDisplay"].ToString();
					}
					ReturnURL.Visible = true;
				}
			}

			labError_Oit_LngAmount.Visible = false;

			if (Request.Params["Action"] != null) {

				switch(Request.Params["Action"].ToString()) {

					case "Add":
						Action = ActionEnum.Add;
						CurrentID = System.Data.SqlTypes.SqlGuid.Null;
						break;

					case "Edit":
						Action = ActionEnum.Edit;
						object ID = Request.Params["ID"];

						if (ID != null) {

							try {

								CurrentID = new Guid(Request.Params["ID"].ToString());
							}
							catch {

								MainDisplay.Visible = false;
								ErrorDisplay.Visible = true;
								lab_Error.Text = "ERROR: Action=Edit-> ID supplied is not a valid Guid";
								return;
							}
						}
						else {

							MainDisplay.Visible = false;
							ErrorDisplay.Visible = true;
							lab_Error.Text = "ERROR: Action=Edit-> No ID was supplied";
							return;
						}
						break;

					case "Delete":
						Action = ActionEnum.Delete;
						if (Request.Params["ID"] != null) {

							try {

								CurrentID = new Guid(Request.Params["ID"].ToString());
							}
							catch {

								MainDisplay.Visible = false;
								ErrorDisplay.Visible = true;
								lab_Error.Text = "ERROR: Action=Edit-> ID supplied is not a valid Int32";
								return;
							}
							cmdDelete_Click(null, null);
							return;
						}
						else {

							MainDisplay.Visible = false;
							ErrorDisplay.Visible = true;
							lab_Error.Text = "ERROR: Action=Delete-> No ID was supplied";
							return;
						}

					default:
						Action = ActionEnum.None;
						CurrentID = System.Data.SqlTypes.SqlGuid.Null;
						MainDisplay.Visible = false;
						ErrorDisplay.Visible = true;
						lab_Error.Text = "ERROR: Action must be Add, Edit Or Delete";
						return;
				}

				if (!Page.IsPostBack) {

					RefreshAll();
				}
			}

			else {

				MainDisplay.Visible = false;
				ErrorDisplay.Visible = true;
				lab_Error.Text = "ERROR: No Action was supplied";
			}
		}

		private void Page_Init(object sender, EventArgs e) {

			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {

			this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
			this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void RefreshForeignTables() {

			com_Oit_GuidOrderID.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
			try {

				com_Oit_GuidOrderID.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblOrder' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblOrder' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

			com_Oit_GuidProductID.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
			try {

				com_Oit_GuidProductID.RefreshData(System.Data.SqlTypes.SqlGuid.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblProduct' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblProduct' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}

		}

		private void UpdateForm() {

			if (Action == ActionEnum.Edit || Action == ActionEnum.None) {

				cmdRefresh.Visible = true;
				cmdDelete.Visible = true;
				cmdUpdate.Visible = true;
				cmdUpdate.Text = "Update";
				RefreshRecord();
			}
			else {

				cmdRefresh.Visible = false;
				cmdDelete.Visible = false;
				cmdUpdate.Visible = true;
				cmdUpdate.Text = "OK";
				EmptyControls();
			}
		}

		private void RefreshAll() {

			RefreshForeignTables();
			UpdateForm();
		}

		private void cmdRefresh_Click(object sender, System.EventArgs e) {

			RefreshAll();
		}

		private void RefreshRecord() {

			OlymarsDemo.AbstractClasses.Abstract_tblOrderItem oAbstract_tblOrderItem = new OlymarsDemo.AbstractClasses.Abstract_tblOrderItem(ConnectionString);

			if (!oAbstract_tblOrderItem.Refresh(CurrentID)) {

				MainDisplay.Visible = false;
				ErrorDisplay.Visible = true;
				lab_Error.Text = String.Format("No record with ID: {0}", CurrentID.ToString());
				return;
			}

			com_Oit_GuidOrderID.Items.FindByValue(Convert.ToString(oAbstract_tblOrderItem.Col_Oit_GuidOrderID)).Selected = true;

			com_Oit_GuidProductID.Items.FindByValue(Convert.ToString(oAbstract_tblOrderItem.Col_Oit_GuidProductID)).Selected = true;

			if (!oAbstract_tblOrderItem.Col_Oit_LngAmount.IsNull) {

				txt_Oit_LngAmount.Text = oAbstract_tblOrderItem.Col_Oit_LngAmount.Value.ToString();
			}
			else {

				txt_Oit_LngAmount.Text = String.Empty;
			}

		}

		private void EmptyControls() {

			com_Oit_GuidOrderID.SelectedIndex = 0;
			com_Oit_GuidProductID.SelectedIndex = 0;
			txt_Oit_LngAmount.Text = String.Empty;
		}

		private void cmdAdd_Click(object sender, System.EventArgs e) {

			if (ReturnURL.Visible) {

				Response.Redirect(String.Format("WebForm_tblOrderItem.aspx?Action=Add&ReturnToUrl={0}&ReturnToDisplay={1}", ReturnURL.NavigateUrl, ReturnURL.Text));
			}
			else {

				Response.Redirect("WebForm_tblOrderItem.aspx?Action=Add");
			}
		}

		private void cmdUpdate_Click(object sender, System.EventArgs e) {

			if (!CheckValues()) {
			
				return;
			}

			if (Action == ActionEnum.Edit) {

				Params.spU_tblOrderItem Param = null;
				SPs.spU_tblOrderItem SP = null;

				Param = new Params.spU_tblOrderItem();

				Param.SetUpConnection(ConnectionString);

				Param.Param_Oit_GuidID = CurrentID;

				Param.Param_Oit_GuidOrderID = new Guid(com_Oit_GuidOrderID.SelectedItem.Value);

				Param.Param_Oit_GuidProductID = new Guid(com_Oit_GuidProductID.SelectedItem.Value);

				if (txt_Oit_LngAmount.Text.Trim() != String.Empty) {

					Param.Param_Oit_LngAmount = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(txt_Oit_LngAmount.Text));
				}

				SP = new SPs.spU_tblOrderItem();

				if (SP.Execute(ref Param)) {

					if (ReturnURL.Visible) {

						Response.Redirect(String.Format("WebForm_tblOrderItem.aspx?Action=Edit&ID={0}&ReturnToUrl={1}&ReturnToDisplay={2}", CurrentID.ToString(), ReturnURL.NavigateUrl, ReturnURL.Text));
					}
					else {

						Response.Redirect(String.Format("WebForm_tblOrderItem.aspx?Action=Edit&ID={0}", CurrentID.ToString()));
					}
					return;
				}
				else {

					if (Param.SqlException != null) {

						throw Param.SqlException;
					}

					if (Param.OtherException != null) {

						throw Param.OtherException;
					}
				}
			}

			else {

				Params.spI_tblOrderItem Param = null;
				SPs.spI_tblOrderItem SP = null;

				Param = new Params.spI_tblOrderItem();

				Param.SetUpConnection(ConnectionString);

				Param.Param_Oit_GuidOrderID = new Guid(com_Oit_GuidOrderID.SelectedItem.Value);

				Param.Param_Oit_GuidProductID = new Guid(com_Oit_GuidProductID.SelectedItem.Value);

				if (txt_Oit_LngAmount.Text.Trim() != String.Empty) {

					Param.Param_Oit_LngAmount = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(txt_Oit_LngAmount.Text));
				}

				SP = new SPs.spI_tblOrderItem();

				if (SP.Execute(ref Param)) {

					if (ReturnURL.Visible) {

						Response.Redirect(String.Format("WebForm_tblOrderItem.aspx?Action=Edit&ID={0}&ReturnToUrl={1}&ReturnToDisplay={2}", Param.Param_Oit_GuidID.ToString(), ReturnURL.NavigateUrl, ReturnURL.Text));
					}
					else {

						Response.Redirect(String.Format("WebForm_tblOrderItem.aspx?Action=Edit&ID={0}", Param.Param_Oit_GuidID.ToString()));
					}
					return;
				}
				else {

					if (Param.SqlException != null) {

						throw Param.SqlException;
					}

					if (Param.OtherException != null) {

						throw Param.OtherException;
					}
				}
			}
		}

		private void cmdDelete_Click(object sender, System.EventArgs e) {

			Params.spD_tblOrderItem Param = null;
			SPs.spD_tblOrderItem SP = null;

			Param = new Params.spD_tblOrderItem();

			Param.SetUpConnection(ConnectionString);

			Param.Param_Oit_GuidID = CurrentID;

			SP = new SPs.spD_tblOrderItem();

			if (SP.Execute(ref Param)) {

				Param.Dispose();
				SP.Dispose();

				MainDisplay.Visible = false;
				ErrorDisplay.Visible = true;
				lab_Error.Text = String.Format("Record with ID: {0} was successfully deleted!", CurrentID.ToString());

				return;
			}
			else {

				if (Param.SqlException != null) {

					throw Param.SqlException;
				}

				if (Param.OtherException != null) {

					throw Param.OtherException;
				}
			}
		}

		private bool CheckValues() {

			bool status = true;
			try {

				System.Data.SqlTypes.SqlInt32 value = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(txt_Oit_LngAmount.Text));
			}

			catch {

				labError_Oit_LngAmount.Text = "INVALID";
				labError_Oit_LngAmount.Visible = true;
				status = false;
			}

			return(status);
		}
	}
}
