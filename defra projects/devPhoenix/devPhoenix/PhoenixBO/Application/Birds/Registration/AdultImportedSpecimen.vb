Namespace Application.Bird.Registration
    <Serializable()> _
    Public Class AdultImportedSpecimen
        Inherits AdultSpecimenType
        Implements IDateAcquired

        <Serializable()> _
        Public Enum CaptureMethods
            Disabled
            Taken_under_license
        End Enum

        Public Sub New()
            'GetPermit()
        End Sub

        'Private Function GetPermit() As BirdRegistrationDataset.PermitRow
        '    'only adds if one doesn't exist
        '    'Dim pr As BirdRegistrationDataset.PermitRow
        '    'If birdDS().Permit Is Nothing OrElse birdDS().Permit.Count = 0 Then
        '    '    pr = birdDS().Permit.NewPermitRow()
        '    '    pr.Permit_text = String.Empty
        '    '    birdDS().Permit.AddPermitRow(pr)
        '    'Else
        '    '    pr = CType(birdDS().Permit(0), BirdRegistrationDataset.PermitRow)
        '    'End If
        '    'Return pr
        'End Function

        'Private Function GetRing(ByVal index As Int32) As BirdRegistrationDataset.FoundRingRow
        '    'only adds if one doesn't exist
        '    Dim fr As BirdRegistrationDataset.FoundRingRow
        '    Try
        '        fr = CType(birdDS.FoundRing(index), BirdRegistrationDataset.FoundRingRow)
        '    Catch
        '        fr = Nothing
        '    End Try
        '    Return fr
        'End Function

        'Private Sub GetRings()
        '    'If mRing Is Nothing Then
        '    '    For Each fr As BirdRegistrationDataset.FoundRingRow In birdDS().FoundRing.Rows
        '    '        With IDMark.AddIDMark(mRing, fr)
        '    '            .Mark = String.Empty
        '    '            .MarkType = 0
        '    '        End With
        '    '    Next fr
        '    'End If
        'End Sub

        'Private Sub GetIDMarks()
        '    'If mIDMarks Is Nothing Then
        '    '    For Each fr As BirdRegistrationDataset.FoundBirdIDMarkRow In birdDS().FoundBirdIDMark.Rows
        '    '        IDMark.AddIDMark(mIDMarks, fr)
        '    '    Next fr
        '    'End If
        'End Sub

        Public Property Permit() As Object
            Get
                Return mPermit
            End Get
            Set(ByVal Value As Object)
                mPermit = Value
            End Set
        End Property
        Private mPermit As Object

