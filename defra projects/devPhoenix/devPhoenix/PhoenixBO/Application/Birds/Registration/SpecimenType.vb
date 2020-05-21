Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class SpecimenType
        Public Sub New()
        End Sub

        Public Sub New(ByVal gender As Application.GenderType)
            mGender = gender
        End Sub

        Public Property SpecimenId() As Int32
            Get
                Return mSpecimenId
            End Get
            Set(ByVal Value As Int32)
                mSpecimenId = Value
            End Set
        End Property
        Private mSpecimenId As Int32

        Public Property IsAmended() As Boolean
            Get
                Return mIsAmended
            End Get
            Set(ByVal Value As Boolean)
                mIsAmended = Value
            End Set
        End Property
        Private mIsAmended As Boolean

        Public Property CommonName() As String
            Get
                If mCommonName Is Nothing Then
                    mCommonName = String.Empty
                End If
                Return mCommonName
            End Get
            Set(ByVal Value As String)
                mCommonName = Value
            End Set
        End Property
        Private mCommonName As String

        Public Property ScientificName() As String
            Get
                If mScientificName Is Nothing Then
                    mScientificName = String.Empty
                End If
                Return mScientificName
            End Get
            Set(ByVal Value As String)
                mScientificName = Value
            End Set
        End Property
        Private mScientificName As String

        Public Property ECAnnex() As String
            Get
                Return mECAnnex
            End Get
            Set(ByVal Value As String)
                mECAnnex = Value
            End Set
        End Property
        <Microsoft.VisualBasic.VBFixedStringAttribute(1)> _
        Private mECAnnex As String

        Public ReadOnly Property IsAnnexA() As Boolean
            Get
                Return (String.Compare(ECAnnex, "A", True) = 0)
            End Get
        End Property

        Public Property RegistrationDocumentReference() As String
            Get
                Return mRegistrationDocumentReference
            End Get
            Set(ByVal Value As String)
                mRegistrationDocumentReference = Value
            End Set
        End Property
        Private mRegistrationDocumentReference As String

        Public Property Article10Reference() As String
            Get
                Return mArticle10Reference
            End Get
            Set(ByVal Value As String)
                mArticle10Reference = Value
            End Set
        End Property
        Private mArticle10Reference As String

        'Public Property Fate() As String
        '    Get
        '        Return mFate
        '    End Get
        '    Set(ByVal Value As String)
        '        mFate = Value
        '    End Set
        'End Property
        'Private mFate As String

        'an object because it can be null - usually an int32
        Public Property FateCode() As Object
            Get
                Return mFateCode
            End Get
            Set(ByVal Value As Object)
                mFateCode = Value
            End Set
        End Property
        Private mFateCode As Object

        Public Property FateCode_Helper() As String
            Get
                Dim Result As String = String.Empty
                If Not mFateCode Is Nothing AndAlso TypeOf mFateCode Is Int32 AndAlso CType(mFateCode, Int32) > 0 Then
                    Dim SpecFate As DataObjects.Entity.SpecimenFate = DataObjects.Entity.SpecimenFate.GetById(CType(mFateCode, Int32))
                    If Not SpecFate Is Nothing Then
                        Result = SpecFate.Description.Trim()
                    End If
                End If
                Return Result
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property Gender() As Application.GenderType
            Get
                Return mGender
            End Get
            Set(ByVal Value As Application.GenderType)
                mGender = Value 'Need this line as value is being reset when array passed through webservice
                'added by TS 14/12/04
                ' retained for serialization.  Gender is read only.
            End Set
        End Property
        Private mGender As Application.GenderType

        Friend Sub SetGender(ByVal gender As Char)
            If gender = System.Char.Parse("F") Then
                mGender = Application.GenderType.Female
            ElseIf gender = System.Char.Parse("M") Then
                mGender = Application.GenderType.Male
            Else
                mGender = Application.GenderType.Unknown
            End If
        End Sub

        Friend Sub SetGender(ByVal gender As String)
            If gender.Length > 1 Then
                gender = gender.Substring(0, 1)
            End If
            If String.Compare(gender, "f", True) = 0 Then
                mGender = Application.GenderType.Female
            ElseIf String.Compare(gender, "m", True) = 0 Then
                mGender = Application.GenderType.Male
            Else
                mGender = Application.GenderType.Unknown
            End If
        End Sub

        Friend Function GetGender() As Char
            Return mGender.ToString().Chars(0)  'MLD 7/1/5 changed so that Unknown returns "U" instead of ""
        End Function

        Friend Sub SetGender(ByVal gender As Application.GenderType)
            mGender = gender
        End Sub

        Friend Function CompareGender(ByVal databaseId As Int32) As Boolean
            Return CompareGender(databaseId, Gender)
        End Function

        Friend Shared Function CompareGender(ByVal databaseId As Int32, ByVal gender As Application.GenderType) As Boolean
            Return (CType(gender, Int32) = databaseId)
        End Function

