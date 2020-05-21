<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFundDetail.ascx.cs" Inherits="YourCompany.Modules.FundDetail.ViewFundDetail" %>
<asp:Panel ID="pnlFundDetail" runat="server" CssClass="divFundDetail" Height="50px"
    Width="250px">
<asp:Panel ID="pnlFundSnippet" runat="server" CssClass="divFundSnippet">
    <asp:Label ID="lblFundSnippet" runat="server" Text="Label"></asp:Label></asp:Panel>
<br class="clearBoth"/>
<asp:Panel ID="pnlFundAims" runat="server" CssClass="divFundAims">
    <asp:Label ID="lblFundAims" runat="server" Text="Label"></asp:Label></asp:Panel>
<br class="clearBoth"/>
<asp:Panel ID="pnlFundManager" runat="server" CssClass="divFundManager">
    <asp:Label ID="lblFundManager" runat="server" Text="Label"></asp:Label></asp:Panel>
<br class="clearBoth"/>
<asp:Panel ID="pnlAssets" CssClass="divFundLiterature" runat="server"></asp:Panel>
<br class="clearBoth"/>
<asp:Panel ID="pnlRatings" CssClass="divFundRatings" runat="server"></asp:Panel>
<br class="clearBoth"/>
</asp:Panel>
