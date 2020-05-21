Public Class apps
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here


        Dim WordApplication As Object = Microsoft.VisualBasic.Interaction.CreateObject("Word.Application")
        Dim doc As Object
        With WordApplication
            doc = .Documents.Add()
        End With

        Dim filename As String = IO.Path.GetTempFileName
        doc.saveas(filename)
        doc.Close()
        doc = Nothing
        WordApplication.Quit()
        WordApplication = Nothing

        Try
            Response.AppendHeader("content-disposition", "inline; filename=" & "")
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
