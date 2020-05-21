Public Class BOAuthorisedUser
    Inherits BaseBO
    'Alterations have been made to this class in light of the Authorised User table being removed. This
    'class has been altered to hide this fact and have the lowest impact on the system

    Public Sub New()
        'default constructor needed for serialization
    End Sub

    Public Sub New(ByVal SSOUserid As Int64)
        'Dim x As New [DO].DataObjects.Service.AuthorisedUserService
        'Dim SSO As New SSOUser(SSOUserid)
        If SSOUserid < 0 Then
            PopulateSysUser(SSOUserid)
        Else
            Populate(SSOUserid)
        End If
        '  x = Nothing
    End Sub

    'Public Sub New(ByVal SSOUserid As Decimal)
    '    MyClass.New(SSOUserid, Nothing)
    'End Sub

    'Public Sub New(ByVal authorisedUserid As Int32)
    '    MyClass.New(authorisedUserid, Nothing)
    'End Sub

    'Private Sub Populate(ByVal authorisedUserid As Int32, ByVal tran As SqlClient.SqlTransaction)
    '    Dim DOAuthorisedUser As New [DO].DataObjects.Entity.AuthorisedUser(authorisedUserid)
    '    Dim SSO As New SSOUser(DOAuthorisedUser.UserId)
    '    Populate(SSO, authorisedUserid, tran)
    'End Sub

    Private Sub PopulateSysUser(ByVal systemUserId As Int64)
        Dim SysUser As SystemUser.SystemUserEnum = CType(systemUserId, SystemUser.SystemUserEnum)
        SSOUserid = systemUserId
        Select Case SysUser
            Case SystemUser.SystemUserEnum.UNEPWCMCDataLoadUser
                'other things could be set in here?!?
                mFullName = "UNEPWCMC Data Load User"
        End Select
        SPNumber = String.Empty
        Location = String.Empty
        Title = String.Empty
        Initials = String.Empty
        FirstName = String.Empty
        Surname = String.Empty
        Grade = String.Empty
        Unit = String.Empty
        Room = String.Empty
        Ext = String.Empty
        STD = String.Empty
        JobTitle = String.Empty
    End Sub

    Private Sub Populate(ByVal ssoId As Int64)
        Dim UserDS As DataSet = Common.GetUserInfo(ssoId)
        If Not UserDS Is Nothing AndAlso UserDS.Tables.Count > 0 AndAlso UserDS.Tables(0).Rows.Count = 1 Then
            Dim dr As DataRow = UserDS.Tables(0).Rows(0)
            SSOUserid = CType(dr.Item("ID"), Int64)
            FirstName = GetField(dr, "FirstName")
            Surname = GetField(dr, "SurName")
            Email = GetField(dr, "Email")

            DefraUser = (GetField(dr, "ADGUID").Length > 0)
            If DefraUser Then
                mFullName = GetField(dr, "FullName")
                SPNumber = GetField(dr, "SPNumber")
                Location = GetField(dr, "Location")
                Title = GetField(dr, "Title")
                Initials = GetField(dr, "Initials")
                Grade = GetField(dr, "Grade")
                Unit = GetField(dr, "Unit")
                Room = GetField(dr, "Room")
                Ext = GetField(dr, "Ext")
                STD = GetField(dr, "STD")
                JobTitle = GetField(dr, "JobTitle")
                Fax = GetField(dr, "Fax")           'MLD 31/1/5 added
            Else
                'as this info isn't given.  Add it manually
                mFullName = String.Concat(FirstName, " ", Surname).Trim()
            End If
        End If

        Roles = GetRoles(ssoId)
        'Dim b As Boolean = Common.IsInRole(1300, Common.RolesList.CaseOfficer)
    End Sub

    Public Shared Function GetRoles(ByVal SSOUserid As Int64) As Common.RolesList()
        Dim RolesDS As DataSet = Nothing
        Try
            RolesDS = Common.GetUserRoles(SSOUserid)
        Catch ex As ArgumentNullException
            'this occurs when we are not connected to SSO
#If DEBUG Then
            'if we are in debug mode then hack an all roles in
            Return New Common.RolesList() {Common.RolesList.CaseOfficer}
#End If
        End Try
        If Not RolesDS Is Nothing AndAlso RolesDS.Tables.Count > 0 AndAlso RolesDS.Tables(0).Rows.Count > 0 Then
            Dim RolesList As New ArrayList
            For Each role As DataRow In RolesDS.Tables(0).Rows
                Dim RoleId As Int32 = CType(role.Item("ID"), Int32)
                If Not System.Enum.GetName(GetType(Common.RolesList), RoleId) Is Nothing Then
                    RolesList.Add(CType(RoleId, Common.RolesList))
                End If
            Next
            If RolesList.Count > 0 Then
                Return CType(RolesList.ToArray(GetType(Common.RolesList)), Common.RolesList())
            End If
        End If
    End Function

    Private Function GetField(ByVal dr As DataRow, ByVal field As String) As String     'MLD 31/1/5 added
        If dr.IsNull(field) Then
            Return String.Empty
        End If
        Return dr.Item(field).ToString()
    End Function

    'Public Sub New(ByVal authorisedUserid As Int32, ByVal tran As SqlClient.SqlTransaction)
    '    Populate(authorisedUserid, tran)
    'End Sub

    Public Property FullName() As String
        Get
            Return mFullName
        End Get
        Set(ByVal Value As String)
            mFullName = Value
        End Set
    End Property
    Private mFullName As String

    Public Roles() As Common.RolesList
    Public SSOUserid As Int64
    Public SPNumber As String
    Public Email As String
    Public Location As String
    Public Title As String
    Public Initials As String
    Public FirstName As String
    Public Surname As String
    Public Grade As String
    Public Unit As String
    Public Room As String
    Public Ext As String
    Public STD As String
    Public JobTitle As String
    Public Fax As String        'MLD 31/1/5 added
    Public Active As Boolean
    Public DefraUser As Boolean

    'Public Shared Function LoadAll() As BOAuthorisedUser()
    '    Dim AuthUserData As DataObjects.Entityset.AuthorisedUserSet = DataObjects.Entity.AuthorisedUser.GetAll()
    '    If Not AuthUserData Is Nothing AndAlso _
    '       AuthUserData.Count > 0 Then
    '        Dim ReturnResults(AuthUserData.Count - 1) As BOAuthorisedUser
    '        Dim Index As Int32 = 0
    '        For Each ThisUser As DataObjects.Entity.AuthorisedUser In AuthUserData
    '            ReturnResults(Index) = New BOAuthorisedUser(ThisUser.AuthorisedUserId)
    '            Index += 1
    '        Next ThisUser
    '        Return ReturnResults
    '    End If
    'End Function
End Class
