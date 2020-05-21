Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/SalesService")> _
Public Class SalesService
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
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Sales", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ExistingSlide", "ExistingSlide"), New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("ProductLife", "ProductLife"), New System.Data.Common.DataColumnMapping("LoadRating", "LoadRating"), New System.Data.Common.DataColumnMapping("ProductTravel", "ProductTravel"), New System.Data.Common.DataColumnMapping("CompetitorID", "CompetitorID"), New System.Data.Common.DataColumnMapping("SideSpace", "SideSpace"), New System.Data.Common.DataColumnMapping("SlideThickness", "SlideThickness"), New System.Data.Common.DataColumnMapping("InstallWidth", "InstallWidth"), New System.Data.Common.DataColumnMapping("PackingTypeId", "PackingTypeId"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("QuoteNumber", "QuoteNumber"), New System.Data.Common.DataColumnMapping("CustomerPartNumber", "CustomerPartNumber"), New System.Data.Common.DataColumnMapping("SalesManagerId", "SalesManagerId"), New System.Data.Common.DataColumnMapping("InitiatorID", "InitiatorID"), New System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"), New System.Data.Common.DataColumnMapping("Contact1ID", "Contact1ID"), New System.Data.Common.DataColumnMapping("Contact2ID", "Contact2ID"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("SlideId", "SlideId"), New System.Data.Common.DataColumnMapping("PlatingID", "PlatingID"), New System.Data.Common.DataColumnMapping("BBUs", "BBUs"), New System.Data.Common.DataColumnMapping("AgentCommision", "AgentCommision"), New System.Data.Common.DataColumnMapping("AnticipatedSuccessId", "AnticipatedSuccessId"), New System.Data.Common.DataColumnMapping("CustomerPayForTooling", "CustomerPayForTooling"), New System.Data.Common.DataColumnMapping("CustomerDrawingsAvailable", "CustomerDrawingsAvailable"), New System.Data.Common.DataColumnMapping("SlideCreationType", "SlideCreationType"), New System.Data.Common.DataColumnMapping("NewBusiness", "NewBusiness"), New System.Data.Common.DataColumnMapping("NilValueProject", "NilValueProject"), New System.Data.Common.DataColumnMapping("PricedInPairs", "PricedInPairs"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("PaymentTermsID", "PaymentTermsID"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("CurrencyId", "CurrencyId"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProjectDescription", "ProjectDescription"), New System.Data.Common.DataColumnMapping("AccuridePartNumberIssue", "AccuridePartNumberIssue"), New System.Data.Common.DataColumnMapping("EngineerID", "EngineerID"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId"), New System.Data.Common.DataColumnMapping("SlideType", "SlideType"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("CurrencyDate", "CurrencyDate"), New System.Data.Common.DataColumnMapping("PartNumber", "PartNumber"), New System.Data.Common.DataColumnMapping("PlatingDescription", "PlatingDescription"), New System.Data.Common.DataColumnMapping("PackingTypeDescription", "PackingTypeDescription"), New System.Data.Common.DataColumnMapping("Competitor", "Competitor"), New System.Data.Common.DataColumnMapping("CurrencyDescription", "CurrencyDescription"), New System.Data.Common.DataColumnMapping("DeliveryTermsDescription", "DeliveryTermsDescription"), New System.Data.Common.DataColumnMapping("PaymentTermsDescription", "PaymentTermsDescription"), New System.Data.Common.DataColumnMapping("AnticipatedSuccessDescription", "AnticipatedSuccessDescription"), New System.Data.Common.DataColumnMapping("SlideTypeDescription", "SlideTypeDescription"), New System.Data.Common.DataColumnMapping("StatusDescription", "StatusDescription"), New System.Data.Common.DataColumnMapping("Engineer", "Engineer"), New System.Data.Common.DataColumnMapping("SalesManager", "SalesManager"), New System.Data.Common.DataColumnMapping("Initiator", "Initiator"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("Contact1", "Contact1"), New System.Data.Common.DataColumnMapping("Contact2", "Contact2")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Sales"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "ps_Update_Sales"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ExistingSlide", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ExistingSlide", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnnualQuantity", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "AnnualQuantity", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "BatchSize", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "ProductLength", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductLife", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "ProductLife", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadRating", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "LoadRating", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProductTravel", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "ProductTravel", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CompetitorID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CompetitorID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SideSpace", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "SideSpace", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SlideThickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "SlideThickness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InstallWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, True, CType(15, Byte), CType(0, Byte), "InstallWidth", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PackingTypeId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@QuoteNumber", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "QuoteNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerPartNumber", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerPartNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SalesManagerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "SalesManagerId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@InitiatorID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "InitiatorID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Contact1ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Contact1ID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Contact2ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Contact2ID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Issue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Issue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "SlideId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PlatingID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PlatingID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@BBUs", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "BBUs", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AgentCommision", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(4, Byte), "AgentCommision", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AnticipatedSuccessId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AnticipatedSuccessId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerPayForTooling", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerPayForTooling", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CustomerDrawingsAvailable", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerDrawingsAvailable", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SlideCreationType", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, True, CType(3, Byte), CType(0, Byte), "SlideCreationType", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NewBusiness", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "NewBusiness", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@NilValueProject", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "NilValueProject", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PricedInPairs", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "PricedInPairs", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "TargetMax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "TargetMin", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PaymentTermsID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "CurrencyId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccuridePartNumber", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "AccuridePartNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectDescription", System.Data.SqlDbType.VarChar, 2500, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ProjectDescription", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccuridePartNumberIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "AccuridePartNumberIssue", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EngineerID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "EngineerID", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectMasterId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SlideType", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "SlideType", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ExistingSlide", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ExistingSlide",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AnnualQuantity", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"AnnualQuantity",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BatchSize", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"BatchSize",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductLength", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"ProductLength",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductLife", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"ProductLife",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LoadRating", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"LoadRating",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProductTravel", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"ProductTravel",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CompetitorID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CompetitorID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SideSpace", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"SideSpace",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SlideThickness", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"SlideThickness",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_InstallWidth", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input,True, CType(15, Byte), CType(0, Byte),"InstallWidth",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PackingTypeId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PackingTypeId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_QuoteNumber", System.Data.SqlDbType.Char, 20, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"QuoteNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerPartNumber", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CustomerPartNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SalesManagerId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"SalesManagerId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_InitiatorID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"InitiatorID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "CustomerID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Contact1ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Contact1ID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Contact2ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Contact2ID", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Issue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"Issue",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SlideId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"SlideId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PlatingID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PlatingID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_BBUs", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"BBUs",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AgentCommision", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AgentCommision",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AnticipatedSuccessId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"AnticipatedSuccessId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerPayForTooling", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CustomerPayForTooling",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CustomerDrawingsAvailable", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"CustomerDrawingsAvailable",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SlideCreationType", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input,True, CType(3, Byte), CType(0, Byte),"SlideCreationType",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NewBusiness", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"NewBusiness",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_NilValueProject", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"NilValueProject",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PricedInPairs", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"PricedInPairs",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMax",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_TargetMin", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"TargetMin",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PaymentTermsID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PaymentTermsID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_CurrencyId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"CurrencyId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccuridePartNumber", System.Data.SqlDbType.Char, 50, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"AccuridePartNumber",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectDescription", System.Data.SqlDbType.VarChar, 2500, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"ProjectDescription",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccuridePartNumberIssue", System.Data.SqlDbType.Char, 1, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"AccuridePartNumberIssue",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EngineerID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"EngineerID",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectMasterId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SlideType", System.Data.SqlDbType.Char, 10, System.Data.ParameterDirection.Input,True, CType(0, Byte), CType(0, Byte),"SlideType",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
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
            Return "tProject"
        End Get
    End Property

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet
    '    'Dim x As DataSet = LoadData(New Object() {46540, 67, 1})
    '    'x.Tables(0).Rows(0).Item("SalesManagerId") = 12
    '    Return LoadData(New Object() {46540, 67, 1})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
   Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectNumber").Value = pudtParams(0)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = pudtParams(1)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pudtParams(2)
        End If
        Return LoadDataSet(SqlDataAdapter1)
    End Function



    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <WebMethod(Description:="Sets the flag in the ProjectStageLink table for the required project.")> _
    Public Function UpdateAcknowledgedBy(ByVal pintUserId As Int32, ByVal pintProjectId As Int32) As Int32
        Dim intReturn As Int32 = 0
        Try
            SqlConnection1.Open()
            Dim udtSqlCommand As SqlCommand = New SqlCommand("ps_UpdateAcknowledgedByForProject", SqlConnection1)
            With udtSqlCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@ProjectId", pintProjectId)
                .Parameters.Add("@UserId", pintUserId)
                Try
                    intReturn = .ExecuteNonQuery()
                    SqlConnection1.Close()
                Catch
                    intReturn = 0
                End Try
            End With
            udtSqlCommand = Nothing
            intReturn = 0
        Catch
            intReturn = 0
        End Try
        Return intReturn
    End Function

    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function

	Public Overrides Sub AssignFields()
		With Fields
			.Add("ExistingSlide", New FieldInfoStructure("ExistingSlide", 20, True, False))
			.Add("AnnualQuantity", New FieldInfoStructure("AnnualQuantity", 53, True, False))
			.Add("BatchSize", New FieldInfoStructure("BatchSize", 53, True, False))
			.Add("ProductLength", New FieldInfoStructure("ProductLength", 53, True, False))
			.Add("ProductLife", New FieldInfoStructure("ProductLife", 53, True, False))
			.Add("LoadRating", New FieldInfoStructure("LoadRating", 53, True, False))
			.Add("ProductTravel", New FieldInfoStructure("ProductTravel", 53, True, False))
			.Add("CompetitorID", New FieldInfoStructure("CompetitorID", 10, True, False))
			.Add("SideSpace", New FieldInfoStructure("SideSpace", 53, True, False))
			.Add("SlideThickness", New FieldInfoStructure("SlideThickness", 53, True, False))
			.Add("InstallWidth", New FieldInfoStructure("InstallWidth", 53, True, False))
			.Add("PackingTypeId", New FieldInfoStructure("PackingTypeId", 10, True, False, , 1))
			.Add("QuoteNumber", New FieldInfoStructure("QuoteNumber", 20, True, False))
			.Add("CustomerPartNumber", New FieldInfoStructure("CustomerPartNumber", 50, True, False))
			.Add("SalesManagerId", New FieldInfoStructure("SalesManagerId", 10, True, False))
			.Add("InitiatorID", New FieldInfoStructure("InitiatorID", 10, True, False))
            .Add("CustomerID", New FieldInfoStructure("CustomerID", 10, True, False))
            .Add("Contact1ID", New FieldInfoStructure("Contact1ID", 10, True, False))
            .Add("Contact2ID", New FieldInfoStructure("Contact2ID", 10, True, False))
			.Add("Issue", New FieldInfoStructure("Issue", 1, False, False, , "A"))
			.Add("SlideId", New FieldInfoStructure("SlideId", 10, True, False))
			.Add("PlatingID", New FieldInfoStructure("PlatingID", 10, True, False))
			.Add("BBUs", New FieldInfoStructure("BBUs", 3, True, False))
            .Add("AgentCommision", New FieldInfoStructure("AgentCommision", 12, True, False, , 0))
			.Add("AnticipatedSuccessId", New FieldInfoStructure("AnticipatedSuccessId", 10, True, False))
			.Add("CustomerPayForTooling", New FieldInfoStructure("CustomerPayForTooling", 1, True, False, , False))
			.Add("CustomerDrawingsAvailable", New FieldInfoStructure("CustomerDrawingsAvailable", 1, True, False, , False))
			.Add("SlideCreationType", New FieldInfoStructure("SlideCreationType", 3, True, False))
			.Add("NewBusiness", New FieldInfoStructure("NewBusiness", 1, True, False, , False))
			.Add("NilValueProject", New FieldInfoStructure("NilValueProject", 1, True, False, , False))
			.Add("PricedInPairs", New FieldInfoStructure("PricedInPairs", 1, True, False, , False))
			.Add("CurrencyDate", New FieldInfoStructure("CurrencyDate", 23, False, False, , "GETDATE"))
			.Add("PaymentTermsID", New FieldInfoStructure("PaymentTermsID", 10, True, False))
			.Add("DeliveryTermsId", New FieldInfoStructure("DeliveryTermsId", 10, True, False))
			.Add("CurrencyId", New FieldInfoStructure("CurrencyId", 10, False, False))
			.Add("AccuridePartNumber", New FieldInfoStructure("AccuridePartNumber", 50, True, False, , "Unknown"))
			.Add("ProjectDescription", New FieldInfoStructure("ProjectDescription", 2500, True, False))
			.Add("AccuridePartNumberIssue", New FieldInfoStructure("AccuridePartNumberIssue", 1, True, False))
			.Add("EngineerID", New FieldInfoStructure("EngineerID", 10, True, False))
			.Add("ProjectMasterId", New FieldInfoStructure("ProjectMasterId", 10, False, False))
			.Add("SlideType", New FieldInfoStructure("SlideType", 10, True, False))
			.Add("ProjectId", New FieldInfoStructure("ProjectId", 10, False, False))
			.Add("PartNumber", New FieldInfoStructure("PartNumber", 15, False, False))
			.Add("Competitor", New FieldInfoStructure("Competitor", 50, True, False))
			.Add("CurrencyDescription", New FieldInfoStructure("CurrencyDescription", 100, False, False))
			.Add("DeliveryTermsDescription", New FieldInfoStructure("DeliveryTermsDescription", 250, True, False))
			.Add("PaymentTermsDescription", New FieldInfoStructure("PaymentTermsDescription", 100, False, False))
			.Add("AnticipatedSuccessDescription", New FieldInfoStructure("AnticipatedSuccessDescription", 30, False, False))
			.Add("Engineer", New FieldInfoStructure("Engineer", 50, True, False))
			.Add("CompanyName", New FieldInfoStructure("CompanyName", 100, True, False))
			.Add("Contact1", New FieldInfoStructure("Contact1", 100, True, False))
		End With
	End Sub
End Class
