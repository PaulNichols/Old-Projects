Imports System.Web.UI.WebControls
Public Interface IManagerControl
    Enum Direction
        Prev
        [Next]
        Refresh
        None
    End Enum

    Sub Redirect(ByVal url As String)
    ReadOnly Property ManagerViewState() As Web.UI.StateBag
    Property CurrentStageNumber() As Int32
    ReadOnly Property ModulePageDetails() As IPageDetails
    Property DataObject() As Object
    Property ObjectBag() As Object
    Property SelectedGridItem() As Object
    Property SelectedGridItemIndex() As Int32
    Property IsReadOnly() As Boolean
    ReadOnly Property SaveButton() As LinkButton
    Overloads Sub GotoStage(ByVal stageNumber As Int32)
    ReadOnly Property NextButton() As LinkButton
    ReadOnly Property CancelButton() As LinkButton
    ReadOnly Property PreviousButton() As LinkButton
    ReadOnly Property AnotherButton() As LinkButton
    ReadOnly Property DeleteButton() As LinkButton
    ReadOnly Property SubjectLabel() As Label
    ReadOnly Property StateLabel() As Label
    Property ChildStageId(ByVal stageClassName As String) As Int32
    Overloads Sub Navigate(ByVal direction As Direction)
    Overloads Sub Navigate(ByVal direction As Direction, ByVal skipSave As Boolean)
    ' Function SaveDataObjects() As Boolean
    Function GetFromObjectBag(ByVal Key As String) As Object
    Sub RemoveFromObjectBag(ByVal Key As String)
    Sub StoreInObjectBag(ByVal Key As String, ByVal Data As Object)
    Sub ClearObjectBag()
    Function KeyExistsInObjectBag(ByVal Key As String) As Boolean
    Function CalledFromMenuClick(ByVal ManagerName As String) As Boolean
    Sub SetAnotherButtonVisible(ByVal Visible As Boolean)
    
End Interface
