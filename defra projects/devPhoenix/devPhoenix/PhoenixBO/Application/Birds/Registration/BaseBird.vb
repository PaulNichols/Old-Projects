Namespace Application.Bird.Registration
    Public MustInherit Class BaseBird

        <Serializable()> _
        Public Enum AcquisitionMethodTypes
            Unknown 'DEFAULT
            Barter
            Exchange
            Gift
            Hire
            Loan
            Other
            [Return]
            Sale
        End Enum

        Public MustOverride Property RingsPicked() As Boolean

        'Public Overridable Function BaseAddSpecimen() As AdultSpecimenType
        '    Throw New NotImplementedException
        'End Function

        'Public Overridable Property BaseSpecimens() As AdultSpecimenType()
        '    Get
        '        ' Throw New NotImplementedException
        '    End Get
        '    Set(ByVal Value As AdultSpecimenType())
        '        'Throw New NotImplementedException
        '    End Set
        'End Property

        Public Overridable Function AddPolymorphicSpecimen() As AdultSpecimenType
            Throw New NotImplementedException
        End Function

        Public Sub AddSpecimen(ByVal specimen As AdultSpecimenType)
            Dim len As Int32 = mSpecimens.Length
            ReDim Preserve mSpecimens(len)
            mSpecimens(len) = specimen
        End Sub

        Public Overridable Property Specimens() As AdultSpecimenType()
            Get
                Return mSpecimens
            End Get
            Set(ByVal Value As AdultSpecimenType())
                mSpecimens = Value
            End Set
        End Property
        Protected mSpecimens(-1) As AdultSpecimenType

        Friend Function GetNextId() As Int32
            Dim MaxId As Int32 = 0
            For Each Bird As AdultSpecimenType In mSpecimens
                If Bird.SpecimenType.SpecimenId > MaxId Then
                    MaxId = Bird.SpecimenType.SpecimenId
                End If
            Next Bird
            'whatever the maximum id was, get the next one
            Return MaxId + 1
        End Function

        Public Overridable Function RemoveSpecimen(ByVal specimenId As Int32) As Boolean 'MLD 19/12/4 refactored
            For Index As Int32 = 0 To mSpecimens.Length - 1
                If mSpecimens(Index).SpecimenType.SpecimenId = specimenId Then
                    'remove the item from the array
                    Dim Length As Int32 = mSpecimens.Length
                    If Index < Length - 1 Then
                        Array.Copy(mSpecimens, Index + 1, mSpecimens, Index, Length - Index - 1)  'overlapped copy
                    End If
                    ReDim Preserve mSpecimens(Length - 2)
                    Return True
                End If
            Next Index
            Return False
        End Function

        Friend Overridable Function IsValid(ByVal addError As BirdRegistration.AddValidationErrorDelegate, ByVal soUserId As Int64, ByVal applicationId As Int32) As Boolean
            Dim ValidState As Boolean = True
            If mSpecimens.Length > 0 Then
                For Each spec As AdultSpecimenType In mSpecimens
                    If spec.Rings.Length > 0 Then
                        For Each ring As idMark In spec.Rings
                            If ring.ShouldFateReasonBePopulated AndAlso _
                               Not ring.IsFateReasonPopulated Then
                                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                                ReplaceVals.Add("%1", ring.Mark)
                                addError(ValidationError.ValidationCodes.IfFatedOtherNeedReasonForRing, ReplaceVals)
                                ValidState = False
                            End If
                        Next ring
                    End If
                    If spec.IDMarks.Length > 0 Then
                        For Each idMark As idMark In spec.IDMarks
                            If idMark.ShouldFateReasonBePopulated AndAlso _
                               Not idMark.IsFateReasonPopulated Then
                                Dim ReplaceVals As New Collections.Specialized.NameValueCollection
                                ReplaceVals.Add("%1", idMark.Mark)
                                addError(ValidationError.ValidationCodes.IfFatedOtherNeedReasonForIdMark, ReplaceVals)
                                ValidState = False
                            End If
                        Next idMark
                    End If
                Next spec
            End If
            Return ValidState
        End Function
    End Class
End Namespace