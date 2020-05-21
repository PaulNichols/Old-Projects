Namespace Application.Bird.Registration
    <Serializable()> _
    Public MustInherit Class AdultSpecimenType
        Public Sub New()
            ' has only one specimen so add it...
            'mSpecimenType = New SpecimenType
        End Sub

        'Public Property SpecimenId() As Int32
        '    Get
        '        Return mSpecimenId
        '    End Get
        '    Set(ByVal Value As Int32)
        '        mSpecimenId = Value
        '    End Set
        'End Property
        'Private mSpecimenId As Int32

        Public Property SpecimenType() As SpecimenType
            Get
                Return mSpecimenType
            End Get
            Set(ByVal Value As SpecimenType)
                mSpecimenType = Value
            End Set
        End Property
        Private mSpecimenType As SpecimenType

        Public Property Rings() As IDMark()
            Get
                Return mRings
            End Get
            Set(ByVal Value As IDMark())
                mRings = Value
            End Set
        End Property
        Private mRings(-1) As IDMark      'MLD 19/1/5 modified so is now never Nothing

        Public Property IDMarks() As IDMark()
            Get
                Return mIDMarks
            End Get
            Set(ByVal Value As IDMark())
                mIDMarks = Value
            End Set
        End Property
        Private mIDMarks(-1) As IDMark      'MLD 19/1/5 modified so is now never Nothing

        Public Function GetIDMarksByPermanence() As IDMark()
            Return GetSortedMarks(Nothing, mRings)
        End Function

        Public Function GetRingsByPermanence() As IDMark()
            Return GetSortedMarks(mIDMarks, Nothing)
        End Function

        Private Class IDMarkComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xMark As IDMark = CType(x, IDMark)
                Dim yMark As IDMark = CType(y, IDMark)
                Return String.Compare(xMark.MarkTypePermanence, yMark.MarkTypePermanence)
            End Function
        End Class

        Public ReadOnly Property AllMarks() As IDMark()
            Get
                Return GetSortedMarks(mIDMarks, mRings)
            End Get
        End Property

        Friend Shared Function GetSortedMarks(ByVal marks As IDMark(), ByVal rings As IDMark()) As IDMark()
            'we need to order everything by permanence
            'create a new array to return the info
            Dim Length As Int32 = 0
            If Not marks Is Nothing Then Length += marks.Length
            If Not rings Is Nothing Then Length += rings.Length
            'deduct the one because VB.NET uses stupid array dimensions!
            Length -= 1
            Dim Result(Length) As IDMark
            Dim Index As Int32 = 0
            If Length > 0 Then
                If Not marks Is Nothing Then AddMarks(Result, marks, Index)
                If Not rings Is Nothing Then AddMarks(Result, rings, Index)
                Array.Sort(Result, New IDMarkComparer)
            End If
            Return Result
        End Function

        Private Shared Sub AddMarks(ByRef list As IDMark(), ByVal marks As IDMark(), ByRef startPoint As Int32)
            For Each Mark As IDMark In marks
                list(startPoint) = Mark
                startPoint += 1
            Next Mark
        End Sub

        Friend ReadOnly Property HasRings() As Boolean
            Get
                Return (mRings.Length > 0)
            End Get
        End Property

        Public Function AddIdMark() As IDMark
            Return AddIdMark(CType(Nothing, IDMark))
        End Function

        Public Function AddIdMark(ByVal newMark As IDMark) As IDMark      'MLD 19/1/5 simplified, as Nothing no longer possible
            Dim Upper As Int32 = mIDMarks.Length
            ReDim Preserve mIDMarks(Upper)
            mIDMarks(Upper) = New IDMark(newMark)
            Return mIDMarks(Upper)
        End Function

        Public Function AddIdMark(ByVal newMark As DataObjects.Entity.SpecimenIDMark) As IDMark
            Dim Upper As Int32 = mIDMarks.Length
            ReDim Preserve mIDMarks(Upper)
            mIDMarks(Upper) = New IDMark(newMark)
            Return mIDMarks(Upper)
        End Function

        Public Function AddIdMark(ByVal newMark As DataRow) As IDMark
            Dim Upper As Int32 = mIDMarks.Length
            ReDim Preserve mIDMarks(Upper)
            mIDMarks(Upper) = New IDMark(newMark)
            Return mIDMarks(Upper)
        End Function

        Public Function AddRing() As IDMark
            Return AddRing(CType(Nothing, IDMark))
        End Function

        Public Function AddRing(ByVal newRing As IDMark) As IDMark     'MLD 19/1/5 simplified, as Nothing no longer possible
            Dim Upper As Int32 = mRings.Length
            ReDim Preserve mRings(Upper)
            mRings(Upper) = New IDMark(newRing)
            Return mRings(Upper)
        End Function

        Public Function AddRing(ByVal newRing As DataRow) As IDMark     'MLD 19/1/5 simplified, as Nothing no longer possible
            Dim Upper As Int32 = mRings.Length
            ReDim Preserve mRings(Upper)
            mRings(Upper) = New IDMark(newRing)
            Return mRings(Upper)
        End Function

        Public Function AddRing(ByVal newRing As DataObjects.Entity.SpecimenIDMark) As IDMark     'MLD 19/1/5 simplified, as Nothing no longer possible
            Dim Upper As Int32 = mRings.Length
            ReDim Preserve mRings(Upper)
            mRings(Upper) = New IDMark(newRing)
            Return mRings(Upper)
        End Function
    End Class
End Namespace
