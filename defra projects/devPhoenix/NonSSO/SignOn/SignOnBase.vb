Imports System.Security
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Web

Public Class SignOnBase

    Protected Shared Sub SetPermissions(ByVal userName As String)

        Dim Roles() As String

        If HttpContext.Current.Request.Cookies("Phoenix_Roles") Is Nothing Then


            ' use it to get the roles
            Dim ws As Security.SecurityWse
            ws = CType(uk.gov.defra.Phoenix.PhoenixCommonCode.Common.CreateWebService(New Security.SecurityWse, "SecurityServiceURL"), Security.SecurityWse)
            Dim SOUser As Security.SSOUser = ws.GetUserByName(userName)
            'Dim secUser As ETR.WebServices.General.User = ws.GetUserByUsername(userName)
            ''''Dim ws As ETR.WebServices.General.WebServiceWse = ETR.WebServices.Common.CreateWebService(ETR.WebServices.Common.WebServicesType.General)
            ''''Dim secUser As ETR.WebServices.General.User = ws.getUserByUsername(userName)

            Dim sb As New System.Text.StringBuilder

            If SOUser Is Nothing Then
                FormsAuthentication.SignOut()
                Return
            End If

            ' set mode
            ''''If secUser.IsAdmin Then
            ''''sb.Append("Admin")
            ''''sb.Append("!")
            ''''Else
            sb.Append("User")
            sb.Append("!")
            ''''End If

            ' gather roles
            If Not SOUser.Permissions Is Nothing Then
                For Each perm As Integer In SOUser.Permissions
                    sb.Append(perm)
                    sb.Append(";") ' use ; as the separator
                Next
            End If

            Dim role As String = sb.ToString() ' "1;2;3;4;5;6;7;8;9;10"
            'sb.Append("1;2;3;4;5;6;7;8;9;10")
            'Dim role As String = sb.ToString()

            ' build authentication ticket
            Dim userRoles As New ArrayList

            Dim Ticket As New FormsAuthenticationTicket(1, HttpContext.Current.User.Identity.Name, DateTime.Now, DateTime.Now.AddMinutes(15), False, role)
            Dim CookieStr As String = FormsAuthentication.Encrypt(Ticket)

            Dim tkt As String() = Ticket.UserData.Split(New Char() {"!"c})

            For Each role In tkt(1).Split(New Char() {";"c})
                userRoles.Add(role)
            Next role

            Roles = CType(userRoles.ToArray(GetType(String)), String())
            'HttpContext.Current.Request.Cookies.Add(New HttpCookie("Phoenix_Roles", FormsAuthentication.Encrypt(Ticket)))
        Else

            ' Get roles from roles cookie
            Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies("Phoenix_Roles").Value)

            'convert the string representation of the role data into a string array
            Dim userRoles As New ArrayList

            Dim role As String

            Dim tkt As String() = Ticket.UserData.Split(New Char() {"!"c})

            ' get roles
            For Each role In tkt(1).Split(New Char() {";"c})
                userRoles.Add(role)
            Next role
            Roles = CType(userRoles.ToArray(GetType(String)), String())

        End If

        HttpContext.Current.User = New GenericPrincipal(HttpContext.Current.User.Identity, Roles)

    End Sub


End Class
