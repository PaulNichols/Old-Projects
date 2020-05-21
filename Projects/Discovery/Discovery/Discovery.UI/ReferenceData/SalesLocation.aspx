<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SalesLocation.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.SalesLocation" Title="Mangement Server - Sales Locations" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <div class="PageTitle">
        Sales Locations</div>
        <hr />
        <br /><asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="SalesLocationFormView" runat="server" DataSourceID="SalesLocationDataSource"
        DataKeyNames="Id,Location"  EnableViewState="False" >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="Table3">
                <tr>
                    <td style="width: 100px">
                        Location:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxLocation" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Telephone Number:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxTelephoneNumber" runat="server" Text='<%# Bind("TelephoneNumber") %>'></asp:TextBox></td>
                </tr>
                  <tr>
                   <td >
                        OpCo:</td>
                    <td >
                        <asp:DropDownList ID="DropDownListOpCo" runat="server" DataSourceID="OpCosObjectDataSource"
                            DataTextField="Description" DataValueField="Id" Width="169px" SelectedValue='<%# Bind("OpCoId") %>'>
                        </asp:DropDownList><asp:ObjectDataSource ID="OpCosObjectDataSource" runat="server"
                            SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                        
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
            <table id="Table1" >
                <tr>
                    <td>
                        Location:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxLocation" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Description:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Telephone Number:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxTelephoneNumber" runat="server" Text='<%# Bind("TelephoneNumber") %>'></asp:TextBox></td>
                </tr>
                  <tr>
                   <td >
                        OpCo:</td>
                    <td >
                        <asp:DropDownList ID="DropDownListOpCo" runat="server" DataSourceID="OpCosObjectDataSource"
                            DataTextField="Description" DataValueField="Id" Width="169px" SelectedValue='<%# Bind("OpCoId") %>'>
                        </asp:DropDownList><asp:ObjectDataSource ID="OpCosObjectDataSource" runat="server"
                            SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                <asp:Parameter DefaultValue="Description" Name="sortExpression" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                    <td style="width: 137px">
                        Location:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 137px">
                        Telephone Number:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelTelephoneNumber" runat="server" Text='<%# Bind("TelephoneNumber") %>'></asp:Label></td>
                </tr>
          <tr>
                    <td style="width: 137px">
                        OpCo:</td>
                    <td style="width: 100px">
                        <asp:Label ID="LabelOpCo" runat="server" Text='<%# Eval("OpCo.Description") %>'></asp:Label></td>
                </tr>
            </table><asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="SalesLocationDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.SalesLocation"
        DeleteMethod="DeleteLocation" InsertMethod="SaveLocation" SelectMethod="GetLocation"
        TypeName="Discovery.BusinessObjects.Controllers.SalesLocationController" UpdateMethod="SaveLocation" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="locationId" QueryStringField="Id"
                Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

