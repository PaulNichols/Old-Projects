Public Class Mins
    Inherits BaseTemplate

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button

    Protected WithEvents lblMeetingOf As System.Web.UI.WebControls.Label
    Protected WithEvents txtDateOfMeeting As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDateOfMeeting As System.Web.UI.WebControls.Label
    Protected WithEvents txtTimeOfMeeting As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTimeOfMeeting As System.Web.UI.WebControls.Label
    Protected WithEvents txtInAttendance As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblInAttendance As System.Web.UI.WebControls.Label
    Protected WithEvents cboType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents CompareValidator2 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler cmdNext.Click, AddressOf MyBase.SetUpTemplate
    End Sub


    Protected Overrides Sub SetBookMarks(ByRef doc As Object)
        With doc.Bookmarks
            .Item("bmMeetingOf").Range.Text = cboType.SelectedValue
            .Item("bmDate").Range.Text = Me.txtDateOfMeeting.Text
            .Item("bmTime").Range.Text = Me.txtTimeOfMeeting.Text
            .Item("bmInAttendance").Range.Text = Me.txtInAttendance.Text.Replace(Environment.NewLine, Environment.NewLine & Microsoft.VisualBasic.Constants.vbTab)
        End With
    End Sub

    Protected Overrides Sub SetSummary(ByRef doc As Object)
        ASPNET.StarterKit.Portal.Global.SetSummary(doc, "Minutes of Meeting", "Meeting of " & cboType.SelectedValue, _
                  CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails).Ref, "MINUTES")
    End Sub
End Class
