Imports System.Web.Services
Imports System.Data.SqlClient
<System.Web.Services.WebService(Namespace:="http://www.Accuride.com/UserService")> _
Public Class UserService
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
    Friend WithEvents SqlDataAdapter3 As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDataAdapter2 As System.Data.SqlClient.SqlDataAdapter

    'Required by the Web Services Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlDataAdapter3 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter1 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlDataAdapter2 = New System.Data.SqlClient.SqlDataAdapter()
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection()
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand()
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand()
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand()
        '
        'SqlDataAdapter3
        '
        Me.SqlDataAdapter3.SelectCommand = Me.SqlSelectCommand2
        Me.SqlDataAdapter3.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_UserSettings", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SettingId", "SettingId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("Value", "Value"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("id", "id"), New System.Data.Common.DataColumnMapping("FullName", "FullName")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SettingId", "SettingId"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("Value", "Value"), New System.Data.Common.DataColumnMapping("Description", "Description"), New System.Data.Common.DataColumnMapping("Id", "Id")})})
        Me.SqlDataAdapter3.UpdateCommand = Me.SqlUpdateCommand2
        '
        'SqlDataAdapter1
        '
        Me.SqlDataAdapter1.DeleteCommand = Me.SqlDeleteCommand1
        Me.SqlDataAdapter1.InsertCommand = Me.SqlInsertCommand1
        Me.SqlDataAdapter1.SelectCommand = Me.SqlSelectCommand1
        Me.SqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ps_Select_User", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("Departmentid", "Departmentid"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Telephone", "Telephone"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("UserName", "UserName")}), New System.Data.Common.DataTableMapping("Table1", "Table1", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("Departmentid", "Departmentid"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Telephone", "Telephone"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("UserName", "UserName")}), New System.Data.Common.DataTableMapping("Table2", "Table2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("Departmentid", "Departmentid"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Telephone", "Telephone"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("UserName", "UserName"), New System.Data.Common.DataColumnMapping("GroupId", "GroupId")}), New System.Data.Common.DataTableMapping("Table3", "Table3", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("AccurideLocationId", "AccurideLocationId"), New System.Data.Common.DataColumnMapping("Departmentid", "Departmentid"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("FullName", "FullName"), New System.Data.Common.DataColumnMapping("Telephone", "Telephone"), New System.Data.Common.DataColumnMapping("UserId", "UserId"), New System.Data.Common.DataColumnMapping("UserName", "UserName"), New System.Data.Common.DataColumnMapping("GroupId", "GroupId")}), New System.Data.Common.DataTableMapping("Table4", "Table4", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table5", "Table5", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table6", "Table6", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")}), New System.Data.Common.DataTableMapping("Table7", "Table7", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        Me.SqlDataAdapter1.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDataAdapter2
        '
        Me.SqlDataAdapter2.SelectCommand = Me.SqlSelectCommand3
        Me.SqlDataAdapter2.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LoadUsersForEvents", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DisplayMember", "DisplayMember"), New System.Data.Common.DataColumnMapping("ValueMember", "ValueMember")})})
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "[LoadUsersForEvents]"
        Me.SqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand3.Parameters.Add(New System.Data.SqlClient.SqlParameter("@StageRuleId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = strCONNECTIONSTRING
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "[ps_Select_User]"
        Me.SqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadCombo", System.Data.SqlDbType.Bit, 1))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@LoadType", System.Data.SqlDbType.Int, 4))
        Me.SqlSelectCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4))
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "[ps_Insert_User]"
        Me.SqlInsertCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Departmentid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "Departmentid", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Email",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FullName", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FullName",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Telephone", System.Data.SqlDbType.VarChar, 15,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Telephone",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserName", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"UserName",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlInsertCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Active", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "[ps_Update_User]"
        Me.SqlUpdateCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Departmentid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "Departmentid", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Email", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Email",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@FullName", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FullName",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Telephone", System.Data.SqlDbType.VarChar, 15,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Telephone",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserName", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"UserName",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Active", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Departmentid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"Departmentid",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Email", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Email",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FullName", System.Data.SqlDbType.VarChar, 50,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"FullName",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Telephone", System.Data.SqlDbType.VarChar, 15,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Telephone",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UserName", System.Data.SqlDbType.VarChar, 20,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"UserName",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Active", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "[ps_Delete_User]"
        Me.SqlDeleteCommand1.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_AccurideLocationId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "AccurideLocationId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Departmentid", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "Departmentid", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Email", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Email", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_FullName", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "FullName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Telephone", System.Data.SqlDbType.VarChar, 15, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "Telephone", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UserName", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, True, CType(0, Byte), CType(0, Byte), "UserName", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(10, Byte), "Active", System.Data.DataRowVersion.Original, Nothing))
        Me.SqlDeleteCommand1.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "[ps_Select_UserSettings]"
        Me.SqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlSelectCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        '
        'SqlUpdateCommand2
        '
        Me.SqlUpdateCommand2.CommandText = "[ps_Update_UserSetting]"
        Me.SqlUpdateCommand2.CommandType = System.Data.CommandType.StoredProcedure
        Me.SqlUpdateCommand2.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, False, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@SettingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "SettingId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, False, CType(10, Byte), CType(0, Byte), "UserId", System.Data.DataRowVersion.Current, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Value", System.Data.SqlDbType.VarChar, 100,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Value",System.Data.DataRowVersion.Current,Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_SettingId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"SettingId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"UserId",System.Data.DataRowVersion.Original, Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Original_Value", System.Data.SqlDbType.VarChar, 100,System.Data.ParameterDirection.Input,True,CType(10,Byte),CType(10,Byte),"Value",System.Data.DataRowVersion.Original,Nothing))
        Me.SqlUpdateCommand2.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Select_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input,True, CType(10, Byte), CType(0, Byte),"id",System.Data.DataRowVersion.Current, Nothing))

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
    End Sub

