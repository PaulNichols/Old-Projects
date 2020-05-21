Imports uk.gov.defra.Phoenix.BO
Imports uk.gov.defra.Phoenix.do
Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReportData
    <Serializable()> Public Class BoReportStaple
        Inherits BaseBO
        Implements IReportStaple

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub New(ByVal reportStapleId As Int32)
            MyBase.new()
            InitialiseReportStaple(reportStapleId)
        End Sub

        Private Sub InitialiseReportStaple(ByVal reportStapleId As Int32)

            mStapleId = reportStapleId

            Select Case reportStapleId
                Case 0
                    mStapleName = "None"
                Case 1
                    mStapleName = "Yes"
                Case 2
                    mStapleName = "No"
            End Select

        End Sub


        Public Property StapleId() As Integer Implements IReportStaple.StapleId
            Get
                Return mStapleId
            End Get
            Set(ByVal Value As Integer)
                mStapleId = Value
            End Set
        End Property
        Private mStapleId As Int32

        Public Property StapleName() As String Implements IReportStaple.StapleName
            Get
                Return mStapleName
            End Get
            Set(ByVal Value As String)
                mStapleName = Value
            End Set
        End Property
        Private mStapleName As String
    End Class
End Namespace