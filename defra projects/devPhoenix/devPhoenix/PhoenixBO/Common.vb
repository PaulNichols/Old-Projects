Imports System.Text
Imports Microsoft.Web.Services.Security
Imports System.Security.Cryptography
Public Class Common
    'Private Const SSOPublicWebServiceURL As String = "http://devsignonWebService:10080/WebService.asmx"
    'Private Const SSOUserName As String = "SignOn.WebService"
    'Private Const SSOPassword As String = "IXCLpo"
    Public Structure PhoenixInfo
        Public Sub New(ByVal userPermissions As String, ByVal userId As String)
            Me.UserPermissions = userPermissions
            Me.SSOUserId = CType(userId, Int64)
        End Sub

        Public UserPermissions As String
        Public SSOUserId As Int64

        Public ReadOnly Property IsEmpty() As Boolean
            Get
                Return UserPermissions Is Nothing OrElse UserPermissions.Length = 0
            End Get
        End Property
    End Structure

    Public Shared Function GetUserInfo(ByVal ssoUserId As Int64) As DataSet
        Dim ws As SSOPublicWebReference.WebServiceWse = GetSSOWebService()
        If Not ws Is Nothing Then
            Return ws.GetUserInfo(ssoUserId)
        End If
    End Function

    Public Shared Function AreInSameRole(ByVal UserToCheck As Long, ByVal UserToCheckAgainst As Long) As Boolean
        Dim UserToCheckAgainstsRoles As BO.Common.RolesList() = BO.BOAuthorisedUser.GetRoles(UserToCheckAgainst)
        Dim UserToChecksRoles As BO.Common.RolesList() = BO.BOAuthorisedUser.GetRoles(UserToCheckAgainst)
        For Each role As RolesList In UserToChecksRoles
            If UserToCheckAgainstsRoles.IndexOf(UserToCheckAgainstsRoles, role) > -1 Then
                Return True
            End If
        Next
    End Function

    Public Shared Function GetUserRoles(ByVal ssoUserId As Int64) As DataSet
        Dim ws As SSOPublicWebReference.WebServiceWse = GetSSOWebService()
        If Not ws Is Nothing Then
            Dim AppId As Int64
            AppId = CType(System.Configuration.ConfigurationSettings.AppSettings("AppId"), Int64)
            Return ws.GetUserRoles(AppId, ssoUserId)
        End If
    End Function

    'Public Shared Function GetUserInfoObject(ByVal ssoUserId As Int64) As Phoenix.BO.SSOUser
    '    Dim ds As DataSet = GetUserInfo(ssoUserId)
    '    If Not ds Is Nothing Then
    '        Dim User As Phoenix.BO.SSOUser = New Phoenix.BO.SSOUser(ssoUserId)
    '        Return User
    '    End If
    'End Function

    'Changed MLD 27/10/4 to (a) work in a loop (b) return nothing instead of stopping
    Public Shared Function GetUsers(ByVal appId As Int32, ByVal roleId As Int32) As DataSet
        For index As Int32 = 1 To 3     'arbitrarily try 3 times
            Try
                Return GetSSOWebService().GetUserList(appId, roleId)
            Catch ex As Exception
            End Try
        Next
        Return Nothing
    End Function

    Public Shared Function GetUsersObject(ByVal appId As Int32, ByVal roleId As Int32) As SSOUser()
        Dim ds As DataSet = GetUsers(appId, roleId)
        If Not ds Is Nothing AndAlso _
           ds.Tables.Count > 0 AndAlso _
           ds.Tables(0).Rows.Count > 0 Then
            Dim ReturnVal(ds.Tables(0).Rows.Count - 1) As SSOUser
            Dim Index As Int32 = 0
            For Each dr As Data.DataRow In ds.Tables(0).Rows
                ReturnVal(Index) = New SSOUser(dr)
                Index += 1
            Next dr
            Return ReturnVal
        Else
            Return Nothing
        End If
    End Function

    Public Structure SSOUser
        Public Sub New(ByVal dr As DataRow)
            Me.UserId = CType(dr.Item("ID"), Int32)
            Me.UserName = dr.Item("FullName").ToString
            If Not dr.IsNull("SPNumber") Then
                Me.SPNumber = dr.Item("SPNumber").ToString
            Else
                Me.SPNumber = Nothing
            End If
        End Sub

        Public Sub New(ByVal userId As Int64, ByVal userName As String, ByVal spNumber As String)
            Me.UserId = userId
            Me.UserName = userName
            Me.SPNumber = spNumber
        End Sub

        Public UserId As Int64
        Public SPNumber As String 'may be nothing
        Public UserName As String

        Public Function GetPhoenixUser() As BOAuthorisedUser
            Dim ReturnVal As BOAuthorisedUser = Nothing
            If UserId > 0 Then
                ReturnVal = New BOAuthorisedUser(UserId)
            End If
            Return ReturnVal
        End Function
    End Structure

    'Public Shared Function IsSSOUserInRole(ByVal appId As Int32, ByVal ssoId As Int64, ByVal roleId As RolesList) As Boolean
    '    Dim Result As Boolean = False

    '    'load the users in the group
    '    Dim Users As SSOUser() = GetUsersObject(appId, CType(roleId, Int32))
    '    If Not Users Is Nothing AndAlso _
    '       Users.Length > 0 Then
    '        For Each User As SSOUser In Users
    '            If User.UserId = ssoId Then Return True
    '        Next User
    '    End If
    'End Function

    Public Enum RolesList
        All = 0
        GuildfordDevelopmentTeam = 117
        TeamLeader = 1500
        HigherManagement = 1501
        Enforcement = 1502
        Inspectorate = 1503
        Administrator = 1504
        DataMaintainer = 1505
        Experienced = 1506
        ApplicationProcessor = 1507
        JNCC = 1508
        CaseOfficer = 1509
        Kew = 1510
        Customer = 1511
        Trainee = 4000
    End Enum

    <Serializable()> _
    Public Enum AssignedToList
        All = 0
        CaseOfficer = 1
        CITESSecretariat = 2
        EC = 3
        HMCustomsAndExcise = 4
        Inspectorate = 5
        JNCC = 6
        Kew = 7
        Other = 8
        Policy = 9
        'RingPicker = 10
        TeamLeader = 11
        Customer = 12
    End Enum

    'Public Shared Function GetUser(ByVal id As Int32) As BOAuthorisedUser
    '    GetUserInfo(id)
    '    Return New BOAuthorisedUser(id)
    'End Function

    'Public Shared Function GetUserName(ByVal id As Int32) As String
    '    Try
    '        Return GetUser(id).FullName
    '    Catch ex As Exception
    '        Return ""
    '    End Try
    'End Function

    Friend Shared Function GetSSOWebService() As SSOPublicWebReference.WebServiceWse
        Dim ws As New SSOPublicWebReference.WebServiceWse
        ws.Url = System.Configuration.ConfigurationSettings.AppSettings("SSOPublicWebServiceURL") 'SSOPublicWebServiceURL
        Dim UserName As String = System.Configuration.ConfigurationSettings.AppSettings("SSOUserName")
        Dim UserPassword As String = System.Configuration.ConfigurationSettings.AppSettings("SSOPassword")

        Dim hashedDataByte As Byte()
        Dim encoder As New UTF8Encoding
        Dim md5Hasher As New MD5CryptoServiceProvider

        hashedDataByte = md5Hasher.ComputeHash(encoder.GetBytes(UserPassword))

        Dim Token As New UsernameToken(UserName, System.Text.ASCIIEncoding.Unicode.GetString(hashedDataByte), PasswordOption.SendPlainText)

        ws.RequestSoapContext.Security.Tokens.Add(Token)
        Return ws
    End Function

    ', ByVal appId As Int32, ByVal ssoId As Int64
    Public Shared Function HasPermissions(ByVal permissions As String, ByVal permission As UserPermissions) As Boolean
        Return HasPermissions(permissions, CType(permission, Int32))
    End Function

    Private Shared Function HasPermissions(ByVal permissions As String, ByVal permission As Int32) As Boolean
        Return HasPermissions(permissions, permission.ToString)
    End Function

    Private Shared Function HasPermissions(ByVal permissions As String, ByVal permission As String) As Boolean

        'Dim Roles As String
        'Dim ws As SSOPublicWebReference.WebServiceWse = GetSSOWebService()
        'Dim returnstate As Boolean = ws.CheckPermission(appId, ssoId, permission)
        'Return returnstate

        'Roles = UserRolesDS.Tables(0).Rows(0).Item.toString
        If Not permissions Is Nothing AndAlso permissions.Length > 0 Then
            Dim PermissionsArray As String() = permissions.Split(";"c)
            Return PermissionsArray.IndexOf(PermissionsArray, CType(System.Enum.Parse(GetType(UserPermissions), permission), Int32).ToString) > -1
        End If

    End Function

    Public Shared Function IsInRole(ByVal ssoUserid As Int64, ByVal role As RolesList) As Boolean
        Dim Roles As RolesList() = BOAuthorisedUser.GetRoles(ssoUserid)
        Return (Roles.IndexOf(Roles, role) >= 0)
    End Function

    'Public Shared Function HasPermissions(ByVal appId As Int32, ByVal ssoId As Int32, ByVal rolelist() As String) As Boolean
    '    If rolelist.Length > 0 Then
    '        For Each Role As String In rolelist
    '            If Not HasPermissions(Role, appId, ssoId) Then
    '                Return False
    '            End If
    '        Next Role
    '    End If
    '    Return True
    'End Function

    <Serializable()> _
    Public Enum UserPermissions
        'Notes
        AddNewPartyNote = 100
        ViewPartyNotes = 101
        ModifyOwnPartyNotes = 102

        'Application Notes
        ModifyApplicationNotes = 132
        AddNewApplicationNote = 133
        ViewApplicationNotes = 134

        'Administration
        CanAdministerSSO = 103
        MaintainReferenceData = 107
        MaintainReferenceData_JNCC_Kew = 108

        'Party
        SearchParty = 104
        ModifyPartyDetails = 105
        AddNewPartyDetails = 106
        CanAuthoriseParty = 109
        CanEnterCITESRegNumber = 110


        'Cites
        HideAdditionalDeclarations = 111
        CanPlacePermitUnderDerogation = 112
        CanChangeMAAddress = 113
        CanSpecifyReexport = 114
        CanViewDuplicatePermits = 115
        CanViewSeizureNotifications = 116
        CanMarkPermitsRetrospective = 117
        CanSetPermitType = 118
        CanSetRecievedDate = 119
        CanDeleteIDMarkFromSpecimen = 121
        CanChooseMACOSyle = 131
        CanAddEditSpecimenReport = 135
        CanAddEditCA = 136
        CanModifySeizureNotification = 137
        CanAddSeizureNotification = 138
        CanViewImportNotification = 139
        CanModifyImportNotification = 140
        CanAddImportNotification = 141
        'Misc
        CanSeeScreenCodes = 120


        'User Roles for UC-222
        'IsJNCC = 122
        'IsTeamLeader = 123
        'IsCaseOfficer = 124
        'IsInspectorate = 125
        'IsKew = 126
        'IsCITESSecretariat = 126
        'IsHMCustomsAndExcise = 127
        'IsPolicy = 128
        'IsEC = 129
        'IsOther = 130
        'IsCustomer = 199

        'UC405 Search taxonomy.
        CanViewTaxonomyAnimalLicensing = 150
        CanViewTaxonomyAnimalDelegation = 151
        CanViewTaxonomyFullDetail = 152
        CanSpecifySpeciesOnApplication = 153
        CanSpecifySpeciesOnApplicationSearch = 154
        CanSpecifySpeciesToMaintainPlantTaxonomy = 155
        CanSpecifySpeciesToMaintainLocalTaxonomyControlInfo = 156
        CanViewTaxonomyFullDetailNotes = 157
        ' DOR
        LogonRequiresCertificateOnly = 160

        'Taxonomy Data Load.
        CanLoadTaxonomyData = 200
        CanOverrideTaxonomyTimeWindow = 201

        CanPickRings = 202
        MaintainPlantTaxonomy = 204
    End Enum

    <Serializable()> _
    Public Class NameValuePair
        Public Sub New()
        End Sub

        Public Sub New(ByVal name As String, ByVal value As Int32)
            Me.Name = name
            Me.Value = value
        End Sub

        Public Name As String
        Public Value As Int32
    End Class
End Class
