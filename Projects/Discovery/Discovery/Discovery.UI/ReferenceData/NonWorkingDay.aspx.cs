using System;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using RJS.Web.WebControl;

namespace Discovery.UI.Web.ReferenceData
{
    public partial class NonWorkingDay : DiscoveryDataDetailPage
    {
      
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {

            ReadRule = "Reference Data: View Non Working Days";
            CreateRule = "Reference Data: Edit Non Working Days";
            UpdateRule = "Reference Data: Edit Non Working Days";
            DeleteRule = "Reference Data: Edit Non Working Days";
     
            //call base class
            base.Page_Load(sender, e);

            // //this means that when the user clicks the textboes the calendars appear
            // txtStartDate.ReadOnly = true;
            //// txtStartDate.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtStartDate','ctl00_ContentPlaceHolder_PopCalendar1');");
            // txtEndDate.ReadOnly = true;
            // //txtEndDate.Attributes.Add("onclick", "PopCalShowCalendar('ctl00_ContentPlaceHolder_txtEndDate','ctl00_ContentPlaceHolder_PopCalendar1');");

            PopCalendar.JavaScriptCustomPath = "../App_Themes/DiscoveryDefault/";

         
        }
        protected void FormView_DataBound(object sender, EventArgs e)
        {
            if (PageFormView.CurrentMode == FormViewMode.Insert)
            {
                PopulateRegions();
            }
        }
        
        private void PopulateRegions()
        {
            // Get the region drop down
            DropDownList dropDownListRegion = GetControl<DropDownList>("ddlRegion", PageFormView);
            DropDownList dropDownListWarehouse = GetControl<DropDownList>("ddlWarehouse", PageFormView);

            // Populate the list of regions
            dropDownListRegion.DataSource = OptrakRegionController.GetRegions();
            dropDownListRegion.DataBind();

            // Now add the all
            dropDownListRegion.Items.Insert(0, new ListItem("All", "-1"));
            dropDownListWarehouse.Items.Insert(0, new ListItem("All", "-1"));

            // Now disable the warehouse
            dropDownListWarehouse.Items[0].Selected = true;
            dropDownListWarehouse.Enabled = false;
        }

        private void PopulateWarehouses()
        {
            // Get the region drop down
            DropDownList dropDownListRegion = GetControl<DropDownList>("ddlRegion", PageFormView);
            DropDownList dropDownListWarehouse = GetControl<DropDownList>("ddlWarehouse", PageFormView);

            dropDownListWarehouse.Items.Clear();
            
            if (dropDownListRegion.SelectedIndex > 0)
            {
                // Populate the list of regions
                try
                {
                    dropDownListWarehouse.DataSource =
                        WarehouseController.GetWarehousesByRegion(Convert.ToInt32(dropDownListRegion.SelectedItem.Value));
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to retrieve Warehouses");
                }
                dropDownListWarehouse.DataBind();
            }
            // Now add the all
            dropDownListWarehouse.Items.Insert(0, new ListItem("All", "-1"));

            // Enable the warehouse drop down
            dropDownListWarehouse.Enabled = (dropDownListWarehouse.Items.Count > 1);
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate the warehouses
            PopulateWarehouses();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TextBox TextBoxStartDate = GetControl<TextBox>("txtStartDate", PageFormView);
            TextBox TextBoxEndDate = GetControl<TextBox>("txtEndDate", PageFormView);

            //if (String.IsNullOrEmpty(txtStartDate.Text))
            //{
            //    DisplayMessage("Enter a start date");
            //    //bool setcursorto = txtStartDate.Focus();
            //    return;
            //}
            //if (String.IsNullOrEmpty(txtEndDate.Text))
            //{
            //    DisplayMessage("Enter a end date");
            //    //bool setcursorto = txtEndDate.Focus();
            //    return;
            //}

            int NoOfDays = 0;
            try
            {
                NoOfDays = NonWorkingDayController.CalculateNoOfDays(Convert.ToDateTime(TextBoxStartDate.Text),
                                                                     Convert.ToDateTime(TextBoxEndDate.Text));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to calculate number of days");
            }

            //if (NoOfDays > 365)
            //{
            //    ((CustomValidator)source).ErrorMessage="Date range must be less than one year ";
            //}
            //args.IsValid = (NoOfDays <= 365);
            //((CustomValidator)source).IsValid = (NoOfDays <= 365);
            if (NoOfDays > 365)
            {
                ((CustomValidator) source).ErrorMessage = "Date range must be less than one year";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void NonWorkingDayFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            //values.Add("startDate", GetControl<RJS.Web.WebControl.PopCalendar>("PopCalendarStartDate", PageFormView).DateValue);
            //values.Add("endDate", GetControl<RJS.Web.WebControl.PopCalendar>("PopCalendarEndDate", PageFormView).DateValue);
            //values.Add("description", GetControl<TextBox>("txtDescription", PageFormView).Text);
            //values.Add("regionId", Convert.ToInt32(GetControl<DropDownList>("ddlRegion", PageFormView).SelectedValue));
            //values.Add("warehouseId", Convert.ToInt32(GetControl<DropDownList>("ddlWarehouse", PageFormView).SelectedValue));
            //values.Add("weekendOnly", GetControl<CheckBox>("chkWeekEnd", PageFormView).Checked);

            DateTime StartDate = Convert.ToDateTime(GetControl<TextBox>("txtStartDate", PageFormView).Text);
            DateTime EndDate = Convert.ToDateTime(GetControl<TextBox>("txtEndDate", PageFormView).Text);
            string Description = GetControl<TextBox>("txtDescription", PageFormView).Text;
            int RegionId = Convert.ToInt32(GetControl<DropDownList>("ddlRegion", PageFormView).SelectedValue);
            // string WarehouseCode = GetControl<DropDownList>("ddlWarehouse", PageFormView).SelectedItem.Text;
            int WarehouseId = Convert.ToInt32(GetControl<DropDownList>("ddlWarehouse", PageFormView).SelectedValue);
            bool WeekendOnly = GetControl<CheckBox>("chkWeekEnd", PageFormView).Checked;
            string UpdatedBy = GetControl<HiddenField>("UpdatedBy", PageFormView).Value;

            //call controller
            try
            {
                int returnValue = NonWorkingDayController.SaveNonWorkingDays(
                    StartDate,
                    EndDate,
                    Description,
                    RegionId,
                    WarehouseId,
                    WeekendOnly,
                    UpdatedBy);
                if (returnValue != -1)
                {
                    DisplayMessage("Non-Working Days have been successfully added");
                    Response.Redirect(String.Format(BackUrl, (null == returnValue) ? "-1" : returnValue.ToString()));
                }
                else
                {
                    DisplayMessage("No details have been added");
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("No details have been added");
            }

            e.Cancel = true;
        }



    }
}