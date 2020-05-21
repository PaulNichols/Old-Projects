<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="OpCoDivision.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.OpCoDivision" Title="Operating Companies Division"  %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>
<%@ Import namespace="Discovery.UI.Web.ReferenceData"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
   <div class="PageTitle">
        Operating Companies Division</div>
        <hr />
        <br />
    <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView EnableViewState="False" ID="OpCoFormView" runat="server" DataSourceID="OpCoDataSource"
         OnItemInserting="MappingFormView_ItemInserting" DataKeyNames="Id,Code" OnItemUpdating="MappingFormView_ItemUpdating" >
       
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="False" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table id="Table2" >
              <tr>
                   <td style="width: 100px">
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
                <tr>
                    <td>
                        Division Code:</td>
                    <td >
                        <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                </tr>
              
                <tr>
                    <td>
                        Logo:</td>
                    <td >
                        <asp:FileUpload ID="FileUploadLogo" runat="server" /></td>
                </tr>
            </table>
            <asp:HiddenField
                ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
              <tr>
                    <td>
                        OpCo:</td>
                    <td >
            <asp:Label ID="LabelDescription" runat="server" Text='<%# Eval("OpCo.Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Code:</td>
                    <td >
            <asp:Label ID="LabelCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label></td>
                </tr>
              
                <tr>
                    <td valign="top">
                        Logo:</td>
                   <td>
                       <asp:Label ID="LabelLogoURI" runat="server" Text='<%# Eval("LogoURI") %>'></asp:Label><br />
                       <img  alt="Branch Logo" src='<%# GetLogoViewerUrl( (int)Eval("Id") ) %>' /></td>
                </tr>
               
            </table>
           
            <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
       
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="OpCoDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.OpCoDivision"
        DeleteMethod="DeleteOpCoDivision" InsertMethod="SaveOpCoDivision" SelectMethod="GetOpCoDivision"
        TypeName="Discovery.BusinessObjects.Controllers.OpcoDivisionController" UpdateMethod="SaveOpCoDivision" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="opcoDivisionId" QueryStringField="Id" Type="Int32" />
            <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

