Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.DelegationGuidelineService)), _
     EntityMapping(GetType(DataObjects.Entity.DelegationGuideline)), _
     CollectionMapping(GetType(DataObjects.Collection.DelegationGuidelineBoundCollection)), _
     Serializable()> _
    Public Class BODelegationGuideline
        Inherits BO.ReferenceData.BOBaseReferenceTable

        Private _ApplicationTypeCode As Int32
        <DOtoBOMapping("ApplicationTypeCode")> _
        Public Property ApplicationTypeCode() As Int32
            Get
                Return _ApplicationTypeCode
            End Get
            Set(ByVal Value As Int32)
                _ApplicationTypeCode = Value
            End Set
        End Property

        Private _Code As Int32
        <DOtoBOMapping("Code")> _
        Public Property Code() As Int32
            Get
                Return _Code
            End Get
            Set(ByVal Value As Int32)
                _Code = Value
            End Set
        End Property

        Private _Subject As String
        <DOtoBOMapping("Subject")> _
        Public Property Subject() As String
            Get
                Return _Subject
            End Get
            Set(ByVal Value As String)
                _Subject = Value
            End Set
        End Property

        Private _Reason As String
        <DOtoBOMapping("Reason")> _
        Public Property Reason() As String
            Get
                Return _Reason
            End Get
            Set(ByVal Value As String)
                _Reason = Value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal Id As Int32)
            MyClass.New(Id, Nothing)
        End Sub

        Public Sub New(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseDelegation(DataObjects.Entity.DelegationGuideline.GetById(id, tran))
        End Sub

        Private Sub InitialiseDelegation(ByVal delegation As DataObjects.Entity.DelegationGuideline)
            ConvertDataObjectTOBO(Me, delegation)
        End Sub

        Public Overloads Shared Function GetAllByAppType(ByVal includeHyphen As Boolean, ByVal applicationtypeId As Int32) As [DO].DataObjects.Collection.DelegationGuidelineBoundCollection
            'TODO: There should be another way to convert from set to collection or the getbyindex should return a collection
            Dim DelegationGuidelineService As New DataObjects.Service.DelegationGuidelineService
            Dim DelegationCollection As New DataObjects.Collection.DelegationGuidelineBoundCollection
            Dim DelSet As DataObjects.EntitySet.DelegationGuidelineSet = DelegationGuidelineService.GetByIndex_IX_ApplicationTypeCode(applicationtypeId)
            If DelSet.Entities.Count > 0 Then DelegationCollection.EntitySet = DelSet
            Return DelegationCollection
        End Function

        Public Shared Shadows Function GetAll(ByVal includeHyphen As Boolean) As Collection.DelegationGuidelineBoundCollection
            Return Entity.DelegationGuideline.GetAll(includeHyphen).Entities
        End Function

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.DelegationGuideline.GetAll(includeHyphen, includeInactive, DataObjects.Base.DelegationGuidelineServiceBase.OrderBy.IX_DelegationGuideLineAndCode)
        End Function
    End Class
End Namespace


