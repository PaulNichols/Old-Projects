Public Class StandardLabel
    Inherits Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdNext As System.Web.UI.WebControls.Button
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTo As System.Web.UI.WebControls.Label
    Protected WithEvents txtTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCopy As System.Web.UI.WebControls.Label
    Protected WithEvents txtCopyTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents txtFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFrom As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTo2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCopyTo2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents radOptions As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtRow As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtColumn As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblRow As System.Web.UI.WebControls.Label
    Protected WithEvents lblColumn As System.Web.UI.WebControls.Label
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator2 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents CompareValidator4 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents lblPrintProblem As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click

        Dim WordApplication As Object = Microsoft.VisualBasic.Interaction.CreateObject("Word.Application")
        Dim doc As Object
        Dim FriendlyName As String = Request.QueryString.Item("FriendlyName")
        Dim PrintSingle As Boolean = CType(radOptions.SelectedValue, Boolean)
        Dim Label As String = CType(System.Configuration.ConfigurationSettings.AppSettings("StandardLabel"), String)

        lblPrintProblem.Visible = False

        If PrintSingle Then
            Dim Row As Int32
            Dim Column As Int32
            Row = CType(txtRow.Text, Int32)
            Column = CType(txtColumn.Text, Int32)
            Try
                With WordApplication
                    doc = .Documents.Add()
                    With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
                        WordApplication.MailingLabel.PrintOut( _
                        Name:=Label, _
                        Address:=.ContactAddress, _
                        SingleLabel:=True, Row:=Row, Column:=Column)
                    End With
                End With
                '                        LaserTray:=WordApplication.WdPaperTray.wdPrinterManualFeed, _

            Catch ex As Exception
                lblPrintProblem.Visible = True
            End Try

        Else
            Dim filename As String
            With WordApplication
                doc = .Documents.Add()
                .MailingLabel.DefaultPrintBarCode = False
                With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
                    doc = WordApplication.MailingLabel.CreateNewDocument( _
                    Name:=Label, _
                    Address:=.ContactAddress, _
                    AutoText:="ToolsCreateLabels1")
                End With
                .Selection.WholeStory()

                With .Selection.ParagraphFormat
                    .LeftIndent = WordApplication.CentimetersToPoints(0.63)
                    .SpaceBeforeAuto = False
                    .SpaceAfterAuto = False
                End With
                filename = IO.Path.GetTempFileName
                doc.saveas(filename)
                doc.Close()
                doc = Nothing
            End With
            WordApplication.Quit()
            WordApplication = Nothing

            Try
                Response.AppendHeader("content-disposition", "inline; filename=" & FriendlyName)
                Response.ContentType = "application/msword"

                Dim stream As IO.FileStream = IO.File.OpenRead(filename)
                Dim ByteArray As Byte()
                ReDim ByteArray(stream.Length)
                stream.Read(ByteArray, 0, stream.Length)

                Response.BinaryWrite(ByteArray)
                stream.Close()
                stream = Nothing
                IO.File.Delete(filename)
                Response.End()
            Catch
            End Try
        End If
    End Sub

    Private Sub radOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radOptions.SelectedIndexChanged
        Dim State As Boolean = (CType(radOptions.SelectedValue, Boolean))
        txtColumn.Visible = State
        txtRow.Visible = State
        lblRow.Visible = State
        lblColumn.Visible = State
        Validate()
        cmdNext.Visible = (radOptions.SelectedIndex > -1 AndAlso IsValid)
    End Sub
End Class
