Namespace Application.Search.Data
    Public Class CITESImportNotificationSearchData_CaseOfficer
        Inherits CITESImportNotificationSearchData
        Implements ISearchDataParty, IPD, IDateOfImport, IISOCode

        Public Sub New()
        End Sub

        Public Property PartyId() As String Implements ISearchDataParty.PartyId
            Get
                Return mPartyId
            End Get
            Set(ByVal Value As String)
                mPartyId = Value
            End Set
        End Property
        Private mPartyId As String

        Public Property PartyName() As String Implements ISearchDataParty.PartyName
            Get
                Return mPartyName
            End Get
            Set(ByVal Value As String)
                mPartyName = Value
            End Set
        End Property
        Private mPartyName As String

        Public Property PD() As String Implements IPD.PD
            Get
                Return mPD
            End Get
            Set(ByVal Value As String)
                mPD = Value
            End Set
        End Property
        Private mPD As String

        Public Property DateOfImport() As String Implements IDateOfImport.DateOfImport
            Get
                Return mDateOfImport
            End Get
            Set(ByVal Value As String)
                mDateOfImport = Value
            End Set
        End Property
        Private mDateOfImport As String

        Public Property ISOCode() As String Implements IISOCode.ISOCode
            Get
                Return mISOCode
            End Get
            Set(ByVal Value As String)
                mISOCode = Value
            End Set
        End Property
        Private mISOCode As String
    End Class
End Namespace