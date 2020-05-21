Namespace Application.CITES
    Public Class BONotificationSpecie
        Inherits BOSpecie
        Implements Application.CITES.INotificationSpecie

        Public Property Derivative() As ReferenceData.BOCITESDerivative Implements INotificationSpecie.Derivative
            Get
                Return mDerivative
            End Get
            Set(ByVal Value As ReferenceData.BOCITESDerivative)
                mDerivative = Value
            End Set
        End Property
        Private mDerivative As ReferenceData.BOCITESDerivative

        Public Property UOM() As BOMeasurement Implements INotificationSpecie.UOM
            Get
                Return mUOM
            End Get
            Set(ByVal Value As BOMeasurement)
                mUOM = Value
            End Set
        End Property
        Private mUOM As BOMeasurement

#Region "Helper Functions"

        Public ReadOnly Property UOMID() As Int32
            Get
                If Not UOM Is Nothing AndAlso UOM.UOMId > 0 Then
                    Return UOM.UOMId
                End If
            End Get
        End Property

        Public ReadOnly Property DerivativeId() As Object
            Get
                If Not Me.Derivative Is Nothing AndAlso Me.Derivative.ID > 0 Then
                    Return Me.Derivative.ID
                End If
            End Get
        End Property
#End Region

#Region "Save"
        Public Overridable Shadows Function Save(ByVal citesNotificationId As Int32, ByVal tran As SqlClient.SqlTransaction) As BOSpecie
            Dim NewBOSpecie As New DataObjects.Entity.Specie
            Dim service As DataObjects.Service.SpecieService = NewBOSpecie.ServiceObject

            Dim ThisSpecie As BONotificationSpecie = MyClass.Save(tran:=tran)

            If ThisSpecie Is Nothing Then
                ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecie)
                Return Me
            Else
                If Not Application.CITES.BOCITESNotification.SetLinks(citesNotificationId, ThisSpecie, tran, False) Then
                    service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    ValidationErrors = New ValidationManager(New ValidationError() {New ValidationError(ValidationError.ValidationCodes.DatabaseErrors)}, ValidationManager.ValidationTitles.CannotSaveSpecie)
                    Return Me
                End If
            End If
            Return ThisSpecie
        End Function

        Public Overridable Shadows Function Save(ByVal tran As SqlClient.SqlTransaction) As BONotificationSpecie
            Created = (Me.SpecieId = 0)
            MyBase.Save(tran)
            Validate()


            If Not mUOM Is Nothing AndAlso Not mUOM.IsEmpty Then
                'there might not be a UOM
                mUOM = mUOM.Save(tran)
            End If
            
            Return Me
        End Function
#End Region

        Public Sub New(ByVal specieId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadSpecie(specieId, tran)
          
        End Sub

        Protected Overrides Function LoadSpecie(ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.Specie
            MyBase.LoadSpecie(id, tran)
            Dim DOSpecie As New [DO].DataObjects.Entity.Specie(SpecieId, tran)
            Dim LinkedNotificationDetails As [DO].DataObjects.EntitySet.NotificationSpecieLinkSet = DOSpecie.GetRelatedNotificationSpecieLink(tran)
            If Not LinkedNotificationDetails Is Nothing AndAlso LinkedNotificationDetails.Entities.Count > 0 Then
                With LinkedNotificationDetails.Entities(0)
                    If .UOMId > 0 Then Me.mUOM = New BOMeasurement(.UOMId, tran)
                    If .CITESDerivativeId > 0 Then Me.mDerivative = New ReferenceData.BOCITESDerivative(.CITESDerivativeId, tran)
                End With
            End If
            ' Return Me
        End Function

        Public Sub New()
            MyBase.New()
        End Sub

        Public Overloads Overrides Sub InitialiseSpecie(ByVal specie As BOSpecie, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.InitialiseSpecie(specie, tran)
            With CType(specie, BONotificationSpecie)
                Derivative = .Derivative
                Me.UOM = .UOM
            End With
        End Sub
    End Class
End Namespace