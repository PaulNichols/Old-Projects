
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.MessageService)), _
     EntityMapping(GetType(DataObjects.Entity.Message)), _
     CollectionMapping(GetType(DataObjects.Collection.MessageBoundCollection)), _
     Serializable()> _
    Public Class BOMessage
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " Initialisation Code "
        Public Sub New()

        End Sub
        Public Sub New(ByVal ident As Int32, ByVal tran As SqlClient.SqlTransaction)
            Initialise(DataObjects.Entity.Message.GetById(ident, tran))
        End Sub

        Private Sub New(ByVal value As DataObjects.Entity.Message)
            Initialise(value)
        End Sub

        Private Sub Initialise(ByVal value As DataObjects.Entity.Message)
            ConvertDataObjectTOBO(Me, value)
        End Sub

#End Region

#Region " DOtoBOMapping "
        <DOtoBOMapping("URL")> _
        Public Property URL() As String
            Get
                Return mURL
            End Get
            Set(ByVal Value As String)
                mURL = Value
            End Set
        End Property
        Private mURL As String

        <DOtoBOMapping("IsWarning")> _
        Public Property IsWarning() As Boolean
            Get
                Return mIsWarning
            End Get
            Set(ByVal Value As Boolean)
                mIsWarning = Value
            End Set
        End Property

        <DOtoBOMapping("IsWarningText")> _
        Public Property IsWarningText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(mIsWarning)
            End Get

            Set(ByVal Value As String)
                mIsWarning = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        Private mIsWarning As Boolean
        <DOtoBOMapping("TitleId")> _
            Public Property TitleId() As Int32
            Get
                Return mTitleId
            End Get
            Set(ByVal Value As Int32)
                mTitleId = Value
            End Set
        End Property
        Private mTitleId As Int32
        <DOtoBOMapping("IndividualMessageId")> _
            Public Property IndividualMessageId() As Int32
            Get
                Return mIndividualMessageId
            End Get
            Set(ByVal Value As Int32)
                mIndividualMessageId = Value
            End Set
        End Property
        Private mIndividualMessageId As Int32
        <DOtoBOMapping("MessageId")> _
            Public Property MessageId() As Int32
            Get
                Return mMessageId
            End Get
            Set(ByVal Value As Int32)
                mMessageId = Value
            End Set
        End Property
        Private mMessageId As Int32

#End Region

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.Message.GetAll(includeHyphen, includeInactive, DataObjects.Base.MessageServiceBase.OrderBy.MessageTestOrder)
        End Function
    End Class
End Namespace




