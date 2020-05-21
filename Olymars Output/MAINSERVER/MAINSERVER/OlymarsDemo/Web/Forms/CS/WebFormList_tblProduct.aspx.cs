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
	/// Summary description for WebFormList_tblProduct.
	/// </summary>
	public class WebFormList_tblProduct: System.Web.UI.Page
	{
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebFormList_tblProduct() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString = String.Empty;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblProduct_SelectDisplay repeater_tblProduct_SelectDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblCategory com_Pro_LngCategoryID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Pro_StrName;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Pro_CurPrice;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Pro_LngCategoryID;
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

				// com_Pro_LngCategoryID
				System.Data.SqlTypes.SqlInt32 colPro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
				if (Request.Params["Pro_LngCategoryID"] != String.Empty) {
				
					try {
					
						colPro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Parse(Request.Params["Pro_LngCategoryID"]);
					}
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}
				com_Pro_LngCategoryID.Initialize(ConnectionString);
				try {

					com_Pro_LngCategoryID.RefreshData(colPro_LngCategoryID);
				}
				catch (OlymarsDemo.DataClasses.CustomException customException) {

					if (customException.Parameter.SqlException != null) {

						throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblCategory' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
					}
					else if (customException.Parameter.OtherException != null) {

						throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebDropDownList_tblCategory' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
					}
					else {

						throw;
					}
				}
				com_Pro_LngCategoryID.Items.Insert(0, "Show all");

				// Any sort preferences?
				CurrentSortEnum sortColumn = CurrentSortEnum.SortBy_Pro_StrName;
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

				if (ViewState["WebFormList_tblProduct_CurrentSort"] == null) {

					ViewState.Add("WebFormList_tblProduct_CurrentSort", sortColumn);
				}
				else {

					ViewState["WebFormList_tblProduct_CurrentSort"] = sortColumn;
				}

				if (ViewState["sortOrder"] == null) {

					ViewState.Add("sortOrder", sortOrder);
				}
				else {

					ViewState["sortOrder"] = sortOrder;
				}
			}

			repeater_tblProduct_SelectDisplay.EnableViewState = true;

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

			this.com_Pro_LngCategoryID.SelectedIndexChanged += new System.EventHandler(this.com_Pro_LngCategoryID_SelectedIndexChanged);
			this.Label_Pro_StrName.Click += new System.EventHandler(this.Label_Pro_StrName_Click);
			this.Label_Pro_CurPrice.Click += new System.EventHandler(this.Label_Pro_CurPrice_Click);
			this.Label_Pro_LngCategoryID.Click += new System.EventHandler(this.Label_Pro_LngCategoryID_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private enum CurrentSortEnum {

			  SortBy_Pro_StrName
			, SortBy_Pro_CurPrice
			, SortBy_Pro_LngCategoryID
		}

		private enum SortOrderEnum {

			Ascending, Descending
		}

		private void com_Pro_LngCategoryID_SelectedIndexChanged(object sender, System.EventArgs e) {

			RefreshList();
		}

		private void RefreshList() {

			System.Data.SqlTypes.SqlInt32 Pro_LngCategoryID = System.Data.SqlTypes.SqlInt32.Null;
			if (com_Pro_LngCategoryID.SelectedIndex != 0) {

				Pro_LngCategoryID = new System.Data.SqlTypes.SqlInt32(System.Convert.ToInt32(com_Pro_LngCategoryID.SelectedItem.Value));
			}

			repeater_tblProduct_SelectDisplay.Initialize(ConnectionString, System.Data.SqlTypes.SqlGuid.Null, Pro_LngCategoryID);
			try {

				repeater_tblProduct_SelectDisplay.RefreshData();
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblProduct' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblProduct' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}
			System.Data.DataView dataView = (System.Data.DataView)repeater_tblProduct_SelectDisplay.DataSource;

			string SortDirective = String.Empty;
			switch ((CurrentSortEnum)ViewState["WebFormList_tblProduct_CurrentSort"]) {

				case CurrentSortEnum.SortBy_Pro_StrName:
					SortDirective = spS_tblProduct.Resultset1.Fields.Column_Pro_StrName.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Pro_CurPrice:
					SortDirective = spS_tblProduct.Resultset1.Fields.Column_Pro_CurPrice.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Pro_LngCategoryID:
					SortDirective = spS_tblProduct_SelectDisplay.Resultset1.Fields.Column_Pro_LngCategoryID_Display.ColumnName;
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
			repeater_tblProduct_SelectDisplay.DataBind();
		}

		private void Label_Pro_StrName_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblProduct_CurrentSort"] == CurrentSortEnum.SortBy_Pro_StrName) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblProduct_CurrentSort"] = CurrentSortEnum.SortBy_Pro_StrName;
			}

			RefreshList();
		}

		private void Label_Pro_CurPrice_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblProduct_CurrentSort"] == CurrentSortEnum.SortBy_Pro_CurPrice) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblProduct_CurrentSort"] = CurrentSortEnum.SortBy_Pro_CurPrice;
			}

			RefreshList();
		}

		private void Label_Pro_LngCategoryID_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblProduct_CurrentSort"] == CurrentSortEnum.SortBy_Pro_LngCategoryID) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblProduct_CurrentSort"] = CurrentSortEnum.SortBy_Pro_LngCategoryID;
			}

			RefreshList();
		}

	}
}
