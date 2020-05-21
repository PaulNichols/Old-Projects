Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class PreviousKeeperAddress
        Public Sub New()

        End Sub

        Public Function GetAddress() As String
            Return PreviousKeeperAddress.GetAddress(Me)
        End Function

        Public Shared Function GetAddress(ByVal address As PreviousKeeperAddress) As String
            Dim Output As New System.IO.StringWriter(New System.Text.StringBuilder)
            Dim Serilizer As New System.Xml.Serialization.XmlSerializer(address.GetType())
            Serilizer.Serialize(Output, address)

            Return Output.ToString()
        End Function

        Public Shared Function SetAddress(ByVal address As String) As PreviousKeeperAddress
            Dim Input As New System.IO.StringReader(address)

            Dim doc As New System.Xml.XmlDocument
            doc.LoadXml(address)
            Dim XMLName As String = doc.ChildNodes.Item(1).Name
            doc = Nothing

            Dim NewInstance As Type = GetTypeByName(XMLName)

            If (NewInstance Is Nothing) Then
                Throw New TypeLoadException("Type not recognized: " + XMLName)
            Else
                Dim Serilizer As New System.Xml.Serialization.XmlSerializer(NewInstance)
                Return CType(Serilizer.Deserialize(Input), PreviousKeeperAddress)
            End If
        End Function

        Private Shared Function GetTypeByName(ByVal name As String) As Type
            Dim ReturnVal As Type = Nothing
            Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim ts As System.Type() = ass.GetTypes()

            For Each t As Type In ts
                If (String.Compare(t.Name, name) = 0) Then
                    ReturnVal = t
                    Exit For
                End If
            Next t
            Return ReturnVal
        End Function

        Public Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal Value As String)
                mName = Value
            End Set
        End Property
        Private mName As String

        Public Property HouseNumber() As String
            Get
                Return mHouseNumber
            End Get
            Set(ByVal Value As String)
                mHouseNumber = Value
            End Set
        End Property
        Private mHouseNumber As String

        Public Property Address1() As String
            Get
                Return mAddress1
            End Get
            Set(ByVal Value As String)
                mAddress1 = Value
            End Set
        End Property
        Private mAddress1 As String

        Public Property Address2() As String
            Get
                Return mAddress2
            End Get
            Set(ByVal Value As String)
                mAddress2 = Value
            End Set
        End Property
        Private mAddress2 As String

        Public Property Address3() As String
            Get
                Return mAddress3
            End Get
            Set(ByVal Value As String)
                mAddress3 = Value
            End Set
        End Property
        Private mAddress3 As String

        Public Property Address4() As String
            Get
                Return mAddress4
            End Get
            Set(ByVal Value As String)
                mAddress4 = Value
            End Set
        End Property
        Private mAddress4 As String

        Public Property Town() As String
            Get
                Return mTown
            End Get
            Set(ByVal Value As String)
                mTown = Value
            End Set
        End Property
        Private mTown As String

        Public Property PostCode() As String
            Get
                Return mPostCode
            End Get
            Set(ByVal Value As String)
                mPostCode = Value
            End Set
        End Property
        Private mPostCode As String

        Public Property Country() As String
            Get
                Return mCountry
            End Get
            Set(ByVal Value As String)
                mCountry = Value
            End Set
        End Property
        Private mCountry As String
    End Class
End Namespace