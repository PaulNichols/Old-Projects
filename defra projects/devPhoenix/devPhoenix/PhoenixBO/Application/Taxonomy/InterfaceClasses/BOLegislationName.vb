Imports uk.gov.defra.Phoenix.DO.DataObjects
Imports uk.gov.defra.Phoenix.BO.Payments

Namespace Taxonomy
    <Serializable()> _
    Public Class BOLegislationName
        Inherits PaymentsBaseBO
        Implements ILegislationName

#Region " Prelim code "
        Public Sub New()
            'This constructor is intentionally empty.
        End Sub

        Public Sub New(ByVal id As Int32)
            Dim service As New Service.TaxonomyLegislationNameService
            Dim result As Entity.TaxonomyLegislationName = service.GetById(id)
            If result Is Nothing Then
                Throw New RecordDoesNotExist("LegislationName", id)
            Else
                InitialiseValue(result)
            End If
        End Sub

        Private Sub InitialiseValue(ByVal val As Entity.TaxonomyLegislationName)
            With val
                mLegislationNameId       = .LegislationNameId
                mLegislationShortName    = .LegislationShortName
                mLegislationLongName     = .LegislationLongName
                mLegislationLevel        = ToChar(.LegislationLevel)
                mLegislationDateAdopted  = .LegislationDateAdopted 
                mLegislationDateEnforced = .LegislationDateEnforced
                mLegislationURL          = .LegislationURL
                mLegislationNameStatus   = ToChar(.LegislationNameStatus)
                mNote                    = .Note
                mCheckSum                = .CheckSum
            End With
        End Sub

        Private Function ToChar(ByVal field As String) as Char
            If field Is Nothing OrElse field.Length = 0 Then
                Return Nothing
            End If
            Return field.ToCharArray()(0)
        End Function

        Public Shared Function GetNames(ByVal withPermittedValues As Boolean) As BOLegislationNameSummary()
            Dim items As Views.EntitySet.LegislationNameWithPermittedCountSet = Views.Entity.LegislationNameWithPermittedCount.GetAll()
            Dim results(items.Count) As BOLegislationNameSummary
            Dim i As Int32 = 0

            For Each item As Views.Entity.LegislationNameWithPermittedCount in items
                If (item.PermittedListingCount > 0) = withPermittedValues Then
                    results(i) = New BOLegislationNameSummary(item.LegislationNameID, item.LegislationShortName)
                    i += 1
                End If
            Next
            Redim Preserve results(i - 1)
            Return results
        End Function

#End Region

#Region " Properties "
        Public Property LegislationNameId As Int32 Implements ILegislationName.LegislationNameId
            Get
                Return mLegislationNameId
            End Get
            Set
                mLegislationNameId = Value
            End Set
        End Property
        Private mLegislationNameId As Int32

        Public Property LegislationShortName As String Implements ILegislationName.LegislationShortName
            Get
                Return mLegislationShortName
            End Get
            Set
                mLegislationShortName = Value
            End Set
        End Property
        Private mLegislationShortName As String

        Public Property LegislationLongName As String Implements ILegislationName.LegislationLongName
            Get
                Return mLegislationLongName
            End Get
            Set
                mLegislationLongName = Value
            End Set
        End Property
        Private mLegislationLongName As String

        Public Property LegislationLevel As Char Implements ILegislationName.LegislationLevel
            Get
                Return mLegislationLevel
            End Get
            Set
                mLegislationLevel = Value
            End Set
        End Property
        Private mLegislationLevel As Char

        Public Property LegislationDateAdopted As Date Implements ILegislationName.LegislationDateAdopted
            Get
                Return mLegislationDateAdopted
            End Get
            Set
                mLegislationDateAdopted = Value
            End Set
        End Property
        Private mLegislationDateAdopted As Date

        Public Property LegislationDateEnforced As Date Implements ILegislationName.LegislationDateEnforced
            Get
                Return mLegislationDateEnforced
            End Get
            Set
                mLegislationDateEnforced = Value
            End Set
        End Property
        Private mLegislationDateEnforced As Date

        Public Property LegislationURL As String Implements ILegislationName.LegislationURL
            Get
                Return mLegislationURL
            End Get
            Set
                mLegislationURL = Value
            End Set
        End Property
        Private mLegislationURL As String

        Public Property LegislationNameStatus As Char Implements ILegislationName.LegislationNameStatus
            Get
                Return mLegislationNameStatus
            End Get
            Set
                mLegislationNameStatus = Value
            End Set
        End Property
        Private mLegislationNameStatus As Char

        Public Property Note As String Implements ILegislationName.Note
            Get
                Return mNote
            End Get
            Set
                mNote = Value
            End Set
        End Property
        Private mNote As String

#End Region

#Region " Save "
        Public Overrides Overloads Function Save() As BaseBO
            Dim service As New Service.TaxonomyLegislationNameService
            Dim tran As SqlClient.SqlTransaction = service.BeginTransaction
            Dim result As BaseBO = MyClass.Save(tran)

            If result Is Nothing Then
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            Else
                service.EndTransaction(tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            End If
            Return result
        End Function

        Public Shadows Function Delete() As Boolean
            Dim service As New Service.TaxonomyLegislationNameService
            Return service.DeleteById(mLegislationNameId, Checksum)
        End Function

        Public Overloads Function Save(ByVal tran As SqlClient.SqlTransaction) As BaseBO
            Dim detail As New Entity.TaxonomyLegislationName
            Dim service As service.TaxonomyLegislationNameService = detail.ServiceObject
            Dim created As Boolean = mLegislationNameId = 0

            If created Then
                Throw New Exception("Not implemented yet")
                'detail = service.Insert(mLegislationNameId, _
                '                      mListingValue, _
                '                      tran)
            Else
                detail = service.Update(mLegislationNameId, _
                                      mLegislationShortName, _
                                      mLegislationLongName, _
                                      mLegislationLevel, _
                                      mLegislationDateAdopted, _
                                      mLegislationDateEnforced, _
                                      mLegislationURL, _
                                      mLegislationNameStatus, _
                                      mNote, _
                                      mChecksum, _
                                      tran)
            End If
            If detail Is Nothing Then
                CheckSqlErrors("Legislation Name", tran, service)
            Else
                If created And Not detail Is Nothing Then
                    mLegislationNameId = detail.Id
                End If
                If detail.CheckSum <> CheckSum Then
                     InitialiseValue(detail)
                End If
            End If
            Return Me
        End Function

#End Region
    End Class

    <Serializable()> _
    Public Class BOLegislationNameSummary

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Int32, ByVal name As String)
            mId   = id
            mName = name
        End Sub

        Public Property Id As Int32
            Get
                Return mId
            End Get
            Set
                mId = Value
            End Set
        End Property
        Private mId As Int32

        Public Property Name As String
            Get
                Return mName
            End Get
            Set
                mName = Value
            End Set
        End Property
        Private mName As String

    End Class
End Namespace
