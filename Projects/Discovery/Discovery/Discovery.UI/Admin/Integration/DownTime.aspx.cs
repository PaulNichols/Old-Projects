using System;

namespace Discovery.UI.Web.Admin
{
    public partial class DownTime : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Down Times";
            CreateRule = "Integration: Edit Down Times";
            DeleteRule = "Integration: Edit Down Times";
            UpdateRule = "Integration: Edit Down Times";

            //call base functionality
            base.Page_Load(sender, e);
        }

        #endregion

        protected override void SetValidation()
        {
            base.SetValidation();
            Validation.AddValidation("TextBoxEndTime", "EndTime");
            Validation.AddValidation("TextBoxStartTime", "StartTime");
        }
    }
}