Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports ASPNET.StarterKit.Portal


Namespace ASPNET.StarterKit.Portal

    '*********************************************************************
    '
    ' ContactDB Class
    '
    ' Class that encapsulates all data logic necessary to add/query/delete
    ' contacts within the Portal database.
    '
    '*********************************************************************

    Public Class ContactsDB


        '*********************************************************************
        '
        ' GetContacts Method
        '
        ' The GetContacts method returns a DataSet containing all of the
        ' contacts for a specific portal module from the contacts
        ' database.
        '
        ' NOTE: A DataSet is returned from this method to allow this method to support
        ' both desktop and mobile Web UI.
        '
        ' Other relevant sources:
        '     + <a href="GetContacts.htm" style="color:green">GetContacts Stored Procedure</a>
        '
        '*********************************************************************

        Public Overloads Function GetContacts(ByVal moduleId As Integer, ByVal filter As String, _
            ByVal userId As Object) As DataSet

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlDataAdapter("Portal_GetContacts", myConnection)

            ' Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterModuleId As SqlParameter
            Dim parameterFilter As SqlParameter
            Dim parameterUserId As SqlParameter

            parameterModuleId = New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleId.Value = moduleId
            parameterFilter = New SqlParameter("@Filter", SqlDbType.VarChar, 255)
            parameterUserId = New SqlParameter("@UserId", SqlDbType.Int, 1)
            If filter = "*" Then
                parameterFilter.Value = ""
            Else
                parameterFilter.Value = filter
            End If
            parameterUserId.Value = userId
            myCommand.SelectCommand.Parameters.Add(parameterModuleId)
            myCommand.SelectCommand.Parameters.Add(parameterFilter)
            myCommand.SelectCommand.Parameters.Add(parameterUserId)

            ' Create and Fill the DataSet
            Dim myDataSet As New DataSet
            myCommand.Fill(myDataSet)
            myConnection.Close()
            ' Return the DataSet
            Return myDataSet
        End Function

        Public Overloads Function GetContacts(ByVal moduleId As Integer, ByVal userid As Object) As DataSet
            Return GetContacts(moduleId, "*", userid)
        End Function


        '*********************************************************************
        '
        ' GetSingleContact Method
        '
        ' The GetSingleContact method returns a SqlDataReader containing details
        ' about a specific contact from the Contacts database table.
        '
        ' Other relevant sources:
        '     + <a href="GetSingleContact.htm" style="color:green">GetSingleContact Stored Procedure</a>
        '
        '*********************************************************************

        Public Function GetSingleContact(ByVal itemId As Integer) As SqlDataReader

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_GetSingleContact", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemId As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemId.Value = itemId
            myCommand.Parameters.Add(parameterItemId)

            ' Execute the command
            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Return the datareader 
            Return result

        End Function


        '*********************************************************************
        '
        ' DeleteContact Method
        '
        ' The DeleteContact method deletes the specified contact from
        ' the Contacts database table.
        '
        ' Other relevant sources:
        '     + <a href="DeleteContact.htm" style="color:green">DeleteContact Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub DeleteContact(ByVal itemID As Integer)

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_DeleteContact", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Value = itemID
            myCommand.Parameters.Add(parameterItemID)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub


        '*********************************************************************
        '
        ' AddContact Method
        '
        ' The AddContact method adds a new contact to the Contacts
        ' database table, and returns the ItemId value as a result.
        '
        ' Other relevant sources:
        '     + <a href="AddContact.htm" style="color:green">AddContact Stored Procedure</a>
        '
        '*********************************************************************
        Public Function AddContact(ByVal moduleId As Integer, ByVal userid As Object, ByVal title As String, _
            ByVal homePhone As String, ByVal userName As String, ByVal companyName As String, _
            ByVal fullName As String, ByVal mobile As String, ByVal salutation As String, _
            ByVal role As String, ByVal address As String, ByVal businessFax As String, ByVal email As String, _
            ByVal homeAddress As String, ByVal homePage As String, ByVal firstName As String, ByVal suffix As String, _
            ByVal surname As String, ByVal fileAs As String, ByVal businessPhone As String, ByVal homeFax As String, _
            ByVal FaxAbbreviation As String, ByVal PhoneAbbreviation As String)

            If userName.Length < 1 Then
                userName = "unknown"
            End If

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_AddContact", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Direction = ParameterDirection.Output
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterModuleID As New SqlParameter("@ModuleID", SqlDbType.Int, 4)
            parameterModuleID.Value = moduleId
            myCommand.Parameters.Add(parameterModuleID)

            Dim parameterUserName As New SqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100)
            parameterUserName.Value = userName
            myCommand.Parameters.Add(parameterUserName)

            Dim parameterhomefax As New SqlParameter("@homefax", SqlDbType.NVarChar, 250)
            parameterhomefax.Value = homeFax
            myCommand.Parameters.Add(parameterhomefax)

            Dim parameterFaxAbbreviation As New SqlParameter("@FaxAbbreviation", SqlDbType.NVarChar, 10)
            parameterFaxAbbreviation.Value = FaxAbbreviation
            myCommand.Parameters.Add(parameterFaxAbbreviation)

            Dim parameterPhoneAbbreviation As New SqlParameter("@PhoneAbbreviation", SqlDbType.NVarChar, 10)
            parameterPhoneAbbreviation.Value = PhoneAbbreviation
            myCommand.Parameters.Add(parameterPhoneAbbreviation)

            Dim parameterName As New SqlParameter("@FullName", SqlDbType.NVarChar, 250)
            parameterName.Value = fullName
            myCommand.Parameters.Add(parameterName)

            Dim parameterMobile As New SqlParameter("@Mobile", SqlDbType.NVarChar, 250)
            parameterMobile.Value = mobile
            myCommand.Parameters.Add(parameterMobile)

            Dim parameterSalutation As New SqlParameter("@Salutation", SqlDbType.NVarChar, 250)
            parameterSalutation.Value = salutation
            myCommand.Parameters.Add(parameterSalutation)

            Dim parameterFirstName As New SqlParameter("@FirstName", SqlDbType.NVarChar, 250)
            parameterFirstName.Value = firstName
            myCommand.Parameters.Add(parameterFirstName)

            Dim parameterBusinessAddress As New SqlParameter("@BusinessAddress", SqlDbType.NVarChar, 250)
            parameterBusinessAddress.Value = address
            myCommand.Parameters.Add(parameterBusinessAddress)

            Dim parameterBusinessPhone As New SqlParameter("@BusinessPhone", SqlDbType.NVarChar, 250)
            parameterBusinessPhone.Value = businessPhone
            myCommand.Parameters.Add(parameterBusinessPhone)

            Dim parameterFileAs As New SqlParameter("@FileAs", SqlDbType.NVarChar, 250)
            parameterFileAs.Value = fileAs
            myCommand.Parameters.Add(parameterFileAs)

            Dim parameterSurname As New SqlParameter("@LastName", SqlDbType.NVarChar, 250)
            parameterSurname.Value = surname
            myCommand.Parameters.Add(parameterSurname)

            Dim parameterSuffix As New SqlParameter("@Suffix", SqlDbType.NVarChar, 250)
            parameterSuffix.Value = suffix
            myCommand.Parameters.Add(parameterSuffix)

            Dim parameterHomePage As New SqlParameter("@BusinessHomePage", SqlDbType.NVarChar, 250)
            parameterHomePage.Value = homePage
            myCommand.Parameters.Add(parameterHomePage)

            Dim parameterHomeAddress As New SqlParameter("@HomeAddress", SqlDbType.NVarChar, 250)
            parameterHomeAddress.Value = homeAddress
            myCommand.Parameters.Add(parameterHomeAddress)


            Dim parameterBusinessFax As New SqlParameter("@BusinessFax", SqlDbType.NVarChar, 250)
            parameterBusinessFax.Value = businessFax
            myCommand.Parameters.Add(parameterBusinessFax)

            Dim parameterHomePhone As New SqlParameter("@HomePhone", SqlDbType.NVarChar, 250)
            parameterHomePhone.Value = homePhone
            myCommand.Parameters.Add(parameterHomePhone)

            Dim parameterCompanyName As New SqlParameter("@CompanyName", SqlDbType.NVarChar, 250)
            parameterCompanyName.Value = companyName
            myCommand.Parameters.Add(parameterCompanyName)

            Dim parameterTitle As New SqlParameter("@Title", SqlDbType.NVarChar, 250)
            parameterTitle.Value = title
            myCommand.Parameters.Add(parameterTitle)

            Dim parameterPublic As New SqlParameter("@Userid", SqlDbType.Int, 1)
            parameterPublic.Value = userid
            myCommand.Parameters.Add(parameterPublic)

            Dim parameterRole As New SqlParameter("@Role", SqlDbType.NVarChar, 250)
            parameterRole.Value = role
            myCommand.Parameters.Add(parameterRole)

            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 250)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)

            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

            Return CInt(parameterItemID.Value)

        End Function


        '*********************************************************************
        '
        ' UpdateContact Method
        '
        ' The UpdateContact method updates the specified contact within
        ' the Contacts database table.
        '
        ' Other relevant sources:
        '     + <a href="UpdateContact.htm" style="color:green">UpdateContact Stored Procedure</a>
        '
        '*********************************************************************

        Public Sub UpdateContact(ByVal moduleId As Integer, ByVal userid As Object, ByVal itemId As Integer, ByVal title As String, _
            ByVal homePhone As String, ByVal userName As String, ByVal companyName As String, _
            ByVal fullName As String, ByVal mobile As String, ByVal salutation As String, _
            ByVal role As String, ByVal address As String, ByVal businessFax As String, ByVal email As String, _
            ByVal homeAddress As String, ByVal homePage As String, ByVal firstName As String, ByVal suffix As String, _
            ByVal surname As String, ByVal fileAs As String, ByVal businessphone As String, ByVal homefax As String, _
            ByVal FaxAbbreviation As String, ByVal PhoneAbbreviation As String)


            If userName.Length < 1 Then
                userName = "unknown"
            End If

            ' Create Instance of Connection and Command Object
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("connectionString"))
            Dim myCommand As New SqlCommand("Portal_UpdateContact", myConnection)

            ' Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim parameterItemID As New SqlParameter("@ItemID", SqlDbType.Int, 4)
            parameterItemID.Value = itemId
            myCommand.Parameters.Add(parameterItemID)

            Dim parameterUserName As New SqlParameter("@UserName", SqlDbType.NVarChar, 100)
            parameterUserName.Value = userName
            myCommand.Parameters.Add(parameterUserName)

            Dim parameterhomefax As New SqlParameter("@homefax", SqlDbType.NVarChar, 250)
            parameterhomefax.Value = homefax
            myCommand.Parameters.Add(parameterhomefax)

            Dim parameterFaxAbbreviation As New SqlParameter("@FaxAbbreviation", SqlDbType.NVarChar, 10)
            parameterFaxAbbreviation.Value = FaxAbbreviation
            myCommand.Parameters.Add(parameterFaxAbbreviation)

            Dim parameterPhoneAbbreviation As New SqlParameter("@PhoneAbbreviation", SqlDbType.NVarChar, 10)
            parameterPhoneAbbreviation.Value = PhoneAbbreviation
            myCommand.Parameters.Add(parameterPhoneAbbreviation)

            Dim parameterName As New SqlParameter("@FullName", SqlDbType.NVarChar, 250)
            parameterName.Value = fullName
            myCommand.Parameters.Add(parameterName)

            Dim parameterMobile As New SqlParameter("@Mobile", SqlDbType.NVarChar, 250)
            parameterMobile.Value = mobile
            myCommand.Parameters.Add(parameterMobile)

            Dim parameterSalutation As New SqlParameter("@Salutation", SqlDbType.NVarChar, 250)
            parameterSalutation.Value = salutation
            myCommand.Parameters.Add(parameterSalutation)

            Dim parameterFirstName As New SqlParameter("@FirstName", SqlDbType.NVarChar, 250)
            parameterFirstName.Value = firstName
            myCommand.Parameters.Add(parameterFirstName)

            Dim parameterBusinessAddress As New SqlParameter("@BusinessAddress", SqlDbType.NVarChar, 250)
            parameterBusinessAddress.Value = address
            myCommand.Parameters.Add(parameterBusinessAddress)

            Dim parameterBusinessPhone As New SqlParameter("@BusinessPhone", SqlDbType.NVarChar, 250)
            parameterBusinessPhone.Value = businessphone
            myCommand.Parameters.Add(parameterBusinessPhone)

            Dim parameterFileAs As New SqlParameter("@FileAs", SqlDbType.NVarChar, 250)
            parameterFileAs.Value = fileAs
            myCommand.Parameters.Add(parameterFileAs)

            Dim parameterSurname As New SqlParameter("@LastName", SqlDbType.NVarChar, 250)
            parameterSurname.Value = surname
            myCommand.Parameters.Add(parameterSurname)

            Dim parameterSuffix As New SqlParameter("@Suffix", SqlDbType.NVarChar, 250)
            parameterSuffix.Value = suffix
            myCommand.Parameters.Add(parameterSuffix)

            Dim parameterHomePage As New SqlParameter("@BusinessHomePage", SqlDbType.NVarChar, 250)
            parameterHomePage.Value = homePage
            myCommand.Parameters.Add(parameterHomePage)

            Dim parameterHomeAddress As New SqlParameter("@HomeAddress", SqlDbType.NVarChar, 250)
            parameterHomeAddress.Value = homeAddress
            myCommand.Parameters.Add(parameterHomeAddress)


            Dim parameterBusinessFax As New SqlParameter("@BusinessFax", SqlDbType.NVarChar, 250)
            parameterBusinessFax.Value = businessFax
            myCommand.Parameters.Add(parameterBusinessFax)

            Dim parameterHomePhone As New SqlParameter("@HomePhone", SqlDbType.NVarChar, 250)
            parameterHomePhone.Value = homePhone
            myCommand.Parameters.Add(parameterHomePhone)

            Dim parameterCompanyName As New SqlParameter("@CompanyName", SqlDbType.NVarChar, 250)
            parameterCompanyName.Value = companyName
            myCommand.Parameters.Add(parameterCompanyName)

            Dim parameterTitle As New SqlParameter("@Title", SqlDbType.NVarChar, 250)
            parameterTitle.Value = title
            myCommand.Parameters.Add(parameterTitle)

            Dim parameterPublic As New SqlParameter("@userid", SqlDbType.Int, 1)
            parameterPublic.Value = userid
            myCommand.Parameters.Add(parameterPublic)

            Dim parameterRole As New SqlParameter("@Role", SqlDbType.NVarChar, 250)
            parameterRole.Value = role
            myCommand.Parameters.Add(parameterRole)

            Dim parameterEmail As New SqlParameter("@Email", SqlDbType.NVarChar, 250)
            parameterEmail.Value = email
            myCommand.Parameters.Add(parameterEmail)


            myConnection.Open()
            myCommand.ExecuteNonQuery()
            myConnection.Close()

        End Sub

    End Class

End Namespace
