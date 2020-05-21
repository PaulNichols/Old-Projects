<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FundPrices_View.ascx.cs" Inherits="Controls_FundPrices_View" %>
<asp:Label ID="LabelAsOf" runat="server">Latest prices as of:</asp:Label><br />
<asp:Label ID="LabelAsOfDate" runat="server"></asp:Label>
<p></p>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Label1" CssClass="FundPriceView_DescriptionItem" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image1" runat="server" CssClass="FundPriceView_DescriptionHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="FundPriceView_BidLatestItem" Text='<%# Bind("BidPriceLatest") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image2" runat="server" CssClass="FundPriceView_BidLatestHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" CssClass="FundPriceView_BidPreviousItem" Text='<%# Bind("BidPricePrevious") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image3" runat="server" CssClass="FundPriceView_BidPreviousHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" CssClass="FundPriceView_OfferLatestItem" Text='<%# Bind("OfferPriceLatest") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image4" runat="server" CssClass="FundPriceView_OfferLatestHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
           <ItemTemplate>
                <asp:Label ID="Label5" runat="server" CssClass="FundPriceView_OfferPreviousItem" Text='<%# Bind("OfferPricePrevious") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image5" runat="server" CssClass="FundPriceView_OfferPreviousHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>

            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" CssClass="FundPriceView_PriceChangeItem" Text='<%# Bind("PriceChange", "{0:N2}") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image6" runat="server" CssClass="FundPriceView_PriceChangeHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="Label7" runat="server" CssClass="FundPriceView_YieldItem" Text='<%# Bind("Yield") %>'></asp:Label>
            </ItemTemplate>
            <HeaderTemplate>
                <asp:Image ID="Image7" runat="server" CssClass="FundPriceView_YieldHeader" />
            </HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="LabelEmpty" runat="server" CssClass="FundPriceView_Empty"
            Text="No Fund Prices have been uploaded"></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>
<br />
<br />
<p>
</p>
<asp:Label ID="LabelHeader" runat="server" Text="Old Mutal Dublin-based Funds"></asp:Label><br />
<br />
<asp:Label ID="LabelText" runat="server">Our Dublin-based funds are managed by Old Mutual Asset Managers (UK) Limited, a sister company of Old Mutual Fund Managers. These are the latest pulished prices for Global Dynamic Fund (updated weekly) and UK Select Smaller Companies Fund (updated daily).</asp:Label><br />
<br />
<asp:GridView ID="GridViewOffShoreFunds" runat="server" AutoGenerateColumns="False"
    CssClass="OffShoreGrid">
    <Columns>
        <asp:TemplateField HeaderText="Fund">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FundName") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("FundName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Currency">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Currency") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Price") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Price", "{0:c}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="LabelNoFunds" runat="server" Text="There are no Off-Shore Funds"></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>
&nbsp;&nbsp;
