Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/CustomerService")> _
Public Class CustomerService
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

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Customer", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("Address1", "Address1"), New System.Data.Common.DataColumnMapping("Address2", "Address2"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("County", "County"), New System.Data.Common.DataColumnMapping("PostCode", "PostCode"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("Phone1", "Phone1"), New System.Data.Common.DataColumnMapping("Phone2", "Phone2"), New System.Data.Common.DataColumnMapping("Fax1", "Fax1"), New System.Data.Common.DataColumnMapping("URL", "URL"), New System.Data.Common.DataColumnMapping("MarketCode", "MarketCode"), New System.Data.Common.DataColumnMapping("PitsCustomer", "PitsCustomer")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("Address1", "Address1"), New System.Data.Common.DataColumnMapping("Address2", "Address2"), New System.Data.Common.DataColumnMapping("City", "City"), New System.Data.Common.DataColumnMapping("County", "County"), New System.Data.Common.DataColumnMapping("PostCode", "PostCode"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("Phone1", "Phone1"), New System.Data.Common.DataColumnMapping("Phone2", "Phone2"), New System.Data.Common.DataColumnMapping("Fax1", "Fax1"), New System.Data.Common.DataColumnMapping("URL", "URL"), New System.Data.Common.DataColumnMapping("MarketCode", "MarketCode"), New System.Data.Common.DataColumnMapping("PitsCustomer", "PitsCustomer")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Customer]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerId", System.Data.SqlDbType.VarChar, 50))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.VarChar, 50))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
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
            Return "tCustomer"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
     Public Function runtest() As System.Data.DataSet
        Return LoadData(New Object() {1, 0})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Select_AccurideLocationId").Value = CType(pudtParams(0), String)
        SqlDataAdapter1.SelectCommand.Parameters("@CustomerId").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = CType(pudtParams(2), Int32)
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function TransferProjects(ByVal strCustomerFromId As String, ByVal strCustomerToId As String) As Boolean
    '    Dim SqlCommand1 As System.Data.SqlClient.SqlCommand
    '    SqlCommand1 = New System.Data.SqlClient.SqlCommand()
    '    With SqlCommand1
    '        .CommandText = "ps_TransferCustomerProjects"
    '        .CommandType = System.Data.CommandType.StoredProcedure
    '        .Connection = Me.SqlConnection1
    '        .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
    '        .Parameters.Add("@CustomerToId", strCustomerToId)
    '        .Parameters.Add("@CustomerFromId", strCustomerFromId)
    '        Try
    '            .Connection.Open()
    '            .ExecuteNonQuery()
    '            Return CType(.Parameters("@RETURN_VALUE").Value.ToString, Int32) = 1
    '        Catch
    '            Return False
    '        End Try
    '    End With
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal uniqueId As Int32, ByVal accurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCustomerCombo(ByVal customerId As Int32, ByVal accurideLocationId As Int32) As DataSet
        Return LoadData(New Object() {accurideLocationId, customerId, 1})
    End Function

    <WebMethod()> Public Function ACTResynchronise() As DataSet
        Dim bSucess As Byte
        Dim SqlMaxIdCommand1 As System.Data.SqlClient.SqlCommand
        SqlMaxIdCommand1 = New System.Data.SqlClient.SqlCommand()
        SqlMaxIdCommand1.CommandText = "ps_CopyACTData"
        SqlMaxIdCommand1.CommandType = System.Data.CommandType.StoredProcedure
        SqlMaxIdCommand1.Connection = Me.SqlConnection1
        Try
            SqlMaxIdCommand1.Connection.Open()

            SqlMaxIdCommand1.ExecuteNonQuery()
            Return LoadData(New Object() {""})
        Catch
            Return Nothing
        End Try
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("CompanyName", New FieldInfoStructure("CompanyName", 100, True, False))
            .Add("Address1", New FieldInfoStructure("Address1", 100, True, False))
            .Add("Address2", New FieldInfoStructure("Address2", 100, True, False))
            .Add("County", New FieldInfoStructure("County", 100, True, False))
            .Add("City", New FieldInfoStructure("City", 100, True, False))
            .Add("Country", New FieldInfoStructure("Country", 100, True, False))
            .Add("PostCode", New FieldInfoStructure("PostCode", 100, True, False))
            .Add("Phone1", New FieldInfoStructure("Phone1", 100, True, False))
            .Add("Phone2", New FieldInfoStructure("Phone2", 100, True, False))
            .Add("Fax1", New FieldInfoStructure("Fax1", 100, True, False))
            .Add("URL", New FieldInfoStructure("URL", 100, True, False))
            .Add("MarketCode", New FieldInfoStructure("MarketCode", 3, True, False))
            .Add("ACTCustomerId", New FieldInfoStructure("ACTCustomerId", 100, True, False))
            .Add("CustomerId", New FieldInfoStructure("CustomerId", 10, False, False))
            .Add("CountryId", New FieldInfoStructure("CountryId", 10, True, False))
        End With
    End Sub
End Class
