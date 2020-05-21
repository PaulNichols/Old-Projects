<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Warehouse.aspx.cs"
    Inherits="Discovery.UI.Web.ReferenceData.Warehouse" Title="Management Server - Warehouses" %>

<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Register Assembly="Discovery.Web.UI.CustomControls" Namespace="Discovery.Web.UI.CustomControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Warehouses</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack" runat="server">Back</asp:HyperLink><br />
    <asp:FormView ID="WarehouseFormView" runat="server" DataSourceID="WarehouseDataSource"
        DataKeyNames="Id,Code"  EnableViewState="False">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <div>
                <table>
                    <tr>
                        <td style="width: 145px">
                            Code:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Description:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Printer:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxPrinterName" runat="server" Text='<%# Bind("PrinterName") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Sales Email:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxSalesEmail" runat="server" Text='<%# Bind("SalesEmail") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Has Optrak:</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxHasOptrak" runat="server" Checked='<%# Bind("HasOptrak") %>' /></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Has Commander:</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxCommander" runat="server" Checked='<%# Bind("HasCommander") %>' /></td>
                    </tr>
               
                    <tr>
                        <td style="width: 145px">
                            Contact Name:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxContactName" runat="server" Text='<%# Bind("ContactName") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Contact Telephone:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxContactTelephone" runat="server" Text='<%# Bind("ContactTelephoneNumber") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Address:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress1" runat="server" Text='<%# Bind("AddressLine1") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress2" runat="server" Text='<%# Bind("AddressLine2") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress3" runat="server" Text='<%# Bind("AddressLine3") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress4" runat="server" Text='<%# Bind("AddressLine4") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Post Code:</td>
                        <td style="width: 272px">
                            <cc1:DiscoveryUppercaseText ID="TextBoxPostCode" runat="server" Text='<%# Bind("PostCode") %>'></cc1:DiscoveryUppercaseText ></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Is TDC?</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxIsTDC" runat="server" Checked='<%# Bind("IsTDC") %>' /></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Optrak Region:</td>
                        <td style="width: 272px">
                            <asp:DropDownList ID="DropDownListRegion" runat="server" AppendDataBoundItems="True"
                                DataSourceID="RegionDataSource" DataTextField="Description" DataValueField="Id"
                                SelectedValue='<%# Bind("RegionId") %>' Width="159px">
                            </asp:DropDownList><asp:ObjectDataSource ID="RegionDataSource" runat="server" SelectMethod="GetRegions"
                                TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
                <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
                <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
                <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            </div>
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender  ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxHasOptrak"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender  ID="ToggleButtonExtender2" runat="server" TargetControlID="CheckBoxCommander"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender  ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsTDC"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert"></asp:ImageButton>
            <asp:ImageButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                CommandName="Cancel" OnClick="InsertCancelButton_Click" SkinID="ButtonCancel"></asp:ImageButton>
            <br />
            <div>
                <table>
                    <tr>
                        <td style="width: 145px">
                            Code:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Description:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Printer:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxPrinterName" runat="server" Text='<%# Bind("PrinterName") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Sales Email:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxSalesEmail" runat="server" Text='<%# Bind("SalesEmail") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Has Optrak:</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxHasOptrak" runat="server" Checked='<%# Bind("HasOptrak") %>' /></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Has Commander:</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxCommander" runat="server" Checked='<%# Bind("HasCommander") %>' /></td>
                    </tr>
         
                    <tr>
                        <td style="width: 145px">
                            Contact Name:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxContactName" runat="server" Text='<%# Bind("ContactName") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Contact Telephone:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxContactTelephone" runat="server" Text='<%# Bind("ContactTelephoneNumber") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Address:</td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress1" runat="server" Text='<%# Bind("AddressLine1") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress2" runat="server" Text='<%# Bind("AddressLine2") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress3" runat="server" Text='<%# Bind("AddressLine3") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                        </td>
                        <td style="width: 272px">
                            <asp:TextBox ID="TextBoxAddress4" runat="server" Text='<%# Bind("AddressLine4") %>'></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Post Code:</td>
                        <td style="width: 272px">
                            <cc1:DiscoveryUppercaseText ID="TextBoxPostCode" runat="server" Text='<%# Bind("PostCode") %>'></cc1:DiscoveryUppercaseText></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Is TDC?</td>
                        <td style="width: 272px">
                            <asp:CheckBox ID="CheckBoxIsTDC" runat="server" Checked='<%# Bind("IsTDC") %>' /></td>
                    </tr>
                    <tr>
                        <td style="width: 145px">
                            Optrak Region:</td>
                        <td style="width: 272px">
                            <asp:DropDownList ID="DropDownListRegion" runat="server" AppendDataBoundItems="True"
                                DataSourceID="RegionDataSource" DataTextField="Description" DataValueField="Id"
                                SelectedValue='<%# Bind("RegionId") %>' Width="159px">
                                <asp:ListItem Selected="True"></asp:ListItem>
                            </asp:DropDownList><asp:ObjectDataSource ID="RegionDataSource" runat="server" SelectMethod="GetRegions"
                                TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HiddenField ID="Id" runat="server" Value='-1' />
                <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
                <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
            </div>
            <!-- AJAX Extender -->
            <ajaxToolkit:ToggleButtonExtender  ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckBoxHasOptrak"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender ID="ToggleButtonExtender2" runat="server" TargetControlID="CheckBoxCommander"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender  ID="ToggleButtonExtender3" runat="server" TargetControlID="CheckBoxIsTDC"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton CommandName="Edit" ID="ButtonEdit" SkinID="ButtonEdit" runat="server" />
            <asp:ImageButton CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                ID="ButtonDelete" SkinID="ButtonDelete" runat="server" /><br />
            <table>
                <tr>
                    <td style="width: 145px">
                        Code:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Description:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Printer:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelPrinterName" runat="server" Text='<%# Eval("PrinterName") %>' Width="129px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Sales Email:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelSalesEmail" runat="server" Text='<%# Eval("SalesEmail") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Has Optrak:</td>
                    <td style="width: 272px">
                        <asp:Image ID="ImageHasOptrak" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("HasOptrak")) %>' />
                        <asp:Image ID="ImageNotOptrak" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("HasOptrak")) %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Has Commander:</td>
                    <td style="width: 272px">
                        <asp:Image ID="ImageHasCommander" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("HasCommander")) %>' />
                        <asp:Image ID="ImageNotCommander" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("HasCommander")) %>' />
                    </td>
                </tr>
              
                <tr>
                    <td style="width: 145px">
                        Contact Name:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelContactName" runat="server" Text='<%# Eval("ContactName") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Contact Telephone:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelContactTelephone" runat="server" Text='<%# Eval("ContactTelephoneNumber") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Address:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelAddressLine1" runat="server" Text='<%# Eval("AddressLine1") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                    </td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelAddressLine2" runat="server" Text='<%# Eval("AddressLine2") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                    </td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelAddressLine3" runat="server" Text='<%# Eval("AddressLine3") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                    </td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelAddressLine4" runat="server" Text='<%# Eval("AddressLine4") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Post Code:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelPostCode" runat="server" Text='<%# Eval("PostCode") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Is TDC?</td>
                    <td style="width: 272px">
                        <asp:Image ID="ImageIsTDC" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("IsTDC")) %>' />
                        <asp:Image ID="ImageNotTDC" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("IsTDC")) %>' />
                    </td>
                </tr>
                <tr>
                    <td style="width: 145px">
                        Optrak Region:</td>
                    <td style="width: 272px">
                        <asp:Label ID="LabelRegion" runat="server" Text='<%# Eval("OptrakRegion.Description") %>'></asp:Label></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="WarehouseDataSource" runat="server" SelectMethod="GetWarehouse"
        TypeName="Discovery.UI.Web.Controllers.WarehouseController" DeleteMethod="DeleteWarehouse"
        InsertMethod="SaveWarehouse" UpdateMethod="SaveWarehouse" DataObjectTypeName="Discovery.UI.Web.Controllers.UIWarehouse" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="WarehouseId" QueryStringField="id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
