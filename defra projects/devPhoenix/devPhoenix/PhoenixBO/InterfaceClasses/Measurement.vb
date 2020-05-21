Public Class BOMeasurement
    Inherits BaseBO
    Implements IMeasurement

#Region " Helper code "

    Public Function IsEmpty() As Boolean
        Return ((mMass Is Nothing OrElse mMass.ToString = "" OrElse CType(mMass, Decimal) = 0) AndAlso (mQty Is Nothing OrElse mQty.ToString = "" OrElse CType(mQty, Int32) = 0) AndAlso UOM.Code Is Nothing)
    End Function

    Public ReadOnly Property NetMass() As String
        Get
            If UOM Is Nothing Then
                Return mMass.ToString.Trim
            Else
                Dim Description As String = String.Empty
                If Not UOM.Description Is Nothing Then Description = UOM.Description.Trim
                If mMass Is Nothing Then
                    Return String.Empty
                Else
                    Return String.Concat(mMass.ToString.Trim, " ", Description)
                End If
            End If

        End Get
    End Property

#End Region

#Region " Prelim code "
    Public Sub New()
        MyBase.New()
        mUOM = New BO.BOUnitOfMeasurement
    End Sub

    Public Sub New(ByVal uomId As Int32, ByVal tran As SqlClient.SqlTransaction)
        MyClass.New()
        LoadUOM(uomId, tran)
    End Sub

    Private Function LoadUOM(ByVal id As Int32) As DataObjects.Entity.UOM
        Return LoadUOM(id, Nothing)
    End Function

    Private Function LoadUOM(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.UOM
        Dim NewUOM As DataObjects.Entity.UOM = DataObjects.Entity.UOM.GetById(id, tran)
        If NewUOM Is Nothing Then
            Throw New RecordDoesNotExist("UOM", id)
        Else
            InitialiseUOM(NewUOM, tran)
            Return NewUOM
        End If
    End Function

    Protected Overridable Sub InitialiseUOM(ByVal uom As DataObjects.Entity.UOM, ByVal tran As SqlClient.SqlTransaction)
        With uom
            'set myself up
            mUOMId = .Id
            CheckSum = .CheckSum
            If Not .IsMassNull Then mMass = .Mass
            If Not .IsQtyNull Then mQty = .Qty
            If .UnitOfMeasurementId > 0 Then
                mUOM = BO.BOUnitOfMeasurement.Create(.UnitOfMeasurementId)
            End If
        End With
    End Sub
#End Region

#Region " Properties "
    Public Property Mass() As Object Implements IMeasurement.Mass
        Get
            Dim ReturnValue As Object = Nothing
            Try
                Dim DecimalValue As Decimal
                DecimalValue = CType(mMass, Decimal)
                If DecimalValue > 0 Then ReturnValue = DecimalValue
            Catch ex As InvalidCastException
                'not a valid decimal so return nothing
            End Try
            Return ReturnValue
        End Get
        Set(ByVal Value As Object)
            mMass = Value
        End Set
    End Property
    Private mMass As Object

    Public Property Qty() As Object Implements IMeasurement.Qty
        Get
            Dim ReturnValue As Object = Nothing
            Try
                Dim IntValue As Int32
                IntValue = CType(mQty, Int32)
                If IntValue > 0 Then ReturnValue = IntValue
            Catch ex As InvalidCastException
                'not a valid int32 so return nothing
            End Try
            Return ReturnValue
        End Get
        Set(ByVal Value As Object)
            mQty = Value
        End Set
    End Property
    Private mQty As Object

    Public Property UOM() As BOUnitOfMeasurement Implements IMeasurement.UOM
        Get
            Return mUOM
        End Get
        Set(ByVal Value As BOUnitOfMeasurement)
            mUOM = Value
        End Set
    End Property
    Private mUOM As BOUnitOfMeasurement

    Public Property UOMId() As Int32 Implements IMeasurement.UOMId
        Get
            Return mUOMId
        End Get
        Set(ByVal Value As Int32)
            mUOMId = Value
        End Set
    End Property
    Private mUOMId As Int32
#End Region

#Region " Save "
    Public Overridable Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BOMeasurement
        MyBase.Save()
        Dim NewUOM As New DataObjects.Entity.UOM
        Dim service As DataObjects.Service.UOMService = NewUOM.ServiceObject
        Created = (UOMId = 0)

        Dim MeasurementId As Object
        If Not (mUOM Is Nothing OrElse mUOM.ID = 0) Then
            MeasurementId = mUOM.ID
        End If

        If Created Then
            NewUOM = service.Insert(Mass, _
                                    Qty, _
                                    MeasurementId, _
                                    tran)
        Else
            NewUOM = service.Update(mUOMId, _
                                    Mass, _
                                    Qty, _
                                    MeasurementId, _
                                    CheckSum, _
                                    tran)
        End If

        If NewUOM Is Nothing AndAlso Not DataObjects.Sprocs.LastError Is Nothing Then
            'TODO: Use errors collection to check to see if the problem was concurrency
            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveMeasurement)
        ElseIf NewUOM Is Nothing Then
            service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveMeasurement)
        Else
            If Created And Not NewUOM Is Nothing Then
                mUOMId = NewUOM.Id
            End If
            If NewUOM.CheckSum <> CheckSum Then
                'no point in initialising unless things have changed
                InitialiseUOM(NewUOM, tran)
            End If
        End If

        Return Me
    End Function
