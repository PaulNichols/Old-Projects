<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFundManager.ascx.cs" Inherits="M2.Modules.FundManager.ViewFundManager" %>
<div style="font-size: xx-small; margin: 0px; width: 100%; color: black; font-family: Verdana; text-align: justify">
<table width="100%">
    <tr></tr>
    <tr>
        <td valign="top" colspan="2">
            <table cellspacing="5" cellpadding="0" width="100%">
                <tr>
                    <td align="left" class="Abstracts_gvRowStyle">
                        <asp:Label ID="lblShow" runat="server" CssClass="Abstracts_PageLabel" Text="Select Category:"></asp:Label></td>
                    <td align="left" valign="top">
                        <asp:DropDownList Width="300px" CssClass="Abstracts_DropDownList" ID="ddlCategories" runat="server" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="Abstracts_gvRowStyle" style="width: 235px">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></td>
                    <td align="left" valign="top">
                    </td>
                </tr>
            </table>
        </td>    
    </tr>
    <tr></tr>
    <tr></tr>
    <tr>
        <td style="height: 100%" valign="top" colspan="2">
            <table cellspacing="1" cellpadding="1" width="100%">
                <tr>
                    <td style="width: 100%;" valign="top">
<%--                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                              <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnSearch" />
                            </Triggers>
                            <ContentTemplate>
 --%>                               <asp:GridView OnPageIndexChanging="gvFunds_PageIndexChanging" OnSorting="gvFunds_Sorting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" ID="gvFunds" runat="server" 
                                GridLines="Both" CssClass="AbstractsGrid" BorderColor="black" DataKeyNames="Id" OnRowDataBound="gvFunds_RowDataBound" OnSelectedIndexChanged="gvFunds_SelectedIndexChanged">
                                    <Pagerstyle CssClass="Abstracts_gvPagerStyle" />
                                    <HeaderStyle CssClass="Abstracts_gvHeaderStyle" />
                                    <RowStyle CssClass="Abstracts_gvRowStyle" />
                                </asp:GridView>                            
<%--                            </ContentTemplate>
                        </asp:UpdatePanel>
--%>                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="cmdAdd" runat="server" CssClass="AbstractsLinkButton" OnClick="cmdAdd_Click" Text="Add New"></asp:LinkButton></td>
    </tr>
</table>
</div>