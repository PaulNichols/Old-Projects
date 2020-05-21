Namespace Application.Search.Data
    Public Class CITESPermitSearchDataKewJNCC_ByApplication
        Inherits BaseCITESPermitSearchData_ByApplication
        Implements IISOCode, IAssignedTo, IDateReferred

        Public Sub New()
        End Sub

        Public Property ISOCode() As String Implements IISOCode.ISOCode
            Get
                Return mISOCode
            End Get
            Set(ByVal Value As String)
                mISOCode = Value
            End Set
        End Property
        Private mISOCode As String

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property DateReferred() As String Implements IDateReferred.DateReferred
            Get
                Return mDateReferred
            End Get
            Set(ByVal Value As String)
                mDateReferred = Value
            End Set
        End Property
        Private mDateReferred As String
        'Public Property ISO() As String
        '    Get
        '        Return mISO
        '    End Get
        '    Set(ByVal Value As String)
        '        mISO = Value
        '    End Set
        'End Property
        'Private mISO As String
    End Class
End Namespace