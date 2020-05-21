Namespace Party
    <Serializable()> _
    Public Class BOPartyIndividual
        Inherits BOParty
        Implements IPerson


        Public PlaceOfBirth As String

        Private mPerson As IPerson
        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New(id, tran)
        End Sub

        Private Sub CreateEmptyPerson()
            If mPerson Is Nothing Then
                mPerson = New Party.BOPerson
            End If
        End Sub

        Public Overrides Property DisplayName() As String Implements IPerson.DisplayName
            Get
                CreateEmptyPerson()
                Return mPerson.DisplayName
            End Get
            Set(ByVal Value As String)
                CreateEmptyPerson()
                mPerson.DisplayName = Value
            End Set
        End Property

        Public Property Forename() As Object Implements IPerson.Forename
            Get
                CreateEmptyPerson()
                Return mPerson.Forename
            End Get
            Set(ByVal Value As Object)
                CreateEmptyPerson()
                mPerson.Forename = Value
            End Set
        End Property

        Public Property PersonId() As Integer Implements IPerson.PersonId
            Get
                CreateEmptyPerson()
                Return mPerson.PersonId
            End Get
            Set(ByVal Value As Integer)
                CreateEmptyPerson()
                mPerson.PersonId = Value
            End Set
        End Property

        Public Property PreferredContactId() As Object Implements IPerson.PreferredContactId
            Get
                CreateEmptyPerson()
                Return mPerson.PreferredContactId
            End Get
            Set(ByVal Value As Object)
                CreateEmptyPerson()
                mPerson.PreferredContactId = Value
            End Set
        End Property

        Public Property Surname() As Object Implements IPerson.Surname
            Get
                CreateEmptyPerson()
                Return mPerson.Surname
            End Get
            Set(ByVal Value As Object)
                CreateEmptyPerson()
                mPerson.Surname = Value
            End Set
        End Property

        Public Property Title() As String Implements IPerson.Title
            Get
                CreateEmptyPerson()
                Return mPerson.Title
            End Get
            Set(ByVal Value As String)
                CreateEmptyPerson()
                mPerson.Title = Value
            End Set
        End Property

        Public Property TitleId() As Int32 Implements IPerson.TitleId
            Get
                CreateEmptyPerson()
                Return mPerson.TitleId
            End Get
            Set(ByVal Value As Int32)
                CreateEmptyPerson()
                mPerson.TitleId = Value
            End Set
        End Property

        <Xml.Serialization.XmlIgnore()> _
        Public Shadows Property PartyId() As Object Implements IPerson.PartyId
            Get
                Return MyBase.PartyId
            End Get
            Set(ByVal Value As Object)
                MyBase.PartyId = CType(Value, Int32)
            End Set
        End Property

        Protected Overloads Overrides Sub InitialiseParty(ByVal party As DataObjects.Entity.Party, ByVal userId As Int64, ByVal tran As SqlClient.SqlTransaction)
            MyBase.InitialiseParty(party, userId, tran)
            Try
                mPerson = GetPersons(party.Id, tran)(0)
            Catch
                mPerson = New BOPerson
            End Try
        End Sub

        Public Function GetContacts() As BOContact() Implements IPerson.GetContacts
            If mPerson Is Nothing Then
                Return Nothing
            Else
                CreateEmptyPerson()
                Return mPerson.GetContacts
            End If
        End Function

        Private Function SavePerson() As BOPerson Implements IPerson.Save
            Return SavePerson(Nothing)
        End Function

        Private Function SavePerson(ByVal tran As SqlClient.SqlTransaction) As BOPerson Implements IPerson.Save
            If Not mPerson Is Nothing Then
                If mPerson.PartyId Is Nothing Then mPerson.PartyId = Me.PartyId
                If tran Is Nothing Then
                    Return mPerson.Save()
                Else
                    Return mPerson.Save(tran)
                End If
            Else
                Return Nothing
            End If
        End Function

        Protected Overrides Function PreSave(ByVal tran As SqlClient.SqlTransaction) As Boolean
            Return (Not SavePerson(tran) Is Nothing)
        End Function

        Protected Overloads Overrides Function Validate(ByVal userID As Int64) As ValidationManager
            Dim Valid As ValidationManager = MyBase.Validate(userID)
            If Valid Is Nothing Then
                'the base class is valid, so check mine
                If Not mPerson Is Nothing Then
                    Return CType(mPerson, IValidate).Validate()
                Else
                    Return Nothing
                End If
            Else
                Return Valid
            End If
        End Function

        'Public Overrides Function Delete() As Object
        '    Stop
        '    MyBase.Delete()
        'End Function
        Public Overloads Function Validate(ByVal userid As Int64, ByVal secure As Boolean) As ValidationManager Implements IPerson.Validate

        End Function

        Protected Overloads Overrides Function Validate(ByVal writeFlag As Boolean, ByVal userid As Int64, ByVal secure As Boolean) As ValidationManager
            Dim Valid As ValidationManager = MyBase.Validate(writeFlag, userid, secure)
            If Valid Is Nothing Then
                'the base class is valid, so check mine
                If Not mPerson Is Nothing Then
                    Return mPerson.Validate(userid, secure)
                Else
                    Return Nothing
                End If
            Else
                Return Valid
            End If
        End Function
    End Class
End Namespace