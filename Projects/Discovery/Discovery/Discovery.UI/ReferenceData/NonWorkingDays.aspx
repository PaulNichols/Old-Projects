<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="NonWorkingDays.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.NonWorkingDays"
    Title="Management Server - Non-Working Days" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">

function SelectAllCheckboxes(spanChk){

// Added as ASPX uses SPAN for checkbox

var oItem = spanChk.children;

var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];

xState=theBox.checked;

elm=theBox.form.elements;

for(i=0;i<elm.length;i++)

if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)

{

//elm[i].click();

if(elm[i].checked!=xState)

//elm[i].click();

elm[i].checked=xState;

}

}



    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <contenttemplate>
    <div class="PageTitle">
        Non-Working Days</div>
    <hr />
        <asp:Panel ID="Panel1" runat="server" Width="800px" CssClass="collapsePanelContainer">
    <asp:Panel ID="CriteriaHeader" runat="server" CssClass="collapsePanelHeader" Width="100%">
        <span class="collapsePanelTitle">Criteria</span><span class="collapsePanelMinMax">
            <asp:Image ID="CriteriaMinMax" ImageUrl="~/Images/Container/Min.gif" runat="server" /></span>
    </asp:Panel>
    <asp:Panel ID="CriteriaContent" runat="server" CssClass="collapsePanelContent" Height="0"
        Width="100%">
    

    <table id="tblFilter" runat="server">
        <tr>
            <td>
                Region</td>
            <td>
                Warehouse</td>
            <td>
                Start Date
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ControlToValidate="txtStartDate"
                    Text="*" ErrorMessage="Please enter a Start Date" SetFocusOnError="True" Width="20px"></asp:RequiredFieldValidator><asp:CompareValidator
                        ID="CompareValidator1" runat="server" ControlToCompare="txtEndDate" ControlToValidate="txtStartDate"
                        ErrorMessage="The Start Date must be less than or equal to the  End Date" Operator="LessThanEqual"
                        SetFocusOnError="True" Type="Date" Width="20px">*</asp:CompareValidator></td>
            <td>
                End Date<asp:RequiredFieldValidator ID="RequiredFieldValidatorEndDate" runat="server"
                    ControlToValidate="txtEndDate" Text="*" ErrorMessage="Please enter a End Date"
                    SetFocusOnError="True" Width="20px"></asp:RequiredFieldValidator></td>
            <td style="width: 50px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlRegion" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                    DataSourceID="RegionDataSource" DataTextField="Code" DataValueField="Id" Width="123px"
                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" OnDataBound="ddlRegion_DataBound">
                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                </asp:DropDownList>
    <asp:ObjectDataSource ID="RegionDataSource" runat="server" SelectMethod="GetRegions"
        TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"></asp:ObjectDataSource>
            </td>
            <td>
                <asp:DropDownList ID="ddlWarehouse" runat="server" AppendDataBoundItems="True" DataSourceID="WarehouseDataSource"
                    DataTextField="Code" DataValueField="Id" Width="119px" OnDataBound="ddlWarehouse_DataBound">
                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                </asp:DropDownList>
    <asp:ObjectDataSource ID="WarehouseDataSource" runat="server" SelectMethod="GetWarehousesByRegion"
        TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlRegion" Name="regionId" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" width="80px"></asp:TextBox>
                <rjs:PopCalendar RequiredDate="True" 
                     ShowWeekend="True" 
                    Control="txtStartDate" 
                    ID="PopCalendarStart" runat="server" 
                    ShowMessageBox="True" WeekendMessage="blah"></rjs:PopCalendar>
            </td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" width="80px"></asp:TextBox>
                <rjs:PopCalendar RequiredDate="True" 
                     ShowWeekend="True" 
                    Control="txtEndDate" 
                    ID="PopCalendar1" runat="server" 
                    ShowMessageBox="True" WeekendMessage="blah"></rjs:PopCalendar>
            </td>
            <td align="right">
                <asp:ImageButton SkinID="ButtonSearch" ID="btnSearch" runat="server" OnClick="btnSearch_Click" /></td>
        </tr>
    </table>
                   </asp:Panel>
</asp:Panel>
<!-- AJAX Extender -->
<ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="CriteriaContent" ExpandControlID="CriteriaMinMax"
        CollapseControlID="CriteriaMinMax" Collapsed="False" ImageControlID="CriteriaMinMax"
        CollapsedSize="0" SuppressPostBack="true" ExpandedImage="~/Images/Container/Min.gif"
        CollapsedImage="~/Images/Container/Max.gif" />

  </contenttemplate>
    </asp:UpdatePanel>
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton"></asp:ImageButton>
    <asp:ImageButton SkinID="ButtonDeleteMultiple" ID="btnDelete" runat="server" OnClick="btnDelete_Click"
        OnClientClick="return confirm('Are you sure you want to delete?');" />
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <contenttemplate>
    <Discovery:DiscoveryGrid  DetailURL="NonWorkingDay.aspx" DefaultSortExpression="NonWorkingDate"
        DataSourceID="ObjectDataSourceNonWorkingDays" ID="grdNonWorkingDays" runat="server"
        DataKeyNames="Id" AutoGenerateColumns="False" DoHiLiteRow="True" Width="295px" AllowPaging="True">
        <Columns>
         <asp:TemplateField  HeaderText="Delete All" FooterText="Delete All">
         
                <headertemplate>

<asp:CheckBox id="chkDeleteAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"  ></asp:CheckBox> 
</headertemplate>
             <headerstyle horizontalalign="Left" />
                <itemtemplate>
<!-- Stop cascading the click event to the row, use a span --><SPAN onclick="JavaScript:return event.cancelBubble=true;"><asp:CheckBox id="chkDelete" runat="server"></asp:CheckBox> </SPAN>
</itemtemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NonWorkingDate" HtmlEncode="False" DataFormatString="{0:d}"
                HeaderText="Date" SortExpression="NonWorkingDate" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="WarehouseCode" HeaderText="Warehouse" SortExpression="WarehouseCode" />
           
        </Columns>
    </Discovery:DiscoveryGrid>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
     
    <asp:ObjectDataSource ID="ObjectDataSourceNonWorkingDays" runat="server"
     EnablePaging="True" SelectMethod="GetNonWorkingDays" SelectCountMethod="NumberOfNonWorkingDays"
        SortParameterName="sortExpression" TypeName="Discovery.BusinessObjects.Controllers.NonWorkingDayController"
        >
        <SelectParameters>
            <asp:ControlParameter ControlID="txtStartDate" Name="dateFrom"
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="txtEndDate" DefaultValue="" Name="dateTo"
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="ddlWarehouse" DefaultValue="-1" Name="warehouseId"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlRegion" DefaultValue="-1" Name="regionId" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
                   <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
         </contenttemplate>
        <triggers>
              
                    <asp:AsyncPostbackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostbackTrigger ControlID="btnDelete" EventName="Click" />
                </triggers>
    </asp:UpdatePanel>
</asp:Content>
