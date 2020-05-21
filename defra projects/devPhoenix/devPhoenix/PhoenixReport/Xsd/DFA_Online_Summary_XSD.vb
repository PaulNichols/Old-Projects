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
Public Class DFA_Online_SummaryData
    Inherits DataSet
    
    Private tableBODFA_Online_Summary As BODFA_Online_SummaryDataTable
    
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
            If (Not (ds.Tables("BODFA_Online_Summary")) Is Nothing) Then
                Me.Tables.Add(New BODFA_Online_SummaryDataTable(ds.Tables("BODFA_Online_Summary")))
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
    Public ReadOnly Property BODFA_Online_Summary As BODFA_Online_SummaryDataTable
        Get
            Return Me.tableBODFA_Online_Summary
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As DFA_Online_SummaryData = CType(MyBase.Clone,DFA_Online_SummaryData)
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
        If (Not (ds.Tables("BODFA_Online_Summary")) Is Nothing) Then
            Me.Tables.Add(New BODFA_Online_SummaryDataTable(ds.Tables("BODFA_Online_Summary")))
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
        Me.tableBODFA_Online_Summary = CType(Me.Tables("BODFA_Online_Summary"),BODFA_Online_SummaryDataTable)
        If (Not (Me.tableBODFA_Online_Summary) Is Nothing) Then
            Me.tableBODFA_Online_Summary.InitVars
        End If
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "DFA_Online_SummaryData"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/DFA_Online_SummaryData.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableBODFA_Online_Summary = New BODFA_Online_SummaryDataTable
        Me.Tables.Add(Me.tableBODFA_Online_Summary)
    End Sub
    
    Private Function ShouldSerializeBODFA_Online_Summary() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub BODFA_Online_SummaryRowChangeEventHandler(ByVal sender As Object, ByVal e As BODFA_Online_SummaryRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BODFA_Online_SummaryDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnRowId As DataColumn
        
        Private columnTotalPaymentsNum As DataColumn
        
        Private columnTotalPaymentsAmt As DataColumn
        
        Private columnTotalRefundsNum As DataColumn
        
        Private columnTotalRefundsAmt As DataColumn
        
        Friend Sub New()
            MyBase.New("BODFA_Online_Summary")
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
        
        Friend ReadOnly Property RowIdColumn As DataColumn
            Get
                Return Me.columnRowId
            End Get
        End Property
        
        Friend ReadOnly Property TotalPaymentsNumColumn As DataColumn
            Get
                Return Me.columnTotalPaymentsNum
            End Get
        End Property
        
        Friend ReadOnly Property TotalPaymentsAmtColumn As DataColumn
            Get
                Return Me.columnTotalPaymentsAmt
            End Get
        End Property
        
        Friend ReadOnly Property TotalRefundsNumColumn As DataColumn
            Get
                Return Me.columnTotalRefundsNum
            End Get
        End Property
        
        Friend ReadOnly Property TotalRefundsAmtColumn As DataColumn
            Get
                Return Me.columnTotalRefundsAmt
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As BODFA_Online_SummaryRow
            Get
                Return CType(Me.Rows(index),BODFA_Online_SummaryRow)
            End Get
        End Property
        
        Public Event BODFA_Online_SummaryRowChanged As BODFA_Online_SummaryRowChangeEventHandler
        
        Public Event BODFA_Online_SummaryRowChanging As BODFA_Online_SummaryRowChangeEventHandler
        
        Public Event BODFA_Online_SummaryRowDeleted As BODFA_Online_SummaryRowChangeEventHandler
        
        Public Event BODFA_Online_SummaryRowDeleting As BODFA_Online_SummaryRowChangeEventHandler
        
        Public Overloads Sub AddBODFA_Online_SummaryRow(ByVal row As BODFA_Online_SummaryRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddBODFA_Online_SummaryRow(ByVal RowId As Long, ByVal TotalPaymentsNum As String, ByVal TotalPaymentsAmt As String, ByVal TotalRefundsNum As String, ByVal TotalRefundsAmt As String) As BODFA_Online_SummaryRow
            Dim rowBODFA_Online_SummaryRow As BODFA_Online_SummaryRow = CType(Me.NewRow,BODFA_Online_SummaryRow)
            rowBODFA_Online_SummaryRow.ItemArray = New Object() {RowId, TotalPaymentsNum, TotalPaymentsAmt, TotalRefundsNum, TotalRefundsAmt}
            Me.Rows.Add(rowBODFA_Online_SummaryRow)
            Return rowBODFA_Online_SummaryRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As BODFA_Online_SummaryDataTable = CType(MyBase.Clone,BODFA_Online_SummaryDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New BODFA_Online_SummaryDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnRowId = Me.Columns("RowId")
            Me.columnTotalPaymentsNum = Me.Columns("TotalPaymentsNum")
            Me.columnTotalPaymentsAmt = Me.Columns("TotalPaymentsAmt")
            Me.columnTotalRefundsNum = Me.Columns("TotalRefundsNum")
            Me.columnTotalRefundsAmt = Me.Columns("TotalRefundsAmt")
        End Sub
        
        Private Sub InitClass()
            Me.columnRowId = New DataColumn("RowId", GetType(System.Int64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnRowId)
            Me.columnTotalPaymentsNum = New DataColumn("TotalPaymentsNum", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTotalPaymentsNum)
            Me.columnTotalPaymentsAmt = New DataColumn("TotalPaymentsAmt", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTotalPaymentsAmt)
            Me.columnTotalRefundsNum = New DataColumn("TotalRefundsNum", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTotalRefundsNum)
            Me.columnTotalRefundsAmt = New DataColumn("TotalRefundsAmt", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTotalRefundsAmt)
        End Sub
        
        Public Function NewBODFA_Online_SummaryRow() As BODFA_Online_SummaryRow
            Return CType(Me.NewRow,BODFA_Online_SummaryRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New BODFA_Online_SummaryRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(BODFA_Online_SummaryRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.BODFA_Online_SummaryRowChangedEvent) Is Nothing) Then
                RaiseEvent BODFA_Online_SummaryRowChanged(Me, New BODFA_Online_SummaryRowChangeEvent(CType(e.Row,BODFA_Online_SummaryRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.BODFA_Online_SummaryRowChangingEvent) Is Nothing) Then
                RaiseEvent BODFA_Online_SummaryRowChanging(Me, New BODFA_Online_SummaryRowChangeEvent(CType(e.Row,BODFA_Online_SummaryRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.BODFA_Online_SummaryRowDeletedEvent) Is Nothing) Then
                RaiseEvent BODFA_Online_SummaryRowDeleted(Me, New BODFA_Online_SummaryRowChangeEvent(CType(e.Row,BODFA_Online_SummaryRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.BODFA_Online_SummaryRowDeletingEvent) Is Nothing) Then
                RaiseEvent BODFA_Online_SummaryRowDeleting(Me, New BODFA_Online_SummaryRowChangeEvent(CType(e.Row,BODFA_Online_SummaryRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveBODFA_Online_SummaryRow(ByVal row As BODFA_Online_SummaryRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BODFA_Online_SummaryRow
        Inherits DataRow
        
        Private tableBODFA_Online_Summary As BODFA_Online_SummaryDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableBODFA_Online_Summary = CType(Me.Table,BODFA_Online_SummaryDataTable)
        End Sub
        
        Public Property RowId As Long
            Get
                Try 
                    Return CType(Me(Me.tableBODFA_Online_Summary.RowIdColumn),Long)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBODFA_Online_Summary.RowIdColumn) = value
            End Set
        End Property
        
        Public Property TotalPaymentsNum As String
            Get
                Try 
                    Return CType(Me(Me.tableBODFA_Online_Summary.TotalPaymentsNumColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBODFA_Online_Summary.TotalPaymentsNumColumn) = value
            End Set
        End Property
        
        Public Property TotalPaymentsAmt As String
            Get
                Try 
                    Return CType(Me(Me.tableBODFA_Online_Summary.TotalPaymentsAmtColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBODFA_Online_Summary.TotalPaymentsAmtColumn) = value
            End Set
        End Property
        
        Public Property TotalRefundsNum As String
            Get
                Try 
                    Return CType(Me(Me.tableBODFA_Online_Summary.TotalRefundsNumColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBODFA_Online_Summary.TotalRefundsNumColumn) = value
            End Set
        End Property
        
        Public Property TotalRefundsAmt As String
            Get
                Try 
                    Return CType(Me(Me.tableBODFA_Online_Summary.TotalRefundsAmtColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBODFA_Online_Summary.TotalRefundsAmtColumn) = value
            End Set
        End Property
        
        Public Function IsRowIdNull() As Boolean
            Return Me.IsNull(Me.tableBODFA_Online_Summary.RowIdColumn)
        End Function
        
        Public Sub SetRowIdNull()
            Me(Me.tableBODFA_Online_Summary.RowIdColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTotalPaymentsNumNull() As Boolean
            Return Me.IsNull(Me.tableBODFA_Online_Summary.TotalPaymentsNumColumn)
        End Function
        
        Public Sub SetTotalPaymentsNumNull()
            Me(Me.tableBODFA_Online_Summary.TotalPaymentsNumColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTotalPaymentsAmtNull() As Boolean
            Return Me.IsNull(Me.tableBODFA_Online_Summary.TotalPaymentsAmtColumn)
        End Function
        
        Public Sub SetTotalPaymentsAmtNull()
            Me(Me.tableBODFA_Online_Summary.TotalPaymentsAmtColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTotalRefundsNumNull() As Boolean
            Return Me.IsNull(Me.tableBODFA_Online_Summary.TotalRefundsNumColumn)
        End Function
        
        Public Sub SetTotalRefundsNumNull()
            Me(Me.tableBODFA_Online_Summary.TotalRefundsNumColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTotalRefundsAmtNull() As Boolean
            Return Me.IsNull(Me.tableBODFA_Online_Summary.TotalRefundsAmtColumn)
        End Function
        
        Public Sub SetTotalRefundsAmtNull()
            Me(Me.tableBODFA_Online_Summary.TotalRefundsAmtColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BODFA_Online_SummaryRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As BODFA_Online_SummaryRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As BODFA_Online_SummaryRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As BODFA_Online_SummaryRow
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
