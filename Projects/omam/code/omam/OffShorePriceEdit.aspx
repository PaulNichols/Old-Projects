<%@ Page AutoEventWireup="true" CodeFile="OffShorePriceEdit.aspx.cs" Inherits="OffShorePriceEdit"
    Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" %>

<%@ Register Src="Controls/OffShorePriceEdit.ascx" TagName="OffShorePriceEdit" TagPrefix="uc1" %>
<%@ Register Src="Controls/FundPrices_View.ascx" TagName="FundPrices_View" TagPrefix="uc2" %>
<%@ Register Src="Controls/FundPrices_Upload.ascx" TagName="FundPrices_Upload" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:OffShorePriceEdit ID="OffShorePriceEdit1" runat="server" OnLoad="OffShorePriceEdit1_Load" />
</asp:Content>

