Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity
Imports uk.gov.defra.Phoenix.BO.Application

Namespace ReportLimis
    Public Class BOLimisMonth

        Protected mMonth As Date
        Protected mAppTypes(3) As AppType
        Protected mTotal As New Line("TOTALS")

        Public Enum Constant
            Article10Type = 0
            CitesType = 1
            ChicksType = 2
            AdultType = 3
            SingleApp = 0
            SemiCompleteApp = 1
            MultipleApp = 2
            RefusedApp = 3
            Total = 4
        End Enum

        Sub New(ByVal month As Date)
            Dim fromDate As New Date(month.Year, month.Month, 1)
            Dim toDate As New Date(month.Year, month.Month, Date.DaysInMonth(month.Year, month.Month))
            Dim hashes()() As Hashtable = BuildHashtables()
            mMonth = month
            mAppTypes(0) = New CitesAppType("ARTICLE 10s")
            mAppTypes(1) = New CitesAppType("CITES PERMITS")
            mAppTypes(2) = New BirdAppType("BIRD REG - Chicks")
            mAppTypes(3) = New BirdAppType("BIRD REG - Adult")
            PopulateCites(hashes, fromDate, toDate)
            PopulateBirds(hashes, fromDate, toDate)
            CheckHashtables(hashes)
            BuildTotals()
        End Sub

        Private Sub PopulateCites(ByRef hashes()() As Hashtable, ByVal fromDate As Date, ByVal toDate As Date)
            Dim collection As SearchCompletedCitesDocumentsBoundCollection = SearchCompletedCitesDocumentsService.GetDocsByDateRange(fromDate, toDate)
            
            For Each item As SearchCompletedCitesDocuments In collection
                Dim index1 As Int32 = CitesAppType.GetMainIndex(item)
                Dim index2 As Int32 = CitesAppType.GetSubIndex(item, fromDate, toDate)
                Dim mainline As Line = mAppTypes(index1).Lines(index2)
                Dim hash As Hashtable = hashes(index1)(index2)
                
                If CitesAppType.IsInTarget(item) Then
                    mainline.DocsInTarget += 1
                End If
                mainline.DocTotal += 1
                hash.Item(item.ApplicationId) = Nothing
            Next
        End Sub

        Private Sub PopulateBirds(ByRef hashes()() As Hashtable, ByVal fromDate As Date, ByVal toDate As Date)
            '************* TO DO, when bird stuff is further advanced
        End Sub

        Private Sub BuildTotals()
            For i As Int32 = 0 To 3
                Dim len As Int32 = mAppTypes(i).Lines.Length - 1
                For j As Int32 = 0 To len - 1
                    mAppTypes(i).Lines(len).AppTotal += mAppTypes(i).Lines(j).AppTotal
                    mAppTypes(i).Lines(len).DocTotal += mAppTypes(i).Lines(j).DocTotal
                    mAppTypes(i).Lines(len).DocsInTarget += mAppTypes(i).Lines(j).DocsInTarget
                Next j
                mTotal.AppTotal += mAppTypes(i).Lines(len).AppTotal
                mTotal.DocTotal += mAppTypes(i).Lines(len).DocTotal
                mTotal.DocsInTarget += mAppTypes(i).Lines(len).DocsInTarget
            Next i
        End Sub

        Private Function BuildHashtables() As Hashtable()()
            Dim hashes(3)() As Hashtable
            For i As Int32 = 0 To 3
                Dim subhash(3) As Hashtable
                hashes(i) = subhash
                For j As Int32 = 0 To 3
                    subhash(j) = New Hashtable
                Next j
            Next i
            Return hashes
        End Function

        Private Sub CheckHashtables(ByVal hashes()() As Hashtable)
            For i As Int32 = 0 To 3
                Dim len As Int32 = mAppTypes(i).Lines.Length
                For j As Int32 = 0 To len - 2
                    mAppTypes(i).Lines(j).AppTotal = hashes(i)(j).Count
                Next j
            Next i
        End Sub

        Public ReadOnly Property Month As Date
            Get
                Return mMonth
            End Get
        End Property

        Public ReadOnly Property AppTypes As AppType()
            Get
                Return mAppTypes
            End Get
        End Property

        Public ReadOnly Property Total As Line
            Get
                Return mTotal
            End Get
        End Property

        Public Function GetSubTotal(ByVal which As Constant) As Line
            Dim app As AppType = mAppTypes(which)
            Return app.Lines(app.Lines.Length - 1)
        End Function

        Public Class AppType
            Private mDescription As String
            Protected mLines() As Line

            Sub New(ByVal description As String)
                mDescription = description
            End Sub

            Public ReadOnly Property Description As String
                Get
                    Return mDescription
                End Get
            End Property

            Public ReadOnly Property Lines As Line()
                Get
                    Return mLines
                End Get
            End Property
        End Class

        Public Class CitesAppType
            Inherits AppType

            Sub New(ByVal description As String)
                MyBase.New(description)
                Redim mLines(4)
                mLines(0) = New Line("single")
                mLines(1) = New Line("semi-complete")
                mLines(2) = New Line("multiple")
                mLines(3) = New Line("refused")
                mLines(4) = New Line("sub-total")
            End Sub

            Public Shared Function IsInTarget(ByVal item As SearchCompletedCitesDocuments) As Boolean
                Dim minsInDay As Int32 = 24 * 60
                Return item.WLRSService = 0 OrElse item.WLRSService <= item.GWDClock * minsInDay
            End Function

            Public Shared Function GetMainIndex(ByVal item As SearchCompletedCitesDocuments) As Int32
                If item.ApplicationTypeId = ApplicationTypes.Article10 Then
                    Return Constant.Article10Type
                End If
                Return Constant.CitesType
            End Function

            Public Shared Function GetSubIndex(ByVal item As SearchCompletedCitesDocuments, ByVal fromDate As Date, ByVal toDate As Date) As Int32
                Dim authorised As Date = item.DateAuthorised
                Dim refused As Date = item.DateRefused

                If authorised < fromDate OrElse authorised > toDate Then
                    authorised = Nothing
                End If
                If refused < fromDate OrElse refused > toDate Then
                    refused = Nothing
                End If
                If refused > authorised Then
                    Return Constant.RefusedApp
                ElseIf item.SemiComplete Then
                    Return Constant.SemiCompleteApp
                ElseIf item.NumberOfCopies > 1 Then
                    Return Constant.MultipleApp
                End If
                Return Constant.SingleApp
            End Function
        End Class

        Public Class BirdAppType
            Inherits AppType

            Sub New(ByVal description As String)
                MyBase.New(description)
                Redim mLines(3)
                mLines(0) = New Line("England")
                mLines(1) = New Line("Scotland")
                mLines(2) = New Line("Wales")
                mLines(3) = New Line("sub-total")
            End Sub
        End Class

        Public Class Line
            Private mDescription As String
            Private mAppTotal As Int32
            Private mDocTotal As Int32
            Private mDocsInTarget As Int32

            Sub New(ByVal description As String)
                mDescription = description
            End Sub

            Public Sub Add(ByVal other As Line)
                mAppTotal += other.mAppTotal
                mDocTotal += other.mDocTotal
                mDocsInTarget += other.mDocsInTarget
            End Sub

            Public ReadOnly Property Description As String
                Get
                    Return mDescription
                End Get
            End Property

            Public Property AppTotal As Int32
                Get
                    Return mAppTotal
                End Get
                Set
                    mAppTotal = Value
                End Set
            End Property

            Public Property DocTotal As Int32
                Get
                    Return mDocTotal
                End Get
                Set
                    mDocTotal = Value
                End Set
            End Property

            Public Property DocsInTarget As Int32
                Get
                    Return mDocsInTarget
                End Get
                Set
                    mDocsInTarget = Value
                End Set
            End Property

            Public ReadOnly Property DocsOutOfTarget As Int32
                Get
                    Return mDocTotal - mDocsInTarget
                End Get
            End Property

            Public ReadOnly Property PercentInTarget As Decimal
                Get
                    If mDocTotal = 0 Then
                        Return 0
                    End If
                    Return Decimal.Round((mDocsInTarget * 100D) / mDocTotal, 2)
                End Get
            End Property
        End Class
    End Class
End Namespace
