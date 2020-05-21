using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Discovery.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Routing
{
    public partial class OptrakGeneration : DiscoveryPage
    {

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Optrak: Generate Files";

            //call base class
            base.Page_Load(sender, e);
        }

        private enum StepsEnum
        {
            Define_Criteria = 0,
            Refine_Shipments = 1,
            Merge_Delivery_Points = 2,
            Finish = 3
        }

        private enum PeriodEnum
        {
            SameDay,
            NextDay
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                RoutingController.GenerateOptrakFiles(RoutingHistoryId.Value, ListBoxRegions.SelectedValue);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Could not generate Optrak files.", DiscoveryMessageType.Error);
            }
        }

        protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
        {
            switch ((StepsEnum)Wizard1.ActiveStepIndex)
            {
                case StepsEnum.Define_Criteria:
                    Wizard1.HeaderText = "Please define the criteria for the Shipments you wish to Route.<hr/>";
                    break;
                case StepsEnum.Refine_Shipments:
                    Wizard1.HeaderText = "Please select any Shipment you do not wish to Route.<hr/>";
                    break;
                case StepsEnum.Merge_Delivery_Points:
                    Wizard1.HeaderText = "Please merge any delivery points.<hr/>";
                    break;
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                if (e.CurrentStepIndex == (int)StepsEnum.Define_Criteria &&
                    e.NextStepIndex == (int)StepsEnum.Refine_Shipments)
                {
                    if (ListBoxRegions.SelectedValue == "")
                    {
                        DisplayMessage("Please Select a Region.");
                        RoutingHistoryId = null;
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        try
                        {
                            RoutingHistoryId = RoutingController.SetOptrakLocks(User.Identity.Name, ListBoxRegions.SelectedValue, (RoutingController.PeriodEnum) RadioButtonListPeriod.SelectedIndex );

                            if (RoutingHistoryId == -2)
                            {
                                //there are no shipments matching the criteria to lock so display message to user
                                DisplayMessage("No shipments were found to Route.");
                                RoutingHistoryId = null;
                                e.Cancel = true;
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, "User Interface");
                        }


                        if (RoutingHistoryId == null)
                        {
                            DisplayMessage(
                                "There was a problem locking the Shipments which match the specified criteria for Routing.",
                                DiscoveryMessageType.Error);
                            e.Cancel = true;
                        }
                        else
                        {
                            GridViewShipmentsToRefine.Sort("ShipmentName,pafpostcode,estimateddeliverydate",
                                                           SortDirection.Ascending);
                        }
                    }
                }

                else if (e.CurrentStepIndex == (int)StepsEnum.Refine_Shipments &&
                         e.NextStepIndex == (int)StepsEnum.Merge_Delivery_Points)
                {
                    List<TDCShipment> shipmentsToRemoveFromRouting = new List<TDCShipment>();

                    foreach (int shipmentId in ShipmentIdsToRemoveFromRouting)
                    {
                        TDCShipment tdcShipment = new TDCShipment();
                        tdcShipment.Id = shipmentId;
                        tdcShipment.Status = Shipment.StatusEnum.Routing;
                        shipmentsToRemoveFromRouting.Add(tdcShipment);
                    }
                    e.Cancel = !RemoveItemsFromRouting(shipmentsToRemoveFromRouting);
                    if (!e.Cancel)
                    {

                        //merge their delivery points using Customer Name and PAF postcode
                        bool mergeWorked = false;
                        try
                        {
                            mergeWorked = RoutingController.MergeDeliveryPointsAutomatically(RoutingHistoryId.Value);
                        }
                        catch (Exception ex)
                        {
                            ExceptionPolicy.HandleException(ex, "User Interface");
                        }
                        if (!mergeWorked)
                        {
                            DisplayMessage("Merging of Delivery points failed.");
                            e.Cancel = true;
                        }

                        MultiView1.ActiveViewIndex = 0;
                        GridViewMergedPoints.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage(ex);
            }
        }


        private bool RemoveItemsFromRouting(List<TDCShipment> shipmentsToRemoveFromRouting)
        {
            bool success = false;
            try
            {

                success = RoutingController.RemoveItemsFromRouting(shipmentsToRemoveFromRouting, User.Identity.Name,
                                                                   RoutingHistoryId.Value);
            }
            catch (Exception e)
            {
                if (ExceptionPolicy.HandleException(e, "User Interface")) DisplayMessage("There was a problem removing the selected Shipments.");
            }
            if (!success)
            {
                DisplayMessage("There was a problem removing the selected Shipments.");
            }
            else
            {
                ShipmentIdsToRemoveFromRouting.Clear();
            }
            return success;
        }

        /// <summary>
        /// Gets the routing history id. This is a unique id that groups shipments together for the current routing process
        /// </summary>
        /// <value>The routing history id.</value>
        private int? RoutingHistoryId
        {
            get
            {
                if (ViewState["RoutingHistoryId"] != null)
                {
                    return Convert.ToInt32(ViewState["RoutingHistoryId"]);
                }
                else
                {
                    return null;
                }
            }
            set { ViewState.Add("RoutingHistoryId", value); }
        }

        protected void ShipmentObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["routingHistoryId"] = RoutingHistoryId.Value;
            e.Arguments.MaximumRows = GridViewShipmentsToRefine.PageSize;
            e.Arguments.StartRowIndex = GridViewShipmentsToRefine.PageIndex;

        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            List<TDCShipment> shipmentsToRemoveFromRouting = new List<TDCShipment>();

            foreach (int shipmentId in ShipmentIdsToRemoveFromRouting)
            {
                TDCShipment tdcShipment = new TDCShipment();
                tdcShipment.Id = shipmentId;
                tdcShipment.Status = Shipment.StatusEnum.Routing;
                shipmentsToRemoveFromRouting.Add(tdcShipment);
            }
            if (RemoveItemsFromRouting(shipmentsToRemoveFromRouting))
            {
                GridViewShipmentsToRefine.DataBind();
                GridViewShipmentsToRefine.PageIndex = 0;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the CheckBoxMerge control. Gathers the Items that have been check for merger
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxMerge_CheckedChanged(object sender, EventArgs e)
        {
            //get the checkbox
            CheckBox mergeCheckBox = (CheckBox)sender;

            if (mergeCheckBox.Checked)
            {
                //get the sitecode of the line checked
                int id =
                    (int)GridViewMergedPoints.DataKeys[((GridViewRow)mergeCheckBox.NamingContainer).RowIndex].Value;
                siteCodesToMerge.Add(id);
            }
        }

        protected void CheckBoxRemove_CheckedChanged(object sender, EventArgs e)
        {
            //get the checkbox
            CheckBox removeCheckBox = (CheckBox)sender;
            //get the id of the shipment line checked
            int id =
                (int)GridViewShipmentsToRefine.DataKeys[((GridViewRow)removeCheckBox.NamingContainer).RowIndex].Value;


            if (removeCheckBox.Checked)
            {
                ShipmentIdsToRemoveFromRouting.Add(id);
            }
            else
            {
                ShipmentIdsToRemoveFromRouting.Remove(id);
            }
        }

        /// <summary>
        /// Gets the shipment ids to remove from routing. These have been built up as the user ticks the items
        /// </summary>
        /// <value>The shipment ids to remove from routing.</value>
        private List<int> ShipmentIdsToRemoveFromRouting
        {
            get
            {
                if (ViewState["ShipmentsToRemoveFromRouting"] == null)
                {
                    ViewState.Add("ShipmentsToRemoveFromRouting", new List<int>());
                }

                return ((List<int>)ViewState["ShipmentsToRemoveFromRouting"]);
            }
        }

        /// <summary>
        /// Gets the site codes to merge. These have been built up as the user ticks the items
        /// </summary>
        /// <value>The site codes to merge.</value>
        private List<int> siteCodesToMerge
        {
            get
            {
                if (ViewState["siteCodesToMerge"] == null)
                {
                    ViewState.Add("siteCodesToMerge", new List<int>());
                }
                ((List<int>)ViewState["siteCodesToMerge"]).Sort();
                return (List<int>)ViewState["siteCodesToMerge"];
            }
        }



        protected void GridViewMergedPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //re-check all checked items for the page of data the user is about to view
                bool check = (siteCodesToMerge.IndexOf((int)GridViewMergedPoints.DataKeys[e.Row.RowIndex].Value) > -1
                                  ? true
                                  : false);
                GetControl<CheckBox>("CheckBoxMerge", e.Row).Checked = check;
            }
        }

        protected void GridViewShipmentsToRefine_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //re-check all checked items for the page of data the user is about to view
                bool check =
                    (ShipmentIdsToRemoveFromRouting.IndexOf(
                         (int)GridViewShipmentsToRefine.DataKeys[e.Row.DataItemIndex].Value) > -1
                         ? true
                         : false);
                GetControl<CheckBox>("CheckBoxRemove", e.Row).Checked = check;
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex == (int)StepsEnum.Refine_Shipments &&
                e.NextStepIndex == (int)StepsEnum.Define_Criteria)
            {
                ////get all shipments that have been lock for routing  during this wizard and change their status back to Mapped 
                ////therefore unlocking them
                //List<TDCShipment> shipmentsToRemove = null;
                //try
                //{
                //   shipmentsToRemove = RoutingController.GetShipmentsByRoutingHistoryId(RoutingHistoryId.Value, false);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}

                if (!RoutingController.RemoveItemsFromRouting(null, User.Identity.Name,
                                                             RoutingHistoryId.Value))
                {
                    DisplayMessage("There was a problem un-locking the Shipments.");
                    e.Cancel = true;
                }
            }
            else if (e.CurrentStepIndex == (int)StepsEnum.Merge_Delivery_Points &&
                     e.NextStepIndex == (int)StepsEnum.Refine_Shipments)
            {
                siteCodesToMerge.Clear();
                GridViewShipmentsToRefine.DataBind();
            }
        }

        protected void ButtonMerge_Click(object sender, EventArgs e)
        {
            if (MultiView1.ActiveViewIndex == 0) //summary view
            {
                if (siteCodesToMerge.Count > 1)
                {
                    //merge the selected delivery points
                    int mainSiteCode = siteCodesToMerge[0];
                    siteCodesToMerge.RemoveAt(0);

                    int returnValue = RoutingController.MergeDeliveryPointsManually(RoutingHistoryId.Value, mainSiteCode, siteCodesToMerge);

                    switch (returnValue)
                    {
                        case 0:
                            siteCodesToMerge.Clear();
                            GridViewMergedPoints.DataBind();
                            break;
                        case -1:
                            DisplayMessage("Items from different delivery warehouse cannot be selected from merging.");
                            break;
                        case -2:
                        case -3:
                            DisplayMessage("Failed to Merge Items.");
                            break;
                    }

                }
                else
                {
                    DisplayMessage("You must select 2 or more items to merge.");
                }
            }
            else //detail view
            {
                ReturnToSummaryView();
            }
        }

        private void ReturnToSummaryView()
        {
            MultiView1.ActiveViewIndex = 0;
            GridViewMergedPoints.DataBind();
            ButtonMerge.Text = "Merge Selected Items";
        }

        protected void GridViewMergedPoints_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                MultiView1.ActiveViewIndex = 1;
                GridViewDeliveryPointDetail_Bind(e.CommandArgument as string);
                ViewState.Add("CurrentSiteCode", e.CommandArgument as string);
                ButtonMerge.Text = "Hide Detail";
                siteCodesToMerge.Clear();
            }
        }

        protected void GridViewDeliveryPointDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UnMerge")
            {
                RoutingController.UnMerge(Convert.ToInt32(e.CommandArgument), RoutingHistoryId.Value);
                //rebind the grid if there's more than 1 in the group before unmerging.
                //If there was only 1 the reshuffeling that occurs in the Unmerge method will mean we will see the new and wrong group of data in the grid
                if (GridViewDeliveryPointDetail.Rows.Count > 1)
                {
                    GridViewDeliveryPointDetail_Bind(ViewState["CurrentSiteCode"] as string);
                }
                else
                {
                    ReturnToSummaryView();
                }
            }
        }

        /// <summary>
        /// Binds the GridViewDeliveryPointDetail grid with data for the chosen site code, it shows a break down
        /// </summary>
        /// <param name="locationCode">The location code.</param>
        private void GridViewDeliveryPointDetail_Bind(string locationCode)
        {
            GridViewDeliveryPointDetail.DataSource =
                RoutingController.GetShipmentsForDeliveryPoint(locationCode, RoutingHistoryId.Value, false);
            GridViewDeliveryPointDetail.DataBind();
        }


        /// <summary>
        /// Handles the SideBarButtonClick event of the Wizard1 control. Cancel=True to stop the bar from doing anything because this isn't 
        /// required an messaes up the logic in the next and previous buttons
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs"/> instance containing the event data.</param>
        protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
        {
            e.Cancel = true;
        }


        protected void MergedShipmentObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["routingHistoryId"] = RoutingHistoryId.Value;
        }


        protected void SetUserDefaultRegion_DataBound(object sender, EventArgs e)
        {
            SetUserDefaultRegion(ListBoxRegions, Profile);
        }

    }
}