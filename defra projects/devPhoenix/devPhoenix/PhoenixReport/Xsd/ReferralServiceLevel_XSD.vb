﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.573
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Data
Imports System.Runtime.Serialization
Imports System.Xml


<Serializable(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Diagnostics.DebuggerStepThrough(),  _
 System.ComponentModel.ToolboxItem(true)>  _
Public Class ReferralServiceLevelData
    Inherits DataSet
    
    Private tableBOServiceLevelDetails As BOServiceLevelDetailsDataTable
    
    Private tableBOServiceLevelTypes As BOServiceLevelTypesDataTable
    
    Private relationBOServiceLevelTypesBOServiceLevelDetails As DataRelation
    
    Public Sub New()
        MyBase.New
        Me.InitClass
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New
        Dim strSchema As String = CType(info.GetValue("XmlSchema", GetType(System.String)),String)
        If (Not (strSchema) Is Nothing) Then
            Dim ds As DataSet = New DataSet
            ds.ReadXmlSchema(New XmlTextReader(New System.IO.StringReader(strSchema)))
            If (Not (ds.Tables("BOServiceLevelDetails")) Is Nothing) Then
                Me.Tables.Add(New BOServiceLevelDetailsDataTable(ds.Tables("BOServiceLevelDetails")))
            End If
            If (Not (ds.Tables("BOServiceLevelTypes")) Is Nothing) Then
                Me.Tables.Add(New BOServiceLevelTypesDataTable(ds.Tables("BOServiceLevelTypes")))
            End If
            Me.DataSetName = ds.DataSetName
            Me.Prefix = ds.Prefix
            Me.Namespace = ds.Namespace
            Me.Locale = ds.Locale
            Me.CaseSensitive = ds.CaseSensitive
            Me.EnforceConstraints = ds.EnforceConstraints
            Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
            Me.InitVars
        Else
            Me.InitClass
        End If
        Me.GetSerializationData(info, context)
        Dim schemaChangedHandler As System.ComponentModel.CollectionChangeEventHandler = AddressOf Me.SchemaChanged
        AddHandler Me.Tables.CollectionChanged, schemaChangedHandler
        AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
    End Sub
    
    <System.ComponentModel.Browsable(false),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property BOServiceLevelDetails As BOServiceLevelDetailsDataTable
        Get
            Return Me.tableBOServiceLevelDetails
        End Get
    End Property
    
    <System.ComponentModel.Browsable(false),  _
     System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)>  _
    Public ReadOnly Property BOServiceLevelTypes As BOServiceLevelTypesDataTable
        Get
            Return Me.tableBOServiceLevelTypes
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As ReferralServiceLevelData = CType(MyBase.Clone,ReferralServiceLevelData)
        cln.InitVars
        Return cln
    End Function
    
    Protected Overrides Function ShouldSerializeTables() As Boolean
        Return false
    End Function
    
    Protected Overrides Function ShouldSerializeRelations() As Boolean
        Return false
    End Function
    
    Protected Overrides Sub ReadXmlSerializable(ByVal reader As XmlReader)
        Me.Reset
        Dim ds As DataSet = New DataSet
        ds.ReadXml(reader)
        If (Not (ds.Tables("BOServiceLevelDetails")) Is Nothing) Then
            Me.Tables.Add(New BOServiceLevelDetailsDataTable(ds.Tables("BOServiceLevelDetails")))
        End If
        If (Not (ds.Tables("BOServiceLevelTypes")) Is Nothing) Then
            Me.Tables.Add(New BOServiceLevelTypesDataTable(ds.Tables("BOServiceLevelTypes")))
        End If
        Me.DataSetName = ds.DataSetName
        Me.Prefix = ds.Prefix
        Me.Namespace = ds.Namespace
        Me.Locale = ds.Locale
        Me.CaseSensitive = ds.CaseSensitive
        Me.EnforceConstraints = ds.EnforceConstraints
        Me.Merge(ds, false, System.Data.MissingSchemaAction.Add)
        Me.InitVars
    End Sub
    
    Protected Overrides Function GetSchemaSerializable() As System.Xml.Schema.XmlSchema
        Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        Me.WriteXmlSchema(New XmlTextWriter(stream, Nothing))
        stream.Position = 0
        Return System.Xml.Schema.XmlSchema.Read(New XmlTextReader(stream), Nothing)
    End Function
    
    Friend Sub InitVars()
        Me.tableBOServiceLevelDetails = CType(Me.Tables("BOServiceLevelDetails"),BOServiceLevelDetailsDataTable)
        If (Not (Me.tableBOServiceLevelDetails) Is Nothing) Then
            Me.tableBOServiceLevelDetails.InitVars
        End If
        Me.tableBOServiceLevelTypes = CType(Me.Tables("BOServiceLevelTypes"),BOServiceLevelTypesDataTable)
        If (Not (Me.tableBOServiceLevelTypes) Is Nothing) Then
            Me.tableBOServiceLevelTypes.InitVars
        End If
        Me.relationBOServiceLevelTypesBOServiceLevelDetails = Me.Relations("BOServiceLevelTypesBOServiceLevelDetails")
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "ReferralServiceLevelData"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/ReferralServiceLevelData.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableBOServiceLevelDetails = New BOServiceLevelDetailsDataTable
        Me.Tables.Add(Me.tableBOServiceLevelDetails)
        Me.tableBOServiceLevelTypes = New BOServiceLevelTypesDataTable
        Me.Tables.Add(Me.tableBOServiceLevelTypes)
        Dim fkc As ForeignKeyConstraint
        fkc = New ForeignKeyConstraint("BOServiceLevelTypesBOServiceLevelDetails", New DataColumn() {Me.tableBOServiceLevelTypes.ServiceLevelIdColumn}, New DataColumn() {Me.tableBOServiceLevelDetails.ServiceLevelIdColumn})
        Me.tableBOServiceLevelDetails.Constraints.Add(fkc)
        fkc.AcceptRejectRule = System.Data.AcceptRejectRule.None
        fkc.DeleteRule = System.Data.Rule.Cascade
        fkc.UpdateRule = System.Data.Rule.Cascade
        Me.relationBOServiceLevelTypesBOServiceLevelDetails = New DataRelation("BOServiceLevelTypesBOServiceLevelDetails", New DataColumn() {Me.tableBOServiceLevelTypes.ServiceLevelIdColumn}, New DataColumn() {Me.tableBOServiceLevelDetails.ServiceLevelIdColumn}, false)
        Me.Relations.Add(Me.relationBOServiceLevelTypesBOServiceLevelDetails)
    End Sub
    
    Private Function ShouldSerializeBOServiceLevelDetails() As Boolean
        Return false
    End Function
    
    Private Function ShouldSerializeBOServiceLevelTypes() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub BOServiceLevelDetailsRowChangeEventHandler(ByVal sender As Object, ByVal e As BOServiceLevelDetailsRowChangeEvent)
    
    Public Delegate Sub BOServiceLevelTypesRowChangeEventHandler(ByVal sender As Object, ByVal e As BOServiceLevelTypesRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelDetailsDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnServiceLevelId As DataColumn
        
        Private columnReferralType As DataColumn
        
        Private columnProcessed As DataColumn
        
        Private columnWithinFiveDays As DataColumn
        
        Private columnBetween6and10Days As DataColumn
        
        Private columnOver10Days As DataColumn
        
        Friend Sub New()
            MyBase.New("BOServiceLevelDetails")
            Me.InitClass
        End Sub
        
        Friend Sub New(ByVal table As DataTable)
            MyBase.New(table.TableName)
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
            Me.DisplayExpression = table.DisplayExpression
        End Sub
        
        <System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        Friend ReadOnly Property ServiceLevelIdColumn As DataColumn
            Get
                Return Me.columnServiceLevelId
            End Get
        End Property
        
        Friend ReadOnly Property ReferralTypeColumn As DataColumn
            Get
                Return Me.columnReferralType
            End Get
        End Property
        
        Friend ReadOnly Property ProcessedColumn As DataColumn
            Get
                Return Me.columnProcessed
            End Get
        End Property
        
        Friend ReadOnly Property WithinFiveDaysColumn As DataColumn
            Get
                Return Me.columnWithinFiveDays
            End Get
        End Property
        
        Friend ReadOnly Property Between6and10DaysColumn As DataColumn
            Get
                Return Me.columnBetween6and10Days
            End Get
        End Property
        
        Friend ReadOnly Property Over10DaysColumn As DataColumn
            Get
                Return Me.columnOver10Days
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As BOServiceLevelDetailsRow
            Get
                Return CType(Me.Rows(index),BOServiceLevelDetailsRow)
            End Get
        End Property
        
        Public Event BOServiceLevelDetailsRowChanged As BOServiceLevelDetailsRowChangeEventHandler
        
        Public Event BOServiceLevelDetailsRowChanging As BOServiceLevelDetailsRowChangeEventHandler
        
        Public Event BOServiceLevelDetailsRowDeleted As BOServiceLevelDetailsRowChangeEventHandler
        
        Public Event BOServiceLevelDetailsRowDeleting As BOServiceLevelDetailsRowChangeEventHandler
        
        Public Overloads Sub AddBOServiceLevelDetailsRow(ByVal row As BOServiceLevelDetailsRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddBOServiceLevelDetailsRow(ByVal parentBOServiceLevelTypesRowByBOServiceLevelTypesBOServiceLevelDetails As BOServiceLevelTypesRow, ByVal ReferralType As String, ByVal Processed As String, ByVal WithinFiveDays As String, ByVal Between6and10Days As String, ByVal Over10Days As String) As BOServiceLevelDetailsRow
            Dim rowBOServiceLevelDetailsRow As BOServiceLevelDetailsRow = CType(Me.NewRow,BOServiceLevelDetailsRow)
            rowBOServiceLevelDetailsRow.ItemArray = New Object() {parentBOServiceLevelTypesRowByBOServiceLevelTypesBOServiceLevelDetails(0), ReferralType, Processed, WithinFiveDays, Between6and10Days, Over10Days}
            Me.Rows.Add(rowBOServiceLevelDetailsRow)
            Return rowBOServiceLevelDetailsRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As BOServiceLevelDetailsDataTable = CType(MyBase.Clone,BOServiceLevelDetailsDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New BOServiceLevelDetailsDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnServiceLevelId = Me.Columns("ServiceLevelId")
            Me.columnReferralType = Me.Columns("ReferralType")
            Me.columnProcessed = Me.Columns("Processed")
            Me.columnWithinFiveDays = Me.Columns("WithinFiveDays")
            Me.columnBetween6and10Days = Me.Columns("Between6and10Days")
            Me.columnOver10Days = Me.Columns("Over10Days")
        End Sub
        
        Private Sub InitClass()
            Me.columnServiceLevelId = New DataColumn("ServiceLevelId", GetType(System.Int64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnServiceLevelId)
            Me.columnReferralType = New DataColumn("ReferralType", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnReferralType)
            Me.columnProcessed = New DataColumn("Processed", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnProcessed)
            Me.columnWithinFiveDays = New DataColumn("WithinFiveDays", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnWithinFiveDays)
            Me.columnBetween6and10Days = New DataColumn("Between6and10Days", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnBetween6and10Days)
            Me.columnOver10Days = New DataColumn("Over10Days", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnOver10Days)
        End Sub
        
        Public Function NewBOServiceLevelDetailsRow() As BOServiceLevelDetailsRow
            Return CType(Me.NewRow,BOServiceLevelDetailsRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New BOServiceLevelDetailsRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(BOServiceLevelDetailsRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.BOServiceLevelDetailsRowChangedEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelDetailsRowChanged(Me, New BOServiceLevelDetailsRowChangeEvent(CType(e.Row,BOServiceLevelDetailsRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.BOServiceLevelDetailsRowChangingEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelDetailsRowChanging(Me, New BOServiceLevelDetailsRowChangeEvent(CType(e.Row,BOServiceLevelDetailsRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.BOServiceLevelDetailsRowDeletedEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelDetailsRowDeleted(Me, New BOServiceLevelDetailsRowChangeEvent(CType(e.Row,BOServiceLevelDetailsRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.BOServiceLevelDetailsRowDeletingEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelDetailsRowDeleting(Me, New BOServiceLevelDetailsRowChangeEvent(CType(e.Row,BOServiceLevelDetailsRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveBOServiceLevelDetailsRow(ByVal row As BOServiceLevelDetailsRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelDetailsRow
        Inherits DataRow
        
        Private tableBOServiceLevelDetails As BOServiceLevelDetailsDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableBOServiceLevelDetails = CType(Me.Table,BOServiceLevelDetailsDataTable)
        End Sub
        
        Public Property ServiceLevelId As Long
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.ServiceLevelIdColumn),Long)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.ServiceLevelIdColumn) = value
            End Set
        End Property
        
        Public Property ReferralType As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.ReferralTypeColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.ReferralTypeColumn) = value
            End Set
        End Property
        
        Public Property Processed As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.ProcessedColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.ProcessedColumn) = value
            End Set
        End Property
        
        Public Property WithinFiveDays As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.WithinFiveDaysColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.WithinFiveDaysColumn) = value
            End Set
        End Property
        
        Public Property Between6and10Days As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.Between6and10DaysColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.Between6and10DaysColumn) = value
            End Set
        End Property
        
        Public Property Over10Days As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelDetails.Over10DaysColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelDetails.Over10DaysColumn) = value
            End Set
        End Property
        
        Public Property BOServiceLevelTypesRow As BOServiceLevelTypesRow
            Get
                Return CType(Me.GetParentRow(Me.Table.ParentRelations("BOServiceLevelTypesBOServiceLevelDetails")),BOServiceLevelTypesRow)
            End Get
            Set
                Me.SetParentRow(value, Me.Table.ParentRelations("BOServiceLevelTypesBOServiceLevelDetails"))
            End Set
        End Property
        
        Public Function IsServiceLevelIdNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.ServiceLevelIdColumn)
        End Function
        
        Public Sub SetServiceLevelIdNull()
            Me(Me.tableBOServiceLevelDetails.ServiceLevelIdColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsReferralTypeNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.ReferralTypeColumn)
        End Function
        
        Public Sub SetReferralTypeNull()
            Me(Me.tableBOServiceLevelDetails.ReferralTypeColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsProcessedNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.ProcessedColumn)
        End Function
        
        Public Sub SetProcessedNull()
            Me(Me.tableBOServiceLevelDetails.ProcessedColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsWithinFiveDaysNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.WithinFiveDaysColumn)
        End Function
        
        Public Sub SetWithinFiveDaysNull()
            Me(Me.tableBOServiceLevelDetails.WithinFiveDaysColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsBetween6and10DaysNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.Between6and10DaysColumn)
        End Function
        
        Public Sub SetBetween6and10DaysNull()
            Me(Me.tableBOServiceLevelDetails.Between6and10DaysColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsOver10DaysNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelDetails.Over10DaysColumn)
        End Function
        
        Public Sub SetOver10DaysNull()
            Me(Me.tableBOServiceLevelDetails.Over10DaysColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelDetailsRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As BOServiceLevelDetailsRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As BOServiceLevelDetailsRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As BOServiceLevelDetailsRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        Public ReadOnly Property Action As DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelTypesDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnServiceLevelId As DataColumn
        
        Private columnDescription As DataColumn
        
        Private columnThrowNewPage As DataColumn
        
        Friend Sub New()
            MyBase.New("BOServiceLevelTypes")
            Me.InitClass
        End Sub
        
        Friend Sub New(ByVal table As DataTable)
            MyBase.New(table.TableName)
            If (table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                Me.CaseSensitive = table.CaseSensitive
            End If
            If (table.Locale.ToString <> table.DataSet.Locale.ToString) Then
                Me.Locale = table.Locale
            End If
            If (table.Namespace <> table.DataSet.Namespace) Then
                Me.Namespace = table.Namespace
            End If
            Me.Prefix = table.Prefix
            Me.MinimumCapacity = table.MinimumCapacity
            Me.DisplayExpression = table.DisplayExpression
        End Sub
        
        <System.ComponentModel.Browsable(false)>  _
        Public ReadOnly Property Count As Integer
            Get
                Return Me.Rows.Count
            End Get
        End Property
        
        Friend ReadOnly Property ServiceLevelIdColumn As DataColumn
            Get
                Return Me.columnServiceLevelId
            End Get
        End Property
        
        Friend ReadOnly Property DescriptionColumn As DataColumn
            Get
                Return Me.columnDescription
            End Get
        End Property
        
        Friend ReadOnly Property ThrowNewPageColumn As DataColumn
            Get
                Return Me.columnThrowNewPage
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As BOServiceLevelTypesRow
            Get
                Return CType(Me.Rows(index),BOServiceLevelTypesRow)
            End Get
        End Property
        
        Public Event BOServiceLevelTypesRowChanged As BOServiceLevelTypesRowChangeEventHandler
        
        Public Event BOServiceLevelTypesRowChanging As BOServiceLevelTypesRowChangeEventHandler
        
        Public Event BOServiceLevelTypesRowDeleted As BOServiceLevelTypesRowChangeEventHandler
        
        Public Event BOServiceLevelTypesRowDeleting As BOServiceLevelTypesRowChangeEventHandler
        
        Public Overloads Sub AddBOServiceLevelTypesRow(ByVal row As BOServiceLevelTypesRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddBOServiceLevelTypesRow(ByVal ServiceLevelId As Long, ByVal Description As String, ByVal ThrowNewPage As Boolean) As BOServiceLevelTypesRow
            Dim rowBOServiceLevelTypesRow As BOServiceLevelTypesRow = CType(Me.NewRow,BOServiceLevelTypesRow)
            rowBOServiceLevelTypesRow.ItemArray = New Object() {ServiceLevelId, Description, ThrowNewPage}
            Me.Rows.Add(rowBOServiceLevelTypesRow)
            Return rowBOServiceLevelTypesRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As BOServiceLevelTypesDataTable = CType(MyBase.Clone,BOServiceLevelTypesDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New BOServiceLevelTypesDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnServiceLevelId = Me.Columns("ServiceLevelId")
            Me.columnDescription = Me.Columns("Description")
            Me.columnThrowNewPage = Me.Columns("ThrowNewPage")
        End Sub
        
        Private Sub InitClass()
            Me.columnServiceLevelId = New DataColumn("ServiceLevelId", GetType(System.Int64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnServiceLevelId)
            Me.columnDescription = New DataColumn("Description", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnDescription)
            Me.columnThrowNewPage = New DataColumn("ThrowNewPage", GetType(System.Boolean), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnThrowNewPage)
            Me.Constraints.Add(New UniqueConstraint("key1", New DataColumn() {Me.columnServiceLevelId}, false))
            Me.columnServiceLevelId.AllowDBNull = false
            Me.columnServiceLevelId.Unique = true
        End Sub
        
        Public Function NewBOServiceLevelTypesRow() As BOServiceLevelTypesRow
            Return CType(Me.NewRow,BOServiceLevelTypesRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New BOServiceLevelTypesRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(BOServiceLevelTypesRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.BOServiceLevelTypesRowChangedEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelTypesRowChanged(Me, New BOServiceLevelTypesRowChangeEvent(CType(e.Row,BOServiceLevelTypesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.BOServiceLevelTypesRowChangingEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelTypesRowChanging(Me, New BOServiceLevelTypesRowChangeEvent(CType(e.Row,BOServiceLevelTypesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.BOServiceLevelTypesRowDeletedEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelTypesRowDeleted(Me, New BOServiceLevelTypesRowChangeEvent(CType(e.Row,BOServiceLevelTypesRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.BOServiceLevelTypesRowDeletingEvent) Is Nothing) Then
                RaiseEvent BOServiceLevelTypesRowDeleting(Me, New BOServiceLevelTypesRowChangeEvent(CType(e.Row,BOServiceLevelTypesRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveBOServiceLevelTypesRow(ByVal row As BOServiceLevelTypesRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelTypesRow
        Inherits DataRow
        
        Private tableBOServiceLevelTypes As BOServiceLevelTypesDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableBOServiceLevelTypes = CType(Me.Table,BOServiceLevelTypesDataTable)
        End Sub
        
        Public Property ServiceLevelId As Long
            Get
                Return CType(Me(Me.tableBOServiceLevelTypes.ServiceLevelIdColumn),Long)
            End Get
            Set
                Me(Me.tableBOServiceLevelTypes.ServiceLevelIdColumn) = value
            End Set
        End Property
        
        Public Property Description As String
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelTypes.DescriptionColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelTypes.DescriptionColumn) = value
            End Set
        End Property
        
        Public Property ThrowNewPage As Boolean
            Get
                Try 
                    Return CType(Me(Me.tableBOServiceLevelTypes.ThrowNewPageColumn),Boolean)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOServiceLevelTypes.ThrowNewPageColumn) = value
            End Set
        End Property
        
        Public Function IsDescriptionNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelTypes.DescriptionColumn)
        End Function
        
        Public Sub SetDescriptionNull()
            Me(Me.tableBOServiceLevelTypes.DescriptionColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsThrowNewPageNull() As Boolean
            Return Me.IsNull(Me.tableBOServiceLevelTypes.ThrowNewPageColumn)
        End Function
        
        Public Sub SetThrowNewPageNull()
            Me(Me.tableBOServiceLevelTypes.ThrowNewPageColumn) = System.Convert.DBNull
        End Sub
        
        Public Function GetBOServiceLevelDetailsRows() As BOServiceLevelDetailsRow()
            Return CType(Me.GetChildRows(Me.Table.ChildRelations("BOServiceLevelTypesBOServiceLevelDetails")),BOServiceLevelDetailsRow())
        End Function
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOServiceLevelTypesRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As BOServiceLevelTypesRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As BOServiceLevelTypesRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As BOServiceLevelTypesRow
            Get
                Return Me.eventRow
            End Get
        End Property
        
        Public ReadOnly Property Action As DataRowAction
            Get
                Return Me.eventAction
            End Get
        End Property
    End Class
End Class
