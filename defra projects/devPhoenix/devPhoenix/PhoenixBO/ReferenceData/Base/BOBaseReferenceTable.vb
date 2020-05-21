Namespace ReferenceData
    Public MustInherit Class BOBaseReferenceTable
        Inherits BaseDataBO
        Implements ReferenceData.IBOBaseReferenceTable

        Public Sub New(ByVal userRole As Int32, ByVal ssoRole As Int32)
            MyClass.New()
            m_UserRole = userRole
            m_SSORole = ssoRole
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Private mDescription As String

        <DOtoBOMapping("Description")> _
        Property Description() As String Implements IBOBaseReferenceTable.description
            Get
                Description = mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property

        Private mActive As Boolean
        <DOtoBOMapping("Active")> _
            Public Property Active() As Boolean 'Implements IBOCode.Active
            Get
                Return mActive
            End Get
            Set(ByVal Value As Boolean)
                mActive = Value
            End Set
        End Property

        <DOtoBOMapping("Editable")> _
        Public Overridable Property Editable() As Boolean
            Get
                Return True
            End Get
            Set(ByVal Value As Boolean)
                ' Does Nothing
            End Set
        End Property

        Private m_UserRole As Int64
        Public Property UserRole() As Int64
            Get
                Return m_UserRole
            End Get
            Set(ByVal Value As int64)
                m_UserRole = Value
            End Set
        End Property

        Private m_SSORole As Integer
        Public Property SSORole() As Integer
            Get
                Return m_SSORole
            End Get
            Set(ByVal Value As Integer)
                m_SSORole = Value
            End Set
        End Property
    End Class
End Namespace
