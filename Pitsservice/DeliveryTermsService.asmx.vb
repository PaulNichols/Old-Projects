Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/DeliveryTermsService")> _
Public Class DeliveryTermsService
    Inherits EPS_Service
    Implements IEPS_Service_Data

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_DeliveryTerms", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SEFreightCode", "SEFreightCode"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("DeliveryCharge", "DeliveryCharge"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("SpecialCharge", "SpecialCharge"), New System.Data.Common.DataColumnMapping("FreightChargeCode", "FreightChargeCode")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SEFreightCode", "SEFreightCode"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("DeliveryCharge", "DeliveryCharge"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("SpecialCharge", "SpecialCharge"), New System.Data.Common.DataColumnMapping("FreightChargeCode", "FreightChargeCode")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SEFreightCode", "SEFreightCode"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("DeliveryCharge", "DeliveryCharge"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("SpecialCharge", "SpecialCharge"), New System.Data.Common.DataColumnMapping("FreightChargeCode", "FreightChargeCode")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SEFreightCode", "SEFreightCode"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("DeliveryCharge", "DeliveryCharge"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("LocationName", "LocationName"), New System.Data.Common.DataColumnMapping("SpecialCharge", "SpecialCharge"), New System.Data.Common.DataColumnMapping("FreightChargeCode", "FreightChargeCode")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_DeliveryTerms]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_DeliveryTerms]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SEFreightCode", System.Data.SqlDbType.VarChar, 2,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SEFreightCode",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SpecialCharge", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SpecialCharge",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FreightChargeCode", System.Data.SqlDbType.VarChar, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FreightChargeCode",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ModifiedBy", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryCharge", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DeliveryCharge", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_DeliveryTerms]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SEFreightCode", System.Data.SqlDbType.VarChar, 2,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SEFreightCode",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryCharge", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DeliveryCharge", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SpecialCharge", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SpecialCharge",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FreightChargeCode", System.Data.SqlDbType.VarChar, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FreightChargeCode",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SEFreightCode", System.Data.SqlDbType.VarChar, 2,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SEFreightCode",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AccurideLocationId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryCharge", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"DeliveryCharge",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SpecialCharge", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"SpecialCharge",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FreightChargeCode", System.Data.SqlDbType.VarChar, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FreightChargeCode",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_DeliveryTerms]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SEFreightCode", System.Data.SqlDbType.VarChar, 2, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "SEFreightCode", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryCharge", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "DeliveryCharge", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SpecialCharge", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "SpecialCharge", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FreightChargeCode", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "FreightChargeCode", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ModifiedBy", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING

    End Sub


#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tDeliveryTerms"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@Select_DeliveryTermsId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(2), Int32)
        If pudtParams.GetUpperBound(0) = 3 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
       Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 0, 1})
    End Function
		
	Public Overrides Sub AssignFields()
		With Fields
			.Add("SEFreightCode", New FieldInfoStructure("SEFreightCode", 2, False, False))
			.Add("DeliveryTermsId", New FieldInfoStructure("DeliveryTermsId", 10, False, False))
			.Add("Description", New FieldInfoStructure("Description", 250, True, False))
			.Add("AccurideLocationId", New FieldInfoStructure("AccurideLocationId", 10, False, False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("DeliveryCharge", New FieldInfoStructure("DeliveryCharge", 19, False, False, , 0))
			.Add("LocationName", New FieldInfoStructure("LocationName", 50, False, False))
			.Add("SpecialCharge", New FieldInfoStructure("SpecialCharge", 1, False, False, , False))
			.Add("FreightChargeCode", New FieldInfoStructure("FreightChargeCode", 1, True, False))
		End With
	End Sub
End Class

