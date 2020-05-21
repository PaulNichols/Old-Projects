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
Public Class SpeciesTradePatternData
    Inherits DataSet
    
    Private tableBOSpeciesTradePattern As BOSpeciesTradePatternDataTable
    
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
            If (Not (ds.Tables("BOSpeciesTradePattern")) Is Nothing) Then
                Me.Tables.Add(New BOSpeciesTradePatternDataTable(ds.Tables("BOSpeciesTradePattern")))
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
    Public ReadOnly Property BOSpeciesTradePattern As BOSpeciesTradePatternDataTable
        Get
            Return Me.tableBOSpeciesTradePattern
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As SpeciesTradePatternData = CType(MyBase.Clone,SpeciesTradePatternData)
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
        If (Not (ds.Tables("BOSpeciesTradePattern")) Is Nothing) Then
            Me.Tables.Add(New BOSpeciesTradePatternDataTable(ds.Tables("BOSpeciesTradePattern")))
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
        Me.tableBOSpeciesTradePattern = CType(Me.Tables("BOSpeciesTradePattern"),BOSpeciesTradePatternDataTable)
        If (Not (Me.tableBOSpeciesTradePattern) Is Nothing) Then
            Me.tableBOSpeciesTradePattern.InitVars
        End If
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "SpeciesTradePatternData"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/SpeciesTradePatternData.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableBOSpeciesTradePattern = New BOSpeciesTradePatternDataTable
        Me.Tables.Add(Me.tableBOSpeciesTradePattern)
    End Sub
    
    Private Function ShouldSerializeBOSpeciesTradePattern() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub BOSpeciesTradePatternRowChangeEventHandler(ByVal sender As Object, ByVal e As BOSpeciesTradePatternRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOSpeciesTradePatternDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnPartDerivative As DataColumn
        
        Private columnSource As DataColumn
        
        Private columnPurpose As DataColumn
        
        Private columnCountryOfOrigin As DataColumn
        
        Private columnCountryFrom As DataColumn
        
        Private columnCountryTo As DataColumn
        
        Private columnAppImport As DataColumn
        
        Private columnAppExport As DataColumn
        
        Private columnAppSeizure As DataColumn
        
        Private columnAppTotal As DataColumn
        
        Private columnTradeImport As DataColumn
        
        Private columnTradeExport As DataColumn
        
        Private columnTradeSeizure As DataColumn
        
        Private columnTradeTotal As DataColumn
        
        Friend Sub New()
            MyBase.New("BOSpeciesTradePattern")
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
        
        Friend ReadOnly Property PartDerivativeColumn As DataColumn
            Get
                Return Me.columnPartDerivative
            End Get
        End Property
        
        Friend ReadOnly Property SourceColumn As DataColumn
            Get
                Return Me.columnSource
            End Get
        End Property
        
        Friend ReadOnly Property PurposeColumn As DataColumn
            Get
                Return Me.columnPurpose
            End Get
        End Property
        
        Friend ReadOnly Property CountryOfOriginColumn As DataColumn
            Get
                Return Me.columnCountryOfOrigin
            End Get
        End Property
        
        Friend ReadOnly Property CountryFromColumn As DataColumn
            Get
                Return Me.columnCountryFrom
            End Get
        End Property
        
        Friend ReadOnly Property CountryToColumn As DataColumn
            Get
                Return Me.columnCountryTo
            End Get
        End Property
        
        Friend ReadOnly Property AppImportColumn As DataColumn
            Get
                Return Me.columnAppImport
            End Get
        End Property
        
        Friend ReadOnly Property AppExportColumn As DataColumn
            Get
                Return Me.columnAppExport
            End Get
        End Property
        
        Friend ReadOnly Property AppSeizureColumn As DataColumn
            Get
                Return Me.columnAppSeizure
            End Get
        End Property
        
        Friend ReadOnly Property AppTotalColumn As DataColumn
            Get
                Return Me.columnAppTotal
            End Get
        End Property
        
        Friend ReadOnly Property TradeImportColumn As DataColumn
            Get
                Return Me.columnTradeImport
            End Get
        End Property
        
        Friend ReadOnly Property TradeExportColumn As DataColumn
            Get
                Return Me.columnTradeExport
            End Get
        End Property
        
        Friend ReadOnly Property TradeSeizureColumn As DataColumn
            Get
                Return Me.columnTradeSeizure
            End Get
        End Property
        
        Friend ReadOnly Property TradeTotalColumn As DataColumn
            Get
                Return Me.columnTradeTotal
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As BOSpeciesTradePatternRow
            Get
                Return CType(Me.Rows(index),BOSpeciesTradePatternRow)
            End Get
        End Property
        
        Public Event BOSpeciesTradePatternRowChanged As BOSpeciesTradePatternRowChangeEventHandler
        
        Public Event BOSpeciesTradePatternRowChanging As BOSpeciesTradePatternRowChangeEventHandler
        
        Public Event BOSpeciesTradePatternRowDeleted As BOSpeciesTradePatternRowChangeEventHandler
        
        Public Event BOSpeciesTradePatternRowDeleting As BOSpeciesTradePatternRowChangeEventHandler
        
        Public Overloads Sub AddBOSpeciesTradePatternRow(ByVal row As BOSpeciesTradePatternRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddBOSpeciesTradePatternRow(ByVal PartDerivative As String, ByVal Source As String, ByVal Purpose As String, ByVal CountryOfOrigin As String, ByVal CountryFrom As String, ByVal CountryTo As String, ByVal AppImport As String, ByVal AppExport As String, ByVal AppSeizure As String, ByVal AppTotal As String, ByVal TradeImport As String, ByVal TradeExport As String, ByVal TradeSeizure As String, ByVal TradeTotal As String) As BOSpeciesTradePatternRow
            Dim rowBOSpeciesTradePatternRow As BOSpeciesTradePatternRow = CType(Me.NewRow,BOSpeciesTradePatternRow)
            rowBOSpeciesTradePatternRow.ItemArray = New Object() {PartDerivative, Source, Purpose, CountryOfOrigin, CountryFrom, CountryTo, AppImport, AppExport, AppSeizure, AppTotal, TradeImport, TradeExport, TradeSeizure, TradeTotal}
            Me.Rows.Add(rowBOSpeciesTradePatternRow)
            Return rowBOSpeciesTradePatternRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As BOSpeciesTradePatternDataTable = CType(MyBase.Clone,BOSpeciesTradePatternDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New BOSpeciesTradePatternDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnPartDerivative = Me.Columns("PartDerivative")
            Me.columnSource = Me.Columns("Source")
            Me.columnPurpose = Me.Columns("Purpose")
            Me.columnCountryOfOrigin = Me.Columns("CountryOfOrigin")
            Me.columnCountryFrom = Me.Columns("CountryFrom")
            Me.columnCountryTo = Me.Columns("CountryTo")
            Me.columnAppImport = Me.Columns("AppImport")
            Me.columnAppExport = Me.Columns("AppExport")
            Me.columnAppSeizure = Me.Columns("AppSeizure")
            Me.columnAppTotal = Me.Columns("AppTotal")
            Me.columnTradeImport = Me.Columns("TradeImport")
            Me.columnTradeExport = Me.Columns("TradeExport")
            Me.columnTradeSeizure = Me.Columns("TradeSeizure")
            Me.columnTradeTotal = Me.Columns("TradeTotal")
        End Sub
        
        Private Sub InitClass()
            Me.columnPartDerivative = New DataColumn("PartDerivative", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnPartDerivative)
            Me.columnSource = New DataColumn("Source", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnSource)
            Me.columnPurpose = New DataColumn("Purpose", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnPurpose)
            Me.columnCountryOfOrigin = New DataColumn("CountryOfOrigin", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnCountryOfOrigin)
            Me.columnCountryFrom = New DataColumn("CountryFrom", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnCountryFrom)
            Me.columnCountryTo = New DataColumn("CountryTo", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnCountryTo)
            Me.columnAppImport = New DataColumn("AppImport", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnAppImport)
            Me.columnAppExport = New DataColumn("AppExport", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnAppExport)
            Me.columnAppSeizure = New DataColumn("AppSeizure", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnAppSeizure)
            Me.columnAppTotal = New DataColumn("AppTotal", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnAppTotal)
            Me.columnTradeImport = New DataColumn("TradeImport", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTradeImport)
            Me.columnTradeExport = New DataColumn("TradeExport", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTradeExport)
            Me.columnTradeSeizure = New DataColumn("TradeSeizure", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTradeSeizure)
            Me.columnTradeTotal = New DataColumn("TradeTotal", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTradeTotal)
        End Sub
        
        Public Function NewBOSpeciesTradePatternRow() As BOSpeciesTradePatternRow
            Return CType(Me.NewRow,BOSpeciesTradePatternRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New BOSpeciesTradePatternRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(BOSpeciesTradePatternRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.BOSpeciesTradePatternRowChangedEvent) Is Nothing) Then
                RaiseEvent BOSpeciesTradePatternRowChanged(Me, New BOSpeciesTradePatternRowChangeEvent(CType(e.Row,BOSpeciesTradePatternRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.BOSpeciesTradePatternRowChangingEvent) Is Nothing) Then
                RaiseEvent BOSpeciesTradePatternRowChanging(Me, New BOSpeciesTradePatternRowChangeEvent(CType(e.Row,BOSpeciesTradePatternRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.BOSpeciesTradePatternRowDeletedEvent) Is Nothing) Then
                RaiseEvent BOSpeciesTradePatternRowDeleted(Me, New BOSpeciesTradePatternRowChangeEvent(CType(e.Row,BOSpeciesTradePatternRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.BOSpeciesTradePatternRowDeletingEvent) Is Nothing) Then
                RaiseEvent BOSpeciesTradePatternRowDeleting(Me, New BOSpeciesTradePatternRowChangeEvent(CType(e.Row,BOSpeciesTradePatternRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveBOSpeciesTradePatternRow(ByVal row As BOSpeciesTradePatternRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOSpeciesTradePatternRow
        Inherits DataRow
        
        Private tableBOSpeciesTradePattern As BOSpeciesTradePatternDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableBOSpeciesTradePattern = CType(Me.Table,BOSpeciesTradePatternDataTable)
        End Sub
        
        Public Property PartDerivative As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.PartDerivativeColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.PartDerivativeColumn) = value
            End Set
        End Property
        
        Public Property Source As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.SourceColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.SourceColumn) = value
            End Set
        End Property
        
        Public Property Purpose As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.PurposeColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.PurposeColumn) = value
            End Set
        End Property
        
        Public Property CountryOfOrigin As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.CountryOfOriginColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.CountryOfOriginColumn) = value
            End Set
        End Property
        
        Public Property CountryFrom As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.CountryFromColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.CountryFromColumn) = value
            End Set
        End Property
        
        Public Property CountryTo As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.CountryToColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.CountryToColumn) = value
            End Set
        End Property
        
        Public Property AppImport As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.AppImportColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.AppImportColumn) = value
            End Set
        End Property
        
        Public Property AppExport As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.AppExportColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.AppExportColumn) = value
            End Set
        End Property
        
        Public Property AppSeizure As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.AppSeizureColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.AppSeizureColumn) = value
            End Set
        End Property
        
        Public Property AppTotal As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.AppTotalColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.AppTotalColumn) = value
            End Set
        End Property
        
        Public Property TradeImport As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.TradeImportColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.TradeImportColumn) = value
            End Set
        End Property
        
        Public Property TradeExport As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.TradeExportColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.TradeExportColumn) = value
            End Set
        End Property
        
        Public Property TradeSeizure As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.TradeSeizureColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.TradeSeizureColumn) = value
            End Set
        End Property
        
        Public Property TradeTotal As String
            Get
                Try 
                    Return CType(Me(Me.tableBOSpeciesTradePattern.TradeTotalColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOSpeciesTradePattern.TradeTotalColumn) = value
            End Set
        End Property
        
        Public Function IsPartDerivativeNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.PartDerivativeColumn)
        End Function
        
        Public Sub SetPartDerivativeNull()
            Me(Me.tableBOSpeciesTradePattern.PartDerivativeColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsSourceNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.SourceColumn)
        End Function
        
        Public Sub SetSourceNull()
            Me(Me.tableBOSpeciesTradePattern.SourceColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsPurposeNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.PurposeColumn)
        End Function
        
        Public Sub SetPurposeNull()
            Me(Me.tableBOSpeciesTradePattern.PurposeColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsCountryOfOriginNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.CountryOfOriginColumn)
        End Function
        
        Public Sub SetCountryOfOriginNull()
            Me(Me.tableBOSpeciesTradePattern.CountryOfOriginColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsCountryFromNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.CountryFromColumn)
        End Function
        
        Public Sub SetCountryFromNull()
            Me(Me.tableBOSpeciesTradePattern.CountryFromColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsCountryToNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.CountryToColumn)
        End Function
        
        Public Sub SetCountryToNull()
            Me(Me.tableBOSpeciesTradePattern.CountryToColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsAppImportNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.AppImportColumn)
        End Function
        
        Public Sub SetAppImportNull()
            Me(Me.tableBOSpeciesTradePattern.AppImportColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsAppExportNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.AppExportColumn)
        End Function
        
        Public Sub SetAppExportNull()
            Me(Me.tableBOSpeciesTradePattern.AppExportColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsAppSeizureNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.AppSeizureColumn)
        End Function
        
        Public Sub SetAppSeizureNull()
            Me(Me.tableBOSpeciesTradePattern.AppSeizureColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsAppTotalNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.AppTotalColumn)
        End Function
        
        Public Sub SetAppTotalNull()
            Me(Me.tableBOSpeciesTradePattern.AppTotalColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTradeImportNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.TradeImportColumn)
        End Function
        
        Public Sub SetTradeImportNull()
            Me(Me.tableBOSpeciesTradePattern.TradeImportColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTradeExportNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.TradeExportColumn)
        End Function
        
        Public Sub SetTradeExportNull()
            Me(Me.tableBOSpeciesTradePattern.TradeExportColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTradeSeizureNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.TradeSeizureColumn)
        End Function
        
        Public Sub SetTradeSeizureNull()
            Me(Me.tableBOSpeciesTradePattern.TradeSeizureColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTradeTotalNull() As Boolean
            Return Me.IsNull(Me.tableBOSpeciesTradePattern.TradeTotalColumn)
        End Function
        
        Public Sub SetTradeTotalNull()
            Me(Me.tableBOSpeciesTradePattern.TradeTotalColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOSpeciesTradePatternRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As BOSpeciesTradePatternRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As BOSpeciesTradePatternRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As BOSpeciesTradePatternRow
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