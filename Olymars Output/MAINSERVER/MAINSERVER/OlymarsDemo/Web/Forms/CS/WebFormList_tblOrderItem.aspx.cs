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
	/// Summary description for WebFormList_tblOrderItem.
	/// </summary>
	public class WebFormList_tblOrderItem: System.Web.UI.Page
	{
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebFormList_tblOrderItem() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString = String.Empty;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.Repeaters.WebRepeaterCustom_spS_tblOrderItem_SelectDisplay repeater_tblOrderItem_SelectDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblOrder com_Oit_GuidOrderID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.DropDownLists.WebDropDownList_tblProduct com_Oit_GuidProductID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Oit_GuidOrderID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Oit_GuidProductID;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Oit_LngAmount;
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

				// com_Oit_GuidOrderID
				System.Data.SqlTypes.SqlGuid colOit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
				if (Request.Params["Oit_GuidOrderID"] != String.Empty) {
				
					try {
					
						colOit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Parse(Request.Params["Oit_GuidOrderID"]);
					}
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}
				com_Oit_GuidOrderID.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				try {

					com_Oit_GuidOrderID.RefreshData(colOit_GuidOrderID);
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
				com_Oit_GuidOrderID.Items.Insert(0, "Show all");

				// com_Oit_GuidProductID
				System.Data.SqlTypes.SqlGuid colOit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
				if (Request.Params["Oit_GuidProductID"] != String.Empty) {
				
					try {
					
						colOit_GuidProductID = System.Data.SqlTypes.SqlGuid.Parse(Request.Params["Oit_GuidProductID"]);
					}
					catch {
					
						// Ignore the parameter and do nothing here
					}
				}
				com_Oit_GuidProductID.Initialize(ConnectionString, System.Data.SqlTypes.SqlInt32.Null);
				try {

					com_Oit_GuidProductID.RefreshData(colOit_GuidProductID);
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
				com_Oit_GuidProductID.Items.Insert(0, "Show all");

				// Any sort preferences?
				CurrentSortEnum sortColumn = CurrentSortEnum.SortBy_Oit_GuidOrderID;
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

				if (ViewState["WebFormList_tblOrderItem_CurrentSort"] == null) {

					ViewState.Add("WebFormList_tblOrderItem_CurrentSort", sortColumn);
				}
				else {

					ViewState["WebFormList_tblOrderItem_CurrentSort"] = sortColumn;
				}

				if (ViewState["sortOrder"] == null) {

					ViewState.Add("sortOrder", sortOrder);
				}
				else {

					ViewState["sortOrder"] = sortOrder;
				}
			}

			repeater_tblOrderItem_SelectDisplay.EnableViewState = true;

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

			this.com_Oit_GuidOrderID.SelectedIndexChanged += new System.EventHandler(this.com_Oit_GuidOrderID_SelectedIndexChanged);
			this.com_Oit_GuidProductID.SelectedIndexChanged += new System.EventHandler(this.com_Oit_GuidProductID_SelectedIndexChanged);
			this.Label_Oit_GuidOrderID.Click += new System.EventHandler(this.Label_Oit_GuidOrderID_Click);
			this.Label_Oit_GuidProductID.Click += new System.EventHandler(this.Label_Oit_GuidProductID_Click);
			this.Label_Oit_LngAmount.Click += new System.EventHandler(this.Label_Oit_LngAmount_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private enum CurrentSortEnum {

			  SortBy_Oit_GuidOrderID
			, SortBy_Oit_GuidProductID
			, SortBy_Oit_LngAmount
		}

		private enum SortOrderEnum {

			Ascending, Descending
		}

		private void com_Oit_GuidOrderID_SelectedIndexChanged(object sender, System.EventArgs e) {

			RefreshList();
		}

		private void com_Oit_GuidProductID_SelectedIndexChanged(object sender, System.EventArgs e) {

			RefreshList();
		}

		private void RefreshList() {

			System.Data.SqlTypes.SqlGuid Oit_GuidOrderID = System.Data.SqlTypes.SqlGuid.Null;
			if (com_Oit_GuidOrderID.SelectedIndex != 0) {

				Oit_GuidOrderID = new System.Data.SqlTypes.SqlGuid(new System.Guid(com_Oit_GuidOrderID.SelectedItem.Value));
			}

			System.Data.SqlTypes.SqlGuid Oit_GuidProductID = System.Data.SqlTypes.SqlGuid.Null;
			if (com_Oit_GuidProductID.SelectedIndex != 0) {

				Oit_GuidProductID = new System.Data.SqlTypes.SqlGuid(new System.Guid(com_Oit_GuidProductID.SelectedItem.Value));
			}

			repeater_tblOrderItem_SelectDisplay.Initialize(ConnectionString, System.Data.SqlTypes.SqlGuid.Null, Oit_GuidOrderID, Oit_GuidProductID);
			try {

				repeater_tblOrderItem_SelectDisplay.RefreshData();
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblOrderItem' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblOrderItem' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}
			System.Data.DataView dataView = (System.Data.DataView)repeater_tblOrderItem_SelectDisplay.DataSource;

			string SortDirective = String.Empty;
			switch ((CurrentSortEnum)ViewState["WebFormList_tblOrderItem_CurrentSort"]) {

				case CurrentSortEnum.SortBy_Oit_GuidOrderID:
					SortDirective = spS_tblOrderItem_SelectDisplay.Resultset1.Fields.Column_Oit_GuidOrderID_Display.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Oit_GuidProductID:
					SortDirective = spS_tblOrderItem_SelectDisplay.Resultset1.Fields.Column_Oit_GuidProductID_Display.ColumnName;
					break;

				case CurrentSortEnum.SortBy_Oit_LngAmount:
					SortDirective = spS_tblOrderItem.Resultset1.Fields.Column_Oit_LngAmount.ColumnName;
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
			repeater_tblOrderItem_SelectDisplay.DataBind();
		}

		private void Label_Oit_GuidOrderID_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrderItem_CurrentSort"] == CurrentSortEnum.SortBy_Oit_GuidOrderID) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrderItem_CurrentSort"] = CurrentSortEnum.SortBy_Oit_GuidOrderID;
			}

			RefreshList();
		}

		private void Label_Oit_GuidProductID_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrderItem_CurrentSort"] == CurrentSortEnum.SortBy_Oit_GuidProductID) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrderItem_CurrentSort"] = CurrentSortEnum.SortBy_Oit_GuidProductID;
			}

			RefreshList();
		}

		private void Label_Oit_LngAmount_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblOrderItem_CurrentSort"] == CurrentSortEnum.SortBy_Oit_LngAmount) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblOrderItem_CurrentSort"] = CurrentSortEnum.SortBy_Oit_LngAmount;
			}

			RefreshList();
		}

	}
}
