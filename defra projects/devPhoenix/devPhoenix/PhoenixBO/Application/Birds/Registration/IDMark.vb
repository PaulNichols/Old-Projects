Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class IDMark
        Private mRow As DataRow

        <Serializable()> _
        Public Enum MarkFateList
            Lost
            Other
        End Enum

        <Serializable()> _
        Public Enum IdMarkTypes
            Unknown = 0
            WLRS_Closed_Ring = 1
            Microchip = 2
            Other_Closed_Ring = 3
            WLRS_Split_Ring = 4
            WLRS_Swiss_Ring = 5
            WLRS_Cable_Tie = 6
            WLRS_Unringed_Licence = 7
            Tattoo = 8
            Other_Ring = 9
            Tag = 11
        End Enum

        Public Sub New()
        End Sub

        Friend Sub New(ByVal mark As String, ByVal markType As Int32, ByVal markFate As Object)
            mMark = mark
            mMarkType = markType
            mMarkFate = markFate
        End Sub

        Friend Sub New(ByVal mark As IDMark)
            If Not mark Is Nothing Then
                mMark = mark.Mark
                mMarkType = mark.MarkType
                mMarkFate = mark.MarkFate
                mFateReason = mark.FateReason
                mMarkStatus = mark.MarkStatus
            End If
        End Sub

        Private Const MARK_NUMBER As String = "IDMarkNumber"
        Private Const MARK_TYPE As String = "IDMarkType"
        Private Const MARK_FATE As String = "IDMarkFate"
        Private Const FATE_REASON As String = "FateReason"
        Private Const MARK_STATUS As String = "Status"

        Friend Sub New(ByVal mark As DataRow)
            If Not mark Is Nothing Then
                mMark = mark.Item(MARK_NUMBER).ToString
                mMarkType = CType(mark.Item(MARK_TYPE), Int32)
                mMarkStatus = CType(System.Enum.Parse(GetType(MarkStatusList), mark.Item(MARK_STATUS).ToString), MarkStatusList)
                'only read fate info if the id has been set to fated
                mMarkFate = Nothing
                mFateReason = Nothing
                If mMarkStatus = MarkStatusList.Fated Then
                    If Not mark.IsNull(MARK_FATE) Then mMarkFate = mark.Item(MARK_FATE)
                    If Not mark.IsNull(FATE_REASON) Then mFateReason = mark.Item(FATE_REASON).ToString
                End If
            End If
        End Sub

        Friend Sub New(ByVal mark As DataObjects.Entity.SpecimenIDMark)
            If Not mark Is Nothing Then
                mMark = mark.IdMark
                mMarkType = mark.IDMarkTypeId
                mMarkStatus = MarkStatusList.Fitted
                If Not mark.IsIdMarkFateIdNull Then
                    mMarkFate = mark.IdMarkFateId
                    mFateReason = String.Empty
                End If
            End If
        End Sub

        Friend Sub PopulateIDMark(ByRef ringRow As DataRow)
            With ringRow
                .Item(MARK_NUMBER) = mMark
                .Item(MARK_TYPE) = mMarkType
                .Item(MARK_STATUS) = System.Enum.GetName(GetType(MarkStatusList), mMarkStatus)
                'only write fate info if the id has been set to fated
                .Item(MARK_FATE) = System.Convert.DBNull
                .Item(FATE_REASON) = System.Convert.DBNull
                If mMarkStatus = MarkStatusList.Fated Then
                    If Not mMarkFate Is Nothing AndAlso TypeOf mMarkFate Is Int32 AndAlso CType(mMarkFate, Int32) > 0 Then .Item(MARK_FATE) = CType(mMarkFate, Int32) Else .Item(MARK_FATE) = System.Convert.DBNull
                    If Not mFateReason Is Nothing AndAlso mFateReason.Length > 0 Then .Item(FATE_REASON) = mFateReason Else .Item(FATE_REASON) = System.Convert.DBNull
                End If
            End With
        End Sub

        'Friend Shared Function AddIDMark(ByRef array As IDMark(), ByVal row As DataRow) As IDMark
        '    Dim Success As IDMark = Nothing
        '    Dim Upper As Int32 = 0
        '    If Not array Is Nothing Then
        '        Upper = array.Length
        '    End If
        '    ReDim Preserve array(Upper)
        '    array(Upper) = New IDMark(row)
        '    Success = array(Upper)
        '    Return Success
        'End Function

        Public Property Mark() As String
            Get
                Return mMark
            End Get
            Set(ByVal Value As String)
                mMark = Value
            End Set
        End Property
        Private mMark As String

        Public Property MarkType() As Int32
            Get
                Return mMarkType
            End Get
            Set(ByVal Value As Int32)
                mMarkType = Value
            End Set
        End Property
        Private mMarkType As Int32

        Public Property FateReason() As String
            Get
                Return mFateReason
            End Get
            Set(ByVal Value As String)
                mFateReason = Value
            End Set
        End Property
        Private mFateReason As String

        Friend ReadOnly Property HasMarkType() As Boolean
            Get
                Return (mMarkType > 0)
            End Get
        End Property

        Public Function GetMarkType() As IdMarkTypes
            Dim Result As IdMarkTypes = IDMark.GetMarkType(mMark)
            mMarkType = Result
            Return Result
        End Function

        Friend Shared Function GetMarkType(ByVal mark As String) As IdMarkTypes
            Dim ReturnVal As IdMarkTypes = IdMarkTypes.Unknown

            ' get all of the mark types in the db
            Dim MarkTypes As DataObjects.EntitySet.IDMarkTypeSet = DataObjects.Entity.IDMarkType.GetAll
            If Not MarkTypes Is Nothing AndAlso MarkTypes.Count > 0 Then
                'if we have types
                For Each MarkType As DataObjects.Entity.IDMarkType In MarkTypes
                    '...ensure that the regular expression link is set
                    If Not MarkType.IsValidationRegExIDNull AndAlso MarkType.ValidationRegExID > 0 Then
                        ' load the appropriate regular expression
                        Dim RegExp As DataObjects.Entity.ValidationRegEx = DataObjects.Entity.ValidationRegEx.GetById(MarkType.ValidationRegExID)
                        '..did we get it?
                        If Not RegExp Is Nothing Then
                            ' parse the expression
                            If System.Text.RegularExpressions.Regex.Match(mark, RegExp.RegEx, Text.RegularExpressions.RegexOptions.None).Success Then
                                'success, set var and bail
                                ReturnVal = CType(MarkType.Id, IdMarkTypes)
                                Exit For
                            End If
                        End If
                    End If
                Next MarkType
            End If

            'send 'em back
            Return ReturnVal
        End Function

        Public Function GetReportDescription() As String
            Return String.Concat(MarkType_Helper.Trim, " ", Mark)
        End Function

        Public Property MarkType_Helper() As String
            Get
                If mMarkType > 0 Then
                    Dim MarkEntity As DataObjects.Entity.IDMarkType = DataObjects.Entity.IDMarkType.GetById(mMarkType)
                    If Not MarkEntity Is Nothing Then
                        Return MarkEntity.Description
                    End If
                End If
                Return String.Empty
            End Get
            Set(ByVal Value As String)
                'just for serialization
            End Set
        End Property

        Public Property MarkTypePermanence() As String      'MLD 19/1/5 added
            Get
                If mMarkType > 0 Then
                    Dim MarkEntity As DataObjects.Entity.IDMarkType = DataObjects.Entity.IDMarkType.GetById(mMarkType)
                    If Not MarkEntity Is Nothing Then
                        Return MarkEntity.Permanence
                    End If
                End If
                Return String.Empty
            End Get
            Set(ByVal Value As String)
                'just for serialization
            End Set
        End Property

        Public Property MarkFate() As Object
            Get
                Return mMarkFate
            End Get
            Set(ByVal Value As Object)
                mMarkFate = Value
            End Set
        End Property
        Private mMarkFate As Object

        <Serializable()> _
        Public Enum MarkStatusList
            NotSet
            Fitted
            Returned
            Fated
        End Enum

        Public Property MarkStatus() As MarkStatusList
            Get
                Return mMarkStatus
            End Get
            Set(ByVal Value As MarkStatusList)
                mMarkStatus = Value
            End Set
        End Property
        Private mMarkStatus As MarkStatusList

        Friend ReadOnly Property ShouldFateReasonBePopulated() As Boolean
            Get
                Return Not mMarkFate Is Nothing AndAlso _
                        TypeOf mMarkFate Is Int32 AndAlso _
                        CType(mMarkFate, Int32) = 1
            End Get
        End Property

        Friend ReadOnly Property IsFateReasonPopulated() As Boolean
            Get
                Return (mFateReason Is Nothing OrElse _
                        Not TypeOf mFateReason Is Int32 OrElse _
                        CType(mFateReason, Int32) = 0)
            End Get
        End Property

        Public Shared Function IsIdMarkARing(ByVal markTypeId As Int32) As Boolean
            Dim Mark As IdMarkTypes
            Try
                Mark = CType(markTypeId, IdMarkTypes)
            Catch ex As Exception
                Throw New InvalidCastException(markTypeId & " doesn't exist in the enum")
            End Try
            Select Case Mark
                Case IdMarkTypes.Other_Closed_Ring, _
                     IdMarkTypes.Other_Closed_Ring, _
                     IdMarkTypes.Other_Ring, _
                     IdMarkTypes.WLRS_Closed_Ring, _
                     IdMarkTypes.WLRS_Split_Ring, _
                     IdMarkTypes.WLRS_Swiss_Ring
                    Return True
                Case Else
                    Return False
            End Select
        End Function
    End Class
End Namespace