#Region " Age "
        Public Property HatchDate() As Object
            Get
                Return mHatchDate
            End Get
            Set(ByVal Value As Object)
                If mAgeStatusId > 0 AndAlso Not Value Is Nothing Then
                    ' only agestatus or hatchdate can be set.  If the other gets set
                    ' and this properties new value is not null then clear the other
                    ' property
                    mAgeStatusId = 0
                End If
                mHatchDate = Value
            End Set
        End Property
        Private mHatchDate As Object

        Public Property IsHatchDateExact() As Object
            Get
                Return mIsHatchDateExact
            End Get
            Set(ByVal Value As Object)
                mIsHatchDateExact = Value
            End Set
        End Property
        Private mIsHatchDateExact As Object

        Public Property AgeStatusId() As Int32
            Get
                Return mAgeStatusId
            End Get
            Set(ByVal Value As Int32)
                If Not mHatchDate Is Nothing AndAlso Value > 0 Then
                    ' only agestatus or hatchdate can be set.  If the other gets set
                    ' and this properties new value is not null then clear the other
                    ' property
                    mHatchDate = Nothing
                    mIsHatchDateExact = Nothing
                End If
                mAgeStatusId = Value
            End Set
        End Property
        Private mAgeStatusId As Int32

        Friend ReadOnly Property AgeStatus_Helper() As String
            Get
                Dim Result As String = String.Empty
                If mAgeStatusId > 0 Then
                    Dim AgeStatusDO As DataObjects.Entity.SpecimenAgeStatus = DataObjects.Entity.SpecimenAgeStatus.GetById(mAgeStatusId)
                    If Not AgeStatusDO Is Nothing Then
                        Result = AgeStatusDO.Description.Trim()
                    End If
                End If
                Return Result
            End Get
        End Property

        'Public Property AgeStatusCode() As Object
        '    Get
        '        Return mAgeStatusCode
        '    End Get
        '    Set(ByVal Value As Object)
        '        mAgeStatusCode = Value
        '    End Set
        'End Property
        'Private mAgeStatusCode As Object

