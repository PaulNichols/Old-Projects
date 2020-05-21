Namespace ReportData
    <Serializable()> _
    Public Class BoReportParty
        Implements IReportParty
        Implements ICloneable

        Private mPartyId As Int32

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal partyId As Int32)
            MyBase.New()
            mPartyId = partyId
            InitilizeReportParty()
        End Sub

        Protected Overridable Sub InitilizeReportParty()
            ' Set default Report Party Details
            mPartyAddress = ""
            mAuthorisedPartyId = ""
            mPartyName = ""

            If mPartyId > 0 Then ' Do we have a Valid Party Id?
                SetReportAddress() ' Set Report Party Details for this PartyId
            End If
        End Sub

        Private Sub SetReportAddress()

            Dim sReportParty As String = "" ' Default return if PartyId is 0

            If mPartyId > 0 Then

                ' Get AddressParty object using partyId
                Dim AddressParty As New Party.BOParty(mPartyId)

                ' Get BOReadOnlyAddress object using AddressParty.GetMailingAddresses
                Dim oReadOnlyAddress As BO.Party.BOReadOnlyAddress = New Party.BOReadOnlyAddress(AddressParty.GetMailingAddress(Nothing))
                mPartyAddress = oReadOnlyAddress.ReportAddress()

                ' Get Party Name
                If AddressParty.IsBusiness Then
                    mPartyName = CType(AddressParty, Party.BOPartyBusiness).DisplayName
                Else
                    mPartyName = CType(AddressParty, Party.BOPartyIndividual).DisplayName
                End If

                ' Get Authorised Party Id
                mAuthorisedPartyId = CType(AddressParty.AuthorisedPartyId, String)

            End If


        End Sub

        Public Property AuthorisedPartyId() As String Implements IReportParty.AuthorisedPartyId
            Get
                Return mAuthorisedPartyId
            End Get
            Set(ByVal Value As String)
                mAuthorisedPartyId = Value
                'Throw New NotImplementedException
            End Set
        End Property
        Private mAuthorisedPartyId As String = ""

        Public Property PartyAddress() As String Implements IReportParty.PartyAddress
            Get
                Return mPartyAddress
            End Get
            Set(ByVal Value As String)
                mPartyAddress = Value
                'Throw New NotImplementedException
            End Set
        End Property
        Private mPartyAddress As String = ""

        Public Property PartyName() As String Implements IReportParty.PartyName
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
                'Throw New NotImplementedException
            End Set
        End Property
        Private mPartyName As String = ""

        Public Overridable Function Clone() As Object Implements System.ICloneable.Clone
            Dim oMyClone As New BoReportParty
            oMyClone.PartyAddress = mPartyAddress
            oMyClone.PartyName = mPartyName
            oMyClone.AuthorisedPartyId = mAuthorisedPartyId

            Return oMyClone

        End Function
    End Class

End Namespace
