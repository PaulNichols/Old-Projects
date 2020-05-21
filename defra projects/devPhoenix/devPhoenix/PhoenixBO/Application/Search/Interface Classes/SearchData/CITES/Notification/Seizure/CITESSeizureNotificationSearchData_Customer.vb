Namespace Application.Search.Data
    Public Class CITESSeizureNotificationSearchData_Customer
        Inherits CITESSeizureNotificationSearchData
        Implements IISOCodeDescription

        Public Sub New()
        End Sub

        Public Property ISOCodeDescription() As String Implements IISOCodeDescription.ISOCodeDescription
            Get
                Return mISOCodeDescription
            End Get
            Set(ByVal Value As String)
                mISOCodeDescription = Value
            End Set
        End Property
        Private mISOCodeDescription As String
    End Class
End Namespace