#End Region

    Public ReadOnly Property TableName() As String Implements IEPS_Service_Data.TableName
        Get
            Return "tUser"
        End Get
    End Property

    <WebMethod()> Public Function GetSalesManagers(ByVal pintAccurideLocationId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@LoadType").Value = 1
        SqlDataAdapter1.SelectCommand.Parameters("@Select_UserId").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pintAccurideLocationId
        SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = 0
        SqlDataAdapter1.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> Public Function LoadNewOwnersCombo(ByVal pintProjectId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter2.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> Public Function LoadProjectTeam(ByVal projectId As Int32, ByVal accurideLocationId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@LoadType").Value = 3
        SqlDataAdapter1.SelectCommand.Parameters("@Select_UserId").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = accurideLocationId
        SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = projectId
        SqlDataAdapter1.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> Public Function LoadUsersForEventsCombo(ByVal pintStageRuleId As Int32, ByVal pintProjectId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter2.SelectCommand.Parameters("@ProjectId").Value = pintProjectId
        SqlDataAdapter2.SelectCommand.Parameters("@StageRuleId").Value = pintStageRuleId
        SqlDataAdapter2.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> Public Function GetDirectors(ByVal pintAccurideLocationId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@LoadType").Value = 2
        SqlDataAdapter1.SelectCommand.Parameters("@Select_UserId").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationId").Value = pintAccurideLocationId
        SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = 0
        SqlDataAdapter1.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> Public Function GetUserSettings(ByVal pintUserId As Int32) As DataSet
        Dim DataSet As New DataSet()
        SqlDataAdapter3.SelectCommand.Parameters("@UserId").Value = pintUserId
        SqlDataAdapter3.Fill(DataSet)
        Return DataSet
    End Function

    <WebMethod()> _
    Public Function UpdateUserSettings(ByVal pudtUserSettingDataSet As DataSet) As DataSet
        If Not (pudtUserSettingDataSet Is Nothing) Then
            SqlDataAdapter3.Update(pudtUserSettingDataSet)
            Return pudtUserSettingDataSet
        Else
            Return Nothing
        End If
    End Function

    <WebMethod(Description:="Returns the identifier for the requested user, 0 if it doesn't exist.")> _
    Public Function GetUserId(ByVal pstrUserName As String) As Int32
        Dim intReturn As Int32 = 0
        Try
            Me.SqlConnection1.Open()
        Catch
            Return 0
        Finally
            Dim udtSelect As New SqlCommand("ps_Select_UserIdForUser", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@UserName", pstrUserName)
            End With
            Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
            Try
                udtDataAdapter.Fill(udtDataSet)
                If Not udtDataSet Is Nothing Then
                    If udtDataSet.Tables(0).Rows.Count > 0 Then

                        intReturn = CType(udtDataSet.Tables(0).Rows(0).Item("UserId").ToString, Int32)
                    End If
                End If
            Catch
                intReturn = 0
            End Try
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return intReturn
    End Function

    <WebMethod()> Public Function GetSingleUserDescription(ByVal pintUserId As Int32) As String
        Dim strReturn As String = ""
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ""
        Finally
            Dim udtSelect As New SqlCommand("ps_Select_UserForUserId", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@UserId", pintUserId)
            End With
            Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
            Try
                udtDataAdapter.Fill(udtDataSet)
                If Not udtDataSet Is Nothing Then
                    If udtDataSet.Tables(0).Rows.Count > 0 Then

                        strReturn = udtDataSet.Tables(0).Rows(0).Item("UserName").ToString
                    End If
                End If
            Catch
                strReturn = ""
            End Try
            udtDataSet = Nothing
            udtSelect = Nothing
        End Try
        Return strReturn
    End Function

    <WebMethod()> Public Function CanUserModifyThisEvent(ByVal pintUserId As Int32, ByVal pintProjectId As Int32) As Boolean
        Dim blnReturn As Boolean = False
        Try
            Me.SqlConnection1.Open()
        Catch
            Return False
        End Try
        Dim udtSelect As New SqlCommand("ps_CanUserModifyThisEvent", Me.SqlConnection1)
        With udtSelect
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, pintUserId))
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@ProjectId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, pintProjectId))
        End With
        Try
            udtSelect.ExecuteNonQuery()
            blnReturn = CType(udtSelect.Parameters("@RETURN_VALUE").Value.ToString, Boolean)
        Catch
            blnReturn = False
        Finally
            udtSelect = Nothing
        End Try
        Return blnReturn
    End Function
    '<WebMethod()> Public Function GetInitiatorsSalesManagers() As Userds
    '    Dim x As New Userds()
    '    SqlDataAdapter1.SelectCommand.Parameters("@LoadType").Value = 1
    '    SqlDataAdapter1.Fill(x)
    '    Return x
    'End Function

    '<System.Web.Services.WebMethodAttribute()> Public Function RUNTEST() As DataSet
    '    Return LoadData(New Object() {1, 0})
    'End Function


    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadData(ByVal pudtParams() As System.Object) As System.Data.DataSet Implements IEPS_Service_Data.LoadData
        'Dim x As New DataSet()
        SqlDataAdapter1.SelectCommand.Parameters("@LoadType").Value = 0
        SqlDataAdapter1.SelectCommand.Parameters("@Select_UserId").Value = CType(pudtParams(1), Int32)
        SqlDataAdapter1.SelectCommand.Parameters("@AccurideLocationid").Value = CType(pudtParams(0), Int32)
        If pudtParams.GetUpperBound(0) = 2 Then
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = 1
        Else
            SqlDataAdapter1.SelectCommand.Parameters("@LoadCombo").Value = Convert.DBNull
        End If
        SqlDataAdapter1.SelectCommand.Parameters("@ProjectId").Value = 0
        Return LoadDataSet(SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function UpdateData(ByVal Changes As System.Data.DataSet) As System.Data.DataSet Implements IEPS_Service_Data.UpdateData
        Return UpdateDataset(Changes, SqlDataAdapter1)
    End Function

    <System.Web.Services.WebMethodAttribute()> _
    Public Function LoadCombo(ByVal pintUniqueId As Integer, ByVal pintAccurideLocationId As Integer) As System.Data.DataSet Implements IEPS_Service_Data.LoadCombo
        Return LoadData(New Object() {pintAccurideLocationId, pintUniqueId, 1})
    End Function

    <System.Web.Services.WebMethodAttribute()> _
         Public Function UpdateUserRights(ByVal pintUserId As Int32, ByVal pstrValue As String, ByVal pstrOriginal_Value As String) As Boolean
        Dim SqlCommand1 As New System.Data.SqlClient.SqlCommand()
        With SqlCommand1
            .CommandText = ".ps_Update_UserRights"
            .CommandType = System.Data.CommandType.StoredProcedure
            .Connection = SqlConnection1
            .Parameters.Add("@Value", pstrValue)
            .Parameters.Add("@Original_Value", pstrOriginal_Value)
            .Parameters.Add("@UserId", pintUserId)
            .Parameters.Add(New System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, True, CType(10, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, Nothing))
            Try
                .Connection.Open()
                .ExecuteNonQuery()
                Return (CType(.Parameters("@RETURN_VALUE").Value.ToString, Int32) = 1)
            Catch
                Return False
            End Try
        End With
    End Function


    <WebMethod()> Public Function GetUserRights2(ByVal pintUserId As Int32) As DataSet
        Dim strReturn As String = ""
        Me.SqlConnection1.Open()
        Dim udtSelect As New SqlCommand("ps_Select_UserRights", Me.SqlConnection1)
        Dim udtDataSet As New DataSet()

        With udtSelect
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@UserId", pintUserId)
        End With
        Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
        udtDataAdapter.Fill(udtDataSet)

        udtSelect = Nothing
        Return udtDataSet
    End Function

    <WebMethod()> _
    Public Function GetUserRights(ByVal pintUserId As Int32) As String
        Dim strReturn As String = ""
        Try
            Me.SqlConnection1.Open()
        Catch
            Return ""
        Finally
            Dim SelectCmd As New SqlCommand("ps_Select_UserRights", Me.SqlConnection1)
            With SelectCmd
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@UserId", pintUserId)
            End With
            strReturn = SelectCmd.ExecuteScalar.ToString
            SelectCmd = Nothing
        End Try
        Return strReturn
    End Function

    <WebMethod()> _
    Public Function StartUpApp(ByVal pstrUserName As String, ByVal pstrCulture As String) As Int32()
        Dim intReturn As Int32() = New Int32() {0, 0, 0}
        Try
            Me.SqlConnection1.Open()
            Dim udtSelect As New SqlCommand("ps_Select_Startup", Me.SqlConnection1)
            Dim udtDataSet As New DataSet()
            With udtSelect
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@UserName", pstrUserName)
                .Parameters.Add("@Culture", pstrCulture)
            End With
            Dim udtDataAdapter As New SqlDataAdapter(udtSelect)
            Try
                udtDataAdapter.Fill(udtDataSet)
                If Not udtDataSet Is Nothing Then
                    If udtDataSet.Tables(0).Rows.Count > 0 Then
                        intReturn = New Int32() {CType(udtDataSet.Tables(0).Rows(0).Item(0).ToString, Int32), CType(udtDataSet.Tables(1).Rows(0).Item(0).ToString, Int32), CType(udtDataSet.Tables(2).Rows(0).Item(0).ToString, Int32)}
                    End If
                End If
                If intReturn(2) = 0 Then

                End If

            Catch

            End Try
            udtDataSet = Nothing
            udtSelect = Nothing
        Catch

        End Try
        Return intReturn
    End Function

    Public Overrides Sub AssignFields()
        With Fields
            .Add("DisplayMember", New FieldInfoStructure("DisplayMember", 50, True, False))
            .Add("ValueMember", New FieldInfoStructure("ValueMember", 10, False, False))
            .Add("UserName", New FieldInfoStructure("UserName", 20, False, False))
            .Add("FullName", New FieldInfoStructure("FullName", 50, True, False))
            .Add("Telephone", New FieldInfoStructure("Telephone", 15, True, False))
            .Add("Email", New FieldInfoStructure("Email", 40, True, False))
            .Add("AccurideLocationId", New FieldInfoStructure("AccurideLocationId", 10, False, False))
            .Add("Departmentid", New FieldInfoStructure("Departmentid", 10, False, False))
            .Add("UserId", New FieldInfoStructure("UserId", 10, False, False))
            .Add("LocationName", New FieldInfoStructure("LocationName", 50, False, False))
            .Add("SettingId", New FieldInfoStructure("SettingId", 10, True, False))
            .Add("Value", New FieldInfoStructure("Value", 100, True, False))
            .Add("Description", New FieldInfoStructure("Description", 22, False, True, "(iif( ([SettingId] = 9) , 'Show Wait Screen' , (iif( ([SettingId] = 8) , 'Show Revert Messagebox' , (iif( ([SettingId] = 1) , 'Last Project Id' , (iif( ([SettingId] = 2) , 'Rights' , (iif( ([SettingId] = 3) , 'Filter 1' , (iif( ([SettingId] = 4) , 'Filter 2' , (iif( ([SettingId] = 5) , 'Load Last Filter' , (iif( ([SettingId] = 6) , 'Load Last Project' , (iif( ([SettingId] = 7) , 'Show Close Messagebox' , '???' )) )) )) )) )) )) )) )) ))"))
            .Add("id", New FieldInfoStructure("id", 10, False, False))
            .Add("Culture", New FieldInfoStructure("Culture", 5, False, False))
            .Add("Active", New FieldInfoStructure("Active", 1, True, False, , True))
        End With
    End Sub
End Class
