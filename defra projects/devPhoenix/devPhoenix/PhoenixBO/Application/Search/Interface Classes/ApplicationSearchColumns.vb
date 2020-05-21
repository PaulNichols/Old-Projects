Namespace Application.Search
    <Serializable()> _
    Public Enum LinkTypes
        None
        Permit
        Application
        Notification
    End Enum

    <Serializable()> _
    Public Class ApplicationSearchColumns
        Public Sub New()
            mHeaderAlignment = PhoenixHorizontalAlign.Center
        End Sub

        Friend Sub New(ByVal columnCaption As String, ByVal bindProperty As String)
            MyClass.New(columnCaption, bindProperty, True)
        End Sub

        Friend Sub New(ByVal columnCaption As String, ByVal bindProperty As String, ByVal canOrder As Boolean)
            MyBase.New()
            mColumnCaption = columnCaption
            mBindProperty = bindProperty
            mCanOrder = canOrder
        End Sub

        Public Property BindProperty() As String
            Get
                Return mBindProperty
            End Get
            Set(ByVal Value As String)
                mBindProperty = Value
            End Set
        End Property
        Private mBindProperty As String

        Public Property ColumnCaption() As String
            Get
                Return mColumnCaption
            End Get
            Set(ByVal Value As String)
                mColumnCaption = Value
            End Set
        End Property
        Private mColumnCaption As String

        Public Property LinkType() As LinkTypes
            Get
                Return mLinkType
            End Get
            Set(ByVal Value As LinkTypes)
                mLinkType = Value
            End Set
        End Property
        Private mLinkType As LinkTypes

        'PJN
        'The reason for this enum duplicating the framework enum is that the enum needs to be in numerical order to allow
        'the proxy generator code to work (not a great fix in my opinion!)
        Public Enum PhoenixHorizontalAlign
            NotSet = System.Web.UI.WebControls.HorizontalAlign.NotSet
            Left = System.Web.UI.WebControls.HorizontalAlign.Left
            Center = System.Web.UI.WebControls.HorizontalAlign.Center
            Right = System.Web.UI.WebControls.HorizontalAlign.Right
            Justify = System.Web.UI.WebControls.HorizontalAlign.Justify
        End Enum

        Public Property CellAlignment() As PhoenixHorizontalAlign
            Get
                Return mCellAlignment
            End Get
            Set(ByVal Value As PhoenixHorizontalAlign)
                mCellAlignment = Value
            End Set
        End Property
        Private mCellAlignment As PhoenixHorizontalAlign

        Public Property HeaderAlignment() As PhoenixHorizontalAlign
            Get
                Return mHeaderAlignment
            End Get
            Set(ByVal Value As PhoenixHorizontalAlign)
                mHeaderAlignment = Value
            End Set
        End Property
        Private mHeaderAlignment As PhoenixHorizontalAlign

        Public Property CanOrder() As Boolean
            Get
                Return mCanOrder
            End Get
            Set(ByVal Value As Boolean)
                mCanOrder = Value
            End Set
        End Property
        Private mCanOrder As Boolean
    End Class
End Namespace
