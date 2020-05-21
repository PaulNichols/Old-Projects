<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditFundManager.ascx.cs" Inherits="M2.Modules.FundManager.EditFundManager" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table width="100%">
    <tr>
        <td style="height: 100%" valign="top" colspan="2">
            <table cellspacing="1" cellpadding="2" width="100%">
                <tr>
                    <td align="left" colspan="1" rowspan="1" style="height: 22px" valign="top">
                        <asp:Label ID="Label7" runat="server" CssClass="Abstracts_Label" Text="Fund Code:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" style="height: 22px" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" style="height: 22px" valign="top">
                        <asp:TextBox ID="txtFundCode" runat="server" CssClass="Abstracts_Text" MaxLength="10"
                            Width="10%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="lblName" runat="server" CssClass="Abstracts_Label" Text="Fund Name:" Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:TextBox ID="txtFundName" runat="server" CssClass="Abstracts_Text" MaxLength="75" Width="60%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="lblDescription" runat="server" CssClass="Abstracts_Label" Text="Short Description:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top"><CE:Editor ID="edtShortDesc" runat="server" Width="100%" Height="400px" RenderRichDropDown="true" UseSimpleAmpersand="false" CustomCulture="" BreakElement="p" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple" >
                        <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                    </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label8" runat="server" CssClass="Abstracts_Label" Text="Fund Aims:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <CE:Editor ID="edtFundAims" runat="server" Width="100%" Height="400px" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple">
                            <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                        </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label1" runat="server" CssClass="Abstracts_Label" Text="Risk Warning:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <CE:Editor ID="edtRiskWarning" runat="server" Width="100%" Height="400px" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple">
                            <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                        </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label2" runat="server" CssClass="Abstracts_Label" Text="Categories:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <table>
                            <tr>
                                <td style="width: 45%">
                                    <asp:Label ID="Label9" runat="server" CssClass="Abstracts_Label" Text="Categories Available:"
                                        Width="100%"></asp:Label><br />
                                        <div style="border-right: thin solid; border-top: thin solid; vertical-align: top;
                                            overflow: scroll; border-left: thin solid; border-bottom: thin solid; height: 150px">
                                            <asp:CheckBoxList ID="cblCategoriesAvailable" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click">>></asp:LinkButton><br />
                                    <br />
                                    <asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"><<</asp:LinkButton>
                                </td>
                                <td style="width: 45%">
                                    <asp:Label ID="Label10" runat="server" CssClass="Abstracts_Label" Text="Categories Assigned:"
                                        Width="100%"></asp:Label>
                                    <div style="border-right: thin solid; border-top: thin solid; vertical-align: top;
                                        overflow: scroll; border-left: thin solid; border-bottom: thin solid; height: 150px">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBoxList ID="cblCategoriesAssigned" runat="server">
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
                    <td align="left" colspan="1" rowspan="1" valign="top" style="height: 26px">
                        <asp:Label ID="Label3" runat="server" CssClass="Abstracts_Label" Text="S&P Rating"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top" style="height: 26px">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top" style="height: 26px">
                        <asp:DropDownList ID="ddlSPRating" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label4" runat="server" CssClass="Abstracts_Label" Text="S&P Rating Details"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <CE:Editor ID="edtSPRating" runat="server" Width="100%" Height="400px" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple">
                            <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                        </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label5" runat="server" CssClass="Abstracts_Label" Text="OBSR Rating"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlOBSRRating" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label6" runat="server" CssClass="Abstracts_Label" Text="OBSR Rating Details"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <CE:Editor ID="edtOBSRRating" runat="server" Width="100%" Height="400px" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple">
                            <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                        </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label11" runat="server" CssClass="Abstracts_Label" Text="Citywire Rating"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlCitywireRating" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label12" runat="server" CssClass="Abstracts_Label" Text="Citywire Details"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <CE:Editor ID="edtCityWireRating" runat="server" Width="100%" Height="400px" EditorBodyStyle="font-family: Arial, Verdana;" AutoConfigure="Simple">
                            <FrameStyle BackColor="White" BorderColor="#DDDDDD" BorderStyle="Solid" BorderWidth="1px"
                            CssClass="CuteEditorFrame" Height="100%" Width="100%" />
                        </CE:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label13" runat="server" CssClass="Abstracts_Label" Text="Factsheet:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlFactsheet" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label14" runat="server" CssClass="Abstracts_Label" Text="Reasons Why:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlReasons" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label15" runat="server" CssClass="Abstracts_Label" Text="Annual Report:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlAnnual" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label16" runat="server" CssClass="Abstracts_Label" Text="Interim Report:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlInterim" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label17" runat="server" CssClass="Abstracts_Label" Text="S&P Report:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlSPReport" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label18" runat="server" CssClass="Abstracts_Label" Text="OBSR Report:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlOBSRReport" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label19" runat="server" CssClass="Abstracts_Label" Text="Sales Aid:"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlSalesAid" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:Label ID="Label20" runat="server" CssClass="Abstracts_Label" Text="Termsheet"
                            Width="100%"></asp:Label></td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                    </td>
                    <td align="left" colspan="1" rowspan="1" valign="top">
                        <asp:DropDownList ID="ddlTermsheet" runat="server" CssClass="Abstracts_DropDownList"
                            Width="400px">
                        </asp:DropDownList></td>
                </tr>
            </table>
            <asp:LinkButton ID="cmdBack" runat="server" CssClass="AbstractsLinkButton" OnClick="cmdBack_Click"
                Text="Back"></asp:LinkButton>
            <asp:LinkButton ID="lnkSave" runat="server" CssClass="AbstractsLinkButton" OnClick="cmdSave_Click"
                Text="Save"></asp:LinkButton></td>
    </tr>
</table>
