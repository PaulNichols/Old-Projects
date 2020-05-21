<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Connection.aspx.cs"
    Inherits="Discovery.UI.Web.Admin.Connection" Title="Connection Details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Connection Details</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />
    <asp:FormView EnableViewState="False" ID="OpCoFormView" runat="server" DataSourceID="ConnectionDataSource"
        DataKeyNames="Id"  OnItemInserting="OpCoFormView_ItemInserting"
        OnDataBound="OpCoFormView_DataBound" OnItemUpdating="OpCoFormView_ItemUpdating">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="Table3">
                <tr>
                    <td style="width: 120px">
                        Name:</td>
                    <td>
                        <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Name is required.">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        Active:</td>
                    <td>
                        <asp:CheckBox ID="CheckBoxActive" runat="server" Checked='<%# Bind("Active") %>' /></td>
                </tr>
                <tr>
                    <td>
                        Connection Type:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListType" runat="server" SelectedValue='<%# Bind("ConnectionType") %>'
                            Width="150px">
                            <asp:ListItem>(Please Select)</asp:ListItem>
                            <asp:ListItem>Shipment</asp:ListItem>
                            <asp:ListItem>Optrak</asp:ListItem>
                            <asp:ListItem>Commander</asp:ListItem>
                            <asp:ListItem Value="MS">Management Server</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        Channel Type:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListChannel" runat="server" SelectedValue='<%# Bind("ChannelType") %>'
                            Width="111px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChannel_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1">(Please Select)</asp:ListItem>
                            <asp:ListItem>FTP</asp:ListItem>
                            <asp:ListItem>MSMQ</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <asp:MultiView ID="MultiViewSettings" runat="server">
                <asp:View ID="View1" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                IP Address:</td>
                            <td>
                                <asp:TextBox ID="TextBoxIPAddress" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxIPAddress"
                                    ErrorMessage="IP Address is required." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Port:</td>
                            <td>
                                <asp:TextBox ID="TextBoxPort" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPort" ErrorMessage="Port is required."
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2"
                                        runat="server" ControlToValidate="TextBoxErrorCount" ErrorMessage="Port is not valid"
                                        Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                User Name:</td>
                            <td>
                                <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxUserName"
                                    ErrorMessage="User Name is required." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Password:</td>
                            <td>
                                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                Error Count:</td>
                            <td>
                                <asp:TextBox ID="TextBoxErrorCount" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxErrorCount"
                                    ErrorMessage="Error Count is required." SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToValidate="TextBoxErrorCount" ErrorMessage="Error count is not valid"
                                        Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                Queue Name:</td>
                            <td>
                                <asp:DropDownList ID="DropDownListQueues" runat="server" DataSourceID="ObjectDataSourceQueues"
                                    Width="245px">
                                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceQueues" runat="server"
                                    SelectMethod="GetQueues" TypeName="Discovery.Integration.IntegrationController">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView><asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender Enabled="false" ID="ToggleButtonExtender1" runat="server"
                TargetControlID="CheckBoxActive" CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table id="Table1">
                <tr>
                    <td style="width: 120px">
                        Name:</td>
                    <td>
                        <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Name is required.">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td>
                        Active:</td>
                    <td>
                        <asp:CheckBox ID="CheckBoxActive" runat="server" Checked='<%# Bind("Active") %>' /></td>
                </tr>
                <tr>
                    <td>
                        Connection Type:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListType" runat="server" SelectedValue='<%# Bind("ConnectionType") %>'
                            Width="150px">
                            <asp:ListItem>(Please Select)</asp:ListItem>
                            <asp:ListItem>Shipment</asp:ListItem>
                            <asp:ListItem>Optrak</asp:ListItem>
                            <asp:ListItem>Commander</asp:ListItem>
                            <asp:ListItem Value="MS">Management Server</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        Channel Type:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListChannel" runat="server" SelectedValue='<%# Bind("ChannelType") %>'
                            Width="111px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChannel_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="-1">(Please Select)</asp:ListItem>
                            <asp:ListItem>FTP</asp:ListItem>
                            <asp:ListItem>MSMQ</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <asp:MultiView ID="MultiViewSettings" runat="server">
                <asp:View ID="View1" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                IP Address:</td>
                            <td>
                                <asp:TextBox ID="TextBoxIPAddress" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxIPAddress"
                                    ErrorMessage="IP Address is required." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Port:</td>
                            <td>
                                <asp:TextBox ID="TextBoxPort" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPort" ErrorMessage="Port is required."
                                    SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator2"
                                        runat="server" ControlToValidate="TextBoxErrorCount" ErrorMessage="Port is not valid"
                                        Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td>
                                User Name:</td>
                            <td>
                                <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxUserName"
                                    ErrorMessage="User Name is required." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Password:</td>
                            <td>
                                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxPassword"
                                    ErrorMessage="Password is required." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Error Count:</td>
                            <td>
                                <asp:TextBox ID="TextBoxErrorCount" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxErrorCount"
                                    ErrorMessage="Error Count is required." SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToValidate="TextBoxErrorCount" ErrorMessage="Error count is not valid"
                                        Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                Queue Name:</td>
                            <td>
                                <asp:DropDownList ID="DropDownListQueues" runat="server" DataSourceID="ObjectDataSourceQueues"
                                    Width="252px">
                                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceQueues" runat="server"
                                    SelectMethod="GetQueues" TypeName="Discovery.Integration.IntegrationController">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView><asp:HiddenField ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender Enabled="false" ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxActive"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 120px">
                        Name:</td>
                    <td>
                        <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Connection Type:</td>
                    <td>
                        <asp:Label ID="LabelConnectionType" runat="server" Text='<%# Eval("ConnectionType") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Channel Type:</td>
                    <td>
                        <asp:Label ID="LabelChannelType" runat="server" Text='<%# Eval("ChannelType") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Active</td>
                    <td>
                        <asp:Image ID="ImageIsActive" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("Active")) %>' />
                        <asp:Image ID="ImageNotActive" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("Active")) %>' />
                </tr>
            </table>
            <asp:MultiView ID="MultiViewSettings" runat="server">
                <asp:View ID="View1" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                IP Address:</td>
                            <td>
                                <asp:Label ID="LabelIPAddress" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                Port:</td>
                            <td>
                                <asp:Label ID="LabelPort" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                User Name:</td>
                            <td>
                                <asp:Label ID="LabelUserName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                Error Count:</td>
                            <td>
                                <asp:Label ID="LabelErrorCount" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                Queue Name:</td>
                            <td>
                                <asp:Label ID="LabelQueueName" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView><br />
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="ConnectionDataSource" runat="server" DataObjectTypeName="Discovery.Integration.Connection"
        InsertMethod="SaveConnection" SelectMethod="GetConnection" TypeName="Discovery.Integration.IntegrationController"
        UpdateMethod="SaveConnection" DeleteMethod="DeleteConnection">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="connectionId" QueryStringField="Id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
