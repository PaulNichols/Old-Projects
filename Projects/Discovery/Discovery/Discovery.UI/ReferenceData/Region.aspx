<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Region.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.Region" Title="Mangement Server - Region Maintenance" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Regions</div>
        <hr />
        <br /> <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="RegionFormView" runat="server" DataSourceID="RegionDataSource"
        DataKeyNames="Id"  EnableViewState="False"  >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table id="TABLE1">
                <tr>
                    <td style="width: 100px">
                        Code:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
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
            <table id="Table2" >
                <tr>
                    <td style="width: 100px">
                        Code:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
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
                        Code:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td style="width: 100px">
            <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                </tr>
            </table><asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
    </asp:FormView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="RegionDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.Region"
        DeleteMethod="DeleteRegion" InsertMethod="SaveRegion" SelectMethod="GetRegion"
        TypeName="Discovery.BusinessObjects.Controllers.RegionController" UpdateMethod="SaveRegion" >
 <DeleteParameters>
                    <asp:Parameter Name="Id" />
                </DeleteParameters>

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="regionId" QueryStringField="id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

