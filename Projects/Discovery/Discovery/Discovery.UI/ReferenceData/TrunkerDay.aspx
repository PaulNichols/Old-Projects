<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="TrunkerDay.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.TrunkerDay" Title="Mangement Server - Trunker Day Maintenance" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
      <div class="PageTitle">
          Trunker Days</div>
        <hr />
        <br /><asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="TrunkerDayFormView" runat="server" DataSourceID="TrunkerDayDataSource"
        DataKeyNames="Id"  EnableViewState="False"  >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="Table2">
                <tr>
                    <td style="width: 100px">
                        Days:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxDays" runat="server" Text='<%# Bind("Days") %>' Width="88px"></cc1:DiscoveryNumericText></td>
                </tr>
               
                <tr>
                    <td style="width: 150px">
                        Source Warehouse:</td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListSourceWarehouse" runat="server" SelectedValue='<%# Bind("SourceWarehouseId") %>'
                            Width="315px" DataSourceID="WarehouseObjectDataSource" DataTextField="Description" DataValueField="Id">
                        </asp:DropDownList></td>
                </tr>
                 <tr>
                    <td style="width: 150px">
                        Destination Warehouse:</td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListDestinationWarehouse" runat="server" SelectedValue='<%# Bind("DestinationWarehouseId") %>'
                            Width="315px" DataSourceID="WarehouseObjectDataSource" DataTextField="Description" DataValueField="Id">
                        </asp:DropDownList></td>
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
                    SkinID="ButtonCancel" /><br /><table id="Table3">
                <tr>
                    <td style="width: 100px">
                        Days:</td>
                    <td style="width: 100px">
                        <cc1:DiscoveryNumericText ID="TextBoxDays" runat="server" Text='<%# Bind("Days") %>' Width="88px"></cc1:DiscoveryNumericText></td>
                </tr>
               
                <tr>
                    <td style="width: 150px">
                        Source Warehouse:</td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListSourceWarehouse" runat="server" AppendDataBoundItems="True"
                            SelectedValue='<%# Bind("SourceWarehouseId") %>' Width="315px" DataSourceID="WarehouseObjectDataSource" DataTextField="Description" DataValueField="Id">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                 <tr>
                    <td style="width: 150px">
                        Destination Warehouse:</td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListDestinationWarehouse" runat="server" AppendDataBoundItems="True"
                            SelectedValue='<%# Bind("DestinationWarehouseId") %>' Width="315px" DataSourceID="WarehouseObjectDataSource" DataTextField="Description" DataValueField="Id">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList></td>
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
                    <td style="width: 100px">
                        Days:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelDays" runat="server" Text='<%# Bind("Days") %>'></asp:Label></td>
                </tr>
               
                <tr>
                    <td style="width: 150px">
                        Source Warhouse:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelSourceWarehouse" runat="server" Text='<%# Eval("SourceWarehouse.Description") %>'></asp:Label></td>
                </tr>
                 <tr>
                    <td style="width: 150px">
                        Destination Warehouse:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelDestWarehouse" runat="server" Text='<%# Eval("DestinationWarehouse.Description") %>'></asp:Label></td>
                </tr>
            </table><asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="TrunkerDayDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.TrunkerDay"
        DeleteMethod="DeleteTrunkerDay" InsertMethod="SaveTrunkerDay" SelectMethod="GetTrunkerDay"
        TypeName="Discovery.BusinessObjects.Controllers.TrunkerDaysController" UpdateMethod="SaveTrunkerDay" >

        <SelectParameters>
            <asp:QueryStringParameter Name="trunkerId" QueryStringField="Id" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server" SelectMethod="GetWarehouses"
        TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

