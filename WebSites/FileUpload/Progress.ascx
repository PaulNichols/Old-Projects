<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Progress.ascx.cs"
    Inherits="Progress" %>

<script language="javascript" type="text/javascript"> 
function showDiv() 
{
    setTimeout('document.images["<%= imgProgress.ClientID %>"].src="img/progress.gif"', 1); 
    document.getElementById('myHiddenDiv').style.display =""; 
} 
</script>


        <asp:Panel ID="PanelProgress" runat="server" CssClass="ProgressPanel">
            <table width="100%" border="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/img/progress.gif" />
                    </td>
                    <td valign="middle" align="center">
                        <asp:Label ID="lblMessage" runat="server" CssClass="ProgressText"><%= Message %></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
   
