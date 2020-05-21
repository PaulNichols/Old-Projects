Imports System.Web.Services
Public Class TestService1
    Inherits ISDWebService

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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Sales", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AnnualQuantity", "AnnualQuantity"), New System.Data.Common.DataColumnMapping("BatchSize", "BatchSize"), New System.Data.Common.DataColumnMapping("ProductLength", "ProductLength"), New System.Data.Common.DataColumnMapping("ProductLife", "ProductLife"), New System.Data.Common.DataColumnMapping("LoadRating", "LoadRating"), New System.Data.Common.DataColumnMapping("ProductTravel", "ProductTravel"), New System.Data.Common.DataColumnMapping("CompetitorID", "CompetitorID"), New System.Data.Common.DataColumnMapping("SideSpace", "SideSpace"), New System.Data.Common.DataColumnMapping("SlideThickness", "SlideThickness"), New System.Data.Common.DataColumnMapping("InstallWidth", "InstallWidth"), New System.Data.Common.DataColumnMapping("PackingTypeId", "PackingTypeId"), New System.Data.Common.DataColumnMapping("SamplesRequiredDate", "SamplesRequiredDate"), New System.Data.Common.DataColumnMapping("SamplesRequested", "SamplesRequested"), New System.Data.Common.DataColumnMapping("SamplesCompleted", "SamplesCompleted"), New System.Data.Common.DataColumnMapping("SamplesCompletedDate", "SamplesCompletedDate"), New System.Data.Common.DataColumnMapping("SamplesCompletedByID", "SamplesCompletedByID"), New System.Data.Common.DataColumnMapping("DrawingScheduledDate", "DrawingScheduledDate"), New System.Data.Common.DataColumnMapping("DrawingScheduledByID", "DrawingScheduledByID"), New System.Data.Common.DataColumnMapping("DrawingRequiredDate", "DrawingRequiredDate"), New System.Data.Common.DataColumnMapping("DrawingRequested", "DrawingRequested"), New System.Data.Common.DataColumnMapping("DrawingCompletedDate", "DrawingCompletedDate"), New System.Data.Common.DataColumnMapping("DrawingCompletedByID", "DrawingCompletedByID"), New System.Data.Common.DataColumnMapping("DrawingCompleted", "DrawingCompleted"), New System.Data.Common.DataColumnMapping("CostingScheduledDate", "CostingScheduledDate"), New System.Data.Common.DataColumnMapping("CostingScheduledByID", "CostingScheduledByID"), New System.Data.Common.DataColumnMapping("CostingRequiredDate", "CostingRequiredDate"), New System.Data.Common.DataColumnMapping("CostingRequested", "CostingRequested"), New System.Data.Common.DataColumnMapping("CostingCompletedDate", "CostingCompletedDate"), New System.Data.Common.DataColumnMapping("CostingCompletedByID", "CostingCompletedByID"), New System.Data.Common.DataColumnMapping("CostingCompleted", "CostingCompleted"), New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("QuoteNumber", "QuoteNumber"), New System.Data.Common.DataColumnMapping("CustomerPartNumber", "CustomerPartNumber"), New System.Data.Common.DataColumnMapping("SalesManagerid", "SalesManagerid"), New System.Data.Common.DataColumnMapping("InitiatorID", "InitiatorID"), New System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"), New System.Data.Common.DataColumnMapping("Contact1ID", "Contact1ID"), New System.Data.Common.DataColumnMapping("Contact2ID", "Contact2ID"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("SlideId", "SlideId"), New System.Data.Common.DataColumnMapping("PlatingID", "PlatingID"), New System.Data.Common.DataColumnMapping("BBUs", "BBUs"), New System.Data.Common.DataColumnMapping("AgentCommision", "AgentCommision"), New System.Data.Common.DataColumnMapping("AnticipatedSuccessId", "AnticipatedSuccessId"), New System.Data.Common.DataColumnMapping("CustomerPayForTooling", "CustomerPayForTooling"), New System.Data.Common.DataColumnMapping("CustomerDrawingsAvailable", "CustomerDrawingsAvailable"), New System.Data.Common.DataColumnMapping("SlideCreationType", "SlideCreationType"), New System.Data.Common.DataColumnMapping("NewBusiness", "NewBusiness"), New System.Data.Common.DataColumnMapping("NilValueProject", "NilValueProject"), New System.Data.Common.DataColumnMapping("PricedInPairs", "PricedInPairs"), New System.Data.Common.DataColumnMapping("TargetMax", "TargetMax"), New System.Data.Common.DataColumnMapping("TargetMin", "TargetMin"), New System.Data.Common.DataColumnMapping("PaymentTermsID", "PaymentTermsID"), New System.Data.Common.DataColumnMapping("ExchangeRate", "ExchangeRate"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("CurrencyId", "CurrencyId"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("ProjectDescription", "ProjectDescription"), New System.Data.Common.DataColumnMapping("AccuridePartNumberIssue", "AccuridePartNumberIssue"), New System.Data.Common.DataColumnMapping("ExistingSlideNumber", "ExistingSlideNumber"), New System.Data.Common.DataColumnMapping("EngineerID", "EngineerID"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId"), New System.Data.Common.DataColumnMapping("SlideType", "SlideType"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Sales"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "data source=uksql3;initial catalog=Pits;integrated security=SSPI;persist security" & _
        " info=False;workstation id=STEVEN;packet size=4096"

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    '<WebMethod(), System.Web.Services.Protocols.SoapHeader("authHdr", Direction:=System.Web.Services.Protocols.SoapHeaderDirection.In, Required:=True)> _
    'Public Function GetSales() As DataSet
    '    If IsRequestOK() Then
    '        Dim x As New DataSet()
    '        SqlDataAdapter1.SelectCommand.Parameters("@ProjectNumber").Value = 15000
    '        SqlDataAdapter1.Fill(x)
    '        Return x
    '    Else
    '        Err.Raise(1)
    '    End If
    'End Function

    '<WebMethod()> _
    'Public Function Hello() As String
    '    Return "Hello"
    'End Function

End Class




