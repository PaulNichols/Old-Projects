Namespace Party
    Public Class BOUKCountry
        Inherits BaseBO
        Implements IBOUKCountry


        Public Shared Function GetAll(ByVal includeHyphen As Boolean) As [DO].DataObjects.Collection.ukCountryBoundCollection
            Return DataObjects.Entity.UKCountry.GetAll(includeHyphen).Entities
        End Function

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal data As DataObjects.Entity.UKCountry)
            MyClass.New()
            InitialiseUKCountry(data)
        End Sub

        Private Sub InitialiseUKCountry(ByVal ukCountry As DataObjects.Entity.UKCountry)
            With ukCountry
                UKCountryId = .Id
                mUKCountryName = .UKCountryName
                CheckSum = .CheckSum
            End With
        End Sub

        Public Property UKCountryId() As Integer Implements IBOUKCountry.UKCountryId
            Get
                Return mUKCountryId
            End Get
            Set(ByVal Value As Integer)
                mUKCountryId = Value
            End Set
        End Property
        Private mUKCountryId As Int32

        Public Property UKCountryName() As String Implements IBOUKCountry.UKCountryName
            Get
                Return mUKCountryName
            End Get
            Set(ByVal Value As String)
                mUKCountryName = Value
            End Set
        End Property
        Private mUKCountryName As String
    End Class
End Namespace