using System;
using System.Web.UI.WebControls;
using Discovery.ComponentServices.Security;
using System.Web.Security;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
 ** CLASS:	User
 **
 ** OVERVIEW:
 ** Page to allow maintenance of a single User
 **
 ** MODIFICATION HISTORY:
 **
 ** Date:		Version:    Who:	Change:
 ** 19/7/06		1.0			PJN		Initial Version
 ************************************************************************************************/

    public partial class User : DiscoveryDataDetailPage
    {
      

        /// <summary>
        /// Handles the DataBound event of the BulletedList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void BulletedList1_DataBound(object sender, EventArgs e)
        {
            if (PageFormView != null)
            {
                //find label that displays if user has no roles
                Label labelNoRelatedRoles = GetControl<Label>("LabelNoRelatedRoles", PageFormView);
                //find list of roles related to user
                BulletedList bulletedListRelatedRoles = GetControl<BulletedList>("BulletedListRelatedRoles", PageFormView);
                //set the visiblity oof the label depending whether the user has roles or not
                labelNoRelatedRoles.Visible = (bulletedListRelatedRoles.Items.Count == 0);
            }
        }


        /// <summary>
        /// Handles the DataBinding event of the LabelWarehouse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void LabelWarehouse_DataBinding(object sender, EventArgs e)
        {
            //translate id's of 0 to the text 'All' for opcos,region and warehouses
            UserDetails userDetails = PageFormView.DataItem as UserDetails;
            if (userDetails != null)
            {
                if (userDetails.WarehouseId == -1)
                {
                    Label labelWarehouse = GetControl<Label>("LabelWarehouse", PageFormView);
                    labelWarehouse.Text = "All";
                }

                if (userDetails.OpCoId == -1)
                {
                    Label labelOpCo = GetControl<Label>("LabelOpCo", PageFormView);
                    labelOpCo.Text = "All";
                }

                if (userDetails.SalesLocationId == -1)
                {
                    Label labelSalesBranch = GetControl<Label>("LabelSalesBranch", PageFormView);
                    labelSalesBranch.Text = "All";
                }

                if (userDetails.RegionId == -1)
                {
                    Label labelRegion = GetControl<Label>("LabelRegion", PageFormView);
                    labelRegion.Text = "All";
                }
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Admin: View Users";
            CreateRule = "Admin: Edit Users";
            DeleteRule = "Admin: Edit Users";
            UpdateRule = "Admin: Edit Users";

            //call base functionality
            base.Page_Load(sender, e);
        }

        /// <summary>
        /// Handles the ItemDeleted event of the PageFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs"/> instance containing the event data.</param>
        protected override void PageFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            BackUrl = "~/Admin/Security/users.aspx";
            base.PageFormView_ItemDeleted(sender, e);
        }

        protected override void DataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            BackUrl = "~/Admin/Security/user.aspx?Id={0}&UrlReferrer=~/Admin/Security/users.aspx?Id={0}";
            base.DataSource_Inserted(sender, e);
        }


        protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case ("ResetPassword"):
                    {
                        try
                        {
                            string generatedPassword;
                            string currentUser = FormView1.DataKey[0].ToString();
                            generatedPassword = Membership.GetUser(currentUser).ResetPassword();
                            if (string.IsNullOrEmpty(generatedPassword))
                            {
                                DisplayMessage("The password was not sucessfully reset.", DiscoveryMessageType.Error);
                            }
                            else
                            {
                                DisplayMessage(string.Format("The users password has been reset to '{0}'.", generatedPassword));
                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(ex.Message, DiscoveryMessageType.Error);
                        }


                        break;
                    }

            }
        }

        protected void DropDownListWarehouse_DataBound(object sender, EventArgs e)
        {
            int warehouseId = ((UserDetails)FormView1.DataItem).WarehouseId;
            BindDropDown(warehouseId, sender);
        }

        protected void DropDownListRegion_DataBound(object sender, EventArgs e)
        {
            int regionId = ((UserDetails)FormView1.DataItem).RegionId;
            BindDropDown(regionId, sender);
        }

        private static void BindDropDown(int idToBind, object sender)
        {
            if (idToBind > 0)
            {
                DropDownList dropDownList = (DropDownList)sender;
                ListItem foundItem = dropDownList.Items.FindByValue(idToBind.ToString());
                string pleaseSelectText = "<Please Select>";
                if (foundItem == null)
                {
                    if (dropDownList.Items[0].Text != pleaseSelectText)
                    {
                        foundItem = new ListItem(pleaseSelectText, "-2");
                        dropDownList.Items.Insert(0, foundItem);
                    }
                }

                if (dropDownList.SelectedItem != null)
                {
                    dropDownList.SelectedItem.Selected = false;
                }
                if (foundItem != null) foundItem.Selected = true;
            }
        }

        protected void DropDownListSalesBranch_DataBound(object sender, EventArgs e)
        {
            int salesBranchId = ((UserDetails)FormView1.DataItem).SalesLocationId;
            BindDropDown(salesBranchId, sender);
        }

        protected void DropDownListOpco_DataBound(object sender, EventArgs e)
        {
            int opcoId = ((UserDetails)FormView1.DataItem).OpCoId;
            BindDropDown(opcoId, sender);
        }

        protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            e.NewValues.Add("WarehouseId", GetControl<DropDownList>("DropDownListWarehouse", FormView1).SelectedValue);
            e.NewValues.Add("OpCoId", GetControl<DropDownList>("DropDownListOpCo", FormView1).SelectedValue);
            e.NewValues.Add("RegionId", GetControl<DropDownList>("DropDownListRegion", FormView1).SelectedValue);
            e.NewValues.Add("SalesLocationId", GetControl<DropDownList>("DropDownListSalesBranch", FormView1).SelectedValue);
        }

        protected override void SetValidation()
        {
            base.SetValidation();
            Validation.AddValidation("TextBoxEmail", "Email");
            Validation.AddValidation("DropDownListWarehouse", "WarehouseId");
            Validation.AddValidation("DropDownListOpCo", "OpCoId");
            Validation.AddValidation("DropDownListRegion", "RegionId");
            Validation.AddValidation("DropDownListSalesBranch", "SalesLocationId");
        }
    }
}