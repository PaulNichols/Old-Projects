Namespace Application.Search
    <Serializable()> _
    Public Class ApplicationSearchCriteriaCommon_Customer
        Inherits ApplicationSearchCriteriaBase
        Implements IApplicationSearchCommon_Customer, ISearchApplicationId

        Public Sub New()
        End Sub

        Public Property ApplicationId() As Int32 Implements ISearchApplicationId.ApplicationId
            Get
                Return mApplicationId
            End Get
            Set(ByVal Value As Int32)
                mApplicationId = Value
            End Set
        End Property
        Private mApplicationId As Int32

        Public Property DateIssued() As DateRange Implements IApplicationSearchCommon_Customer.DateIssued
            Get
                Return mDateIssued
            End Get
            Set(ByVal Value As DateRange)
                mDateIssued = Value
            End Set
        End Property
        Private mDateIssued As DateRange

        Public Property AcceptedScientificName() As String Implements IApplicationSearchCommon_Customer.AcceptedScientificName
            Get
                Return mAcceptedScientificName
            End Get
            Set(ByVal Value As String)
                mAcceptedScientificName = Value
            End Set
        End Property
        Private mAcceptedScientificName As String

        Public Property IDMarkType() As ReferenceData.BOIDMarkType Implements IApplicationSearchCommon_Customer.IDMarkType
            Get
                Return mIDMarkType
            End Get
            Set(ByVal Value As ReferenceData.BOIDMarkType)
                mIDMarkType = Value
            End Set
        End Property
        Private mIDMarkType As ReferenceData.BOIDMarkType

        Public Property IDMarkNumber() As String Implements IApplicationSearchCommon_Customer.IDMarkNumber
            Get
                Return mIDMarkNumber
            End Get
            Set(ByVal Value As String)
                mIDMarkNumber = Value
            End Set
        End Property
        Private mIDMarkNumber As String
    End Class
End Namespace
