<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Page</title>
    
    <style type="text/css">
    
    .shipmentHeaderNormal
    {
	    height: 30px;
	    background-image: url(Accordion/Header.gif);
	    background-repeat: repeat-x;
	    cursor: pointer;
	    vertical-align: middle;
	    padding-left: 6px;
    }
    
    .shipmentContent
    {
	    background-color: white;
	    background-position-y: bottom;
	    background-image: url(Container/Footer.gif);
	    border-right: dimgray 0px solid;
	    border-top: dimgray 0px solid;
	    border-left: dimgray 0px solid;
	    border-bottom: dimgray 0px solid;
	    padding-left: 0px;
	    background-repeat: repeat-x;
	    overflow:hidden;
	    height:0px;
    }
    
    .accordionHeader
    {
        border: 1px solid #2F4F4F;
        color: white;
        background-color: #2E4d7B;
	    font-family: Arial, Sans-Serif;
	    font-size: 12px;
	    font-weight: bold;
        padding: 5px;
        margin-top: 5px;
        cursor: pointer;
    }

    .accordionContent
    {
        background-color: #D3DEEF;
        border: 1px dashed #2F4F4F;
        border-top: none;
        padding: 5px;
        padding-top: 10px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server">
        </asp:ScriptManager>
        
        <asp:FormView ID="FormView1" runat="server" DataSourceID="ObjectDataSource1">
            <EditItemTemplate>
            
            This is the edit template....
            
            
            
            
            
            
            
            
            
            <asp:Panel CssClass="shipmentHeaderNormal" ID="panelHeader1" runat="server">
            Page 1
            </asp:Panel>
            <asp:Panel ID="panelContent1" runat="server" CssClass="shipmentContent">
                                CheckSum:
                                <asp:TextBox ID="CheckSumTextBox" runat="server" Text='<%# Bind("CheckSum") %>'>
                                </asp:TextBox><br />
                                Code:
                                <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>'>
                                </asp:TextBox><br />
                                Id:
                                <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>'>
                                </asp:TextBox><br />
                                FtpUserName:
                                <asp:TextBox ID="FtpUserNameTextBox" runat="server" Text='<%# Bind("FtpUserName") %>'>
                                </asp:TextBox><br />
                                IsArchived:
                                <asp:CheckBox ID="IsArchivedCheckBox" runat="server" Checked='<%# Bind("IsArchived") %>' /><br />
            </asp:Panel>
            <asp:Panel CssClass="shipmentHeaderNormal" ID="panelHeader2" runat="server">
            Page 2
            </asp:Panel>
            <asp:Panel ID="panelContent2" runat="server" CssClass="shipmentContent">

                                UpdatedBy:
                                <asp:TextBox ID="UpdatedByTextBox" runat="server" Text='<%# Bind("UpdatedBy") %>'>
                                </asp:TextBox><br />
                                Description:
                                <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'>
                                </asp:TextBox><br />
                                FtpPassword:
                                <asp:TextBox ID="FtpPasswordTextBox" runat="server" Text='<%# Bind("FtpPassword") %>'>
                                </asp:TextBox><br />
                                FtpIP:
                                <asp:TextBox ID="FtpIPTextBox" runat="server" Text='<%# Bind("FtpIP") %>'>
                                </asp:TextBox><br />
            </asp:Panel>
            <%-- Collapsible Panel Start --%>
            <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelExtenderShipment1" runat="Server"
                TargetControlID="panelContent1" CollapsedSize="0" Collapsed="True" ExpandControlID="panelHeader1"
                CollapseControlID="panelHeader1" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                ExpandDirection="Vertical" />
            <ajaxToolkit:CollapsiblePanelExtender ID="collapsiblePanelExtenderShipment2" runat="Server"
                TargetControlID="panelContent2" CollapsedSize="0" Collapsed="True" ExpandControlID="panelHeader2"
                CollapseControlID="panelHeader2" AutoCollapse="False" AutoExpand="False" ScrollContents="False"
                ExpandDirection="Vertical" />
            <%-- Collapsible Panel End --%>

                                
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                    Text="Update">
                </asp:LinkButton>
                <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel">
                </asp:LinkButton>
            </EditItemTemplate>
            
            <InsertItemTemplate>
                CheckSum:
                <asp:TextBox ID="CheckSumTextBox" runat="server" Text='<%# Bind("CheckSum") %>'>
                </asp:TextBox><br />
                Code:
                <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>'>
                </asp:TextBox><br />
                UpdatedDate:
                <asp:TextBox ID="UpdatedDateTextBox" runat="server" Text='<%# Bind("UpdatedDate") %>'>
                </asp:TextBox><br />
                CanUpdate:
                <asp:CheckBox ID="CanUpdateCheckBox" runat="server" Checked='<%# Bind("CanUpdate") %>' /><br />
                Validator:
                <asp:TextBox ID="ValidatorTextBox" runat="server" Text='<%# Bind("Validator") %>'>
                </asp:TextBox><br />
                Id:
                <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>'>
                </asp:TextBox><br />
                IsValid:
                <asp:CheckBox ID="IsValidCheckBox" runat="server" Checked='<%# Bind("IsValid") %>' /><br />
                ValidationMessages:
                <asp:TextBox ID="ValidationMessagesTextBox" runat="server" Text='<%# Bind("ValidationMessages") %>'>
                </asp:TextBox><br />
                FtpUserName:
                <asp:TextBox ID="FtpUserNameTextBox" runat="server" Text='<%# Bind("FtpUserName") %>'>
                </asp:TextBox><br />
                IsArchived:
                <asp:CheckBox ID="IsArchivedCheckBox" runat="server" Checked='<%# Bind("IsArchived") %>' /><br />
                UpdatedBy:
                <asp:TextBox ID="UpdatedByTextBox" runat="server" Text='<%# Bind("UpdatedBy") %>'>
                </asp:TextBox><br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'>
                </asp:TextBox><br />
                FtpPassword:
                <asp:TextBox ID="FtpPasswordTextBox" runat="server" Text='<%# Bind("FtpPassword") %>'>
                </asp:TextBox><br />
                FtpIP:
                <asp:TextBox ID="FtpIPTextBox" runat="server" Text='<%# Bind("FtpIP") %>'>
                </asp:TextBox><br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                    Text="Insert">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel">
                </asp:LinkButton>
            </InsertItemTemplate>
            <ItemTemplate>
                CheckSum:
                <asp:Label ID="CheckSumLabel" runat="server" Text='<%# Bind("CheckSum") %>'></asp:Label><br />
                Code:
                <asp:Label ID="CodeLabel" runat="server" Text='<%# Bind("Code") %>'></asp:Label><br />
                UpdatedDate:
                <asp:Label ID="UpdatedDateLabel" runat="server" Text='<%# Bind("UpdatedDate") %>'>
                </asp:Label><br />
                CanUpdate:
                <asp:CheckBox ID="CanUpdateCheckBox" runat="server" Checked='<%# Bind("CanUpdate") %>'
                    Enabled="false" /><br />
                Validator:
                <asp:Label ID="ValidatorLabel" runat="server" Text='<%# Bind("Validator") %>'></asp:Label><br />
                Id:
                <asp:Label ID="IdLabel" runat="server" Text='<%# Bind("Id") %>'></asp:Label><br />
                IsValid:
                <asp:CheckBox ID="IsValidCheckBox" runat="server" Checked='<%# Bind("IsValid") %>'
                    Enabled="false" /><br />
                ValidationMessages:
                <asp:Label ID="ValidationMessagesLabel" runat="server" Text='<%# Bind("ValidationMessages") %>'>
                </asp:Label><br />
                FtpUserName:
                <asp:Label ID="FtpUserNameLabel" runat="server" Text='<%# Bind("FtpUserName") %>'>
                </asp:Label><br />
                IsArchived:
                <asp:CheckBox ID="IsArchivedCheckBox" runat="server" Checked='<%# Bind("IsArchived") %>'
                    Enabled="false" /><br />
                UpdatedBy:
                <asp:Label ID="UpdatedByLabel" runat="server" Text='<%# Bind("UpdatedBy") %>'></asp:Label><br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>'>
                </asp:Label><br />
                FtpPassword:
                <asp:Label ID="FtpPasswordLabel" runat="server" Text='<%# Bind("FtpPassword") %>'>
                </asp:Label><br />
                FtpIP:
                <asp:Label ID="FtpIPLabel" runat="server" Text='<%# Bind("FtpIP") %>'></asp:Label><br />
                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                    Text="Edit">
                </asp:LinkButton>
                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="Delete">
                </asp:LinkButton>
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                    Text="New">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:FormView>
        <br />
        <%-- Status Legend Start --%>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Discovery.BusinessObjects.OpCo"
            DeleteMethod="DeleteOpCo" InsertMethod="SaveOpCo" SelectMethod="GetOpCo" TypeName="Discovery.BusinessObjects.Controllers.OpcoController"
            UpdateMethod="SaveOpCo">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="3" Name="opcoId" QueryStringField="Id" Type="Int32" />
                <asp:Parameter Name="fullyPopulate" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;<%-- Status Legend End --%>
    </form>
</body>
</html>
