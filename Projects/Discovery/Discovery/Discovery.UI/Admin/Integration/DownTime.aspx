<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="DownTime.aspx.cs" Inherits="Discovery.UI.Web.Admin.DownTime" Title="Connection Down Time Details"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
   <div class="PageTitle">
       Down Time Details</div>
        <hr />
        <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView EnableViewState="False" ID="DownTimeFormView" runat="server" DataSourceID="DownTimeDataSource"
        DataKeyNames="Id"  >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 120px">
                        Connection Name:</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ConnectionsObjectDataSource"
                            DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ConnectionId") %>'
                            Width="214px">
                        </asp:DropDownList><asp:ObjectDataSource ID="ConnectionsObjectDataSource" runat="server"
                            SelectMethod="GetConnections" TypeName="Discovery.Integration.IntegrationController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Name" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Day Of Week:</td>
                    <td >
                        <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("DayOfWeek") %>'
                            Width="125px">
                            <asp:ListItem >Sunday</asp:ListItem>
                            <asp:ListItem >Monday</asp:ListItem>
                            <asp:ListItem>Tuesday</asp:ListItem>
                            <asp:ListItem >Wednesday</asp:ListItem>
                            <asp:ListItem >Thursday</asp:ListItem>
                            <asp:ListItem >Friday</asp:ListItem>
                            <asp:ListItem >Saturday</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Start Time:</td>
                    <td>
                        <asp:TextBox ID="TextBoxStartTime" Width="120px" Text='<%# Bind("StartTime","{0:t}") %>' runat="server"></asp:TextBox>
                       </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        End Time:</td>
                    <td>
                        <asp:TextBox ID="TextBoxEndTime" Width="120px" Text='<%# Bind("EndTime","{0:t}") %>' runat="server"></asp:TextBox>
                     
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="TextBoxStartTime"
                            ControlToValidate="TextBoxEndTime" Display="Dynamic" ErrorMessage="The End Time must be greater than the Start Time"
                            Text="*" Operator="GreaterThanEqual"></asp:CompareValidator></td>
                </tr>
            </table>
            <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 120px">
                        Connection Name:</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ConnectionsObjectDataSource"
                            DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("ConnectionId") %>'
                            Width="214px">
                        </asp:DropDownList><asp:ObjectDataSource ID="ConnectionsObjectDataSource" runat="server"
                            SelectMethod="GetConnections" TypeName="Discovery.Integration.IntegrationController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Name" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Day Of Week:</td>
                    <td >
                        <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("DayOfWeek") %>'
                            Width="150px">
                            <asp:ListItem >Sunday</asp:ListItem>
                            <asp:ListItem >Monday</asp:ListItem>
                            <asp:ListItem>Tuesday</asp:ListItem>
                            <asp:ListItem >Wednesday</asp:ListItem>
                            <asp:ListItem >Thursday</asp:ListItem>
                            <asp:ListItem >Friday</asp:ListItem>
                            <asp:ListItem >Saturday</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Start Time:</td>
                    <td>
                        <asp:TextBox ID="TextBoxStartTime" Text='<%# Bind("StartTime","{0:t}") %>' runat="server"></asp:TextBox></td>
                        </tr>
                <tr>
                    <td style="width: 120px">
                        End Time:</td>
                    <td>
                        <asp:TextBox ID="TextBoxEndTime" Text='<%# Bind("EndTime","{0:t}") %>' runat="server"></asp:TextBox>
                          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxStartTime"
                            ControlToValidate="TextBoxEndTime" Display="Dynamic" ErrorMessage="The End Time must be greater than the Start Time"
                            Text="*" Operator="GreaterThanEqual"></asp:CompareValidator>
                        </td>
                </tr>
            </table>
            <asp:HiddenField
                ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 120px">
                        Connection Name:</td>
                    <td>
                        <asp:Label ID="LabelConnectionName" runat="server" Text='<%# Eval("Connection.Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Day Of Week:</td>
                    <td >
                        <asp:Label ID="LabelDayOfWeek" runat="server" Text='<%# Eval("DayOfWeek") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Start Time:</td>
                    <td>
                        <asp:Label ID="LabelStartTime" runat="server" Text='<%# Eval("StartTime", "{0:t}") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        End Time:</td>
                    <td>
                        <asp:Label ID="LabelEndTime" runat="server" Text='<%# Eval("EndTime", "{0:t}") %>'></asp:Label></td>
                </tr>
            </table>
            <br />
            <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
       
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="DownTimeDataSource" runat="server" DataObjectTypeName="Discovery.Integration.ConnectionDownTime" InsertMethod="SaveDownTime" SelectMethod="GetDownTime"
        TypeName="Discovery.Integration.IntegrationController" UpdateMethod="SaveDownTime" DeleteMethod="DeleteDownTime" OldValuesParameterFormatString="original_{0}"  >

        <SelectParameters>
            <asp:QueryStringParameter Name="downTimeId" QueryStringField="Id" Type="Int32" />
            
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

