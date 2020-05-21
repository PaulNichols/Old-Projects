<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="YourCompany.Modules.FundDetail.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="S&P Image Display Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtSPImageFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Citywire Image Display Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtCWImageFolder" runat="server" CssClass="Abstracts_TextBox" MaxLength="100"
                TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="OBSR Image Display Folder:"></asp:Label></td>
        <td style="width: 50%">
            <asp:TextBox ID="txtOBSRImageFolder" runat="server" CssClass="Abstracts_TextBox"
                MaxLength="100" TabIndex="1" Width="200px"></asp:TextBox></td>
    </tr>
</table>
