Imports System.Configuration
Namespace ASPNET.StarterKit.Portal

    Public MustInherit Class Contacts
        Inherits ASPNET.StarterKit.Portal.PortalModuleControl
        Protected WithEvents lblFileAsOrder As System.Web.UI.WebControls.Label
        Protected WithEvents lblCompanyNameOrder As System.Web.UI.WebControls.Label
        Protected WithEvents grdContacts As System.Web.UI.WebControls.DataGrid
        Protected Title As UserControl
        Protected Alphabet As UserControl
        ' Protected WithEvents butPrint As System.Web.UI.WebControls.HyperLink
        Protected GeneralTemplates As UserControl
        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        Protected WithEvents lblPublic As System.Web.UI.WebControls.Label
        'Protected WithEvents lblPrintHeader As System.Web.UI.WebControls.Label
        'Protected WithEvents lblPrintSpaceer As System.Web.UI.WebControls.Label
        Protected _styleSheet As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

        End Sub

#End Region

        '*******************************************************
        '
        ' The Page_Load event handler on this User Control is used to
        ' obtain a DataReader of contact information from the Contacts
        ' table, and then databind the results to a DataGrid
        ' server control.  It uses the ASPNET.StarterKit.PortalContactsDB()
        ' data component to encapsulate all data functionality.
        '
        '*******************************************************'
        'Public testprop As String = "hello"

        Public ReadOnly Property PublicLabel() As Label
            Get
                Return Me.lblPublic
            End Get
        End Property


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' Obtain contact information from Contacts table
            ' and bind to the DataGrid Control
            Dim Rebind As Boolean

            If Not IsPostBack Then

                If lblPublic.Text.Equals(String.Empty) Then lblPublic.Text = "True"
                lblFileAsOrder.Text = "Asc"
                lblFileAsOrder.Font.Bold = True
                lblCompanyNameOrder.Text = "Desc"
            Else
                'SetPublic((radPublic.SelectedItem.Value = 0))
                PublicLabel.Text = CType(CType(Alphabet.FindControl("radPublic"), RadioButtonList).SelectedItem.Value = 0, Boolean)
                'Response.Redirect(Request.Url.ToString)
                Rebind = True
            End If
            Dim IsPrint As Boolean = (Not Request.QueryString("Print") Is Nothing)
            'Title.Visible = Not IsPrint
            Alphabet.Visible = Not IsPrint
            GeneralTemplates.Visible = Not IsPrint
            If IsPrint Then
                CType(FindControl("Title").FindControl("butPrintPreview"), LinkButton).Text = "Close Preview"
                _styleSheet = "stylesPrint.css"
                'If Not grdContacts.PageSize = CType(ConfigurationSettings.AppSettings("ContactsPrintPageSize"), Int32) Then
                'grdContacts.PageSize = CType(ConfigurationSettings.AppSettings("ContactsPrintPageSize"), Int32)
                Rebind = True
                'End If
            Else
                CType(FindControl("Title").FindControl("butPrintPreview"), LinkButton).Text = "Print Preview"
                _styleSheet = "ASPNETPortal.css"
                If Not grdContacts.PageSize = CType(ConfigurationSettings.AppSettings("ContactsGridPageSize"), Int32) Then
                    Rebind = True
                    grdContacts.PageSize = CType(ConfigurationSettings.AppSettings("ContactsGridPageSize"), Int32)
                End If
            End If
            If IsPrint Then CType(FindControl("Title").FindControl("ModuleTitle"), Label).Text = "SHAFTESBURY CONTACT LIST"
            CType(FindControl("Title").FindControl("butPrintPreview"), LinkButton).Visible = True
            CType(FindControl("Title").FindControl("butPrintPreview"), LinkButton).Attributes.Add("IsPrint", IsPrint)
            Page.FindControl("Banner").Visible = Not IsPrint

            grdContacts.ShowFooter = Not IsPrint
            grdContacts.AllowPaging = Not IsPrint
            grdContacts.AllowSorting = Not IsPrint

            If Rebind OrElse Not IsPostBack Then GridBind()

        End Sub

        Private Overloads Sub GridBind()
            If lblCompanyNameOrder.Font.Bold Then
                GridBind("Company Name " & lblCompanyNameOrder.Text)
            Else
                GridBind("FileAs " & lblFileAsOrder.Text)
            End If
        End Sub

        Private Overloads Sub GridBind(ByVal sortExpression As String)
            Dim contacts As New ASPNET.StarterKit.Portal.ContactsDB

            With grdContacts
                Dim UserId As Object
                If Not Boolean.Parse(lblPublic.Text) Then
                    UserId = CType(Session("User"), UserDetails).Id
                End If
                If Request.QueryString.Count > 2 Then
                    .DataSource = contacts.GetContacts(ModuleId, Request.QueryString.Item(2), UserId).Tables(0).DefaultView
                Else
                    .DataSource = contacts.GetContacts(ModuleId, "A", UserId).Tables(0).DefaultView
                End If
                If sortExpression <> "" Then
                    CType(.DataSource, DataView).Sort = sortExpression
                End If
                .DataBind()
                If .PageCount = 1 Then .Columns(0).FooterText = ""
                .PagerStyle.Visible = (.PageCount > 1)
                If (Not Request.QueryString("Print") Is Nothing) Then
                    RemoveHyperlinks()
                End If
            End With
        End Sub

        Private Sub RemoveHyperlinks()
            For Each item As DataGridItem In grdContacts.Items
                For Each cell As TableCell In item.Cells
                    For Each control As control In cell.Controls
                        If TypeOf control Is HyperLink Then
                            CType(control, HyperLink).NavigateUrl = ""
                        End If
                    Next
                Next cell
            Next item
        End Sub

        Private Sub grdContacts_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdContacts.SortCommand
            If e.SortExpression = "FileAs" Then
                If lblFileAsOrder.Text = "Desc" Then
                    GridBind(e.SortExpression & " Asc")
                    lblFileAsOrder.Text = "Asc"
                    lblFileAsOrder.Font.Bold = True
                    lblCompanyNameOrder.Font.Bold = False
                Else
                    GridBind(e.SortExpression & " Desc")
                    lblFileAsOrder.Text = "Desc"
                    lblFileAsOrder.Font.Bold = True
                    lblCompanyNameOrder.Font.Bold = False
                End If
                lblCompanyNameOrder.Text = "Desc"
            Else
                If lblCompanyNameOrder.Text = "Desc" Then
                    GridBind(e.SortExpression & " Asc")
                    lblCompanyNameOrder.Text = "Asc"
                    lblFileAsOrder.Font.Bold = False
                    lblCompanyNameOrder.Font.Bold = True
                Else
                    GridBind(e.SortExpression & " Desc")
                    lblCompanyNameOrder.Text = "Desc"
                    lblFileAsOrder.Font.Bold = False
                    lblCompanyNameOrder.Font.Bold = True
                End If
                lblFileAsOrder.Text = "Asc"
            End If
        End Sub

        Private Sub grdContacts_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdContacts.PageIndexChanged
            grdContacts.CurrentPageIndex = e.NewPageIndex
            GridBind()
        End Sub


    End Class

End Namespace