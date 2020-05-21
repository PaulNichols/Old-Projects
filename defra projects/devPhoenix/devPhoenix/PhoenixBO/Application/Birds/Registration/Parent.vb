Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class Parent
        Inherits SpecimenType

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal gender As Application.GenderType)
            MyBase.New(gender)
        End Sub

        Public Property OwnedByKeeper() As Boolean  'MLD added 10/1/5
            Get
                Return mOwnedByKeeper
            End Get
            Set(ByVal Value As Boolean)
                mOwnedByKeeper = Value
            End Set
        End Property
        Private mOwnedByKeeper As Boolean = False

        Public Function AddMarks() As IDMark    'MLD modified 10/1/5
            Dim len As Int32 = mIdMarks.Length
            ReDim Preserve mIdMarks(len)
            mIdMarks(len) = New IDMark
            Return mIdMarks(len)
        End Function

        Public Property IdMarks() As IDMark()
            Get
                Return mIdMarks
            End Get
            Set(ByVal Value As IDMark())
                mIdMarks = Value
            End Set
        End Property
        Private mIdMarks(-1) As IDMark  'MLD modified 10/1/5 so that starts off empty, not Nothing

        Public Function GetMostPermanentMark() As String    'MLD added 10/1/5
            Dim bestMark As IDMark
            Dim permanence As String = "Z1" 'higher than Z
            Dim description As String
            
            For Each mark As IDMark in mIdMarks
                For Each type As Entity.IDMarkType in MarkTypes
                    If type.IDMarkTypeID = mark.MarkType Then
                        If type.Permanence < permanence Then
                            permanence = type.Permanence
                            description = type.Description
                            bestMark = mark
                        End If
                    End If
                Next
            Next
            If bestMark Is Nothing Then
                Return ""
            End If
            Return description + ": " + bestMark.Mark
        End Function

        Private Shared Readonly Property MarkTypes As EntitySet.IDMarkTypeSet  'MLD added 10/1/5
            Get
                If mMarkTypes Is Nothing Then
                    mMarkTypes = Entity.IDMarkType.GetAll()
                End If
                Return mMarkTypes
            End Get
        End Property
        Private Shared mMarkTypes As EntitySet.IDMarkTypeSet

    End Class
End Namespace