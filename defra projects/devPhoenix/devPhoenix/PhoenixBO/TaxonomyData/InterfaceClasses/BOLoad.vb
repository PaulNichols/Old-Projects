Imports System.xml
Imports System.xml.Schema
Imports System
Imports System.Collections

Namespace TaxonomyData

    Public Class BOLoad

#Region "Test"
        'TODO: Nick - Remove this code before production.
        Private Function EncodeBase64(ByVal ByteArray As Byte()) As String
            Dim CompressedMessage As String = System.Convert.ToBase64String(ByteArray)
            Return CompressedMessage
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



        'TODO: Nick - Remove this before production.
        'Public Overloads Function HandleTaxonomyDataLoadMessage(ByVal Base64Message As String, ByVal NumOfTimesMessageDataIsBase64Encoded As Int32) As String Implements ILoad.HandleTaxonomyDataLoadMessage
        '    '    'Try
        '    '    '    'Load the XML and Schema and validate them.
        '    '    '    Try
        '    '    '        ValidateXMLAgainstSchema(Load("c:\web\XSD\vTaxOrder.xml"), Load("c:\web\xsd\Transfer.xsd"))
        '    '    '    Catch ex As Exception
        '    '    '        Throw New ApplicationException("The supplied XML cannot be validated against the Schemer", ex)
        '    '    '    End Try
        '    '    '    'Get the compressed message contained in the GovTalk XML.
        '    Dim CompressedMessage As String = Base64Message
        '    '    '    Try
        '    Dim MessageByteArray() As Byte
        '    '    '        Dim GovTalkXMLDoc As XmlDocument = New XmlDocument
        '    '    '        GovTalkXMLDoc.LoadXml(XML)
        '    '    '        Dim MessageNode As XmlNodeList = GovTalkXMLDoc.GetElementsByTagName("MessageData")
        '    '    '        If NumOfTimesMessageDataIsBase64Encoded >= 0 Then
        '    '    '            CompressedMessage = MessageNode.Item(0).InnerText
        '    '    '            For Decode As Int32 = 1 To NumOfTimesMessageDataIsBase64Encoded - 1
        '    '    '                CompressedMessage = DecodeBase64ToString(CompressedMessage)
        '    '    '            Next
        '    MessageByteArray = ConvertBase64MessageToByteArray(CompressedMessage, NumOfTimesMessageDataIsBase64Encoded)
        '    '    '        ElseIf IsXML = True Then
        '    '    '            CompressedMessage = MessageNode.Item(0).InnerText
        '    '    '        Else
        '    '    '            MessageByteArray = DecodeBase64toByteArray(XML)
        '    '    '            CompressedMessage = EncodeBase64(MessageByteArray)

        '    '    '        End If
        '    '    '        MessageNode = Nothing
        '    '    '        GovTalkXMLDoc = Nothing
        '    '    '        GC.Collect()
        '    Save(MessageByteArray) 'TODO: Nick - Remove this code before production.
        '    '    '        Save(CompressedMessage) 'TODO: Nick - Remove this code before production.
        '    '    '    Catch ex As Exception
        '    '    '        Throw New ApplicationException("Could not retrieve compressed message data from GovTalk XML", ex)
        '    '    '    End Try
        '    '    'Decompress the message.
        '    '    Try
        '    '        'TODO: Nick - Raise a common code change request to get the decompress to return a sbyte array (and possibly UTF-8 encoding).
        '    '        Dim MessageStringArray() As String = DeCompressToStringArray(DecodeBase64toByteArray(XML))
        '    '    Catch ex As Exception
        '    '        Throw New ApplicationException("Could not decompress message data", ex)
        '    '    End Try

        '    '    'Catch ex As Exception
        '    '    '    Throw New ApplicationException("HandleTaxonomyDataLoadMessage failed", ex)
        '    '    'End Try
        'End Function


        Private Shared reader As XmlValidatingReader = Nothing
        Private Shared treader As XmlTextReader = Nothing
        Private Shared filename As [String] = String.Empty

        Private Overloads Shared Sub ValidateXMLAgainstSchema(ByVal XMLToValidate As String, ByVal ValidatingXSD As String)

            Try
                Dim ReaderXML As XmlValidatingReader = Nothing
                Dim XSD As Xml.Schema.XmlSchema = Nothing
                Dim SchemaCollection As New XmlSchemaCollection

                'Create the XmlParserContext.
                Dim context As New XmlParserContext(Nothing, Nothing, "", XmlSpace.None)
                'Implement the XML reader.
                ReaderXML = New XmlValidatingReader(XMLToValidate, XmlNodeType.Document, context)
                'Implement the XSD reader.
                Dim TheStringReader As New System.io.StringReader(ValidatingXSD)
                Dim TheXMLTextReader As New XmlTextReader(TheStringReader)
                XSD = New Xml.Schema.XmlSchema
                'Add the schema.
                SchemaCollection.Add(XSD.Read(TheXMLTextReader, AddressOf ValidationCallback))
                'Set the schema type and add the schema to the reader.
                ReaderXML.ValidationType = ValidationType.Schema
                ReaderXML.Schemas.Add(SchemaCollection)
                'Add the handler to raise the validation event.
                AddHandler reader.ValidationEventHandler, AddressOf ValidationCallback
                While reader.Read
                End While
            Catch Ex As Exception
                Throw New Exception("Cannot validate XML against schema", Ex)
            End Try
        End Sub

        Private Shared Sub ValidationCallback(ByVal sender As Object, ByVal args As ValidationEventArgs)
            Throw New Exception("Cannot validate XML against schema")
        End Sub

        Public Sub Save(ByVal theData() As Byte)
            Dim theStream As System.IO.FileStream
            Dim theWriter As System.IO.BinaryWriter

            Try
                ' Open a stream
                theStream = New System.IO.FileStream("c:\web\test.zip", IO.FileMode.OpenOrCreate, IO.FileAccess.Write)

                ' Create a writer on the stream
                theWriter = New System.IO.BinaryWriter(theStream)

                theWriter.Write(theData)
                theWriter.Flush()

            Catch ex As Exception

                theData = theData

            Finally

                ' Close our stream and reader

                If Not theStream Is Nothing Then
                    theStream.Close()
                End If

                If Not theWriter Is Nothing Then
                    theWriter.Close()
                End If
            End Try

        End Sub

        Public Sub save(ByVal theData As String)

            ' Create an instance of StreamWriter to write text to a file.
            Dim sw As System.io.StreamWriter = New System.IO.StreamWriter("c:\web\mybase64.txt")
            ' Add some text to the file.
            sw.Write(theData)
            sw.Close()

        End Sub

        Private Function Load(ByVal theFile As String) As String

            Dim theStream As System.IO.FileStream
            Dim theReader As System.IO.BinaryReader
            Dim theData() As Byte
            Dim theString As String

            Try
                ' Open a stream
                theStream = New System.IO.FileStream(theFile, IO.FileMode.Open, IO.FileAccess.Read)

                ' Create a reader on the stream
                theReader = New System.IO.BinaryReader(theStream)

                ' Read in the data
                theData = theReader.ReadBytes(CType(theReader.BaseStream.Length, Integer))

                Dim Encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                theString = Encoding.GetString(theData)

            Catch ex As Exception

                theData = theData

            Finally

                ' Close our stream and reader

                If Not theStream Is Nothing Then
                    theStream.Close()
                End If

                If Not theReader Is Nothing Then
                    theReader.Close()
                End If
            End Try
            Return theString

        End Function

        Public Enum TaxonomyDataLoadRequestStageEnum
            DataRequest = 1
            DataLoad = 2
        End Enum

        Public Enum TaxonomyDataLoadRequestStatusEnum
            Started = 1
            Completed = 2
            Failed = 3
        End Enum


        <Reportable()> _
            Friend Property Stage() As TaxonomyDataLoadRequestStageEnum
            Get
                Return mStage
            End Get
            Set(ByVal Value As TaxonomyDataLoadRequestStageEnum)
                mStage = Value
            End Set
        End Property
        Private mStage As TaxonomyDataLoadRequestStageEnum

        Friend Property Files() As BOFiles
            Get
                Return mFiles
            End Get
            Set(ByVal Value As BOFiles)
                mFiles = Value
            End Set
        End Property
        Private mFiles As BOFiles

        Friend Property Message() As String
            Get
                Return mMessage
            End Get
            Set(ByVal Value As String)
                mMessage = Value
            End Set
        End Property
        Private mMessage As String

        Friend Property Diagnostics() As String
            Get
                Return mDiagnostics
            End Get
            Set(ByVal Value As String)
                mDiagnostics = Value
            End Set
        End Property
        Private mDiagnostics As String

        Friend Property TaxonomyDataLoadDataID() As Int32
            Get
                Return mTaxonomyDataLoadDataID
            End Get
            Set(ByVal Value As Int32)
                mTaxonomyDataLoadDataID = Value
            End Set
        End Property
        Private mTaxonomyDataLoadDataID As Int32

        Friend Sub SetFiles(ByVal Files As BOFiles)
            Try
                Me.Files = Files
            Catch ex As Exception
                Throw New Exception("Cannot set files", ex)
            End Try
        End Sub


        'Friend Shared Function LoadLatest() As BORequest
        '    Try
        '        Dim NewDORequest As New DataObjects.Entity.TaxonomyDataLoadRequest
        '        Dim service As DataObjects.Service.TaxonomyDataLoadRequestService = NewDORequest.ServiceObject
        '        Dim DORequest As DataObjects.Entity.TaxonomyDataLoadRequest = service.GetLatestDataLoadRequest
        '        If DORequest Is Nothing = False Then
        '            Return New BORequest(DORequest)
        '        Else
        '            Return Nothing
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception("Cannot load latest Taxonomy Data Load Request", ex)
        '    End Try
        'End Function

        <AttributeUsage(AttributeTargets.Property)> Public Class Reportable
            Inherits Attribute
        End Class
