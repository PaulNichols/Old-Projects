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
using OlymarsDemo.DataClasses.StoredProcedures;

namespace OlymarsDemo.Web.Forms
{
	/// <summary>
	/// Summary description for WebFormList_tblOrder.
	/// </summary>
	public class WebFormList_tblOrder: System.Web.UI.Page
	{
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebFormList_tblOrder() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString = String.Empty;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrder_SelectDisplay repeater_tblOrder_SelectDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCustomer com_Ord_LngCustomerID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Ord_DatOrderedOn;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Ord_LngCustomerID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Ord_CurTotal;
		private System.Globalization.CultureInfo CurrentUserCulture;
		private void Page_Load(object sender, System.EventArgs e) {

			string Language = "en-US";
			if (Request.UserLanguages.Length != 0) {

				Language = Request.UserLanguages[0];
			}

			CurrentUserCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Language);

			if (ConfigurationSettings.AppSettings["OlymarsDemo ConnectionString"] != null) {

				ConnectionString = ConfigurationSettings.AppSettings["OlymarsDemo ConnectionString"];
			}
			else if (Application["ConnectionString"] != null) {

				ConnectionString = Application["ConnectionString"].ToString().Trim();
			}

			if (!Page.IsPostBack) {

				// com_Ord_LngCustomerID
				System.Data.SqlTypes.SqlInt32 colOrd_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
				if (Request.Params["Ord_LngCustomerID"] != String.Empty) {
				
					try {
					
						colOrd_LngCustomerID = System.Data.SqlTypes.SqlInt32.Parse(Request.Params["Ord_LngCustomerID"]);
					}
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}
				com_Ord_LngCustomerID.Initialize(ConnectionString);
				try {

					com_Ord_LngCustomerID.RefreshData(colOrd_LngCustomerID);
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
				com_Ord_LngCustomerID.Items.Insert(0, "Show all");

				// Any sort preferences?
				CurrentSortEnum sortColumn = CurrentSortEnum.SortBy_Ord_DatOrderedOn;
				if (Request.Params["SortBy"] != String.Empty) {
				
					try {
					
						sortColumn = (CurrentSortEnum)Enum.Parse(typeof(CurrentSortEnum), "SortBy_" + Request.Params["SortBy"]);
					}
					
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}

				SortOrderEnum sortOrder = SortOrderEnum.Ascending;
				if (Request.Params["SortOrder"] != String.Empty) {
				
					try {
					
						sortOrder = (SortOrderEnum)Enum.Parse(typeof(SortOrderEnum), Request.Params["SortOrder"]);
					}
					
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}

				if (ViewState["WebFormList_tblOrder_CurrentSort"] == null) {

					ViewState.Add("WebFormList_tblOrder_CurrentSort", sortColumn);
				}
				else {

					ViewState["WebFormList_tblOrder_CurrentSort"] = sortColumn;
				}

				if (ViewState["sortOrder"] == null) {

					ViewState.Add("sortOrder", sortOrder);
				}
				else {

					ViewState["sortOrder"] = sortOrder;
				}
			}

			repeater_tblOrder_SelectDisplay.EnableViewState = true;

			RefreshList();
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

			this.com_Ord_LngCustomerID.SelectedIndexChanged += new System.EventHandler(this.com_Ord_LngCustomerID_SelectedIndexChanged);
			this.Label_Ord_DatOrderedOn.Click += new System.EventHandler(this.Label_Ord_DatOrderedOn_Click);
			this.Label_Ord_LngCustomerID.Click += new System.EventHandler(this.Label_Ord_LngCustomerID_Click);
			this.Label_Ord_CurTotal.Click += new System.EventHandler(this.Label_Ord_CurTotal_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private enum CurrentSortEnum {

			  SortBy_Ord_DatOrderedOn
			, SortBy_Ord_LngCustomerID
			, SortBy_Ord_CurTotal
		}

		private enum SortOrderEnum {

			Ascending, Descending
		}

		private void com_Ord_LngCustomerID_SelectedIndexChanged(object sender, System.EventArgs e) {

			RefreshList();
		}

		private void RefreshList() {

			System.Data.SqlTypes.SqlInt32 Ord_LngCustomerID = System.Data.SqlTypes.SqlInt32.Null;
			if (com_Ord_LngCustomerID.SelectedIndex != 0) {

				Ord_LngCustomerID = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(com_Ord_LngCustomerID.SelectedItem.Value));
			}

			repeater_tblOrder_SelectDisplay.Initialize(ConnectionString, System.Data.SqlTypes.SqlGuid.Null, Ord_LngCustomerID);
			try {

				repeater_tblOrder_SelectDisplay.RefreshData();
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblOrder' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblOrder' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}
			System.Data.DataView dataView = (System.Data.DataView)repeater_tblOrder_SelectDisplay.DataSource;

			string SortDirective = String.Empty;
			switch ((CurrentSortEnum)ViewState["WebFormList_tblOrder_CurrentSort"]) {

				case CurrentSortEnum.SortBy_Ord_DatOrderedOn:
					SortDirective = spS_tblOrder.Resultset1.Fields.Column_Ord_DatOrderedOn.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Ord_LngCustomerID:
					SortDirective = spS_tblOrder_SelectDisplay.Resultset1.Fields.Column_Ord_LngCustomerID_Display.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Ord_CurTotal:
					SortDirective = spS_tblOrder.Resultset1.Fields.Column_Ord_CurTotal.ColumnName;
					break;

			}

			switch ((SortOrderEnum)ViewState["sortOrder"]) {

				case SortOrderEnum.Ascending:
					SortDirective += " ASC";
					break;

				case SortOrderEnum.Descending:
					SortDirective += " DESC";
					break;
			}

			dataView.Sort = SortDirective;
			repeater_tblOrder_SelectDisplay.DataBind();
		}

		private void Label_Ord_DatOrderedOn_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrder_CurrentSort"] == CurrentSortEnum.SortBy_Ord_DatOrderedOn) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrder_CurrentSort"] = CurrentSortEnum.SortBy_Ord_DatOrderedOn;
			}

			RefreshList();
		}

		private void Label_Ord_LngCustomerID_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrder_CurrentSort"] == CurrentSortEnum.SortBy_Ord_LngCustomerID) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrder_CurrentSort"] = CurrentSortEnum.SortBy_Ord_LngCustomerID;
			}

			RefreshList();
		}

		private void Label_Ord_CurTotal_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrder_CurrentSort"] == CurrentSortEnum.SortBy_Ord_CurTotal) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrder_CurrentSort"] = CurrentSortEnum.SortBy_Ord_CurTotal;
			}

			RefreshList();
		}

	}
}
