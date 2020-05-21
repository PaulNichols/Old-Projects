<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="LogEntry.aspx.cs"
    Inherits="Discovery.UI.Web.ReferenceData.LogEntry" Title="Management Server - Log Entries" %>

<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Log Entry</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
    <asp:FormView Width="100%" ID="logentryFormView" runat="server" EnableViewState="true"
        DataSourceID="logentryDataSource" DataKeyNames="Id" OnDataBound="logentryFormView_DataBound">
        <ItemTemplate>
        
            <asp:ImageButton ID="ButtonAck"  SkinID="ButtonAcknowledge"  runat="server" OnClick="ButtonAcknowledged_Click" /><br />
              
            <table>
                <tr>
                    <td style="width: 125px">
                        Error Number:</td>
                    <td>
                        <asp:Label ID="LabelEventId" runat="server" Text='<%# Eval("EventId") %>'></asp:Label></td>
                </tr>
                  <tr>
                    <td style="width: 125px">
                        Acknowledged By:</td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("AcknowledgedBy") %>'></asp:Label></td>
                </tr>
                  <tr>
                    <td style="width: 125px">
                        Acknowledged Date:</td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("AcknowledgedDate") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        Title:</td>
                    <td>
                        <asp:Label ID="LabelTitle" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        Date/Time:</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("TimeStamp") %>'></asp:Label></td>
                </tr>
           
                <tr>
                    <td style="width: 125px">
                        Severity:</td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Severity") %>'></asp:Label></td>
                </tr>
                <tr >
                    <td valign="top" style="width: 125px">
                        Message:</td>
                    <td>
                        <asp:Label ID="Label1" visible='<%# Eval("FormattedMessageXML").ToString()=="" %>' runat="server" Text='<%# Eval("Message").ToString().Replace(Environment.NewLine,"<BR>") %>'></asp:Label>
                        <asp:Label ID="Label5" visible='<%# Eval("FormattedMessageXML").ToString()!="" %>' runat="server" Text="See below"></asp:Label>
                        
                        </td>
                </tr>
               
                <tr >
                    <td style="width: 125px">
                        Formatted Message:</td>
                    <td>
                        <asp:Label ID="Label4" runat="server"  visible='<%# Eval("FormattedMessageXML").ToString()=="" %>'  Text='<%# Eval("FormattedMessage").ToString().Replace(Environment.NewLine,"<BR>") %>'></asp:Label>
                        <asp:Label ID="Label6" visible='<%# Eval("FormattedMessageXML").ToString()!="" %>' runat="server" Text="See below"></asp:Label>
                        
                        </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="LabelDetails" runat="server" Visible='<%# Eval("FormattedMessageXML").ToString()!="" %>'
                Text='Entry Details'></asp:Label><asp:Image ID="CriteriaMinMax" Visible='<%# Eval("FormattedMessageXML").ToString()!="" %>'
                    ImageUrl="~/Images/Container/Max.gif" runat="server" />
            <asp:Panel ID="Panel1" Visible='<%# Eval("FormattedMessageXML").ToString()!="" %>'
                runat="server" Width="100%">
                <asp:Xml ID="XmlBasic" runat="server" Visible='<%# Eval("FormattedMessageXML").ToString()!="" %>'
                    TransformSource="LogMessage.xsl" DocumentContent='<%# Eval("FormattedMessageXML") %>'></asp:Xml><br />
                <!-- AJAX Extender -->
                <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="ExtendedPanel"
                    CollapsedSize="0" ExpandedSize="250" Collapsed="True" SuppressPostBack="true"
                    ExpandControlID="CriteriaMinMax" CollapseControlID="CriteriaMinMax" ScrollContents="True"
                    TextLabelID="LabelDetails" CollapsedText="" ExpandedText="" ExpandedImage="~/Images/Container/Min.gif"
                    CollapsedImage="~/Images/Container/Max.gif" ImageControlID="CriteriaMinMax" />
                <br />
                <asp:Panel Width="100%" runat="server" ID="ExtendedPanel">
                    <asp:Xml ID="XmlExtended" runat="server" TransformSource="LogMessageExtended.xsl"
                        DocumentContent='<%# Eval("FormattedMessageXML") %>'></asp:Xml>
                </asp:Panel>
            </asp:Panel>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
      </ContentTemplate>
     
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="logentryDataSource" runat="server" SelectMethod="GetLogEntry"
        TypeName="Discovery.BusinessObjects.Controllers.LogController">
        <SelectParameters>
            <asp:QueryStringParameter Name="logId" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
