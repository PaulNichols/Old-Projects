<%@ Control Language="C#" AutoEventWireup="true" CodeFile="upload.ascx.cs" Inherits="upload" %>
<%@ Register Src="Progress.ascx" TagName="Progress" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="3 sec process" />
    </ContentTemplate>
</asp:UpdatePanel>
<!-- AJAX Update Panel Start -->
<asp:UpdateProgress ID="updateProgressMessage" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
    DisplayAfter="100">
    <ProgressTemplate>
        <uc1:Progress ID="Progress1" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<!-- AJAX Update Panel End -->
<hr />
<div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <strong>Select File For Upload: </strong><asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button
                ID="Button1" runat="server" OnClick="UploadFile" OnClientClick="showDiv();" Text="Upload" />
            <br />
            <asp:Label ID="LabelError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>
    <span id='myHiddenDiv' style='display: none'>
        <uc1:Progress ID="Progress2" ShowImage="false" runat="server" />
    </span>
</div>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="FileId" Target="_blank" DataNavigateUrlFormatString="DisplayFile.aspx?FileId={0}"
            DataTextField="FileName" />
        <asp:BoundField DataField="FileSize" HeaderText="FileSize" ReadOnly="True" SortExpression="FileSize" />
        <asp:BoundField DataField="UploadedBy" HeaderText="UploadedBy" ReadOnly="True" SortExpression="UploadedBy" />
        <asp:BoundField DataField="UploadedDate" HeaderText="UploadedDate" ReadOnly="True"
            SortExpression="UploadedDate" />
    </Columns>
</asp:GridView>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetUploadedFiles"
    TypeName="FileController">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="1" Name="projectId" QueryStringField="Project"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
