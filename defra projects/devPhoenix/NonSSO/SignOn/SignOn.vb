Imports System.Security
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Web

''' -----------------------------------------------------------------------------
''' Project	 : TRUtils
''' Class	 : gov.defra.Registry.Utils.SignOn
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Custom Authentication implementation of the <see cref="gov.defra.Registry.Utils.ISignOn"/> interface.
''' </summary>
''' <remarks>
''' Generates a cookie called 'Trading_Registry_Roles' that is used to store the list of allowed permissions/roles.
''' </remarks>
''' <history>
''' 	[x912761]	14/01/2004	Created
''' </history>
''' -----------------------------------------------------------------------------

Public NotInheritable Class CustomSignOn
    Inherits SignOnBase
    Implements SignOn.ISignOn

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Implements <see cref="gov.defra.Registry.Utils.ISignOn.CheckAuthentication"/> of the <see cref="gov.defra.Registry.Utils.ISignOn"/> interface. 
    ''' </summary>
    ''' <remarks>
    ''' <para>When called for the first time this method will use the RetrieveUserRolesByUserName method of the ETRUsersWebService to gather the list of
    ''' permissions using the name stored in <see cref="System.Web.HttpContext.Current.User.Identity.Name">HttpContext.Current.User.Identity.Name</see> 
    ''' and store these in the cookie 'Trading_Registry_Roles'.
    ''' </para>
    ''' <para>On subsequet calls the permissions will be extracted from the cookie.
    ''' </para>
    ''' </remarks>
    ''' <example>Normally used in the Application_AuthenticateRequest method of a Global.asax
    ''' <code>
    '''  Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
    '''    If Request.IsAuthenticated Then
    '''    Dim signOn As New SignOn
    '''        signOn.CheckAuthentication()
    '''    End If
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' <history>
    ''' 	[x912761]	14/01/2004	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CheckAuthentication() Implements SignOn.ISignOn.CheckAuthentication

        SetPermissions(HttpContext.Current.User.Identity.Name)

    End Sub

    Public Function CurrentUserName() As String Implements SignOn.ISignOn.CurrentUserName
        Return HttpContext.Current.User.Identity.Name
    End Function

    'Public Function UserRegistryMode() As Utils.Configuration.RegistryMode Implements Utils.SignOn.ISignOn.UserRegistryMode

    '    Dim retVal As Utils.Configuration.RegistryMode = Utils.Configuration.RegistryMode.User

    '    If HttpContext.Current.Request.Cookies("Trading_Registry_Roles") Is Nothing Then

    '        ' get name from identiry
    '        Dim name As String = HttpContext.Current.User.Identity.Name

    '        ' use it to get the roles
    '        Dim ws As ETR.WebServices.General.WebServiceWse = ETR.WebServices.Common.CreateWebService(WebServices.Common.WebServicesType.General)
    '        Dim secUser As ETR.WebServices.General.User = ws.FindUser(name)

    '        ' set mode
    '        If secUser.IsAdmin Then
    '            retVal = Utils.Configuration.RegistryMode.Admin
    '        Else
    '            retVal = Utils.Configuration.RegistryMode.User
    '        End If



    '    Else

    '        ' Get roles from roles cookie
    '        Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies("Trading_Registry_Roles").Value)

    '        'convert the string representation of the role data into a string array
    '        Dim userRoles As New ArrayList

    '        Dim role As String

    '        Dim tkt As String() = ticket.UserData.Split(New Char() {"!"c})

    '        'get mode
    '        Select Case tkt(0)
    '            Case "Admin"
    '                retVal = Utils.Configuration.RegistryMode.Admin
    '            Case "User"
    '                retVal = Utils.Configuration.RegistryMode.User
    '        End Select

    '    End If

    '    HttpContext.Current.Session("UserName") = HttpContext.Current.User.Identity.Name

    '    Return retVal

    'End Function


End Class


