<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Sequences.aspx.cs" Inherits="Discovery.UI.Web.Admin.Sequences" Title="Untitled Page"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls" TagPrefix="Discovery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div class="PageTitle">
        Sequences</div>
        <hr />
        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" />
              <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
          
        <Discovery:DiscoveryGrid DetailURL="Sequence.aspx" DefaultSortExpression="Name"  ID="GridView1" runat="server" DataSourceID="SequencesDataSource" DataKeyNames="Id" DoHiLiteRow="True" AutoGenerateColumns="False" Width="224px">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Seed" HeaderText="Starting Value"/>
                <asp:BoundField DataField="Increment" HeaderText="Increment Value"/>
                <asp:BoundField DataField="CurrentValue" HeaderText="Current Value"/>
            </Columns>
          
        </Discovery:DiscoveryGrid>
          </ContentTemplate>
            </asp:UpdatePanel>
        <asp:ObjectDataSource ID="SequencesDataSource" runat="server" SelectMethod="GetSequences"
            TypeName="Discovery.BusinessObjects.Controllers.SequenceController"></asp:ObjectDataSource>
    
</asp:Content>

