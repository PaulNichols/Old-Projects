Namespace Taxonomy
    Public Class BOUsage
        Implements IUsage

#Region " Prelim Code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction)
            LoadObject(source, id, tran)
        End Sub

        Public Sub New(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal id As Int32)
            LoadObject(Source, id, Nothing)
        End Sub

        Private Overloads Function LoadObject(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal id As Int32) As DataObjects.Entity.TaxonomyUsage
            Return Me.LoadObject(Source, id, Nothing)
        End Function

        Private Overloads Function LoadObject(ByVal Source As TaxonomyData.TaxonomyRowSourceEnum, ByVal id As Int32, ByVal tran As SqlClient.SqlTransaction) As DataObjects.Entity.TaxonomyUsage
            Dim NewDO As DataObjects.Entity.TaxonomyUsage = DataObjects.Entity.TaxonomyUsage.GetById(Source, id, tran)
            If NewDO Is Nothing Then
                Throw New RecordDoesNotExist("Usage", id)
            Else
                InitialiseObject(NewDO, tran)
                Return NewDO
            End If
        End Function

        Protected Overridable Sub InitialiseObject(ByVal NewDO As DataObjects.Entity.TaxonomyUsage, ByVal tran As SqlClient.SqlTransaction)
            With NewDO
                Me.Level = _
                    DataObjects.Entity.TaxonomyLevelOfUse.GetById( _
                    .LevelOfUseID, tran).LevelOfUseDescription
                Me.LevelID = .LevelOfUseID
                Me.Part = _
                    DataObjects.Entity.TaxonomyPart.GetById(.PartID, tran).PartDescription
                Me.PartID = .PartID
                Me.Type = _
                    DataObjects.Entity.TaxonomyUsageType.GetById(.UsageTypeID, tran).UsageTypeDescription
                Me.TypeID = .UsageTypeID
            End With
        End Sub

#End Region

        Private PartValue As String
        Public Property Part() As String Implements IUsage.Part
            Get
                Return PartValue
            End Get
            Set(ByVal Value As String)
                PartValue = Value
            End Set
        End Property

        Private LevelValue As String
        Public Property Level() As String Implements IUsage.Level
            Get
                Return LevelValue
            End Get
            Set(ByVal Value As String)
                LevelValue = Value
            End Set
        End Property

        Private TypeValue As String
        Public Property Type() As String Implements IUsage.Type
            Get
                Return TypeValue
            End Get
            Set(ByVal Value As String)
                TypeValue = Value
            End Set
        End Property

        Private LevelIDValue As Int32
        Public Property LevelID() As Int32 Implements IUsage.LevelID
            Get
                Return LevelIDValue
            End Get
            Set(ByVal Value As Int32)
                LevelIDValue = Value
            End Set
        End Property

        Private PartIDValue As Int32
        Public Property PartID() As Int32 Implements IUsage.PartID
            Get
                Return PartIDValue
            End Get
            Set(ByVal Value As Int32)
                PartIDValue = Value
            End Set
        End Property

        Private TypeIDValue As Int32
        Public Property TypeID() As Int32 Implements IUsage.TypeID
            Get
                Return TypeIDValue
            End Get
            Set(ByVal Value As Int32)
                TypeIDValue = Value
            End Set
        End Property
    End Class
End Namespace