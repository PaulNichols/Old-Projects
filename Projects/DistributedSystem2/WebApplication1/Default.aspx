<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Localize ID="Localize1" runat="server"></asp:Localize>
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem Text="New Item" Value="New Item">
                    <asp:MenuItem Text="New Item" Value="New Item">
                        <asp:MenuItem Text="New Item" Value="New Item">
                            <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                <asp:MenuItem Text="New Item" Value="New Item">
                    <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" BackColor="#F7F6F3" BorderColor="#CCCCCC"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
            Height="342px" Width="824px">
            <StepStyle BorderWidth="0px" ForeColor="#5D7B9D" />
            <StartNavigationTemplate>
                &nbsp;
                <asp:Button ID="StartNextButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CommandName="MoveNext" Font-Names="Verdana"
                    Font-Size="0.8em" ForeColor="#284775" Text="Next" />
            </StartNavigationTemplate>
            <SideBarStyle BackColor="#7C6F57" BorderWidth="0px" Font-Size="0.9em" VerticalAlign="Top" />
            <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
            <StepNavigationTemplate>
                <asp:Button ID="StepPreviousButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="MovePrevious"
                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Previous" />
                <asp:Button ID="StepNextButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CommandName="MoveNext" Font-Names="Verdana"
                    Font-Size="0.8em" ForeColor="#284775" Text="Next" />
            </StepNavigationTemplate>
            <SideBarTemplate>
                <asp:DataList ID="SideBarList" runat="server">
                    <SelectedItemStyle Font-Bold="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" BorderWidth="0px" Font-Names="Verdana"
                            ForeColor="White"></asp:LinkButton>
                    </ItemTemplate>
                </asp:DataList>
            </SideBarTemplate>
            <WizardSteps>
                <asp:WizardStep runat="server" Title="Step 1">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="..." />
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                        CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                        ForeColor="Black" Height="180px" SelectedDate="2006-03-18" Visible="False" Width="200px">
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="Gray" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    </asp:Calendar>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Step 2">
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Finish">
                </asp:WizardStep>
            </WizardSteps>
            <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True" Font-Size="0.9em"
                ForeColor="White" HorizontalAlign="Left" />
            <FinishNavigationTemplate>
                <asp:Button ID="FinishPreviousButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" CommandName="MovePrevious"
                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Previous" />
                <asp:Button ID="FinishButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                    BorderStyle="Solid" BorderWidth="1px" CommandName="MoveComplete" Font-Names="Verdana"
                    Font-Size="0.8em" ForeColor="#284775" Text="Finish" />
            </FinishNavigationTemplate>
            <HeaderTemplate>
                FReDD Puchase Wizard
            </HeaderTemplate>
        </asp:Wizard>
    
    </div>
    </form>
</body>
</html>