#End Region

#Region " Validate "
    Public Function Validate() As ValidationError
        MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSaveUOM)
        Return Validate(MyBase.ValidationErrors)
    End Function

    Public Function Validate(ByVal validationMan As ValidationManager) As ValidationError
        If (Not mUOM Is Nothing AndAlso mUOM.ID > 0) AndAlso (mMass Is Nothing OrElse CType(mMass, Decimal) <= 0) Then
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.MustHaveAMassIfUOMSelected))
        ElseIf (Not mMass Is Nothing AndAlso CType(mMass, Decimal) > 0) AndAlso (mUOM Is Nothing OrElse mUOM.ID <= 0) Then
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.MustHaveAUOMIfMassGreaterThanZero))
        ElseIf (mUOM Is Nothing OrElse mUOM.ID <= 0) AndAlso (mMass Is Nothing OrElse CType(mMass, Decimal) <= 0) AndAlso (mQty Is Nothing OrElse CType(mQty, Int32) <= 0) Then
            'nothing selected!
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.MustHaveEitherAQtyOrUOMAndMass))
        ElseIf (Not mUOM Is Nothing AndAlso mUOM.ID >= 0) AndAlso (Not mMass Is Nothing AndAlso CType(mMass, Decimal) > 0) AndAlso (Not mQty Is Nothing AndAlso CType(mQty, Int32) > 0) Then
            'everything selected!
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.MustHaveEitherAQtyOrUOMAndMass))
        ElseIf (Not mUOM Is Nothing AndAlso mUOM.ID >= 0) AndAlso (Not mQty Is Nothing AndAlso CType(mQty, Int32) > 0) Then
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.CannotHaveUOMAndQuantity))
        ElseIf (Not mMass Is Nothing AndAlso CType(mMass, Decimal) > 0) AndAlso (Not mQty Is Nothing AndAlso CType(mQty, Int32) > 0) Then
            validationMan.AddError(New ValidationError(ValidationError.ValidationCodes.CannotHaveMassAndQuantity))
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region " Operations "
    Shared Function IsEqual(ByVal left As BOMeasurement, ByVal right As BOMeasurement) As Boolean
        If left Is Nothing And right Is Nothing Then
            Return True
        ElseIf Not left Is Nothing AndAlso Not right Is Nothing Then
            Return (left.UOMId = right.UOMId)
        Else
            Return False
        End If
        Return True
    End Function
#End Region

#Region " Test "
    '    Public Function Test() As Boolean

    '    End Function

    '    Private Function TestLoad() As Boolean
    '        ' load one that shouldn't exist
    '        Dim This As New BOMeasurement(0)
    '        If Not This Is Nothing Then Return False
    '    End Function

    '    Private Function TestUpdate() As Boolean

    '    End Function

    '    Private Function TestInsert() As Boolean
    '        'create a new item
    '        Dim This As New BOMeasurement
    '        With This
    '            .
    '        End With
    '    End Function
#End Region

    Public Overrides Function clone() As Object
        Dim ClonedObject As BOMeasurement = CType(MyBase.Clone, BOMeasurement)

        With ClonedObject
            .UOMId = 0
        End With
        Return ClonedObject
    End Function
End Class