#Region " Source "
        Public Property IsFromUK() As Object
            Get
                Return mIsFromUK
            End Get
            Set(ByVal Value As Object)
                mIsFromUK = Value
            End Set
        End Property
        Private mIsFromUK As Object

        Public Property Origin() As Int32
            Get
                Return mOrigin
            End Get
            Set(ByVal Value As Int32)
                mOrigin = Value
            End Set
        End Property
        Private mOrigin As Int32

        Public Property Origin2() As Int32
            Get
                Return mOrigin2
            End Get
            Set(ByVal Value As Int32)
                mOrigin2 = Value
            End Set
        End Property
        Private mOrigin2 As Int32

        Public Property Origin_Helper() As String
            Get
                Return GetOrigin(mOrigin)
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property Origin2_Helper() As String
            Get
                Return GetOrigin(mOrigin2)
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Private Function GetOrigin(ByVal originId As Int32) As String
            Dim Result As String = String.Empty
            If originId > 0 Then
                Dim Data As DataObjects.Entity.CITESSource
                Try
                    'this will fail if the id doesn't exist in the db
                    Data = New DataObjects.Entity.CITESSource(originId)
                    If Not Data Is Nothing Then Result = Data.Description
                Catch
                    'no point in doing anything with the error as the default empty string will return the
                    'desired result!
                End Try
            End If

            Return Result
        End Function

        Public Property CaptureMethod() As Object
            Get
                Return mCaptureMethod
            End Get
            Set(ByVal Value As Object)
                mCaptureMethod = Value
            End Set
        End Property
        Private mCaptureMethod As Object

        Public Property CaptureMethod_Helper() As String
            Get
                If Not mCaptureMethod Is Nothing AndAlso mCaptureMethod.ToString.Length > 0 Then
                    Return System.Enum.GetName(GetType(CaptureMethods), mCaptureMethod).ToString.Replace("_", " ")
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property

        Public Property EULicenseNumber() As Object
            Get
                Return mEULicenseNumber
            End Get
            Set(ByVal Value As Object)
                mEULicenseNumber = Value
            End Set
        End Property
        Private mEULicenseNumber As Object

        Public Property SpecialConditions() As Object
            Get
                Return mSpecialConditions
            End Get
            Set(ByVal Value As Object)
                mSpecialConditions = Value
            End Set
        End Property
        Private mSpecialConditions As Object

        Public Property CountryOfOrigin() As Int32
            Get
                Return mCountryOfOrigin
            End Get
            Set(ByVal Value As Int32)
                mCountryOfOrigin = Value
            End Set
        End Property
        Private mCountryOfOrigin As Int32

        Public Property CountryOfOrigin_Helper() As String
            Get
                Dim Result As String = String.Empty
                If mCountryOfOrigin > 0 Then
                    Dim Data As DataObjects.Entity.Country
                    Try
                        'this will fail if the id doesn't exist in the db
                        Data = New DataObjects.Entity.Country(mCountryOfOrigin)
                        If Not Data Is Nothing Then Result = Data.ShortName
                    Catch
                        'no point in doing anything with the error as the default empty string will return the
                        'desired result!
                    End Try
                End If

                Return Result
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property
#End Region

#Region " Quarantine "
        Public Property KeeperName() As String
            Get
                If mKeeperName Is Nothing Then
                    mKeeperName = String.Empty
                End If
                Return mKeeperName
            End Get
            Set(ByVal Value As String)
                mKeeperName = Value
            End Set
        End Property
        Private mKeeperName As String

        Public Property Address() As String
            Get
                If mAddress Is Nothing Then
                    mAddress = String.Empty
                End If
                Return mAddress
            End Get
            Set(ByVal Value As String)
                mAddress = Value
            End Set
        End Property
        Private mAddress As String

        Public Property EndDate() As Date
            Get
                Return mEndDate
            End Get
            Set(ByVal Value As Date)
                mEndDate = Value
            End Set
        End Property
        Private mEndDate As Date
#End Region

        Public Property DateAcquired() As Date Implements IDateAcquired.DateAcquired
            Get
                If mDateAcquired.Ticks = 0 Then mDateAcquired = Date.Now
                Return mDateAcquired
            End Get
            Set(ByVal Value As Date)
                mDateAcquired = Value
            End Set
        End Property
        Private mDateAcquired As Date

        Public Property DateTaken() As Date
            Get
                Return mDateTaken
            End Get
            Set(ByVal Value As Date)
                mDateTaken = Value
            End Set
        End Property
        Private mDateTaken As Date

        Public Property IsWithinEU() As Boolean
            Get
                Return mIsWithinEU
            End Get
            Set(ByVal Value As Boolean)
                mIsWithinEU = Value
            End Set
        End Property
        Private mIsWithinEU As Boolean

        Public Property KeptAddressId() As Object
            Get
                Return mKeptAddressId
            End Get
            Set(ByVal Value As Object)
                mKeptAddressId = Value
            End Set
        End Property
        Private mKeptAddressId As Object

        Public Property KeptAddress_Helper() As String
            Get
                Return AdultFoundSpecimen.KeptAddress(mKeptAddressId)
            End Get
            Set(ByVal Value As String)
                'for proxy
            End Set
        End Property

        Public Property AcquisitionDetails() As String
            Get
                Return mAcquisitionDetails
            End Get
            Set(ByVal Value As String)
                mAcquisitionDetails = Value
            End Set
        End Property
        Private mAcquisitionDetails As String

        'optional
        Public Property PreviousKeeper() As PreviousKeeperAddress
            Get
                Return mPreviousKeeper
            End Get
            Set(ByVal Value As PreviousKeeperAddress)
                mPreviousKeeper = Value
            End Set
        End Property
        Private mPreviousKeeper As PreviousKeeperAddress

        Public Property Statements() As Statements
            Get
                Return mStatements
            End Get
            Set(ByVal Value As Statements)
                mStatements = Value
            End Set
        End Property
        Private mStatements As Statements = New Statements

