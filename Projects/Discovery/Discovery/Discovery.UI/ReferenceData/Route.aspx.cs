using System;

namespace Discovery.UI.Web.ReferenceData
{
    /*************************************************************************************************
    ** CLASS:	Routes
    **
    ** OVERVIEW:
    ** This page displays all Routes
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public partial class Route : DiscoveryDataDetailPage
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
            ReadRule = "Reference Data: View Routes";
            CreateRule = "Reference Data: Edit Routes";
            DeleteRule = "Reference Data: Edit Routes";
            UpdateRule = "Reference Data: Edit Routes";

            //call base class
            base.Page_Load(sender, e);

            EnsureChildControls();
        }
        
        #endregion

        #region Protected Methods

        protected override void SetValidation()
        {
            Validation.AddValidation("TextBoxCode", "Code");
            Validation.AddValidation("TextBoxDescription", "Description");
            base.SetValidation();
        }

        #region CheckBoxes

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxIsSameDay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxIsSameDay_CheckedChanged(object sender, EventArgs e)
        {
            ////id the user checks IsSameDay then the IsNextDay control must be unticked
            //CheckBox checkBoxIsNextDay = GetControl<CheckBox>("CheckBoxIsNextDay",PageFormView) ;
            //if (((CheckBox)sender).Checked)
            //{
            //    checkBoxIsNextDay.Checked = false;
            //}
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxIsNextDay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxIsNextDay_CheckedChanged(object sender, EventArgs e)
        {
            ////id the user checks IsNextDay then the IsSameDay control must be unticked
            //CheckBox checkBoxIsSameDay = GetControl<CheckBox>("CheckBoxIsSameDay",PageFormView) ;
            //if (((CheckBox)sender).Checked)
            //{
            //    checkBoxIsSameDay.Checked = false;
            //}
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxIsCollection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxIsCollection_CheckedChanged(object sender, EventArgs e)
        {
            ////id the user checks IsCollection then the IsSpecial control must be unticked
            //CheckBox checkBoxIsSpecial = GetControl<CheckBox>("CheckBoxIsSpecial", PageFormView);
            //checkBoxIsSpecial.Checked = !((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxIsSpecial control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxIsSpecial_CheckedChanged(object sender, EventArgs e)
        {
            ////id the user checks IsSpecial then the IsCollection control must be unticked
            //CheckBox checkBoxIsCollection = GetControl<CheckBox>("CheckBoxIsCollection",PageFormView)  ;
            //checkBoxIsCollection.Checked = !((CheckBox)sender).Checked;
        }

        #endregion

        #endregion

      
    }
}