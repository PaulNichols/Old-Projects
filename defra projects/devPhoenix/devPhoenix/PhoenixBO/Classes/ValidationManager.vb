<Serializable()> _
Public Class ValidationManager
    Inherits ErrorManager
    Public Enum ValidationTitles
        CannotSaveParty
        CannotSavePerson
        CannotSaveSeizureNotification
        CannotSaveImportNotification
        CannotSaveUOM
        CannotSaveImportSpecie
        UnableToUpdateReport
        CannotSavePermit
        CannotSaveApplication
        CannotSaveSpecimenMark
        CannotSaveAdditionalDeclaration
        CannotSaveSpecie
        CannotSaveSpecimen
        CannotSaveCITESNotification
        CannotSaveContact
        CannotSaveMeasurement
        CannotSaveNote
        CannotSaveExportApplication
        CannotSaveImportApplication
        CannotSaveArticle10Application
        TaxonomySearchString
        CannotUpdateReportAuthorisationQ
        CannotUpdateReportPrinter
        CannotUpdateReport
        CannotSaveArticle30Application
        CannotUpdateReferenceData
        CannotSaveCash
        CannotSavePayment
        CannotSavePaymentBasket
        CannotSavePaymentBasketPayment
        CannotSaveCheque
        CannotSaveTaxon
        CannotSaveFeeReduction
        CannotSaveRefund
        CannotSavePostalOrder
        EggValidationFailed

        'do not remove
        UnknownError
    End Enum

    Public Sub New()
    End Sub

    Public Sub New(ByVal errors As Array, ByVal titleId As ValidationTitles)
        MyClass.New(errors, titleId, False)
    End Sub

    'Public Sub New(ByVal errors As ValidationError(), ByVal titleId As ValidationTitles, ByVal ignoreWarnings As Boolean)
    '    MyBase.Init(errors, titleId)
    '    mIgnoreWarnings = ignoreWarnings
    'End Sub

    Public Sub New(ByVal titleId As ValidationTitles)
        MyClass.New(Nothing, titleId, False)
    End Sub

    Public Sub New(ByVal titleId As ValidationTitles, ByVal ignoreWarnings As Boolean)
        MyClass.New(Nothing, titleId, ignoreWarnings)
    End Sub

    Private mIgnoreWarnings As Boolean
    Public Sub New(ByVal errors As Array, ByVal titleId As ValidationTitles, ByVal ignoreWarnings As Boolean)
        'Dim errorList As Array
        'errors.CopyTo(errorList)
        MyBase.Init(errors, titleId)
        mIgnoreWarnings = ignoreWarnings
    End Sub

    Public Sub AddError(ByVal [error] As ValidationError)
        Dim Count As Int32 = 0
        If mIgnoreWarnings AndAlso [error].IsWarning Then
            ' do nothing as this is an error and we have been asked to ignore them
        Else
            If Errors Is Nothing Then
                ReDim Me.Errors(0)
            Else
                ReDim Preserve Me.Errors(Me.Errors.Length)
            End If
            Me.Errors(Me.Errors.Length - 1) = [error]
        End If
    End Sub

End Class
