using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Discovery.Integration;

namespace Discovery.UI.Web.Admin
{
    public partial class Connection : DiscoveryDataDetailPage
    {
        #region Properties

        #endregion

        #region Protected Methods

        #endregion

        #region Events

        #endregion

        protected override void SetValidation()
        {
            base.SetValidation();
            //validation.AddValidation("TextBoxCode", "Code");
            //validation.AddValidation("TextBoxDescription", "Description");
        }


        protected void DropDownListChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the correct view to be visible depending on the type of connection we are adding
            //the views hold either ftp or msmq settings
            GetControl<MultiView>("MultiViewSettings", PageFormView).ActiveViewIndex =
                (int)Enum.Parse(typeof(Integration.Connection.ChannelTypeEnum), GetControl<DropDownList>("DropDownListChannel", PageFormView).SelectedValue);
        }

        protected void OpCoFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            AddValues(e.Values);
        }

        private void AddValues(IOrderedDictionary values)
        {
            //the settings property holds either MSMQ or FTP settings
            //and as it is a "special" property we need to help the databinding a little
            MultiView multiView = GetControl<MultiView>("MultiViewSettings", PageFormView);
            ConnectionSettings connectionSettings;
            if (
                (Integration.Connection.ChannelTypeEnum)Enum.Parse(typeof(Integration.Connection.ChannelTypeEnum), GetControl<DropDownList>("DropDownListChannel", PageFormView).SelectedValue) ==
                Integration.Connection.ChannelTypeEnum.MSMQ)
            {
                connectionSettings = new MSMQConnectionSettings();

                ((MSMQConnectionSettings) connectionSettings).QueueName =
                    GetControl<DropDownList>("DropDownListQueues", multiView).Text;
            }
            else
            {
                connectionSettings = new FTPConnectionSettings();

                ((FTPConnectionSettings) connectionSettings).Port = Convert.ToInt32(GetControl<TextBox>("TextBoxPort", multiView).Text);
                ((FTPConnectionSettings) connectionSettings).IpAddress =
                    GetControl<TextBox>("TextBoxIPAddress", multiView).Text;
                ((FTPConnectionSettings) connectionSettings).Password =
                    GetControl<TextBox>("TextBoxPassword", multiView).Text;
                ((FTPConnectionSettings) connectionSettings).Username =
                    GetControl<TextBox>("TextBoxUserName", multiView).Text;

                ((FTPConnectionSettings)connectionSettings).ErrorCount =
                   Convert.ToInt32(GetControl<TextBox>("TextBoxErrorCount", multiView).Text);
            }
            values.Add("Settings", connectionSettings);
        }


        protected void OpCoFormView_DataBound(object sender, EventArgs e)
        {
            if (PageFormView.DataItem!=null && (PageFormView.CurrentMode == FormViewMode.ReadOnly || PageFormView.CurrentMode == FormViewMode.Edit))
            {
                //make sure the correct view is shown and therefor the correct connection settings
                MultiView multiView = GetControl<MultiView>("MultiViewSettings", PageFormView);
                Integration.Connection connection = (Integration.Connection) PageFormView.DataItem;
                Integration.Connection.ChannelTypeEnum channelTypeEnum = (connection.ChannelType);

                multiView.ActiveViewIndex = Convert.ToInt32(channelTypeEnum);
                
                if (PageFormView.CurrentMode == FormViewMode.ReadOnly)
                {
                    if (channelTypeEnum == Integration.Connection.ChannelTypeEnum.FTP)
                    {
                        FTPConnectionSettings ftpConnection = (FTPConnectionSettings)connection.Settings;
                        GetControl<Label>("LabelIPAddress", multiView).Text = ftpConnection.IpAddress;
                        GetControl<Label>("LabelPort", multiView).Text = ftpConnection.Port.ToString();
                        GetControl<Label>("LabelUserName", multiView).Text = ftpConnection.Username;
                        GetControl<Label>("LabelErrorCount", multiView).Text = ftpConnection.ErrorCount.ToString();

                    }
                    else
                    {
                        MSMQConnectionSettings msmqConnection = (MSMQConnectionSettings)connection.Settings;
                        GetControl<Label>("LabelQueueName", multiView).Text = msmqConnection.QueueName;
                    }
                }
                else
                {
                    if (channelTypeEnum == Integration.Connection.ChannelTypeEnum.FTP)
                    {
                        FTPConnectionSettings ftpConnection = (FTPConnectionSettings)connection.Settings;
                        GetControl<TextBox>("TextBoxIPAddress", multiView).Text = ftpConnection.IpAddress;
                        GetControl<TextBox>("TextBoxPort", multiView).Text = ftpConnection.Port.ToString();
                        GetControl<TextBox>("TextBoxUserName", multiView).Text = ftpConnection.Username;
                        GetControl<TextBox>("TextBoxPassword", multiView).Text = ftpConnection.Password;    
                        GetControl<TextBox>("TextBoxErrorCount", multiView).Text = ftpConnection.ErrorCount.ToString();    
                    }
                    else
                    {
                        MSMQConnectionSettings msmqConnection = (MSMQConnectionSettings)connection.Settings;
                        GetControl<DropDownList>("DropDownListQueues", multiView).Text = msmqConnection.QueueName;
                    }
                }
                    
            }
          
        }


        protected void OpCoFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            AddValues(e.NewValues);
        }
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            ReadRule = "Integration: View Connections";
            CreateRule = "Integration: Edit Connections";
            DeleteRule = "Integration: Edit Connections";
            UpdateRule = "Integration: Edit Connections";

            //call base functionality
            base.Page_Load(sender, e);
        }
        
    }
}