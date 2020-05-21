Namespace TaxonomyData
    <Serializable()> Public Class BOManifest
        Implements IManifest, IDisposable

#Region "Prelim code"
        Public Sub New()

        End Sub

        Public Sub New(ByVal NewXML As System.IO.MemoryStream)
            Try
                ValidateAgainstManifestSchema(NewXML)
            Catch Ex As Exception
                Throw New Exception("Cannot create new BOManifest object", Ex)
            End Try
        End Sub

#End Region

#Region "Methods"
        Friend Function Serialise() As String
            Try
                Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(Me.GetType)
                Dim sw As System.IO.StringWriter = New System.IO.StringWriter
                xs.Serialize(sw, Me)
                Return sw.ToString
            Catch ex As Exception
                Throw New Exception("Cannot serialise BOManifest object", ex)
            End Try
        End Function

        Friend Shared Function Deserialise(ByVal SerialisedObject As String) As BOManifest
            Try
                Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(BOManifest))
                Dim sr As System.IO.StringReader = New System.IO.StringReader(SerialisedObject)
                Return CType(xs.Deserialize(sr), BOManifest)
            Catch ex As Exception
                Throw New Exception("Cannot deserialise the BOManifest object", ex)
            End Try
        End Function

#End Region

#Region "Properties"

        Private Property TheManifest() As TaxonomyData.Manifest
            Get
                If mTheManifest Is Nothing = True Then
                    mTheManifest = New TaxonomyData.Manifest
                End If
                Return mTheManifest
            End Get
            Set(ByVal Value As TaxonomyData.Manifest)
                mTheManifest = Value
            End Set
        End Property
        Private mTheManifest As TaxonomyData.Manifest

        Friend ReadOnly Property XML() As String
            Get
                Return Me.TheManifest.GetXml
            End Get
        End Property

        Friend ReadOnly Property Entries() As IEnumerable
            Get
                Return TheManifest.Row.Rows
            End Get
        End Property

#End Region

#Region "Helper Functions"


        Private Sub ValidateAgainstManifestSchema(ByVal XMLtoValidate As System.IO.MemoryStream)
            Try
                'Validate the manifest against the manifest schema.
                Me.TheManifest.ReadXml(XMLtoValidate)
            Catch ex As Exception
                Throw New Exception("Cannot validate manifest against the manifest schema", ex)
            End Try
        End Sub

        Private Sub ValidateAgainstManifestSchema(ByVal XMLtoValidate As String)
            Try
                'Validate the manifest against the manifest schema.
                Dim TheStringReader As New System.io.StringReader(XMLtoValidate)
                Dim TheXMLTextReader As New System.xml.XmlTextReader(TheStringReader)
                Me.TheManifest.ReadXml(TheXMLTextReader)
            Catch ex As Exception
                Throw New Exception("Cannot validate manifest against the manifest schema", ex)
            End Try
        End Sub

#End Region


        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not mTheManifest Is Nothing Then
                mTheManifest.Dispose()
            End If
        End Sub
    End Class
End Namespace