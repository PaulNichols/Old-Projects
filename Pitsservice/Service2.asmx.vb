'Imports System.Web.Services

'<WebService(Namespace := "http://tempuri.org/")> _
'Public Class Service2
'    Inherits System.Web.Services.WebService

'#Region " Web Services Designer Generated Code "

'    Public Sub New()
'        MyBase.New()

'        'This call is required by the Web Services Designer.
'        InitializeComponent()

'        'Add your own initialization code after the InitializeComponent() call

'    End Sub

'    'Required by the Web Services Designer
'    Private components As System.ComponentModel.IContainer

'    'NOTE: The following procedure is required by the Web Services Designer
'    'It can be modified using the Web Services Designer.  
'    'Do not modify it using the code editor.
'    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
'    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
'    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
'    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
'    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
'    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
'    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
'        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
'        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
'        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
'        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
'        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
'        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
'        '
'        'SqlDataAdapter1
'        '
'        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
'        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
'        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
'        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "tBracket", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BracketNumber", "BracketNumber"), New System.Data.Common.DataColumnMapping("BracketName", "BracketName")})})
'        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
'        '
'        'SqlSelectCommand1
'        '
'        Me.SqlSelectCommand1.CommandText = "[NewSelectCommand]"
'        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
'        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
'        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
'        '
'        'SqlInsertCommand1
'        '
'        Me.SqlInsertCommand1.CommandText = "[NewInsertCommand]"
'        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
'        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
'        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
'        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketNumber", System.Data.SqlDbType.VarChar, 25,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"@BracketNumber",System.Data.DataRowVersion.Current,Nothing))
'        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketName", System.Data.SqlDbType.VarChar, 40,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"@BracketName",System.Data.DataRowVersion.Current,Nothing))
'        '
'        'SqlUpdateCommand1
'        '
'        Me.SqlUpdateCommand1.CommandText = "[NewUpdateCommand]"
'        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
'        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketNumber", System.Data.SqlDbType.VarChar, 25,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"@BracketNumber",System.Data.DataRowVersion.Current,Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketName", System.Data.SqlDbType.VarChar, 40,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"@BracketName",System.Data.DataRowVersion.Current,Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketId",System.Data.DataRowVersion.Original, Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketName",System.Data.DataRowVersion.Original, Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketNumber",System.Data.DataRowVersion.Original, Nothing))
'        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BracketId", System.Data.SqlDbType.Int, 4,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"@BracketId",System.Data.DataRowVersion.Current,Nothing))
'        '
'        'SqlDeleteCommand1
'        '
'        Me.SqlDeleteCommand1.CommandText = "[NewDeleteCommand]"
'        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
'        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
'        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
'        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketId",System.Data.DataRowVersion.Original, Nothing))
'        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketName", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketName",System.Data.DataRowVersion.Original, Nothing))
'        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BracketNumber", System.Data.SqlDbType.VarChar, 25, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"BracketNumber",System.Data.DataRowVersion.Original, Nothing))
'        '
'        'SqlConnection1
'        '
'        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING

'    End Sub

'    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
'        'CODEGEN: This procedure is required by the Web Services Designer
'        'Do not modify it using the code editor.
'        If disposing Then
'            If Not (components Is Nothing) Then
'                components.Dispose()
'            End If
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'#End Region

'    ' WEB SERVICE EXAMPLE
'    ' The HelloWorld() example service returns the string Hello World.
'    ' To build, uncomment the following lines then save and build the project.
'    ' To test this web service, ensure that the .asmx file is the start page
'    ' and press F5.
'    '
'    '<WebMethod()> Public Function HelloWorld() As String
'    '	HelloWorld = "Hello World"
'    ' End Function

'End Class
