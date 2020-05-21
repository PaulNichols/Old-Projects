<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DiscoveryPager.ascx.cs" Inherits="Discovery.UI.Web.UserControls.DiscoveryPager" %>
<asp:Panel ID="panelPager" runat="server" CssClass="pagerFooter">
    Page Size: <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" CssClass="pagerPageSize">
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
        <asp:ListItem>40</asp:ListItem>
        <asp:ListItem>80</asp:ListItem>
    </asp:DropDownList>
    <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">...</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage1" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">1</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage2" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">2</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage3" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">3</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage4" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">4</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage5" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">5</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage6" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">6</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage7" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">7</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage8" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">8</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage9" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">9</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage10" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">10</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage11" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">11</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage12" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">12</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage13" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">13</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage14" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">14</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage15" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">15</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage16" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">16</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage17" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">17</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage18" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">18</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage19" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">19</asp:LinkButton> 
    <asp:LinkButton ID="lnkPage20" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">20</asp:LinkButton> 
    <asp:LinkButton ID="lnkNext" runat="server" OnClick="linkPage_Click" CssClass="pagerTitle">...</asp:LinkButton>
    &nbsp;<asp:Label ID="lblTotalPages" runat="server" CssClass="pagerPageTotal" Text="(???)"></asp:Label>&nbsp;&nbsp;</asp:Panel>
