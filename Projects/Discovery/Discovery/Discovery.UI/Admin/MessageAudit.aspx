<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="MessageAudit.aspx.cs" Inherits="Discovery.UI.Web.Admin.MessageAudit" Title="Mangement Server - Message Audit"%>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
      <div class="PageTitle">
          Message Audit</div>
        <hr />
        <br /><asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="AuditEntryFormView" runat="server" DataSourceID="AuditEntryDataSource"
        DataKeyNames="Id" >
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" Visible="False" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" Visible="False" /><br />
            <table>
                <tr>
                    <td valign="top">
                        Source System:</td>
                    <td style="width: 600px">
                        <asp:Label ID="lblSourceSystem" runat="server" Text='<%# Eval("SourceSystem") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        Destination System:</td>
                    <td style="width: 600px">
                        <asp:Label ID="lblDestinationSystem" runat="server" Text='<%# Eval("DestinationSystem") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        Type:</td>
                    <td style="width: 600px">
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        File Sequence:</td>
                    <td style="width: 600px">
                        <asp:Label ID="lblSequence" runat="server" Text='<%# Eval("Sequence") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        Message:</td>
                    <td style="width: 600px" >
                        <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td >
                        Recieved Date:</td>
                    <td>
                        <asp:Label ID="LabelUpdatedDate" runat="server" Text='<%# Eval("ReceivedDate", "{0:G}") %>'></asp:Label></td>
                </tr>
            </table><asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="AuditEntryDataSource" runat="server" SelectMethod="GetAuditEntry"
        TypeName="Discovery.BusinessObjects.Controllers.AuditEntryController" OldValuesParameterFormatString="original_{0}" >

        <SelectParameters>
            <asp:QueryStringParameter Name="auditEntryId" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

