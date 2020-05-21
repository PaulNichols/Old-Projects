<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" DataSourceID="PrinterObjectDataSource"
            Width="251px" AutoPostBack="True">
            <asp:ListItem Selected="True" Text="All" Value=""/>
        </asp:DropDownList><asp:ObjectDataSource ID="PrinterObjectDataSource" runat="server"
            SelectMethod="GetPrinters" TypeName="PrinterController"></asp:ObjectDataSource>
        <br />
        &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="PrinterJobsObjectDataSource" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Owner" HeaderText="Owner" SortExpression="Owner" />
                <asp:BoundField DataField="PrinterName" HeaderText="PrinterName" SortExpression="PrinterName" />
                <asp:BoundField DataField="Document" HeaderText="Document" SortExpression="Document" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Driver" HeaderText="Driver" SortExpression="Driver" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:ObjectDataSource ID="PrinterJobsObjectDataSource" runat="server" SelectMethod="GetPrintJobs"
            TypeName="PrinterController">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="printerToFind" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
        &nbsp;
    </form>
</body>
</html>
