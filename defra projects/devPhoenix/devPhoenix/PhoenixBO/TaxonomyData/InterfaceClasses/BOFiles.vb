Namespace TaxonomyData
    Public Class BOFiles
        Inherits BaseBO
        Implements IFiles

        Const ManifestFileName As String = "manifest.xml"


#Region "Prelim code"
        Public Sub New(ByVal NewBase64Message As String, ByVal NewNumberOfTimesEncoded As Int32)
            Try
                Me.Tables = New BOTables
                DeCompress(ConvertBase64MessageToByteArray(NewBase64Message, NewNumberOfTimesEncoded))
                InitialiseNew()
            Catch Ex As Exception
                Throw New Exception("Cannot create new BOFiles object", Ex)
            End Try
        End Sub

        Private Sub InitialiseNew()
            ValidateNew()
            For Each Table As Manifest.RowRow In Me.Manifest.Entries
                Select Case Table.TableName.ToUpper
                    Case "VAQUATIC"
                        Me.mVAQUATICTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VAQUATICDataTable)
                    Case "VAREAOFUSE"
                        Me.mVAREAOFUSETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VAREAOFUSEDataTable)
                    Case "VBRU"
                        Me.mVBRUTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VBRUDataTable)
                    Case "VCOMMONNAME"
                        Me.mVCOMMONNAMETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VCOMMONNAMEDataTable)
                    Case "VCOUNTRY"
                        Me.mVCOUNTRYTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VCOUNTRYDataTable)
                    Case "VDECISIONS"
                        Me.mVDECISIONSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VDECISIONSDataTable)
                    Case "VDISTRIBAQUA"
                        Me.mVDISTRIBAQUATable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VDISTRIBAQUADataTable)
                    Case "VDISTRIBBRU"
                        Me.mVDISTRIBBRUTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VDISTRIBBRUDataTable)
                    Case "VDISTRIBCTY"
                        Me.mVDISTRIBCTYTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VDISTRIBCTYDataTable)
                    Case "VFAMILY"
                        Me.mVFAMILYTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VFAMILYDataTable)
                    Case "VGENUS"
                        Me.mVGENUSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VGENUSDataTable)
                    Case "VHIGHCOMMON"
                        Me.mVHIGHCOMMONTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VHIGHCOMMONDataTable)
                    Case "VHIGHDECISIONS"
                        Me.mVHIGHDECISIONSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VHIGHDECISIONSDataTable)
                    Case "VHIGHLEGAL"
                        Me.mVHIGHLEGALTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VHIGHLEGALDataTable)
                    Case "VHIGHQUOTAS"
                        Me.mVHIGHQUOTASTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VHIGHQUOTASDataTable)
                    Case "VHIGHSYNONYMS"
                        Me.mVHIGHSYNONYMSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VHIGHSYNONYMSDataTable)
                    Case "VKINGDOM"
                        Me.mVKINGDOMTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VKINGDOMDataTable)
                    Case "VLEGAL"
                        Me.mVLEGALTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VLEGALDataTable)
                    Case "VLEGALNAME"
                        Me.mVLEGALNAMETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VLEGALNAMEDataTable)
                    Case "VLEVELOFUSE"
                        Me.mVLEVELOFUSETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VLEVELOFUSEDataTable)
                    Case "VNOTIFICATION"
                        Me.mVNOTIFICATIONTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VNOTIFICATIONDataTable)
                    Case "VPART"
                        Me.mVPARTTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VPARTDataTable)
                    Case "VQUOTAS"
                        Me.mVQUOTASTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VQUOTASDataTable)
                    Case "VQUOTASOURCE"
                        Me.mVQUOTASOURCETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VQUOTASOURCEDataTable)
                    Case "VQUOTATERMS"
                        Me.mVQUOTATERMSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VQUOTATERMSDataTable)
                    Case "VSPECIES"
                        Me.mVSPECIESTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VSPECIESDataTable)
                    Case "VSYNLINK"
                        Me.mVSYNLINKTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VSYNLINKDataTable)
                    Case "VTAXCLASS"
                        Me.mVTAXCLASSTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VTAXCLASSDataTable)
                    Case "VTAXORDER"
                        Me.mVTAXORDERTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VTAXORDERDataTable)
                    Case "VTAXPHYLUM"
                        Me.mVTAXPHYLUMTable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VTAXPHYLUMDataTable)
                    Case "VUSED"
                        Me.mVUSETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VUSEDDataTable)
                    Case "VUSETYPE"
                        Me.mVUSETYPETable = CType(Me.Tables.Item(Table.TableName), TaxonomyData.Transfer.VUSETYPEDataTable)
                    Case Else
                        Throw New Exception("class initialise does not cater for " & Table.TableName & " table")
                End Select
            Next
        End Sub

