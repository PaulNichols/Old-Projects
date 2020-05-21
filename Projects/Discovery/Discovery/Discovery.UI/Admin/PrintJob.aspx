<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PrintJob.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.PrintJob" Title="Print Job"  %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>

   <div class="PageTitle">
        Print Job</div>
        <hr />
        <br />
    <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />


    <asp:FormView EnableViewState="False" ID="OpCoFormView" runat="server" DataSourceID="PrintJobObjectDataSource"    >
  
        <ItemTemplate>
            <asp:ImageButton ID="ImageButtonPause" runat="server" AlternateText="Pause" ImageUrl="~/Images/media_pause.gif"
                OnClick="ImageButtonPause_Click" />
            <asp:ImageButton ID="ImageButtonResume" runat="server" AlternateText="Resume" ImageUrl="~/Images/media_play_green.gif"
                OnClick="ImageButtonResume_Click" />
            <asp:ImageButton ID="ImageButtonCancel" runat="server" AlternateText="Cancel" ImageUrl="~/Images/delete2.gif"
                OnClick="ImageButtonCancel_Click" /><br />
            <table id="TABLE1" onclick="return TABLE1_onclick()">
                <tr>
                    <td style="width: 100px">
                        Job Id:
                    </td>
                    <td style="width: 152px">
                        <asp:Label ID="JobIdLabel" runat="server" Text='<%# Bind("JobId") %>'></asp:Label></td>
                    <td style="width: 99px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Name:</td>
                    <td style="width: 152px">
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Printer Name:</td>
                    <td style="width: 100px">
                        <asp:Label ID="PrinterNameLabel" runat="server" Text='<%# Bind("PrinterName") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Document:</td>
                    <td style="width: 152px">
                        <asp:Label ID="DocumentLabel" runat="server" Text='<%# Bind("Document") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Status:</td>
                    <td style="width: 100px">
                        <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Owner:</td>
                    <td style="width: 152px">
                        <asp:Label ID="OwnerLabel" runat="server" Text='<%# Bind("Owner") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Caption:</td>
                    <td style="width: 100px">
                        <asp:Label ID="CaptionLabel" runat="server" Text='<%# Bind("Caption") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px; height: 21px">
                        Total Pages:</td>
                    <td style="width: 152px; height: 21px">
                        <asp:Label ID="TotalPagesLabel" runat="server" Text='<%# Bind("TotalPages") %>'></asp:Label></td>
                    <td style="width: 99px; height: 21px">
                        Description:</td>
                    <td style="width: 100px; height: 21px">
                        <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Priority:
                    </td>
                    <td style="width: 152px">
                        <asp:Label ID="PriorityLabel" runat="server" Text='<%# Bind("Priority") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Job Status:</td>
                    <td style="width: 100px">
                        <asp:Label ID="JobStatusLabel" runat="server" Text='<%# Bind("JobStatus") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Driver Name:
                    </td>
                    <td style="width: 152px">
                        <asp:Label ID="DriverNameLabel" runat="server" Text='<%# Bind("DriverName") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Host Print Queue:</td>
                    <td style="width: 100px">
                        <asp:Label ID="HostPrintQueueLabel" runat="server" Text='<%# Bind("HostPrintQueue") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        PaperWidth:</td>
                    <td style="width: 152px">
                        <asp:Label ID="PaperWidthLabel" runat="server" Text='<%# Bind("PaperWidth") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Paper Size:</td>
                    <td style="width: 100px">
                        <asp:Label ID="PaperSizeLabel" runat="server" Text='<%# Bind("PaperSize") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Paper Length:
                    </td>
                    <td style="width: 152px">
                        <asp:Label ID="PaperLengthLabel" runat="server" Text='<%# Bind("PaperLength") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Size:</td>
                    <td style="width: 100px">
                        <asp:Label ID="SizeLabel" runat="server" Text='<%# Bind("Size") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Data Type:</td>
                    <td style="width: 152px">
                        <asp:Label ID="DataTypeLabel" runat="server" Text='<%# Bind("DataType") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Print Processor:</td>
                    <td style="width: 100px">
                        <asp:Label ID="PrintProcessorLabel" runat="server" Text='<%# Bind("PrintProcessor") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Notify:</td>
                    <td style="width: 152px">
                        <asp:Label ID="NotifyLabel" runat="server" Text='<%# Bind("Notify") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Until Time:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="UntilTimeLabel" runat="server" Text='<%# Bind("UntilTime") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Pages Printed:</td>
                    <td style="width: 152px">
                        <asp:Label ID="PagesPrintedLabel" runat="server" Text='<%# Bind("PagesPrinted") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Elapsed Time:</td>
                    <td style="width: 100px">
                        <asp:Label ID="ElapsedTimeLabel" runat="server" Text='<%# Bind("ElapsedTime") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Parameters:</td>
                    <td style="width: 152px">
                        <asp:Label ID="ParametersLabel" runat="server" Text='<%# Bind("Parameters") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Color:</td>
                    <td style="width: 100px">
                        <asp:Label ID="ColorLabel" runat="server" Text='<%# Bind("Color") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Start Time:</td>
                    <td style="width: 152px">
                        <asp:Label ID="StartTimeLabel" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Install Date:</td>
                    <td style="width: 100px">
                        <asp:Label ID="InstallDateLabel" runat="server" Text='<%# Bind("InstallDate") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Status Mask:</td>
                    <td style="width: 152px">
                        <asp:Label ID="StatusMaskLabel" runat="server" Text='<%# Bind("StatusMask") %>'></asp:Label></td>
                    <td style="width: 99px">
                        Time Submitted:
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="TimeSubmittedLabel" runat="server" Text='<%# Bind("TimeSubmitted") %>'></asp:Label></td>
                </tr>
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            PrintProcessor:
            <asp:TextBox ID="PrintProcessorTextBox" runat="server" Text='<%# Bind("PrintProcessor") %>'>
            </asp:TextBox><br />
            UntilTime:
            <asp:TextBox ID="UntilTimeTextBox" runat="server" Text='<%# Bind("UntilTime") %>'>
            </asp:TextBox><br />
            PrinterName:
            <asp:TextBox ID="PrinterNameTextBox" runat="server" Text='<%# Bind("PrinterName") %>'>
            </asp:TextBox><br />
            PagesPrinted:
            <asp:TextBox ID="PagesPrintedTextBox" runat="server" Text='<%# Bind("PagesPrinted") %>'>
            </asp:TextBox><br />
            Parameters:
            <asp:TextBox ID="ParametersTextBox" runat="server" Text='<%# Bind("Parameters") %>'>
            </asp:TextBox><br />
            PaperSize:
            <asp:TextBox ID="PaperSizeTextBox" runat="server" Text='<%# Bind("PaperSize") %>'>
            </asp:TextBox><br />
            Notify:
            <asp:TextBox ID="NotifyTextBox" runat="server" Text='<%# Bind("Notify") %>'>
            </asp:TextBox><br />
            PaperLength:
            <asp:TextBox ID="PaperLengthTextBox" runat="server" Text='<%# Bind("PaperLength") %>'>
            </asp:TextBox><br />
            TotalPages:
            <asp:TextBox ID="TotalPagesTextBox" runat="server" Text='<%# Bind("TotalPages") %>'>
            </asp:TextBox><br />
            ElapsedTime:
            <asp:TextBox ID="ElapsedTimeTextBox" runat="server" Text='<%# Bind("ElapsedTime") %>'>
            </asp:TextBox><br />
            JobId:
            <asp:TextBox ID="JobIdTextBox" runat="server" Text='<%# Bind("JobId") %>'>
            </asp:TextBox><br />
            Status:
            <asp:TextBox ID="StatusTextBox" runat="server" Text='<%# Bind("Status") %>'>
            </asp:TextBox><br />
            Priority:
            <asp:TextBox ID="PriorityTextBox" runat="server" Text='<%# Bind("Priority") %>'>
            </asp:TextBox><br />
            StartTime:
            <asp:TextBox ID="StartTimeTextBox" runat="server" Text='<%# Bind("StartTime") %>'>
            </asp:TextBox><br />
            Color:
            <asp:TextBox ID="ColorTextBox" runat="server" Text='<%# Bind("Color") %>'>
            </asp:TextBox><br />
            Owner:
            <asp:TextBox ID="OwnerTextBox" runat="server" Text='<%# Bind("Owner") %>'>
            </asp:TextBox><br />
            Document:
            <asp:TextBox ID="DocumentTextBox" runat="server" Text='<%# Bind("Document") %>'>
            </asp:TextBox><br />
            Caption:
            <asp:TextBox ID="CaptionTextBox" runat="server" Text='<%# Bind("Caption") %>'>
            </asp:TextBox><br />
            JobStatus:
            <asp:TextBox ID="JobStatusTextBox" runat="server" Text='<%# Bind("JobStatus") %>'>
            </asp:TextBox><br />
            StatusMask:
            <asp:TextBox ID="StatusMaskTextBox" runat="server" Text='<%# Bind("StatusMask") %>'>
            </asp:TextBox><br />
            DriverName:
            <asp:TextBox ID="DriverNameTextBox" runat="server" Text='<%# Bind("DriverName") %>'>
            </asp:TextBox><br />
            TimeSubmitted:
            <asp:TextBox ID="TimeSubmittedTextBox" runat="server" Text='<%# Bind("TimeSubmitted") %>'>
            </asp:TextBox><br />
            HostPrintQueue:
            <asp:TextBox ID="HostPrintQueueTextBox" runat="server" Text='<%# Bind("HostPrintQueue") %>'>
            </asp:TextBox><br />
            Size:
            <asp:TextBox ID="SizeTextBox" runat="server" Text='<%# Bind("Size") %>'>
            </asp:TextBox><br />
            Description:
            <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'>
            </asp:TextBox><br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>'>
            </asp:TextBox><br />
            InstallDate:
            <asp:TextBox ID="InstallDateTextBox" runat="server" Text='<%# Bind("InstallDate") %>'>
            </asp:TextBox><br />
            PaperWidth:
            <asp:TextBox ID="PaperWidthTextBox" runat="server" Text='<%# Bind("PaperWidth") %>'>
            </asp:TextBox><br />
            DataType:
            <asp:TextBox ID="DataTypeTextBox" runat="server" Text='<%# Bind("DataType") %>'>
            </asp:TextBox><br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Update">
            </asp:LinkButton>
            <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel">
            </asp:LinkButton>
        </EditItemTemplate>
        <InsertItemTemplate>
            PrintProcessor:
            <asp:TextBox ID="PrintProcessorTextBox" runat="server" Text='<%# Bind("PrintProcessor") %>'>
            </asp:TextBox><br />
            UntilTime:
            <asp:TextBox ID="UntilTimeTextBox" runat="server" Text='<%# Bind("UntilTime") %>'>
            </asp:TextBox><br />
            PrinterName:
            <asp:TextBox ID="PrinterNameTextBox" runat="server" Text='<%# Bind("PrinterName") %>'>
            </asp:TextBox><br />
            PagesPrinted:
            <asp:TextBox ID="PagesPrintedTextBox" runat="server" Text='<%# Bind("PagesPrinted") %>'>
            </asp:TextBox><br />
            Parameters:
            <asp:TextBox ID="ParametersTextBox" runat="server" Text='<%# Bind("Parameters") %>'>
            </asp:TextBox><br />
            PaperSize:
            <asp:TextBox ID="PaperSizeTextBox" runat="server" Text='<%# Bind("PaperSize") %>'>
            </asp:TextBox><br />
            Notify:
            <asp:TextBox ID="NotifyTextBox" runat="server" Text='<%# Bind("Notify") %>'>
            </asp:TextBox><br />
            PaperLength:
            <asp:TextBox ID="PaperLengthTextBox" runat="server" Text='<%# Bind("PaperLength") %>'>
            </asp:TextBox><br />
            TotalPages:
            <asp:TextBox ID="TotalPagesTextBox" runat="server" Text='<%# Bind("TotalPages") %>'>
            </asp:TextBox><br />
            ElapsedTime:
            <asp:TextBox ID="ElapsedTimeTextBox" runat="server" Text='<%# Bind("ElapsedTime") %>'>
            </asp:TextBox><br />
            JobId:
            <asp:TextBox ID="JobIdTextBox" runat="server" Text='<%# Bind("JobId") %>'>
            </asp:TextBox><br />
            Status:
            <asp:TextBox ID="StatusTextBox" runat="server" Text='<%# Bind("Status") %>'>
            </asp:TextBox><br />
            Priority:
            <asp:TextBox ID="PriorityTextBox" runat="server" Text='<%# Bind("Priority") %>'>
            </asp:TextBox><br />
            StartTime:
            <asp:TextBox ID="StartTimeTextBox" runat="server" Text='<%# Bind("StartTime") %>'>
            </asp:TextBox><br />
            Color:
            <asp:TextBox ID="ColorTextBox" runat="server" Text='<%# Bind("Color") %>'>
            </asp:TextBox><br />
            Owner:
            <asp:TextBox ID="OwnerTextBox" runat="server" Text='<%# Bind("Owner") %>'>
            </asp:TextBox><br />
            Document:
            <asp:TextBox ID="DocumentTextBox" runat="server" Text='<%# Bind("Document") %>'>
            </asp:TextBox><br />
            Caption:
            <asp:TextBox ID="CaptionTextBox" runat="server" Text='<%# Bind("Caption") %>'>
            </asp:TextBox><br />
            JobStatus:
            <asp:TextBox ID="JobStatusTextBox" runat="server" Text='<%# Bind("JobStatus") %>'>
            </asp:TextBox><br />
            StatusMask:
            <asp:TextBox ID="StatusMaskTextBox" runat="server" Text='<%# Bind("StatusMask") %>'>
            </asp:TextBox><br />
            DriverName:
            <asp:TextBox ID="DriverNameTextBox" runat="server" Text='<%# Bind("DriverName") %>'>
            </asp:TextBox><br />
            TimeSubmitted:
            <asp:TextBox ID="TimeSubmittedTextBox" runat="server" Text='<%# Bind("TimeSubmitted") %>'>
            </asp:TextBox><br />
            HostPrintQueue:
            <asp:TextBox ID="HostPrintQueueTextBox" runat="server" Text='<%# Bind("HostPrintQueue") %>'>
            </asp:TextBox><br />
            Size:
            <asp:TextBox ID="SizeTextBox" runat="server" Text='<%# Bind("Size") %>'>
            </asp:TextBox><br />
            Description:
            <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'>
            </asp:TextBox><br />
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>'>
            </asp:TextBox><br />
            InstallDate:
            <asp:TextBox ID="InstallDateTextBox" runat="server" Text='<%# Bind("InstallDate") %>'>
            </asp:TextBox><br />
            PaperWidth:
            <asp:TextBox ID="PaperWidthTextBox" runat="server" Text='<%# Bind("PaperWidth") %>'>
            </asp:TextBox><br />
            DataType:
            <asp:TextBox ID="DataTypeTextBox" runat="server" Text='<%# Bind("DataType") %>'>
            </asp:TextBox><br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                Text="Insert">
            </asp:LinkButton>
            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel">
            </asp:LinkButton>
        </InsertItemTemplate>
      
       
    </asp:FormView>
    <asp:ObjectDataSource ID="PrintJobObjectDataSource" runat="server" SelectMethod="GetPrintJob" TypeName="Discovery.BusinessObjects.Controllers.PrinterController">
        <SelectParameters>
            <asp:QueryStringParameter Name="jobId" QueryStringField="Id" Type="UInt32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
</asp:Content>

