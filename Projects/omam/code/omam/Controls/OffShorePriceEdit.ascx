<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OffShorePriceEdit.ascx.cs"
    Inherits="Controls_OffShorePrice" %>
<p>
</p>
<asp:Label ID="LabelHeader" CssClass="OffShoreEdit_Header" runat="server" Text="Old Mutal Dublin-based Funds"></asp:Label><br />
<br />
<asp:Label ID="LabelText" runat="server" CssClass="OffShoreEdit_Text">Our Dublin-based funds are managed by Old Mutual Asset Managers (UK) Limited, a sister company of Old Mutual Fund Managers. These are the latest pulished prices for Global Dynamic Fund (updated weekly) and UK Select Smaller Companies Fund (updated daily).</asp:Label><br />
<br />
<asp:GridView ID="GridViewOffShoreFunds" runat="server" AutoGenerateColumns="False"
    CssClass="OffShoreGrid" DataKeyNames="OMAMFundId" OnRowCancelingEdit="GridViewOffShoreFunds_RowCancelingEdit"
    OnRowEditing="GridViewOffShoreFunds_RowEditing" OnRowUpdating="GridViewOffShoreFunds_RowUpdating">
    <Columns>
        <asp:TemplateField HeaderText="Fund">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="OffShoreEdit_FundName" Text='<%# Bind("FundName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Currency">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="OffShoreEdit_Currency" Text='<%# Bind("Currency") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price">
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxPrice" runat="server" CssClass="OffShoreEdit_PriceEdit" Text='<%# Bind("Price") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorPrice" runat="server" ControlToValidate="TextBoxPrice"
                    CssClass="OffShoreEdit_EditValidator" ErrorMessage="Price must be numeric" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Double" ValidationGroup="EditValidationControls">*</asp:CompareValidator>
            </EditItemTemplate>
                       <ItemTemplate>
                <asp:Label ID="Label3" runat="server" CssClass="OffShoreEdit_Price" Text='<%# Bind("Price", "{0:N4}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Image" InsertVisible="False" ShowEditButton="True"
            ValidationGroup="EditValidationControls">
            <ControlStyle CssClass="FundUpload_CommandButtonsControl" />
            <ItemStyle CssClass="FundUpload_CommandButtonsItem" />
        </asp:CommandField>
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="LabelNoFunds" runat="server" CssClass="OffShoreEdit_NoFunds" Text="There are no Off-Shore Funds"></asp:Label>
    </EmptyDataTemplate>
    <HeaderStyle CssClass="OffShoreEdit_GridHeader" />
</asp:GridView>
<asp:ValidationSummary ID="EditValidationControls" runat="server" CssClass="OffShoreEdit_ValidationSummary"
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="EditValidationControls" />
