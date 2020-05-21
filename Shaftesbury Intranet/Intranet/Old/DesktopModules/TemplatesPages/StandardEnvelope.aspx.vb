Public Class StandardEnvelope
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

     

        Dim WordApplication As Object = Microsoft.VisualBasic.Interaction.CreateObject("Word.Application")
        Dim doc As Object
        Dim FriendlyName As String = Request.QueryString.Item("FriendlyName")
        With WordApplication
            doc = .Documents.Add()

            Dim Envelope As String = CType(System.Configuration.ConfigurationSettings.AppSettings("StandardEnvelope"), String)
            With CType(Session("CurrentContact"), ASPNET.StarterKit.Portal.ContactDetails)
                doc.Envelope(ExtractAddress:=False, OmitReturnAddress _
                        :=False, PrintBarCode:=False, Address:= _
                        .ContactAddress, AutoText:="ToolsCreateLabels1", ReturnAddress:="", ReturnAutoText:= _
                        "ToolsCreateLabels2", AddressFromLeft:=0, AddressFromTop:= _
                        0, ReturnAddressFromLeft:=0, _
                        ReturnAddressFromTop:=0, DefaultOrientation:=3 _
                        , DefaultFaceUp:=True)


            End With
        
        End With

        Dim filename As String = IO.Path.GetTempFileName
        doc.saveas(filename)
        doc.Close()
        doc = Nothing
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
    End Sub

End Class
