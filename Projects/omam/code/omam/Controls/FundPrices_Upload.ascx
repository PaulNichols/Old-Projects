<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FundPrices_Upload.ascx.cs"
    Inherits="Controls_FundPrices_Upload" %>
<%@ Register Src="OffShorePriceEdit.ascx" TagName="OffShorePriceEdit" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<script language="javascript">
        function ValidateFile(source, args)     
        {
            var fileAndPath=document.getElementById(source.controltovalidate).value;
            var lastPathDelimiter=fileAndPath.lastIndexOf("\\");
            var fileNameOnly=fileAndPath.substring(lastPathDelimiter+1);
            var file_extDelimiter=fileNameOnly.lastIndexOf(".");
            var file_ext=fileNameOnly.substring(file_extDelimiter+1).toLowerCase();
            if(file_ext!="txt")             
            {
                 args.IsValid = false;                     
            }
        }
</script>

Please select Fund Prices file to upload (NOTE: If prices already exist they will
be overwitten and any manual alterations will be lost):
<br />
<asp:FileUpload ID="FileUploadPrices" runat="server" CssClass="FundUpload_UploadControl" /><asp:Button
    ID="ButtonUpload" runat="server" CssClass="FundUpload_Button" OnClick="UploadFile"
    Text="Upload" />
<br />
<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateFile"
    ControlToValidate="FileUploadPrices" CssClass="FundUpload_ValidateFileExtension"
    Display="dynamic" ErrorMessage="The upload file should have a .txt extension"></asp:CustomValidator><asp:BulletedList
        ID="BulletedListErrors" runat="server" CssClass="FundUpload_ServerValidationErrors">
    </asp:BulletedList>
<br />
<br />
<asp:Label ID="LabelHeader" runat="server" Text="Uploaded Prices:" Visible="False"
    CssClass="FundUpload_Header"></asp:Label><br />
<br />
<asp:GridView ID="GridViewUploadedPrices" runat="server" AutoGenerateColumns="False"
    CssClass="FundUpload_UploadGrid" DataKeyNames="Id" OnRowCancelingEdit="GridViewUploadedPrices_RowCancelingEdit"
    OnRowEditing="GridViewUploadedPrices_RowEditing" OnRowUpdating="GridViewUploadedPrices_RowUpdating">
    <Columns>
        <asp:TemplateField HeaderText="Fund Code"
            SortExpression="FundCode">
            <EditItemTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="FundUpload_FundCodeEdit" Text='<%# Bind("FundCode") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="FundUpload_FundCodeItem" Text='<%# Bind("FundCode") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="FundUpload_UploadDateHeader" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description" SortExpression="Description">
            <EditItemTemplate>
                <asp:Label ID="Label3" runat="server" CssClass="FundUpload_DescriptionEdit" Text='<%# Bind("Description") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" CssClass="FundUpload_DescriptionItem" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price Type" SortExpression="PriceType">
            <EditItemTemplate>
                <asp:Label ID="Label4" runat="server" CssClass="FundUpload_PriceTypeEdit" Text='<%# Bind("PriceType") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" CssClass="FundUpload_PriceTypeItem" Text='<%# Bind("PriceType") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Bid Price" SortExpression="BidPrice">
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxBidPrice" runat="server" CssClass="FundUpload_BidPriceEdit"
                    Text='<%# Bind("BidPrice") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorBid" runat="server" ControlToValidate="TextBoxBidPrice"
                    CssClass="FundUpload_BidValidator" ErrorMessage="Bid Price must be numeric"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="EditValidationControls">*</asp:CompareValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="FundUpload_BidPriceItem" Text='<%# Bind("BidPrice", "{0:c}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Offer Price" SortExpression="OfferPrice">
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxOfferPrice" runat="server" CssClass="FundUpload_OfferPriceEdit"
                    Text='<%# Bind("OfferPrice") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorOffer" runat="server" ControlToValidate="TextBoxOfferPrice"
                    CssClass="FundUpload_OfferValidator" ErrorMessage="Offer Price must be numeric"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="EditValidationControls">*</asp:CompareValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" CssClass="FundUpload_OfferPriceItem" Text='<%# Bind("OfferPrice", "{0:c}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Yield" SortExpression="Yield">
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxYield" runat="server" CssClass="FundUpload_YieldEdit"
                    Text='<%# Bind("Yield") %>'></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorYield" runat="server" ControlToValidate="TextBoxYield"
                    CssClass="FundUpload_YieldValidator" ErrorMessage="Yield must be numeric" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Double" ValidationGroup="EditValidationControls">*</asp:CompareValidator>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" CssClass="FundUpload_YieldItem" Text='<%# Bind("Yield", "{0:c}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Price Date" SortExpression="UploadDate">
            <EditItemTemplate>
                <asp:Label ID="Label7" runat="server" CssClass="FundUpload_UploadDateEdit"
                    Text='<%# Bind("UploadDate", "{0:dd/MM//yyyy HH:mm}") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="Label7" runat="server" CssClass="FundUpload_UploadDateItem" Text='<%# Bind("UploadDate", "{0:dd/MM//yyyy HH:mm}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Image" InsertVisible="False" ShowEditButton="True"
            ValidationGroup="EditValidationControls" >
            <ControlStyle CssClass="FundUpload_CommandButtonsControl" />
            <ItemStyle CssClass="FundUpload_CommandButtonsItem" />
        </asp:CommandField>
    </Columns>
</asp:GridView>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="FundUpload_ValidationSummary"
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="EditValidationControls" />
