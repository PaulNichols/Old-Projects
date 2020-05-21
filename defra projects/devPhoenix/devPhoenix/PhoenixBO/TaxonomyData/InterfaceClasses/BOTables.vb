Namespace TaxonomyData
    <Serializable()> Public Class BOTables
        Implements ITables, IDisposable

#Region "Prelim code"
        Public Sub New()
            Try

            Catch Ex As Exception
                Throw New Exception("Cannot create new BOTables object", Ex)
            End Try
        End Sub

#End Region

#Region "Properties"
        Friend ReadOnly Property Transfer() As Transfer
            Get
                If mTheTables Is Nothing = True Then
                    mTheTables = New Transfer
                End If
                Return mTheTables
            End Get
        End Property
        Private mTheTables As Transfer

#End Region

#Region "Methods"

        Friend Function Item(ByVal TableName As String) As DataTable
            If Me.Transfer.Tables.Contains(TableName) Then
                Return Me.Transfer.Tables.Item(TableName)
            Else
                Return Nothing
            End If
        End Function

        Friend Shared Function Deserialise(ByVal SerialisedObject As String) As BOTables
            Try
                Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(GetType(BOTables))
                Dim sr As System.IO.StringReader = New System.IO.StringReader(SerialisedObject)
                Return CType(xs.Deserialize(sr), BOTables)
            Catch ex As Exception
                Throw New Exception("Cannot deserialise the BOTables object", ex)
            End Try
        End Function

        Friend Sub Add(ByVal TableName As String, ByVal NewXML As System.IO.MemoryStream)
            Try
                ValidateAgainstTransferSchema(TableName, NewXML)
            Catch ex As Exception
                Throw New Exception("Cannot add XML as table", ex)
            End Try
        End Sub

        Friend Function Serialise() As String
            Try
                Dim xs As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(Me.GetType)
                Dim sw As System.IO.StringWriter = New System.IO.StringWriter
                xs.Serialize(sw, Me)
                Return sw.ToString
            Catch ex As Exception
                Throw New Exception("Cannot serialise BOTables object", ex)
            End Try
        End Function
#End Region

#Region "Helper Functions"

        Private Sub ValidateAgainstTransferSchema(ByVal TableName As String, ByVal XMLtoValidate As System.IO.MemoryStream)
            Try
                'Validate the manifest against the manifest schema.
                Me.Transfer.ReadXml(XMLtoValidate)
            Catch ex As Exception
                Throw New Exception("Cannot validate " & TableName & " against the transfer schema", ex)
            End Try
        End Sub

        Private Sub ValidateAgainstTransferSchema(ByVal TableName As String, ByVal XMLtoValidate As String)
            Try
                'Validate the manifest against the manifest schema.
                Dim TheStringReader As New System.io.StringReader(XMLtoValidate)
                Dim TheXMLTextReader As New System.xml.XmlTextReader(TheStringReader)
                Me.Transfer.ReadXml(TheXMLTextReader)
            Catch ex As Exception
                Throw New Exception("Cannot validate " & TableName & " against the transfer schema", ex)
            End Try
        End Sub

#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not mTheTables Is Nothing Then
                mTheTables.Dispose()
            End If
        End Sub
    End Class
End Namespace