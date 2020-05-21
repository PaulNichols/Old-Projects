Public Interface IBaseUpdatableGrid
    Sub GetColumns(ByRef DataGridColumnCollection As PhoenixControls.Controls.BaseGridAndPager.PhoenixGridColumnCollection)
    Sub GetData(ByRef dataSource As Object, ByRef uniqueIdentifier As String)
    Property [ReadOnly]() As Boolean
    Function AddItem(ByVal item As Object) As Boolean
    Function ReplaceItem(ByVal item As Object, ByVal index As Int32) As Boolean
    Function RemoveItem(ByVal index As Int32) As Boolean
    'Function GetItemIndex(ByVal item As Object) As Int32
    Sub ClearItems()
    Sub Refresh()
    Sub AddingControlToCell(ByRef controlToAdd As Control, ByVal column As ITemplate, ByVal row As Int32)
    ReadOnly Property UniqueIdentifier() As String
    ReadOnly Property DeleteFunctionName() As String
    ReadOnly Property SaveFunctionName() As String
    Sub ShowControl(ByRef control As Control, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
    Function GetWebService() As Object
    ReadOnly Property WebServiceType() As WebServiceTypeEnum


    Enum WebServiceTypeEnum
        WebService = 1
        CreateParty = 2
        Lists = 3
        SearchParty = 4
        ' PAF = 5
        PartyNotes = 6
        Common = 7
        ' sz added 6/10/2003
        MaintainGWDRefData = 8
        CITESApplication = 9
        SeizureNotification = 10
        Configuration = 11
        ReportService = 12
        ImportNotification = 13
        TaxonomyService = 14
        SecurityService = 15
        ApplicationProgression = 16
        ApplicationSearch = 17
        ' mld added 16/08/2004
        PaymentsService = 18
        ' ajc added 23/09/2004
        SSOWse = 19
        Notification = 20
        TaxonNotes = 21
        BirdService = 22
        Enquiry = 23
    End Enum

End Interface
