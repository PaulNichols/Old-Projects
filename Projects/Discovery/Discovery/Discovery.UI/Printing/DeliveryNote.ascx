<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeliveryNote.ascx.cs"
    Inherits="Discovery.UI.Web.UserControls.Printing_DeliveryNote" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!-- Button To Display Or Hide Popup -->
<asp:ImageButton ID="btnPrintNote" runat="server" ImageUrl="~/Images/1x1.gif" Visible="true"
    ToolTip="Adds a line to this shipment" />
<!-- Add Popup  Start -->
<asp:Panel ID="panelPrintNote" runat="server" CssClass="DeliveryNotePopup">
    <CR:CrystalReportViewer ID="DeliveryNoteViewer" runat="server" AutoDataBind="true"
        BorderStyle="None" DisplayGroupTree="False" EnableDrillDown="False" EnableParameterPrompt="False"
        HasCrystalLogo="False" HasDrillUpButton="False" HasExportButton="False" HasSearchButton="False"
        HasToggleGroupTreeButton="False" HasViewList="False" PrintMode="ActiveX" ToolbarStyle-BorderStyle="None"
        EnableDatabaseLogonPrompt="False" PageZoomFactor="75" ToolbarStyle-BackColor="#E0E0E0"
        Width="830px" BestFitPage="False" BorderWidth="1px" HasZoomFactorList="False"
        Height="650px" ToolbarStyle-BorderColor="Black" ToolbarStyle-BorderWidth="1px"
        EnableTheming="False" ToolbarStyle-CssClass="DiscoveryToolBar" OnNavigate="DeliveryNoteViewer_Navigate" />
    <!-- Close Window -->
    <asp:ImageButton ID="btnCancelPrintNote" SkinID="ButtonCancel" runat="server" CausesValidation="false" /></asp:Panel>
<!-- Add Popup  End -->
<!-- AJAX EXTENDERS -->
<ajaxToolkit:ModalPopupExtender runat="server" ID="popupPrintNote" TargetControlID="btnPrintNote"
    PopupControlID="panelPrintNote" BackgroundCssClass="ModalPopup" CancelControlID="btnCancelPrintNote" />
