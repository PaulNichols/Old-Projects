Namespace Application
    <ServiceMapping(GetType(DataObjects.Service.SavedApplicationSearchService)), _
    EntityMapping(GetType(DataObjects.Entity.SavedApplicationSearch)), _
    CollectionMapping(GetType(DataObjects.Collection.SavedApplicationSearchBoundCollection))> _
   Public Class BOSaveApplicationSearch
        Inherits BOSaveApplicationSearchLite
        Implements IBOSaveApplicationSearch

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id)
        End Sub

        <DOtoBOMapping("Criteria")> _
Public Property Criteria() As String Implements IBOSaveApplicationSearch.Criteria
            Get
                Return mCriteria
            End Get
            Set(ByVal Value As String)
                mCriteria = Value
            End Set
        End Property
        Private mCriteria As String

        <DOtoBOMapping("Modified")> _
     Public Property Modified() As Date Implements IBOSaveApplicationSearch.Modified
            Get
                Return mModified
            End Get
            Set(ByVal Value As Date)
                mModified = Value
            End Set
        End Property
        Private mModified As Date

        <DOtoBOMapping("UserId")> _
             Public Property UserId() As Decimal Implements IBOSaveApplicationSearch.UserId
            Get
                Return mUserId
            End Get
            Set(ByVal Value As Decimal)
                mUserId = Value
            End Set
        End Property
        Private mUserId As Decimal

        <DOtoBOMapping("WorkList")> _
        Public Property WorkList() As Boolean Implements IBOSaveApplicationSearch.WorkList
            Get
                Return mWorkList
            End Get
            Set(ByVal Value As Boolean)
                mWorkList = Value
            End Set
        End Property
        Private mWorkList As Boolean

        Public Shared Function DeleteById(ByVal id As Int32) As Boolean
            Return [DO].DataObjects.Entity.SavedApplicationSearch.DeleteById(id, 0, Nothing)
        End Function

        Public Shared Function GetSavedSearches(ByVal userId As Decimal, ByVal includeWorklist As Boolean) As Application.BOSaveApplicationSearch()
            Dim SavedApplicationSearches As [DO].DataObjects.EntitySet.SavedApplicationSearchSet = [DO].DataObjects.Service.SavedApplicationSearchService.GetSavedSearches(userId, includeWorklist)
            If Not SavedApplicationSearches Is Nothing AndAlso SavedApplicationSearches.Entities.Count > 0 Then
                Dim ReturnValues(SavedApplicationSearches.Entities.Count - 1) As Application.BOSaveApplicationSearch
                Dim Index As Int32
                For Each item As [DO].DataObjects.Entity.SavedApplicationSearch In SavedApplicationSearches.Entities
                    ReturnValues(Index) = New Application.BOSaveApplicationSearch(item.SavedSearchId)
                    Index += 1
                Next
                Return ReturnValues
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetWorkList(ByVal userId As Decimal) As Application.BOSaveApplicationSearch
            Dim WorkList As [DO].DataObjects.Entity.SavedApplicationSearch = [DO].DataObjects.Service.SavedApplicationSearchService.GetWorkList(userId)
            If Not WorkList Is Nothing Then
                Return New Application.BOSaveApplicationSearch(WorkList.SavedSearchId)
            Else
                Return Nothing
            End If
        End Function

        Public Overloads Overrides Function Save() As BaseBO
            Dim Service As New [DO].DataObjects.Service.SavedApplicationSearchService
            Dim Tran As SqlClient.SqlTransaction = Service.BeginTransaction
            Dim Success As BaseBO

            If Me.WorkList Then
                If Service.DeleteAll(Me.UserId, Tran) Then
                    Success = MyBase.Save(Tran)
                End If
            Else
                Try
                    Success = MyBase.Save(Tran)
                Catch ex As Exception
                    Throw ex
                End Try

                If Not Success Is Nothing Then
                    Dim SavedSearches As Application.BOSaveApplicationSearch() = GetSavedSearches(Me.UserId, False)
                    'only allowed 5 saved searches and we are about to save 
                    Do While SavedSearches.Length > 5

                        If [DO].DataObjects.Entity.SavedApplicationSearch.DeleteById(SavedSearches(0).SavedSearchId, 0, Tran) Then
                            Dim TempArray(SavedSearches.Length - 2) As Application.BOSaveApplicationSearch
                            System.Array.Copy(SavedSearches, 1, TempArray, 0, TempArray.Length)
                            ReDim SavedSearches(SavedSearches.Length - 2)
                            SavedSearches = TempArray
                        End If
                    Loop
                End If
            End If
            If Not Success Is Nothing Then
                Service.EndTransaction(Tran, EnterpriseObjects.Service.TransactionEndEnum.Commit)
            Else
                Service.EndTransaction(Tran, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
            End If
            Return Success
        End Function

        Public Class SavedSearchDetails
            Public Property SearchCriteria() As Application.Search.ApplicationSearchCriteriaDetailed
                Get
                    Return mSearchCriteria
                End Get
                Set(ByVal Value As Application.Search.ApplicationSearchCriteriaDetailed)
                    mSearchCriteria = Value
                End Set
            End Property
            Private mSearchCriteria As Application.Search.ApplicationSearchCriteriaDetailed

            Public Property SearchMode() As Application.Search.ApplicationSearch.SearchMode
                Get
                    Return mSearchMode
                End Get
                Set(ByVal Value As Application.Search.ApplicationSearch.SearchMode)
                    mSearchMode = Value
                End Set
            End Property
            Private mSearchMode As Application.Search.ApplicationSearch.SearchMode
        End Class

    End Class

    <ServiceMapping(GetType(DataObjects.Service.SavedApplicationSearchService)), _
      EntityMapping(GetType(DataObjects.Entity.SavedApplicationSearch)), _
      CollectionMapping(GetType(DataObjects.Collection.SavedApplicationSearchBoundCollection))> _
      Public Class BOSaveApplicationSearchLite
        Inherits BaseDataBO
        Implements IBOSaveApplicationSearchLite

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Int32)
            MyBase.New(id)
        End Sub

        Private mDescription As String

        <DOtoBOMapping("Description")> _
        Property Description() As String Implements IBOSaveApplicationSearchLite.description
            Get
                Return mDescription
            End Get
            Set(ByVal Value As String)
                mDescription = Value
            End Set
        End Property

        <DOtoBOMapping("SavedSearchId")> _
             Public Property SavedSearchId() As Integer Implements IBOSaveApplicationSearchLite.SavedSearchId
            Get
                Return mSavedSearchId
            End Get
            Set(ByVal Value As Integer)
                mSavedSearchId = Value
            End Set
        End Property
        Private mSavedSearchId As Int32


        Public Shared Function GetSavedSearchesLite(ByVal userId As Decimal, ByVal includeWorklist As Boolean) As Application.BOSaveApplicationSearchLite()
            Dim SavedApplicationSearches As [DO].DataObjects.EntitySet.SavedApplicationSearchSet = [DO].DataObjects.Service.SavedApplicationSearchService.GetSavedSearches(userId, includeWorklist)
            If Not SavedApplicationSearches Is Nothing AndAlso SavedApplicationSearches.Entities.Count > 0 Then
                Dim ReturnValues(SavedApplicationSearches.Entities.Count - 1) As Application.BOSaveApplicationSearchLite
                Dim Index As Int32
                For Each item As [DO].DataObjects.Entity.SavedApplicationSearch In SavedApplicationSearches.Entities
                    ReturnValues(Index) = New Application.BOSaveApplicationSearchLite(item.SavedSearchId)
                    Index += 1
                Next
                Return ReturnValues
            Else
                Return Nothing
            End If
        End Function
    End Class
End Namespace