Public Class DirectServiceObjectFactory
    Inherits ServiceObjectFactory

    Public Overrides Function Create(ByVal serviceObjectType As System.Type) As EnterpriseObjects.Service
        Return CType(System.Activator.CreateInstance(serviceObjectType), EnterpriseObjects.Service)
    End Function

    Public Overrides Function ToString() As String
        Return "Direct"
    End Function

End Class
