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
	/// Summary description for WebForm_tblOrder.
	/// </summary>
	public class WebForm_tblOrder : System.Web.UI.Page {

		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Ord_DatOrderedOn;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.TextBox txt_Ord_DatOrderedOn;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label labError_Ord_DatOrderedOn;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Ord_LngCustomerID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCustomer com_Ord_LngCustomerID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label lab_Ord_CurTotal;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.TextBox txt_Ord_CurTotal;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.Label labError_Ord_CurTotal;
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
		public WebForm_tblOrder() {

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

			labError_Ord_DatOrderedOn.Visible = false;
			labError_Ord_CurTotal.Visible = false;

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

			com_Ord_LngCustomerID.Initialize(ConnectionString);
			try {

				com_Ord_LngCustomerID.RefreshData(System.Data.SqlTypes.SqlInt32.Null);
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblCustomer' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblCustomer' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
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

			OlymarsDemo.AbstractClasses.Abstract_tblOrder oAbstract_tblOrder = new OlymarsDemo.AbstractClasses.Abstract_tblOrder(ConnectionString);

			if (!oAbstract_tblOrder.Refresh(CurrentID)) {

				MainDisplay.Visible = false;
				ErrorDisplay.Visible = true;
				lab_Error.Text = String.Format("No record with ID: {0}", CurrentID.ToString());
				return;
			}

			if (!oAbstract_tblOrder.Col_Ord_DatOrderedOn.IsNull) {

				txt_Ord_DatOrderedOn.Text = oAbstract_tblOrder.Col_Ord_DatOrderedOn.Value.ToString();
			}
			else {

				txt_Ord_DatOrderedOn.Text = String.Empty;
			}

			com_Ord_LngCustomerID.Items.FindByValue(Convert.ToString(oAbstract_tblOrder.Col_Ord_LngCustomerID)).Selected = true;

			if (!oAbstract_tblOrder.Col_Ord_CurTotal.IsNull) {

				txt_Ord_CurTotal.Text = oAbstract_tblOrder.Col_Ord_CurTotal.Value.ToString();
			}
			else {

				txt_Ord_CurTotal.Text = String.Empty;
			}

		}

		private void EmptyControls() {

			txt_Ord_DatOrderedOn.Text = String.Empty;
			com_Ord_LngCustomerID.SelectedIndex = 0;
			txt_Ord_CurTotal.Text = String.Empty;
		}

		private void cmdAdd_Click(object sender, System.EventArgs e) {

			if (ReturnURL.Visible) {

				Response.Redirect(String.Format("WebForm_tblOrder.aspx?Action=Add&ReturnToUrl={0}&ReturnToDisplay={1}", ReturnURL.NavigateUrl, ReturnURL.Text));
			}
			else {

				Response.Redirect("WebForm_tblOrder.aspx?Action=Add");
			}
		}

		private void cmdUpdate_Click(object sender, System.EventArgs e) {

			if (!CheckValues()) {
			
				return;
			}

			if (Action == ActionEnum.Edit) {

				Params.spU_tblOrder Param = null;
				SPs.spU_tblOrder SP = null;

				Param = new Params.spU_tblOrder();

				Param.SetUpConnection(ConnectionString);

				Param.Param_Ord_GuidID = CurrentID;

				if (txt_Ord_DatOrderedOn.Text.Trim() != String.Empty) {

					Param.Param_Ord_DatOrderedOn = new System.Data.SqlTypes.SqlDateTime(System.Convert.ToDateTime(txt_Ord_DatOrderedOn.Text));
				}

				Param.Param_Ord_LngCustomerID = Convert.ToInt32(com_Ord_LngCustomerID.SelectedItem.Value);

				if (txt_Ord_CurTotal.Text.Trim() != String.Empty) {

					Param.Param_Ord_CurTotal = new System.Data.SqlTypes.SqlMoney(System.Convert.ToDecimal(txt_Ord_CurTotal.Text));
				}

				SP = new SPs.spU_tblOrder();

				if (SP.Execute(ref Param)) {

					if (ReturnURL.Visible) {

						Response.Redirect(String.Format("WebForm_tblOrder.aspx?Action=Edit&ID={0}&ReturnToUrl={1}&ReturnToDisplay={2}", CurrentID.ToString(), ReturnURL.NavigateUrl, ReturnURL.Text));
					}
					else {

						Response.Redirect(String.Format("WebForm_tblOrder.aspx?Action=Edit&ID={0}", CurrentID.ToString()));
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

				Params.spI_tblOrder Param = null;
				SPs.spI_tblOrder SP = null;

				Param = new Params.spI_tblOrder();

				Param.SetUpConnection(ConnectionString);

				if (txt_Ord_DatOrderedOn.Text.Trim() != String.Empty) {

					Param.Param_Ord_DatOrderedOn = new System.Data.SqlTypes.SqlDateTime(System.Convert.ToDateTime(txt_Ord_DatOrderedOn.Text));
				}

				Param.Param_Ord_LngCustomerID = Convert.ToInt32(com_Ord_LngCustomerID.SelectedItem.Value);

				if (txt_Ord_CurTotal.Text.Trim() != String.Empty) {

					Param.Param_Ord_CurTotal = new System.Data.SqlTypes.SqlMoney(System.Convert.ToDecimal(txt_Ord_CurTotal.Text));
				}

				SP = new SPs.spI_tblOrder();

				if (SP.Execute(ref Param)) {

					if (ReturnURL.Visible) {

						Response.Redirect(String.Format("WebForm_tblOrder.aspx?Action=Edit&ID={0}&ReturnToUrl={1}&ReturnToDisplay={2}", Param.Param_Ord_GuidID.ToString(), ReturnURL.NavigateUrl, ReturnURL.Text));
					}
					else {

						Response.Redirect(String.Format("WebForm_tblOrder.aspx?Action=Edit&ID={0}", Param.Param_Ord_GuidID.ToString()));
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

			Params.spD_tblOrder Param = null;
			SPs.spD_tblOrder SP = null;

			Param = new Params.spD_tblOrder();

			Param.SetUpConnection(ConnectionString);

			Param.Param_Ord_GuidID = CurrentID;

			SP = new SPs.spD_tblOrder();

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

				System.Data.SqlTypes.SqlDateTime value = new System.Data.SqlTypes.SqlDateTime(System.Convert.ToDateTime(txt_Ord_DatOrderedOn.Text));
			}

			catch {

				labError_Ord_DatOrderedOn.Text = "INVALID";
				labError_Ord_DatOrderedOn.Visible = true;
				status = false;
			}

			try {

				System.Data.SqlTypes.SqlMoney value = new System.Data.SqlTypes.SqlMoney(System.Convert.ToDecimal(txt_Ord_CurTotal.Text));
			}

			catch {

				labError_Ord_CurTotal.Text = "INVALID";
				labError_Ord_CurTotal.Visible = true;
				status = false;
			}

			return(status);
		}
	}
}
