Public MustInherit Class ServiceObjectFactory

    ' Create method...
    Public MustOverride Function Create(ByVal serviceObjectType As Type) As Service

End Class
