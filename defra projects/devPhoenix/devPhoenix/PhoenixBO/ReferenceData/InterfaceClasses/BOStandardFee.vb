Namespace ReferenceData
    <ServiceMapping(GetType(DataObjects.Service.StandardFeeService)), _
     EntityMapping(GetType(DataObjects.Entity.StandardFee)), _
     CollectionMapping(GetType(DataObjects.Collection.StandardFeeBoundCollection)), _
     Serializable()> _
    Public Class BOStandardFee
        Inherits BO.ReferenceData.BOBaseReferenceTable

#Region " Variables "
        Private _CommercialPurpose As Integer
        Private _Fee As Decimal
        Private _PlantOrCoral As Integer
        Private _MaximumNumberOfSpecies As Int32
        Private _MinimumNumberOfSpecies As Int32
        Private _BirdFeeLevel As Short
        Private _LinkedApplicationTypeCode As Int32
        Private _ApplicationTypeCode As Int32
#End Region

#Region " DOtoBOMapping and Properties "

        <DOtoBOMapping("ApplicationTypeCode")> _
        Public Property ApplicationTypeCode() As Int32
            Get
                Return _ApplicationTypeCode
            End Get
            Set(ByVal Value As Int32)
                _ApplicationTypeCode = Value
            End Set
        End Property
        <DOtoBOMapping("LinkedApplicationTypeCode")> _
        Public Property LinkedApplicationTypeCode() As Int32
            Get
                Return _LinkedApplicationTypeCode
            End Get
            Set(ByVal Value As Int32)
                _LinkedApplicationTypeCode = Value
            End Set
        End Property
        <DOtoBOMapping("BirdFeeLevel")> _
        Public Property BirdFeeLevel() As Short
            Get
                Return _BirdFeeLevel
            End Get
            Set(ByVal Value As Short)
                _BirdFeeLevel = Value
            End Set
        End Property
        <DOtoBOMapping("MinimumNumberOfSpecies")> _
        Public Property MinimumNumberOfSpecies() As Int32
            Get
                Return _MinimumNumberOfSpecies
            End Get
            Set(ByVal Value As Int32)
                _MinimumNumberOfSpecies = Value
            End Set
        End Property
        <DOtoBOMapping("MaximumNumberOfSpecies")> _
        Public Property MaximumNumberOfSpecies() As Int32
            Get
                Return _MaximumNumberOfSpecies
            End Get
            Set(ByVal Value As Int32)
                _MaximumNumberOfSpecies = Value
            End Set
        End Property
        <DOtoBOMapping("PlantOrCoral")> _
        Public Property PlantOrCoral() As Integer
            Get
                Return _PlantOrCoral
            End Get
            Set(ByVal Value As Integer)
                _PlantOrCoral = Value
            End Set
        End Property
        <DOtoBOMapping("PlantOrCoralText")> _
        Public Property PlantOrCoralText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(_PlantOrCoral)
            End Get
            Set(ByVal Value As String)
                PlantOrCoral = Application.Search.ApplicationSearch.ConvertEnglishToInt32(Value)
            End Set
        End Property
        <DOtoBOMapping("CommercialPurpose")> _
        Public Property CommercialPurpose() As Integer
            Get
                Return _CommercialPurpose
            End Get
            Set(ByVal Value As Integer)
                _CommercialPurpose = Value
            End Set
        End Property
        <DOtoBOMapping("CommercialPurposeText")> _
        Public Property CommercialPurposeText() As String
            Get
                Return Application.Search.ApplicationSearch.ConvertToEnglishBoolean(_CommercialPurpose)
            End Get
            Set(ByVal Value As String)
                CommercialPurpose = Application.Search.ApplicationSearch.ConvertEnglishToInt32(Value)
            End Set
        End Property

        <DOtoBOMapping("Fee")> _
        Public Property Fee As Decimal      'MLD 29/3/5 moved in front of FeeText, as order is important for updating (loony, I know)
            Get
                Return _Fee
            End Get
            Set
                _Fee = Value
            End Set
        End Property

        <DOtoBOMapping("FeeText")> _
        Public Property FeeText As String   'MLD 29/3/5 refactored
            Get
                Return _Fee.ToString("c").Substring(1)
            End Get
            Set
                _Fee = Decimal.Parse(Value)
            End Set
        End Property
#End Region

#Region " Functions "

        Public Overrides Function Validate() As Boolean
            ' Rules for Validation.
            ' ApplicationTypeCode must exist - by default, since from a Pick List
            ' If LINKED code, must exist (see above) and if used must have CITES="Yes"
            ' Minimum must be 0 as Default.  Must not be overlap with otherMaximum/Minimum?
            ' Maximum is 99 and must be greater than Minimum
            ' BirdFeeLevel Need Not be supplied (= 0 by Default)

            ' 2. If LINKED code used must have CITES="Yes" for LinkedApplicationTypeCode
            ' 5. SET BirdFeeLevel = 0 if CITES="Yes" for ApplicationTypeCode
            ' 6. Plant or Coral - set to No if CITES="No"  for ApplicationTypeCode
            ' 6. Commercial Purpose - set to No if CITES="No" for ApplicationTypeCode

            Dim myDS As Data.DataSet
            Try
                myDS = [DO].DataObjects.Sprocs.dbo_usp_ValidateStandardFee_NonGenerated(ID, BirdFeeLevel, PlantOrCoral, CommercialPurpose, ApplicationTypeCode, LinkedApplicationTypeCode, MinimumNumberOfSpecies, MaximumNumberOfSpecies, Nothing, GetType(System.Data.DataSet))
                If myDS.Tables(0).Rows.Count = 1 Then
                    'select @OutBFLevel BirdFeeLevel, @OutPlant PlantOrCoral, @OutCommercial CommercialPurpose, @OutMinimumG Minimum ,@OutMaximumG Maximum

                    With myDS.Tables(0).Rows(0)
                        BirdFeeLevel = Short.Parse(.Item("BirdFeeLevel").ToString)
                        PlantOrCoral = Integer.Parse(.Item("PlantOrCoral").ToString)
                        CommercialPurpose = Integer.Parse(.Item("CommercialPurpose").ToString)
                        MaximumNumberOfSpecies = Integer.Parse(.Item("MinimumG").ToString)
                        MaximumNumberOfSpecies = Integer.Parse(.Item("MaximumG").ToString)
                    End With
                    Return True
                Else
                    Throw New Exception("Unexpected error has occurred!")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            myDS.Dispose()
            myDS = Nothing

        End Function

#End Region

#Region " Initialisation Routines "
        Public Sub New()
            ' Establish the Defaults
            _PlantOrCoral = 0
            _CommercialPurpose = 0
            _Fee = 0
            _BirdFeeLevel = 0
            _MaximumNumberOfSpecies = 0
            _MinimumNumberOfSpecies = 0

        End Sub
#End Region

        Protected Overrides Function GetLookupDataEntitySet(ByVal service As EnterpriseObjects.Service, ByVal includeHyphen As Boolean, ByVal includeInactive As Boolean) As EnterpriseObjects.EntitySet
            Return DataObjects.Entity.StandardFee.GetAll(includeHyphen, includeInactive, DataObjects.Base.StandardFeeServiceBase.OrderBy.IX_StandardFee)
        End Function
    End Class

End Namespace

