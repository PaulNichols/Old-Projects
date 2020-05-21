Public MustInherit Class BaseTemplate
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
    End Sub

    Protected MustOverride Sub SetBookMarks(ByRef doc As Object)

    Protected MustOverride Sub SetSummary(ByRef doc As Object)

    Protected Overridable Sub UpdateUser(ByVal user As ASPNET.StarterKit.Portal.UserDetails)

    End Sub

    Protected Sub SetUpTemplate(ByVal sender As System.Object, ByVal e As System.EventArgs)

        UpdateUser(CType(Session("User"), ASPNET.StarterKit.Portal.UserDetails))

        Dim URL As String = Request.QueryString.Item("url")
        Dim FriendlyName As String = Request.QueryString.Item("FriendlyName")
        ' Dim WordApplication As Object = System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application")
        Dim WordApplication As New Word.Application
        ' Dim WordApplication As Object = Microsoft.VisualBasic.Interaction.CreateObject("Word.Application")
        Dim doc As Object = WordApplication.Documents.Add(URL, False, 0, True)

        SetBookMarks(doc)

        SetSummary(doc)

        Dim filename As String = IO.Path.GetTempFileName
        doc.saveas(filename)
        doc.Close()
        doc = Nothing
        WordApplication.Quit()
        WordApplication = Nothing

        ' Stop : Response.Redirect(filename)

        Try
            Response.Buffer = True
            Response.Clear()
            Response.AddHeader("Content-Disposition", "inline; filename=""" & FriendlyName & """")

            '    Response.ContentType = "application/x-msdownload"
            Response.ContentType = "application/msword"

            'Response.WriteFile(filename)

            Dim stream As IO.FileStream = IO.File.OpenRead(filename)
            Dim ByteArray As Byte()
            ReDim ByteArray(stream.Length)
            stream.Read(ByteArray, 0, stream.Length)

            Response.BinaryWrite(ByteArray)
            stream.Close()
            stream = Nothing
            IO.File.Delete(filename)
            ' Response.End()
            'Response.Flush()
            ' Response.Close()
        Catch ex As System.Exception
            Stop
        End Try
    End Sub
End Class