#End Region

        Private Const SPECIMEN_ID As String = "SpecimenID"
        Private Const IS_AMENDED As String = "IsAmended"
        Private Const COMMON_NAME As String = "CommonName"
        Private Const SCIENTIFIC_NAME As String = "ScientificName"
        Private Const HATCH_DATE As String = "HatchDate"
        Private Const IS_HATCH_DATE_EXACT As String = "IsHatchDateExact"
        Private Const AGE_STATUS As String = "AgeStatusId"
        Private Const SPECIMEN_GENDER As String = "Gender"
        Private Const REGISTRATION_DOC_REF As String = "RegistrationDocumentReference"
        Private Const ARTICLE_10_REF As String = "Article10Reference"
        Private Const FATE_CODE As String = "FateCode"
        Private Const EC_ANNEX As String = "ECAnnex"
        Friend Shared Sub CreateSpecimen(ByRef specimen As AdultSpecimenType, ByVal specimenRows As DataRow()) ', ByVal applicationId As Int32)
            'check to see if one is created
            If Not specimen Is Nothing Then
                'sort out the specimens
                Dim NewSpecType As New SpecimenType
                'If BirdRegistration.IsSubmitted(applicationId) Then
                '    'get it from the database

                'Else
                For Each SpecimenRow As DataRow In specimenRows
                    CreateSpecimen(NewSpecType, SpecimenRow)
                    'should only be one row, so bail after
                    Exit For
                Next SpecimenRow
                'End If

                specimen.SpecimenType = NewSpecType
            End If
        End Sub

        Friend Shared Sub CreateSpecimen(ByRef specimen As SpecimenType, ByVal specimenRow As DataRow)
            With specimenRow
                If Not .IsNull(SPECIMEN_ID) AndAlso CType(.Item(SPECIMEN_ID), Int32) > 0 Then specimen.SpecimenId = CType(.Item(SPECIMEN_ID), Int32)
                specimen.IsAmended = CType(.Item(IS_AMENDED), Boolean)
                specimen.CommonName = .Item(COMMON_NAME).ToString
                specimen.ScientificName = .Item(SCIENTIFIC_NAME).ToString
                If Not .IsNull(HATCH_DATE) Then specimen.HatchDate = CType(.Item(HATCH_DATE), Date)
                If Not .IsNull(IS_HATCH_DATE_EXACT) Then specimen.IsHatchDateExact = .Item(IS_HATCH_DATE_EXACT)
                If Not .IsNull(AGE_STATUS) Then specimen.AgeStatusId = CType(.Item(AGE_STATUS), Int32)
                specimen.SetGender(CType(.Item(SPECIMEN_GENDER), Char))
                If Not .IsNull(REGISTRATION_DOC_REF) Then specimen.RegistrationDocumentReference = .Item(REGISTRATION_DOC_REF).ToString
                If Not .IsNull(ARTICLE_10_REF) Then specimen.Article10Reference = .Item(ARTICLE_10_REF).ToString
                If Not .IsNull(FATE_CODE) Then specimen.FateCode = .Item(FATE_CODE)
                If Not .IsNull(EC_ANNEX) Then specimen.ECAnnex = .Item(EC_ANNEX).ToString
            End With
        End Sub

        Friend Sub UpdateSpecimen(ByVal newSpecRow As DataRow)
            With newSpecRow
                If SpecimenId > 0 Then .Item(SPECIMEN_ID) = SpecimenId Else .Item(SPECIMEN_ID) = Convert.DBNull
                .Item(IS_AMENDED) = IsAmended
                .Item(COMMON_NAME) = CommonName
                .Item(SCIENTIFIC_NAME) = ScientificName
                If Not HatchDate Is Nothing AndAlso TypeOf HatchDate Is Date Then .Item(HATCH_DATE) = CType(HatchDate, Date) Else .Item(HATCH_DATE) = Convert.DBNull
                If Not IsHatchDateExact Is Nothing AndAlso TypeOf IsHatchDateExact Is Boolean Then .Item(IS_HATCH_DATE_EXACT) = CType(IsHatchDateExact, Boolean) Else .Item(IS_HATCH_DATE_EXACT) = Convert.DBNull
                If AgeStatusId > 0 Then .Item(AGE_STATUS) = AgeStatusId Else .Item(AGE_STATUS) = Convert.DBNull
                .Item(SPECIMEN_GENDER) = GetGender()
                If Not RegistrationDocumentReference Is Nothing AndAlso TypeOf RegistrationDocumentReference Is String Then .Item(REGISTRATION_DOC_REF) = RegistrationDocumentReference.ToString Else .Item(REGISTRATION_DOC_REF) = Convert.DBNull
                If Not Article10Reference Is Nothing AndAlso TypeOf Article10Reference Is String Then .Item(ARTICLE_10_REF) = Article10Reference.ToString Else .Item(ARTICLE_10_REF) = Convert.DBNull
                If Not FateCode Is Nothing AndAlso TypeOf FateCode Is Int32 Then .Item(FATE_CODE) = CType(FateCode, Int32) Else .Item(FATE_CODE) = Convert.DBNull
                If Not ECAnnex Is Nothing AndAlso TypeOf ECAnnex Is String Then .Item(EC_ANNEX) = ECAnnex.ToString Else .Item(EC_ANNEX) = Convert.DBNull
            End With
        End Sub
    End Class
End Namespace
