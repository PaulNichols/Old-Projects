Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/EnquiryFilterService")> _
Public Class EnquiryFilterService
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
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_Enquiry_Filters", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("EnquiryFilterid", "EnquiryFilterid"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("AddAllRecord", "AddAllRecord"), New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "ps_Select_Enquiry_Filters"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
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
            Return Nothing
        End Get
    End Property

    <WebMethod()> Public Function GetFilters(ByVal pinLocationId As Int32) As DataSet
        Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pinLocationId
        SqlDataAdapter1.Fill(x)
        Return x
    End Function

    <WebMethod()> Public Function GetData(ByVal pintEnquiryFilterId As Int32, ByVal pstrAllString As String, ByVal pstrDisplayMember As String, ByVal pintAccurideLocationId As Int32, ByVal pintAccurideLocationId2 As Int32) As DataSet
        'ps_SelectEnquiry
        Dim ReturnValue As New DataSet()
        Try
            SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim sqlComm As New SqlCommand("ps_SelectEnquiry", SqlConnection1)
            sqlComm.CommandType = CommandType.StoredProcedure
            With sqlComm.Parameters
                .Add("@AccurideLocationId", pintAccurideLocationId)
                .Add("@AccurideLocationId2", pintAccurideLocationId2)
                .Add("@EnquiryFilterId", pintEnquiryFilterId)
                .Add("@DisplayMember", pstrDisplayMember)
                If Not pstrAllString Is Nothing AndAlso pstrAllString.Length > 0 Then
                    .Add("@AllString", pstrAllString)
                End If
            End With
            Dim sqlDA As New SqlDataAdapter(sqlComm)
            sqlDA.Fill(ReturnValue)
            sqlDA = Nothing
            sqlComm = Nothing
        End Try
        Return ReturnValue
        'Dim strSQLStatement As String
        'Try
        '    SqlConnection1.Open()
        'Catch
        '    Return Nothing
        'End Try

        'Dim udtSelect As New SqlCommand("Select * From tEnquiryFilterRules Where IsDisplayDataSQL = 1 and EnquiryFilterId = " & pintEnquiryFilterId, SqlConnection1)
        'Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
        'Dim udtDataSet As New DataSet()

        'udtDataAdapter.Fill(udtDataSet)

        'If udtDataSet.Tables(0).Rows.Count > 0 Then
        '    strSQLStatement = udtDataSet.Tables(0).Rows(0).Item("DisplayData").ToString.Replace("@AccurideLocationId", pintAccurideLocationId.ToString)

        '    If pintAccurideLocationId2 > 0 Then
        '        strSQLStatement = strSQLStatement.Replace("@Select_AccurideLocationId", pintAccurideLocationId2.ToString)
        '    Else
        '        strSQLStatement = strSQLStatement.Replace("@Select_AccurideLocationId", "Select AccurideLocationId From tAccurideLocation")
        '    End If
        '    Dim intPos As Int32

        '    intPos = strSQLStatement.IndexOf(" ", 1)
        '    ' Return strSQLStatement.Substring(0, intPos + 1) & "'" & udtDataSet.Tables(0).Rows(0).Item("EnquiryFilterRulesId").ToString & "' as EnquiryFilterRulesId," & strSQLStatement.Substring(intPos + 2)
        '    udtSelect.CommandText = strSQLStatement.Substring(0, intPos) & "'" & udtDataSet.Tables(0).Rows(0).Item("EnquiryFilterRulesId").ToString & "' as EnquiryFilterRulesId," & strSQLStatement.Substring(intPos + 1)
        '    udtDataAdapter = New SqlDataAdapter(udtSelect)
        '    udtDataSet.Clear()
        '    Try
        '        udtDataAdapter.Fill(udtDataSet)
        '    Catch
        '        Return Nothing
        '    End Try
        'Else
        '    udtSelect.CommandText = "select * from tEnquiryFilterRules where  EnquiryFilterId=" & pintEnquiryFilterId
        '    udtDataAdapter = New SqlDataAdapter(udtSelect)
        '    udtDataSet.Clear()
        '    udtDataAdapter.Fill(udtDataSet)
        '    If udtDataSet.Tables(0).Rows.Count > 0 Then
        '        Dim i As Int32
        '        For i = 0 To udtDataSet.Tables(0).Rows.Count - 1
        '            If i > 0 Then strSQLStatement &= "union "
        '            strSQLStatement &= "select '" & udtDataSet.Tables(0).Rows(i).Item("DisplayData").ToString & _
        '                "' as '" & pstrDisplayMember & "','" & udtDataSet.Tables(0).Rows(i).Item("EnquiryFilterRulesId").ToString & "' as EnquiryFilterRulesId ," & udtDataSet.Tables(0).Rows(i).Item("ColumnOrder").ToString & " as ColumnOrder "
        '        Next i
        '        strSQLStatement &= " order by ColumnOrder"
        '        '         Return strSQLStatement
        '        udtSelect.CommandText = strSQLStatement
        '        udtDataAdapter = New SqlDataAdapter(udtSelect)
        '        udtDataSet.Clear()
        '        Try
        '            udtDataAdapter.Fill(udtDataSet)
        '        Catch
        '            Return Nothing
        '        End Try
        '    End If
        'End If

        'If udtDataSet.Tables(0).Rows.Count > 0 Then
        '    If pstrAllString.Length > 0 And pstrDisplayMember.Length > 0 Then
        '        If udtDataSet.Tables(0).Rows.Count > 0 Then
        '            Dim objRow As DataRow = udtDataSet.Tables(0).NewRow()
        '            objRow.BeginEdit()
        '            objRow.Item(pstrDisplayMember) = "All " & pstrAllString & "s"
        '            objRow.EndEdit()
        '            udtDataSet.Tables(0).Rows.InsertAt(objRow, udtDataSet.Tables(0).Rows.Count)
        '            objRow = Nothing
        '        End If
        '    End If
        'End If

        'Return udtDataSet
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function test() As DataSet
        'Return LoadData(New Object() {15, 1, 1, "67"})
        Return LoadData(New Object() {18, 1, 1, "", 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As DataSet Implements IEPS_Service_Data.LoadData
        Dim ReturnValue As New DataSet()
        Try
            SqlConnection1.Open()
        Catch
            Return ReturnValue
        Finally
            Dim sqlComm As New SqlCommand("ps_SelectEnquiry_LoadData", SqlConnection1)
            sqlComm.CommandType = CommandType.StoredProcedure
            With sqlComm.Parameters
                .Add("@AccurideLocationId", CType(pudtParams(1), Int32))
                .Add("@ProjectMasterAccurideLocationId", CType(pudtParams(2), Int32))
                .Add("@Ignore", CType(pudtParams(0), Int32))
                .Add("@NewCondition", pudtParams(3))
                .Add("@EnquiryFilterId", CType(pudtParams(4), Int32))
                '.Add("@ValueMember", pudtParams(5))
                If pudtParams.Length > 5 Then
                    .Add("@FurtherEnquiryFilterId", CType(pudtParams(5), Int32))
                    .Add("@FurtherEnquiryFilter", pudtParams(6))
                End If
            End With
            Dim sqlDA As New SqlDataAdapter(sqlComm)
            sqlDA.Fill(ReturnValue)
            sqlDA = Nothing
            sqlComm = Nothing
        End Try
        Return ReturnValue
        'Dim blnIgnore As Boolean = (CType(pudtParams(0), Int32) < 0)
        'Dim strSQLStatement As String
        'Dim udtDataSet As New DataSet()
        'Dim strCondition As String
        'Dim strBasicOrderBySQL As String = " Order By Project desc, tProject.Issue"
        ''CType(pudtParams(1), Int32)()
        'Dim strBasicSQL As String = "Select ProjectMasterGroup As Project," & _
        '        "tProject.Issue," & _
        '        "tSlide.PartNumber As [Slide Type]," & _
        '        "[dbo].GiveLanguageString (3," & CType(pudtParams(1), Int32) & ",tUser.departmentId) As Department," & _
        '        "tUser.FullName As [Current Owner]," & _
        '        "tCustomer.CompanyName As Customer," & _
        '        "tProject.ProjectId As ProjectId," & _
        '        "tProjectEventLink.AcknowledgedByNewOwner " & _
        '        "FROM tProjectEventLink LEFT OUTER JOIN	 " & _
        '        "tUser ON tProjectEventLink.OwnerId = tUser.UserId RIGHT OUTER JOIN " & _
        '        "tEvent ON tProjectEventLink.EventId = tEvent.EventId RIGHT OUTER JOIN " & _
        '        "tProjectMaster LEFT OUTER JOIN	" & _
        '        "tCustomer ON tProjectMaster.CustomerID = tCustomer.CustomerId RIGHT OUTER JOIN	 " & _
        '        "tProject ON tProjectMaster.ProjectMasterId = tProject.ProjectMasterId ON " & _
        '        "tProjectEventLink.ProjectId = tProject.ProjectId LEFT OUTER JOIN	" & _
        '        "tSlide ON tProject.SlideId = tSlide.SlideId " & _
        '        "WHERE " & _
        '        "(tProject.Issue IN	(SELECT MAX(Issue) " & _
        '        "FROM tProject tTempProject WHERE tTempProject.ProjectMasterGroup = " & _
        '        "tProject.ProjectMasterGroup))"

        'If CType(pudtParams(2), Int32) > 0 Then
        '    strBasicSQL &= " And tProjectMaster.AccurideLocationId = " & CType(pudtParams(2), Int32)
        'End If
        'Try
        '    SqlConnection1.Open()
        'Catch
        '    Return Nothing
        'End Try

        'Dim udtSelect As SqlCommand
        'Dim udtDataAdapter As SqlDataAdapter
        'If Not blnIgnore Then
        '    udtSelect = New SqlCommand("Select Condition From tEnquiryFilterRules Where EnquiryFilterRulesId = " & CType(pudtParams(0), Int32), SqlConnection1)
        '    udtDataAdapter = New SqlDataAdapter(udtSelect)
        '    Try
        '        udtDataAdapter.Fill(udtDataSet)
        '    Catch
        '        Return Nothing
        '    End Try
        '    If udtDataSet.Tables(0).Rows.Count = 0 Then
        '        Return Nothing
        '    End If
        'End If

        'If blnIgnore Then
        '    udtSelect = New SqlCommand(strBasicSQL & strBasicOrderBySQL, SqlConnection1)
        'Else
        '    strCondition = udtDataSet.Tables(0).Rows(0).Item("Condition").ToString.TrimEnd
        '    Dim intLoop As Int32
        '    If pudtParams.GetUpperBound(0) > 1 Then
        '        For intLoop = pudtParams.GetLowerBound(0) + 3 To pudtParams.GetUpperBound(0)
        '            strCondition = strCondition.Replace("#" & intLoop - 3 & "#", CType(pudtParams(intLoop), String))
        '        Next intLoop
        '    End If
        '    udtSelect = New SqlCommand(strBasicSQL & " And (" & strCondition & ")" & strBasicOrderBySQL, SqlConnection1)
        'End If
        'udtDataAdapter = New SqlDataAdapter(udtSelect)
        'udtDataSet = New DataSet()
        'Try
        '    udtDataAdapter.Fill(udtDataSet)
        'Catch
        '    Return Nothing
        'End Try

        'Return udtDataSet
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        'N/A
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return Nothing
    End Function
    Public Overrides Sub AssignFields()
        With Fields
            .Add("EnquiryFilterid", New FieldInfoStructure("EnquiryFilterid", 10, False, False))
            .Add("AddAllRecord", New FieldInfoStructure("AddAllRecord", 1, True, False, , False))
            .Add("DisplayMember", New FieldInfoStructure("DisplayMember", 50, True, False))
            .Add("ValueMember", New FieldInfoStructure("ValueMember", 50, True, False))
        End With
    End Sub
End Class
