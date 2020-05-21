<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FundPrices_Upload.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<%@ Register Src="Controls/OffShorePriceEdit.ascx" TagName="OffShorePriceEdit" TagPrefix="uc2" %>

<%@ Register Src="Controls/FundPrices_Upload.ascx" TagName="FundPrices_Upload" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FundPrices_Upload ID="FundPrices_Upload1" runat="server" />
</asp:Content>

