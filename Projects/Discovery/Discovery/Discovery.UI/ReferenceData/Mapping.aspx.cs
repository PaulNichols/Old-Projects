using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.BusinessObjects;
using Discovery.BusinessObjects.Controllers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Discovery.UI.Web.Mapping
{
    /*************************************************************************************************
   ** CLASS:	Mapping
   **
   ** OVERVIEW:
   ** This page allows a user to view all mappings
   **
   ** MODIFICATION HISTORY:
   **
   ** Date:		Version:    Who:	Change:
   ** 19/7/06	1.0			PJN		Initial Version
   ************************************************************************************************/
    public partial class Mapping : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Private Method(s)
            

        /// <summary>
        /// Gets the drop down list destination property control.
        /// </summary>
        /// <returns></returns>
        private DropDownList GetDropDownListDestinationPropertyControl()
        {
            return GetControl<DropDownList>("DropDownListDestinationProperty",PageFormView);
        }

        /// <summary>
        /// Gets the drop down list destination type control.
        /// </summary>
        /// <returns></returns>
        private DropDownList GetDropDownListDestinationTypeControl()
        {
            return GetControl<DropDownList>("DropDownListDestinationType", PageFormView);
        }

        /// <summary>
        /// Gets the drop down list source type control.
        /// </summary>
        /// <returns></returns>
        private DropDownList GetDropDownListSourceTypeControl()
        {
            return GetControl<DropDownList>("DropDownListSourceType", PageFormView);
        }

        /// <summary>
        /// Gets the drop down list source property control.
        /// </summary>
        /// <returns></returns>
        private DropDownList GetDropDownListSourcePropertyControl()
        {
            return GetControl<DropDownList>("DropDownListSourceProperty", PageFormView);
        }

        /// <summary>
        /// Populates the source property drop down control.
        /// </summary>
        /// <param name="dropDownListDestinationType">Type of the drop down list destination.</param>
        private void PopulateSourcePropertyDropDownControl(DropDownList dropDownListDestinationType)
        {
            List<MappingPropertyAssociation> dataSource = new List<MappingPropertyAssociation>();
            try
            {
                int mappingClassAssociationId = Convert.ToInt32(dropDownListDestinationType.SelectedValue);
                dataSource =
                    MappingController.GetMappingPropertyAssociationsByClassAssociationId(mappingClassAssociationId);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to retrieve data");
            }

            DropDownList dropDownListSourceProperty = GetDropDownListSourcePropertyControl();


            dropDownListSourceProperty.Items.Clear();
            if (dataSource.Count != 1)
            {
                
                dropDownListSourceProperty.Items.Add(new ListItem(null, null));
            }
            foreach (MappingPropertyAssociation association in dataSource)
            {
                if (dropDownListSourceProperty.Items.FindByText(association.SourceProperty) == null)
                {
                    dropDownListSourceProperty.Items.Add(new ListItem(association.SourceProperty, association.SourceProperty));
                }
            }
            //dropDownListSourceProperty.DataSource = dataSource;
            //dropDownListSourceProperty.DataBind();
            if (dropDownListSourceProperty.SelectedValue != null && dropDownListSourceProperty.SelectedValue != "")
            {
                PopulateDestinationPropertyDropDownControl(GetDropDownListDestinationPropertyControl());
            }
        }

        /// <summary>
        /// Populates the destination property drop down control.
        /// </summary>
        /// <param name="dropDownListDestinationProperty">The drop down list destination property.</param>
        private void PopulateDestinationPropertyDropDownControl(DropDownList dropDownListDestinationProperty)
        {
            string sourceProperty = GetDropDownListSourcePropertyControl().SelectedValue.ToString();
            List<MappingPropertyAssociation> dataSource = null;
            try
            {
               dataSource=MappingController.GetMappingPropertyAssociations(sourceProperty, Convert.ToInt32(GetDropDownListDestinationTypeControl().SelectedValue));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "User Interface")) DisplayMessage("Failed to retrieve data");
            }

            dropDownListDestinationProperty.Items.Clear();
            if (dataSource.Count != 1)
            {
                dropDownListDestinationProperty.Items.Add(new ListItem(null, null));
            }
            foreach (MappingPropertyAssociation association in dataSource)
            {
                //if the item does not already exist then add it
                if (dropDownListDestinationProperty.Items.FindByText(association.DestinationProperty)==null)
                {
                    dropDownListDestinationProperty.Items.Add(new ListItem(association.DestinationProperty, association.Id.ToString()));
                }
            }
            //dropDownListDestinationProperty.DataSource = dataSource;
            //dropDownListDestinationProperty.DataBind();
            if (dropDownListDestinationProperty.SelectedValue != null &&
                dropDownListDestinationProperty.SelectedValue != "")
            {
                SetUpDestinationValueDropDownControl(dropDownListDestinationProperty);
            }
        }


        /// <summary>
        /// Sets the up destination value drop down control.
        /// </summary>
        /// <param name="dropDownListDestinationProperty">The drop down list destination property.</param>
        private void SetUpDestinationValueDropDownControl(DropDownList dropDownListDestinationProperty)
        {
            int mappingPropertyAssociationId = Convert.ToInt32(dropDownListDestinationProperty.SelectedValue);

            Dictionary<string, string> lookupItems =
                MappingController.GetMappingLookup(mappingPropertyAssociationId);

            DropDownList dropDownDestinationValue =GetControl<DropDownList>("DropDownListDestinationValue",PageFormView)  ;
            TextBox textBoxDestinationValue = GetControl<TextBox>("TextBoxDestinationValue",PageFormView)  ;
                     
            dropDownDestinationValue.Visible = (lookupItems.Count > 0);
            textBoxDestinationValue.Visible = (lookupItems.Count == 0);
           //if (dropDownDestinationValue.Visible)
           //{
           //       validation.AddValidation("DropDownListDestinationValue", "DestinationValue");
           //}

            if (dropDownDestinationValue.Visible)
            {
                dropDownDestinationValue.Items.Clear();
                if (lookupItems.Count != 1)
                {
                    dropDownDestinationValue.Items.Add(new ListItem(null, null));
                }
                //dropDownDestinationValue.DataSource = lookupItems;
                //dropDownDestinationValue.DataBind();
                foreach (KeyValuePair<string,string> kvp in lookupItems)
                {
                    dropDownDestinationValue.Items.Add(new ListItem(kvp.Value, kvp.Key));
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Handles the DataBound event of the DropDownListDestinationProperty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDestinationProperty_DataBound(object sender, EventArgs e)
        {
            DropDownList dropDownListDestinationProperty = (DropDownList) sender;
            FormView formView = (FormView) dropDownListDestinationProperty.NamingContainer;

            if (formView.DataItem != null)
            {
                string destinationProperty =
                    ((BusinessObjects.Mapping) formView.DataItem).MappingPropertyAssociation.DestinationProperty;

                dropDownListDestinationProperty.ClearSelection();

                ListItem listItem = dropDownListDestinationProperty.Items.FindByText(destinationProperty);
                if (listItem != null) listItem.Selected = true;
            }
        }

        /// <summary>
        /// Handles the ItemUpdating event of the MappingFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void MappingFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            AddValues(e.NewValues);
        }

        /// <summary>
        /// Handles the ItemInserting event of the MappingFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs"/> instance containing the event data.</param>
        protected void MappingFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            AddValues(e.Values);
        }

        /// <summary>
        /// Adds values before saving.
        /// </summary>
        /// <param name="values">The values.</param>
        private void AddValues(IOrderedDictionary values)
        {
            DropDownList dropDownListDestinationValue =
                GetControl<DropDownList>("DropDownListDestinationValue",PageFormView)  ;

            if (dropDownListDestinationValue.Visible)
            {
                values.Add("DestinationValue",
                             dropDownListDestinationValue.SelectedValue);
            }
            else
            {
                TextBox textBoxDestinationValue = GetControl<TextBox>("TextBoxDestinationValue",PageFormView)  ;
                values.Add("DestinationValue", textBoxDestinationValue.Text);
            }

            values.Add("mappingPropertyAssociationId",
                         GetDropDownListDestinationPropertyControl().SelectedValue);
        }

        /// <summary>
        /// Handles the DataBound event of the DropDownListSourceType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListSourceType_DataBound(object sender, EventArgs e)
        {
            DropDownList dropDownListSourceType = (DropDownList) sender;
            FormView formView = (FormView) dropDownListSourceType.NamingContainer;

            if (formView.DataItem != null)
            {
                string sourceType =
                    ((BusinessObjects.Mapping) formView.DataItem).MappingPropertyAssociation.MappingClassAssociation.
                        SourceType;

                dropDownListSourceType.ClearSelection();
                //be careful of the possibility that the value saved on the
                //database does not exist in the valid selections that are displayed
                //on the list
                ListItem li = dropDownListSourceType.Items.FindByText(sourceType);
                if (li != null)
                {
                    li.Selected = true;
                    PopulateDestinationTypeDropDownControl();
                }
            }
        }

        /// <summary>
        /// Handles the DataBound event of the DropDownListSourceProperty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListSourceProperty_DataBound(object sender, EventArgs e)
        {
            DropDownList dropDownListSourceProperty = (DropDownList)sender;
            FormView formView = (FormView)dropDownListSourceProperty.NamingContainer;

            if (formView.DataItem != null)
            {
                string sourceProperty =
                    ((BusinessObjects.Mapping)formView.DataItem).MappingPropertyAssociation.SourceProperty;

                dropDownListSourceProperty.ClearSelection();
                //be careful of the possibility that the value saved on the
                //database does not exist in the valid selections that are displayed
                //on the list
                ListItem li = dropDownListSourceProperty.Items.FindByText(sourceProperty);
                if (li != null)
                {
                    li.Selected = true;
                    PopulateDestinationPropertyDropDownControl(GetDropDownListDestinationPropertyControl());
                }
            }
        }

        /// <summary>
        /// Handles the DataBound event of the DropDownListDestinationType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDestinationType_DataBound(object sender, EventArgs e)
        {
            DropDownList dropDownListDestinationType = (DropDownList) sender;
            FormView formView = (FormView) dropDownListDestinationType.NamingContainer;

            if (formView.DataItem != null)
            {
                string DestinationType =
                    ((BusinessObjects.Mapping) formView.DataItem).MappingPropertyAssociation.MappingClassAssociation.
                        DestinationType;

                dropDownListDestinationType.ClearSelection();
                //be careful of the possibility that the value saved on the
                //database does not exist in the valid selections that are displayed
                //on the list
                ListItem li = dropDownListDestinationType.Items.FindByText(DestinationType);
                if (li != null) li.Selected = true;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListSourceType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDestinationTypeDropDownControl();
        }

        /// <summary>
        /// Populates the destination type drop down control.
        /// </summary>
        private void PopulateDestinationTypeDropDownControl()
        {
            DropDownList dropDownListSourceType = GetDropDownListSourceTypeControl();
            DropDownList dropdownListDestinationType = GetDropDownListDestinationTypeControl();

            List<MappingClassAssociation> dataSource =
                MappingController.GetMappingClassAssociationsBySourceType(dropDownListSourceType.SelectedValue);

            dropdownListDestinationType.Items.Clear();

            if (dataSource.Count != 1)
            {
                dropdownListDestinationType.Items.Add(new ListItem(null, null));
            }
            foreach (MappingClassAssociation association in dataSource)
            {
                dropdownListDestinationType.Items.Add(new ListItem(association.DestinationType, association.Id.ToString()));
            }
            //dropdownListDestinationType.DataSource = dataSource;
            //dropdownListDestinationType.DataBind();
            if (dropdownListDestinationType.SelectedValue != null && dropdownListDestinationType.SelectedValue != "")
            {
                PopulateSourcePropertyDropDownControl(dropdownListDestinationType);
                PopulateDestinationPropertyDropDownControl(GetDropDownListDestinationPropertyControl());
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDestinationType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDestinationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownListDestinataionType = (DropDownList) sender;

            // DropDownList dropDownListSourceProperty = GetDropDownListSourcePropertyControl();

            PopulateSourcePropertyDropDownControl(dropDownListDestinataionType);
        }


        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListSourceProperty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListSourceProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDestinationPropertyDropDownControl(GetDropDownListDestinationPropertyControl());
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDestinationProperty control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDestinationProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList downListDestinationProperty = (DropDownList) sender;
            SetUpDestinationValueDropDownControl(downListDestinationProperty);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Reference Data: View Mappings";
            CreateRule = "Reference Data: Edit Mappings";
            UpdateRule = "Reference Data: Edit Mappings";
            DeleteRule = "Reference Data: Edit Mappings";
            base.Page_Load(sender, e);
        }
        
        #endregion
      
        protected override void SetValidation()
        {
            base.SetValidation();

            //validation.AddValidation("TextBoxSourceValue", "SourceValue");
            //validation.AddValidation("TextBoxDestinationValue", "DestinationValue");
            //   validation.AddValidation("DropDownListDestinationValue", "DestinationValue");
            //validation.AddValidation("DropDownListSourceType", "SourceType");
            //validation.AddValidation("DropDownListDestinationType", "DestinationType");
            //validation.AddValidation("DropDownListSourceProperty", "SourceProperty");
            //validation.AddValidation("DropDownListDestinationProperty", "DestinationProperty");
            //validation.AddValidation("DropDownListDestinationSystem", "DestinationSystemId");
            //validation.AddValidation("DropDownListSourceSystem", "SourceSystemId");
        }


        /// <summary>
        /// Handles the DataBound event of the DropDownListDestinationValue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDestinationValue_DataBound(object sender, EventArgs e)
        {
            DropDownList dropDownListDestinationValue = (DropDownList) sender;
            FormView formView = (FormView) dropDownListDestinationValue.NamingContainer;

            if (formView.DataItem != null)
            {
                string destinationValue = ((BusinessObjects.Mapping) formView.DataItem).DestinationValue;

                dropDownListDestinationValue.ClearSelection();

                ListItem listItem = dropDownListDestinationValue.Items.FindByText(destinationValue);
                if (listItem != null) listItem.Selected = true;
            }
        }


    }
}