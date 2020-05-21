<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Funds_Display.ascx.cs"
    Inherits="Controls_Funds_Display" %>
<asp:BulletedList ID="CategoriesList" runat="server" CssClass="categoryList" DisplayMode="HyperLink">
</asp:BulletedList>
<asp:GridView ID="GridViewFunds" runat="server" AutoGenerateColumns="False" CssClass="FundDisplayGrid"
    OnRowCreated="GridViewFunds_RowCreated" OnRowDataBound="GridViewFunds_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="Fund">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="FundDisplayGridFundName"
                    NavigateUrl='<%# Eval("FundId", "funddetail.aspx?id={0}") %>' Text='<%# Eval("FundName") %>'></asp:HyperLink>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Bid Price">
          
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="FundDisplayGridBidPrice" Text='<%# Bind("BidPrice", "{0:N2}%") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Offer Price">
           
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="FundDisplayGridOfferPrice" Text='<%# Bind("OfferPrice", "{0:N2}%") %>'></asp:Label>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Factsheet">
            <ItemTemplate>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="S&amp;P">
            <ItemTemplate>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="OBSR">
            <ItemTemplate>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Citywire">
            <ItemTemplate>
            </ItemTemplate>
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:TemplateField>
        <asp:BoundField HeaderText="OMAM TV">
            <ControlStyle CssClass="FundDisplayGridControls" />
            <FooterStyle CssClass="FundDisplayGridFooter" />
            <HeaderStyle CssClass="FundDisplayGridHeader" />
            <ItemStyle CssClass="FundDisplayGridItems" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
