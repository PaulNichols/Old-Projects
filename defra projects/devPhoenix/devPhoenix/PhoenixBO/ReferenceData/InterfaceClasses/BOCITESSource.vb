
Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.CITESSourceService)), _
     EntityMapping(GetType(DataObjects.Entity.CITESSource)), _
     CollectionMapping(GetType(DataObjects.Collection.CITESSourceBoundCollection)), _
     Serializable()> _
    Public Class BOCITESSource
        Inherits BO.ReferenceData.BOBaseReferenceTable
        Private m_ListEntry As String

        <DOtoBOMapping("CodeDescription")> _
       Public Property CodeDescription() As String
            Get
                Return mCodeDescription
            End Get
            Set(ByVal Value As String)
                mCodeDescription = Value
            End Set
        End Property
        Private mCodeDescription As String

        Private mCode As String
        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                If mCode Is Nothing Then
                    Return Nothing
                Else
                    Return mCode.ToUpper
                End If
            End Get
            Set(ByVal Value As String)
                mCode = Value
            End Set
        End Property

        <DOtoBOMapping("ListEntry")> _
Public Property ListEntry() As String
            Get
                Return m_ListEntry
            End Get
            Set(ByVal Value As String)
                m_ListEntry = Value
            End Set
        End Property

        <DOtoBOMapping("AdditionalSource")> _
     Public Property AdditionalSource() As Boolean
            Get
                Return mAdditionalSource
            End Get
            Set(ByVal Value As Boolean)
                mAdditionalSource = Value
            End Set
        End Property

        Public Property AdditionalSourceText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(mAdditionalSource)
            End Get
            Set(ByVal Value As String)
                mAdditionalSource = Application.Search.ApplicationSearch.ConvertEnglishToBoolean(Value)
            End Set
        End Property

        Private mAdditionalSource As Boolean

        Public Shared Shadows Function GetAllSecondSource() As BOCITESSource()
            Dim Service As New DataObjects.Service.CITESSourceService
            Dim SourceCollection As DataObjects.EntitySet.CITESSourceSet = Service.GetByIndex_IX_CITESSource_1(0, False)
            If Not SourceCollection Is Nothing Then
                Dim ReturnArray(SourceCollection.Entities.Count - 1) As BO.ReferenceData.BOCITESSource
                Dim i As Int32

                For Each Source As DataObjects.Entity.CITESSource In SourceCollection.Entities
                    ReturnArray(i) = New BO.ReferenceData.BOCITESSource(Source.SourceId)
                    i += 1
                Next
                Return ReturnArray
            End If
        End Function

        Public Sub New()

        End Sub

        Public Sub New(ByVal sourceId As Int32)
            MyClass.New(sourceId, Nothing)
        End Sub

        Public Sub New(ByVal sourceId As Int32, ByVal tran As SqlClient.SqlTransaction)
            InitialiseSource(DataObjects.Entity.CITESSource.GetById(sourceId, tran))
        End Sub

        Private Sub InitialiseSource(ByVal citesSource As DataObjects.Entity.CITESSource)
            ConvertDataObjectTOBO(Me, citesSource)
        End Sub

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.CITESSource.GetAll(includeHyphen, includeInactive, DataObjects.Base.CITESSourceServiceBase.OrderBy.IX_CITESSource)
        End Function
    End Class
End Namespace



