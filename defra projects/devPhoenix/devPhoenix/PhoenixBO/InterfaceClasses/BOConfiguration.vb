Public Class BOConfiguration
    Inherits BaseBO
    '    Implements IBOConfiguration

    Private mDatabaseValues As Hashtable = Nothing

    Public Sub New()
        MyBase.New()
        '       InitialiseConfiguration()
    End Sub

    Public Function GetValue(ByVal propertyName As String) As Object
        Return GetValue(propertyName, Nothing)
    End Function

    Friend Function GetValue(ByVal propertyName As String, ByVal hash As Hashtable) As Object
        Dim Result As Object = System.Configuration.ConfigurationSettings.AppSettings(propertyName)
        If Result Is Nothing Then
            'have we been to the database?  If we haven't, we may as well retrieve all of the values
            'whilst we are there
            If hash Is Nothing Then
                hash = New Hashtable
                Dim Params As DataObjects.EntitySet.PhoenixParameterSet = DataObjects.Entity.PhoenixParameter.GetAll(False)
                If Not Params Is Nothing AndAlso _
                    Params.Count > 0 Then
                    For Each Param As DataObjects.Entity.PhoenixParameter In Params
                        'only add active params
                        If Param.Active Then
                            hash.Add(Param.ParamName, Param.ParamValue)
                        End If
                    Next Param
                End If
            End If
            'by this point we should either just loaded some params or have access to 
            'a hashtable of them.  Just to be sure though...
            If Not hash Is Nothing AndAlso _
                hash.Count > 0 AndAlso _
                hash.ContainsKey(propertyName) Then
                Result = hash.Item(propertyName)
            End If
        End If
        Return Result
    End Function

    Public Function IsInt32(ByVal value As Object) As Boolean
        Try
            Dim Result As Int32 = CType(value, Int32)
            Return True
        Catch ex As FormatException
            Return False
        End Try
    End Function
    'Private Sub InitialiseConfiguration()
    '    Dim Config As New DataObjects.Entity.Configuration(1)
    '    With Config
    '        CITESSpecimenImportCountry = New BO.ReferenceData.BOCountry(.CITESSpecimenImportCountry)
    '        DefaultCountry = New BO.ReferenceData.BOCountry(.DefaultCountry)
    '        LiveDerivative = New BO.ReferenceData.BOCITESDerivative(.LiveDerivativeCode)
    '        mCostCode = .CostCode

    '        Dim Codes As String = .ExportAppendixISpecimenSource
    '        If Not Codes Is Nothing AndAlso _
    '           Codes.Length > 0 Then
    '            mExportAppendixISpecimenSource = Codes.Split(","c)
    '        End If
    '        Codes = .ExportAppendixISpecimenPurpose
    '        If Not Codes Is Nothing AndAlso _
    '           Codes.Length > 0 Then
    '            mExportAppendixISpecimenPurpose = Codes.Split(","c)
    '        End If
    '        Codes = .ImportSpecieSourceCountry
    '        If Not Codes Is Nothing AndAlso _
    '           Codes.Length > 0 Then
    '            mImportSpecieSourceCountry = Codes.Split(","c)
    '        End If
    '        Codes = .AuthorisedLocation
    '        If Not Codes Is Nothing AndAlso _
    '           Codes.Length > 0 Then
    '            mAuthorisedLocation = Codes.Split(","c)
    '        End If
    '    End With
    'End Sub

    'Public Property LiveDerivative() As ReferenceData.BOCITESDerivative Implements IBOConfiguration.LiveDerivative
    '    Get
    '        Return mLiveDerivative
    '    End Get
    '    Set(ByVal Value As ReferenceData.BOCITESDerivative)
    '        mLiveDerivative = Value
    '    End Set
    'End Property
    'Private mLiveDerivative As ReferenceData.BOCITESDerivative

    'Public Property CITESSpecimenImportCountry() As ReferenceData.BOCountry Implements IBOConfiguration.CITESSpecimenImportCountry
    '    Get
    '        Return mCITESSpecimenImportCountry
    '    End Get
    '    Set(ByVal Value As ReferenceData.BOCountry)
    '        mCITESSpecimenImportCountry = Value
    '    End Set
    'End Property
    'Private mCITESSpecimenImportCountry As ReferenceData.BOCountry

    'Public Property DefaultCountry() As ReferenceData.BOCountry Implements IBOConfiguration.DefaultCountry
    '    Get
    '        Return mDefaultCountry
    '    End Get
    '    Set(ByVal Value As ReferenceData.BOCountry)
    '        mDefaultCountry = Value
    '    End Set
    'End Property
    'Private mDefaultCountry As ReferenceData.BOCountry

    'Public Property UnMatchedSeizureNotificationDays() As Int32 Implements IBOConfiguration.UnMatchedSeizureNotificationDays
    '    Get
    '        Return mUnMatchedSeizureNotificationDays
    '    End Get
    '    Set(ByVal Value As Int32)
    '        mUnMatchedSeizureNotificationDays = Value
    '    End Set
    'End Property
    'Private mUnMatchedSeizureNotificationDays As Int32

    'Public Property PossibleDuplicatesNumberOfDays() As Int32 Implements IBOConfiguration.PossibleDuplicatesNumberOfDays
    '    Get
    '        Return mPossibleDuplicatesNumberOfDays
    '    End Get
    '    Set(ByVal Value As Int32)
    '        mPossibleDuplicatesNumberOfDays = Value
    '    End Set
    'End Property
    'Private mPossibleDuplicatesNumberOfDays As Int32

    'Public Property ExportAppendixISpecimenSource() As String() Implements IBOConfiguration.ExportAppendixISpecimenSource
    '    Get
    '        Return mExportAppendixISpecimenSource
    '    End Get
    '    Set(ByVal Value As String())
    '        mExportAppendixISpecimenSource = Value
    '    End Set
    'End Property
    'Private mExportAppendixISpecimenSource As String()

    'Public Property ExportAppendixISpecimenPurpose() As String() Implements IBOConfiguration.ExportAppendixISpecimenPurpose
    '    Get
    '        Return mExportAppendixISpecimenPurpose
    '    End Get
    '    Set(ByVal Value As String())
    '        mExportAppendixISpecimenPurpose = Value
    '    End Set
    'End Property
    'Private mExportAppendixISpecimenPurpose As String()

    'Public Property ImportSpecieSourceCountry() As String() Implements IBOConfiguration.ImportSpecieSourceCountry
    '    Get
    '        Return mImportSpecieSourceCountry
    '    End Get
    '    Set(ByVal Value As String())
    '        mImportSpecieSourceCountry = Value
    '    End Set
    'End Property
    'Private mImportSpecieSourceCountry As String()

    'Public Property AuthorisedLocation() As String() Implements IBOConfiguration.AuthorisedLocation
    '    Get
    '        Return mAuthorisedLocation
    '    End Get
    '    Set(ByVal Value As String())
    '        mAuthorisedLocation = Value
    '    End Set
    'End Property
    'Private mAuthorisedLocation As String()

    'Public Property CostCode() As String Implements IBOConfiguration.CostCode   'MLD 19/10/4
    '    Get
    '        Return mCostCode
    '    End Get
    '    Set(ByVal Value As String)
    '        mCostCode = Value
    '    End Set
    'End Property
    'Private mCostCode As String

    Friend Function IsInImportSpecieSourceCountry(ByVal code As String) As Boolean
        Dim ImportSpecieSourceCountry As Object = GetValue("ImportSpecieSourceCountry")
        If Not ImportSpecieSourceCountry Is Nothing AndAlso _
           TypeOf ImportSpecieSourceCountry Is String AndAlso _
           ImportSpecieSourceCountry.ToString.Length > 0 Then
            For Each _code As String In ImportSpecieSourceCountry.ToString
                If String.Compare(_code, code, True) = 0 Then
                    Return True
                End If
            Next _code
        End If
        Return False
    End Function

    Friend Function IsInExportAppendixISpecimenSource(ByVal sourceCode As String) As Boolean
        Dim ExportAppendixISpecimenSource As Object = GetValue("ExportAppendixISpecimenSource")
        If Not ExportAppendixISpecimenSource Is Nothing AndAlso _
           TypeOf ExportAppendixISpecimenSource Is String AndAlso _
           ExportAppendixISpecimenSource.ToString.Length > 0 Then
            For Each _code As String In ExportAppendixISpecimenSource.ToString
                If String.Compare(_code, sourceCode, True) = 0 Then
                    Return True
                End If
            Next _code
        End If
        Return False
    End Function

    Friend Function IsInExportAppendixISpecimenPurpose(ByVal purposeCode As String) As Boolean
        Dim ExportAppendixISpecimenPurpose As Object = GetValue("ExportAppendixISpecimenPurpose")
        If Not ExportAppendixISpecimenPurpose Is Nothing AndAlso _
           TypeOf ExportAppendixISpecimenPurpose Is String AndAlso _
           ExportAppendixISpecimenPurpose.ToString.Length > 0 Then
            For Each _code As String In ExportAppendixISpecimenPurpose.ToString
                If String.Compare(_code, purposeCode, True) = 0 Then
                    Return True
                End If
            Next _code
        End If
        Return False
    End Function

    Friend Function IsInAuthorisedLocation(ByVal sourceCode As String) As Boolean
        Dim AuthorisedLocation As Object = GetValue("AuthorisedLocation")
        If Not AuthorisedLocation Is Nothing AndAlso _
           TypeOf AuthorisedLocation Is String AndAlso _
           AuthorisedLocation.ToString.Length > 0 Then
            For Each _code As String In AuthorisedLocation.ToString
                If String.Compare(_code, sourceCode, True) = 0 Then
                    Return True
                End If
            Next _code
        End If
        Return False
    End Function

    Friend Function IsLiveDerivative(ByVal derivative As ReferenceData.BOCITESDerivative) As Boolean
        Try
            Dim LiveDerivativeCode As Object = GetValue("LiveDerivativeCode")
            If Not LiveDerivativeCode Is Nothing AndAlso _
               Int32.Parse(LiveDerivativeCode.toString) > 0 AndAlso _
               LiveDerivativeCode.toString.Length > 0 Then
                Dim LiveDerivative As New BO.ReferenceData.BOCITESDerivative(CType(LiveDerivativeCode, Int32))
                Return (Not LiveDerivative Is Nothing AndAlso _
                        Not derivative Is Nothing AndAlso _
                        LiveDerivative.ID = derivative.ID)
            Else
                Return False
            End If
        Catch ex As FormatException
            'caused by parse
            Return False
        End Try
    End Function

    Friend Function IsDefaultArticle30CertificateTypeId(ByVal Article30CertificateTypeId As Int32) As Boolean
        Try
            Dim DefaultArticle30CertificateTypeId As Object = GetValue("DefaultArticle30CertificateTypeId")
            If Not DefaultArticle30CertificateTypeId Is Nothing AndAlso _
               Int32.Parse(DefaultArticle30CertificateTypeId.ToString) > 0 AndAlso _
               DefaultArticle30CertificateTypeId.ToString.Length > 0 Then
                Dim DefaultArticle30CertificateType As New BO.ReferenceData.BOArticle10CertificateType(CType(DefaultArticle30CertificateTypeId, Int32))
                Return (Not DefaultArticle30CertificateTypeId Is Nothing AndAlso _
                        DefaultArticle30CertificateType.ID = Article30CertificateTypeId)
            Else
                Return False
            End If
        Catch ex As FormatException
            Return False
        End Try
    End Function
End Class
