Namespace Application.Search.Data
    Public Class CITESPermitSearchDataKewJNCC_ByPermit
        Inherits BaseCITESPermitSearchData_ByPermit
        Implements IAssignedTo, ISAAdvice, IReferred, IDateReferred, IQuantity, ICountryOfOrigin, IInspectorateAdvice

        Public Sub New()
        End Sub

        Public Property DateReferred() As String Implements IDateReferred.DateReferred
            Get
                Return mDateReferred
            End Get
            Set(ByVal Value As String)
                mDateReferred = Value
            End Set
        End Property
        Private mDateReferred As String

        Public Property CountryOfOrigin() As String Implements ICountryOfOrigin.CountryOfOrigin
            Get
                Return mCountryOfOrigin
            End Get
            Set(ByVal Value As String)
                mCountryOfOrigin = Value
            End Set
        End Property
        Private mCountryOfOrigin As String

        Public Property AssignedTo() As String Implements IAssignedTo.AssignedTo
            Get
                Return mAssignedTo
            End Get
            Set(ByVal Value As String)
                mAssignedTo = Value
            End Set
        End Property
        Private mAssignedTo As String

        Public Property SAAdvice() As String Implements ISAAdvice.SAAdvice
            Get
                Return mSAAdvice
            End Get
            Set(ByVal Value As String)
                mSAAdvice = Value
            End Set
        End Property
        Private mSAAdvice As String

        Public Property Referred() As String Implements IReferred.Referred
            Get
                Return mReferred
            End Get
            Set(ByVal Value As String)
                mReferred = Value
            End Set
        End Property
        Private mReferred As String

        Public Property InspectorateAdvice() As String Implements IInspectorateAdvice.InspectorateAdvice
            Get
                Return mInspectorateAdvice
            End Get
            Set(ByVal Value As String)
                mInspectorateAdvice = Value
            End Set
        End Property
        Private mInspectorateAdvice As String

        Public Property Quantity() As String Implements IQuantity.Quantity
            Get
                Return mQuantity
            End Get
            Set(ByVal Value As String)
                mQuantity = Value
            End Set
        End Property
        Private mQuantity As String
    End Class
End Namespace