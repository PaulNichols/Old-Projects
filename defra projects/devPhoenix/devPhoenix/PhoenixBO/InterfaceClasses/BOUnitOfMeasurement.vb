<ServiceMapping(GetType(DataObjects.Service.CITESUnitOfMeasurementService)), _
 EntityMapping(GetType(DataObjects.Entity.CITESUnitOfMeasurement)), _
 CollectionMapping(GetType(DataObjects.Collection.CITESUnitOfMeasurementBoundCollection)), _
 Serializable()> _
Public Class BOUnitOfMeasurement
    Inherits ReferenceData.BOBaseReferenceTable 'BOBaseLookUpTable
    Implements IBOUnitOfMeasurement

    Public Shared Function Create(ByVal id As Int32) As BOUnitOfMeasurement
        Dim UOMDO As New DataObjects.Entity.CITESUnitOfMeasurement(id)
        'create a instance of me in order to get the data out
        Dim BOUOM As New BOUnitOfMeasurement
        Return CType(BOUOM.ConvertDataObjectTOBO(UOMDO), BOUnitOfMeasurement)
    End Function

    '<DOtoBOMapping("Active")> _
    'Public Property Active() As Boolean Implements IBOCode.Active
    '    Get
    '        Return mActive
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        mActive = Value
    '    End Set
    'End Property
    'Private mActive As Boolean

    <DOtoBOMapping("Code")> _
        Public Property Code() As String Implements IBOCode.Code
        Get
            Return mCode
        End Get
        Set(ByVal Value As String)
            mCode = Value
        End Set
    End Property
    Private mCode As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal measurementID As Int32)
        MyClass.New(measurementID, Nothing)
    End Sub

    Public Sub New(ByVal measurement As Object)
        If measurement.GetType Is Integer.MinValue.GetType Then
            InitialiseUOM(DataObjects.Entity.CITESUnitOfMeasurement.GetById(CType(measurement, Integer), Nothing))
        Else
            InitialiseUOM(CType(measurement, DataObjects.Entity.CITESUnitOfMeasurement))
        End If
    End Sub


    Public Sub New(ByVal measurementID As Int32, ByVal tran As SqlClient.SqlTransaction)
        'Dim Country As New DataObjects.Entity.Country(countryId)
        InitialiseUOM(DataObjects.Entity.CITESUnitOfMeasurement.GetById(measurementID, tran))
    End Sub

    Private Sub InitialiseUOM(ByVal measurement As DataObjects.Entity.CITESUnitOfMeasurement)
        ConvertDataObjectTOBO(Me, measurement)
    End Sub

    Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
        Return DataObjects.Entity.CITESUnitOfMeasurement.GetAll(includeHyphen, includeInactive, DataObjects.Base.CITESUnitOfMeasurementServiceBase.OrderBy.DefaultOrder)
    End Function
End Class
