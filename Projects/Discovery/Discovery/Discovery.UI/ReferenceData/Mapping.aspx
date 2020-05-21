<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Mapping.aspx.cs" Inherits="Discovery.UI.Web.Mapping.Mapping" Title="Mangement Server - Mapping Maintenance" %>
<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <div class="PageTitle">
        Mappings</div>
    <hr /><asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink>
    <asp:FormView EnableViewState="true"
        ID="MappingFormView" runat="server" DataSourceID="MappingDataSource"
         OnItemInserting="MappingFormView_ItemInserting" DataKeyNames="Id" OnItemUpdating="MappingFormView_ItemUpdating">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 135px">
                        Source System:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceSystem" runat="server" Width="200px" SelectedValue='<%# Bind("SourceSystemId") %>' DataSourceID="SourceSystemDataSource" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="SourceSystemDataSource" runat="server"
                            SelectMethod="GetMappingSourceSystems" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination System:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListDestinationSystem" runat="server" Width="200px" SelectedValue='<%# Bind("DestinationSystemId") %>' DataSourceID="DestinationSystemDataSource" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="DestinationSystemDataSource" runat="server"
                            SelectMethod="GetMappingDestinationSystems" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Type:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceType" runat="server" DataSourceID="SourceTypeDataSource"
                            DataTextField="SourceType" DataValueField="SourceTypeFullName" Width="400px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSourceType_SelectedIndexChanged" OnDataBound="DropDownListSourceType_DataBound" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="SourceTypeDataSource" runat="server"
                            SelectMethod="GetUniqueMappingSourceTypes" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Type:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListDestinationType" runat="server" Width="400px" AutoPostBack="True"  DataTextField="DestinationType" DataValueField="Id" OnDataBound="DropDownListDestinationType_DataBound" OnSelectedIndexChanged="DropDownListDestinationType_SelectedIndexChanged">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Property:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceProperty" runat="server" Width="400px" DataTextField="SourceProperty" DataValueField="SourceProperty" OnDataBound="DropDownListSourceProperty_DataBound" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSourceProperty_SelectedIndexChanged" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Property:</td>
                    <td style="width: 254px">
                        <asp:DropDownList AutoPostBack="true" ID="DropDownListDestinationProperty" runat="server" 
                            DataTextField="DestinationProperty" DataValueField="Id" 
                            Width="400px" OnDataBound="DropDownListDestinationProperty_DataBound" OnSelectedIndexChanged="DropDownListDestinationProperty_SelectedIndexChanged">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        From Value:</td>
                    <td style="width: 254px">
                        <asp:TextBox ID="TextBoxSourceValue" runat="server" Text='<%# Bind("SourceValue") %>'
                            Width="276px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 135px; height: 48px;">
                        To Value:</td>
                    <td style="width: 254px;">
                        <asp:TextBox ID="TextBoxDestinationValue" runat="server" Visible="False" Width="276px"></asp:TextBox><asp:DropDownList ID="DropDownListDestinationValue"
                                runat="server" Visible="False" Width="400px" OnDataBound="DropDownListDestinationValue_DataBound">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonSave" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" /><br />
            <table>
                <tr>
                    <td style="width: 135px">
                        Source System:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceSystem" runat="server" Width="200px" SelectedValue='<%# Bind("SourceSystemId") %>' DataSourceID="SourceSystemDataSource" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="SourceSystemDataSource" runat="server"
                            SelectMethod="GetMappingSourceSystems" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination System:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListDestinationSystem" runat="server" Width="200px" SelectedValue='<%# Bind("DestinationSystemId") %>' DataSourceID="DestinationSystemDataSource" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="DestinationSystemDataSource" runat="server"
                            SelectMethod="GetMappingDestinationSystems" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Type:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceType" runat="server" DataSourceID="SourceTypeDataSource"
                            DataTextField="SourceType" DataValueField="SourceTypeFullName" Width="400px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListSourceType_SelectedIndexChanged" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList><asp:ObjectDataSource ID="SourceTypeDataSource" runat="server"
                            SelectMethod="GetUniqueMappingSourceTypes" TypeName="Discovery.BusinessObjects.Controllers.MappingController">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Type:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListDestinationType" runat="server" Width="400px" AppendDataBoundItems="True" AutoPostBack="True"  DataTextField="DestinationType" DataValueField="Id" OnDataBound="DropDownListDestinationType_DataBound" OnSelectedIndexChanged="DropDownListDestinationType_SelectedIndexChanged">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Property:</td>
                    <td style="width: 254px">
                        <asp:DropDownList ID="DropDownListSourceProperty" runat="server" Width="400px" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="SourceProperty" DataValueField="SourceProperty" OnDataBound="DropDownListSourceProperty_DataBound" OnSelectedIndexChanged="DropDownListSourceProperty_SelectedIndexChanged" >
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Property:</td>
                    <td style="width: 254px">
                        <asp:DropDownList AutoPostBack="true" ID="DropDownListDestinationProperty" runat="server" 
                            DataTextField="DestinationProperty" DataValueField="Id" 
                            Width="400px" OnDataBound="DropDownListDestinationProperty_DataBound" OnSelectedIndexChanged="DropDownListDestinationProperty_SelectedIndexChanged">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        From Value:</td>
                    <td style="width: 254px">
                        <asp:TextBox ID="TextBoxSourceValue" runat="server" Text='<%# Bind("SourceValue") %>'
                            Width="276px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 135px; height: 48px;">
                        To Value:</td>
                    <td style="width: 254px; height: 48px;">
                        <asp:TextBox ID="TextBoxDestinationValue" runat="server" Visible="False" Width="276px"></asp:TextBox><asp:DropDownList ID="DropDownListDestinationValue"
                                runat="server" Visible="False" Width="400px" AppendDataBoundItems="True">
                            <asp:ListItem Value="-1">(Please Select)</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value="-1" />
            <asp:HiddenField ID="CheckSum" runat="server" Value="0" />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>'  />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td style="width: 135px">
                        Source System:</td>
                    <td style="width: 254px">
            <asp:Label ID="SourceSystemLabel" runat="server" Text='<%# Eval("SourceSystem.Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination System:</td>
                    <td style="width: 254px">
            <asp:Label ID="DestinationSystemLabel" runat="server" Text='<%# Eval("DestinationSystem.Name") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Type:</td>
                    <td style="width: 254px">
                     
                    
            <asp:Label ID="SourceTypeLabel" runat="server" Text='<%# Eval("MappingPropertyAssociation.MappingClassAssociation.SourceType") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Type:</td>
                    <td style="width: 254px">
            <asp:Label ID="DestinationTypeLabel" runat="server" Text='<%# Eval("MappingPropertyAssociation.MappingClassAssociation.DestinationType") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Source Property:</td>
                    <td style="width: 254px">
            <asp:Label ID="SourcePropertyLabel" runat="server" Text='<%# Eval("MappingPropertyAssociation.SourceProperty") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        Destination Property:</td>
                    <td style="width: 254px">
            <asp:Label ID="DestinationPropertyLabel" runat="server" Text='<%# Eval("MappingPropertyAssociation.DestinationProperty") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        From Value:</td>
                    <td style="width: 254px">
            <asp:Label ID="SourceValueLabel" runat="server" Text='<%# Eval("SourceValue") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 135px">
                        To Value:</td>
                    <td style="width: 254px">
            <asp:Label ID="DestinationValueLabel" runat="server" Text='<%# Eval("DestinationValue") %>'></asp:Label></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="MappingDataSource" runat="server" DataObjectTypeName="Discovery.BusinessObjects.Mapping"
        DeleteMethod="DeleteMapping" InsertMethod="SaveMapping" SelectMethod="GetMapping"
        TypeName="Discovery.BusinessObjects.Controllers.MappingController" UpdateMethod="SaveMapping">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="mappingId" QueryStringField="Id"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <br />
    <br />
</asp:Content>

