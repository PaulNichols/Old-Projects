using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Discovery.UI.Web.Admin
{
    public partial class Task : DiscoveryDataDetailPage
    {
        //used to give the page numbers a more meaningful description
        private enum StepEnum
        {
            Start,
            SourceConnection,
            DestinationConnection,
            SourceConnectionDetails,
            DestinationConnectionDetails
        }

        #region Properties

        /// <summary>
        /// Gets the task wizard. Helper method
        /// </summary>
        /// <returns></returns>
        private Wizard TaskWizard
        {
            get { return GetControl<Wizard>("TaskWizard", PageFormView); }
        }

        private bool SourceChannelIsMSMQ
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ChannelType == Integration.Connection.ChannelTypeEnum.MSMQ;
                }
                else
                {
                    return
                        ((Integration.Connection.ChannelTypeEnum)Convert.ToInt32(SourceChannelTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ChannelTypeEnum.MSMQ;
                }
            }
        }

        private bool DestinationChannelIsMSMQ
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ChannelType == Integration.Connection.ChannelTypeEnum.MSMQ;
                }
                else
                {
                    return
                        ((Integration.Connection.ChannelTypeEnum)
                         Convert.ToInt32(DestinationChannelTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ChannelTypeEnum.MSMQ;
                }
            }
        }

        private bool DestinationChannelIsFTP
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ChannelType == Integration.Connection.ChannelTypeEnum.FTP;
                }
                else
                {
                    return
                        ((Integration.Connection.ChannelTypeEnum)
                         Convert.ToInt32(DestinationChannelTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ChannelTypeEnum.FTP;
                }
            }
        }

        private bool SourceChannelIsFTP
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ChannelType == Integration.Connection.ChannelTypeEnum.FTP;
                }
                else
                {
                    return
                        ((Integration.Connection.ChannelTypeEnum)Convert.ToInt32(SourceChannelTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ChannelTypeEnum.FTP;
                }
            }
        }

        private bool SourceTypeIsERP
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Shipment;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(SourceConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Shipment;
                }
            }
        }

        private bool DestinationTypeIsERP
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Shipment;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(DestinationConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Shipment;
                }
            }
        }

        private bool DestinationTypeIsOptrak
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Optrak;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(DestinationConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Optrak;
                }
            }
        }

        private bool DestinationTypeIsCommander
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Commander;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(DestinationConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Commander;
                }
            }
        }

        private bool SourceTypeIsOptrak
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Optrak;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(SourceConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Optrak;
                }
            }
        }

        private bool SourceTypeIsCommander
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.Commander;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(SourceConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.Commander;
                }
            }
        }
        private bool SourceTypeIsMS
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).SourceConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.MS;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(SourceConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.MS;
                }
            }
        }

        private bool DestinationTypeIsMS
        {
            get
            {
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    return ((Integration.Task)PageFormView.DataItem).DestinationConnection.ConnectionType == Integration.Connection.ConnectionTypeEnum.MS;
                }
                else
                {
                    return
                        ((Integration.Connection.ConnectionTypeEnum)
                         Convert.ToInt32(DestinationConnectionTypeDropDownList.SelectedValue)) ==
                        Integration.Connection.ConnectionTypeEnum.MS;
                }
            }
        }

        protected DropDownList DestinationConnectionDropDownList
        {
            get
            {
                //rebind the destination connections dropdwon list because the connection type fileter has changed
                return GetControl<DropDownList>("DropDownListDestinationConnection", TaskWizard);
            }
        }

        /// <summary>
        /// Gets the source connection drop down list. Helper method
        /// </summary>
        /// <returns></returns>
        protected DropDownList SourceConnectionDropDownList
        {
            get { return GetControl<DropDownList>("DropDownListConnection", TaskWizard); }
        }

        /// <summary>
        /// Gets the source channel type drop down list. Helper method
        /// </summary>
        /// <returns></returns>
        protected DropDownList SourceChannelTypeDropDownList
        {
            get { return GetControl<DropDownList>("DropDownListChannelType", TaskWizard); }
        }

        /// <summary>
        /// Gets the destination channel type drop down list. Helper method
        /// </summary>
        /// <returns></returns>
        protected DropDownList DestinationChannelTypeDropDownList
        {
            get { return GetControl<DropDownList>("DropDownListDestinationChannelType", TaskWizard); }
        }

        /// <summary>
        /// 
        /// Gets the destination connection type drop down list. Helper method
        /// </summary>
        /// <returns></returns>
        protected DropDownList DestinationConnectionTypeDropDownList
        {
            get { return GetControl<DropDownList>("DropDownListDestinationConnectionType", TaskWizard); }
        }

        /// <summary>
        /// Gets the source connection type drop down list. Helper method
        /// </summary>
        /// <returns></returns>
        protected DropDownList SourceConnectionTypeDropDownList
        {
            get { return GetControl<DropDownList>("DropDownListConnectionType", TaskWizard); }
        }

        #endregion

        #region Methods

        protected override void SetValidation()
        {
            base.SetValidation();
            //validation.AddValidation("TextBoxCode", "Code");
            //validation.AddValidation("TextBoxDescription", "Description");
        }

        private void AddValues(IOrderedDictionary values)
        {
            //gather all the details from the wizard, there seems to be a bug with the databinding 
            //so I've had to do this manually

            //find the wizard
            Wizard wizard = TaskWizard;
            bool destinationChannelIsMSMQ = DestinationChannelIsMSMQ;
            bool sourceTypeIsERP = SourceTypeIsERP;
            bool sourceTypeIsOptrak = SourceTypeIsOptrak;


            //read values from controls
            values["Name"] = GetControl<TextBox>("TextBoxName", wizard).Text;
            values["RemoveDataFile"] = GetControl<CheckBox>("CheckBoxRemoveDataFile", wizard).Checked;
            values["RemoveFlagFile"] = GetControl<CheckBox>("CheckBoxRemoveFlagFile", wizard).Checked;
            values["DataFileExtension"] = GetControl<TextBox>("TextBoxDataFileExtension", wizard).Text;
            values["FlagFileExtension"] = GetControl<TextBox>("TextBoxFlagFileExtension", wizard).Text;
            values["MonitorSequenceNumber"] = GetControl<CheckBox>("CheckBoxMonitorSequenceNumber", wizard).Checked;
            string sequenceNumber = GetControl<TextBox>("TextBoxSequenceNumber", wizard).Text;
            if (!string.IsNullOrEmpty(GetControl<TextBox>("TextBoxSequenceNumber", wizard).Text))
            {
                values["SequenceNumber"] = sequenceNumber;
            }
            else
            {
                values["SequenceNumber"] = 0;
            }
            values["SourceDirectory"] = GetControl<TextBox>("TextBoxSourceDirectory", wizard).Text;
            values["DestinationDirectory"] = GetControl<TextBox>("TextBoxDestinationDirectory", wizard).Text;
            values["SourceConnectionId"] = GetControl<DropDownList>("DropDownListConnection", wizard).SelectedValue;
            if ((sourceTypeIsERP || SourceTypeIsCommander) && destinationChannelIsMSMQ)
            {
                values["SourceConnectionIdentifier"] =
                    GetControl<DropDownList>("DropDownListSourceIdentifierOpco", wizard).SelectedValue;
            }
            else if (sourceTypeIsOptrak && destinationChannelIsMSMQ)
            {
                values["SourceConnectionIdentifier"] =
                   GetControl<DropDownList>("DropDownListSourceIdentifierRegion", wizard).SelectedValue;
            }
            else if (SourceChannelIsMSMQ && SourceTypeIsMS)
            {
                values["SourceConnectionIdentifier"] = "MS";
            }

            if (DestinationTypeIsOptrak  && SourceChannelIsMSMQ)
            {
                values["DestinationConnectionIdentifier"] =
                   GetControl<DropDownList>("DropDownListDestinationIdentifierRegion", wizard).SelectedValue;
            }
            else if (DestinationTypeIsCommander && SourceChannelIsMSMQ)
            {
                values["DestinationConnectionIdentifier"] =
                   GetControl<DropDownList>("DropDownListDestinationIdentifierWarehouse", wizard).SelectedValue;
            }
            else if (DestinationChannelIsMSMQ && DestinationTypeIsMS)
            {
                values["DestinationConnectionIdentifier"] = "MS";
            }

            values["DestinationConnectionId"] =
                GetControl<DropDownList>("DropDownListDestinationConnection", wizard).SelectedValue;

            values["Frequency"] = GetControl<TextBox>("TextBoxFrequency", wizard).Text;
        }

        #endregion

        #region Event Handlers

        protected void TaskWizard_ActiveStepChanged(object sender, EventArgs e)
        {
            Wizard wizard = TaskWizard;

            //set the visibility of some of the settings to make the wizard more usable

            switch ((StepEnum)wizard.ActiveStepIndex)
            {
                case StepEnum.SourceConnectionDetails:
                    {
                        //set the summary details
                        GetControl<Literal>("LiteralSourceName", (Control)sender).Text = SourceConnectionDropDownList.SelectedItem.Text;
                        GetControl<Literal>("LiteralSourceChannel", (Control)sender).Text = SourceChannelTypeDropDownList.SelectedItem.Text;
                        GetControl<Literal>("LiteralSourceType", (Control)sender).Text = SourceConnectionTypeDropDownList.SelectedItem.Text;

                        SetSourceSettingsVisibility(wizard);
                        break;
                    }
                case StepEnum.DestinationConnectionDetails:
                    {
                        //set the summary details
                        GetControl<Literal>("LiteralDestinationName", (Control)sender).Text = DestinationConnectionDropDownList.SelectedItem.Text;
                        GetControl<Literal>("LiteralDestinationChannel", (Control)sender).Text = DestinationChannelTypeDropDownList.SelectedItem.Text;
                        GetControl<Literal>("LiteralDestinationType", (Control)sender).Text = DestinationConnectionTypeDropDownList.SelectedItem.Text;

                        SetDestinationSettingsVisibility(wizard);
                        break;
                    }
            }

        }

        protected void TaskFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            AddValues(e.Values);
        }

        protected void DropDownListConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rebind the source connections dropdwon list because the connection type fileter has changed
            SourceConnectionDropDownList.DataBind();
        }

        protected void DropDownListChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rebind the source connections dropdwon list because the channel type fileter has changed
            SourceConnectionDropDownList.DataBind();
        }

        protected void DropDownListDestinationConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rebind the connections dropdwon list because the connection type fileter has changed
            DestinationConnectionDropDownList.DataBind();
        }

        protected void DropDownListDestinationChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rebind the destination connections dropdwon list because the connection type fileter has changed
            DestinationConnectionDropDownList.DataBind();
        }


        protected void TaskFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            AddValues(e.NewValues);
        }

        private void SetDestinationSettingsVisibility(Control container)
        {
            bool destinationChannelIsFTP = DestinationChannelIsFTP;
            bool destinationChannelIsMSMQ = DestinationChannelIsMSMQ;
            bool sourceChannelIsMSMQ = SourceChannelIsMSMQ;
            bool sourceTypeIsERP = SourceTypeIsERP;

            GetControl<HtmlTableRow>("RowDestinationDirectory", container).Visible = destinationChannelIsFTP;

            HtmlTableRow row = GetControl<HtmlTableRow>("RowNA", container);
            if (row != null)
                row.Visible = destinationChannelIsMSMQ;

            row = GetControl<HtmlTableRow>("RowDestinationWarehouse", container);
            if (row != null)
                row.Visible = DestinationTypeIsCommander && sourceChannelIsMSMQ;

           row = GetControl<HtmlTableRow>("RowDestinationRegion", container);
            if (row != null)
                row.Visible = DestinationTypeIsOptrak && sourceChannelIsMSMQ;

            row = GetControl<HtmlTableRow>("RowDestinationOpco", container);
            if (row != null)
                row.Visible = DestinationTypeIsERP && sourceChannelIsMSMQ;
        }

        private void SetSourceSettingsVisibility(Control container)
        {
            bool sourceChannelIsMSMQ = SourceChannelIsMSMQ;
            bool sourceTypeIsERP = SourceTypeIsERP;
            bool sourceTypeIsOptrak = SourceTypeIsOptrak;
            bool sourceChannelIsFTP = SourceChannelIsFTP;
            bool destinationChannelIsMSMQ = DestinationChannelIsMSMQ;

            HtmlTableRow row = GetControl<HtmlTableRow>("RowSourceSettingsNa", container);
            if (row != null)
                row.Visible = sourceChannelIsMSMQ;

            row = GetControl<HtmlTableRow>("RowSourceRegion", container);
            if (row != null)
                row.Visible = sourceTypeIsOptrak && destinationChannelIsMSMQ;

                row = GetControl<HtmlTableRow>("RowSourceWarehouse", container);
            if (row != null)
                row.Visible = SourceTypeIsCommander && destinationChannelIsMSMQ;

            row = GetControl<HtmlTableRow>("RowSourceOpco", container);
            if (row != null)
                row.Visible = (sourceTypeIsERP || SourceTypeIsCommander) && destinationChannelIsMSMQ;

            GetControl<HtmlTableRow>("RowSourceDirectory", container).Visible = sourceChannelIsFTP;
            GetControl<HtmlTableRow>("RowFlagFileExtension", container).Visible = sourceChannelIsFTP && sourceTypeIsERP;
            GetControl<HtmlTableRow>("RowRemoveFlagFile", container).Visible = sourceChannelIsFTP && sourceTypeIsERP;
            GetControl<HtmlTableRow>("RowDataFileExtension", container).Visible = sourceChannelIsFTP;
            GetControl<HtmlTableRow>("RowRemoveDataFile", container).Visible = sourceChannelIsFTP;
            GetControl<HtmlTableRow>("RowMonitorSequenceNumber", container).Visible = sourceChannelIsFTP && sourceTypeIsERP;
            GetControl<HtmlTableRow>("RowCurrentSequenceNumber", container).Visible = sourceChannelIsFTP && sourceTypeIsERP;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Tasks";
            CreateRule = "Integration: Edit Tasks";
            DeleteRule = "Integration: Edit Tasks";
            UpdateRule = "Integration: Edit Tasks";

            //call base functionality
            base.Page_Load(sender, e);
        }

        #endregion


        protected void TaskFormView_DataBound(object sender, EventArgs e)
        {
            if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                SetSourceSettingsVisibility(PageFormView);
                SetDestinationSettingsVisibility(PageFormView);
            }
        }

        protected void DropDownListConnection_DataBound(object sender, EventArgs e)
        {
            if (TaskFormView.DataItem != null) ((DropDownList)sender).SelectedValue =
                ((Integration.Task)TaskFormView.DataItem).SourceConnectionId.ToString();
        }

        protected void DropDownListDestinationConnection_DataBound(object sender, EventArgs e)
        {
            if (TaskFormView.DataItem != null) ((DropDownList)sender).SelectedValue =
                ((Integration.Task)TaskFormView.DataItem).DestinationConnectionId.ToString();
        }

    protected void DropDownListSourceIdentifier_DataBound(object sender, EventArgs e)
        {
            if (TaskFormView.DataItem != null) ((DropDownList)sender).SelectedValue =((Integration.Task)TaskFormView.DataItem).SourceConnectionIdentifier;
        }

          protected void DropDownListDestinationIdentifier_DataBound(object sender, EventArgs e)
        {
            if (TaskFormView.DataItem != null) ((DropDownList)sender).SelectedValue =((Integration.Task)TaskFormView.DataItem).DestinationConnectionIdentifier;
        }
    }
}