#End Region

#Region "Methods"

        Friend Function CompareManifest(ByVal Files As BOFiles) As Int32
            If Files Is Nothing = False Then
                Return String.Compare(Me.Manifest.XML, Files.Manifest.XML, False)
            Else
                Return 1
            End If
        End Function

#End Region

#Region "Properties"

        Private mOldTables As TaxonomyData.BOTables

        Private Property Manifest() As BOManifest
            Get
                Return mManifest
            End Get
            Set(ByVal Value As BOManifest)
                mManifest = Value
            End Set
        End Property
        Private mManifest As BOManifest

        Private Property Tables() As TaxonomyData.BOTables
            Get
                If mTables Is Nothing = True Then
                    mTables = New TaxonomyData.BOTables
                End If
                Return mTables
            End Get
            Set(ByVal Value As TaxonomyData.BOTables)
                mTables = Value
            End Set
        End Property
        Private mTables As TaxonomyData.BOTables

        Friend ReadOnly Property Transfer() As Transfer
            Get
                Return Me.Tables.Transfer
            End Get
        End Property

        Dim mVAQUATICTable As TaxonomyData.Transfer.VAQUATICDataTable
        Dim mVAREAOFUSETable As TaxonomyData.Transfer.VAREAOFUSEDataTable
        Dim mVBRUTable As TaxonomyData.Transfer.VBRUDataTable
        Dim mVCOMMONNAMETable As TaxonomyData.Transfer.VCOMMONNAMEDataTable
        Dim mVCOUNTRYTable As TaxonomyData.Transfer.VCOUNTRYDataTable
        Dim mVDECISIONSTable As TaxonomyData.Transfer.VDECISIONSDataTable
        Dim mVDISTRIBAQUATable As TaxonomyData.Transfer.VDISTRIBAQUADataTable
        Dim mVDISTRIBBRUTable As TaxonomyData.Transfer.VDISTRIBBRUDataTable
        Dim mVDISTRIBCTYTable As TaxonomyData.Transfer.VDISTRIBCTYDataTable
        Dim mVFAMILYTable As TaxonomyData.Transfer.VFAMILYDataTable
        Dim mVGENUSTable As TaxonomyData.Transfer.VGENUSDataTable
        Dim mVHIGHCOMMONTable As TaxonomyData.Transfer.VHIGHCOMMONDataTable
        Dim mVHIGHDECISIONSTable As TaxonomyData.Transfer.VHIGHDECISIONSDataTable
        Dim mVHIGHLEGALTable As TaxonomyData.Transfer.VHIGHLEGALDataTable
        Dim mVHIGHQUOTASTable As TaxonomyData.Transfer.VHIGHQUOTASDataTable
        Dim mVHIGHSYNONYMSTable As TaxonomyData.Transfer.VHIGHSYNONYMSDataTable
        Dim mVKINGDOMTable As TaxonomyData.Transfer.VKINGDOMDataTable
        Dim mVLEGALTable As TaxonomyData.Transfer.VLEGALDataTable
        Dim mVLEGALNAMETable As TaxonomyData.Transfer.VLEGALNAMEDataTable
        Dim mVLEVELOFUSETable As TaxonomyData.Transfer.VLEVELOFUSEDataTable
        Dim mVNOTIFICATIONTable As TaxonomyData.Transfer.VNOTIFICATIONDataTable
        Dim mVPARTTable As TaxonomyData.Transfer.VPARTDataTable
        Dim mVQUOTASTable As TaxonomyData.Transfer.VQUOTASDataTable
        Dim mVQUOTASOURCETable As TaxonomyData.Transfer.VQUOTASOURCEDataTable
        Dim mVQUOTATERMSTable As TaxonomyData.Transfer.VQUOTATERMSDataTable
        Dim mVSPECIESTable As TaxonomyData.Transfer.VSPECIESDataTable
        Dim mVSYNLINKTable As TaxonomyData.Transfer.VSYNLINKDataTable
        Dim mVTAXCLASSTable As TaxonomyData.Transfer.VTAXCLASSDataTable
        Dim mVTAXORDERTable As TaxonomyData.Transfer.VTAXORDERDataTable
        Dim mVTAXPHYLUMTable As TaxonomyData.Transfer.VTAXPHYLUMDataTable
        Dim mVUSETable As TaxonomyData.Transfer.VUSEDDataTable
        Dim mVUSETYPETable As TaxonomyData.Transfer.VUSETYPEDataTable

        Public ReadOnly Property VAQUATICTable() As Transfer.VAQUATICDataTable Implements IFiles.VAQUATICTable
            Get
                Return mVAQUATICTable
            End Get
        End Property

        Public ReadOnly Property VAREAOFUSETable() As Transfer.VAREAOFUSEDataTable Implements IFiles.VAREAOFUSETable
            Get
                Return mVAREAOFUSETable
            End Get
        End Property

        Public ReadOnly Property VBRUTable() As Transfer.VBRUDataTable Implements IFiles.VBRUTable
            Get
                Return mVBRUTable
            End Get
        End Property

        Public ReadOnly Property VCOMMONNAMETable() As Transfer.VCOMMONNAMEDataTable Implements IFiles.VCOMMONNAMETable
            Get
                Return mVCOMMONNAMETable
            End Get
        End Property

        Public ReadOnly Property VCOUNTRYTable() As Transfer.VCOUNTRYDataTable Implements IFiles.VCOUNTRYTable
            Get
                Return mVCOUNTRYTable
            End Get
        End Property

        Public ReadOnly Property VDECISIONSTable() As Transfer.VDECISIONSDataTable Implements IFiles.VDECISIONSTable
            Get
                Return mVDECISIONSTable
            End Get
        End Property

        Public ReadOnly Property VDISTRIBAQUATable() As Transfer.VDISTRIBAQUADataTable Implements IFiles.VDISTRIBAQUATable
            Get
                Return mVDISTRIBAQUATable
            End Get
        End Property

        Public ReadOnly Property VDISTRIBBRUTable() As Transfer.VDISTRIBBRUDataTable Implements IFiles.VDISTRIBBRUTable
            Get
                Return mVDISTRIBBRUTable
            End Get
        End Property

        Public ReadOnly Property VDISTRIBCTYTable() As Transfer.VDISTRIBCTYDataTable Implements IFiles.VDISTRIBCTYTable
            Get
                Return mVDISTRIBCTYTable
            End Get
        End Property

        Public ReadOnly Property VFAMILYTable() As Transfer.VFAMILYDataTable Implements IFiles.VFAMILYTable
            Get
                Return mVFAMILYTable
            End Get
        End Property

        Public ReadOnly Property VGENUSTable() As Transfer.VGENUSDataTable Implements IFiles.VGENUSTable
            Get
                Return mVGENUSTable
            End Get
        End Property

        Public ReadOnly Property VHIGHCOMMONTable() As Transfer.VHIGHCOMMONDataTable Implements IFiles.VHIGHCOMMONTable
            Get
                Return mVHIGHCOMMONTable
            End Get
        End Property

        Public ReadOnly Property VHIGHDECISIONSTable() As Transfer.VHIGHDECISIONSDataTable Implements IFiles.VHIGHDECISIONSTable
            Get
                Return mVHIGHDECISIONSTable
            End Get
        End Property

        Public ReadOnly Property VHIGHLEGALTable() As Transfer.VHIGHLEGALDataTable Implements IFiles.VHIGHLEGALTable
            Get
                Return mVHIGHLEGALTable
            End Get
        End Property

        Public ReadOnly Property VHIGHQUOTASTable() As Transfer.VHIGHQUOTASDataTable Implements IFiles.VHIGHQUOTASTable
            Get
                Return mVHIGHQUOTASTable
            End Get
        End Property

        Public ReadOnly Property VHIGHSYNONYMSTable() As Transfer.VHIGHSYNONYMSDataTable Implements IFiles.VHIGHSYNONYMSTable
            Get
                Return mVHIGHSYNONYMSTable
            End Get
        End Property

        Public ReadOnly Property VKINGDOMTable() As Transfer.VKINGDOMDataTable Implements IFiles.VKINGDOMTable
            Get
                Return mVKINGDOMTable
            End Get
        End Property

        Public ReadOnly Property VLEGALNAMETable() As Transfer.VLEGALNAMEDataTable Implements IFiles.VLEGALNAMETable
            Get
                Return mVLEGALNAMETable
            End Get
        End Property

        Public ReadOnly Property VLEGALTable() As Transfer.VLEGALDataTable Implements IFiles.VLEGALTable
            Get
                Return mVLEGALTable
            End Get
        End Property

        Public ReadOnly Property VLEVELOFUSETable() As Transfer.VLEVELOFUSEDataTable Implements IFiles.VLEVELOFUSETable
            Get
                Return mVLEVELOFUSETable
            End Get
        End Property

        Public ReadOnly Property VNOTIFICATIONTable() As Transfer.VNOTIFICATIONDataTable Implements IFiles.VNOTIFICATIONTable
            Get
                Return mVNOTIFICATIONTable
            End Get
        End Property

        Public ReadOnly Property VPARTTable() As Transfer.VPARTDataTable Implements IFiles.VPARTTable
            Get
                Return mVPARTTable
            End Get
        End Property

        Public ReadOnly Property VQUOTASOURCETable() As Transfer.VQUOTASOURCEDataTable Implements IFiles.VQUOTASOURCETable
            Get
                Return mVQUOTASOURCETable
            End Get
        End Property

        Public ReadOnly Property VQUOTASTable() As Transfer.VQUOTASDataTable Implements IFiles.VQUOTASTable
            Get
                Return mVQUOTASTable
            End Get
        End Property

        Public ReadOnly Property VQUOTATERMSTable() As Transfer.VQUOTATERMSDataTable Implements IFiles.VQUOTATERMSTable
            Get
                Return mVQUOTATERMSTable
            End Get
        End Property

        Public ReadOnly Property VSPECIESTable() As Transfer.VSPECIESDataTable Implements IFiles.VSPECIESTable
            Get
                Return mVSPECIESTable
            End Get
        End Property

        Public ReadOnly Property VSYNLINKTable() As Transfer.VSYNLINKDataTable Implements IFiles.VSYNLINKTable
            Get
                Return mVSYNLINKTable
            End Get
        End Property

        Public ReadOnly Property VTAXCLASSTable() As Transfer.VTAXCLASSDataTable Implements IFiles.VTAXCLASSTable
            Get
                Return mVTAXCLASSTable
            End Get
        End Property

        Public ReadOnly Property VTAXORDERTable() As Transfer.VTAXORDERDataTable Implements IFiles.VTAXORDERTable
            Get
                Return mVTAXORDERTable
            End Get
        End Property

        Public ReadOnly Property VTAXPHYLUMTable() As Transfer.VTAXPHYLUMDataTable Implements IFiles.VTAXPHYLUMTable
            Get
                Return mVTAXPHYLUMTable
            End Get
        End Property

        Public ReadOnly Property VUSETable() As Transfer.VUSEDDataTable Implements IFiles.VUSEdTable
            Get
                Return mVUSETable
            End Get
        End Property

        Public ReadOnly Property VUSETYPETable() As Transfer.VUSETYPEDataTable Implements IFiles.VUSETYPETable
            Get
                Return mVUSETYPETable
            End Get
        End Property
