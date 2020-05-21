<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliveryNote.aspx.cs" Inherits="Printing_DeliveryNote" %>

<%@ Register Src="DeliveryNote.ascx" TagName="DeliveryNote" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-color:White;">
    <form id="form1" runat="server">
        <!-- AJAX Script Manager Start -->
        <asp:ScriptManager runat="server" ID="ajaxScriptManager" />

        <!-- Delivery note printing -->
        <uc1:DeliveryNote ID="ctrlDeliveryNote" runat="server" PrintPreview="true" />
    </form>
</body>
</html>
