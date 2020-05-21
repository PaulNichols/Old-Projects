<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Task.aspx.cs"
    Inherits="Discovery.UI.Web.Admin.Task" Title="Task Details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div class="PageTitle">
        Task Details</div>
    <hr />
    <br />
    <asp:HyperLink ID="HyperLinkBack"  SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />
    <asp:FormView ID="TaskFormView" runat="server" DataSourceID="TaskDataSource" DataKeyNames="Id"
         OnItemInserting="TaskFormView_ItemInserting"
        OnItemUpdating="TaskFormView_ItemUpdating" OnDataBound="TaskFormView_DataBound">
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdateCancel" runat="server" CausesValidation="False"
                CommandName="Cancel" SkinID="ButtonCancel" />
            <asp:Wizard Width="500px" Height="300px" StepStyle-VerticalAlign="Top" ID="TaskWizard"
                runat="server" ActiveStepIndex="0" OnActiveStepChanged="TaskWizard_ActiveStepChanged">
                <FinishNavigationTemplate>
                    <table>
                        <tr style="">
                            <td>
                                <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                    Text="Previous" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:Button ID="FinishButton" runat="server" CommandName="Update" Text="Finish" BackColor="#FFFBFF"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" />
                            </td>
                        </tr>
                    </table>
                </FinishNavigationTemplate>
                <StepStyle VerticalAlign="Top" />
                <WizardSteps>
                    <asp:WizardStep runat="server" StepType="Start" Title="General">
                        <table>
                            <tr>
                                <td>
                                    Name:</td>
                                <td>
                                    <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName"
                                        ErrorMessage="Name is required." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Frequency (Seconds):</td>
                                <td>
                                    <asp:TextBox ID="TextBoxFrequency" runat="server" Text='<%# Bind("Frequency") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxFrequency"
                                        ErrorMessage="Frequency is required." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxFrequency"
                                        ErrorMessage="Frequency must be a number" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Integer">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Source Connection">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Connection Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListConnectionType" runat="server" Width="207px" AutoPostBack="True"
                                        SelectedValue='<%# Convert.ToInt32(Eval("SourceConnection.ConnectionType")) %>'
                                        OnSelectedIndexChanged="DropDownListConnectionType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Shipment</asp:ListItem>
                                        <asp:ListItem Value="1">Optrak</asp:ListItem>
                                        <asp:ListItem Value="2">Commander</asp:ListItem>
                                        <asp:ListItem Value="3">Management Server</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Channel Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListChannelType" runat="server" Width="207px" AutoPostBack="True"
                                        SelectedValue='<%# Convert.ToInt32(Eval("SourceConnection.ChannelType")) %>'
                                        OnSelectedIndexChanged="DropDownListChannelType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">FTP</asp:ListItem>
                                        <asp:ListItem Value="1">MSMQ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Connection:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListConnection" runat="server"  Width="207px" DataTextField="Name"
                                        DataValueField="Id" DataSourceID="ConnectionObjectDataSource" OnDataBound="DropDownListConnection_DataBound">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ConnectionObjectDataSource" runat="server" SelectMethod="GetConnections"
                                        TypeName="Discovery.Integration.IntegrationController">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DropDownListConnectionType" Name="connectionType"
                                                PropertyName="SelectedValue" Type="Object" />
                                            <asp:ControlParameter ControlID="DropDownListChannelType" Name="channelType" PropertyName="SelectedValue"
                                                Type="Object" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Destination Connection">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Connection Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationConnectionType" runat="server" Width="207px"
                                        AutoPostBack="True" SelectedValue='<%# Convert.ToInt32(Eval("DestinationConnection.ConnectionType")) %>'
                                        OnSelectedIndexChanged="DropDownListDestinationConnectionType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Shipment</asp:ListItem>
                                        <asp:ListItem Value="1">Optrak</asp:ListItem>
                                        <asp:ListItem Value="2">Commander</asp:ListItem>
                                        <asp:ListItem Value="3">Management Server</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Channel Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationChannelType" runat="server" Width="207px"
                                        AutoPostBack="True" SelectedValue='<%# Convert.ToInt32(Eval("DestinationConnection.ChannelType")) %>'
                                        OnSelectedIndexChanged="DropDownListDestinationChannelType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">FTP</asp:ListItem>
                                        <asp:ListItem Value="1">MSMQ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Connection:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationConnection"  DataTextField="Name" DataValueField="Id"
                                        DataSourceID="DestinationConnectionObjectDataSource" runat="server" Width="207px" OnDataBound="DropDownListDestinationConnection_DataBound">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="DestinationConnectionObjectDataSource" runat="server" SelectMethod="GetConnections"
                                        TypeName="Discovery.Integration.IntegrationController">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DropDownListDestinationConnectionType" Name="connectionType"
                                                PropertyName="SelectedValue" Type="Object" />
                                            <asp:ControlParameter ControlID="DropDownListDestinationChannelType" Name="channelType"
                                                PropertyName="SelectedValue" Type="Object" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Source Settings">
                        <b>Source Connection -</b><br />
                        <b>Name: </b>
                        <asp:Literal ID="LiteralSourceName" runat="server"></asp:Literal>
                        <br />
                        <b>Type: </b>
                        <asp:Literal ID="LiteralSourceType" runat="server"></asp:Literal>
                        <br />
                        <b>Channel: </b>
                        <asp:Literal ID="LiteralSourceChannel" runat="server"></asp:Literal>
                        <br />
                        <hr />
                        <br />
                        <table id="Source_Settings" runat="server">
                            <tr id="RowSourceSettingsNa" runat="server">
                                <td id="Td48" colspan="2" runat="server">
                                    No Source Settings are applicable</td>
                            </tr>
                            <tr id="RowSourceWarehouse" runat="server">
                                <td style="width: 100px" runat="server">
                                    Warehouse:</td>
                                <td runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierWarehouse"   OnDataBound="DropDownListSourceIdentifier_DataBound"
                                        DataSourceID="WarehouseObjectDataSource" DataTextField="CodeAndDescription" DataValueField="Code"
                                        runat="server" Width="189px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                                <tr id="RowSourceRegion" runat="server">
                                <td id="Td19" style="width: 100px" runat="server">
                                    Region:</td>
                                <td id="Td20" runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierRegion" 
                                        DataSourceID="RegionObjectDataSource" DataTextField="CodeAndDescription" DataValueField="Code" OnDataBound="DropDownListSourceIdentifier_DataBound"
                                        runat="server" Width="189px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="RowSourceOpco" runat="server">
                                <td style="width: 100px" runat="server">
                                    Opco:</td>
                                <td runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierOpco" runat="server" DataSourceID="OpcoObjectDataSource" OnDataBound="DropDownListSourceIdentifier_DataBound"
                                        DataTextField="Description" DataValueField="Code" Width="203px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="OpcoObjectDataSource" runat="server" SelectMethod="GetOpCos"
                                        TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowSourceDirectory" runat="server">
                                <td style="width: 100px" runat="server">
                                    Source Directory:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxSourceDirectory" runat="server" MaxLength="256" Text='<%# Bind("SourceDirectory") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="RowDataFileExtension" runat="server">
                                <td style="width: 100px" runat="server">
                                    Data File Extension:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxDataFileExtension" runat="server" MaxLength="5" Text='<%# Bind("DataFileExtension") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDataFileExtension"
                                        ErrorMessage="You must enter a data file extension" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxDataFileExtension"
                                        ErrorMessage="File extensions do not need to start with '.'" SetFocusOnError="True"
                                        ValidationExpression="^[a-zA-Z]*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="RowFlagFileExtension" runat="server">
                                <td style="width: 100px" runat="server">
                                    Flag File Extension:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxFlagFileExtension" runat="server" MaxLength="5" Text='<%# Bind("FlagFileExtension") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxFlagFileExtension"
                                        ErrorMessage="File extensions do not need to start with '.'" SetFocusOnError="True"
                                        ValidationExpression="^[a-zA-Z]*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="RowRemoveDataFile" runat="server">
                                <td style="width: 100px" runat="server">
                                    Remove Data File:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxRemoveDataFile" runat="server" Checked='<%# Bind("RemoveDataFile") %>' />
                                </td>
                            </tr>
                            <tr id="RowRemoveFlagFile" runat="server">
                                <td style="width: 100px" runat="server">
                                    Remove Flag File:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxRemoveFlagFile" runat="server" Checked='<%# Bind("RemoveFlagFile") %>' />
                                </td>
                            </tr>
                            <tr id="RowMonitorSequenceNumber" runat="server">
                                <td style="width: 100px" runat="server">
                                    Monitor Sequence Number:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxMonitorSequenceNumber" runat="server" Checked='<%# Bind("MonitorSequenceNumber") %>' />
                                </td>
                            </tr>
                            <tr id="RowCurrentSequenceNumber" runat="server">
                                <td runat="server">
                                    Current Sequence Number:</td>
                                <td runat="server">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td rowspan="2">
                                                <asp:TextBox ID="TextBoxSequenceNumber" runat="server" Text='<%# Bind("SequenceNumber") %>'></asp:TextBox>
                                            </td>
                                          
                                        </tr>
                                      
                                    </table>
                                    <br />
                              </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" StepType="Finish" Title="Destination Settings">
                        <b>Destination Connection -</b><br />
                        <b>Name: </b>
                        <asp:Literal ID="LiteralDestinationName" runat="server"></asp:Literal>
                        <br />
                        <b>Type: </b>
                        <asp:Literal ID="LiteralDestinationType" runat="server"></asp:Literal>
                        <br />
                        <b>Channel: </b>
                        <asp:Literal ID="LiteralDestinationChannel" runat="server"></asp:Literal>
                        <br />
                        <hr />
                        <br />
                        <table>
                            <tr id="RowNA" runat="server">
                                <td colspan="2" runat="server">
                                    No Destination Settings are applicable</td>
                            </tr>
                            <tr id="RowDestinationWarehouse" runat="server">
                                <td id="Td42" style="width: 100px" runat="server">
                                    Warehouse:</td>
                                <td id="Td43" style="width: 127px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierWarehouse" DataSourceID="WarehouseObjectDataSource" OnDataBound="DropDownListDestinationIdentifier_DataBound"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server" SelectMethod="GetWarehouses"
                                        TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                             <tr id="RowDestinationRegion" runat="server">
                                <td id="Td25" style="width: 100px" runat="server">
                                    Region:</td>
                                <td id="Td26" style="width: 127px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierRegion" DataSourceID="RegionObjectDataSource" OnDataBound="DropDownListDestinationIdentifier_DataBound"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSourceRegion" runat="server" SelectMethod="GetRegionss"
                                        TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowDestinationOpCo" runat="server">
                                <td id="Td46" style="width: 100px" runat="server">
                                    Opco:</td>
                                <td id="Td47" style="width: 100px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierOpco" runat="server" DataSourceID="OPcoObjectDataSource2" OnDataBound="DropDownListDestinationIdentifier_DataBound"
                                        DataTextField="Description" DataValueField="Code" Width="203px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="OPcoObjectDataSource2" runat="server" SelectMethod="GetOpCos"
                                        TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowDestinationDirectory" runat="server">
                                <td style="width: 100px" runat="server">
                                    Directory:</td>
                                <td style="width: 100px" runat="server">
                                    <asp:TextBox ID="TextBoxDestinationDirectory" runat="server" MaxLength="256" Text='<%# Bind("DestinationDirectory") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='<%# Bind("CheckSum") %>' />
            <asp:HiddenField ID="IsArchived" runat="server" Value='<%# Bind("IsArchived") %>' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </EditItemTemplate>
        <InsertItemTemplate>
            <asp:ImageButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                OnClick="InsertButton_Click" SkinID="ButtonInsert" /><asp:ImageButton ID="InsertCancelButton"
                    runat="server" CausesValidation="False" CommandName="Cancel" OnClick="InsertCancelButton_Click"
                    SkinID="ButtonCancel" />&nbsp;
            <br />
            <asp:Wizard Width="500px" Height="300px" StepStyle-VerticalAlign="Top" ID="TaskWizard"
                runat="server" ActiveStepIndex="0" OnActiveStepChanged="TaskWizard_ActiveStepChanged">
                <StepStyle VerticalAlign="Top" />
                <WizardSteps>
                    <asp:WizardStep runat="server" StepType="Start" Title="General">
                        <table>
                            <tr>
                                <td>
                                    Name:</td>
                                <td>
                                    <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxName"
                                        ErrorMessage="Name is required." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Frequency (Seconds):</td>
                                <td>
                                    <asp:TextBox ID="TextBoxFrequency" runat="server" Text='<%# Bind("Frequency") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxFrequency"
                                        ErrorMessage="Frequency is required." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxFrequency"
                                        ErrorMessage="Frequency must be a number" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Integer">*</asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Source Connection">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Connection Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListConnectionType" runat="server" Width="207px" AutoPostBack="True"
                                        SelectedValue='<%# Convert.ToInt32(Eval("SourceConnection.ConnectionType")) %>'
                                        OnSelectedIndexChanged="DropDownListConnectionType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Shipment</asp:ListItem>
                                        <asp:ListItem Value="1">Optrak</asp:ListItem>
                                        <asp:ListItem Value="2">Commander</asp:ListItem>
                                        <asp:ListItem Value="3">Management Server</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Channel Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListChannelType" runat="server" Width="207px" AutoPostBack="True"
                                        SelectedValue='<%# Convert.ToInt32(Eval("SourceConnection.ChannelType")) %>'
                                        OnSelectedIndexChanged="DropDownListChannelType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">FTP</asp:ListItem>
                                        <asp:ListItem Value="1">MSMQ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Connection:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListConnection" runat="server" Width="207px" DataTextField="Name"
                                        DataValueField="Id" DataSourceID="ConnectionObjectDataSource">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ConnectionObjectDataSource" runat="server" SelectMethod="GetConnections"
                                        TypeName="Discovery.Integration.IntegrationController">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DropDownListConnectionType" Name="connectionType"
                                                PropertyName="SelectedValue" Type="Object" />
                                            <asp:ControlParameter ControlID="DropDownListChannelType" Name="channelType" PropertyName="SelectedValue"
                                                Type="Object" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Destination Connection">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Connection Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationConnectionType" runat="server" Width="207px"
                                        AutoPostBack="True" SelectedValue='<%# Convert.ToInt32(Eval("DestinationConnection.ConnectionType")) %>'
                                        OnSelectedIndexChanged="DropDownListDestinationConnectionType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Shipment</asp:ListItem>
                                        <asp:ListItem Value="1">Optrak</asp:ListItem>
                                        <asp:ListItem Value="2">Commander</asp:ListItem>
                                        <asp:ListItem Value="3">Management Server</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Channel Type:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationChannelType" runat="server" Width="207px"
                                        AutoPostBack="True" SelectedValue='<%# Convert.ToInt32(Eval("DestinationConnection.ChannelType")) %>'
                                        OnSelectedIndexChanged="DropDownListDestinationChannelType_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">FTP</asp:ListItem>
                                        <asp:ListItem Value="1">MSMQ</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Connection:</td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DropDownListDestinationConnection" DataTextField="Name" DataValueField="Id"
                                        DataSourceID="DestinationConnectionObjectDataSource" runat="server" Width="207px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="DestinationConnectionObjectDataSource" runat="server" SelectMethod="GetConnections"
                                        TypeName="Discovery.Integration.IntegrationController">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DropDownListDestinationConnectionType" Name="connectionType"
                                                PropertyName="SelectedValue" Type="Object" />
                                            <asp:ControlParameter ControlID="DropDownListDestinationChannelType" Name="channelType"
                                                PropertyName="SelectedValue" Type="Object" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                  <asp:WizardStep ID="WizardStep1"   runat="server" Title="Source Settings">
                           <b>Source Connection -</b><br />
                        <b>Name: </b>
                        <asp:Literal ID="LiteralSourceName" runat="server" ></asp:Literal>
                        <br />
                        <b>Type: </b>
                        <asp:Literal ID="LiteralSourceType" runat="server" ></asp:Literal>
                        <br />
                        <b>Channel: </b>
                        <asp:Literal ID="LiteralSourceChannel" runat="server" ></asp:Literal>
                        <br />
                        <hr />
                       
                        <br />
                        <table id="Source_Settings" runat="server">
                            <tr id="RowSourceSettingsNa" runat="server">
                                <td id="Td48" colspan="2" runat="server">
                                    No Source Settings are applicable</td>
                            </tr>
                            <tr id="RowSourceWarehouse" runat="server">
                                <td style="width: 100px" runat="server">
                                    Warehouse:</td>
                                <td runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierWarehouse" DataSourceID="WarehouseObjectDataSource"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                               <tr id="RowSourceRegion" runat="server">
                                <td id="Td27" style="width: 100px" runat="server">
                                    Region:</td>
                                <td id="Td28" runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierRegion" DataSourceID="RegionObjectDataSource"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="RowSourceOpco" runat="server">
                                <td style="width: 100px" runat="server">
                                    Opco:</td>
                                <td runat="server">
                                    <asp:DropDownList ID="DropDownListSourceIdentifierOpco" runat="server" DataSourceID="OpcoObjectDataSource"
                                        DataTextField="Description" DataValueField="Code" Width="203px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="OpcoObjectDataSource" runat="server" SelectMethod="GetOpCos"
                                        TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowSourceDirectory" runat="server">
                                <td style="width: 100px" runat="server">
                                    Source Directory:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxSourceDirectory" runat="server" MaxLength="256" Text='<%# Bind("SourceDirectory") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="RowDataFileExtension" runat="server">
                                <td style="width: 100px" runat="server">
                                    Data File Extension:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxDataFileExtension" runat="server" MaxLength="5" Text='<%# Bind("DataFileExtension") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxDataFileExtension"
                                        ErrorMessage="You must enter a data file extension" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxDataFileExtension"
                                        ErrorMessage="File extensions do not need to start with '.'" SetFocusOnError="True"
                                        ValidationExpression="^[a-zA-Z]*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="RowFlagFileExtension" runat="server">
                                <td style="width: 100px" runat="server">
                                    Flag File Extension:</td>
                                <td runat="server">
                                    <asp:TextBox ID="TextBoxFlagFileExtension" runat="server" MaxLength="5" Text='<%# Bind("FlagFileExtension") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxFlagFileExtension"
                                        ErrorMessage="File extensions do not need to start with '.'" SetFocusOnError="True"
                                        ValidationExpression="^[a-zA-Z]*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="RowRemoveDataFile" runat="server">
                                <td style="width: 100px" runat="server">
                                    Remove Data File:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxRemoveDataFile" runat="server" Checked='<%# Bind("RemoveDataFile") %>' />
                                </td>
                            </tr>
                            <tr id="RowRemoveFlagFile" runat="server">
                                <td style="width: 100px" runat="server">
                                    Remove Flag File:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxRemoveFlagFile" runat="server" Checked='<%# Bind("RemoveFlagFile") %>' />
                                </td>
                            </tr>
                            <tr id="RowMonitorSequenceNumber" runat="server">
                                <td style="width: 100px" runat="server">
                                    Monitor Sequence Number:</td>
                                <td runat="server">
                                    <asp:CheckBox ID="CheckBoxMonitorSequenceNumber" runat="server" Checked='<%# Bind("MonitorSequenceNumber") %>' />
                                </td>
                            </tr>
                            <tr id="RowCurrentSequenceNumber" runat="server">
                                <td runat="server">
                                    Current Sequence Number:</td>
                                <td runat="server">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td rowspan="2">
                                                <asp:TextBox ID="TextBoxSequenceNumber" runat="server" Text='<%# Bind("SequenceNumber") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ButtonUp" runat="server" Height="10px" ImageUrl="~/Images/up.gif"
                                                    Width="10px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:ImageButton ID="ButtonDown" runat="server" Height="10px" ImageUrl="~/Images/down.gif"
                                                    Width="10px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                  
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                   <asp:WizardStep ID="WizardStep2" runat="server" StepType="Finish" Title="Destination Settings">
                    
                           <b>Destination Connection -</b><br />
                        <b>Name: </b>
                        <asp:Literal ID="LiteralDestinationName" runat="server" ></asp:Literal>
                        <br />
                        <b>Type: </b>
                        <asp:Literal ID="LiteralDestinationType" runat="server" ></asp:Literal>
                        <br />
                        <b>Channel: </b>
                        <asp:Literal ID="LiteralDestinationChannel" runat="server" ></asp:Literal>
                        <br />
                        <hr />
                        <br />
                        <table>
                            <tr id="RowNA" runat="server">
                                <td colspan="2" runat="server">
                                    No Destination Settings are applicable</td>
                            </tr>
                            <tr id="RowDestinationWarehouse" runat="server">
                                <td id="Td42" style="width: 100px" runat="server">
                                    Warehouse:</td>
                                <td id="Td43" style="width: 127px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierWarehouse" DataSourceID="WarehouseObjectDataSource"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server" SelectMethod="GetWarehouses"
                                        TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                               <tr id="RowDestinationRegion" runat="server">
                                <td id="Td29" style="width: 100px" runat="server">
                                    Region:</td>
                                <td id="Td30" style="width: 127px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierRegion" DataSourceID="RegionObjectDataSource"
                                        DataTextField="CodeAndDescription" DataValueField="Code" runat="server" Width="189px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="RegionObjectDataSource" runat="server" SelectMethod="GetRegions"
                                        TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowDestinationOpCo" runat="server">
                                <td id="Td46" style="width: 100px" runat="server">
                                    Opco:</td>
                                <td id="Td47" style="width: 100px" runat="server">
                                    <asp:DropDownList ID="DropDownListDestinationIdentifierOpco" runat="server" DataSourceID="ObjectDataSource1"
                                        DataTextField="Description" DataValueField="Code" Width="203px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetOpCos"
                                        TypeName="Discovery.BusinessObjects.Controllers.OpcoController">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="false" Name="fullyPopulate" Type="Boolean" />
                                            <asp:Parameter DefaultValue="" Name="sortExpression" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr id="RowDestinationDirectory" runat="server">
                                <td style="width: 100px" runat="server">
                                    Directory:</td>
                                <td style="width: 100px" runat="server">
                                    <asp:TextBox ID="TextBoxDestinationDirectory" runat="server" MaxLength="256" Text='<%# Bind("DestinationDirectory") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                </WizardSteps>
                <FinishNavigationTemplate>
                    <table>
                        <tr style="">
                            <td>
                                <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                    Text="Previous" BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" ForeColor="Black" />
                            </td>
                            <td>
                                <asp:Button ID="FinishButton" runat="server" CommandName="Insert" Text="Finish" BackColor="#FFFBFF"
                                    BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" />
                            </td>
                        </tr>
                    </table>
                </FinishNavigationTemplate>
            </asp:Wizard>
            <asp:HiddenField ID="Id" runat="server" Value='-1' />
            <asp:HiddenField ID="CheckSum" runat="server" Value='0' />
            <asp:HiddenField ID="UpdatedBy" runat="server" Value='<%# Bind("UpdatedBy") %>' />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><asp:ImageButton
                ID="ButtonDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?');"
                SkinID="ButtonDelete" /><br />
            <table>
                <tr>
                    <td>
                        Name:</td>
                    <td>
                        <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td21" runat="server">
                        Source Connection:</td>
                    <td id="Td22" runat="server">
                        <asp:Label ID="SourceConnectionName" runat="server" Text='<%# Eval("SourceConnection.Name") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="Tr4" runat="server">
                    <td id="Td23" runat="server">
                        Destination Connection:</td>
                    <td id="Td24" runat="server">
                        <asp:Label ID="LabelDestinationConnectionName" runat="server" Text='<%# Eval("DestinationConnection.Name") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowSourceOpco" runat="server">
                    <td id="Td51" runat="server">
                        OpCo:</td>
                    <td id="Td52" style="width: 127px" runat="server">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("SourceConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowDestinationOpCo" runat="server">
                    <td id="Td53" runat="server">
                        OpCo:</td>
                    <td id="Td54" style="width: 127px" runat="server">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("DestinationConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Frequency (Seconds):</td>
                    <td>
                        <asp:Label ID="LabelFrequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowSourceDirectory" runat="server">
                    <td id="Td1" runat="server">
                        Source Directory:</td>
                    <td id="Td2" runat="server">
                        <asp:Label ID="LabelSourceDirectory" runat="server" Text='<%# Eval("SourceDirectory") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowDataFileExtension" runat="server">
                    <td id="Td3" runat="server">
                        Data File Extension:</td>
                    <td id="Td4" runat="server">
                        <asp:Label ID="LabelDataFileExtension" runat="server" Text='<%# Eval("DataFileExtension") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowFlagFileExtension" runat="server">
                    <td id="Td5" runat="server">
                        Flag File Extension:</td>
                    <td id="Td6" runat="server">
                        <asp:Label ID="LabelFlagFileExtension" runat="server" Text='<%# Eval("FlagFileExtension") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowRemoveDataFile" runat="server">
                    <td id="Td7" runat="server">
                        Remove Data File:</td>
                    <td id="Td8" runat="server">
                        <asp:Image ID="ImageRemoveDataFile" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("RemoveDataFile")) %>' />
                        <asp:Image ID="ImageNotRemoveDataFile" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("RemoveDataFile")) %>' />
                    </td>
                </tr>
                <tr id="RowRemoveFlagFile" runat="server">
                    <td id="Td9" runat="server">
                        Remove Flag File:</td>
                    <td id="Td10" runat="server">
                        <asp:Image ID="ImageRemoveFlagFile" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("RemoveFlagFile")) %>' />
                        <asp:Image ID="ImageNotRemoveFlagFile" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("RemoveFlagFile")) %>' />
                    </td>
                </tr>
                <tr id="RowMonitorSequenceNumber" runat="server">
                    <td id="Td11" runat="server">
                        Monitor Sequence Number:</td>
                    <td id="Td12" runat="server">
                        <asp:Image ID="ImageMonitorSequenceNumber" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("MonitorSequenceNumber")) %>' />
                        <asp:Image ID="ImageNotMonitorSequenceNumber" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("MonitorSequenceNumber")) %>' />
                    </td>
                </tr>
                <tr id="RowCurrentSequenceNumber" runat="server">
                    <td id="Td13" runat="server">
                        Current Sequence Number:</td>
                    <td id="Td14" runat="server">
                        <asp:Label ID="LabelSequenceNumber" runat="server" Text='<%# Eval("SequenceNumber") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowSourceWarehouse" runat="server">
                    <td id="Td15" runat="server">
                        Warehouse:</td>
                    <td id="Td16" style="width: 127px" runat="server">
                        <asp:Label ID="LabelIdentifier" runat="server" Text='<%# Eval("SourceConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowDestinationWarehouse" runat="server">
                    <td id="Td49" runat="server">
                        Warehouse:</td>
                    <td id="Td50" style="width: 127px" runat="server">
                        <asp:Label ID="LabelDestinationConnectionIdentifier" runat="server" Text='<%# Eval("DestinationConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                    <tr id="RowSourceRegion" runat="server">
                    <td id="Td31" runat="server">
                        Region:</td>
                    <td id="Td32" style="width: 127px" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("SourceConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowDestinationRegion" runat="server">
                    <td id="Td33" runat="server">
                        Region:</td>
                    <td id="Td34" style="width: 127px" runat="server">
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("DestinationConnectionIdentifier") %>'></asp:Label>
                    </td>
                </tr>
                <tr id="RowDestinationDirectory" runat="server">
                    <td id="Td17" runat="server">
                        Destination Directory:</td>
                    <td id="Td18" runat="server">
                        <asp:Label ID="LabelDestinationDirectory" runat="server" Text='<%# Eval("DestinationDirectory") %>'></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
        </ItemTemplate>
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="WarehouseObjectDataSource" runat="server" SelectMethod="GetWarehouses"
        TypeName="Discovery.BusinessObjects.Controllers.WarehouseController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="RegionObjectDataSource" runat="server" SelectMethod="GetRegions"
        TypeName="Discovery.BusinessObjects.Controllers.OptrakRegionController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Code,Description" Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TaskDataSource" runat="server" DataObjectTypeName="Discovery.Integration.Task"
        SelectMethod="GetTask" TypeName="Discovery.Integration.IntegrationController"
        InsertMethod="SaveTask" DeleteMethod="DeleteTask" UpdateMethod="SaveTask" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="taskId" QueryStringField="Id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
