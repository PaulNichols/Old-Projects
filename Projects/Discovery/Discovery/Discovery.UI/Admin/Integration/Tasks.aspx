<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Tasks.aspx.cs"
    Inherits="Discovery.UI.Web.Admin.Tasks" Title="Integration Tasks" %>

<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        <table style="width: 100%">
            <tr>
                <td style="width: 100px">
                    Tasks</td>
            </tr>
        </table>
    </div>
    <hr />
    <br />
    <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" CausesValidation="False">
    </asp:ImageButton>
    <br />
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <Discovery:DiscoveryGrid DetailURL="task.aspx" DefaultSortExpression="Name" DataKeyNames="Id"
                ID="GridView1" runat="server" DataSourceID="TasksDataSource" DoHiLiteRow="True">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Task Name" SortExpression="Name" />
                    <asp:BoundField DataField="Frequency" HeaderText="Freq (Secs)" SortExpression="Frequency" />
                    <asp:TemplateField>
                        <headertemplate>
                            <table>
                                <tr>
                                    <td colspan="4" style="WIDTH: 400px; TEXT-ALIGN: center">Source Connection</TD>
                                </tr>
                                <tr>
                                  <td style="WIDTH: 120px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkSourceIdentifier" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="SourceConnectionIdentifier" runat="server" Text="ID"></asp:LinkButton>
                                    </td>
                                    
                                    <td style="WIDTH: 150px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkSourceName" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="SourceConnection.Name" runat="server" Text="Name"></asp:LinkButton>
                                    </td>
                                    <td style="WIDTH: 80px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkSourceType" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="SourceConnection.ConnectionType" runat="server" Text="Type"></asp:LinkButton>
                                    </td>
                                    <td style="WIDTH: 50px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkSourceChannel" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="SourceConnection.ChannelType" runat="server" Text="Channel"></asp:LinkButton>
                                    </td>
                                  
                                </tr>
                            </table>
                        </headertemplate>
                        
                        <itemtemplate>
                            <table>
                                <tr>
                                    <td style="WIDTH: 120px; TEXT-ALIGN: left">
                                        <asp:Literal id="Literal23" runat="server" Text='<%# Eval("SourceConnectionIdentifier") %>'></asp:Literal>
                                    </td>
                                   
                                    <td style="WIDTH: 150px; TEXT-ALIGN: left">
                                        <asp:Literal id="Literal4" runat="server" Text='<%# Eval("SourceConnection.Name") %>'></asp:Literal>
                                    </td>
                                    <td style="WIDTH: 80px; TEXT-ALIGN: left">
                                        <asp:Literal id="Literal1" runat="server" Text='<%# Eval("SourceConnection.ConnectionType") %>'></asp:Literal>
                                    </td>
                                            <td style="WIDTH: 50px; TEXT-ALIGN: left">
                                        <asp:Literal id="Literal13" runat="server" Text='<%# Eval("SourceConnection.ChannelType") %>'></asp:Literal>
                                    </td>                           
                                </tr>
                            </table>
                        </itemtemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                        <headertemplate>
                            <table>
                                <tr>
                                    <td colspan="4" style="WIDTH: 400; TEXT-ALIGN: center">Destination Connection</TD>
                                </tr>
                                <tr>
                                    <td style="WIDTH: 100px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkDestinationIdentifier" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DesctinationConnectionIdentifier" runat="server" Text="ID"></asp:LinkButton>
                                    </td>
                                  
                                    <td style="WIDTH: 150px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkDestinationName" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DestinationConnection.Name" runat="server" Text="Name"></asp:LinkButton>
                                    </td> 
                                      <td style="WIDTH: 80px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkDestinationType" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DestinationConnection.ConnectionType" runat="server" Text="Type"></asp:LinkButton>
                                    </td>                                    
                                    <td style="WIDTH: 50px; TEXT-ALIGN: left">
                                        <asp:LinkButton id="lnkDestinationChannel" CommandName="Sort" BackColor="#6B696B" Font-Bold="True" ForeColor="White" CommandArgument="DestinationConnection.ChannelType" runat="server" Text="Channel"></asp:LinkButton>
                                    </td>      
                                                              
                                </tr>
                            </table>
                        </headertemplate>
                        
                        <itemtemplate>
                            <table>
                                <tr>
                                    <td style="WIDTH: 100px; TEXT-ALIGN: left">
                                        <asp:Literal id="Literal43" runat="server" Text='<%# Eval("DestinationConnectionIdentifier") %>'></asp:Literal>
                                    </td>
                                      <td style="WIDTH: 150px" align="left">
                                        <asp:Literal id="Literal5" runat="server" Text='<%# Eval("DestinationConnection.Name") %>'></asp:Literal>
                                    </td>    
                                    <td style="WIDTH: 80px" align="left">
                                        <asp:Literal id="Literal6" runat="server" Text='<%# Eval("DestinationConnection.ConnectionType") %>'></asp:Literal>
                                    </td>
                                                                  
                                    <td style="WIDTH: 50px" align="left">
                                        <asp:Literal id="Literal7" runat="server" Text='<%# Eval("DestinationConnection.ChannelType") %>'></asp:Literal>
                                    </td>
                                  
                                </tr>
                            </table>
                        </itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </Discovery:DiscoveryGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="TasksDataSource" runat="server" SelectMethod="GetTasks"
        TypeName="Discovery.Integration.IntegrationController" OnSelected="ConnectionsDataSource_Selected">
    </asp:ObjectDataSource>
</asp:Content>
