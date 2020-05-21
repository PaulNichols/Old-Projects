Namespace SearchTaxonomy
    <Serializable()> Public Class BOUsageCriteria
        Implements IUsageCriteria

#Region " Properties "

        Public Sub New()
            'This constructor is intentionally empty, but defined for explicitness.
        End Sub

        Public Property LevelOfUseID() As Int32 Implements IUsageCriteria.LevelOfUseID
            Get
                Return mLevelOfUseID
            End Get
            Set(ByVal Value As Integer)
                mLevelOfUseID = Value
            End Set
        End Property
        Private mLevelOfUseID As Int32

        Public Property PartID() As Int32 Implements IUsageCriteria.PartID
            Get
                Return mPartID
            End Get
            Set(ByVal Value As Integer)
                mPartID = Value
            End Set
        End Property
        Private mPartID As Int32

        Public Property UsageTypeID() As Int32 Implements IUsageCriteria.UsageTypeID
            Get
                Return mUsageTypeID
            End Get
            Set(ByVal Value As Integer)
                mUsageTypeID = Value
            End Set
        End Property
        Private mUsageTypeID As Int32

#End Region

    End Class
End Namespace