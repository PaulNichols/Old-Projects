Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.CITESDerivativeService)), _
     EntityMapping(GetType(DataObjects.Entity.CITESDerivative)), _
     CollectionMapping(GetType(DataObjects.Collection.CITESDerivativeBoundCollection)), _
     Serializable()> _
    Public Class BOCITESDerivative
        '     EntityMapping(GetType(DataObjects.EntitySet.CITESDerivativeSet)), _
        Inherits BO.ReferenceData.BOBaseReferenceTable
        Implements ReferenceData.IBOCITESDerivative

        Protected Overloads Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.CITESDerivative.GetAll(includeHyphen, includeInactive, DataObjects.Base.CITESDerivativeServiceBase.OrderBy.IX_CITESDerivative)
        End Function

        Private mPreferredUOM_01 As BOUnitOfMeasurement
        Private mPreferredUOM_02 As BOUnitOfMeasurement
        Private mPreferredUOM_03 As BOUnitOfMeasurement

        Private mAlternativeUOM_01 As BOUnitOfMeasurement
        Private mAlternativeUOM_02 As BOUnitOfMeasurement
        Private mAlternativeUOM_03 As BOUnitOfMeasurement

        Public Property CodeDescription() As String
            Get
                Return String.Concat(Code, ", ", Description)
            End Get
            Set(ByVal Value As String)
            End Set
        End Property

        Public Property PreferredUOM_01Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.PreferredUOM_01Object
            Get
                Return mPreferredUOM_01
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mPreferredUOM_01 = Value
            End Set
        End Property

        <DOtoBOMapping("PreferredUOMId_01")> _
            Public Property PreferredUOMId_01() As Object
            Get
                If mPreferredUOM_01 Is Nothing OrElse mPreferredUOM_01.ID <= 0 Then
                    Return Nothing
                Else
                    Return mPreferredUOM_01.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mPreferredUOM_01 = Nothing
                ElseIf mPreferredUOM_01 Is Nothing OrElse mPreferredUOM_01.ID <> CType(Value, Int32) Then
                    mPreferredUOM_01 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        Public Property PreferredUOM_02Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.PreferredUOM_02Object
            Get
                Return mPreferredUOM_02
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mPreferredUOM_02 = Value
            End Set
        End Property

        <DOtoBOMapping("PreferredUOMId_02")> _
            Public Property PreferredUOMId_02() As Object
            Get
                If mPreferredUOM_02 Is Nothing OrElse mPreferredUOM_02.ID <= 0 Then
                    Return Nothing
                Else
                    Return mPreferredUOM_02.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mPreferredUOM_02 = Nothing
                ElseIf mPreferredUOM_02 Is Nothing OrElse mPreferredUOM_02.ID <> CType(Value, Int32) Then
                    mPreferredUOM_02 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        Public Property PreferredUOM_03Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.PreferredUOM_03Object
            Get
                Return mPreferredUOM_03
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mPreferredUOM_03 = Value
            End Set
        End Property

        <DOtoBOMapping("PreferredUOMId_03")> _
            Public Property PreferredUOMId_03() As Object
            Get
                If mPreferredUOM_03 Is Nothing OrElse mPreferredUOM_03.ID <= 0 Then
                    Return Nothing
                Else
                    Return mPreferredUOM_03.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mPreferredUOM_03 = Nothing
                ElseIf mPreferredUOM_03 Is Nothing OrElse mPreferredUOM_03.ID <> CType(Value, Int32) Then
                    mPreferredUOM_03 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        Public Property AlternativeUOM_01Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.AlternativeUOM_01Object
            Get
                Return mAlternativeUOM_01
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mAlternativeUOM_01 = Value
            End Set
        End Property

        <DOtoBOMapping("AlternativeUOMId_01")> _
            Public Property AlternativeUOMId_01() As Object
            Get
                If mAlternativeUOM_01 Is Nothing OrElse mAlternativeUOM_01.ID <= 0 Then
                    Return Nothing
                Else
                    Return mAlternativeUOM_01.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mAlternativeUOM_01 = Nothing
                ElseIf mAlternativeUOM_01 Is Nothing OrElse mAlternativeUOM_01.ID <> CType(Value, Int32) Then
                    mAlternativeUOM_01 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        Public Property AlternativeUOM_02Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.AlternativeUOM_02Object
            Get
                Return mAlternativeUOM_02
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mAlternativeUOM_02 = Value
            End Set
        End Property

        <DOtoBOMapping("AlternativeUOMId_02")> _
            Public Property AlternativeUOMId_02() As Object
            Get
                If mAlternativeUOM_02 Is Nothing OrElse mAlternativeUOM_02.ID <= 0 Then
                    Return Nothing
                Else
                    Return mAlternativeUOM_02.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mAlternativeUOM_02 = Nothing
                ElseIf mAlternativeUOM_02 Is Nothing OrElse mAlternativeUOM_02.ID <> CType(Value, Int32) Then
                    mAlternativeUOM_02 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        Public Property AlternativeUOM_03Object() As BOUnitOfMeasurement Implements IBOCITESDerivative.AlternativeUOM_03Object
            Get
                Return mAlternativeUOM_03
            End Get
            Set(ByVal Value As BOUnitOfMeasurement)
                mAlternativeUOM_03 = Value
            End Set
        End Property

        <DOtoBOMapping("AlternativeUOMId_03")> _
            Public Property AlternativeUOMId_03() As Object
            Get
                If mAlternativeUOM_03 Is Nothing OrElse mAlternativeUOM_03.ID <= 0 Then
                    Return Nothing
                Else
                    Return mAlternativeUOM_03.ID
                End If
            End Get
            Set(ByVal Value As Object)
                If Value Is Nothing OrElse Value Is Convert.DBNull Then
                    mAlternativeUOM_03 = Nothing
                ElseIf mAlternativeUOM_03 Is Nothing OrElse mAlternativeUOM_03.ID <> CType(Value, Int32) Then
                    mAlternativeUOM_03 = New BO.BOUnitOfMeasurement(CType(Value, Int32))
                End If
            End Set
        End Property

        <DOtoBOMapping("Explanation")> _
        Public Property Explanation() As String Implements IBOCITESDerivative.Explanation
            Get
                Return mExplanation
            End Get
            Set(ByVal Value As String)
                mExplanation = Value
            End Set
        End Property
        Private mExplanation As String


        <DOtoBOMapping("SortSequence")> _
       Public Property SortSequence() As Integer Implements IBOCITESDerivative.SortSequence
            Get
                Return mSortSequence
            End Get
            Set(ByVal Value As Integer)
                mSortSequence = Value
            End Set
        End Property
        Private mSortSequence As Int32

        Private mCode As String
        <DOtoBOMapping("Code")> _
        Public Property Code() As String Implements IBOCode.Code
            Get
                If Not mCode Is Nothing Then
                    Return (mCode.ToUpper)
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)
                mCode = Value
            End Set
        End Property

        Public Sub New()

        End Sub

        Public Sub New(ByVal derivativeId As Int32)
            MyClass.New(derivativeId, Nothing)
        End Sub

        Public Sub New(ByVal derivativeId As Int32, ByVal tran As SqlClient.SqlTransaction)
            'Dim Country As New DataObjects.Entity.Country(countryId)
            InitialiseDerivative(DataObjects.Entity.CITESDerivative.GetById(derivativeId, tran))
        End Sub
        Public Sub New(ByVal derivative As DataObjects.Entity.CITESDerivative)
            InitialiseDerivative(derivative)
        End Sub
        Private Sub InitialiseDerivative(ByVal derivative As DataObjects.Entity.CITESDerivative)
            ConvertDataObjectTOBO(Me, derivative)
            With derivative
                If Not .IsPreferredUOMId_01Null Then
                    mPreferredUOM_01 = New BOUnitOfMeasurement(.PreferredUOMId_01)
                End If
                If Not .IsPreferredUOMId_02Null Then
                    mPreferredUOM_02 = New BOUnitOfMeasurement(.PreferredUOMId_02)
                End If
                If Not .IsPreferredUOMId_03Null Then
                    mPreferredUOM_03 = New BOUnitOfMeasurement(.PreferredUOMId_03)
                End If
                If Not .IsAlternativeUOMId_01Null Then
                    mAlternativeUOM_01 = New BOUnitOfMeasurement(.AlternativeUOMId_01)
                End If
                If Not .IsAlternativeUOMId_02Null Then
                    mAlternativeUOM_02 = New BOUnitOfMeasurement(.AlternativeUOMId_02)
                End If
                If Not .IsAlternativeUOMId_03Null Then
                    mAlternativeUOM_03 = New BOUnitOfMeasurement(.AlternativeUOMId_03)
                End If
                mExplanation = .Explanation
                mSortSequence = .SortSequence
                mCode = .Code
            End With
            derivative = Nothing
        End Sub

        Public Function GetAllOrdered(ByVal includeHyphen As Boolean, ByVal includeActive As Boolean) As DataObjects.Collection.CITESDerivativeBoundCollection
            Try
                Dim Service As DataObjects.Service.CITESDerivativeService = DataObjects.Entity.CITESDerivative.ServiceObject
                If Not Service Is Nothing Then
                    Dim ES As DataObjects.EntitySet.CITESDerivativeSet = Service.GetAll(includeHyphen, includeActive, DataObjects.Base.CITESDerivativeServiceBase.OrderBy.IX_CITESDerivative_ComboOrder)
                    Return CType(ES.GetBoundCollection(ES, 0, GetType(DataObjects.Collection.CITESDerivativeBoundCollection)), DataObjects.Collection.CITESDerivativeBoundCollection)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Class
End Namespace