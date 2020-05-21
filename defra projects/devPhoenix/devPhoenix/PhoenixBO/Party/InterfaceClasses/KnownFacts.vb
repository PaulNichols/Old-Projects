Namespace Party
    Public Class BOKnownFacts
        Inherits BaseBO

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal myEnt As DataObjects.Entity.tblKnownFacts)
            MyClass.New()
            Initialise(myEnt)
        End Sub

        Private Sub Initialise(ByVal myEnt As DataObjects.Entity.tblKnownFacts)
            With myEnt
                Initialise(.Id, .KnownFact1, .KnownFact2, .PartyId, .SSOKnownFactId, .SSOUserId, .CheckSum)
            End With
        End Sub

        Private Sub initialise(ByVal pID As Integer, ByVal pKnownfact1 As String, ByVal pKnownfact2 As String, ByVal pPartyid As Integer, ByVal pKnownfactid As Object, ByVal pSSOUserid As Object, ByVal intChecksum As Integer)
            Id = pID
            KnownFact1 = pKnownfact1
            KnownFact2 = pKnownfact2
            PartyId = pPartyid
            SSOKnownFactId = pKnownfactid
            SSOUserId = pSSOUserid
            CheckSum = intChecksum
        End Sub

        Public Sub New(ByVal knownfact1 As String, ByVal knownfact2 As String, ByVal partyid As Integer)
            MyClass.new()
            Initialise(0, knownfact1, knownfact2, partyid, Nothing, Nothing, 0)
        End Sub

        Public Sub New(ByVal id As Integer, ByVal knownfact1 As String, ByVal knownfact2 As String, ByVal partyid As Integer, ByVal knownfactid As Object, ByVal ssouserid As Object)
            'Will normally ownly be kicked off by New with KnownFacts and Party Id's
            MyClass.New()
            Initialise(id, knownfact1, knownfact2, partyid, knownfactid, ssouserid, 0)
        End Sub

        Public Property PhoenixFactId() As Integer
            Get
                Return mFactId
            End Get
            Set(ByVal Value As Integer)
                mFactId = Value
            End Set
        End Property
        Private mFactId As Integer

        Public Property Id() As Integer
            Get
                Return mFactId
            End Get
            Set(ByVal Value As Integer)
                mFactId = Value
            End Set
        End Property

        Public Property KnownFact1() As String
            Get
                Return mKnownFact1
            End Get
            Set(ByVal Value As String)
                mKnownFact1 = Value
            End Set
        End Property
        Private mKnownFact1 As String

        Public Property KnownFact2() As String
            Get
                Dim strRC As String = String.Empty
                Try
                    strRC = mKnownFact2
                Catch ex As Exception
                End Try
                Return strRC
            End Get
            Set(ByVal Value As String)
                mKnownFact2 = Value
            End Set
        End Property
        Private mKnownFact2 As String

        Public Overridable Property UpdateKnownFact() As Boolean
            Get
                Return mblnUpdate
            End Get
            Set(ByVal Value As Boolean)
                mblnUpdate = Value
            End Set
        End Property
        Private mblnUpdate As Boolean = False

        Public Property PartyId() As Integer
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As Integer)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As Integer

        Public Property SSOKnownFactId() As Object
            Get
                Return mSSOKnownFactId
            End Get
            Set(ByVal Value As Object)
                mSSOKnownFactId = Value
            End Set
        End Property
        Private mSSOKnownFactId As Object

        Public Property SSOUserId() As Object
            Get

                Return mSSOUserId
            End Get
            Set(ByVal Value As Object)
                mSSOUserId = Value
            End Set
        End Property
        Private mSSOUserId As Object

        Public Shadows Function Save() As BaseBO
            Return Me.Save(Nothing)
        End Function

        Public Shadows Function Save(ByVal tran As System.Data.SqlClient.SqlTransaction) As BaseBO
            If mblnUpdate Then
                If (PhoenixFactId = 0) AndAlso (KnownFact1.ToString.Length > 0 AndAlso KnownFact2.Length > 0) Then
                    Id = DataObjects.Sprocs.eosp_CreatetblKnownFacts(KnownFact1, KnownFact2, mPartyId, SSOKnownFactId, SSOUserId, tran)
                Else
                    DataObjects.Sprocs.eosp_UpdatetblKnownFacts(PhoenixFactId, KnownFact1, KnownFact2, PartyId, SSOKnownFactId, SSOUserId, CheckSum, tran)
                End If
            End If

            mblnUpdate = False
        End Function
    End Class
End Namespace