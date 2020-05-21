Namespace Taxonomy
    <Serializable()> _
    Public Class BOQuotaDisplay
        Implements IQuotaDisplay

        Public Sub New()

        End Sub

        Public Sub NewObject(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal CITESNotificationName As String, ByVal ExportQuotaSource As String, ByVal ExportQuotaTerm As String, ByVal ID As Int32, ByVal ISO2CountryCode As String, ByVal QuotaUnit As String, ByVal QuotaVolume As String, ByVal QuotaYear As String)
            mCITESNotificationName = CITESNotificationName
            mExportQuotaSource = ExportQuotaSource
            mExportQuotaTerm = ExportQuotaTerm
            mID = ID
            mISO2CountryCode = ISO2CountryCode
            mQuotaUnit = QuotaUnit
            mQuotaVolume = QuotaVolume
            mQuotaYear = QuotaYear
            Dim DOQuotaNote As DataObjects.Entity.TaxonomyExportQuota = DataObjects.Entity.TaxonomyExportQuota.GetById(Source, ID)
            If DOQuotaNote.Note Is Nothing Then
                mNote = DOQuotaNote.Note
            Else
                mNote = ""
            End If
            Me.Source = Source
        End Sub

        Public Sub New(ByVal DOExportQuota As DataObjects.Views.Entity.TaxonomyExportQuotaAll)
            With DOExportQuota
                NewObject(CType(System.Enum.Parse(GetType(TaxonomyData.TaxonomyRowSourceEnum), .Source.ToString), TaxonomyData.TaxonomyRowSourceEnum), .CITESNotificationName, .ExportQuotaSource, .ExportQuotaTerm, .Id, .ISO2CountryCode, .QuotaUnit, .QuotaVolume.ToString, .QuotaYear.ToString)
            End With
        End Sub

        Public Property Source() As TaxonomyData.TaxonomyRowSourceEnum
            Get
                Return mSource
            End Get
            Set(ByVal Value As TaxonomyData.TaxonomyRowSourceEnum)
                mSource = Value
            End Set
        End Property
        Private mSource As TaxonomyData.TaxonomyRowSourceEnum

        Public Property CITESNotificationName() As String Implements IQuotaDisplay.CITESNotificationName
            Get
                Return mCITESNotificationName
            End Get
            Set(ByVal Value As String)
                mCITESNotificationName = Value
            End Set
        End Property
        Private mCITESNotificationName As String

        Public Property ExportQuotaSource() As String Implements IQuotaDisplay.ExportQuotaSource
            Get
                Return mExportQuotaSource
            End Get
            Set(ByVal Value As String)
                mExportQuotaSource = Value
            End Set
        End Property
        Private mExportQuotaSource As String

        Public Property ExportQuotaTerm() As String Implements IQuotaDisplay.ExportQuotaTerm
            Get
                Return mExportQuotaTerm
            End Get
            Set(ByVal Value As String)
                mExportQuotaTerm = Value
            End Set
        End Property
        Private mExportQuotaTerm As String

        Public Property ID() As Int32 Implements IQuotaDisplay.ID
            Get
                Return mID
            End Get
            Set(ByVal Value As Int32)
                mID = Value
            End Set
        End Property
        Private mID As Int32

        Public Property ISO2CountryCode() As String Implements IQuotaDisplay.ISO2CountryCode
            Get
                Return mISO2CountryCode
            End Get
            Set(ByVal Value As String)
                mISO2CountryCode = Value
            End Set
        End Property
        Private mISO2CountryCode As String

        Public Property Note() As String Implements IQuotaDisplay.Note
            Get
                Return mNote
            End Get
            Set(ByVal Value As String)
                mNote = Value
            End Set
        End Property
        Private mNote As String

        Public Property QuotaUnit() As String Implements IQuotaDisplay.QuotaUnit
            Get
                Return mQuotaUnit
            End Get
            Set(ByVal Value As String)
                mQuotaUnit = Value
            End Set
        End Property
        Private mQuotaUnit As String

        Public Property QuotaVolume() As String Implements IQuotaDisplay.QuotaVolume
            Get
                Return mQuotaVolume
            End Get
            Set(ByVal Value As String)
                mQuotaVolume = Value
            End Set
        End Property
        Private mQuotaVolume As String

        Public Property QuotaYear() As String Implements IQuotaDisplay.QuotaYear
            Get
                Return mQuotaYear
            End Get
            Set(ByVal Value As String)
                mQuotaYear = Value
            End Set
        End Property
        Private mQuotaYear As String

        Public Property TruncatedNote() As String Implements IQuotaDisplay.TruncatedNote
            Get
                Dim stringtoreturn As String
                If Note Is Nothing = False Then
                    If Note.Length <= 20 Then
                        stringtoreturn = Note
                    Else
                        'Return the first 5 words.
                        Dim splitnote() As String = Note.Split((" ").ToCharArray)
                        stringtoreturn = String.Join(" ", splitnote, 0, 5) & " (more)"
                    End If
                Else
                    stringtoreturn = ""
                End If
                Return stringtoreturn
            End Get
            Set(ByVal Value As String)

            End Set
        End Property
    End Class
End Namespace