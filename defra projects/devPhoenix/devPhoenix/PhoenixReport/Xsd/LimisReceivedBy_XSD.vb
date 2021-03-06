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
Public Class LimisReceivedByData
    Inherits DataSet
    
    Private tableBOAppTypeDetail As BOAppTypeDetailDataTable
    
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
            If (Not (ds.Tables("BOAppTypeDetail")) Is Nothing) Then
                Me.Tables.Add(New BOAppTypeDetailDataTable(ds.Tables("BOAppTypeDetail")))
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
    Public ReadOnly Property BOAppTypeDetail As BOAppTypeDetailDataTable
        Get
            Return Me.tableBOAppTypeDetail
        End Get
    End Property
    
    Public Overrides Function Clone() As DataSet
        Dim cln As LimisReceivedByData = CType(MyBase.Clone,LimisReceivedByData)
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
        If (Not (ds.Tables("BOAppTypeDetail")) Is Nothing) Then
            Me.Tables.Add(New BOAppTypeDetailDataTable(ds.Tables("BOAppTypeDetail")))
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
        Me.tableBOAppTypeDetail = CType(Me.Tables("BOAppTypeDetail"),BOAppTypeDetailDataTable)
        If (Not (Me.tableBOAppTypeDetail) Is Nothing) Then
            Me.tableBOAppTypeDetail.InitVars
        End If
    End Sub
    
    Private Sub InitClass()
        Me.DataSetName = "LimisReceivedByData"
        Me.Prefix = ""
        Me.Namespace = "http://tempuri.org/LimisReceivedByData.xsd"
        Me.Locale = New System.Globalization.CultureInfo("en-US")
        Me.CaseSensitive = false
        Me.EnforceConstraints = true
        Me.tableBOAppTypeDetail = New BOAppTypeDetailDataTable
        Me.Tables.Add(Me.tableBOAppTypeDetail)
    End Sub
    
    Private Function ShouldSerializeBOAppTypeDetail() As Boolean
        Return false
    End Function
    
    Private Sub SchemaChanged(ByVal sender As Object, ByVal e As System.ComponentModel.CollectionChangeEventArgs)
        If (e.Action = System.ComponentModel.CollectionChangeAction.Remove) Then
            Me.InitVars
        End If
    End Sub
    
    Public Delegate Sub BOAppTypeDetailRowChangeEventHandler(ByVal sender As Object, ByVal e As BOAppTypeDetailRowChangeEvent)
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOAppTypeDetailDataTable
        Inherits DataTable
        Implements System.Collections.IEnumerable
        
        Private columnAppTypeId As DataColumn
        
        Private columnDescription As DataColumn
        
        Private columnInternetApps As DataColumn
        
        Private columnTelephoneApps As DataColumn
        
        Private columnPostalApps As DataColumn
        
        Private columnEMailApps As DataColumn
        
        Private columnFaxApps As DataColumn
        
        Private columnPageHeading As DataColumn
        
        Private columnTotalHeading As DataColumn
        
        Friend Sub New()
            MyBase.New("BOAppTypeDetail")
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
        
        Friend ReadOnly Property AppTypeIdColumn As DataColumn
            Get
                Return Me.columnAppTypeId
            End Get
        End Property
        
        Friend ReadOnly Property DescriptionColumn As DataColumn
            Get
                Return Me.columnDescription
            End Get
        End Property
        
        Friend ReadOnly Property InternetAppsColumn As DataColumn
            Get
                Return Me.columnInternetApps
            End Get
        End Property
        
        Friend ReadOnly Property TelephoneAppsColumn As DataColumn
            Get
                Return Me.columnTelephoneApps
            End Get
        End Property
        
        Friend ReadOnly Property PostalAppsColumn As DataColumn
            Get
                Return Me.columnPostalApps
            End Get
        End Property
        
        Friend ReadOnly Property EMailAppsColumn As DataColumn
            Get
                Return Me.columnEMailApps
            End Get
        End Property
        
        Friend ReadOnly Property FaxAppsColumn As DataColumn
            Get
                Return Me.columnFaxApps
            End Get
        End Property
        
        Friend ReadOnly Property PageHeadingColumn As DataColumn
            Get
                Return Me.columnPageHeading
            End Get
        End Property
        
        Friend ReadOnly Property TotalHeadingColumn As DataColumn
            Get
                Return Me.columnTotalHeading
            End Get
        End Property
        
        Public Default ReadOnly Property Item(ByVal index As Integer) As BOAppTypeDetailRow
            Get
                Return CType(Me.Rows(index),BOAppTypeDetailRow)
            End Get
        End Property
        
        Public Event BOAppTypeDetailRowChanged As BOAppTypeDetailRowChangeEventHandler
        
        Public Event BOAppTypeDetailRowChanging As BOAppTypeDetailRowChangeEventHandler
        
        Public Event BOAppTypeDetailRowDeleted As BOAppTypeDetailRowChangeEventHandler
        
        Public Event BOAppTypeDetailRowDeleting As BOAppTypeDetailRowChangeEventHandler
        
        Public Overloads Sub AddBOAppTypeDetailRow(ByVal row As BOAppTypeDetailRow)
            Me.Rows.Add(row)
        End Sub
        
        Public Overloads Function AddBOAppTypeDetailRow(ByVal AppTypeId As Long, ByVal Description As String, ByVal InternetApps As String, ByVal TelephoneApps As String, ByVal PostalApps As String, ByVal EMailApps As String, ByVal FaxApps As String, ByVal PageHeading As Boolean, ByVal TotalHeading As Boolean) As BOAppTypeDetailRow
            Dim rowBOAppTypeDetailRow As BOAppTypeDetailRow = CType(Me.NewRow,BOAppTypeDetailRow)
            rowBOAppTypeDetailRow.ItemArray = New Object() {AppTypeId, Description, InternetApps, TelephoneApps, PostalApps, EMailApps, FaxApps, PageHeading, TotalHeading}
            Me.Rows.Add(rowBOAppTypeDetailRow)
            Return rowBOAppTypeDetailRow
        End Function
        
        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Me.Rows.GetEnumerator
        End Function
        
        Public Overrides Function Clone() As DataTable
            Dim cln As BOAppTypeDetailDataTable = CType(MyBase.Clone,BOAppTypeDetailDataTable)
            cln.InitVars
            Return cln
        End Function
        
        Protected Overrides Function CreateInstance() As DataTable
            Return New BOAppTypeDetailDataTable
        End Function
        
        Friend Sub InitVars()
            Me.columnAppTypeId = Me.Columns("AppTypeId")
            Me.columnDescription = Me.Columns("Description")
            Me.columnInternetApps = Me.Columns("InternetApps")
            Me.columnTelephoneApps = Me.Columns("TelephoneApps")
            Me.columnPostalApps = Me.Columns("PostalApps")
            Me.columnEMailApps = Me.Columns("EMailApps")
            Me.columnFaxApps = Me.Columns("FaxApps")
            Me.columnPageHeading = Me.Columns("PageHeading")
            Me.columnTotalHeading = Me.Columns("TotalHeading")
        End Sub
        
        Private Sub InitClass()
            Me.columnAppTypeId = New DataColumn("AppTypeId", GetType(System.Int64), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnAppTypeId)
            Me.columnDescription = New DataColumn("Description", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnDescription)
            Me.columnInternetApps = New DataColumn("InternetApps", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnInternetApps)
            Me.columnTelephoneApps = New DataColumn("TelephoneApps", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTelephoneApps)
            Me.columnPostalApps = New DataColumn("PostalApps", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnPostalApps)
            Me.columnEMailApps = New DataColumn("EMailApps", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnEMailApps)
            Me.columnFaxApps = New DataColumn("FaxApps", GetType(System.String), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnFaxApps)
            Me.columnPageHeading = New DataColumn("PageHeading", GetType(System.Boolean), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnPageHeading)
            Me.columnTotalHeading = New DataColumn("TotalHeading", GetType(System.Boolean), Nothing, System.Data.MappingType.Element)
            Me.Columns.Add(Me.columnTotalHeading)
        End Sub
        
        Public Function NewBOAppTypeDetailRow() As BOAppTypeDetailRow
            Return CType(Me.NewRow,BOAppTypeDetailRow)
        End Function
        
        Protected Overrides Function NewRowFromBuilder(ByVal builder As DataRowBuilder) As DataRow
            Return New BOAppTypeDetailRow(builder)
        End Function
        
        Protected Overrides Function GetRowType() As System.Type
            Return GetType(BOAppTypeDetailRow)
        End Function
        
        Protected Overrides Sub OnRowChanged(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanged(e)
            If (Not (Me.BOAppTypeDetailRowChangedEvent) Is Nothing) Then
                RaiseEvent BOAppTypeDetailRowChanged(Me, New BOAppTypeDetailRowChangeEvent(CType(e.Row,BOAppTypeDetailRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowChanging(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowChanging(e)
            If (Not (Me.BOAppTypeDetailRowChangingEvent) Is Nothing) Then
                RaiseEvent BOAppTypeDetailRowChanging(Me, New BOAppTypeDetailRowChangeEvent(CType(e.Row,BOAppTypeDetailRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleted(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleted(e)
            If (Not (Me.BOAppTypeDetailRowDeletedEvent) Is Nothing) Then
                RaiseEvent BOAppTypeDetailRowDeleted(Me, New BOAppTypeDetailRowChangeEvent(CType(e.Row,BOAppTypeDetailRow), e.Action))
            End If
        End Sub
        
        Protected Overrides Sub OnRowDeleting(ByVal e As DataRowChangeEventArgs)
            MyBase.OnRowDeleting(e)
            If (Not (Me.BOAppTypeDetailRowDeletingEvent) Is Nothing) Then
                RaiseEvent BOAppTypeDetailRowDeleting(Me, New BOAppTypeDetailRowChangeEvent(CType(e.Row,BOAppTypeDetailRow), e.Action))
            End If
        End Sub
        
        Public Sub RemoveBOAppTypeDetailRow(ByVal row As BOAppTypeDetailRow)
            Me.Rows.Remove(row)
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOAppTypeDetailRow
        Inherits DataRow
        
        Private tableBOAppTypeDetail As BOAppTypeDetailDataTable
        
        Friend Sub New(ByVal rb As DataRowBuilder)
            MyBase.New(rb)
            Me.tableBOAppTypeDetail = CType(Me.Table,BOAppTypeDetailDataTable)
        End Sub
        
        Public Property AppTypeId As Long
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.AppTypeIdColumn),Long)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.AppTypeIdColumn) = value
            End Set
        End Property
        
        Public Property Description As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.DescriptionColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.DescriptionColumn) = value
            End Set
        End Property
        
        Public Property InternetApps As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.InternetAppsColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.InternetAppsColumn) = value
            End Set
        End Property
        
        Public Property TelephoneApps As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.TelephoneAppsColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.TelephoneAppsColumn) = value
            End Set
        End Property
        
        Public Property PostalApps As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.PostalAppsColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.PostalAppsColumn) = value
            End Set
        End Property
        
        Public Property EMailApps As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.EMailAppsColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.EMailAppsColumn) = value
            End Set
        End Property
        
        Public Property FaxApps As String
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.FaxAppsColumn),String)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.FaxAppsColumn) = value
            End Set
        End Property
        
        Public Property PageHeading As Boolean
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.PageHeadingColumn),Boolean)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.PageHeadingColumn) = value
            End Set
        End Property
        
        Public Property TotalHeading As Boolean
            Get
                Try 
                    Return CType(Me(Me.tableBOAppTypeDetail.TotalHeadingColumn),Boolean)
                Catch e As InvalidCastException
                    Throw New StrongTypingException("Cannot get value because it is DBNull.", e)
                End Try
            End Get
            Set
                Me(Me.tableBOAppTypeDetail.TotalHeadingColumn) = value
            End Set
        End Property
        
        Public Function IsAppTypeIdNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.AppTypeIdColumn)
        End Function
        
        Public Sub SetAppTypeIdNull()
            Me(Me.tableBOAppTypeDetail.AppTypeIdColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsDescriptionNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.DescriptionColumn)
        End Function
        
        Public Sub SetDescriptionNull()
            Me(Me.tableBOAppTypeDetail.DescriptionColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsInternetAppsNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.InternetAppsColumn)
        End Function
        
        Public Sub SetInternetAppsNull()
            Me(Me.tableBOAppTypeDetail.InternetAppsColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTelephoneAppsNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.TelephoneAppsColumn)
        End Function
        
        Public Sub SetTelephoneAppsNull()
            Me(Me.tableBOAppTypeDetail.TelephoneAppsColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsPostalAppsNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.PostalAppsColumn)
        End Function
        
        Public Sub SetPostalAppsNull()
            Me(Me.tableBOAppTypeDetail.PostalAppsColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsEMailAppsNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.EMailAppsColumn)
        End Function
        
        Public Sub SetEMailAppsNull()
            Me(Me.tableBOAppTypeDetail.EMailAppsColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsFaxAppsNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.FaxAppsColumn)
        End Function
        
        Public Sub SetFaxAppsNull()
            Me(Me.tableBOAppTypeDetail.FaxAppsColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsPageHeadingNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.PageHeadingColumn)
        End Function
        
        Public Sub SetPageHeadingNull()
            Me(Me.tableBOAppTypeDetail.PageHeadingColumn) = System.Convert.DBNull
        End Sub
        
        Public Function IsTotalHeadingNull() As Boolean
            Return Me.IsNull(Me.tableBOAppTypeDetail.TotalHeadingColumn)
        End Function
        
        Public Sub SetTotalHeadingNull()
            Me(Me.tableBOAppTypeDetail.TotalHeadingColumn) = System.Convert.DBNull
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThrough()>  _
    Public Class BOAppTypeDetailRowChangeEvent
        Inherits EventArgs
        
        Private eventRow As BOAppTypeDetailRow
        
        Private eventAction As DataRowAction
        
        Public Sub New(ByVal row As BOAppTypeDetailRow, ByVal action As DataRowAction)
            MyBase.New
            Me.eventRow = row
            Me.eventAction = action
        End Sub
        
        Public ReadOnly Property Row As BOAppTypeDetailRow
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
