Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ContactService")> _
Public Class ContactService
    Inherits EPS_Service
    Implements IEPS_Service_Data

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Contact", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactId", "ContactId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactId", "ContactId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactId", "ContactId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactId", "ContactId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Contact"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerId", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactId", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tContact"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@CustomerId").Value = pudtParams(0)
        If pudtParams(1) Is Convert.DBNull OrElse CType(pudtParams(1), Int32) = 0 Then
            SqlDataAdapter1.SelectCommand.Parameters("@ContactId").Value = Convert.DBNull
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@ContactId").Value = pudtParams(1)
        End If
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadContactCombo(ByVal customerId As Int32) As DataSet
        Return LoadData(New Object() {customerId, 0, 1})
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("CustomerId", New FieldInfoStructure("CompanyId", 10, True, False))
            .Add("ContactId", New FieldInfoStructure("ContactId", 10, False, False))
            .Add("ContactName", New FieldInfoStructure("ContactName", 100, True, False))
            .Add("WorkPhone", New FieldInfoStructure("WorkPhone", 30, True, False))
        End With
    End Sub
End Class
