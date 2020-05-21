using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
  ** CLASS:	LoadCategory
  **
  ** OVERVIEW:
  ** This page allows a uers to view, add, edit and delete a single Load Category
  **
  ** MODIFICATION HISTORY:
  **
  ** Date:		   Version:    Who:	    Change:
  ** 04-Oct-2006   1.0         TVL		Initial Version
  ************************************************************************************************/
    public partial class LoadCategory : DiscoveryDataDetailPage
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
            ReadRule = "Reference Data: View Load Categories";
            CreateRule = "Reference Data: Edit Load Categories";
            DeleteRule = "Reference Data: Edit Load Categories";
            UpdateRule = "Reference Data: Edit Load Categories";

            //call base class
            base.Page_Load(sender, e);
        }
        
        #endregion

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("TextBoxDescription", "Description");
            base.SetValidation();
        }

    }
}