#End Region

#Region "Helper Functions"

        Private Function DecodeBase64toByteArray(ByVal Base64 As String) As System.Byte()
            Dim CompressedMessage As String
            Dim CompressedMessageByteArray As System.Byte() = System.Convert.FromBase64String(Base64)
            Return CompressedMessageByteArray
        End Function

        Private Function DecodeBase64ToString(ByVal Base64 As String) As String
            Try
                Dim CompressedMessage As String
                Dim CompressedMessageByteArray As Byte() = System.Convert.FromBase64String(Base64)
                Dim Encoding As System.Text.Encoding = System.Text.Encoding.Default
                CompressedMessage = Encoding.GetString(CompressedMessageByteArray)
                Return CompressedMessage
            Catch ex As Exception
                Throw New Exception("Cannot decode Base 64 encoded string to type string")
            End Try
        End Function

        Private Function ConvertBase64MessageToByteArray(ByVal Base64Message As String, ByVal NumberOfTimesEncoded As Int32) As System.Byte()
            Try
                Dim MessageByteArray() As System.Byte
                Dim CompressedMessage As String = Base64Message
                If NumberOfTimesEncoded >= 0 Then
                    For Decode As Int32 = 1 To NumberOfTimesEncoded - 1
                        CompressedMessage = DecodeBase64ToString(CompressedMessage)
                    Next
                    MessageByteArray = DecodeBase64toByteArray(CompressedMessage)
                End If
                Return MessageByteArray
            Catch ex As Exception
                Throw New ApplicationException("Cannot convert Base 64 encoded string to system.byte array", ex)
            End Try
        End Function

        Private Sub DeCompress(ByVal CompressedBytes() As System.Byte)
            Try
                'Extract all the files.
                Dim sByteArray(CompressedBytes.Length) As SByte
                Buffer.BlockCopy(CompressedBytes, 0, sByteArray, 0, CompressedBytes.Length)
                Dim MyByteArrayInputStream As New java.io.ByteArrayInputStream(sByteArray)
                Dim MyZipInputStream As New java.util.zip.ZipInputStream(MyByteArrayInputStream)
                ExtractEntriesFromArchive(MyZipInputStream)
            Catch Ex As Exception
                Throw New Exception("Cannot decompress data", Ex)
            End Try
        End Sub

        Private Function ConvertByteArrayToString(ByVal ByteArray As System.Byte()) As String
            Try
                Dim CompressedMessage As String
                Dim Encoding As System.Text.Encoding = System.Text.Encoding.Default
                CompressedMessage = Encoding.GetString(ByteArray)
                Return CompressedMessage
            Catch Ex As Exception
                Throw New Exception("Cannot convert Byte Array to String", Ex)
            End Try
        End Function

        Private Sub ExtractEntriesFromArchive(ByVal MyZipInputStream As java.util.zip.ZipInputStream)
            Dim TableName As String
            Try
                Const NoMoreBytes As Int32 = -1
                Const BuffSize As Int32 = 2048
                Dim MyZipEntry As java.util.zip.ZipEntry = Nothing
                Do
                    Dim memStream As New System.IO.MemoryStream
                    MyZipEntry = MyZipInputStream.getNextEntry
                    If MyZipEntry Is Nothing = False Then
                        TableName = MyZipEntry.getName
                        Dim BytesRead As Int32 = 0
                        Dim sInflatedBytes(BuffSize - 1) As SByte
                        'Decompress the bytes.
                        BytesRead = MyZipInputStream.read(sInflatedBytes, 0, sInflatedBytes.Length)
                        Do Until BytesRead = NoMoreBytes
                            'Copy the signed bytes to unsigned bytes.
                            Dim InflatedBytes(sInflatedBytes.Length - 1) As System.Byte
                            Try
                                Buffer.BlockCopy(sInflatedBytes, 0, InflatedBytes, 0, BytesRead)
                            Catch ex2 As Exception
                                ex2 = ex2
                            End Try

                            'Add the bytes to the memory stream.
                            If BytesRead > 0 Then
                                memStream.Write(InflatedBytes, 0, BytesRead)
                            End If

                            'Decompress the next block of bytes.
                            Try
                                BytesRead = MyZipInputStream.read(sInflatedBytes, 0, sInflatedBytes.Length)
                            Catch ex4 As Exception
                                ex4 = ex4
                            End Try

                        Loop
                        memStream.Flush()
                        memStream.Position = 0
                        If String.Compare(ManifestFileName, TableName) = 0 Then
                            Me.Manifest = New BOManifest(memStream)
                        Else
                            Me.Tables.Add(TableName, memStream)
                        End If
                        memStream.Close()
                    End If
                Loop Until MyZipEntry Is Nothing = True
            Catch ex As Exception
                Throw New Exception("Cannot extract entries from archive for the table: " & TableName, ex)
            End Try
        End Sub

        Private Sub ExtractEntriesFromArchive_old(ByVal MyZipInputStream As java.util.zip.ZipInputStream)
            Dim TableName As String
            Try
                Const NoMoreBytes As Int32 = -1
                Const BuffSize As Int32 = 2048
                Dim MyZipEntry As java.util.zip.ZipEntry = Nothing
                '####Dim TheStream As System.IO.TextWriter  '####
                Dim TheMemoryStream As New System.IO.MemoryStream(New Byte() {})
                Dim ds As New Transfer
                '####ds.VDECISIONS.Rows.Add(New Object() {1, 2060, 242, "Spc", "(+)", "1997-07-22 00:00:00", "", "", ""})

                'ds.ReadXmlSchema("C:\Web\xsd\Transfer with optional.xsd")
                Do

                    MyZipEntry = MyZipInputStream.getNextEntry
                    If MyZipEntry Is Nothing = False Then
                        TableName = MyZipEntry.getName
                        Dim BytesRead As Int32 = 0
                        Dim sInflatedBytes(BuffSize - 1) As SByte
                        'Decompress the bytes.
                        BytesRead = MyZipInputStream.read(sInflatedBytes, 0, sInflatedBytes.Length)
                        '####Dim MessageStringBuilder As New Text.StringBuilder
                        '####Dim TheXML As String = String.Empty
                        '####Dim TheXML As New ArrayList
                        '####TheStream = New System.IO.TextWriter
                        '####TheOffset = 0
                        '####Dim TheXMLTextWriter As New System.Xml.XmlTextWriter(TheMemoryStream, System.Text.Encoding.Unicode)
                        Dim startIdx As Int32 = 0 '####
                        Dim TheArray(100000000) As Byte '####

                        Do Until BytesRead = NoMoreBytes
                            'Copy the signed bytes to unsigned bytes.
                            Dim InflatedBytes(sInflatedBytes.Length - 1) As System.Byte
                            Try
                                Buffer.BlockCopy(sInflatedBytes, 0, InflatedBytes, 0, BytesRead)
                            Catch ex2 As Exception
                                ex2 = ex2
                            End Try
                            'Store the data for further use.
                            Try
                                '####MessageStringBuilder.Append(ConvertByteArrayToString(InflatedBytes))
                                '####TheXML = ConvertByteArrayToString(InflatedBytes)
                                '####TheXML.Add(ConvertByteArrayToString(InflatedBytes))
                                '####TheStream.Write(ConvertByteArrayToString(InflatedBytes))
                                '####TheOffset += InflatedBytes.Length
                                '####TheXMLTextWriter.WriteRaw(ConvertByteArrayToString(InflatedBytes))
                                '####TheXMLTextWriter.x()
                                '####ReDim Preserve TheArray(startIdx + (BytesRead - 1))
                                Array.Copy(InflatedBytes, 0, TheArray, startIdx, BytesRead)
                                startIdx += InflatedBytes.Length
                            Catch ex3 As Exception
                                ex3 = ex3
                            End Try
                            'Decompress the bytes.
                            Try
                                BytesRead = MyZipInputStream.read(sInflatedBytes, 0, sInflatedBytes.Length)
                            Catch ex4 As Exception
                                ex4 = ex4
                            End Try
                        Loop
                        If String.Compare(ManifestFileName, TableName) = 0 Then
                            '####Manifest = New BOManifest(MessageStringBuilder.ToString)
                            Dim AnotherArray(startIdx) As Byte
                            Array.Copy(TheArray, AnotherArray, startIdx)
                            ReDim TheArray(-1)
                            GC.Collect()
                            GC.WaitForPendingFinalizers()
                            Dim memStream As New System.IO.MemoryStream(AnotherArray)
                            Manifest = New BOManifest(memStream)
                        Else
                            '####Tables = New TaxonomyData.BOTables
                            '####Tables.Add(TableName, MessageStringBuilder.ToString)

                            'Dim Encoding As System.Text.Encoding

                            'Dim Buf(-1) As Byte
                            'Dim aString As String
                            'Dim bufLength As Int32
                            'For Each ArrayElement As String In TheXML
                            '    Buf = Encoding.UTF8.GetBytes(ArrayElement)
                            '    bufLength = Buf.Length
                            '    startIdx = TheArray.Length
                            '    ReDim Preserve TheArray(startIdx + (bufLength - 1))
                            '    Array.Copy(Buf, 0, TheArray, startIdx, bufLength)
                            'Next
                            'Me.Tables = New TaxonomyData.BOTables
                            Dim AnotherArray(startIdx) As Byte
                            Array.Copy(TheArray, AnotherArray, startIdx)
                            ReDim TheArray(-1)
                            GC.Collect()
                            GC.WaitForPendingFinalizers()
                            Dim memStream As New System.IO.MemoryStream(AnotherArray)

                            Dim filStream As New System.IO.FileStream("C:\Rubbish\Output\" & TableName, IO.FileMode.Create, IO.FileAccess.Write)
                            filStream.Write(AnotherArray, 0, AnotherArray.Length - 1)
                            filStream.Flush()
                            filStream.Close()

                            'If TableName = "vDistribCty.xml" Then
                            '####Tables.Add(TableName, memStream)
                            ds.ReadXml(memstream)
                            Try

                            Catch ex As Exception

                            End Try
                            'Dim f As New System.IO.FileStream("C:\\somefile.xml", IO.FileMode.Open)
                            'Dim wtr As New System.xml.XmlTextWriter(memStream, System.Text.Encoding.UTF8)
                            '//write XML here
                            'memStream.Seek(0, System.IO.SeekOrigin.Begin)
                            ''//open a validating reader
                            'Dim rdr As New System.Xml.XmlValidatingReader(New System.Xml.XmlTextReader(memStream))
                            'rdr.Schemas.Add(Nothing, "C:\Web\xsd\Copy of Transfer.xsd")
                            'Dim blah As New System.Xml.Schema.XmlSchema
                            'rdr.ValidationType = Xml.ValidationType.Schema
                            'Try
                            '    While (rdr.Read())
                            '    End While
                            'Catch exxml As Exception
                            '    exxml = exxml
                            'End Try
                            'Perhaps the way forward is to split the xml into acceptable chunks that can be validated.

                        End If

                    End If
                Loop Until MyZipEntry Is Nothing = True
            Catch ex As Exception
                Throw New Exception("Cannot extract entries from archive for the table: " & TableName, ex)
            End Try
        End Sub

        Private Function TableActionOrder() As String()
            Dim TableActionOrderArray As String() = New String() {"VLEGALNAME.xml", "VQUOTATERMS.xml", "VQUOTASOURCE.xml", "VNOTIFICATION.xml", "VUSETYPE.xml", "VPART.xml", "VLEVELOFUSE.xml", "VAQUATIC.xml", "VAREAOFUSE.xml", "VCOUNTRY.xml", "VBRU.xml", "VKINGDOM.xml", "VTAXPHYLUM.xml", "VTAXCLASS.xml", "VTAXORDER.xml", "VFAMILY.xml", "VGENUS.xml", "VSPECIES.xml", "VCOMMONNAME.xml", "VHIGHCOMMON.xml", "VDISTRIBAQUA.xml", "VDISTRIBBRU.xml", "VDISTRIBCTY.xml", "VUSE.xml", "VSYNLINK.xml", "VHIGHSYNONYMS.xml", "VDECISIONS.xml", "VHIGHDECISIONS.xml", "VLEGAL.xml", "VHIGHLEGAL.xml", "VQUOTAS.xml", "VHIGHQUOTAS.xml"}
            Return TableActionOrderArray
        End Function
#End Region

#Region " Validate "

        Private Function NewValidates(ByVal ThrowExceptionOnFailure As Boolean) As Boolean
            If ThrowExceptionOnFailure = True Then
                ValidateNew()
            Else
                Try
                    ValidateNew()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        End Function

        Private Sub ValidateNew()
            Try
                If Me.Tables Is Nothing = True Then
                    Throw New Exception("BOFiles does not contain the correct tables")
                End If
                If Me.Manifest Is Nothing = True Then
                    Throw New Exception("BOFiles does not contain a manifest")
                End If
                'Check that the tables specified in the manifest do appear in the tables collection and that the number of rows specified are the number of rows supplied.

                For Each TableRow As Manifest.RowRow In Me.Manifest.Entries
                    With TableRow
                        Dim DBTable As DataTable
                        Try
                            DBTable = Me.Tables.Item(.TableName)
                        Catch TableNameEx As Exception
                            Throw New Exception("Table " & .TableName & " does not exist", TableNameEx)
                        End Try
                        If DBTable Is Nothing = False Then
                            If Int64.Parse(.RowCount.ToString) <> DBTable.Rows.Count Then
                                Throw New Exception("The manifest specifies table " & .TableName & " as having " & .RowCount.ToString & " rows. It actually has " & DBTable.Rows.Count.ToString & " rows")
                            End If
                        Else
                            Throw New Exception("Table " & .TableName & " is missing")
                        End If
                    End With
                Next
            Catch Ex As Exception
                Throw New Exception("Validation of new files failed", Ex)
            End Try
        End Sub

        Private Function Validates() As Boolean
            Return (Not Validate() Is Nothing) OrElse (Not ValidationErrors.HasErrors)
        End Function

        Protected Overridable Overloads Function Validate() As ValidationManager
            Return New ValidationManager
        End Function

#End Region

       
    End Class
End Namespace