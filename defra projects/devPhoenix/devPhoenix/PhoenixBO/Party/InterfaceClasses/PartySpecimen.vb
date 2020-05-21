Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Party
    Public Class BOPartySpecimen
        Inherits BaseBO

#Region " Prelim code "

        ''' <summary>
        ''' Enum of the different relationship roles a party can have with a specimen.
        ''' Examples: breeder, keeper, owner.
        ''' </summary>
        Public Enum Role
	        Keeper = 1
	        Owner = 2
        End Enum

        ''' <summary>
        ''' Status of the PartySpecimen link
        ''' </summary>
        Public Enum Status
	        Current = 0
	        Exported = 1
	        Lost = 2
	        Released = 3
	        Stolen = 4
	        Transferred = 5
	        Sold = 6
        End Enum

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal partyId As Int32, ByVal specimenId As Int32, ByVal addressId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyBase.New()
            Me.PartyId = partyId
            Me.AddressId = addressId
            Me.SpecimenId = specimenId
        End Sub

        Public Sub New(ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction)
            MyClass.New()
            LoadPartySpecimen(specimenId, tran)
        End Sub

        Public Sub New(ByVal specimenId As Int32)
            MyClass.New(specimenId, Nothing)
        End Sub

        Private Function LoadPartySpecimen(ByVal specimenId As Int32) As Entity.PartySpecimen
            Return LoadPartySpecimen(specimenId, Nothing)
        End Function

        Private Function LoadPartySpecimen(ByVal specimenId As Int32, ByVal tran As SqlClient.SqlTransaction) As Entity.PartySpecimen
            Dim specimens As EntitySet.PartySpecimenSet = Entity.PartySpecimen.GetForSpecimen(specimenId)
            For Each specimen As Entity.PartySpecimen In specimens
                If specimen.IsEndDateNull() Then
                    InitialisePartySpecimen(specimen, tran)
                    Return specimen
                End If
            Next
            Throw New RecordDoesNotExist("PartySpecimen", specimenId)
        End Function

        Friend Overridable Sub InitialisePartySpecimen(ByVal specimen As Entity.PartySpecimen, ByVal tran As SqlClient.SqlTransaction)
            With specimen
                CheckSum = .CheckSum
                mSpecimenId = .SpecimenID
                mPartyId = .PartyID
                mAddressId = .AddressId
                mRoleType = .RoleType
                mStartDate = .StartDate     'MLD 26/1/5 added (no need to check for null, assignment copes)
                mEndDate = .EndDate     'MLD 26/1/5 added (no need to check for null, assignment copes)
                If Not .IsPartySpecimenStatusNull Then Me.mPartySpecimenStatus = .PartySpecimenStatus
            End With
        End Sub
#End Region

#Region " Properties "


        Private mSpecimenId As Integer
        Private mPartyId As Integer
        Private mAddressId As Integer
        Private mRoleType As Integer = Role.Keeper
        Private mStartDate As Date = Date.Now
        Private mEndDate As Date
        Private mPartySpecimenStatus As Integer = Status.Current

        Public Property SpecimenId As Integer
            Get
                Return mSpecimenId
            End Get
            Set
                mSpecimenId = value
            End Set
        End Property
        
        Public Property PartyId As Integer
            Get
                Return mPartyId
            End Get
            Set
                mPartyId = value
            End Set
        End Property
        
        Public Property AddressId As Integer
            Get
                Return mAddressId
            End Get
            Set
                mAddressId = value
            End Set
        End Property
        
        Public Property RoleType As Integer
            Get
                Return mRoleType
            End Get
            Set
                mRoleType = value
            End Set
        End Property
        
        Public Property StartDate As Date
            Get
                Return mStartDate
            End Get
            Set
                mStartDate = value
            End Set
        End Property
        
        Public Property EndDate As Date
            Get
                Return mEndDate
            End Get
            Set
                mEndDate = value
            End Set
        End Property
        
        Public Property PartySpecimenStatus As Integer
            Get
                Return mPartySpecimenStatus
            End Get
            Set
                PartySpecimenStatus = value
            End Set
        End Property
#End Region

#Region " Helper Functions "
        Public Shared Function GetPartySpecimensByPartyId(ByVal partyId As Int32) As BOPartySpecimen()
            Return GetPartySpecimens(Entity.PartySpecimen.GetForParty(partyId))
        End Function

        Public Shared Function GetPartySpecimensByAddressId(ByVal addressId As Int32) As BOPartySpecimen()
            Return GetPartySpecimens(Entity.PartySpecimen.GetForAddress(addressId))
        End Function

        Private Shared Function GetPartySpecimens(ByRef specimens As EntitySet.PartySpecimenSet) As BOPartySpecimen()
            Dim results(-1) As BOPartySpecimen
            If Not specimens Is Nothing Then
                Dim index As Integer = 0
                Redim results(specimens.Count - 1)
                For Each specimen As Entity.PartySpecimen in specimens
                    results(index) = New BOPartySpecimen()
                    results(index).InitialisePartySpecimen(specimen, Nothing)
                    index += 1
                Next
            End If 
            Array.Sort(results, New PartySpecimenComparer)   
            Return results
       End Function
#End Region

#Region " Save "
        Public Overloads Overrides Function Save() As BaseBO
            Dim cash As New Entity.PartySpecimen
            Dim service As service.PartySpecimenService = cash.ServiceObject
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction

            Dim result As BaseBO = MyClass.Save(tran)
            If result Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return result
        End Function

        Public Overridable Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            MyBase.Save()

            Dim specimen As New Entity.PartySpecimen
            Dim service As service.PartySpecimenService = specimen.ServiceObject

            If Created Then
                service.Insert(mSpecimenId, _
                               mPartyId, _
                               mAddressId, _
                               mStartDate, _
                               GetDatabaseDate(mEndDate), _
                               mRoleType, _
                               mPartySpecimenStatus, _
                               tran)
            Else
                service.Update(mSpecimenId, _
                                mPartyId, _
                                mAddressId, _
                                mStartDate, _
                                GetDatabaseDate(mEndDate), _
                                mRoleType, _
                                mPartySpecimenStatus, _
                                CheckSum, _
                                tran)
            End If
            Return Me
        End Function

#End Region

#Region " Validate "
        'Public Overloads Function Validate(ByVal userID As Int32, ByVal writeFlag As Boolean) As ValidationManager
        '    ' init the errors list
        '    MyBase.ValidationErrors = New ValidationManager(ValidationManager.ValidationTitles.CannotSavePartySpecimen)

        '    'MyBase.Validate(userID)

        '    ''check to see if we have too many species
        '    'If Not MyBase.Specie Is Nothing Then
        '    '    If MyBase.Specie.Length > 6 Then
        '    '        MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.TooManySpecie))
        '    '    End If
        '    '    For Each SpecieItem As BOSpecie In MyBase.Specie
        '    '        If Not SpecieItem.IsAnnexC AndAlso Not SpecieItem.IsAnnexD Then
        '    '            If writeFlag Then Validated = False
        '    '            MyBase.ValidationErrors.AddError(New ValidationError(ValidationError.ValidationCodes.SpecieMustBeAnnexCOrAnnexD))
        '    '            'no point in telling them that more than one has failed, so bail ourt of loop
        '    '            Exit For
        '    '        End If
        '    '    Next SpecieItem
        '    'End If

        '    If MyBase.ValidationErrors.HasErrors Then
        '        'If writeFlag Then Validated = False
        '    Else
        '        'If writeFlag Then Validated = True
        '        MyBase.ValidationErrors = Nothing
        '    End If

        '    Return MyBase.ValidationErrors
        'End Function
#End Region

#Region " Operations "


#End Region

        Private Class PartySpecimenComparer
            Implements IComparer

            Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xitem As BOPartySpecimen = CType(x, BOPartySpecimen)
                Dim yitem As BOPartySpecimen = CType(y, BOPartySpecimen)
                Dim result As Integer = Compare(xitem.PartyId, yitem.PartyId)
                
                If result = 0 Then
                    result = Compare(xitem.AddressId, yitem.AddressId)
                    If result = 0 Then
                        result = Compare(xitem.SpecimenId, yitem.SpecimenId)
                    End If
                End If
                Return result
            End Function

            Private Function Compare(ByVal x As Integer, ByVal y As Integer) As Integer
                If x < y Then Return -1
                If x > y Then Return 1
                Return 0
            End Function
        End Class
    End Class
End Namespace
