<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="LogEntries.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.LogEntries"
    Title="Management Server - Log Entries" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Log Viewer<br />
    </div>
    <hr />
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanelContainer">
        <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
            <span class="collapsePanelTitle">Search Criteria</span><span class="collapsePanelMinMax">
                <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
        </asp:Panel>
        <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Height="0"
            Width="100%">
            <table>
                <tr>
                    <td style="width: 60px">
                        Opco:</td>
                    <td style="width: 110px">
                        <asp:DropDownList  CssClass="EditText" ID="DropDownListOpco" runat="server" AppendDataBoundItems="true"
                            DataSourceID="ObjectDataSourceOpco" DataTextField="Description" DataValueField="Code"
                            OnDataBound="DropDownListOpco_DataBound2" Width="175px">
                            <asp:ListItem Value="">-</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceOpco" runat="server"
                            SelectMethod="GetOpCos" TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                        </asp:ObjectDataSource>
                    </td>
                    <td style="width: 60px">
                        Category:</td>
                    <td style="width: 110px">
                        <asp:DropDownList  CssClass="EditText" ID="DropDownListCategory" runat="server" AppendDataBoundItems="True"
                            DataSourceID="CategoriesObjectDataSource" DataTextField="Value" DataValueField="Key"
                            OnDataBound="DropDownListCategory_DataBound" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCategory_SelectedIndexChanged">
                             <asp:ListItem Value="">-</asp:ListItem>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="CategoriesObjectDataSource" runat="server" SelectMethod="GetCategories"
                            TypeName="Discovery.BusinessObjects.Controllers.LogController"></asp:ObjectDataSource>
                    </td>
                    <td style="width: 60px">
                        Error Type:</td>
                    <td style="width: 110px">
                        <asp:DropDownList ID="DropDownListErrorType" runat="server" AppendDataBoundItems="true"
                             DataTextField="ExceptionDescription"
                            DataValueField="ExceptionType" OnDataBound="DropDownListErrorType_DataBound"
                            Width="200px">
                            <asp:ListItem Value="">-</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date/Time From:</td>
                    <td>
                        <asp:TextBox ID="txtTimestampFrom" runat="server" CssClass="EditText" Width="156px"></asp:TextBox><asp:RegularExpressionValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTimestampFrom"
                            ErrorMessage="Please enter a valid From Date" ValidationExpression="^([0-2]\d|[3][0-1])\/([0]\d|[1][0-2])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$" >*</asp:RegularExpressionValidator></td>
                    <td>
                        To:</td>
                    <td>
                        <asp:TextBox ID="txtTimestampTo" runat="server" CssClass="EditText" Width="156px"></asp:TextBox><asp:RegularExpressionValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTimestampTo"
                            ErrorMessage="Please enter a valid To Date" ValidationExpression="^([0-2]\d|[3][0-1])\/([0]\d|[1][0-2])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$">*</asp:RegularExpressionValidator></td>
                    <td align="left" style="width: 79px">
                        Severity:</td>
                    <td align="right" style="width: 79px">
                        <asp:DropDownList  CssClass="EditText" ID="DropDownListSeverity" AppendDataBoundItems="true" runat="server"
                            DataSourceID="ObjectDataSourceSeverity" DataTextField="Value" DataValueField="Value"
                            OnDataBound="DropDownListSeverity_DataBound" Width="200px">
                            <asp:ListItem Value="">-</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourceSeverity" runat="server"
                            SelectMethod="GetSeverities" TypeName="Discovery.BusinessObjects.Controllers.LogController">
                        </asp:ObjectDataSource>
                    </td>
                    <td align="right" style="width: 79px">
                    </td>
                  
                </tr>
                <tr>
                  <td align="left">
                        Acknowledged:</td>
                    <td align="left">
                        <asp:DropDownList  CssClass="EditText"  ID="DropDownListAcknowledged" Width="75px" runat="server" OnDataBound="DropDownListAcknowledged_DataBound">
                            <asp:ListItem Value="">-</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList></td>
                    <td align="left" >
                        Contains Text:</td>
                    <td colspan="3" style="width: 100%">
                        <asp:TextBox   CssClass="EditText" Width="90%" ID="TextBoxMessageContents" runat="server"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                       
                    <td align="right" >
                        <asp:ImageButton ID="ImageButtonSearch" runat="server" OnClick="ImageButtonSearch_Click"
                            SkinID="ButtonSearch" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="398px" />
    </asp:Panel>
    <!-- AJAX Extender -->
    <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="CriteriaContent"
        ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax" Collapsed="False"
        ImageControlID="CriteriaMinMax" CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />
    <br />

            <Discovery:DiscoveryGrid DetailURL="logentry.aspx" DefaultSortExpression="" AllowPaging="true"
                AllowSorting="true" ID="GridView1" runat="server" DataSourceID="LogEntriesObjectDataSource"
                DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound" >
                <Columns>
                    <asp:BoundField DataField="TimeStamp" HeaderText="Date/Time" SortExpression="TimeStamp" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" SortExpression="CategoryName" />
                    <asp:BoundField DataField="ErrorType" HeaderText="Error Type" SortExpression="ErrorType" />
                    <asp:BoundField DataField="Severity" HeaderText="Severity" SortExpression="Severity" />
                    <asp:BoundField DataField="AcknowledgedBy" HeaderText="Acknowledged By" SortExpression="AcknowledgedBy" />
                </Columns>
            </Discovery:DiscoveryGrid>
        </ContentTemplate>
     
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="LogEntriesObjectDataSource" runat="server" EnablePaging="True"
        SelectCountMethod="NumberOfLogEntries" SelectMethod="GetLogEntries" SortParameterName="sortExpression"
        TypeName="Discovery.BusinessObjects.Controllers.LogController" OnSelecting="LogEntriesObjectDataSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="searchCriteria" Type="object" />
            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
