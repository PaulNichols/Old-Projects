Namespace Application.Search.Data
    Public Class CITESSeizureNotificationSearchData_CaseOfficer
        Inherits CITESSeizureNotificationSearchData_Inspectorate
        Implements IPD, ILinked, IActive

        Public Sub New()
        End Sub

        Public Property Linked() As String Implements ILinked.Linked
            Get
                Return mLinked
            End Get
            Set(ByVal Value As String)
                mLinked = Value
            End Set
        End Property
        Private mLinked As String

        Public Property PD() As String Implements IPD.PD
            Get
                Return mPD
            End Get
            Set(ByVal Value As String)
                mPD = Value
            End Set
        End Property
        Private mPD As String

        Public Property Active() As String Implements IActive.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As String)
                mActive = Value
            End Set
        End Property
        Private mActive As String

    End Class
End Namespace