#End Region


        Private Delegate Sub StartLoadTaxonomyDataDelegate(ByVal Base64Message As String, ByVal NumberOfTimesEncoded As Int32, ByVal UseCheckpoints As Boolean, ByVal UseTransaction As Boolean)

        Public Shared Sub StartLoadTaxonomyData(ByVal Base64Message As String, ByVal NumberOfTimesEncoded As Int32, ByVal UseCheckpoints As Boolean, ByVal UseTransaction As Boolean)
            Dim LoadDataDelegate As New StartLoadTaxonomyDataDelegate(AddressOf LoadTaxonomyData)
            LoadDataDelegate.BeginInvoke(Base64Message, NumberOfTimesEncoded, UseCheckpoints, UseTransaction, Nothing, Nothing)
        End Sub

        Public Shared Function GetReport() As BOMessageStatus
            Dim Results As DataSet
            Results = [DO].DataObjects.Sprocs.dbo_usp_SelectLatestTaxonomyDataLoadMessageStatus(Nothing, Results.GetType)
            If Results Is Nothing = False AndAlso Results.Tables(0).Rows.Count > 0 Then
                Return New BOMessageStatus(Results)
            Else
                Return Nothing
            End If
        End Function

        Public Overloads Shared Sub LoadTaxonomyData(ByVal Base64Message As String, ByVal NumberOfTimesEncoded As Int32, ByVal UseCheckpoints As Boolean, ByVal UseTransaction As Boolean)
            Try
                Dim Scripter As BOScripter
                'Create a new message object based on the received message.
                Dim CurrentMessage As New BOMessage(Base64Message, NumberOfTimesEncoded)
                CurrentMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Pending
                CurrentMessage.Save()
                'Asscoiate a request with this message.
                CurrentMessage.FulfillLatestRequest()
                Dim PreviousMessage As BOMessage = BOMessage.LoadPrevious(CurrentMessage.TaxonomyDataLoadMessageID)
                'Check if there is any other activity.
                If PreviousMessage Is Nothing = False _
                AndAlso PreviousMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Pending Then
                    'The previous message is pending which means that the current message has to remain as pending.
                    CurrentMessage.Status.Diagnostics = "A previous message with TaxonomyDataLoadMessageID = " & PreviousMessage.TaxonomyDataLoadMessageID & " is still pending so this message cannot be processed."
                    CurrentMessage.Status.Date = DateTime.Now
                    CurrentMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Pending
                    CurrentMessage.SaveStatus()
                Else
                    Try
                        PreviousMessage = BOMessage.LoadPreviouslyLoaded(CurrentMessage.TaxonomyDataLoadMessageID)
                        If PreviousMessage Is Nothing = False Then
                            PreviousMessage.DecodeMessage()
                        End If
                    Catch ex As Exception
                        CurrentMessage.Status.Diagnostics = "Cannot decode previously loaded message. " & New BOException(ex).GetInnerExceptionsMessage
                        CurrentMessage.Status.Date = DateTime.Now
                        CurrentMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Rejected
                        CurrentMessage.SaveStatus()
                        Throw New Exception(CurrentMessage.Status.Diagnostics)
                    End Try

                    'Validate the current message.
                    Try
                        CurrentMessage.DecodeMessage()
                    Catch ex As Exception
                        CurrentMessage.Status.Diagnostics = "Cannot decode message. " & New BOException(ex).GetInnerExceptionsMessage
                        CurrentMessage.Status.Date = DateTime.Now
                        CurrentMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Rejected
                        CurrentMessage.SaveStatus()
                        Throw New Exception(CurrentMessage.Status.Diagnostics)
                    End Try

                    'Check if there are any differences in the current and previous files.
                    If CurrentMessage.CompareTo(PreviousMessage) = 0 Then
                        'The files are the same so there is no need to script the differences.
                        CurrentMessage.Status.Diagnostics = "There are no differences between this message and the previously loaded message, therefore this message is being ignored."
                        CurrentMessage.Status.Date = DateTime.Now
                        CurrentMessage.Status.Status = BOMessageStatus.MessageStatusEnum.Loaded
                        CurrentMessage.SaveStatus()
                    Else
                        Scripter = CurrentMessage.GetScripter(PreviousMessage)
                        PreviousMessage = Nothing
                        GC.Collect()
                        GC.WaitForPendingFinalizers()
                        Scripter.ValidateLogical()
                    End If
                    If Scripter.Status.HasError = False Then
                        Scripter.ManagePhysical(UseCheckpoints, UseTransaction)
                    End If
                End If
                'Report back.

                Stop
            Catch ex As Exception
                Throw New Exception("Cannot handle Taxonomy Data Load message", ex)
            End Try
        End Sub

        Public Shared Sub HandleRequest(ByVal Request As BORequest, ByVal EarliestStartTime As String, ByVal LatestStartTime As String, ByVal Override As Boolean, ByVal Permissions As String, ByVal RequestUri As String)

            Dim EarliestStartTimeHHMM As TaxonomyData.TimeHHMM
            Dim LatestStartTimeHHMM As TaxonomyData.TimeHHMM
            Try
                If Common.HasPermissions(Permissions, Common.UserPermissions.CanLoadTaxonomyData) Then
                    Try
                        EarliestStartTimeHHMM = New TaxonomyData.TimeHHMM(EarliestStartTime)
                        LatestStartTimeHHMM = New TaxonomyData.TimeHHMM(LatestStartTime)
                        Try
                            'Check that the earlist start time is before the latest start time.
                            If (Override = True AndAlso Common.HasPermissions(Permissions, Common.UserPermissions.CanOverrideTaxonomyTimeWindow)) _
                            OrElse (TimeHHMM.Compare(EarliestStartTimeHHMM, LatestStartTimeHHMM) = -1) Then
                                'The earlist start time is before the latest start time
                                'so check that the current time is after or equal to the earlist start time and before or equal to the latest start time.
                                If (Override = True) OrElse _
                                (TimeHHMM.Compare(EarliestStartTimeHHMM, TimeHHMM.Now) <= 0 _
                                AndAlso TimeHHMM.Compare(LatestStartTimeHHMM, TimeHHMM.Now) >= 0) Then
                                    'The current time is after or equal to the earlist start time and before or equal to the latest start time
                                    'so perform the request.
                                    Request.Deliver(RequestUri)
                                Else
                                    Request.Status = BORequest.RequestResponseEnum.RequestOutsideWindow
                                End If
                            Else
                                If Override = True Then
                                    'The request does not have the permissions to override the time window.
                                    Request.Status = BORequest.RequestResponseEnum.OverridePermissionFailed
                                Else
                                    'The earlist start time is not before the latest start time so return an error message.
                                    Request.Status = BORequest.RequestResponseEnum.StartTimesInvalid
                                End If
                            End If
                        Catch ex As Exception
                            Request.Status = BORequest.RequestResponseEnum.GeneralError
                        End Try
                    Catch ex As Exception
                        Request.Status = BORequest.RequestResponseEnum.StartTimesInvalid
                    End Try
                Else
                    Request.Status = BORequest.RequestResponseEnum.RequestPermissionFailed
                End If
            Catch ex As Exception
                Request.Status = BORequest.RequestResponseEnum.GeneralError
            End Try
            Request.Save()
        End Sub

    End Class

    Public Class TimeHHMM
        Implements IComparer

        Protected Const TotalMinutesInHour As Int32 = 60
        Protected Const MaxMinutesInHour As Int32 = 59
        Protected Const MinMinutesInHour As Int32 = 0
        Protected Const TotalHoursInDay As Int32 = 24
        Protected Const MaxHoursInDay As Int32 = 23
        Protected Const MinHoursInDay As Int32 = 0

        Protected ReadOnly Property TimeSeparator() As Char
            Get
                Return ":".Chars(0)
            End Get
        End Property

        Public Shared Function Parse(ByVal Time As String) As TimeHHMM
            Return New TimeHHMM(Time)
        End Function

        Public Shared Function Now() As TimeHHMM
            Dim TheNow As Date = DateTime.Now
            Return New TimeHHMM(TheNow.Hour, TheNow.Minute)
        End Function

        Private mTimeSpan As TimeSpan

        Public Sub New()
            mTimeSpan = New TimeSpan
        End Sub

        Public Sub New(ByVal HH As Int32, ByVal MM As Int32)
            ValidateHH(HH)
            ValidateMM(MM)
            Initialise(HH, MM)
        End Sub

        Public Sub New(ByVal HH As String, ByVal MM As String)
            ValidateHH(HH)
            ValidateMM(MM)
            Initialise(HH, MM)
        End Sub

        Public Sub New(ByVal Time As String)
            Dim SplitTime() As String = Time.Split(TimeSeparator)
            ValidateSplitTime(SplitTime)
            Initialise(SplitTime(0), SplitTime(1))
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Hours.ToString & TimeSeparator & Me.Minutes.ToString
        End Function

        Public Function ToTimeSpan() As TimeSpan
            Return New TimeSpan(mTimeSpan.Ticks)
        End Function

        Public ReadOnly Property TotalMinutes() As Double
            Get
                Return mTimeSpan.TotalMinutes
            End Get
        End Property

        Public ReadOnly Property TotalHours() As Double
            Get
                Return mTimeSpan.TotalHours
            End Get
        End Property

        Public ReadOnly Property TotalSeconds() As Double
            Get
                Return mTimeSpan.TotalSeconds
            End Get
        End Property

        Private Sub ValidateSplitTime(ByVal SplitTime() As String)
            If SplitTime.Length <> 2 Then
                Throw New Exception
            End If
            ValidateHH(SplitTime(0))
            ValidateMM(SplitTime(1))
        End Sub

        Private Sub ValidateHH(ByVal HH As Int32)
            If HH < 0 _
            OrElse HH > 23 Then
                Throw New Exception("Invalid time")
            End If
        End Sub

        Private Sub ValidateHH(ByVal HH As String)
            ValidateHH(CType(HH, Int32))
        End Sub

        Private Sub ValidateMM(ByVal MM As Int32)
            If MM < MinMinutesInHour _
            OrElse MM > MaxMinutesInHour Then
                Throw New Exception("Invalid time")
            End If
        End Sub

        Private Sub ValidateMM(ByVal MM As String)
            ValidateMM(CType(MM, Int32))
        End Sub

        Public ReadOnly Property Hours() As Int32
            Get
                Return mTimeSpan.Hours
            End Get
        End Property

        Public ReadOnly Property Minutes() As Int32
            Get
                Return mTimeSpan.Minutes
            End Get
        End Property

        Private Sub Initialise(ByVal Hours As String, ByVal Minutes As String)
            Initialise(CType(Hours, Int32), CType(Minutes, Int32))
        End Sub

        Private Sub Initialise(ByVal Hours As Int32, ByVal Minutes As Int32)
            mTimeSpan = New TimeSpan(Hours:=Hours, Minutes:=Minutes, seconds:=MinMinutesInHour)
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            If Not TypeOf x Is TimeHHMM Then
                Throw New Exception("x is not TimeHHMM")
            End If
            If Not TypeOf y Is TimeHHMM Then
                Throw New Exception("y is not TimeHHMM")
            End If
            Return Compare(CType(x, TimeHHMM), CType(y, TimeHHMM))
        End Function

        Public Shared Function Compare(ByVal x As TimeHHMM, ByVal y As TimeHHMM) As Integer
            Return TimeSpan.Compare(CType(x, TimeHHMM).ToTimeSpan, CType(y, TimeHHMM).ToTimeSpan)
        End Function
    End Class
End Namespace