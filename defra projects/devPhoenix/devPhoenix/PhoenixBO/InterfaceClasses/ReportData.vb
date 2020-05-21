Public Class ReportDataResults
    Public Sub New(ByVal data As Data.DataSet, ByVal searchReference As String)
        mReportData = data
        mSearchReference = searchReference
    End Sub

    Public Property ReportData() As Data.DataSet
        Get
            Return mReportData
        End Get
        Set(ByVal Value As Data.DataSet)
            mReportData = Value
        End Set
    End Property
    Private mReportData As Data.DataSet

    Public Property SearchReference() As String
        Get
            Return mSearchReference
        End Get
        Set(ByVal Value As String)
            mSearchReference = Value
        End Set
    End Property
    Private mSearchReference As String

    Public Property DatabaseId() As Integer
        Get
            Return mDatabaseId
        End Get
        Set(ByVal Value As Integer)
            mDatabaseId = Value
        End Set
    End Property
    Private mDatabaseId As Int32 = -1
End Class
