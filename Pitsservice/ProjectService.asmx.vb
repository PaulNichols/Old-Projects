Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/ProjectService")> _
Public Class ProjectService
    Inherits EPS_Service
    Implements IEPS_Service_Data
#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter3 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter1 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter4 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter5 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter6 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter7 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter8 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand8 As System.Data.SqlClient.SqlCommand

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter3 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter7 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter6 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter5 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter4 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter8 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand5 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand6 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand8 = New System.Data.SqlClient.SqlCommand()
        '
        'SqlDataAdapter3
        '
        Me.SqlDataAdapter3.SelectCommand = Me.SqlSelectCommand3
        Me.SqlDataAdapter3.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_ProjectStageLink", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectEventLinkId", "ProjectEventLinkId"), New System.Data.Common.DataColumnMapping("EventId", "EventId"), New System.Data.Common.DataColumnMapping("OwnerId", "OwnerId"), New System.Data.Common.DataColumnMapping("AcknowledgedByNewOwner", "AcknowledgedByNewOwner")})})
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand2
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_ProjNumbers", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectNo", "ProjectNo")})})
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Project", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Title", "Title"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("PositionName", "PositionName"), New System.Data.Common.DataColumnMapping("ContactNumber", "ContactNumber"), New System.Data.Common.DataColumnMapping("Email", "Email")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("CustomerPartNumber", "CustomerPartNumber"), New System.Data.Common.DataColumnMapping("GroupLetter", "GroupLetter"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId"), New System.Data.Common.DataColumnMapping("SalesManagerSigned", "SalesManagerSigned"), New System.Data.Common.DataColumnMapping("PartNumber", "PartNumber"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"), New System.Data.Common.DataColumnMapping("StatusDescription", "StatusDescription"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("ProjectStage", "ProjectStage")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ProjectMasterGroup", "ProjectMasterGroup"), New System.Data.Common.DataColumnMapping("ApprovedBatch", "ApprovedBatch"), New System.Data.Common.DataColumnMapping("CustomerPartNumber", "CustomerPartNumber"), New System.Data.Common.DataColumnMapping("EngineerID", "EngineerID"), New System.Data.Common.DataColumnMapping("GroupLetter", "GroupLetter"), New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("PlatingId", "PlatingId"), New System.Data.Common.DataColumnMapping("ProjectId", "ProjectId"), New System.Data.Common.DataColumnMapping("ProjectMasterId", "ProjectMasterId"), New System.Data.Common.DataColumnMapping("SalesManagerSigned", "SalesManagerSigned"), New System.Data.Common.DataColumnMapping("SlideId", "SlideId"), New System.Data.Common.DataColumnMapping("PartNumber", "PartNumber"), New System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"), New System.Data.Common.DataColumnMapping("AccuridePartNumber", "AccuridePartNumber"), New System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"), New System.Data.Common.DataColumnMapping("StatusDescription", "StatusDescription"), New System.Data.Common.DataColumnMapping("Description", "Description")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        '
        'SqlDataAdapter7
        '
        Me.SqlDataAdapter7.SelectCommand = Me.SqlSelectCommand7
        Me.SqlDataAdapter7.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Project_Revisions", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Issue", "Issue"), New System.Data.Common.DataColumnMapping("ProjId", "ProjId")})})
        '
        'SqlDataAdapter6
        '
        Me.SqlDataAdapter6.SelectCommand = Me.SqlSelectCommand6
        Me.SqlDataAdapter6.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_GetProjectEventGroups", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GroupingId", "GroupingId")})})
        '
        'SqlDataAdapter5
        '
        Me.SqlDataAdapter5.SelectCommand = Me.SqlSelectCommand5
        Me.SqlDataAdapter5.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Project_OwnerDetails", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("ContactNuimber", "ContactNuimber"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("PositionName", "PositionName")})})
        '
        'SqlDataAdapter4
        '
        Me.SqlDataAdapter4.SelectCommand = Me.SqlSelectCommand4
        Me.SqlDataAdapter4.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Project_ContactDetails", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Title", "Title"), New System.Data.Common.DataColumnMapping("ContactName", "ContactName"), New System.Data.Common.DataColumnMapping("PositionName", "PositionName"), New System.Data.Common.DataColumnMapping("ContactNumber", "ContactNumber"), New System.Data.Common.DataColumnMapping("Email", "Email")})})
        '
        'SqlDataAdapter8
        '
        Me.SqlDataAdapter8.SelectCommand = Me.SqlSelectCommand8
        Me.SqlDataAdapter8.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "CreateExcel", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("InitiatorId", "InitiatorId"), New System.Data.Common.DataColumnMapping("Project Number", "Project Number"), New System.Data.Common.DataColumnMapping("StartDate", "StartDate"), New System.Data.Common.DataColumnMapping("Company Name", "Company Name"), New System.Data.Common.DataColumnMapping("Country", "Country"), New System.Data.Common.DataColumnMapping("Market Code", "Market Code"), New System.Data.Common.DataColumnMapping("Slide Type", "Slide Type"), New System.Data.Common.DataColumnMapping("Status", "Status"), New System.Data.Common.DataColumnMapping("Reason For Closure", "Reason For Closure"), New System.Data.Common.DataColumnMapping("Latest Event", "Latest Event"), New System.Data.Common.DataColumnMapping("Event Date", "Event Date"), New System.Data.Common.DataColumnMapping("Costed Date", "Costed Date"), New System.Data.Common.DataColumnMapping("Quantity", "Quantity"), New System.Data.Common.DataColumnMapping("BBU PA", "BBU PA"), New System.Data.Common.DataColumnMapping("Taget Price", "Taget Price"), New System.Data.Common.DataColumnMapping("Review Team Price", "Review Team Price"), New System.Data.Common.DataColumnMapping("Review Approved By", "Review Approved By"), New System.Data.Common.DataColumnMapping("Nul Value", "Nul Value"), New System.Data.Common.DataColumnMapping("Currency", "Currency"), New System.Data.Common.DataColumnMapping("Engineer", "Engineer"), New System.Data.Common.DataColumnMapping("No Of Tools", "No Of Tools"), New System.Data.Common.DataColumnMapping("Tool Cost", "Tool Cost"), New System.Data.Common.DataColumnMapping("Amortized", "Amortized"), New System.Data.Common.DataColumnMapping("Last Comment", "Last Comment")})})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strConnectionString
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "ps_Select_ProjNumbers"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "ps_Select_ProjectStageLink"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Project"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "ps_Select_Project_ContactDetails"
        Me.SqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand4.Connection = Me.SqlConnection1
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand4.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand5
        '
        Me.SqlSelectCommand5.CommandText = "ps_Select_Project_OwnerDetails"
        Me.SqlSelectCommand5.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand5.Connection = Me.SqlConnection1
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand5.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand6
        '
        Me.SqlSelectCommand6.CommandText = "ps_GetProjectEventGroups"
        Me.SqlSelectCommand6.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand6.Connection = Me.SqlConnection1
        Me.SqlSelectCommand6.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand6.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand7
        '
        Me.SqlSelectCommand7.CommandText = "ps_Select_Project_Revisions"
        Me.SqlSelectCommand7.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand7.Connection = Me.SqlConnection1
        Me.SqlSelectCommand7.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand7.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand8
        '
        Me.SqlSelectCommand8.CommandText = "CreateExcel"
        Me.SqlSelectCommand8.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand8.Connection = Me.SqlConnection1
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand8.Parameters.Add(New System.Data.SqlClient.SqlParameter("@GetMilestones", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))

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


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = CType(pudtParams(0), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectNumber").Value = CType(pudtParams(1), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
         Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
  Public Function test() As DataSet
        Return LoadData(New Object() {1, 46797})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        'Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <WebMethod()> Public Function GetProjNumbers() As DataSet
        Dim x As New DataSet()
        SqlDataAdapter2.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function GetProjStage(ByVal pintProjectId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter3.SelectCommand.Parameters("@Projectid").Value = pintProjectId
        SqlDataAdapter3.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function GetSalesExcelData_Test(ByVal pintAccurideLocationId As Int32, ByVal pintUserId As Int32, ByVal pblnGetMileStones As Boolean) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter8.SelectCommand.Parameters("@AccurideLocationid").Value = pintAccurideLocationId
        SqlDataAdapter8.SelectCommand.Parameters("@UserId").Value = pintUserId
        SqlDataAdapter8.SelectCommand.Parameters("@GetMilestones").Value = pblnGetMileStones
        SqlDataAdapter8.SelectCommand.CommandTimeout = 200
        SqlDataAdapter8.Fill(x)
        If Not x Is Nothing Then
            x.Tables(0).TableName = "Parent"
            If x.Tables.Count > 1 Then
                x.Tables(1).TableName = "Child"
            End If
        End If
        Return x
    End Function

    <WebMethod()> Public Function GetSalesExcelData(ByVal pintAccurideLocationId As Int32, ByVal pintUserId As Int32, ByVal pblnGetMileStones As Boolean) As String
        Dim x As New DataSet()
        SqlDataAdapter8.SelectCommand.Parameters("@AccurideLocationid").Value = pintAccurideLocationId
        SqlDataAdapter8.SelectCommand.Parameters("@UserId").Value = pintUserId
        SqlDataAdapter8.SelectCommand.Parameters("@GetMilestones").Value = pblnGetMileStones
        SqlDataAdapter8.SelectCommand.CommandTimeout = 200
        SqlDataAdapter8.Fill(x)
        If Not x Is Nothing Then
            x.Tables(0).TableName = "Parent"
            If x.Tables.Count > 1 Then
                x.Tables(1).TableName = "Child"
                Dim udtRelation As New DataRelation("Join", x.Tables("Parent").Columns("ProjectId"), x.Tables("Child").Columns("ProjectId"))
                Try
                    x.Relations.Add(udtRelation)
                Catch e As Exception
                    Return e.message
                End Try

                udtRelation = Nothing
            End If
        End If
        Dim strData As New System.Text.StringBuilder("")
        Dim strHeader As New System.Text.StringBuilder("")
        Dim blnRan As Boolean = False
        Dim Q As String = """"
        Try
            If Not x Is Nothing Then
                If x.Tables.Count > 0 Then
                    If x.Tables(0).Rows.Count > 0 Then
                        Dim udtRow As DataRow
                        Dim udtColumn As DataColumn
                        Dim udtChildRow As DataRow
                        'Add Headers
                        For Each udtColumn In x.Tables("Parent").Columns
                            If Not udtColumn.ColumnName.ToString = "ProjectId" Then
                                strHeader.Append(Q & udtColumn.ColumnName.ToString & Q & ",")
                            End If
                        Next udtColumn

                        'Add rows
                        For Each udtRow In x.Tables("Parent").Rows
                            For Each udtColumn In x.Tables("Parent").Columns
                                If Not udtColumn.ColumnName.ToString = "ProjectId" Then
                                    strData.Append(Q & udtRow.Item(udtColumn).ToString & Q & ",")
                                End If
                            Next udtColumn
                            If pblnGetMileStones Then
                                'Add Child Rows
                                For Each udtChildRow In udtRow.GetChildRows("Join")
                                    For Each udtColumn In x.Tables("Child").Columns
                                        'AddMileStone Headers
                                        If udtColumn.ColumnName.ToString <> "ProjectId" Then
                                            If Not blnRan Then
                                                strHeader.Append(Q & udtColumn.ColumnName.ToString & Q & ",")
                                            End If
                                            strData.Append(Q & udtChildRow.Item(udtColumn).ToString & Q & ",")
                                        End If
                                    Next udtColumn
                                Next udtChildRow
                                blnRan = True
                            End If
                            strData.Append(Environment.NewLine)
                        Next udtRow
                        udtRow = Nothing
                        strHeader.Append(Environment.NewLine)
                    End If
                End If
            End If
        Catch e As Exception
            strData = New System.Text.StringBuilder("")
        End Try
        x = Nothing
        Return strHeader.ToString & strData.ToString
    End Function

    <WebMethod()> Public Function GetProjectContacts(ByVal pintProjectId As Int32, ByVal pintLocationId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter4.SelectCommand.Parameters("@ProjectNumber").Value = pintProjectId
        SqlDataAdapter4.SelectCommand.Parameters("@AccurideLocationId").Value = pintLocationId
        SqlDataAdapter4.Fill(x)
        Return x
    End Function


    <WebMethod()> Public Function GetProjectRevisions(ByVal pintProjectId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter7.SelectCommand.Parameters("@ProjectNumber").Value = pintProjectId
        SqlDataAdapter7.Fill(x)
        Return x
    End Function


    <WebMethod()> Public Function GetProjectOwners(ByVal pintProjectId As Int32, ByVal pintLocationId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter5.SelectCommand.Parameters("@ProjectNumber").Value = pintProjectId
        SqlDataAdapter5.SelectCommand.Parameters("@AccurideLocationId").Value = pintLocationId
        SqlDataAdapter5.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function IsProjectInEngineering(ByVal pintProjectId As Int32) As Boolean
        Dim x As New DataSet()
        Dim strReturn As String = ""
        Dim blnInEngineering As Boolean = False

        SqlDataAdapter6.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter6.Fill(x)
        If Not x Is Nothing Then
            Dim udtDataRow As DataRow
            For Each udtDataRow In x.Tables(0).Rows
                blnInEngineering = (CType(udtDataRow.Item(0).ToString, Int32) = 2)
                If blnInEngineering Then
                    Exit For
                End If
            Next udtDataRow
            udtDataRow = Nothing
        End If
        x = Nothing
        Return blnInEngineering
    End Function

    <WebMethod()> Public Function IsInitiationFormComplete(ByVal projectId As Int32) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim SelectFunction As New SqlCommand("select dbo.IsInitiationFormComplete (" & projectId & ")", Me.SqlConnection1)
            ReturnValue = CType(SelectFunction.ExecuteScalar(), Boolean)
            SelectFunction = Nothing
        End Try
        Return ReturnValue
    End Function

    '<WebMethod()> Public Function IsInitiationFormComplete(ByVal projectId As Int32) As Boolean
    '    Dim ReturnValue As Boolean = False
    '    Try
    '        Me.SqlConnection1.Open()
    '    Catch
    '        Return ReturnValue
    '    Finally
    '        Dim SelectFunction As New SqlCommand("select dbo.StageFunctions_IsSalesDataComplete (" & projectId & ")", Me.SqlConnection1)
    '        ReturnValue = (CType(SelectFunction.ExecuteScalar(), Int32) = 0)
    '        SelectFunction = Nothing
    '    End Try
    '    Return ReturnValue
    'End Function

    <WebMethod()> Public Function IsUserInEngineering(ByVal userId As Int32) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim SelectFunction As New SqlCommand("select dbo.IsUserInEngineering (" & userId & ")", Me.SqlConnection1)
            ReturnValue = CType(SelectFunction.ExecuteScalar(), Boolean)
            SelectFunction = Nothing
        End Try
        Return ReturnValue
    End Function

    <System.Web.Services.WebMethodAttribute()> Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> Public Function GetProjectIdFromNumber(ByVal ProjectNumber As String) As Int32
        Dim intReturn As Int32 = 0
        Try
            SqlConnection1.Open()
        Catch
            Return 0
        Finally
            Dim udtSelect As New SqlCommand("GetProjectIdFromNumber", SqlConnection1)
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
                .Parameters.Add("@ProjectNumber", ProjectNumber.TrimEnd)
                Try
                    udtSelect.ExecuteNonQuery()
                    intReturn = CType(.Parameters("@RETURN_VALUE").Value, Int32)
                Catch
                    intReturn = 0
                End Try
            End With
            udtSelect = Nothing
        End Try
        Return intReturn
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("ProjectEventLinkId", New FieldInfoStructure("ProjectEventLinkId", 10, False, False))
            .Add("EventId", New FieldInfoStructure("EventId", 10, False, False))
            .Add("OwnerId", New FieldInfoStructure("OwnerId", 10, False, False))
            .Add("AcknowledgedByNewOwner", New FieldInfoStructure("AcknowledgedByNewOwner", 1, False, False, , False))
            .Add("CustomerPartNumber", New FieldInfoStructure("CustomerPartNumber", 50, True, False))
            .Add("GroupLetter", New FieldInfoStructure("GroupLetter", 1, False, False, , "A"))
            .Add("Issue", New FieldInfoStructure("Issue", 1, False, False, , "A"))
            .Add("ProjectId", New FieldInfoStructure("ProjectId", 10, False, False))
            .Add("ProjectMasterId", New FieldInfoStructure("ProjectMasterId", 10, False, False))
            .Add("SalesManagerSigned", New FieldInfoStructure("SalesManagerSigned", 1, True, False, , False))
            .Add("PartNumber", New FieldInfoStructure("PartNumber", 15, False, False))
            .Add("CompanyName", New FieldInfoStructure("CompanyName", 100, True, False))
            .Add("AccuridePartNumber", New FieldInfoStructure("AccuridePartNumber", 50, True, False, , "Unknown"))
            .Add("CustomerID", New FieldInfoStructure("CustomerID", 100, True, False))
            .Add("Description", New FieldInfoStructure("Description", 2500, True, False))
            .Add("ContactName", New FieldInfoStructure("ContactName", 100, True, False))
            .Add("ContactNumber", New FieldInfoStructure("ContactNumber", 30, True, False))
            .Add("ContactNuimber", New FieldInfoStructure("ContactNuimber", 30, True, False))
            .Add("Projid", New FieldInfoStructure("Projid", 10, False, False))
        End With
    End Sub
End Class
