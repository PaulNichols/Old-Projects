<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OpCo.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.OpCo" Title="Mangement Server - OpCo Maintenance"  %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
   <div class="PageTitle">
        Operating Companies</div>
        <hr />
        <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />

    <asp:FormView ID="OpCoFormView" runat="server" DataSourceID="OpCoDataSource"
        DataKeyNames="Id,Code" OnItemDeleting="OpCoFormView_ItemDeleting" >
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
                    <td>
                        Code:</td>
                    <td style="width: 359px">
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        Description:</td>
                    <td style="width: 359px">
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
    <asp:ObjectDataSource ID="OpCoDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.OpCo"
        DeleteMethod="DeleteOpCo" InsertMethod="SaveOpCo" SelectMethod="GetOpCo"
        TypeName="Discovery.BusinessObjects.Controllers.OpcoController" UpdateMethod="SaveOpCo" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="opcoId" QueryStringField="Id" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

