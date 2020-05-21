Imports System.ComponentModel
Imports System.Web.UI.WebControls
Imports uk.gov.defra.PhoenixControls.Controls

Namespace Controls
    <ToolboxBitmap(GetType(DataGrid)), _
       ToolboxData("<{0}:BaseGridAndPager runat='server' ShowGridHeader='True' width='100%'></{0}:BaseGridAndPager>")> _
   Public Class BaseGridAndPager
        Inherits Web.UI.WebControls.WebControl
        Implements INamingContainer

        Public Event ShowControl(ByRef control As Control, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Public Event SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
        Public Event SetRowForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Public Event SetCellForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal cellIndex As Int32)

        Protected WithEvents DataGridPager As uk.gov.defra.CommonCode.DataGridPager
        Public WithEvents DataGrid As BaseGrid

        Enum TemplateType
            Item
        End Enum

        'Private mDataSource As Object
        Private mDataMember As String
        Private mTag As Object          'MLD added 20/8/4


        Public Sub Refresh()
            Refresh(Me.Page.Session("DataSource"))
        End Sub

        Public Sub Refresh(ByVal collection As Object)
            Dim TheManager As IManagerControl = Manager()

            Try
                If Not collection Is Nothing Then
                    If SelectedIndex >= 0 AndAlso _
                       Not TheManager.SelectedGridItem Is Nothing Then
                        CType(collection, Array).SetValue(TheManager.SelectedGridItem, SelectedIndex)
                    End If
                End If
                Me.Page.Session("DataSource") = collection
                BindGrid(collection)
            Catch ex As Exception

            End Try
            ' End If
        End Sub

        Public Property Tag() As Object     'MLD added 20/8/4
            Get
                Return mTag
            End Get
            Set(ByVal Value As Object)
                mTag = Value
            End Set
        End Property

        Private ReadOnly Property Manager() As IManagerControl
            Get
                Return GetManager(Me.Parent)
            End Get
        End Property

        Private Function GetManager(ByVal container As Control) As IManagerControl

            If TypeOf container Is IManagerControl Then
                Return CType(container, IManagerControl)
            Else
                Return GetManager(container.Parent)
            End If
        End Function

        Private ReadOnly Property ManagedControl() As Control
            Get
                Return GetManaged(Me.Parent)
            End Get
        End Property

        Private Function GetManaged(ByVal container As Control) As Control

            If TypeOf container Is IManagedControl AndAlso TypeOf container.Parent Is IManagerControl Then
                Return container
            Else
                Return GetManaged(container.Parent)
            End If
        End Function

        Public ReadOnly Property ColumnCount() As Int32
            Get
                If Not DataGrid Is Nothing Then
                    Return DataGrid.Columns.Count
                End If
            End Get
        End Property

        Public Property UniqueIdentifier() As String
            Get
                If DataGrid Is Nothing Then Me.InitialiseBaseGrid()
                Return DataGrid.UniqueIdentifier(viewstate)
            End Get
            Set(ByVal Value As String)
                If DataGrid Is Nothing Then Me.InitialiseBaseGrid()
                DataGrid.UniqueIdentifier(viewstate) = Value
            End Set
        End Property

        'Public Shadows Sub DataBind(ByVal uniqueIdentifier As String, ByVal dataSource As Object)
        '    mDataSource = dataSource
        '    '   mDataMember = dataMember
        '    Me.UniqueIdentifier = uniqueIdentifier
        'End Sub

        'Public Property DataSource() As Object
        '    Get
        '        Return viewstate("DataSource")
        '    End Get
        '    Set(ByVal Value As Object)
        '        If viewstate("DataSource") Is Nothing Then
        '            viewstate.Add("DataSource", Value)
        '        Else
        '            viewstate("DataSource") = Value
        '        End If
        '    End Set
        'End Property
        Public Property ShowGridHeader() As Boolean
            Get
                Return mShowGridHeader
            End Get
            Set(ByVal Value As Boolean)
                mShowGridHeader = Value
            End Set
        End Property
        Private mShowGridHeader As Boolean

        Public Overrides ReadOnly Property Controls() As System.Web.UI.ControlCollection
            Get
                'Me.EnableViewState = True
                If Visible Then EnsureChildControls() '         Temporarily commented out 21/3/5
                Return MyBase.Controls
            End Get
        End Property

        Public Sub InitialiseBaseGrid()
            DataGrid = New BaseGrid(mShowGridHeader)
        End Sub

        Protected Overrides Sub CreateChildControls()

            Controls.Clear()

            InitialiseBaseGrid()

            Controls.Add(DataGrid)

            If AllowGridPaging Then
                With DataGrid
                    ' .AllowPaging = True
                    .AllowCustomPaging = True
                    .PageSize = 10
                    .PagerStyle.Mode = PagerMode.NumericPages
                    .PagerStyle.PageButtonCount = 10
                End With
            End If


            BindData()
            SetGridStyle()
            For Each row As DataGridItem In DataGrid.Items
                For Each cell As TableCell In row.Cells
                    AddEventHandlers(cell)
                Next
            Next
            ' If Me.DOPostBindAteration Then PostBindAlteration()
        End Sub

        'Public Property DOPostBindAteration() As Boolean
        '    Get
        '        Return CType(viewstate("DOPostBindAteration"), Boolean)
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        viewstate.Add("DOPostBindAteration", Value)
        '    End Set
        'End Property

        Public Property AllowGridPaging() As Boolean
            Get
                Return CType(viewstate("AllowGridPaging"), Boolean)
            End Get
            Set(ByVal Value As Boolean)
                viewstate.Add("AllowGridPaging", Value)
            End Set
        End Property

        Public Sub SetGridStyle()
            ' Me.DataGrid.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(0, 204, 204, 153)
            Me.DataGrid.HeaderStyle.CssClass = "CategoryHeader"


        End Sub



        'Private Property DataGridColumnCollection() As ArrayLists
        '    Get
        '        If viewstate("DataGridColumnCollection") Is Nothing Then
        '            viewstate.Add("DataGridColumnCollection", New ArrayList)
        '        End If
        '        Return CType(viewstate("DataGridColumnCollection"), ArrayList)
        '    End Get
        '    Set(ByVal Value As ArrayList)
        '        If viewstate("DataGridColumnCollection") Is Nothing Then
        '            viewstate.Add("DataGridColumnCollection", Value)
        '        Else
        '            viewstate("DataGridColumnCollection") = Value
        '        End If
        '    End Set
        'End Property
        Private WithEvents mDataGridColumnCollection As PhoenixGridColumnCollection

        <Serializable()> _
        Public Class PhoenixGridColumnCollection
            Inherits ArrayList

            Public Event AddingControl(ByRef controlToAdd As Control, ByVal column As ITemplate, ByVal row As Int32)

            Protected Overridable Sub OnAddingControl(ByRef controltoadd As Control, ByVal column As ITemplate, ByVal row As Int32)
                RaiseEvent AddingControl(controltoadd, column, row)
            End Sub

            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
              ByVal dataField As String, ByVal columnHeaderText As String, _
              ByVal templateControl As Type, ByVal bind As Boolean) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Return AddGridColumn(templateType, dataField, columnHeaderText, templateControl, bind, "View", False)
            End Function

            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
                ByVal datasource As Object, ByVal datatextfield As String, ByVal datavaluefield As String, ByVal columnHeaderText As String, _
                ByVal templateControl As Type, ByVal bind As Boolean, ByVal commandName As String, Optional ByVal postBack As Boolean = False) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Dim Column As New PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumnWithCombo(datasource, datatextfield, datavaluefield)
                Select Case templateType
                    Case templateType.Item
                        If templateControl.Equals(GetType(DropDownList)) Then
                            Dim Template As New BaseGrid.DropdownListTemplateColumn(commandName)
                            AddHandler Template.AddingControl, AddressOf OnAddingControl
                            Column.ItemTemplate = Template
                        End If
                End Select

                Column.HeaderText = columnHeaderText

                Add(Column)
                Return Column
            End Function

            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
                       ByVal dataField As String, ByVal columnHeaderText As String, _
                       ByVal templateControl As Type, ByVal bind As Boolean, ByVal commandName As String) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Return AddGridColumn(templateType, dataField, columnHeaderText, templateControl, bind, commandName, False, "")
            End Function

            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
             ByVal dataField As String, ByVal columnHeaderText As String, _
             ByVal templateControl As Type, ByVal bind As Boolean, ByVal commandName As String, ByVal postBack As Boolean) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Return AddGridColumn(templateType, dataField, columnHeaderText, templateControl, bind, commandName, postBack, "")
            End Function


            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
                 ByVal dataField As String, ByVal columnHeaderText As String, _
                 ByVal templateControl As Type, ByVal bind As Boolean, ByVal commandName As String, ByVal postBack As Boolean, ByVal tooltip As String) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Dim Column As New PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn(dataField, bind)
                Select Case templateType
                    Case templateType.Item
                        If templateControl.Equals(GetType(LinkButton)) Then
                            Dim Template As New BaseGrid.LinkButtonTemplateColumn(commandName, tooltip)
                            AddHandler Template.AddingControl, AddressOf OnAddingControl
                            Column.ItemTemplate = Template
                        End If
                        If templateControl.Equals(GetType(CheckBox)) Then
                            Dim Template As New BaseGrid.CheckboxTemplateColumn(commandName)
                            Template.postback = postBack
                            AddHandler Template.AddingControl, AddressOf OnAddingControl
                            Column.ItemTemplate = Template
                        End If
                        If templateControl.Equals(GetType(TextBox)) Then
                            Dim Template As New BaseGrid.TextBoxTemplateColumn(commandName)
                            Template.postback = postBack
                            AddHandler Template.AddingControl, AddressOf OnAddingControl
                            Column.ItemTemplate = Template
                        End If
                End Select

                Column.HeaderText = columnHeaderText

                Add(Column)
                Return Column
            End Function

            Public Overloads Function AddGridColumn(ByVal templateType As TemplateType, _
                ByVal textFieldName As String, ByVal columnHeaderText As String, _
                ByVal bind As Boolean, ByVal blankTarget As Boolean, ByVal url As String) As PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn

                Dim Column As New PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn(textFieldName, bind)
                Dim Template As New BaseGrid.HyperLinkTemplateColumn(textFieldName, url, blankTarget)
                AddHandler Template.AddingControl, AddressOf OnAddingControl
                Column.ItemTemplate = Template

                Column.HeaderText = columnHeaderText

                Add(Column)
                Return Column
            End Function

            Public Overloads Function AddGridColumn(ByVal dataField As String, ByVal headerText As String) As PhoenixControls.Controls.BaseGrid.PhoenixGridBoundColumn
                Dim Column As New PhoenixControls.Controls.BaseGrid.PhoenixGridBoundColumn(dataField, headerText)
                Add(Column)
                Return Column
            End Function
        End Class

        Public Event ViewCommand(ByVal sender As Object, ByVal dataItem As Object)
        Public Event GetData(ByVal sender As Object, ByRef dataSource As Object, ByRef uniqueIdentifier As String)
        Public Event GetColumns(ByVal sender As Object, ByRef DataGridColumnCollection As PhoenixControls.Controls.BaseGridAndPager.PhoenixGridColumnCollection)
        Public Event PostGetData()



        Public Sub BindData()
            If Manager.CurrentStageNumber = _
                Manager.ChildStageId(GetManaged(Me.NamingContainer).ID.ToLower) Then


                If Not mUseViewState OrElse DataSource Is Nothing Then
                    'Dim DataSource As Object
                    'Dim Id As String = UniqueIdentifier
                    RaiseEvent GetData(Me, DataSource, UniqueIdentifier)
                    If DataSource Is Nothing Then
                        Try
                            If Not UpdatableGrid Is Nothing Then 'TS stop raising when both are nothing added 02/11/04
                                UpdatableGrid.GetData(DataSource, UniqueIdentifier)
                            End If
                        Catch ex As Exception
                            Throw New Exception("There is a problem binding the grid data", ex) 'Added by NT 17/03/05 - exception was being swallowed.
                        End Try
                    End If
                    SelectedIndex = -1
                    'UniqueIdentifier = Id
                    Me.Page.Session.Add("DataSource", DataSource)
                Else
                End If



                If Not mUseViewState OrElse (Not Me.Page.Session("DataSource") Is Nothing AndAlso Not UniqueIdentifier Is Nothing) Then

                    AddColumns()

                End If
                BindGrid()
                RaiseEvent PostGetData()
            End If
        End Sub

        Public Overloads Sub AddColumns(ByVal dataGridColumnCollection As PhoenixControls.Controls.BaseGridAndPager.PhoenixGridColumnCollection)
            mDataGridColumnCollection = dataGridColumnCollection

            With DataGrid
                .Columns.Clear()

                AddHandler mDataGridColumnCollection.AddingControl, AddressOf OnAddingControl   'MLD
                If Not mDataGridColumnCollection Is Nothing Then
                    For Each column As System.Web.UI.WebControls.DataGridColumn In mDataGridColumnCollection
                        ' column.HeaderStyle.Width = New Unit(1, UnitType.Percentage)
                        If column.HeaderStyle.HorizontalAlign = HorizontalAlign.NotSet Then
                            column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center

                        End If
                        If column.ItemStyle.HorizontalAlign = HorizontalAlign.NotSet Then
                            column.ItemStyle.HorizontalAlign = HorizontalAlign.Center

                        End If
                        .AllowSorting = True
                        'AddHandler mDataGridColumnCollection.AddingControl, AddressOf OnAddingControl
                        .Columns.Add(column)
                    Next

                End If
            End With
        End Sub

        Public Overloads Sub AddColumns()
            mDataGridColumnCollection = New PhoenixControls.Controls.BaseGridAndPager.PhoenixGridColumnCollection
            If Not UpdatableGrid Is Nothing Then AddHandler mDataGridColumnCollection.AddingControl, AddressOf UpdatableGrid.AddingControlToCell
            Try
                UpdatableGrid.GetColumns(mDataGridColumnCollection)
            Catch ex As Exception
                RaiseEvent GetColumns(Me, mDataGridColumnCollection)
            End Try

            Me.AddColumns(mDataGridColumnCollection)
        End Sub

        Protected Overridable Sub OnAddingControl(ByRef controlToAdd As Control, ByVal column As ITemplate, ByVal row As Int32)
            If Not CheckBoxState Is Nothing AndAlso TypeOf controlToAdd Is CheckBox Then
                Try
                    CType(controlToAdd, CheckBox).Checked = CType(CheckBoxState(row), Boolean)
                Catch ex As Exception
                End Try
            End If
        End Sub

        Private ReadOnly Property UpdatableGrid() As IBaseUpdatableGrid
            Get
                Return CType(Parent.FindControl("pagInfo"), IBaseUpdatableGrid)
            End Get
        End Property

        Private Sub BindGrid()
            BindGrid(Me.Page.Session("DataSource"))
        End Sub

        Public WriteOnly Property UseViewState() As Boolean
            Set(ByVal Value As Boolean)
                mUseViewState = Value
            End Set
        End Property
        Private mUseViewState As Boolean = True

        Public Shadows Property DataSource() As Object
            Get
                Try
                    Return DataGrid.DataSource
                Catch ex As Exception
                    Return Me.Page.Session("DataSource")
                End Try
            End Get
            Set(ByVal Value As Object)
                Try
                    DataGrid.DataSource = Value
                Catch ex As Exception
                    If DataGrid Is Nothing Then
                        Throw New Exception("DataGrid bound too early", ex) 'Added by MLD 13/5/5
                    Else
                        Throw New Exception("There is a problem assigning the grid datasource -may not be a collection of objects", ex) 'Added by TS 26/04/05 - exception was being swallowed.
                    End If
                End Try
                Me.Page.Session("DataSource") = Value
            End Set
        End Property

        Private Function GetDataSourceItem(ByVal index As Int32) As Object
            Return CType(Page.Session("DataSource"), Object())(index)
        End Function

        Private Sub BindGrid(ByVal collection As Object)
            With DataGrid
                .AutoGenerateColumns = False

                Dim DS() As Object
                If collection Is Nothing Then
                    DS = Nothing
                Else
                    If AllowGridPaging Then
                        ReDim DS(DataGrid.PageSize - 1)
                        Dim maxLength As Int32 = CType(collection, Object()).Length
                        'If Not CType(collection, Object()).Length < DataGrid.PageSize Then
                        If Not maxLength < DataGrid.PageSize Then
                            'Array.Copy(CType(collection, Object()), DataGrid.CurrentPageIndex * DataGrid.PageSize, DS, 0, Me.DataGrid.PageSize)
                            Dim sourceIdx As Int32 = DataGrid.CurrentPageIndex * DataGrid.PageSize
                            Dim copyLength As Int32 = Me.DataGrid.PageSize
                            If sourceIdx + copyLength > maxLength Then
                                copyLength = maxLength - sourceIdx
                                ReDim DS(copyLength - 1)
                            End If
                            Array.Copy(CType(collection, Object()), sourceIdx, DS, 0, copyLength)
                            DataGrid.AllowPaging = True
                        Else
                            ReDim DS(CType(collection, Object()).Length - 1)
                            DS = CType(collection, Object())
                            DataGrid.AllowPaging = False
                        End If
                        DataGrid.VirtualItemCount = CType(collection, Object()).Length
                    Else
                        If Not collection Is Nothing Then
                            ReDim DS(CType(collection, Object()).Length - 1)
                            DS = CType(collection, Object())
                        End If
                    End If
                End If

                .DataSource = DS
                .UniqueIdentifier = UniqueIdentifier
                Try
                    .DataBind()
                    'PostBindAlteration()
                Catch ex As System.Exception
                    Throw ex
                End Try
            End With
        End Sub

        Public Property SelectedIndex() As Int32
            Get
                If Not Manager.ManagerViewState("SelectedIndex") Is Nothing Then
                    Return CType(Manager.ManagerViewState("SelectedIndex"), Int32)
                End If
            End Get
            Set(ByVal Value As Int32)
                Manager.ManagerViewState.Add("SelectedIndex", Value)
            End Set
        End Property

        Private Function HasInterface(ByVal gridContainer As Object, ByVal sender As Object, ByVal e As CommandEventArgs) As Boolean
            If Not gridContainer.GetType.GetInterface(GetType(IManagerControl).ToString) Is Nothing Then
                With CType(gridContainer, IManagerControl)
                    If TypeOf sender Is CheckBox Then
                        Dim index As Int32 = CType(CType(CType(sender, CheckBox).Parent, System.Web.UI.WebControls.TableCell).Parent, DataGridItem).ItemIndex
                        CheckedIndex = index                'MLD 23/3/5 some refactoring
                        If CheckBoxState Is Nothing Then
                            CheckBoxState = New SortedList
                        End If
                        CheckBoxState.Item(index) = e.CommandArgument
                        Try
                            CType(Parent, IBaseUpdatableGridContainer).GridClick(e)
                        Catch ex As Exception
                            RaiseEvent ViewCommand(sender, e.CommandArgument)
                        End Try
                    Else
                        Dim index1 As Int32 = CType(e.CommandArgument, Int32)
                        Dim index2 As Int32 = index1 + DataGrid_CurrentPageIndex * DataGrid.PageSize    'MLD 22/3/5 added
                        SelectedIndex = index1
                        .SelectedGridItemIndex = index2                                                 'MLD 22/3/5 modified
                        .SelectedGridItem = GetDataSourceItem(index2)                                   'MLD 22/3/5 modified
                        Try
                            CType(Parent, IBaseUpdatableGridContainer).GridClick(e)
                        Catch ex As Exception
                            RaiseEvent ViewCommand(sender, .SelectedGridItem)
                        End Try
                    End If

                End With
                ' Me.BindData()
                Return True
            End If
        End Function

        Public Function GetDropDownListSelectedIndex(ByVal rowIndex As Int32, ByVal columnIndex As Int32) As Int32
            If Not rowIndex > Me.DataGrid.Items.Count - 1 Then
                Dim Row As DataGridItem = Me.DataGrid.Items(rowIndex)
                Dim Column As WebControls.TableCell

                If Row.ItemType = ListItemType.Item OrElse Row.ItemType = ListItemType.AlternatingItem Then
                    If Not columnIndex > Row.Cells.Count - 1 Then
                        Column = Row.Cells(columnIndex)
                        If Column.Controls.Count > 0 Then
                            For Each CellControl As Control In Column.Controls
                                'not much use if there is more than one dropdown in a cell!
                                If TypeOf CellControl Is DropDownList Then
                                    Return CType(CellControl, DropDownList).SelectedIndex
                                End If
                            Next
                        End If
                    End If
                End If
            End If
            Return -1
        End Function

        Property CheckedIndex() As Int32
            Get
                Return CType(viewstate("CheckedIndex"), Int32)
            End Get
            Set(ByVal Value As Int32)
                viewstate.Add("CheckedIndex", Value)
            End Set
        End Property

        Public Property CheckBoxState() As SortedList
            Get
                If viewstate("CheckBoxState") Is Nothing Then
                    viewstate.Add("CheckBoxState", New SortedList)
                End If
                Return CType(viewstate("CheckBoxState"), SortedList)
            End Get
            Set(ByVal Value As SortedList)
                viewstate.Add("CheckBoxState", Value)
            End Set
        End Property

        Public Function GetSelectedUniqueIdentifiers() As Int32()
            If CheckBoxState Is Nothing OrElse CheckBoxState.Count = 0 Then Exit Function
            Dim ReturnIds As New ArrayList

            For Each item As DictionaryEntry In CheckBoxState
                If CType(item.Value, Boolean) Then
                    Dim Dataitem As Object = CType(DataSource, Object())(CType(item.Key, Int32))
                    ReturnIds.Add(CType(Dataitem.GetType.GetProperty(UniqueIdentifier).GetValue(Dataitem, Nothing), Int32))
                End If
            Next

            Return CType(ReturnIds.ToArray(GetType(Int32)), Int32())
        End Function


        Public Function GetSelectedObjects() As Object()
            If CheckBoxState Is Nothing OrElse CheckBoxState.Count = 0 Then Exit Function
            Dim ReturnItems As New ArrayList

            For Each item As DictionaryEntry In CheckBoxState
                If CType(item.Value, Boolean) Then
                    Dim Dataitem As Object = CType(DataSource, Object())(CType(item.Key, Int32))
                    ReturnItems.Add(Dataitem)
                End If
            Next

            Return ReturnItems.ToArray()
        End Function

        Public Sub CheckAllItems(ByVal check As Boolean)
            If Not DataSource Is Nothing Then
                CheckBoxState = New SortedList

                Dim i As Int32
                For i = 0 To CType(Me.DataSource, Object()).Length - 1
                    CheckBoxState.Add(i, check)
                Next

            End If
            Me.SetCheckBoxes()
        End Sub

        Public Function AreNoItemsChecked(ByVal dataSource As Object) As Boolean
            If CheckBoxState Is Nothing OrElse CheckBoxState.Count = 0 Then Return True
            If DataGrid Is Nothing Then Me.InitialiseBaseGrid()
            For Each row As DataGridItem In DataGrid.Items

                For Each cell As TableCell In row.Cells

                    Try
                        If CType(cell.Controls(0), CheckBox).Checked Then
                            Return False
                        End If
                    Catch ex As Exception

                    End Try

                Next cell
            Next row
            Return True
        End Function

        Public Function AreAllItemsChecked(ByVal dataSource As Object) As Boolean
            For Each row As DataGridItem In DataGrid.Items

                For Each cell As TableCell In row.Cells

                    Try
                        If Not CType(cell.Controls(0), CheckBox).Checked Then
                            Return False
                        End If
                    Catch ex As Exception

                    End Try

                Next cell
            Next row
            Return True
        End Function

        Private Sub onCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            onViewCommand(sender, New CommandEventArgs("Checked", CType(sender, CheckBox).Checked))

        End Sub



        Private Sub onViewCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
            If Not HasInterface(Me.NamingContainer.Parent, sender, e) Then
                If Not HasInterface(Me.NamingContainer.Parent.Parent, sender, e) Then
                    HasInterface(Me.NamingContainer.Parent.Parent.Parent, sender, e)
                End If
            End If
        End Sub

        Private Sub AddEventHandlers(ByVal cell As TableCell)
            AddRemoveEventHandlers(cell, True)
        End Sub

        Private Sub RemoveEventHandlers(ByVal cell As TableCell)
            AddRemoveEventHandlers(cell, False)
        End Sub

        Private Sub AddRemoveEventHandlers(ByVal cell As TableCell, ByVal add As Boolean)
            If cell.Controls.Count > 0 Then
                Dim Control As Control = cell.Controls(0)
                If Control.GetType.Equals(GetType(LinkButton)) Then
                    RemoveHandler CType(Control, LinkButton).Command, AddressOf onViewCommand
                    If add Then
                        AddHandler CType(Control, LinkButton).Command, AddressOf onViewCommand
                    End If
                ElseIf Control.GetType.Equals(GetType(CheckBox)) Then
                    RemoveHandler CType(Control, CheckBox).CheckedChanged, AddressOf onCheckedChanged
                    If add Then
                        AddHandler CType(Control, CheckBox).CheckedChanged, AddressOf onCheckedChanged
                    End If
                End If
            End If
        End Sub

        'Public Property ParseCellText() As Boolean
        '    Get
        '        If Not viewstate("ParseCellText") Is Nothing Then Return CType(viewstate("ParseCellText"), Boolean)
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        viewstate.Add("ParseCellText", Value)
        '    End Set
        'End Property

        Public Sub SetCheckBoxes()
            If DataGrid Is Nothing Then InitialiseBaseGrid()
            For Each row As DataGridItem In DataGrid.Items
                For Each cell As TableCell In row.Cells
                    Try
                        CType(cell.Controls(0), CheckBox).EnableViewState = True
                        CType(cell.Controls(0), CheckBox).Checked = CType(CheckBoxState(row.ItemIndex), Boolean)
                    Catch ex As Exception

                    End Try
                Next cell
            Next
        End Sub

        Public Sub ChangeDateFormat()
            If Not DataGrid Is Nothing AndAlso Not DataGrid.Items Is Nothing Then
                For Each row As DataGridItem In DataGrid.Items
                    For Each cell As TableCell In row.Cells
                        'Dim NewValue As String = DataHelper.GetFormattedText("Boolean", GetType(String), cell.Text)
                        'If NewValue Is Nothing Then
                        '    'it wasn't a boolean
                        '    '...try a date
                        Dim NewValue As String = DataHelper.GetFormattedText("ShortDate", GetType(String), cell.Text)
                        'End If
                        If Not NewValue Is Nothing Then
                            'we have something exceptable so use it
                            cell.Text = NewValue
                        End If


                        'Try
                        '    cell.Text = Date.Parse(cell.Text).ToShortDateString
                        'Catch exDate As Exception
                        'End Try                 
                    Next cell
                Next
            End If
        End Sub

        Public Sub ShowActive()
            For Each row As DataGridItem In DataGrid.Items
                Dim Active As Object = Nothing
                Try
                    Active = CType(DataSource, Object())(row.DataSetIndex).GetType.GetProperty("Active").GetValue(CType(DataSource, Object())(row.DataSetIndex), Nothing)
                Catch

                End Try

                If Not Active Is Nothing AndAlso Not Boolean.Parse(Active.ToString) Then
                    row.ForeColor = Drawing.Color.Gray
                End If
            Next
        End Sub

        '     Private Sub PostBindAlteration()
        'Dim i As Int32
        ' For Each row As DataGridItem In DataGrid.Items


        ' For Each cell As TableCell In row.Cells
        'PN commented out as this doesn't seem to work, this is now done in the BOs
        'Dim NewValue As String = DataHelper.GetFormattedText("Boolean", GetType(String), cell.Text)
        'If NewValue Is Nothing Then
        '    'it wasn't a boolean
        '    '...try a date
        '    NewValue = DataHelper.GetFormattedText("ShortDate", GetType(String), cell.Text)
        'End If
        'If Not NewValue Is Nothing Then
        '    'we have something exceptable so use it
        '    cell.Text = NewValue
        'Else
        '    'all is cool - so leave it a lone
        'End If

        'Try
        '    If Boolean.Parse(cell.Text) Then
        '        cell.Text = DataHelper.GetFormattedText("Boolean", GetType(String), cell.Text)
        '        cell.Text = "Yes"
        '    Else
        '        cell.Text = "No"
        '    End If
        'Catch exBoolean As Exception
        '    Try
        '        cell.Text = Date.Parse(cell.Text).ToShortDateString
        '    Catch exDate As Exception
        '    End Try
        'End Try

        'Try
        '    CType(cell.Controls(0), CheckBox).EnableViewState = True
        '    CType(cell.Controls(0), CheckBox).Checked = CType(CheckBoxState(row.ItemIndex), Boolean)
        'Catch ex As Exception

        'End Try

        ' Next cell
        ' Next row

        'Dim i As Int32
        'Dim ControlVisible As Boolean

        'For Each column As WebControls.DataGridColumn In DataGrid.Columns
        '    ControlVisible = False
        '    If TypeOf column Is uk.gov.defra.PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn Then
        '        For Each row As DataGridItem In DataGrid.Items
        '            If row.Cells(i).Controls.Count > 0 Then
        '                ControlVisible = row.Cells(i).Controls(0).Visible
        '            Else
        '                ControlVisible = True
        '            End If
        '            If ControlVisible Then Exit For
        '        Next
        '    Else
        '        ControlVisible = True
        '    End If
        '    column.Visible = ControlVisible
        '    i += 1
        'Next
        ' End Sub

        Public Sub HideColumns()
            Dim i As Int32
            Dim ControlVisible As Boolean

            If Not DataGrid Is Nothing Then
                For Each column As WebControls.DataGridColumn In DataGrid.Columns
                    ControlVisible = False
                    If TypeOf column Is uk.gov.defra.PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn Then
                        For Each row As DataGridItem In DataGrid.Items
                            If row.Cells(i).Controls.Count > 0 Then
                                ControlVisible = row.Cells(i).Controls(0).Visible
                            Else
                                ControlVisible = True
                            End If
                            If ControlVisible Then Exit For
                        Next
                    Else
                        ControlVisible = True
                    End If
                    column.Visible = ControlVisible
                    i += 1
                Next
            End If
        End Sub

        Public Overrides Sub DataBind()
            If Not mDataGridColumnCollection Is Nothing Then
                For Each column As Object In mDataGridColumnCollection
                    If TypeOf column Is BaseGrid.PhoenixGridTemplateColumn Then
                        Dim templateColumn As BaseGrid.PhoenixGridTemplateColumn = CType(column, BaseGrid.PhoenixGridTemplateColumn)
                        Dim template As BaseGrid.TemplateColumnBase = CType(templateColumn.ItemTemplate, BaseGrid.TemplateColumnBase)
                        Try
                            template.Row = 0
                        Catch ex As Exception

                        End Try

                    End If
                Next
            End If
            MyBase.DataBind()
        End Sub

        Private Sub OnShowControl(ByRef control As System.Web.UI.Control, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ShowControl
            Try
                UpdatableGrid.ShowControl(control, e)
            Catch
                RaiseEvent ShowControl(control, e)
            End Try
        End Sub

        Private Sub OnSortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGrid.SortCommand
            RaiseEvent SortCommand(source, e)
        End Sub

        Private Sub OnSetRowForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.SetRowForeColour
            RaiseEvent SetRowForeColour(e)
        End Sub

        Private Sub OnSetCellForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal cellIndex As Int32) Handles DataGrid.SetCellForeColour
            RaiseEvent SetCellForeColour(e, cellIndex)

        End Sub

        Private Property DataGrid_CurrentPageIndex() As Int32   'MLD 22/3/5 completed
            Get
                Return CType(ViewState("CurrentPageIndex"), Int32)
            End Get
            Set(ByVal Value As Int32)
                ViewState("CurrentPageIndex") = Value
            End Set
        End Property

        Private Sub DataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid.PageIndexChanged
            DataGrid.CurrentPageIndex = e.NewPageIndex
            DataGrid_CurrentPageIndex = e.NewPageIndex          'MLD 22/3/5 added
            BindGrid()
        End Sub


    End Class

    <Serializable()> _
      Public Class BaseGrid
        Inherits WebControls.DataGrid

        Public Property UniqueIdentifier(ByVal state As StateBag) As String
            Get
                If state("UniqueIdentifier") Is Nothing Then
                    state.Add("UniqueIdentifier", Nothing)
                End If
                Dim uID As Object = state("UniqueIdentifier")
                If uID Is Nothing Then
                    Return Nothing
                Else
                    Return uID.ToString
                End If
            End Get
            Set(ByVal Value As String)
                If state("UniqueIdentifier") Is Nothing Then
                    state.Add("UniqueIdentifier", Value)
                Else
                    state("UniqueIdentifier") = Value
                End If
            End Set
        End Property

        Public Property UniqueIdentifier() As String
            Get
                Return UniqueIdentifier(viewstate)
            End Get
            Set(ByVal Value As String)
                UniqueIdentifier(viewstate) = Value
            End Set
        End Property

        Public Event ViewCommand(ByVal source As System.Web.UI.WebControls.LinkButton, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Public Event ShowControl(ByRef control As Control, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Public Event SetRowForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Public Event SetCellForeColour(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal cellIndex As Int32)

        'Public Event EditCommand(ByVal source As System.Web.UI.WebControls.LinkButton, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        'Public Event DeleteCommand(ByVal source As System.Web.UI.WebControls.LinkButton, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        'Public Event ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)


        Public Overloads Sub AddColumn(ByVal column As PhoenixGridTemplateColumn)
            Columns.Add(column)
        End Sub

        Public Overloads Sub AddColumn(ByVal column As PhoenixGridBoundColumn)
            Columns.Add(column)
        End Sub

        <Serializable()> _
        Public MustInherit Class TemplateColumnBase         'MLD added 2/9/4
            Implements System.Web.UI.ITemplate

            Private mRow As Int32 = 0

            Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
                InstantiateInImpl(container)
                mRow += 1
            End Sub

            Protected MustOverride Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)

            Public Event AddingControl(ByRef controlToAdd As Control, ByVal column As ITemplate, ByVal row As Int32)

            Protected Overridable Sub OnAddingControl(ByRef ControlToAdd As Control, ByVal row As Int32)
                RaiseEvent AddingControl(ControlToAdd, Me, row)
            End Sub

            Public Property Row() As Int32
                Get
                    Return mRow
                End Get
                Set(ByVal Value As Int32)
                    mRow = Value
                End Set
            End Property
        End Class

        <Serializable()> _
        Public Class DropdownListTemplateColumn
            Inherits TemplateColumnBase

            'Protected WithEvents LinkButton As LinkButton
            Private mCommandName As String

            Public Sub New(ByVal commandName As String)
                MyBase.New()
                mCommandName = commandName
            End Sub

            Protected Overrides Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)
                Dim ControlToAdd As New DropDownList
                With ControlToAdd
                    '.DataSource()
                    '.DataTextField = "Description"
                    '.DataValueField = "Id"
                    '.DataBind()
                End With
                OnAddingControl(CType(ControlToAdd, Control), Row)
                container.Controls.Add(CType(ControlToAdd, Control))
            End Sub

        End Class


        <Serializable()> _
        Public Class LinkButtonTemplateColumn
            Inherits TemplateColumnBase

            'Protected WithEvents LinkButton As LinkButton
            Public CommandName As String
            Public ToolTip As String

            Public Sub New(ByVal commandName As String, ByVal toolTip As String)
                MyBase.New()
                Me.CommandName = commandName
                If Not toolTip = "" Then Me.ToolTip = toolTip
            End Sub

            Protected Overrides Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)
                Dim ControlToAdd As New LinkButton
                With ControlToAdd
                    .CausesValidation = False
                    .CommandName = CommandName
                    .ToolTip = Me.ToolTip
                    Select Case CommandName
                        Case "Delete"
                            .ID = "cmdDelete"
                    End Select
                End With
                OnAddingControl(CType(ControlToAdd, Control), Row)
                container.Controls.Add(CType(ControlToAdd, Control))
            End Sub

        End Class

        <Serializable()> _
        Public Class CheckboxTemplateColumn
            Inherits TemplateColumnBase

            'Protected WithEvents LinkButton As LinkButton
            Private mCommandName As String

            Public PostBack As Boolean

            Public Sub New(ByVal commandName As String)
                MyBase.New()
                mCommandName = commandName
            End Sub

            Protected Overrides Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)
                Dim ControlToAdd As New CheckBox
                With ControlToAdd
                    .Text = mCommandName
                    .AutoPostBack = PostBack
                End With
                OnAddingControl(CType(ControlToAdd, Control), Row)
                container.Controls.Add(CType(ControlToAdd, Control))
            End Sub

        End Class

        <Serializable()> _
        Public Class HyperLinkTemplateColumn
            Inherits TemplateColumnBase

            Private mText As String
            Private mNavigateUrl As String
            Private mTarget As String

            Public Sub New(ByVal text As String, ByVal url As String, ByVal blankTarget As Boolean)
                MyBase.New()
                mText = text
                mNavigateUrl = url '& "_NavigateUrl"
                mTarget = ""
                If blankTarget Then mTarget = "_blank"
            End Sub

            Protected Overrides Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)
                Dim ControlToAdd As New HyperLink
                With ControlToAdd
                    .Text = mText
                    .NavigateUrl = mNavigateUrl
                    .Target = mTarget
                End With
                OnAddingControl(CType(ControlToAdd, Control), Row)
                container.Controls.Add(CType(ControlToAdd, Control))
            End Sub

        End Class

        <Serializable()> _
          Public Class TextBoxTemplateColumn
            Inherits TemplateColumnBase

            'Protected WithEvents LinkButton As LinkButton
            Private mCommandName As String

            Public PostBack As Boolean

            Public Sub New(ByVal commandName As String)
                MyBase.New()
                mCommandName = commandName
            End Sub

            Protected Overrides Sub InstantiateInImpl(ByVal container As System.Web.UI.Control)
                Dim ControlToAdd As New TextBox
                With ControlToAdd
                    .Text = mCommandName
                    .AutoPostBack = PostBack
                End With
                OnAddingControl(CType(ControlToAdd, Control), Row)
                container.Controls.Add(CType(ControlToAdd, Control))
            End Sub

        End Class

        <Serializable()> _
      Class PhoenixGridTemplateColumnWithCombo
            Inherits PhoenixGridTemplateColumn

            Public Sub New(ByVal datasource As Object, ByVal DataTextField As String, ByVal DataValueField As String)
                MyBase.New(datasource, True)
                Me.DataTextField = DataTextField
                Me.DataValueField = DataValueField
            End Sub

            Public Property DataTextField() As String
                Get
                    Return mDataTextField
                End Get
                Set(ByVal Value As String)
                    mDataTextField = Value
                End Set
            End Property
            Private mDataTextField As String

            Public Property DataValueField() As String
                Get
                    Return mDataValueField
                End Get
                Set(ByVal Value As String)
                    mDataValueField = Value
                End Set
            End Property
            Private mDataValueField As String
        End Class

        <Serializable()> _
        Class PhoenixGridTemplateColumn
            Inherits System.Web.UI.WebControls.TemplateColumn

            Public Property DataField() As Object
                Get
                    Return mDataField
                End Get
                Set(ByVal Value As Object)
                    mDataField = Value
                End Set
            End Property
            Private mDataField As Object

            Public Property Bind() As Boolean
                Get
                    Return mBind
                End Get
                Set(ByVal Value As Boolean)
                    mBind = Value
                End Set
            End Property
            Private mBind As Boolean

            Public Overrides Property ItemTemplate() As System.Web.UI.ITemplate
                Get
                    Return MyBase.ItemTemplate
                End Get
                Set(ByVal Value As System.Web.UI.ITemplate)
                    MyBase.ItemTemplate = Value
                End Set
            End Property

            Public Sub New(ByVal dataField As Object, ByVal bind As Boolean)
                Me.DataField = dataField
                Me.Bind = bind
            End Sub
        End Class

        <Serializable()> _
        Class PhoenixGridBoundColumn
            Inherits System.Web.UI.WebControls.BoundColumn
            Public MDataField As Object
            Public Sub New(ByVal dataField As Object, ByVal headerText As String)
                MDataField = dataField
                Me.HeaderText = headerText
            End Sub
        End Class

        'Private Sub BaseGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles MyBase.ItemDataBound
        '    If e.Item.ItemType = ListItemType.Item Then
        '        CType(CType(Columns(0), PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn).ItemTemplate, PhoenixControls.Controls.BaseGrid.LinkButtonTemplateColumn).Text = _

        '    End If

        'End Sub

        Protected Overloads Overrides Function CreateItem(ByVal itemIndex As Integer, ByVal dataSourceIndex As Integer, _
            ByVal itemType As System.Web.UI.WebControls.ListItemType) As System.Web.UI.WebControls.DataGridItem

            Return MyBase.CreateItem(itemIndex, dataSourceIndex, itemType)
        End Function


        Protected Overrides Sub OnItemDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            'tooltip
            MyBase.OnItemDataBound(e)
            If Not e.Item.DataItem Is Nothing AndAlso (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim i As Int32
                RaiseEvent SetRowForeColour(e)
                For i = 0 To e.Item.Cells.Count - 1
                    Try
                        RaiseEvent ShowControl(e.Item.Cells(i).Controls(0), e)
                    Catch ex As Exception

                    End Try
                    If Columns(i).GetType.Equals(GetType(PhoenixGridTemplateColumn)) OrElse Columns(i).GetType.Equals(GetType(PhoenixGridBoundColumn)) Then
                        Try

                            Dim TextValue As Object
                            If Columns(i).GetType.Equals(GetType(PhoenixGridBoundColumn)) OrElse CType(Columns(i), PhoenixGridTemplateColumn).Bind Then
                                Try
                                    TextValue = PhoenixControls.Utils.GetPropertyEx(e.Item.DataItem, CType(Columns(i), PhoenixGridBoundColumn).MDataField.ToString)
                                Catch ex As Exception
                                    Try
                                        TextValue = PhoenixControls.Utils.GetPropertyEx(e.Item.DataItem, CType(Columns(i), PhoenixGridTemplateColumn).DataField.ToString)

                                    Catch exx As Exception
                                        'Stop
                                    End Try
                                End Try
                            Else
                                TextValue = CType(Columns(i), PhoenixGridTemplateColumn).DataField
                            End If

                            If Not TextValue Is Nothing Then
                                If e.Item.Cells(i).Controls.Count > 0 Then
                                    e.Item.Cells(i).Controls(0).GetType.GetProperty("Text").SetValue(e.Item.Cells(i).Controls(0), TextValue.ToString, Nothing)

                                    If TypeOf e.Item.Cells(i).Controls(0) Is LinkButton Then
                                        With CType(e.Item.Cells(i).Controls(0), LinkButton)
                                            .CommandArgument = e.Item.DataSetIndex.ToString
                                            .Attributes.Add("CommandName", CType(CType(CType(CType((Columns(i)), System.Web.UI.WebControls.DataGridColumn), uk.gov.defra.PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn).ItemTemplate, System.Web.UI.ITemplate), uk.gov.defra.PhoenixControls.Controls.BaseGrid.LinkButtonTemplateColumn).CommandName)

                                            Dim toolTip As String = ""
                                            Try
                                                .ToolTip = PhoenixControls.Utils.GetPropertyEx(e.Item.DataItem, CType(Columns(i), PhoenixGridTemplateColumn).DataField.ToString & "_ToolTip").ToString
                                            Catch ex As Exception
                                                .ToolTip = ""
                                            End Try

                                        End With
                                    ElseIf TypeOf e.Item.Cells(i).Controls(0) Is HyperLink Then
                                        CType(e.Item.Cells(i).Controls(0), HyperLink).NavigateUrl = PhoenixControls.Utils.GetPropertyEx(e.Item.DataItem, CType(Columns(i), PhoenixGridTemplateColumn).DataField.ToString & "_NavigateUrl").ToString
                                    End If
                                Else
                                    e.Item.Cells(i).GetType.GetProperty("Text").SetValue(e.Item.Cells(i), TextValue.ToString, Nothing)
                                    Try
                                        e.Item.Cells(i).ToolTip = PhoenixControls.Utils.GetPropertyEx(e.Item.DataItem, CType(Columns(i), PhoenixGridTemplateColumn).DataField.ToString & "_ToolTip").ToString
                                    Catch ex As Exception
                                        e.Item.Cells(i).ToolTip = ""
                                    End Try
                                End If

                            Else

                            End If
                        Catch ex As System.Exception
                            'Stop
                        End Try

                    ElseIf Columns(i).GetType.Equals(GetType(PhoenixGridTemplateColumnWithCombo)) Then
                        If e.Item.Cells(i).Controls.Count > 0 Then
                            If TypeOf e.Item.Cells(i).Controls(0) Is DropDownList Then
                                With CType(e.Item.Cells(i).Controls(0), DropDownList)
                                    .DataSource = CType(Columns(i), PhoenixGridTemplateColumnWithCombo).DataField
                                    .DataTextField = CType(Columns(i), PhoenixGridTemplateColumnWithCombo).DataTextField
                                    .DataValueField = CType(Columns(i), PhoenixGridTemplateColumnWithCombo).DataValueField
                                    .DataBind()
                                End With
                            End If
                        End If
                    End If

                    RaiseEvent SetCellForeColour(e, i)

                Next i
            End If
        End Sub

        Public Sub New(ByVal showHeader As Boolean)
            MyBase.New()

            Me.HeaderStyle.Font.Bold = True
            Me.ShowHeader = showHeader
            Me.Width = New Unit(100, UnitType.Percentage)
            EnableViewState = False
        End Sub

        Private Sub Page_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles MyBase.ItemCommand
            Try
                Select Case e.CommandName
                    Case "View" : RaiseEvent ViewCommand(CType(source, WebControls.LinkButton), e)
                        'Case "Edit" : RaiseEvent EditCommand(CType(source, WebControls.LinkButton), e)
                        'Case "Delete" : RaiseEvent DeleteCommand(CType(source, WebControls.LinkButton), e)
                        'Case Else : RaiseEvent ItemCommand(source, e)

                End Select
            Catch ex As Exception

            End Try

        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            '  Me.EnableViewState = True
        End Sub

        'Private Sub Page_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles MyBase.SortCommand
        '    RaiseEvent SortCommand(source, e)
        '    'ctype(ctype(source,uk.gov.defra.PhoenixControls.Controls.BaseGrid).Columns(0),uk.gov.defra.PhoenixControls.Controls.BaseGrid.PhoenixGridTemplateColumn).SortExpression
        'End Sub
    End Class

    <Serializable()> _
  Public Class GridPager
        Inherits uk.gov.defra.CommonCode.DataGridPager

        Public Sub New(ByVal grid As WebControls.DataGrid)
            MyBase.New()
        End Sub

    End Class
End Namespace