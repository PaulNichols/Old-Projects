Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.ApplicationTypeService)), _
     EntityMapping(GetType(DataObjects.Entity.ApplicationType)), _
     CollectionMapping(GetType(DataObjects.Collection.ApplicationTypeBoundCollection)), _
     Serializable()> _
    Public Class BOApplicationType
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " DOtoBOMapping Code "
        Private _Code As String
        Private _WLRSService As Int32
        Private _JNCCService1 As Int32
        Private _JNCCService2 As Int32
        Private _KewService1 As Int32
        Private _KewService2 As Int32
        Private _CitesYesNo As Boolean
        <DOtoBOMapping("CitesYesNo")> _
        Public Property CitesYesNo() As Boolean
            Get
                Return _CitesYesNo
            End Get
            Set(ByVal Value As Boolean)
                _CitesYesNo = Value
            End Set
        End Property
        Public Property CitesYesNoText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(_CitesYesNo)
            End Get
            Set(ByVal Value As String)
                _CitesYesNo = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        <DOtoBOMapping("JNCCService1")> _
        Public Property JNCCService1() As Int32
            Get
                Return _JNCCService1
            End Get
            Set(ByVal Value As Int32)
                _JNCCService1 = Value
            End Set
        End Property

        <DOtoBOMapping("JNCCService2")> _
        Public Property JNCCService2() As Int32
            Get
                Return _JNCCService2
            End Get
            Set(ByVal Value As Int32)
                _JNCCService2 = Value
            End Set
        End Property

        <DOtoBOMapping("KewService1")> _
        Public Property KewService1() As Int32
            Get
                Return _KewService1
            End Get
            Set(ByVal Value As Int32)
                _KewService1 = Value
            End Set
        End Property
        <DOtoBOMapping("KewService2")> _
        Public Property KewService2() As Int32
            Get
                Return _KewService2
            End Get
            Set(ByVal Value As Int32)
                _KewService2 = Value
            End Set
        End Property


        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal Value As String)
                _Code = Value
            End Set
        End Property


        <DOtoBOMapping("WLRSService")> _
        Public Property WLRSService() As Int32
            Get
                Return _WLRSService
            End Get
            Set(ByVal Value As Int32)
                _WLRSService = Value
            End Set
        End Property

#End Region

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.ApplicationType.GetAll(includeHyphen, includeInactive, DataObjects.Base.ApplicationTypeServiceBase.OrderBy.IX_ApplicationType)
        End Function
    End Class

End Namespace

