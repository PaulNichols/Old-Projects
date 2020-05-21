Namespace Party
    <Serializable()> _
    Public Class PartyBankDetail
        Inherits BaseBO
        Implements IPartyBankDetail

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property AccountNumber() As String Implements IPartyBankDetail.AccountNumber
            Get
                Return mAccountNumber
            End Get
            Set(ByVal Value As String)
                mAccountNumber = Value
            End Set
        End Property
        Private mAccountNumber As String

        Public Property SortCode() As String Implements IPartyBankDetail.SortCode
            Get
                Return mSortCode
            End Get
            Set(ByVal Value As String)
                mSortCode = Value
            End Set
        End Property
        Private mSortCode As String

    End Class
End Namespace