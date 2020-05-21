Namespace Taxonomy
    <Serializable()> _
    Public Class BOAnimalDelegationAuthorityDisplay
        Implements IAnimalDelegationAuthorityDisplay

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

#End Region

#Region " Properties "
        Public Property AnimalDelegationAuthorityID As Int32 Implements IAnimalDelegationAuthorityDisplay.AnimalDelegationAuthorityId
            Get
                Return mAnimalDelegationAuthorityID
            End Get
            Set
                mAnimalDelegationAuthorityID = Value
            End Set
        End Property
        Private mAnimalDelegationAuthorityID As Int32

        Public Property DelegationCode() As String Implements IAnimalDelegationAuthorityDisplay.DelegationCode
            Get
                Return mDelegationCode
            End Get
            Set(ByVal Value As String)
                mDelegationCode = Value
            End Set
        End Property
        Private mDelegationCode As String

        Public Property DelegationSubject() As String Implements IAnimalDelegationAuthorityDisplay.DelegationSubject
            Get
                Return mDelegationSubject
            End Get
            Set(ByVal Value As String)
                mDelegationSubject = Value
            End Set
        End Property
        Private mDelegationSubject As String

        Public Property ID() As Int32 Implements IAnimalDelegationAuthorityDisplay.ID
            Get
                Return mID
            End Get
            Set(ByVal Value As Int32)
                mID = Value
            End Set
        End Property
        Private mID As Int32

        Public Property RTARoadmap() As String Implements IAnimalDelegationAuthorityDisplay.RTARoadmap
            Get
                Return mRTARoadmap
            End Get
            Set(ByVal Value As String)
                mRTARoadmap = Value
            End Set
        End Property
        Private mRTARoadmap As String

        Public Property ApplicationTypeDescription() As String Implements IAnimalDelegationAuthorityDisplay.ApplicationTypeDescription
            Get
                Return mApplicationTypeDescription
            End Get
            Set(ByVal Value As String)
                mApplicationTypeDescription = Value
            End Set
        End Property
        Private mApplicationTypeDescription As String

#End Region

    End Class
End Namespace