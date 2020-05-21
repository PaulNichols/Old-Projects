<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageFM.ascx.cs" Inherits="FundManager.ManageFM" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<div style="font-size: xx-small; margin: 0px; width: 100%; color: black; font-family: Verdana; text-align: justify">
    <div id="divFMGrid" runat="server" visible="true">
        <table width="100%">
            <tr></tr>
            <tr>
                <td style="height: 100%" valign="top" colspan="2">
                    <table cellspacing="1" cellpadding="1" width="100%">
                        <tr>
                            <td class="Abstracts_gvHeaderStyle" rowspan="1">
                                <asp:Label ID="lblFMMatrix" runat="server" Text="" CssClass="Abstracts_Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" valign="top">
                                <asp:GridView OnPageIndexChanging="gvFM_PageIndexChanging" OnSorting="gvFM_Sorting" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" ID="gvFM" runat="server" 
                                GridLines="Both" CssClass="AbstractsGrid" BorderColor="black" DataKeyNames="Id" OnRowDataBound="gvFM_RowDataBound" OnSelectedIndexChanged="gvFM_SelectedIndexChanged">
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
                <asp:Label ID="Label1" runat="server" CssClass="Abstracts_Label" Text="Fund Manager Name:"
                    Width="200px"></asp:Label></td>
            <td colspan="2">
                <asp:TextBox ID="txtFMName" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                    TabIndex="1" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label3" runat="server" CssClass="Abstracts_Label" Text="Profile:"
                    Width="200px"></asp:Label></td>
            <td valign="top" colspan="2">
                    <CE:Editor ID="Editor1" runat="server" Width="100%" Height="300px" EditorBodyStyle="font-family: Arial, Verdana;">
                        <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%"  />
                        </CE:Editor>            
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label2" runat="server" CssClass="Abstracts_Label" Text="Fund Manager Image:"
                    Width="200px"></asp:Label></td>
            <td colspan="2" valign="top">
                <asp:DropDownList ID="ddlFundManager" runat="server" CssClass="Abstracts_DropDownList"
                    Width="400px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label4" runat="server" CssClass="Abstracts_Label" Text="Managed Funds:"
                    Width="200px"></asp:Label></td>
            <td colspan="2" valign="top">
                <table>
                    <tr>
                        <td style="width: 45%">
                            <asp:Label ID="Label9" runat="server" CssClass="Abstracts_Label" Text="Funds Available:"
                                Width="100%"></asp:Label><br />
                            <div style="border-right: thin solid; border-top: thin solid; vertical-align: top;
                                overflow: scroll; border-left: thin solid; border-bottom: thin solid; height: 150px">
                                <asp:CheckBoxList ID="cblFundsAvailable" runat="server">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click">>></asp:LinkButton><br />
                            <br />
                            <asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"><<</asp:LinkButton>
                        </td>
                        <td style="width: 45%">
                            <asp:Label ID="Label10" runat="server" CssClass="Abstracts_Label" Text="Funds Managed:"
                                Width="100%"></asp:Label>
                            <div style="border-right: thin solid; border-top: thin solid; vertical-align: top;
                                overflow: scroll; border-left: thin solid; border-bottom: thin solid; height: 150px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="cblFundsAssigned" runat="server">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkRemove" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="lnkAdd" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="lnkSaveIssue" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkSaveIssue_Click" Text="Save" TabIndex="6"></asp:LinkButton>
                <asp:LinkButton ID="lnkCancelIssue" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkCancelIssue_Click" TabIndex="6" Text="Cancel"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteIssue" runat="server" CssClass="Abstracts_Link_Button"
                    OnClick="lnkDeleteIssue_Click" TabIndex="6" Text="Delete" Visible="False"></asp:LinkButton></td>
            <td style="width: 220px">
            </td>
        </tr>
    </table>
    </div>
</div>
