Namespace Taxonomy
    <Serializable()> _
    Public Class AccessionExportQuota

        Public Sub New()

        End Sub

        Private CITESNotificationReferenceValue As String
        Public Property CITESNotificationReference() As String
            Get
                Return CITESNotificationReferenceValue
            End Get
            Set(ByVal Value As String)
                CITESNotificationReferenceValue = Value
            End Set
        End Property

        Private SourceValue As String
        Public Property Source() As String
            Get
                Return SourceValue
            End Get
            Set(ByVal Value As String)
                SourceValue = Value
            End Set
        End Property

        Private TermsValue As String
        Public Property Terms() As String
            Get
                Return TermsValue
            End Get
            Set(ByVal Value As String)
                TermsValue = Value
            End Set
        End Property

        Private YearValue As String
        Public Property Year() As String
            Get
                Return YearValue
            End Get
            Set(ByVal Value As String)
                YearValue = Value
            End Set
        End Property

        Private QuotaValue As String
        Public Property Quota() As String
            Get
                Return QuotaValue
            End Get
            Set(ByVal Value As String)
                QuotaValue = Value
            End Set
        End Property

        Private UnitValue As String
        Public Property Unit() As String
            Get
                Return UnitValue
            End Get
            Set(ByVal Value As String)
                UnitValue = Value
            End Set
        End Property

        Private CountryValue As String
        Public Property Country() As String
            Get
                Return CountryValue
            End Get
            Set(ByVal Value As String)
                CountryValue = Value
            End Set
        End Property

    End Class
End Namespace
