
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.PhoenixParameterService)), _
     EntityMapping(GetType(DataObjects.Entity.PhoenixParameter)), _
     CollectionMapping(GetType(DataObjects.Collection.PhoenixParameterBoundCollection)), _
     Serializable()> _
    Public Class BOPhoenixParameter
        Inherits BO.ReferenceData.BOBaseReferenceTable
#Region " DOtoBOMapping "
        Private _ParamID As String
        <DOtoBOMapping("ParamName")> _
        Public Property ParamName() As String
            Get
                Return _ParamID
            End Get
            Set(ByVal Value As String)
                _ParamID = Value
            End Set
        End Property

        Private _Value As String
        <DOtoBOMapping("ParamValue")> _
        Public Property ParamValue() As String
            Get
                Return _Value
            End Get
            Set(ByVal Value As String)
                _Value = Value
            End Set
        End Property

        Private _RegExpId As Integer
        <DOtoBOMapping("ValidationRegExID")> _
        Public Property ValidationRegExID() As Integer
            Get
                Return _RegExpId
            End Get
            Set(ByVal Value As Integer)
                _RegExpId = Value
                PopulateRegularExpression()
            End Set
        End Property

        Private Sub PopulateRegularExpression()

            Dim myDOVal As uk.gov.defra.Phoenix.DO.DataObjects.Entity.ValidationRegEx = DataObjects.Entity.ValidationRegEx.GetById(_RegExpId)
            _RegularExpression = myDOVal.RegEx
            myDOVal = Nothing

        End Sub
        Private _RegularExpression As String

        Public Property RegularExpression() As String
            Get
                Return _RegularExpression
            End Get
            Set(ByVal Value As String)
                _RegularExpression = Value
            End Set
        End Property

        Private _ValidationMessage As String
        <DOtoBOMapping("ValidationMessage")> _
        Public Property ValidationMessage() As String
            Get
                Return _ValidationMessage
            End Get
            Set(ByVal Value As String)
                _ValidationMessage = Value
            End Set
        End Property

#End Region

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.PhoenixParameter.GetAll(includeHyphen, includeInactive, DataObjects.Base.PhoenixParameterServiceBase.OrderBy.UQ_PhoenixParameter_ParamName)
        End Function
    End Class
End Namespace





