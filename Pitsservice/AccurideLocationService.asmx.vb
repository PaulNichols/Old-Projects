Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/AccurideLocationService")> _
Public Class AccurideLocationService
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
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_AccurideLocation", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationid", "AccurideLocationid"), New System.Data.Common.DataColumnMapping("Address1", "Address1"), New System.Data.Common.DataColumnMapping("Address2", "Address2"), New System.Data.Common.DataColumnMapping("Address3", "Address3"), New System.Data.Common.DataColumnMapping("Address4", "Address4"), New System.Data.Common.DataColumnMapping("CultureId", "CultureId"), New System.Data.Common.DataColumnMapping("DefaultCurrencyId", "DefaultCurrencyId"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("PostCode", "PostCode"), New System.Data.Common.DataColumnMapping("Tel", "Tel"), New System.Data.Common.DataColumnMapping("WorkingHoursSat", "WorkingHoursSat"), New System.Data.Common.DataColumnMapping("WorkingHoursSun", "WorkingHoursSun"), New System.Data.Common.DataColumnMapping("WorkingHoursMon", "WorkingHoursMon"), New System.Data.Common.DataColumnMapping("WorkingHoursTue", "WorkingHoursTue"), New System.Data.Common.DataColumnMapping("WorkingHoursWed", "WorkingHoursWed"), New System.Data.Common.DataColumnMapping("WorkingHoursThur", "WorkingHoursThur"), New System.Data.Common.DataColumnMapping("WorkingHoursFri", "WorkingHoursFri")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("Address1", "Address1"), New System.Data.Common.DataColumnMapping("Address2", "Address2"), New System.Data.Common.DataColumnMapping("Address3", "Address3"), New System.Data.Common.DataColumnMapping("Address4", "Address4"), New System.Data.Common.DataColumnMapping("PostCode", "PostCode"), New System.Data.Common.DataColumnMapping("Tel", "Tel"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("DefaultCurrencyId", "DefaultCurrencyId"), New System.Data.Common.DataColumnMapping("CultureId", "CultureId"), New System.Data.Common.DataColumnMapping("WorkingHoursSat", "WorkingHoursSat"), New System.Data.Common.DataColumnMapping("WorkingHoursSun", "WorkingHoursSun"), New System.Data.Common.DataColumnMapping("WorkingHoursMon", "WorkingHoursMon"), New System.Data.Common.DataColumnMapping("WorkingHoursTue", "WorkingHoursTue"), New System.Data.Common.DataColumnMapping("WorkingHoursWed", "WorkingHoursWed"), New System.Data.Common.DataColumnMapping("WorkingHoursThur", "WorkingHoursThur"), New System.Data.Common.DataColumnMapping("WorkingHoursFri", "WorkingHoursFri")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_AccurideLocation"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationid",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UserLocationId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "ps_Insert_AccurideLocation"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LocationName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "LocationName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address1", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address1", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address2", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address2", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address3", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address3", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address4", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address4", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PostCode", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PostCode", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Tel", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Fax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Email", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DefaultCurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "DefaultCurrencyId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CultureId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CultureId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CountryId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CountryID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursSat", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSat", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursSun", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSun", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursMon", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursMon", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursTue", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursTue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursWed", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursWed", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursThur", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursThur", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursFri", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursFri", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_AccurideLocation"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address1", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address1", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address2", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address2", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address3", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address3", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Address4", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address4", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CultureId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CultureId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CountryId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CountryId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DefaultCurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "DefaultCurrencyId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Email", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fax", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Fax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LocationName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "LocationName", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PostCode", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PostCode", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Tel", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Tel", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursSat", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSat", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursSun", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSun", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursMon", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursMon", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursTue", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursTue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursWed", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursWed", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursThur", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursThur", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@WorkingHoursFri", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursFri", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address1", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Address1",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address2", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Address2",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address3", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Address3",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address4", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Address4",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CultureId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CultureId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CountryId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CountryId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DefaultCurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DefaultCurrencyId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Email", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Email",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Fax", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Fax",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LocationName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"LocationName",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PostCode", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"PostCode",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Tel", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Tel",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursSat", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursSat",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursSun", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursSun",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursMon", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursMon",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursTue", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursTue",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursWed", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursWed",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursThur", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursThur",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursFri", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(18, Byte), CType(2, Byte),"WorkingHoursFri",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationid",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "ps_Delete_AccurideLocation"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address1", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address1", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address2", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address2", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address3", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address3", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Address4", System.Data.SqlDbType.Char, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Address4", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CultureId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CultureId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CountryId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CountryId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DefaultCurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "DefaultCurrencyId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Email", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Email", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Fax", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Fax", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LocationName", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "LocationName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PostCode", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PostCode", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Tel", System.Data.SqlDbType.Char, 30, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Tel", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursSat", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSat", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursSun", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursSun", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursMon", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursMon", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursTue", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursTue", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursWed", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursWed", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursThur", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursThur", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_WorkingHoursfri", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(18, Byte), CType(2, Byte), "WorkingHoursfri", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationid", System.Data.DataRowVersion.Current, Nothing))
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

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@Select_AccurideLocationid").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserLocationId").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
 Public Function test() As System.Data.DataSet
        Return LoadData(New Object() {0, 1})

    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <WebMethod()> Public Function GetLocationForMachine(ByVal strCurrentCulture As String) As Int32
        Dim intReturn As Int32 = 10
        'Try
        '    Me.SqlConnection1.Open()
        'Catch
        '    intReturn = -1
        'Finally
        Me.SqlConnection1.Open()

        Dim udtSelect As New SqlCommand("GetLocationForMachine", Me.SqlConnection1)

        With udtSelect
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            .Parameters.Add("@Culture", strCurrentCulture)
            Try
                .ExecuteNonQuery()
                intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
            Catch
                intReturn = -1
            End Try
        End With
        ' End Try
        Return intReturn
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, 0, 1})
    End Function

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tAccurideLocation"
        End Get
    End Property

    Public Overrides Sub AssignFields()
        With Fields
            .Add("AccurideLocationid", New FieldInfoStructure("AccurideLocationid", 10, False, False))
            .Add("LocationName", New FieldInfoStructure("LocationName", 50, False, False))
            .Add("Address1", New FieldInfoStructure("Address1", 100, True, False))
            .Add("Address2", New FieldInfoStructure("Address2", 100, True, False))
            .Add("Address3", New FieldInfoStructure("Address3", 100, True, False))
            .Add("Address4", New FieldInfoStructure("Address4", 100, True, False))
            .Add("PostCode", New FieldInfoStructure("PostCode", 10, True, False))
            .Add("Tel", New FieldInfoStructure("Tel", 30, True, False))
            .Add("Fax", New FieldInfoStructure("Fax", 30, True, False))
            .Add("Email", New FieldInfoStructure("Email", 20, True, False))
            .Add("DefaultCurrencyId", New FieldInfoStructure("DefaultCurrencyId", 10, False, False))
            .Add("CultureId", New FieldInfoStructure("CultureId", 10, False, False))
            .Add("WorkingHoursSat", New FieldInfoStructure("WorkingHoursSat", 18, False, False, , 0))
            .Add("WorkingHoursSun", New FieldInfoStructure("WorkingHoursSun", 18, False, False, , 0))
            .Add("WorkingHoursMon", New FieldInfoStructure("WorkingHoursMon", 18, False, False, , 0))
            .Add("WorkingHoursTue", New FieldInfoStructure("WorkingHoursTue", 18, False, False, , 0))
            .Add("WorkingHoursWed", New FieldInfoStructure("WorkingHoursWed", 18, False, False, , 0))
            .Add("WorkingHoursThur", New FieldInfoStructure("WorkingHoursThur", 18, False, False, , 0))
            .Add("WorkingHoursFri", New FieldInfoStructure("WorkingHoursFri", 18, False, False, , 0))
            .Add("CountryId", New FieldInfoStructure("CountryId", 10, True, False))
            .Add("tCurrency_Description_Alternative", New FieldInfoStructure("tCurrency_Description_Alternative", 100, False, False))
            .Add("tCulture_CultureName_Alternative", New FieldInfoStructure("tCulture_CultureName_Alternative", 100, False, False))
        End With
    End Sub

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Encrypt(ByVal text As String) As String
    '    Dim Encryptor As New Crypt(Crypt.SymmProvEnum.DES)
    '    Return Encryptor.Encrypting(text, "Iridium")
    'End Function


End Class
