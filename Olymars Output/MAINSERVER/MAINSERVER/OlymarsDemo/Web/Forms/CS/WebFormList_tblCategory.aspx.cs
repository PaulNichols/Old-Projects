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
	/// Summary description for WebFormList_tblCategory.
	/// </summary>
	public class WebFormList_tblCategory: System.Web.UI.Page
	{
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		public WebFormList_tblCategory() {

			Page.Init += new System.EventHandler(Page_Init);
		}

		private string ConnectionString = String.Empty;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected OlymarsDemo.Web.Repeaters.WebRepeater_tblCategory repeater_tblCategory_SelectDisplay;
		/// <summary>
		/// [To be supplied.]
		/// </summary>
		protected System.Web.UI.WebControls.LinkButton Label_Cat_StrName;
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

				// Any sort preferences?
				CurrentSortEnum sortColumn = CurrentSortEnum.SortBy_Cat_StrName;
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

				if (ViewState["WebFormList_tblCategory_CurrentSort"] == null) {

					ViewState.Add("WebFormList_tblCategory_CurrentSort", sortColumn);
				}
				else {

					ViewState["WebFormList_tblCategory_CurrentSort"] = sortColumn;
				}

				if (ViewState["sortOrder"] == null) {

					ViewState.Add("sortOrder", sortOrder);
				}
				else {

					ViewState["sortOrder"] = sortOrder;
				}
			}

			repeater_tblCategory_SelectDisplay.EnableViewState = true;

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

			this.Label_Cat_StrName.Click += new System.EventHandler(this.Label_Cat_StrName_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private enum CurrentSortEnum {

			  SortBy_Cat_StrName
		}

		private enum SortOrderEnum {

			Ascending, Descending
		}

		private void RefreshList() {

			repeater_tblCategory_SelectDisplay.Initialize(ConnectionString);
			try {

				repeater_tblCategory_SelectDisplay.RefreshData();
			}
			catch (OlymarsDemo.DataClasses.CustomException customException) {

				if (customException.Parameter.SqlException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblCategory' class. Exception message is: {0}", customException.Parameter.SqlException.Message), customException);
				}
				else if (customException.Parameter.OtherException != null) {

					throw new Exception(String.Format("An exception has been thrown in the underlying DataClass that is used by the 'WebRepeater_tblCategory' class. Exception message is: {0}", customException.Parameter.OtherException.Message), customException);
				}
				else {

					throw;
				}
			}
			System.Data.DataView dataView = (System.Data.DataView)repeater_tblCategory_SelectDisplay.DataSource;

			string SortDirective = String.Empty;
			switch ((CurrentSortEnum)ViewState["WebFormList_tblCategory_CurrentSort"]) {

				case CurrentSortEnum.SortBy_Cat_StrName:
					SortDirective = spS_tblCategory.Resultset1.Fields.Column_Cat_StrName.ColumnName;
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
			repeater_tblCategory_SelectDisplay.DataBind();
		}

		private void Label_Cat_StrName_Click(object sender, System.EventArgs e) {

			if ((CurrentSortEnum)ViewState["WebFormList_tblCategory_CurrentSort"] == CurrentSortEnum.SortBy_Cat_StrName) {

				if ((SortOrderEnum)ViewState["sortOrder"] == SortOrderEnum.Ascending) {

					ViewState["sortOrder"] = SortOrderEnum.Descending;
				}
				else {

					ViewState["sortOrder"] = SortOrderEnum.Ascending;
				}
			}
			else {

				ViewState["WebFormList_tblCategory_CurrentSort"] = CurrentSortEnum.SortBy_Cat_StrName;
			}

			RefreshList();
		}

	}
}
