Namespace Application
    Interface IBOPermitHistory
        Inherits IBOPermitInfo

        Property PermitHistoryId() As Int32

        Property ChangedByUser() As BOAuthorisedUser
        Property ChangeDate() As Date

    End Interface
End Namespace