#Region " FR 2908 "
        Public Property PurposeOfImport() As Int32
            Get
                Return mPurposeOfImport
            End Get
            Set(ByVal Value As Int32)
                mPurposeOfImport = Value
            End Set
        End Property
        Private mPurposeOfImport As Int32

        Public Property PurposeOfImportExplanation() As String
            Get
                Return mPurposeOfImportExplanation
            End Get
            Set(ByVal Value As String)
                mPurposeOfImportExplanation = Value
            End Set
        End Property
        Private mPurposeOfImportExplanation As String

        Public Property PurposeOfImport_Helper() As String
            Get
                Dim Result As String = String.Empty
                If mPurposeOfImport > 0 Then
                    Dim Data As DataObjects.Entity.CITESPurpose
                    Try
                        'this will fail if the id doesn't exist in the db
                        Data = New DataObjects.Entity.CITESPurpose(mPurposeOfImport)
                        If Not Data Is Nothing Then Result = Data.Description
                    Catch
                        'no point in doing anything with the error as the default empty string will return the
                        'desired result!
                    End Try
                End If

                Return Result
            End Get
            Set(ByVal Value As String)
                'only here for proxy!
            End Set
        End Property
#End Region

#Region " FR 3015 "
        Public Property ImportedDate() As Date
            Get
                If mImportedDate.Ticks = 0 Then
                    mImportedDate = Date.Now
                End If
                Return mImportedDate
            End Get
            Set(ByVal Value As Date)
                mImportedDate = Value
            End Set
        End Property
        Private mImportedDate As Date
#End Region

        Public Property InjuryDetails() As String
            Get
                Return mInjuryDetails
            End Get
            Set(ByVal Value As String)
                mInjuryDetails = Value
            End Set
        End Property
        Private mInjuryDetails As String

        Public Property AcquisitionMethod() As BaseBird.AcquisitionMethodTypes
            Get
                Return mAcquisitionMethod
            End Get
            Set(ByVal Value As BaseBird.AcquisitionMethodTypes)
                mAcquisitionMethod = Value
            End Set
        End Property
        Private mAcquisitionMethod As BaseBird.AcquisitionMethodTypes

        Public Property AcquisitionMethod_Helper() As String
            Get
                Return System.Enum.GetName(GetType(BaseBird.AcquisitionMethodTypes), mAcquisitionMethod)
            End Get
            Set(ByVal Value As String)
                'helper. so not set required.  Just for proxy
            End Set
        End Property

        Friend Sub SetAquisitionMethod(ByVal newValue As String)
            If Not newValue Is Nothing AndAlso newValue.Length > 0 Then
                Dim Result As BaseBird.AcquisitionMethodTypes
                Try
                    Result = CType(System.Enum.Parse(GetType(BaseBird.AcquisitionMethodTypes), newValue), BaseBird.AcquisitionMethodTypes)
                Catch
                    Throw New ArgumentException(newValue & " cannot be parsed into AquisitionMethod")
                End Try
                mAcquisitionMethod = Result
            End If
        End Sub

        Public Property DateFound() As Date
            Get
                Return mDateFound
            End Get
            Set(ByVal Value As Date)
                mDateFound = Value
            End Set
        End Property
        Private mDateFound As Date

        Public Property Parents() As Parents
            Get
                If mParents Is Nothing Then mParents = New Parents
                Return mParents
            End Get
            Set(ByVal Value As Parents)
                mParents = Value
            End Set
        End Property
        Private mParents As Parents
    End Class
End Namespace
