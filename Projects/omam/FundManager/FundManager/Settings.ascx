<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="M2.Modules.FundManager.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="S&P Image Folder:"></asp:Label></td>
        <td style="width: 50%">                                    
            <asp:TextBox ID="txtSPImageFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Citywire Image Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtCWImageFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="OBSR Image Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtOBSRImageFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 50%">
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="FactSheet Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtFactsheetFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Reasons Why Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtReasonFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Annual Report Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtAnnualReportFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Interim Report Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtInterimFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="S&P Report Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtSPReportFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="OBSR Report Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtOBSRReportFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label10" runat="server" Text="Sales Aid Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtSalesAidFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Termsheet Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtTermsheetFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 50%">
        </td>
    </tr>
</table>
