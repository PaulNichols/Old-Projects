<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Discovery.UI.Web.Scheduling.Schedule" Title="Mangement Server - Schedule Maintenance" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="cc1" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Schedule</div>
        <hr />
        <br /> <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />


    <asp:FormView ID="ScheduleFormView" runat="server" DataSourceID="ScheduleDataSource"
            DataKeyNames="Id"  EnableViewState="False" >

        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate"  runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" />
            <br />

            <table id="table1">
            
                <tr valign="top">
                    <td class="Normal">
                        Available Tasks:
                    </td>
                    <td style="width: 399px">
                        <asp:DropDownList CssClass="NormalTextBox" ID="ddlTypes" Runat="server" DataSourceID="TypesObjectDataSource" SelectedValue='<%# Bind("TypeFullName") %>' DataTextField="Value" DataValueField="Key" Width="169px" />
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Schedule Enabled:
                    </td>
                    <td style="width: 399px">
                        <asp:checkbox CssClass="Normal" ID="chkEnabled" Runat="server" Text="Yes" Checked='<%# Bind("Enabled") %>' />
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Time Lapse:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:textbox id="txtTimeLapse" cssclass="NormalTextBox" Width="50px" maxlength="10" runat="server" Text='<%# Bind("TimeLapse") %>' />
                        <asp:dropdownlist ID="ddlTimeLapseMeasurement" Cssclass="NormalTextBox" Runat="server" SelectedValue='<%# Bind("TimeLapseMeasurement") %>'>
                            <asp:listitem Value="m">Minutes</asp:listitem>
                            <asp:listitem Value="h">Hours</asp:listitem>
                            <asp:listitem Value="d">Days</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: "5" and select "Minutes" to run task every 5 minutes.  Leave blank to disable timer for this task.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Retry Frequency:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:textbox id="txtRetryTimeLapse" cssclass="NormalTextBox" Width="50px" maxlength="10" runat="server" Text='<%# Bind("RetryTimeLapse") %>' />
                        <asp:dropdownlist ID="ddlRetryTimeLapseMeasurement" CssClass="NormalTextBox" Runat="server" SelectedValue='<%# Bind("RetryTimeLapseMeasurement") %>'>
                            <asp:listitem Value="m">Minutes</asp:listitem>
                            <asp:listitem Value="h">Hours</asp:listitem>
                            <asp:listitem Value="d">Days</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: "5" and select "Minutes" to retry the task every 5 minutes after a failure.    Leave blank to disable retry-timer for this task.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Retain Schedule History:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:dropdownlist CssClass="NormalTextBox" ID="ddlRetainHistoryNum" Runat="server" SelectedValue='<%# Bind("RetainHistoryNum") %>'>
                            <asp:listitem Value="1">1</asp:listitem>
                            <asp:listitem Value="5">5</asp:listitem>
                            <asp:listitem Value="10">10</asp:listitem>
                            <asp:listitem Value="25">25</asp:listitem>
                            <asp:listitem Value="50">50</asp:listitem>
                            <asp:listitem Value="100">100</asp:listitem>
                            <asp:listitem Value="250">250</asp:listitem>
                            <asp:listitem Value="500">500</asp:listitem>
                            <asp:listitem Value="-1">All</asp:listitem>
                        </asp:dropdownlist><br>
                            Example: Select "10" to keep the ten most recent schedule history rows. 
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Run on Event:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:dropdownlist id="ddlAttachToEvent" cssclass="NormalTextBox" runat="server" SelectedValue='<%# Bind("AttachToEvent") %>'>
                            <asp:listitem>None</asp:listitem>
                            <asp:listitem Value="APPLICATION_START">Application Start</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: Select "Application Start" to run this event when the web app starts.  Please note, 
                            events run on APPLICATION_END may not run reliably on some hosts.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Catch Up Enabled:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:checkbox CssClass="Normal" ID="chkCatchUpEnabled" Runat="server" Text="Yes" Checked='<%# Bind("CatchUpEnabled") %>' />
                        <br>
                            If checked, if the webserver is ever out of service, when the webserver is back 
                            in service this event will run once for each frequency that was missed during 
                            the downtime.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Object Dependencies:
                    </td>
                    <td class="Normal" style="width: 399px">
                        <asp:textbox id="txtObjectDependencies" cssclass="NormalTextBox" Width="390px" maxlength="150"
                            runat="server" Text='<%# Bind("ObjectDependencies") %>' />
                        <br>
                            Enter the tables or other objects that this event is dependent on. Example: 
                            "SiteLog,Users,UsersOnline"
                    </td>
                </tr>

            </table>

           <asp:HiddenField
                ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        
        <InsertItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="ImageButton2"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" />
            <br />
            <table id="Table2" >
                <tr valign="top">
                    <td class="Normal">
                        Available Tasks:
                    </td>
                    <td>
                        <asp:dropdownlist CssClass="NormalTextBox" ID="ddlTypes" Runat="server" DataSourceID="TypesObjectDataSource" SelectedValue='<%# Bind("TypeFullName") %>' DataTextField="Value" DataValueField="Key" Width="185px"/>
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Schedule Enabled:
                    </td>
                    <td>
                        <asp:checkbox CssClass="Normal" ID="chkEnabled" Runat="server" Text="Yes" Checked='<%# Bind("Enabled") %>' />
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Time Lapse:
                    </td>
                    <td class="Normal">
                        <asp:textbox id="txtTimeLapse" cssclass="NormalTextBox" Width="50px" maxlength="10" runat="server" Text='<%# Bind("TimeLapse") %>' />
                        <asp:dropdownlist ID="ddlTimeLapseMeasurement" CssClass="NormalTextBox" Runat="server" SelectedValue='<%# Bind("TimeLapseMeasurement") %>'>
                            <asp:listitem Value="m">Minutes</asp:listitem>
                            <asp:listitem Value="h">Hours</asp:listitem>
                            <asp:listitem Value="d">Days</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: "5" and select "Minutes" to run task every 5 minutes.  Leave blank to disable timer for this task.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Retry Frequency:
                    </td>
                    <td class="Normal">
                        <asp:textbox id="txtRetryTimeLapse" cssclass="NormalTextBox" Width="50px" maxlength="10" runat="server" Text='<%# Bind("RetryTimeLapse") %>' />
                        <asp:dropdownlist ID="ddlRetryTimeLapseMeasurement" CssClass="NormalTextBox" Runat="server" SelectedValue='<%# Bind("RetryTimeLapseMeasurement") %>'>
                            <asp:listitem Value="m">Minutes</asp:listitem>
                            <asp:listitem Value="h">Hours</asp:listitem>
                            <asp:listitem Value="d">Days</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: "5" and select "Minutes" to retry the task every 5 minutes after a failure.    Leave blank to disable retry-timer for this task.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Retain Schedule History:
                    </td>
                    <td class="Normal">
                        <asp:dropdownlist CssClass="NormalTextBox" ID="ddlRetainHistoryNum" Runat="server" SelectedValue='<%# Bind("RetainHistoryNum") %>'>
                            <asp:listitem Value="1">1</asp:listitem>
                            <asp:listitem Value="5">5</asp:listitem>
                            <asp:listitem Value="10">10</asp:listitem>
                            <asp:listitem Value="25">25</asp:listitem>
                            <asp:listitem Value="50">50</asp:listitem>
                            <asp:listitem Value="100">100</asp:listitem>
                            <asp:listitem Value="250">250</asp:listitem>
                            <asp:listitem Value="500">500</asp:listitem>
                            <asp:listitem Value="-1">All</asp:listitem>
                        </asp:dropdownlist><br>
                            Example: Select "10" to keep the ten most recent schedule history rows. 
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Run on Event:
                    </td>
                    <td class="Normal">
                        <asp:dropdownlist id="ddlAttachToEvent" cssclass="NormalTextBox" runat="server" SelectedValue='<%# Bind("AttachToEvent") %>'>
                            <asp:listitem>None</asp:listitem>
                            <asp:listitem Value="APPLICATION_START">Application Start</asp:listitem>
                        </asp:dropdownlist>
                        <br>
                            Example: Select "Application Start" to run this event when the web app starts.  Please note, 
                            events run on APPLICATION_END may not run reliably on some hosts.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Catch Up Enabled:
                    </td>
                    <td class="Normal">
                        <asp:checkbox CssClass="Normal" ID="chkCatchUpEnabled" Runat="server" Text="Yes" Checked='<%# Bind("CatchUpEnabled") %>' />
                        <br>
                            If checked, if the webserver is ever out of service, when the webserver is back 
                            in service this event will run once for each frequency that was missed during 
                            the downtime.
                    </td>
                </tr>

                <tr valign="top">
                    <td class="Normal">
                        Object Dependencies:
                    </td>
                    <td class="Normal">
                        <asp:textbox id="txtObjectDependencies" cssclass="NormalTextBox" Width="390px" maxlength="150"
                            runat="server" Text='<%# Bind("ObjectDependencies") %>' />
                        <br>
                            Enter the tables or other objects that this event is dependent on. Example: 
                            "SiteLog,Users,UsersOnline"
                    </td>
                </tr>

            </table>
            
            <asp:HiddenField
                ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
        ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
        SkinID="ButtonDelete" /><br />

            <table>
                <tr>
                    <td style="width: 100px">
                        Available Tasks:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblTypeFullName" runat="server" Text='<%# Bind("TypeFullName") %>'></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 100px">
                        Schedule Enabled:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblEnabled" runat="server" Text='<%# Bind("Enabled") %>'></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 100px">
                        Time Lapsed:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblTimeLapse" runat="server" Text='<%# GetTimeLapse(Int32.Parse(Eval("TimeLapse").ToString()), Eval("TimeLapseMeasurement").ToString()) %>'></asp:Label>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 100px">
                        Retry Frequency:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblRetryTimeLapse" runat="server" Text='<%# GetTimeLapse(Int32.Parse(Eval("RetryTimeLapse").ToString()), Eval("TimeLapseMeasurement").ToString()) %>'></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 100px">
                        Retain Schedule History:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblRetainHistoryNum" runat="server" Text='<%# Bind("RetainHistoryNum") %>'></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 100px">
                        Run on Event:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblAttachToEvent" runat="server" Text='<%# Bind("AttachToEvent") %>'></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 100px">
                        Catch Up Enabled:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblCatchUpEnabled" runat="server" Text='<%# Bind("CatchUpEnabled") %>'></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="width: 100px">
                        Object Dependencies:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="LblObjectDependencies" runat="server" Text='<%# Bind("ObjectDependencies") %>'></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
      
    </asp:FormView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="ScheduleDataSource" runat="server" DataObjectTypeName="Discovery.Scheduling.ScheduleItem"
        DeleteMethod="DeleteSchedule" InsertMethod="SaveSchedule" SelectMethod="GetSchedule"
        TypeName="Discovery.Scheduling.SchedulingController" UpdateMethod="SaveSchedule" OldValuesParameterFormatString="original_{0}" >
        
        <DeleteParameters>
            <asp:Parameter Name="scheduleID" Type="Int32" />
        </DeleteParameters>

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="scheduleID" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TypesObjectDataSource" runat="server" SelectMethod="GetAssemblyTypes"
        TypeName="Discovery.Scheduling.SchedulingController"></asp:ObjectDataSource>
</asp:Content>

