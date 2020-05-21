Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/QuoteService")> _
Public Class QuoteService
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
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter

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
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand2
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "GetProjectsForProjectMaster", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectNumber", "ProjectNumber"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId")})})
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Quote", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("QuoteId", "QuoteId"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId"), New System.Data.Common.DataColumnMapping("ContactId", "ContactId"), New System.Data.Common.DataColumnMapping("QuoteDate", "QuoteDate"), New System.Data.Common.DataColumnMapping("EnquiryNumber", "EnquiryNumber"), New System.Data.Common.DataColumnMapping("DeliveryTermsId", "DeliveryTermsId"), New System.Data.Common.DataColumnMapping("Tax", "Tax"), New System.Data.Common.DataColumnMapping("PaymentTermsId", "PaymentTermsId"), New System.Data.Common.DataColumnMapping("Validity", "Validity"), New System.Data.Common.DataColumnMapping("LeadTime", "LeadTime"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("CustomerId", "CustomerId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("ToolCost", "ToolCost")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DefaultCustomerId", "DefaultCustomerId"), New System.Data.Common.DataColumnMapping("DefaultCompanyName", "DefaultCompanyName"), New System.Data.Common.DataColumnMapping("DefaultDeliveryTermsId", "DefaultDeliveryTermsId"), New System.Data.Common.DataColumnMapping("DefaultPaymentTermsId", "DefaultPaymentTermsId"), New System.Data.Common.DataColumnMapping("DefaultQuoteDate", "DefaultQuoteDate")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectNumber", "ProjectNumber"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_Quote]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@QuoteId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_Quote]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectMasterId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactId", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ContactId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@QuoteDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "QuoteDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EnquiryNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "EnquiryNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Tax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "Tax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Validity", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Validity", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LeadTime", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "LeadTime", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_Quote]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "ProjectMasterId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ContactId", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "ContactId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@QuoteDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "QuoteDate", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@EnquiryNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "EnquiryNumber", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Tax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, False, CType(19, Byte), CType(0, Byte), "Tax", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Validity", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Validity", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LeadTime", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "LeadTime", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"ProjectMasterId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactId", System.Data.SqlDbType.VarChar, 100,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"ContactId",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_QuoteDate", System.Data.SqlDbType.DateTime, 8,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"QuoteDate",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EnquiryNumber", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"EnquiryNumber",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"DeliveryTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Tax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input,True, CType(19, Byte), CType(0, Byte),"Tax",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"PaymentTermsId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Validity", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Validity",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LeadTime", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"LeadTime",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_QuoteId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"QuoteId",System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_Quote]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "ProjectMasterId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_ContactId", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "ContactId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_QuoteDate", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "QuoteDate", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_EnquiryNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "EnquiryNumber", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_DeliveryTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "DeliveryTermsId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Tax", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, True, CType(19, Byte), CType(0, Byte), "Tax", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_PaymentTermsId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "PaymentTermsId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Validity", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Validity", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_LeadTime", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "LeadTime", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_QuoteId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "QuoteId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "[GetProjectsForProjectMaster]"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectMasterId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@QuoteId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadReportData(ByVal accurideLocationId As Int32, ByVal userId As Int32, ByVal projectMasterId As Int32, ByVal quoteId As Int32) As Boolean
        Dim x As New DataSet
        Dim blnThereIsData As Boolean = False

        With SqlDataAdapter1
            .SelectCommand.Parameters("@AccurideLocationId").Value = accurideLocationId
            .SelectCommand.Parameters("@QuoteId").Value = quoteId
            .SelectCommand.Parameters("@ProjectMasterId").Value = projectMasterId
            .SelectCommand.Parameters("@UserId").Value = userId
            .Fill(x)
        End With
        If Not x Is Nothing AndAlso _
           x.Tables.Count > 0 Then
            blnThereIsData = x.Tables(0).Rows.Count > 0
        End If
        Return blnThereIsData
    End Function

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tQuote"
        End Get
    End Property

    <System.Web.Services.WebMethodAttribute()> _
    Public Function test() As System.Data.DataSet
        Return LoadData(New Object() {14, 66})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectMasterId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@UserId").Value = CType(pudtParams(1), Int32)
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    '<System.Web.Services.WebMethodAttribute()> _
    'Public Function Test() As DataSet
    '    Return LoadData(New Object() {1011, 66})
    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        If Changes.Tables(0).Rows(0).Item("QuoteDate").ToString.Trim.Length = 0 Then Changes.Tables(0).Rows(0).Item("QuoteDate") = Convert.DBNull '"1/1/01"
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
        'Return LoadData(New Object() {pintUniqueId, 0, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function GetProjectsForQuote(ByVal pintQuoteId As Int32) As System.Data.DataSet
        Dim x As New DataSet
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectMasterId").Value = 0
        SqlDataAdapter2.SelectCommand.Parameters("@QuoteId").Value = pintQuoteId
        SqlDataAdapter2.Fill(x)
        Return x
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateProjectsForQuote(ByVal pintQuoteId As Int32, ByVal pudtNewProjectIdList() As Int32) As Boolean
        Dim intProjectId As Int32
        Try
            SqlConnection1.Open()
        Catch
            Return False
        Finally
            Dim udtOldProjectIds As DataSet = GetProjectsForQuote(pintQuoteId)
            Dim udtSQL As New SqlCommand("UpdateProjectsForQuote", Me.SqlConnection1)

            With udtSQL
                .CommandType = CommandType.StoredProcedure

                .Parameters.Add("@QuoteId", pintQuoteId)
                .Parameters.Add("@ProjectId", 0)
                .Parameters.Add("@Type", 0)
                If pudtNewProjectIdList Is Nothing Then
                    .Parameters("@ProjectId").Value = 0
                    .Parameters("@Type").Value = 1 'tells the sp to delete all for my quote
                    .ExecuteNonQuery()
                Else
                    'see if my project allready exists
                    Dim udtRow As DataRow
                    Dim intloop As Int32
                    Dim blnFound As Boolean
                    For Each udtRow In udtOldProjectIds.Tables(0).Rows
                        intProjectId = CType(udtRow.Item("ProjectId").ToString, Int32)
                        blnFound = False
                        For intloop = pudtNewProjectIdList.GetLowerBound(0) To pudtNewProjectIdList.GetUpperBound(0)
                            If intProjectId = pudtNewProjectIdList(intloop) Then
                                blnFound = True
                                Exit For
                            End If
                        Next intloop
                        If blnFound Then
                            'do nothing, as we need this one
                        Else
                            'it hasn't been found so we must remove it
                            .Parameters("@ProjectId").Value = intProjectId
                            .Parameters("@Type").Value = 2
                            .ExecuteNonQuery()
                        End If
                    Next udtRow
                    'loop through new projects seeing if they need adding
                    For intloop = pudtNewProjectIdList.GetLowerBound(0) To pudtNewProjectIdList.GetUpperBound(0)
                        blnFound = False
                        For Each udtRow In udtOldProjectIds.Tables(0).Rows
                            If CType(udtRow.Item("ProjectId").ToString, Int32) = pudtNewProjectIdList(intloop) Then
                                blnFound = True
                                Exit For
                            End If
                        Next udtRow
                        If blnFound Then
                            'do nothing, as we need this one
                        Else
                            'it hasn't been found so we must remove it
                            .Parameters("@ProjectId").Value = pudtNewProjectIdList(intloop)
                            .Parameters("@Type").Value = 3
                            .ExecuteNonQuery()
                        End If
                    Next intloop

                    udtRow = Nothing
                End If
            End With
            '        Catch e As Exception
            '            Return e.Message 'False
        End Try
        Return True
    End Function


    Public Overrides Sub AssignFields()
        With Fields
            .Add("QuoteId", New FieldInfoStructure("QuoteId", 10, False, False))
            .Add("ProjectMasterId", New FieldInfoStructure("ProjectMasterId", 10, False, False))
            .Add("ContactId", New FieldInfoStructure("ContactId", 100, True, False))
            .Add("QuoteDate", New FieldInfoStructure("QuoteDate", 23, False, False, , "GETDATE"))
            .Add("EnquiryNumber", New FieldInfoStructure("EnquiryNumber", 50, False, False))
            .Add("DeliveryTermsId", New FieldInfoStructure("DeliveryTermsId", 10, True, False))
            .Add("Tax", New FieldInfoStructure("Tax", 19, False, False))
            .Add("PaymentTermsId", New FieldInfoStructure("PaymentTermsId", 10, False, False))
            .Add("Validity", New FieldInfoStructure("Validity", 50, False, False))
            .Add("LeadTime", New FieldInfoStructure("LeadTime", 20, False, False))
            .Add("CustomerID", New FieldInfoStructure("CustomerID", 100, True, False))
            .Add("ContactName", New FieldInfoStructure("ContactName", 100, True, False))
            .Add("Description_Alternative", New FieldInfoStructure("Description_Alternative", 250, True, False))
            .Add("Description_Alternative1", New FieldInfoStructure("Description_Alternative1", 100, False, False))
            .Add("ProjectId", New FieldInfoStructure("ProjectId", 10, False, False))
        End With
    End Sub
End Class
