Namespace Party.Search
    Public MustInherit Class SearchParameters
        Public Sub New()
        End Sub
        Public Sub New(ByVal ISO2 As String, ByVal houseNumber As String, ByVal address1 As String, ByVal address2 As String, ByVal address3 As String, ByVal address4 As String, ByVal postcode As String, ByVal contactDetail As String)
            Me.Address1 = address1
            Me.Address2 = address2
            Me.Address3 = address3
            Me.Address4 = address4
            Me.Postcode = postcode
            Me.ISO2 = ISO2
            Me.ContactDetail = contactDetail
            Me.HouseNumber = houseNumber
        End Sub

        Public Authorised As DataObjects.Views.Service.SearchPartyService.AuthorisedLoadType
        Public Address1 As String
        Public Address2 As String
        Public Address3 As String
        Public Address4 As String
        Public Postcode As String
        Public ISO2 As String
        Public ContactDetail As String
        Public HouseNumber As String

        Public Overrides Function ToString() As String
            Return String.Concat("HouseNumber", HouseNumber, Environment.NewLine, _
                                    "Address1: ", Address1, Environment.NewLine, _
                                 "Address2: ", Address2, Environment.NewLine, _
                                 "Address3: ", Address3, Environment.NewLine, _
                                 "Address4: ", Address4, Environment.NewLine, _
                                 "Postcode: ", ISO2, Environment.NewLine, _
                                 "ISO: ", Postcode, Environment.NewLine, _
                                 "Contact Detail: ", ContactDetail)
        End Function
    End Class

    Public Class SearchParameters_Both
        Inherits SearchParameters
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal name As String, ByVal ISO2 As String, ByVal houseNumber As String, ByVal address1 As String, ByVal address2 As String, ByVal address3 As String, ByVal address4 As String, ByVal postcode As String, ByVal contactDetail As String)
            MyBase.New(ISO2, houseNumber, address1, address2, address3, address4, postcode, contactDetail)
            Me.Name = name
        End Sub

        Public Name As String

        Public Overrides Function ToString() As String
            Dim Result As String = MyBase.ToString
            Return String.Concat("Both - ", Environment.NewLine, _
                                 "Name: ", Name, Environment.NewLine, _
                                 Result)
        End Function

    End Class

    Public Class SearchParameters_Business
        Inherits SearchParameters
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal businessName As String, ByVal ISO2 As String, ByVal houseNumber As String, ByVal address1 As String, ByVal address2 As String, ByVal address3 As String, ByVal address4 As String, ByVal postcode As String, ByVal contactDetail As String, ByVal BusinessTypeID As Int32)
            MyBase.New(ISO2, houseNumber, address1, address2, address3, address4, postcode, contactDetail)
            Me.BusinessName = businessName
            Me.BusinessTypeID = BusinessTypeID
        End Sub

        Public BusinessName As String
        Public BusinessTypeID As Int32


        Public Overrides Function ToString() As String
            Dim Result As String = MyBase.ToString
            Return String.Concat("Business - ", Environment.NewLine, _
                                 "Business Name: ", BusinessName, Environment.NewLine, _
                                 Result)
        End Function

    End Class

    Public Class SearchParameters_Individual
        Inherits SearchParameters
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal ISO2 As String, ByVal forename As String, ByVal surname As String, ByVal housenumber As String, ByVal address1 As String, ByVal address2 As String, ByVal address3 As String, ByVal address4 As String, ByVal postcode As String, ByVal contactDetail As String)
            MyBase.New(ISO2, HouseNumber, address1, address2, address3, address4, postcode, contactDetail)
            Me.Forename = forename
            Me.Surname = surname
        End Sub

        Public Forename As String
        Public Surname As String

        Public Overrides Function ToString() As String
            Dim Result As String = MyBase.ToString
            Return String.Concat("Individual - ", Environment.NewLine, _
                                 "Forename: ", Forename, Environment.NewLine, _
                                 "Surname: ", Surname, Environment.NewLine, _
                                 Result)
        End Function
    End Class
End Namespace