<%@ Page Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="OpCoDivisions.aspx.cs" Inherits="Discovery.UI.Web.ReferenceData.OpCoDivisions" Title="Operating Company Divisions"  %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="Discovery" %>
      
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
  <div class="PageTitle">
        Operating Company Divisions</div>
        <hr />
        

        <asp:ImageButton ID="NewButton" runat="server" SkinID="NewButton" CausesValidation="False"/><br />
           
 <asp:UpdatePanel runat="server" ID="UpdatePanel3">
           
                <ContentTemplate>
        <Discovery:DiscoveryGrid AllowPaging="true"  DetailURL="OpcoDivision.aspx" DefaultSortExpression="OpCo.Description,Code"  ID="GridView1" runat="server" DataSourceID="OpCosDataSource"  DataKeyNames="Id">
            <Columns>
              <asp:TemplateField HeaderText="Operating Company" SortExpression="OpCo.Description">
                  <ItemTemplate>
                        <asp:Label ID="LabelOpCoDescription" runat="server" Text='<%# Eval("OpCo.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Code" HeaderText="Division Code" SortExpression="Code" />
                
            </Columns>
                 
         </Discovery:DiscoveryGrid>
              </ContentTemplate>
            </asp:UpdatePanel>
        
        <asp:ObjectDataSource ID="OpCosDataSource" runat="server" SelectMethod="GetOpCoDivisions"
            TypeName="Discovery.BusinessObjects.Controllers.OpcoDivisionController">
            <SelectParameters>
                <asp:Parameter DefaultValue="true" Name="fullyPopulate" Type="Boolean" />
                <asp:Parameter DefaultValue="OpCo.Description,Code" Name="sortExpression" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
   
</asp:Content>

