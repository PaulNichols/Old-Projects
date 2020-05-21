<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ErrorType.aspx.cs" Inherits="Admin_Logging_ErrorType" Title="Management Server - Error Handling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">


<%@ Import namespace="System.Web.UI.WebControls"%>
<%@ Import namespace="System.Web.UI"%>


   <div class="PageTitle">
        Error Type</div>
        <hr />
        <br />
    <asp:HyperLink ID="HyperLinkBack" SkinID="HyperLinkBack"  runat="server">Back</asp:HyperLink><br />

    <asp:FormView ID="ErrorFormView" runat="server" DataSourceID="ErrorDataSource"
        DataKeyNames="Id" OnDataBound="ErrorFormView_DataBound"  >
        <EditItemTemplate>
            <asp:ImageButton ID="ButtonUpdate" runat="server" CausesValidation="True" CommandName="Update"
                OnClick="ButtonUpdate_Click" SkinID="ButtonSave" /><asp:ImageButton ID="ButtonUpdateCancel"
                    runat="server" CausesValidation="False" CommandName="Cancel" SkinID="ButtonCancel" /><br /><table>
                        <tr>
                            <td style="width: 100px">
                                Category:</td>
                            <td >
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Policy") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                OpCo:</td>
                            <td >
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("OpCoCode") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                Description:</td>
                            <td >
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ExceptionDescription") %>'></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                Priority:</td>
                            <td >
                                <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Priority") %>'
                                    Width="201px" DataSourceID="ObjectDataSourcePriorities" DataTextField="Value" DataValueField="Key">
                                </asp:DropDownList><asp:ObjectDataSource ID="ObjectDataSourcePriorities" runat="server"
                                    SelectMethod="GetPriorities" TypeName="Discovery.BusinessObjects.Controllers.ErrorTypeController">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                Requires Acknowledgement?:</td>
                            <td >
                                <asp:CheckBox ID="CheckRequiresAcknowledgement" runat="server" Checked='<%# Bind("RequiresAcknowledgement") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="LabelEmailOperatorText" runat="server">Email the Operator:</asp:Label></td>
                            <td >
                                <asp:CheckBox ID="CheckBoxEmailOperator" runat="server" Checked='<%# Bind("EmailOperator") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="LabelEmailRecipientsText" runat="server">Email Recipients:</asp:Label></td>
                            <td >
                                <asp:TextBox ID="TextBoxEmailRecipients" runat="server" Width="300px" Height="65px" Text='<%# Bind("EmailRecipients") %>'
                                    TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                                 <asp:Label ID="LabelEmailSubjectText" runat="server">Email Subject:</asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBoxEmailSubject" runat="server" Text='<%# Bind("EmailSubject") %>'
                                    Width="299px"></asp:TextBox></td>
                        </tr>
               
            </table>
              <ajaxToolkit:ToggleButtonExtender Enabled="true" ID="ToggleButtonExtender1" runat="server" TargetControlID="CheckRequiresAcknowledgement"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
            <ajaxToolkit:ToggleButtonExtender Enabled="true" ID="ToggleButtonExtender2" runat="server" TargetControlID="CheckBoxEmailOperator"
                CheckedImageUrl="~/images/check.gif" UncheckedImageUrl="~/images/uncheck.gif"
                ImageHeight="16" ImageWidth="16" />
                  <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ExceptionType") %>' />
            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("OpCoId") %>' />
            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Bind("CheckSum") %>' />
        </EditItemTemplate>
   
        <ItemTemplate>
            <asp:ImageButton ID="ButtonEdit" runat="server" CommandName="Edit" SkinID="ButtonEdit" /><br />
            <table>
                <tr>
                    <td style="width: 100px">
                        Category:</td>
                    <td >
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Policy") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        OpCo:</td>
                    <td >
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("OpCoCode") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Description:</td>
                    <td >
            <asp:Label ID="LabelDescription" runat="server" Text='<%# Eval("ExceptionDescription") %>'></asp:Label></td>
                </tr>
                 <tr>
                    <td style="width: 100px">
                        Priority:</td>
                    <td >
                        
                        
                        <asp:Label ID="LabelPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Requires Acknowledgement?:</td>
                    <td >
                                                <asp:Image ID="ImageRequiresAcknowledgement" runat="server" SkinID="Check" Visible='<%# Convert.ToBoolean(Eval("RequiresAcknowledgement")) %>' />
                        <asp:Image ID="ImageDoesNotRequireAcknowledgement" runat="server" SkinID="UnCheck" Visible='<%# !Convert.ToBoolean(Eval("RequiresAcknowledgement")) %>' />
                        
                        </td>
                </tr>
                
                <tr>
                    <td style="width: 100px">
                         <asp:Label ID="LabelEmailOperatorText" runat="server" >Email the Operator:</asp:Label></td>
                    <td >
                        
                        <asp:Image ID="ImageEmailOperator" runat="server" SkinID="Check"  />
                        <asp:Image ID="ImageDontEmailOperator" runat="server" SkinID="UnCheck"  />

                        
                        </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="LabelEmailRecipientsText" runat="server" >Email Recipients:</asp:Label></td>
                    <td >
                        <asp:Label ID="LabelEmailRecipients" runat="server" Text='<%# Bind("EmailRecipients") %>'></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="LabelEmailSubjectText" runat="server">Email Subject:</asp:Label></td>
                    <td>
                        <asp:Label ID="LabelEmailSubject" runat="server" Text='<%# Bind("EmailSubject") %>'></asp:Label></td>
                </tr>
            </table>
            <asp:HiddenField ID="Id" runat="server" Value='<%# Bind("Id") %>' />
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ExceptionType") %>' />
            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("OpCoId") %>' />
            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Bind("CheckSum") %>' />
        </ItemTemplate>
      
       
    </asp:FormView>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />
    <asp:ObjectDataSource ID="ErrorDataSource" runat="server"
         UpdateMethod="SaveErrorType"  SelectMethod="GetErrorType" DataObjectTypeName="Discovery.BusinessObjects.ErrorType"
        TypeName="Discovery.BusinessObjects.Controllers.ErrorTypeController"  >

        <SelectParameters>
            <asp:QueryStringParameter Name="exceptionType" QueryStringField="exceptionType" Type="String" />
            <asp:QueryStringParameter Name="opcoCode" QueryStringField="opcoCode" Type="String" />
            <asp:QueryStringParameter Name="policyName" QueryStringField="policyName" Type="String" />
                
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

