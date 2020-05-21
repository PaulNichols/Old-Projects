Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/CostingService")> _
Public Class CostingService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Costing", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("AnnualQty", "AnnualQty"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Amortize", "Amortize"), New System.Data.Common.DataColumnMapping("GrossMargin", "GrossMargin"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("PackingCost", "PackingCost"), New System.Data.Common.DataColumnMapping("PackingTypeId", "PackingTypeId"), New System.Data.Common.DataColumnMapping("CapitalToolCost", "CapitalToolCost"), New System.Data.Common.DataColumnMapping("AmortizeCost", "AmortizeCost"), New System.Data.Common.DataColumnMapping("Batch250", "Batch250"), New System.Data.Common.DataColumnMapping("Batch500", "Batch500"), New System.Data.Common.DataColumnMapping("Batch1000", "Batch1000"), New System.Data.Common.DataColumnMapping("Batch2000", "Batch2000"), New System.Data.Common.DataColumnMapping("Batch", "Batch"), New System.Data.Common.DataColumnMapping("PrimeCost", "PrimeCost"), New System.Data.Common.DataColumnMapping("PrimeMargin", "PrimeMargin"), New System.Data.Common.DataColumnMapping("MinGrossPercent", "MinGrossPercent"), New System.Data.Common.DataColumnMapping("MaxGrossPercent", "MaxGrossPercent"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId"), New System.Data.Common.DataColumnMapping("CurrencyId", "CurrencyId"), New System.Data.Common.DataColumnMapping("CostingId", "CostingId"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("PaymentTermsId", "PaymentTermsId"), New System.Data.Common.DataColumnMapping("PlatingId", "PlatingId"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("AnnualQty", "AnnualQty"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("PlatingCode", "PlatingCode"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Amortize", "Amortize"), New System.Data.Common.DataColumnMapping("GrossMargin", "GrossMargin"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("PackingCost", "PackingCost"), New System.Data.Common.DataColumnMapping("PackingTypeId", "PackingTypeId"), New System.Data.Common.DataColumnMapping("CapitalToolCost", "CapitalToolCost"), New System.Data.Common.DataColumnMapping("AmortizeCost", "AmortizeCost"), New System.Data.Common.DataColumnMapping("Batch250", "Batch250"), New System.Data.Common.DataColumnMapping("Batch500", "Batch500"), New System.Data.Common.DataColumnMapping("Batch1000", "Batch1000"), New System.Data.Common.DataColumnMapping("Batch2000", "Batch2000"), New System.Data.Common.DataColumnMapping("Batch", "Batch"), New System.Data.Common.DataColumnMapping("PrimeCost", "PrimeCost"), New System.Data.Common.DataColumnMapping("PrimeMargin", "PrimeMargin"), New System.Data.Common.DataColumnMapping("MinGrossPercent", "MinGrossPercent"), New System.Data.Common.DataColumnMapping("MaxGrossPercent", "MaxGrossPercent"), New System.Data.Common.DataColumnMapping("CurrencyToId", "CurrencyToId"), New System.Data.Common.DataColumnMapping("CurrencyId", "CurrencyId"), New System.Data.Common.DataColumnMapping("CostingId", "CostingId"), New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("PaymentTermsId", "PaymentTermsId"), New System.Data.Common.DataColumnMapping("PlatingId", "PlatingId"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("DeliveryDesc", "DeliveryDesc"), New System.Data.Common.DataColumnMapping("PaymentDesc", "PaymentDesc"), New System.Data.Common.DataColumnMapping("PackingDesc", "PackingDesc")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CostingSlideId", "CostingSlideId"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Amortize", "Amortize"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("PaymentTermsId", "PaymentTermsId"), New System.Data.Common.DataColumnMapping("PlatingId", "PlatingId"), New System.Data.Common.DataColumnMapping("AnnualQty", "AnnualQty"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("CurrencyId", "CurrencyId"), New System.Data.Common.DataColumnMapping("GrossMargin", "GrossMargin"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("Pairs", "Pairs"), New System.Data.Common.DataColumnMapping("PackingTypeId", "PackingTypeId"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Costing]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GrossMargin", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(4, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Amortize", System.Data.SqlDbType.Bit, 1))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyToId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_Costing]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerId", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CustomerId",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Amortize", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Amortize",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlatingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PlatingId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnnualQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "AnnualQty", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "BatchSize", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "CurrencyId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PackingTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccuridePartNumber", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"AccuridePartNumber",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "ProductLength", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_Costing]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerId", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CustomerId",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Amortize", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Amortize",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlatingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PlatingId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnnualQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "AnnualQty", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "BatchSize", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "CurrencyId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GrossMargin", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "GrossMargin", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Pairs", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Pairs",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PackingTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccuridePartNumber", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"AccuridePartNumber",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "ProductLength", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "TargetMin", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "TargetMax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerId", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CustomerId",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 250,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Description",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Amortize", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Amortize",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PaymentTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PlatingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PlatingId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AnnualQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"AnnualQty",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"BatchSize",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CurrencyId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_GrossMargin", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(16, Byte), CType(4, Byte),"GrossMargin",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"CurrencyDate",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Pairs", System.Data.SqlDbType.Bit, 1,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Pairs",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PackingTypeId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccuridePartNumber", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"AccuridePartNumber",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"ProductLength",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMin",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMax",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingSlideId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_Costing]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "CustomerId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Description", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Amortize", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Amortize", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlatingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PlatingId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnnualQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "AnnualQty", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "BatchSize", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "CurrencyId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GrossMargin", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, False, CType(16, Byte), CType(4, Byte), "GrossMargin", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CurrencyDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Pairs", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "Pairs", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PackingTypeId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccuridePartNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AccuridePartNumber", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, False, CType(15, Byte), CType(0, Byte), "ProductLength", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "TargetMin", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "TargetMax", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CustomerId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 250, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Description",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Amortize", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Amortize",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PaymentTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PlatingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PlatingId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AnnualQty", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"AnnualQty",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"BatchSize",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CurrencyId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_GrossMargin", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(16, Byte), CType(4, Byte),"GrossMargin",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CurrencyDate",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Pairs", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Pairs",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PackingTypeId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccuridePartNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"AccuridePartNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"ProductLength",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMin",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMax",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_CostingSlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CostingSlideId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tCostingSlide"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function Test() As System.Data.DataSet
        Return LoadData(New Object() {0, 1, 17}) '0.3D, 0})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function ReCost(ByVal costingSlideId As Int32) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim SelectFunction As New SqlCommand("Costing_Calculate", Me.SqlConnection1)
            SelectFunction.CommandType = CommandType.StoredProcedure
            SelectFunction.Parameters.Add("@CostingSlideId", costingSlideId)
            SelectFunction.ExecuteNonQuery()
            ReturnValue = True
            SelectFunction = Nothing
        End Try
        Return ReturnValue
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@CostingSlideId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@CurrencyToId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = CType(pudtParams(2), Int32)
        If pudtParams.GetUpperBound(0) > 2 Then
            If Not pudtParams(3).Equals(Convert.DBNull) Then
                SqlDataAdapter1.SelectCommand.Parameters("@GrossMargin").Value = CType(pudtParams(3), Decimal)
            End If
        End If
        If pudtParams.GetUpperBound(0) > 3 Then
            If Not pudtParams(4).Equals(Convert.DBNull) Then
                SqlDataAdapter1.SelectCommand.Parameters("@Amortize").Value = CType(pudtParams(4), Boolean)
            End If
        End If

        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Int32, ByVal pintAccurideLocationId As Int32) As DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function CanCost(ByVal projectId As Int32) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim SelectFunction As New SqlCommand("Select dbo.CanCost (" & projectId & ")", Me.SqlConnection1)
            Dim ReturnObject As Object = SelectFunction.ExecuteScalar()
            If ReturnObject Is Nothing OrElse ReturnObject.Equals(Convert.DBNull) Then
                ReturnValue = True
            Else
                ReturnValue = CType(SelectFunction.ExecuteScalar(), Boolean)
            End If
            SelectFunction = Nothing
        End Try
        Return ReturnValue
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("AccuridePartNumber", New FieldInfoStructure("AccuridePartNumber", 50, True, False))
            .Add("Amortize", New FieldInfoStructure("Amortize", 1, True, False, , False))
            .Add("AnnualQty", New FieldInfoStructure("AnnualQty", 53, True, False))
            .Add("BatchSize", New FieldInfoStructure("BatchSize", 53, True, False))
            .Add("CostingSlideId", New FieldInfoStructure("CostingSlideId", 10, False, False))
            .Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
            .Add("CurrencyId", New FieldInfoStructure("CurrencyId", 10, True, False))
            .Add("CustomerId", New FieldInfoStructure("CustomerId", 100, True, False))
            .Add("DeliveryTermsId", New FieldInfoStructure("DeliveryTermsId", 10, True, False))
            .Add("Description", New FieldInfoStructure("Description", 250, True, False))
            .Add("GrossMargin", New FieldInfoStructure("GrossMargin", 16, False, False, , 0.3))
            .Add("PackingTypeId", New FieldInfoStructure("PackingTypeId", 10, True, False))
            .Add("Pairs", New FieldInfoStructure("Pairs", 1, False, False, , True))
            .Add("PaymentTermsId", New FieldInfoStructure("PaymentTermsId", 10, True, False))
            .Add("PlatingId", New FieldInfoStructure("PlatingId", 10, True, False))
            .Add("ProductLength", New FieldInfoStructure("ProductLength", 53, True, False))
            .Add("ProjectId", New FieldInfoStructure("ProjectId", 10, True, False))
            .Add("TargetMax", New FieldInfoStructure("TargetMax", 19, False, False, , 0))
            .Add("TargetMin", New FieldInfoStructure("TargetMin", 19, False, False, , 0))
        End With
    End Sub
End Class
