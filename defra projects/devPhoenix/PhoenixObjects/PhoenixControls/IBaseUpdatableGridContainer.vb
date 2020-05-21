Public Interface IBaseUpdatableGridContainer
    Sub GridClick(ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    Sub SetGridContainer()
    Function AddItem(ByVal item As Object) As Boolean
    Function ReplaceItem(ByVal newItem As Object, ByVal index As Int32) As Boolean
    Function RemoveItem(ByVal index As Int32) As Boolean
    '  Function GetItemIndex(ByVal item As Object) As Int32
    ReadOnly Property ItemCount() As Int32
    ReadOnly Property Items() As Object()
    ReadOnly Property Manager() As uk.gov.defra.PhoenixControls.IManagerControl
    ReadOnly Property Grid() As IBaseUpdatableGrid
    ReadOnly Property AllwaysGetItems() As Boolean
End Interface
