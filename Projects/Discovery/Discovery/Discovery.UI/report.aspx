<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp;
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            ReportSourceID="CrystalReportSource1" />
    
    </div>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="D:\Management Server\Discovery\Business Objects\test.rpt">
                <DataSources>
                    <CR:DataSourceRef DataSourceID="ObjectDataSource1" TableName="Discovery_BusinessObjects_TDCShipment" />
                    <CR:DataSourceRef DataSourceID="ObjectDataSource1" ReportName="testlines.rpt" TableName="Discovery_BusinessObjects_TDCShipmentLine" />
                </DataSources>
            </Report>
        </CR:CrystalReportSource>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetShipment" TypeName="Discovery.BusinessObjects.Controllers.TDCShipmentController">
            <SelectParameters>
                <asp:Parameter DefaultValue="1024" Name="shipmentId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
