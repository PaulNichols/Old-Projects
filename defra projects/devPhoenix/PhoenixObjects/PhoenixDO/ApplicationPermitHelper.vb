Namespace Helpers
    'This class is designed to assist the developer in seperating the application reference into its
    'component parts
    '12345/01 produces ApplicationId = 12345, PermitId = 1
    '12345/2 produces ApplicationId = 12345, PermitId = 2
    '12345 produces ApplicationId = 12345, PermitId = null
    '12345/ produces ApplicationId = 12345, PermitId = null
    '12345/x (where x is anything that cannot be cast to an int) produces ApplicationId = 12345, PermitId = null
    'x (where x is non numeric data) throws an exception 
    Friend Class ApplicationPermitHelper
        Public Sub New(ByVal applicationReference As String)
            If applicationReference.IndexOf("/") >= 0 Then
                mApplicationId = CType(applicationReference.Substring(0, applicationReference.IndexOf("/")), Int32)
                mPermitId = CType(applicationReference.Replace(mApplicationId.ToString & "/", ""), Int32)
            Else
                Try
                    mApplicationId = CType(applicationReference, Int32)
                    mPermitId = Nothing
                Catch ex As Exception
                    Throw New InvalidCastException("The Application Reference " & applicationReference & " is invalid")
                End Try
            End If
        End Sub

        Friend ReadOnly Property ApplicationId() As Int32
            Get
                Return mApplicationId
            End Get
        End Property
        Private mApplicationId As Int32

        Friend ReadOnly Property PermitId() As Object
            Get
                Return mPermitId
            End Get
        End Property
        Private mPermitId As Object
    End Class
End Namespace