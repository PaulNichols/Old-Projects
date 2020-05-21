Imports System.Xml.Serialization

Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.ScientificAdviceService)), _
     EntityMapping(GetType(DataObjects.Entity.ScientificAdvice)), _
     CollectionMapping(GetType(DataObjects.Collection.ScientificAdviceBoundCollection)), _
     Serializable()> _
    Public Class BOScientificAdvice
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " DO to BO Mapping "

        Private _UserId As Int64

        <DOtoBOMapping("UserRole")> _
        Public Shadows Property UserRole() As Int64
            Get
                Return _UserId
            End Get
            Set(ByVal Value As Int64)
                _UserId = Value
            End Set
        End Property

        Private mAdviceText As String

        <DOtoBOMapping("AdviceText")> _
        Public Property AdviceText() As String
            Get
                Return mAdviceText
            End Get
            Set(ByVal Value As String)
                mAdviceText = Value
            End Set
        End Property


        Private mCode As String

        <DOtoBOMapping("Code")> _
        Public Property Code() As String
            Get
                Return mCode
            End Get
            Set(ByVal Value As String)
                mCode = Value
            End Set
        End Property

        Private mEditable As Boolean

        <DOtoBOMapping("Editable")> _
        Public Overrides Property Editable() As Boolean
            Get
                Return mEditable
            End Get
            Set(ByVal Value As Boolean)
                mEditable = Value
            End Set
        End Property

        Private mSSORole As Integer

        <DOtoBOMapping("SSORole")> _
        Public Shadows Property SSORole() As Integer
            Get
                Return mSSORole
            End Get
            Set(ByVal Value As Integer)
                mSSORole = Value
            End Set
        End Property

        Private _RecommendedStatus As Integer

        <DOtoBOMapping("RecommendedStatus")> _
        Public Shadows Property RecommendedStatus() As Integer
            Get
                Return _RecommendedStatus
            End Get
            Set(ByVal Value As Integer)
                _RecommendedStatus = Value
            End Set
        End Property

        Private mDefaultAdviceText As String

        <DOtoBOMapping("DefaultAdviceText")> _
        Public Property DefaultAdviceText() As String
            Get
                Return mDefaultAdviceText
            End Get
            Set(ByVal Value As String)
                mDefaultAdviceText = Value
            End Set
        End Property

#End Region

        Public Sub New()

        End Sub

        Public Sub New(ByVal scientificAdviceId As Int32)
            MyClass.New(scientificAdviceId, Nothing)
        End Sub

        Public Sub New(ByVal scientificAdviceId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseScientificAdviceId(DataObjects.Entity.ScientificAdvice.GetById(scientificAdviceId, tran))
        End Sub

        Private Sub InitialiseScientificAdviceId(ByVal scientificAdvice As DataObjects.Entity.ScientificAdvice)
            ConvertDataObjectTOBO(Me, scientificAdvice)
        End Sub

        Private Function ConvertRowToDOEntity(ByVal pRow As DataRow) As DataObjects.Entity.ScientificAdvice

            Dim myObj As DataObjects.Entity.ScientificAdvice = New DataObjects.Entity.ScientificAdvice
            myObj.Populate(pRow)
            Return myObj

        End Function

        Public Overrides Function GetLookupData(ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean, ByVal SSOUser As Int64, ByVal SSORole As Integer) As BaseDataBO()
            Dim myS As New uk.gov.defra.EnterpriseObjects.Service

            Dim myDS As DataSet = [DO].DataObjects.Sprocs.dbo_usp_GetValidScientificAdvice_NonGenerated(CType(SSOUser, Int32), SSORole, Nothing, GetType(System.Data.DataSet))
            Dim myDOObj(myDS.Tables(0).Rows.Count - 1) As DataObjects.Entity.ScientificAdvice

            Dim myInt As Int32 = 0
            Dim myObj(myDS.Tables(0).Rows.Count - 1) As BO.ReferenceData.BOScientificAdvice

            ' First populate the DataObject with the DataSet ...
            For Each myRow As DataRow In myDS.Tables(0).Rows
                myDOObj(myInt) = ConvertRowToDOEntity(myRow)
                myInt += 1
            Next

            'Convert the DataObject to the Business Object
            For myInt = 0 To myDOObj.Length - 1
                myObj(myInt) = CType(ConvertDataObjectTOBO(myDOObj(myInt)), BO.ReferenceData.BOScientificAdvice)
            Next

            Return myObj

        End Function
    End Class
End Namespace


