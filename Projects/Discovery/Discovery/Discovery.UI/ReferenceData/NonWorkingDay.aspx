<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="NonWorkingDay.aspx.cs"
    Inherits="Discovery.UI.Web.ReferenceData.NonWorkingDay" Title="Mangement Server - Non Working Days" %>

<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Non-Working Days</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />
    <asp:FormView ID="NonWorkingDayFormView" runat="server" DataSourceID="NonWorkingDayDataSource"
        DataKeyNames="Id" OnDataBound="FormView_DataBound" OnItemInserting="NonWorkingDayFormView_ItemInserting">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 169px; height: 35px;">
                        Non Working Date:</td>
                    <td style="width: 192px; height: 35px;">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("NonWorkingDate", "{0:d}") %>'></asp:Label>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("NonWorkingDate") %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 169px; height: 38px">
                        Description:</td>
                    <td style="width: 192px; height: 38px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Height="30px" Text='<%# Bind("Description") %>'
                            Width="185px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 169px; height: 30px">
                        Warehouse</td>
                    <td style="width: 192px; height: 30px">
                        <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%# Eval("WarehouseCode") %>'></asp:Label>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("WarehouseId") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Updated</td>
                    <td>
                        <asp:Label ID="LabelUpdatedDate" runat="server" Text='<%# Eval("UpdatedDate", "{0:d}") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Update By</td>
                    <td>
                        <asp:Label ID="LabelUpdatedBy" runat="server" Text='<%# Eval("UpdatedBy") %>'></asp:Label></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CommandName="Insert" SkinID="ButtonInsert"
                OnClick="InsertButton_Click" /><asp:ImageButton ID="InsertCancelButton" runat="server"
                    CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 100px">
                        Region</td>
                    <td>
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" DataTextField="Code"
                            DataValueField="Id" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="146px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <td>
                    Warehouse
                </td>
                <td>
                    <asp:DropDownList ID="ddlWarehouse" runat="server" DataTextField="Code" DataValueField="Id"
                        Width="152px">
                    </asp:DropDownList>
                </td>
                </tr>
                <td>
                    Start Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ControlToValidate="txtStartDate"
                        ErrorMessage="Please enter a Start date" Text="*" SetFocusOnError="True">*
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorStartDate" runat="server" ControlToCompare="txtEndDate"
                        ControlToValidate="txtStartDate" ErrorMessage="The Start Date must be less than or equal to the  End Date"
                        Operator="LessThanEqual" SetFocusOnError="True" Type="Date" Width="20px">*
                    </asp:CompareValidator>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Height="17px"></asp:TextBox>&nbsp;<rjs:PopCalendar
                        ID="PopCalendarStartDate" runat="server" BorderWidth="1px" Buttons="[<][m][y]  [>]"
                        Control="txtStartDate" CssClass="Classic" Culture="en-GB English (United Kingdom)"
                        Fade="0.5" Format="dd/mm/yyyy" Move="True" RequiredDate="False" Separator="/"
                        ShowMessageBox="True" ShowWeekend="True" WeekendMessage="blah" RequiredDateMessage="Please select a Start Date from the Calender" />
                </td>
                </tr>
                <td>
                    End Date
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEndDate" runat="server" ControlToValidate="txtEndDate"
                        ErrorMessage="Please enter a End date" Text="*" SetFocusOnError="True">*
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtEndDate" OnServerValidate="CustomValidator1_ServerValidate"
                        ErrorMessage="Date range must be less than one year" SetFocusOnError="False"
                        runat="server">*
                    </asp:CustomValidator></td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>&nbsp;<rjs:PopCalendar
                        ID="PopCalendarEndDate" runat="server" BorderWidth="1px" Buttons="[<][m][y]  [>]"
                        Control="txtEndDate" CssClass="Classic" Culture="en-GB English (United Kingdom)"
                        Fade="0.5" Format="dd/mm/yyyy" Move="True" RequiredDate="False" Separator="/"
                        ShowMessageBox="True" ShowWeekend="True" WeekendMessage="blah" RequiredDateMessage="Please select a End Date from the calender" />
                </td>
                </tr>
                <td>
                    Description
                </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server"
                    ControlToValidate="txtDescription" ErrorMessage="Please enter a description"
                    SetFocusOnError="True">*
                </asp:RequiredFieldValidator></td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
                <td />
                <td>
                    <asp:CheckBox ID="chkWeekEnd" runat="server" Text="Weekend ONLY?" /></td>
                </tr>
            </table>
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 126px;">
                        Non Working Date</td>
                    <td style="width: 126px;">
                        <asp:Label ID="LabelNonWorkingDate" runat="server" Text='<%# Bind("NonWorkingDate", "{0:d}") %>'
                            Width="200px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Description:</td>
                    <td>
                        <asp:Label ID="LabelDescription" runat="server" Text='<%# Eval("Description") %>'
                            Width="173px"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Warehouse</td>
                    <td>
                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%# Eval("WarehouseCode") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Last Updated</td>
                    <td>
                        <asp:Label ID="LabelUpdatedDate" runat="server" Text='<%# Eval("UpdatedDate", "{0:d}") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Update By</td>
                    <td>
                        <asp:Label ID="LabelUpdatedBy" runat="server" Text='<%# Eval("UpdatedBy") %>'></asp:Label></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please ensure the following fields entered: "
        ShowMessageBox="True" ShowSummary="False" />
    <asp:ObjectDataSource ID="NonWorkingDayDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.NonWorkingDay"
        DeleteMethod="DeleteNonWorkingDay" InsertMethod="SaveNonWorkingDays" SelectMethod="GetNonWorkingDay"
        TypeName="Discovery.BusinessObjects.Controllers.NonWorkingDayController" UpdateMethod="SaveNonWorkingDay">
        <SelectParameters>
            <asp:QueryStringParameter Name="nonWorkingDayId" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
