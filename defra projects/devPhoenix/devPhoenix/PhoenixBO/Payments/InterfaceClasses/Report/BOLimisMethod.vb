Namespace ReportLimis
    Public Class BOLimisMethod
        
        Public Enum MethodType
            Email = 1
            Fax = 2
            Internet = 3
            Phone = 5
            Post = 6
        End Enum

        Sub New(ByVal applicationTypeId As Int32, ByVal method As Int32, ByVal total As Int32)
            mApplicationTypeId = applicationTypeId
            mMethod = method
            mTotal = total
        End Sub

        Private mApplicationTypeId As Int32
        Private mMethod As Int32
        Private mTotal As Int32
        
        Public ReadOnly Property ApplicationTypeId As Int32
            Get
                Return mApplicationTypeId
            End Get
        End Property
        
        Public ReadOnly Property Method As Int32
            Get
                Return mMethod
            End Get
        End Property
        
        Public ReadOnly Property Total As Int32
            Get
                Return mTotal
            End Get
        End Property

        Public Shared Function GetByMonth(ByVal month As Int32, ByVal year As Int32) As BOLimisMethod()
            Dim startDate As New Date(year, month, 1)
            Dim endDate As New Date(year, month, Date.DaysInMonth(year, month))
            Dim entities As EnterpriseObjects.EntitySet = DataObjects.Entity.Application.GetMethodTotals(startDate, endDate)
            Dim results(-1) As BOLimisMethod
            
            If Not entities Is Nothing Then
                Redim results(entities.Count - 1)
                For i As Int32 = 0 To entities.Count - 1
                    Dim entity As EnterpriseObjects.Entity = entities.GetEntity(i)
                    Dim appType As Int32 = CType(entity(0), Int32)
                    Dim method As Int32  = CType(entity(1), Int32)
                    Dim total As Int32   = CType(entity(2), Int32)
                    results(i) = New BOLimisMethod(appType, method, total)
                Next
            End If
            Return results
        End Function
    End Class
End Namespace
