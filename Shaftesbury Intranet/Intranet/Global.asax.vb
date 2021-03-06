Imports System.Security
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Threading
Imports System.Globalization
Imports System.Web.SessionState

Namespace ASPNET.StarterKit.Portal

    Public Class Global
        Inherits System.Web.HttpApplication

        Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
            ' Fires when the session ends
            Response.Redirect("http://intranet/default.aspx")
        End Sub

        Shared Function SetSummary(ByVal doc As Object, ByVal title As String, ByVal subject As String, ByVal author As String, _
            ByVal keywords As String) As Boolean

            Dim oSummary As Object
            oSummary = doc.application.Dialogs(86)
            With oSummary
                .Title = title
                .Subject = subject
                .Author = author
                .Keywords = keywords
                Try
                    .Execute()
                Catch
                    Return False
                End Try
            End With
            Return True

        End Function

        '*********************************************************************
        '
        ' Application_BeginRequest Event
        '
        ' The Application_BeginRequest method is an ASP.NET event that executes 
        ' on each web request into the portal application.  The below method
        ' obtains the current tabIndex and TabId from the querystring of the 
        ' request -- and then obtains the configuration necessary to process
        ' and render the request.
        '
        ' This portal configuration is stored within the application's "Context"
        ' object -- which is available to all pages, controls and components
        ' during the processing of a single request.
        ' 
        '*********************************************************************



        Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

            Dim tabIndex As Integer = 0
            Dim tabId As Integer = 1

            ' Get TabIndex from querystring
            If Not (Request.Params("tabindex") Is Nothing) Then
                tabIndex = CInt(Request.Params("tabindex"))
            End If

            ' Get TabID from querystring
            If Not (Request.Params("tabid") Is Nothing) Then
                tabId = CInt(Request.Params("tabid"))
            End If

            ' Add the PortalSettings object to the context
            Context.Items.Add("PortalSettings", New PortalSettings(tabIndex, tabId))

            ' Read the configuration info from the XML file or retrieve from Cache
            ' and add to the context
            Dim config As Configuration = New Configuration
            Context.Items.Add("SiteSettings", config.GetSiteSettings())

            Try
                If Not (Request.UserLanguages Is Nothing) Then
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages(0))
                    ' Default to English if there are no user languages
                Else
                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
                End If
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture
            Catch ex As Exception
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
            End Try
        End Sub


        '*********************************************************************
        '
        ' Application_AuthenticateRequest Event
        '
        ' If the client is authenticated with the application, then determine
        ' which security roles he/she belongs to and replace the "User" intrinsic
        ' with a custom IPrincipal security object that permits "User.IsInRole"
        ' role checks within the application
        '
        ' Roles are cached in the browser in an in-memory encrypted cookie.  If the
        ' cookie doesn't exist yet for this session, create it.
        '
        '*********************************************************************

        Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)

            If Request.IsAuthenticated = True Then

                Dim roles() As String

                ' Create the roles cookie if it doesn't exist yet for this session.
                If Request.Cookies("portalroles") Is Nothing Then

                    ' Get roles from UserRoles table, and add to cookie
                    Dim _user As New UsersDB
                    roles = _user.GetRoles(User.Identity.Name)

                    ' Create a string to persist the roles
                    Dim roleStr As String = ""
                    Dim role As String

                    For Each role In roles

                        roleStr += role
                        roleStr += ";"

                    Next role

                    ' Create a cookie authentication ticket.
                    '   version
                    '   user name
                    '   issue time
                    '   expires every hour
                    '   don't persist cookie
                    '   roles
                    Dim ticket As New FormsAuthenticationTicket(1, _
                     Context.User.Identity.Name, _
                     DateTime.Now, _
                     DateTime.Now.AddHours(1), _
                     False, _
                     roleStr)

                    ' Encrypt the ticket
                    Dim cookieStr As String = FormsAuthentication.Encrypt(ticket)

                    ' Send the cookie to the client
                    Response.Cookies("portalroles").Value = cookieStr
                    Response.Cookies("portalroles").Path = "/"
                    Response.Cookies("portalroles").Expires = DateTime.Now.AddMinutes(1)

                Else

                    ' Get roles from roles cookie
                    Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Context.Request.Cookies("portalroles").Value)

                    'convert the string representation of the role data into a string array
                    Dim userRoles As New ArrayList

                    Dim role As String

                    For Each role In ticket.UserData.Split(New Char() {";"c})
                        userRoles.Add(role)
                    Next role

                    roles = CType(userRoles.ToArray(GetType(String)), String())

                End If

                ' Add our own custom principal to the request containing the roles in the auth ticket
                Context.User = New GenericPrincipal(Context.User.Identity, roles)

            End If

        End Sub

        '*********************************************************************
        '
        ' GetApplicationPath Method
        '
        ' This method returns the correct relative path when installing
        ' the portal on a root web site instead of virtual directory
        '
        '*********************************************************************
        Public Shared Function GetApplicationPath(ByVal request As HttpRequest) As String
            Dim path As String = String.Empty
            Try
                If request.ApplicationPath <> "/" Then
                    path = request.ApplicationPath
                End If
            Catch e As Exception
                Throw e
            End Try

            Return path
        End Function 'GetApplicationPath

    End Class

End Namespace
