<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCategories.ascx.cs" Inherits="M2.Modules.FundManager.EditCategories" %>
<div style="font-size: xx-small; margin: 0px; width: 100%; color: black; font-family: Verdana; text-align: justify">
    <div id="divCategoryList" runat="server" visible="true">
        <table width="100%">
    <tr></tr>
    <tr>
        <td style="height: 100%" valign="top" colspan="2">
            <table cellspacing="1" cellpadding="1" width="100%">
                <tr>
                    <td class="Abstracts_gvHeaderStyle" rowspan="1">
                        <asp:Label ID="lblNewsMatrix" runat="server" Text="" CssClass="Abstracts_Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" valign="top">
                        <asp:GridView OnPageIndexChanging="gvAllNews_PageIndexChanging" OnSorting="gvAllNews_Sorting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" ID="gvAllNews" runat="server" 
                        GridLines="Both" CssClass="AbstractsGrid" BorderColor="black" DataKeyNames="Id" OnRowDataBound="gvAllNews_RowDataBound" OnSelectedIndexChanged="gvAllNews_SelectedIndexChanged">
                            <Pagerstyle CssClass="Abstracts_gvPagerStyle" />
                            <HeaderStyle CssClass="Abstracts_gvHeaderStyle" />
                            <RowStyle CssClass="Abstracts_gvRowStyle" />
                        </asp:GridView>                            
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="cmdAdd" runat="server" CssClass="AbstractsLinkButton" OnClick="cmdAdd_Click" Text="Add New"></asp:LinkButton>
            <asp:LinkButton ID="cmdBack" runat="server" CssClass="AbstractsLinkButton" OnClick="cmdBack_Click" Text="Back"></asp:LinkButton>            
        </td>
    </tr>
    </table>
    </div>
    <div id="divAddActivity" runat="server" visible="false">
    <table>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="lblCategoryId" runat="server" CssClass="Abstracts_Label" Text="Id:"
                    Width="100px" Visible="False"></asp:Label>
            </td>
            <td style="width: 120px">
                <asp:TextBox ID="txtCategoryId" runat="server" CssClass="Abstracts_TextBox" MaxLength="10"
                    TabIndex="1" Width="10px" Enabled="False" Visible="False"></asp:TextBox></td>
            <td style="width: 220px" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label1" runat="server" CssClass="Abstracts_Label" Text="Category Name:"
                    Width="200px"></asp:Label></td>
            <td colspan="2">
                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                    TabIndex="1" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:Label ID="Label2" runat="server" CssClass="Abstracts_Label" Text="Availble in these sites:"
                    Width="100%"></asp:Label></td>
            <td colspan="2">
                <asp:GridView ID="gvSites" runat="server" AllowPaging="true" AllowSorting="true"
                    AutoGenerateColumns="False" BorderColor="black" CssClass="AbstractsGrid" DataKeyNames="PortalId"
                    GridLines="Both" OnPageIndexChanging="gvSites_PageIndexChanging" OnRowDataBound="gvSites_RowDataBound"
                    OnSelectedIndexChanged="gvSites_SelectedIndexChanged" OnSorting="gvSites_Sorting">
                    <RowStyle CssClass="Abstracts_gvRowStyle" />
                    <PagerStyle CssClass="Abstracts_gvPagerStyle" />
                    <HeaderStyle CssClass="Abstracts_gvHeaderStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="lnkSaveCategory" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkSaveCategory_Click" Text="Save" TabIndex="6"></asp:LinkButton>
                <asp:LinkButton ID="lnkCancelCategory" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkCancelCategory_Click" TabIndex="6" Text="Cancel"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteCategory" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkDeleteCategory_Click" TabIndex="6" Text="Delete" Visible="False"></asp:LinkButton></td>
            <td style="width: 220px">
            </td>
        </tr>
    </table>
    </div>
</div>
