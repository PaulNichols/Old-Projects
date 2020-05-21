Namespace Application.Search.Data
    Public MustInherit Class CITESSeizureNotificationSearchData
        Inherits BaseCITESNotificationSearchData
        Implements ICustomsRef, IDateSeized

        Public Sub New()
        End Sub

        Public Property CustomsRef() As String Implements ICustomsRef.CustomsRef
            Get
                Return mCustomsRef
            End Get
            Set(ByVal Value As String)
                mCustomsRef = Value
            End Set
        End Property
        Private mCustomsRef As String

        Public Property DateSeized() As String Implements IDateSeized.DateSeized
            Get
                Return mDateSeized
            End Get
            Set(ByVal Value As String)
                mDateSeized = Value
            End Set
        End Property
        Private mDateSeized As String
    End Class
End Namespace