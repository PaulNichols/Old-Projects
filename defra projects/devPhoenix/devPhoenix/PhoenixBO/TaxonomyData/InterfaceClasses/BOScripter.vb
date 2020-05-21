Namespace TaxonomyData

    Public Enum TaxonomyRowSourceEnum
        Standard = 1
        Higher = 2
        Kew = 3
    End Enum

    Public Class TaxonomyRowSourceAccessor
        Public Function GetSource(ByVal Taxon As Taxonomy.BOTaxon) As TaxonomyRowSourceEnum
            Select Case Taxon.KingdomID
                Case BO.Taxonomy.TaxonomySearch.GetPlantKingdom.KingdomID
                    Return TaxonomyRowSourceEnum.Kew
                Case Else
                    Select Case Taxon.TaxonType
                        Case Taxonomy.TaxonTypeEnum.Species, Taxonomy.TaxonTypeEnum.Epithet, Taxonomy.TaxonTypeEnum.Stock
                            Return TaxonomyRowSourceEnum.Standard
                        Case Else
                            Return TaxonomyRowSourceEnum.Higher
                    End Select
            End Select
        End Function
    End Class

    Public Class BOTaxonomyDataError
        Friend Sub New()
            mMessages = New ArrayList
        End Sub

        Friend Sub New(ByVal Message As String)
            MyClass.New()
            Me.AddMessage(Message)
        End Sub

        Private mMessages As ArrayList

        Friend Sub AddMessage(ByVal Message As String)
            mMessages.Add(Message)
        End Sub

        Friend ReadOnly Property HasError() As Boolean
            Get
                Return mMessages.Count > 0
            End Get
        End Property

        Friend ReadOnly Property Messages() As ArrayList
            Get
                Return mMessages
            End Get
        End Property
    End Class

    ''This is a test class that I can use and comment the other one out.
    'Public Class BOScripter

    '    Public Sub New(ByVal NewTransfer As Transfer, ByVal OldTransfer As Transfer)
    '        Try

    '        Catch Ex As Exception
    '            Throw New Exception("Cannot create new BOScripter object", Ex)
    '        End Try
    '    End Sub

    '    Friend ReadOnly Property Status() As BOTaxonomyDataError
    '        Get
    '            Return mTaxonomyDataError
    '        End Get
    '    End Property
    '    Private mTaxonomyDataError As BOTaxonomyDataError

    '    Friend Sub ValidateLogical()

    '    End Sub

    '    Friend Function ManagePhysical() As Boolean

    '    End Function

    '    Friend Sub LoadPhysical()

    '    End Sub
    'End Class

    Public Class BOScripter
        'Inherits BaseBO

#Region "Prelim code"
        Public Sub New(ByVal NewTransfer As Transfer, ByVal OldTransfer As Transfer)
            Try
                mTaxonomyDataError = New BOTaxonomyDataError
                mDiagnostics = New ArrayList
                Me.TransferNew = NewTransfer
                SplitDataSet(NewTransfer, OldTransfer)
            Catch Ex As Exception
                Throw New Exception("Cannot create new BOScripter object", Ex)
            End Try
        End Sub

        Private Sub SplitDataSet(ByVal NewTransfer As Transfer, ByVal OldTransfer As Transfer)
            Try
                'Attempt to get the deletes and additions.
                Dim TransferMerge As DataSet
                If OldTransfer Is Nothing = False Then
                    TransferMerge = OldTransfer.Copy
                    TransferMerge.AcceptChanges()
                Else
                    TransferMerge = New Transfer
                End If

                TransferMerge.Tables("VAQUATIC").PrimaryKey = New DataColumn() {TransferMerge.Tables("VAQUATIC").Columns("AQURECID")}
                TransferMerge.Tables("VBRU").PrimaryKey = New DataColumn() {TransferMerge.Tables("VBRU").Columns("BRURECID")}
                TransferMerge.Tables("VAREAOFUSE").PrimaryKey = New DataColumn() {TransferMerge.Tables("VAREAOFUSE").Columns("AOURECID")}
                TransferMerge.Tables("VCOMMONNAME").PrimaryKey = New DataColumn() {TransferMerge.Tables("VCOMMONNAME").Columns("COMRECID")}
                TransferMerge.Tables("VCOUNTRY").PrimaryKey = New DataColumn() {TransferMerge.Tables("VCOUNTRY").Columns("CTYRECID")}
                TransferMerge.Tables("VDECISIONS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VDECISIONS").Columns("DECRECID")}
                TransferMerge.Tables("VDISTRIBAQUA").PrimaryKey = New DataColumn() {TransferMerge.Tables("VDISTRIBAQUA").Columns("DAQRECID")}
                TransferMerge.Tables("VDISTRIBBRU").PrimaryKey = New DataColumn() {TransferMerge.Tables("VDISTRIBBRU").Columns("DBURECID")}
                TransferMerge.Tables("VDISTRIBCTY").PrimaryKey = New DataColumn() {TransferMerge.Tables("VDISTRIBCTY").Columns("DCTRECID")}
                TransferMerge.Tables("VFAMILY").PrimaryKey = New DataColumn() {TransferMerge.Tables("VFAMILY").Columns("FAMRECID")}
                TransferMerge.Tables("VGENUS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VGENUS").Columns("GENRECID")}
                TransferMerge.Tables("VHIGHCOMMON").PrimaryKey = New DataColumn() {TransferMerge.Tables("VHIGHCOMMON").Columns("HCOMRECID")}
                TransferMerge.Tables("VHIGHDECISIONS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VHIGHDECISIONS").Columns("HDECRECID")}
                TransferMerge.Tables("VHIGHLEGAL").PrimaryKey = New DataColumn() {TransferMerge.Tables("VHIGHLEGAL").Columns("HLEGRECID")}
                TransferMerge.Tables("VHIGHQUOTAS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VHIGHQUOTAS").Columns("HQUORECID")}
                TransferMerge.Tables("VHIGHSYNONYMS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VHIGHSYNONYMS").Columns("HSYNRECID")}
                TransferMerge.Tables("VKINGDOM").PrimaryKey = New DataColumn() {TransferMerge.Tables("VKINGDOM").Columns("KGMRECID")}
                TransferMerge.Tables("VLEGAL").PrimaryKey = New DataColumn() {TransferMerge.Tables("VLEGAL").Columns("LEGRECID")}
                TransferMerge.Tables("VLEGALNAME").PrimaryKey = New DataColumn() {TransferMerge.Tables("VLEGALNAME").Columns("LNMRECID")}
                TransferMerge.Tables("VLEVELOFUSE").PrimaryKey = New DataColumn() {TransferMerge.Tables("VLEVELOFUSE").Columns("LOURECID")}
                TransferMerge.Tables("VNOTIFICATION").PrimaryKey = New DataColumn() {TransferMerge.Tables("VNOTIFICATION").Columns("NOTRECID")}
                TransferMerge.Tables("VPART").PrimaryKey = New DataColumn() {TransferMerge.Tables("VPART").Columns("PARRECID")}
                TransferMerge.Tables("VQUOTAS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VQUOTAS").Columns("QUORECID")}
                TransferMerge.Tables("VQUOTASOURCE").PrimaryKey = New DataColumn() {TransferMerge.Tables("VQUOTASOURCE").Columns("QUOSRCRECID")}
                TransferMerge.Tables("VQUOTATERMS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VQUOTATERMS").Columns("QUOTRMRECID")}
                TransferMerge.Tables("VSPECIES").PrimaryKey = New DataColumn() {TransferMerge.Tables("VSPECIES").Columns("SPCRECID")}
                TransferMerge.Tables("VSYNLINK").PrimaryKey = New DataColumn() {TransferMerge.Tables("VSYNLINK").Columns("SYNRECID")}
                TransferMerge.Tables("VTAXCLASS").PrimaryKey = New DataColumn() {TransferMerge.Tables("VTAXCLASS").Columns("CLARECID")}
                TransferMerge.Tables("VTAXORDER").PrimaryKey = New DataColumn() {TransferMerge.Tables("VTAXORDER").Columns("ORDRECID")}
                TransferMerge.Tables("VTAXPHYLUM").PrimaryKey = New DataColumn() {TransferMerge.Tables("VTAXPHYLUM").Columns("PHYRECID")}
                TransferMerge.Tables("VUSED").PrimaryKey = New DataColumn() {TransferMerge.Tables("VUSED").Columns("USERECID")}
                TransferMerge.Tables("VUSETYPE").PrimaryKey = New DataColumn() {TransferMerge.Tables("VUSETYPE").Columns("UTYRECID")}

                If NewTransfer Is Nothing = False Then
                    TransferMerge.Merge(NewTransfer, False)
                Else
                    Throw New Exception("NewTransfer is nothing")
                End If

                'I cannot use a merge to get the updates as both datasets in the merge contain a complete database, so the items in the merge that are not inserts and are not deletes are assumed to be updates, even if there is no difference in the data.
                Dim ConfirmedUpdates As DataSet
                If Not OldTransfer Is Nothing Then
                    ConfirmedUpdates = OldTransfer.Clone
                Else
                    ConfirmedUpdates = New Transfer
                End If
                Dim PossibleUpdates As DataSet = TransferMerge.GetChanges(DataRowState.Modified)
                If PossibleUpdates Is Nothing = False Then
                    PossibleUpdates.AcceptChanges()
                    For Each Table As DataTable In PossibleUpdates.Tables
                        Dim RowIdxMax As Int32 = Table.Rows.Count - 1
                        Dim RowIdx As Int32 = 0
                        While RowIdx <= RowIdxMax
                            Dim Row As DataRow = Table.Rows(RowIdx)
                            Dim ColIdxMax As Int32 = Row.ItemArray.Length - 1
                            Dim ColIdx As Int32 = 0
                            While ColIdx <= ColIdxMax
                                If String.Compare(CType(Row.ItemArray(ColIdx), String), CType(OldTransfer.Tables(Table.TableName).Rows(RowIdx).ItemArray(ColIdx), String)) <> 0 Then
                                    ConfirmedUpdates.Tables(Table.TableName).ImportRow(Row)
                                    Exit While
                                End If
                                ColIdx += 1
                            End While 'Col.
                            RowIdx += 1
                        End While 'Row.
                    Next Table
                End If
                Me.TransferUpdates = ConfirmedUpdates
                Me.TransferInserts = TransferMerge.GetChanges(DataRowState.Added)
                Me.TransferDeletes = TransferMerge.GetChanges(DataRowState.Unchanged)

            Catch ex As Exception
                Throw New Exception("Cannot split Dataset", ex)
            End Try
        End Sub

#End Region

#Region "Properties"

        Friend ReadOnly Property Status() As BOTaxonomyDataError
            Get
                Return mTaxonomyDataError
            End Get
        End Property
        Private mTaxonomyDataError As BOTaxonomyDataError

        Private mDiagnostics As ArrayList

        Private Property TransferInserts() As DataSet
            Get
                Return mTransferInserts
            End Get
            Set(ByVal Value As DataSet)
                mTransferInserts = CType(Value, Transfer)
            End Set
        End Property
        Private mTransferInserts As Transfer

        Private Property TransferDeletes() As DataSet
            Get
                Return mTransferDeletes
            End Get
            Set(ByVal Value As DataSet)
                mTransferDeletes = CType(Value, Transfer)
            End Set
        End Property
        Private mTransferDeletes As Transfer

        Private Property TransferUpdates() As DataSet
            Get
                Return mTransferUpdates
            End Get
            Set(ByVal Value As DataSet)
                mTransferUpdates = CType(Value, Transfer)
            End Set
        End Property
        Private mTransferUpdates As Transfer

        Private Property TransferNew() As DataSet
            Get
                Return mTransferNew
            End Get
            Set(ByVal Value As DataSet)
                mTransferNew = Value
            End Set
        End Property
        Private mTransferNew As DataSet

        Private mUseCheckPoints As Boolean

        Private mUseTransaction As Boolean

#End Region

#Region " Methods "

        Private Enum TableActionOrderEnum
            PhysicalInsert
            PhysicalUpdate
            PhysicalDelete
            LogicalValidation
        End Enum

        Private Transaction As System.Data.SqlClient.SqlTransaction
        Private EO As EnterpriseObjects.Service


        Private Sub LoadVTables()

            EO = New EnterpriseObjects.Service
            If mUseTransaction = True Then
                Transaction = EO.BeginTransaction()
            Else
                Transaction = Nothing
            End If
            Try
                [DO].DataObjects.Sprocs.dbo_usp_TruncateTDLTables(Transaction)
                For Each TableName As String In Me.TableActionOrder(TableActionOrderEnum.LogicalValidation)
                    Dim SingleTableDataset As New DataSet

                    Select Case TableName.ToUpper
                        Case "VAQUATIC"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VAQUATIC)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLAquatic", SingleTableDataset, Transaction)
                        Case "VAREAOFUSE"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VAREAOFUSE)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLAreaOfUse", SingleTableDataset, Transaction)
                        Case "VBRU"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VBRU)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLBru", SingleTableDataset, Transaction)
                        Case "VCOMMONNAME"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VCOMMONNAME)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLCommonName", SingleTableDataset, Transaction)
                        Case "VCOUNTRY"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VCOUNTRY)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLCountry", SingleTableDataset, Transaction)
                        Case "VDECISIONS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VDECISIONS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLDecisions", SingleTableDataset, Transaction)
                        Case "VDISTRIBAQUA"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VDISTRIBAQUA)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLDistribAqua", SingleTableDataset, Transaction)
                        Case "VDISTRIBBRU"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VDISTRIBBRU)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLDistribBru", SingleTableDataset, Transaction)
                        Case "VDISTRIBCTY"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VDISTRIBCTY)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLDistribCty", SingleTableDataset, Transaction)
                        Case "VFAMILY"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VFAMILY)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLFamily", SingleTableDataset, Transaction)
                        Case "VGENUS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VGENUS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLGenus", SingleTableDataset, Transaction)
                        Case "VHIGHCOMMON"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VHIGHCOMMON)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLHighCommon", SingleTableDataset, Transaction)
                        Case "VHIGHDECISIONS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VHIGHDECISIONS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLHighDecisions", SingleTableDataset, Transaction)
                        Case "VHIGHLEGAL"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VHIGHLEGAL)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLHighLegal", SingleTableDataset, Transaction)
                        Case "VHIGHQUOTAS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VHIGHQUOTAS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLHighQuotas", SingleTableDataset, Transaction)
                        Case "VHIGHSYNONYMS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VHIGHSYNONYMS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLHighSynonyms", SingleTableDataset, Transaction)
                        Case "VKINGDOM"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VKINGDOM)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLKingdom", SingleTableDataset, Transaction)
                        Case "VLEGAL"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VLEGAL)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLLegal", SingleTableDataset, Transaction)
                        Case "VLEGALNAME"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VLEGALNAME)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLLegalName", SingleTableDataset, Transaction)
                        Case "VLEVELOFUSE"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VLEVELOFUSE)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLLevelOfUse", SingleTableDataset, Transaction)
                        Case "VNOTIFICATION"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VNOTIFICATION)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLNotification", SingleTableDataset, Transaction)
                        Case "VPART"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VPART)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLPart", SingleTableDataset, Transaction)
                        Case "VQUOTAS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VQUOTAS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLQuotas", SingleTableDataset, Transaction)
                        Case "VQUOTASOURCE"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VQUOTASOURCE)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLQuotaSource", SingleTableDataset, Transaction)
                        Case "VQUOTATERMS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VQUOTATERMS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLQuotaTerms", SingleTableDataset, Transaction)
                        Case "VSPECIES"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VSPECIES)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLSpecies", SingleTableDataset, Transaction)
                        Case "VSYNLINK"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VSYNLINK)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLSynLink", SingleTableDataset, Transaction)
                        Case "VTAXCLASS"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VTAXCLASS)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLTaxClass", SingleTableDataset, Transaction)
                        Case "VTAXORDER"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VTAXORDER)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLTaxOrder", SingleTableDataset, Transaction)
                        Case "VTAXPHYLUM"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VTAXPHYLUM)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLTaxPhylum", SingleTableDataset, Transaction)
                        Case "VUSE"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VUSED)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLUse", SingleTableDataset, Transaction)
                        Case "VUSETYPE"
                            SingleTableDataset.Merge(CType(Me.TransferNew, Transfer).VUSETYPE)
                            [DO].DataObjects.TaxonomyXMLHelper.Insert("TDLUseType", SingleTableDataset, Transaction)
                        Case Else
                            Throw New Exception("ValidateLogical does not cater for " & TableName & " table")
                    End Select

                Next
                If mUseTransaction = True Then
                    EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                End If
            Catch ex As Exception
                If mUseTransaction = True Then
                    EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                End If
                Throw ex
            End Try

        End Sub

        Friend Sub ValidateLogical()
            Try
                LoadVTables()
                ValidateData()
            Catch ex As Exception
                Throw New Exception("Cannot validate the logical tables", ex)
            End Try

        End Sub

        Friend Function ManagePhysical(ByVal UseCheckPoints As Boolean, ByVal UseTransaction As Boolean) As Boolean
            Try

                mUseTransaction = UseTransaction
                mUseCheckPoints = UseCheckPoints
                Dim EO As EnterpriseObjects.Service = New EnterpriseObjects.Service
                If mUseTransaction Then
                    Transaction = EO.BeginTransaction()
                Else
                    Transaction = Nothing
                End If
                Try
                    If Status.HasError = False Then
                        ManagePhysicalDeletes()
                    End If
                    If Status.HasError = False Then
                        ManagePhysicalInserts()
                    End If
                    If Status.HasError = False Then
                        ManagePhysicalUpdates()
                    End If
                    If Status.HasError = False Then
                        If mUseTransaction Then
                            EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Commit)
                        End If
                    Else
                        If mUseTransaction Then
                            EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                        End If
                    End If
                Catch exSQL As SqlClient.SqlException
                    Throw exSQL
                Catch ex As Exception
                    If mUseTransaction Then
                        EO.EndTransaction(Transaction, EnterpriseObjects.Service.TransactionEndEnum.Rollback)
                    End If
                    Throw ex
                End Try
            Catch ex As Exception
                Throw New Exception("Cannot manage physical database", ex)
            End Try
        End Function

#End Region

#Region " Validate "

        'Private Function Validates() As Boolean
        '    Return (Not Validate() Is Nothing) OrElse (Not Me.ValidationErrors.HasErrors)
        'End Function

        'Protected Overridable Overloads Function Validate() As ValidationManager
        '    Return New ValidationManager
        'End Function

#End Region

#Region "Helper Functions"

        Private Sub SaveTransaction(ByVal SavePointName As String)
            If mUseCheckPoints = True And mUseTransaction = True Then
                Transaction.Save(SavePointName)
            End If
        End Sub

        Private Sub ManagePhysicalUpdates()
            For Each TableName As String In Me.TableActionOrder(TableActionOrderEnum.PhysicalUpdate)
                Select Case TableName.ToUpper
                    Case "VAQUATIC".ToUpper
                        PhysicalUpdateVAQUATIC()
                    Case "VAREAOFUSE".ToUpper
                        PhysicalUpdateVAREAOFUSE()
                    Case "VBRU".ToUpper
                        PhysicalUpdateVBRU()
                    Case "VCOMMONNAME".ToUpper
                        PhysicalUpdateVCOMMONNAME()
                    Case "VCOUNTRY".ToUpper
                        PhysicalUpdateVCOUNTRY()
                    Case "VDECISIONS".ToUpper
                        PhysicalUpdateVDECISIONS()
                    Case "VDISTRIBAQUA".ToUpper
                        PhysicalUpdateVDISTRIBAQUA()
                    Case "VDISTRIBCTY".ToUpper
                        PhysicalUpdateVDISTRIBCTY()
                    Case "VFAMILY".ToUpper
                        PhysicalUpdateVFAMILY()
                    Case "VGENUS".ToUpper
                        PhysicalUpdateVGENUS()
                    Case "VHIGHCOMMON".ToUpper
                        PhysicalUpdateVHIGHCOMMON()
                    Case "VHIGHDECISIONS".ToUpper
                        PhysicalUpdateVHIGHDECISIONS()
                    Case "VHIGHLEGAL".ToUpper
                        PhysicalUpdateVHIGHLEGAL()
                    Case "VHIGHQUOTAS".ToUpper
                        PhysicalUpdateVHIGHQUOTAS()
                    Case "VHIGHSYNONYMS".ToUpper
                        PhysicalUpdateVHIGHSYNONYMS()
                    Case "VKINGDOM".ToUpper
                        PhysicalUpdateVKINGDOM()
                    Case "VLEGAL".ToUpper
                        PhysicalUpdateVLEGAL()
                    Case "VLEGALNAME".ToUpper
                        PhysicalUpdateVLEGALNAME()
                    Case "VLEVELOFUSE".ToUpper
                        PhysicalUpdateVLEVELOFUSE()
                    Case "VNOTIFICATION".ToUpper
                        PhysicalUpdateVNOTIFICATION()
                    Case "VPART".ToUpper
                        PhysicalUpdateVPART()
                    Case "VQUOTAS".ToUpper
                        PhysicalUpdateVQUOTAS()
                    Case "VQUOTASOURCE".ToUpper
                        PhysicalUpdateVQUOTASOURCE()
                    Case "VQUOTATERMS".ToUpper
                        PhysicalUpdateVQUOTATERMS()
                    Case "VSPECIES".ToUpper
                        PhysicalUpdateVSPECIES()
                    Case "VSYNLINK".ToUpper
                        PhysicalUpdateVSYNLINK()
                    Case "VTAXCLASS".ToUpper
                        PhysicalUpdateVTAXCLASS()
                    Case "VTAXORDER".ToUpper
                        PhysicalUpdateVTAXORDER()
                    Case "VTAXPHYLUM".ToUpper
                        PhysicalUpdateVTAXPHYLUM()
                    Case "VUSE".ToUpper
                        PhysicalUpdateVUSE()
                    Case "VUSETYPE".ToUpper
                        PhysicalUpdateVUSETYPE()
                    Case Else
                        Throw New Exception("ManagePhysicalUpdates does not cater for " & TableName & " table")
                End Select
                If Status.HasError = True Then
                    Exit For
                End If
            Next
        End Sub

        Private Sub ManagePhysicalInserts()
            For Each TableName As String In Me.TableActionOrder(TableActionOrderEnum.PhysicalInsert)
                Select Case TableName.ToUpper
                    Case "VAQUATIC".ToUpper
                        PhysicalInsertVAQUATIC()
                    Case "VAREAOFUSE".ToUpper
                        PhysicalInsertVAREAOFUSE()
                    Case "VBRU".ToUpper
                        PhysicalInsertVBRU()
                    Case "VCOMMONNAME".ToUpper
                        PhysicalInsertVCOMMONNAME()
                    Case "VCOUNTRY".ToUpper
                        PhysicalInsertVCOUNTRY()
                    Case "VDECISIONS".ToUpper
                        PhysicalInsertVDECISIONS()
                    Case "VDISTRIBAQUA".ToUpper
                        PhysicalInsertVDISTRIBAQUA()
                    Case "VDISTRIBCTY".ToUpper
                        PhysicalInsertVDISTRIBCTY()
                    Case "VFAMILY".ToUpper
                        PhysicalInsertVFAMILY()
                    Case "VGENUS".ToUpper
                        PhysicalInsertVGENUS()
                    Case "VHIGHCOMMON".ToUpper
                        PhysicalInsertVHIGHCOMMON()
                    Case "VHIGHDECISIONS".ToUpper
                        PhysicalInsertVHIGHDECISIONS()
                    Case "VHIGHLEGAL".ToUpper
                        PhysicalInsertVHIGHLEGAL()
                    Case "VHIGHQUOTAS".ToUpper
                        PhysicalInsertVHIGHQUOTAS()
                    Case "VHIGHSYNONYMS".ToUpper
                        PhysicalInsertVHIGHSYNONYMS()
                    Case "VKINGDOM".ToUpper
                        PhysicalInsertVKINGDOM()
                    Case "VLEGAL".ToUpper
                        PhysicalInsertVLEGAL()
                    Case "VLEGALNAME".ToUpper
                        PhysicalInsertVLEGALNAME()
                    Case "VLEVELOFUSE".ToUpper
                        PhysicalInsertVLEVELOFUSE()
                    Case "VNOTIFICATION".ToUpper
                        PhysicalInsertVNOTIFICATION()
                    Case "VPART".ToUpper
                        PhysicalInsertVPART()
                    Case "VQUOTAS".ToUpper
                        PhysicalInsertVQUOTAS()
                    Case "VQUOTASOURCE".ToUpper
                        PhysicalInsertVQUOTASOURCE()
                    Case "VQUOTATERMS".ToUpper
                        PhysicalInsertVQUOTATERMS()
                    Case "VSPECIES".ToUpper
                        PhysicalInsertVSPECIES()
                    Case "VSYNLINK".ToUpper
                        PhysicalInsertVSYNLINK()
                    Case "VTAXCLASS".ToUpper
                        PhysicalInsertVTAXCLASS()
                    Case "VTAXORDER".ToUpper
                        PhysicalInsertVTAXORDER()
                    Case "VTAXPHYLUM".ToUpper
                        PhysicalInsertVTAXPHYLUM()
                    Case "VUSE".ToUpper
                        PhysicalInsertVUSE()
                    Case "VUSETYPE".ToUpper
                        PhysicalInsertVUSETYPE()
                    Case Else
                        Throw New Exception("ManagePhysicalInserts does not cater for " & TableName & " table")
                End Select
                If Status.HasError = True Then
                    Exit For
                End If
            Next
        End Sub

        Private Sub ManagePhysicalDeletes()
            For Each TableName As String In Me.TableActionOrder(TableActionOrderEnum.PhysicalDelete)
                Select Case TableName.ToUpper
                    Case "VAQUATIC".ToUpper
                        PhysicalDeleteVAQUATIC()
                    Case "VAREAOFUSE".ToUpper
                        PhysicalDeleteVAREAOFUSE()
                    Case "VBRU".ToUpper
                        PhysicalDeleteVBRU()
                    Case "VCOMMONNAME".ToUpper
                        PhysicalDeleteVCOMMONNAME()
                    Case "VCOUNTRY".ToUpper
                        PhysicalDeleteVCOUNTRY()
                    Case "VDECISIONS".ToUpper
                        PhysicalDeleteVDECISIONS()
                    Case "VDISTRIBAQUA".ToUpper
                        PhysicalDeleteVDISTRIBAQUA()
                    Case "VDISTRIBCTY".ToUpper
                        PhysicalDeleteVDISTRIBCTY()
                    Case "VFAMILY".ToUpper
                        PhysicalDeleteVFAMILY()
                    Case "VGENUS".ToUpper
                        PhysicalDeleteVGENUS()
                    Case "VHIGHCOMMON".ToUpper
                        PhysicalDeleteVHIGHCOMMON()
                    Case "VHIGHDECISIONS".ToUpper
                        PhysicalDeleteVHIGHDECISIONS()
                    Case "VHIGHLEGAL".ToUpper
                        PhysicalDeleteVHIGHLEGAL()
                    Case "VHIGHQUOTAS".ToUpper
                        PhysicalDeleteVHIGHQUOTAS()
                    Case "VHIGHSYNONYMS".ToUpper
                        PhysicalDeleteVHIGHSYNONYMS()
                    Case "VKINGDOM".ToUpper
                        PhysicalDeleteVKINGDOM()
                    Case "VLEGAL".ToUpper
                        PhysicalDeleteVLEGAL()
                    Case "VLEGALNAME".ToUpper
                        PhysicalDeleteVLEGALNAME()
                    Case "VLEVELOFUSE".ToUpper
                        PhysicalDeleteVLEVELOFUSE()
                    Case "VNOTIFICATION".ToUpper
                        PhysicalDeleteVNOTIFICATION()
                    Case "VPART".ToUpper
                        PhysicalDeleteVPART()
                    Case "VQUOTAS".ToUpper
                        PhysicalDeleteVQUOTAS()
                    Case "VQUOTASOURCE".ToUpper
                        PhysicalDeleteVQUOTASOURCE()
                    Case "VQUOTATERMS".ToUpper
                        PhysicalDeleteVQUOTATERMS()
                    Case "VSPECIES".ToUpper
                        PhysicalDeleteVSPECIES()
                    Case "VSYNLINK".ToUpper
                        PhysicalDeleteVSYNLINK()
                    Case "VTAXCLASS".ToUpper
                        PhysicalDeleteVTAXCLASS()
                    Case "VTAXORDER".ToUpper
                        PhysicalDeleteVTAXORDER()
                    Case "VTAXPHYLUM".ToUpper
                        PhysicalDeleteVTAXPHYLUM()
                    Case "VUSE".ToUpper
                        PhysicalDeleteVUSE()
                    Case "VUSETYPE".ToUpper
                        PhysicalDeleteVUSETYPE()
                    Case Else
                        Throw New Exception("ManagePhysicalDeletes does not cater for " & TableName & " table")
                End Select
                If Status.HasError = True Then
                    Exit For
                End If
            Next
        End Sub

        Private Delegate Function DupProcedureDelegate(ByVal transaction As System.Data.SqlClient.SqlTransaction, ByVal useDataSetType As System.Type) As DataSet
        Private Delegate Function FKProcedureDelegate(ByVal transaction As System.Data.SqlClient.SqlTransaction, ByVal useDataSetType As System.Type) As DataSet
        Private Delegate Function FKDataProcedureDelegate(ByVal transaction As System.Data.SqlClient.SqlTransaction, ByVal useDataSetType As System.Type) As DataSet

        Private Sub ValidateFK(ByVal FKProcedure As FKProcedureDelegate, ByVal ErrorMessage As String)
            Try
                Dim Results As DataSet
                Results = FKProcedure.Invoke(Nothing, GetType(System.Data.DataSet))
                If Results.Tables(0).Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage(ErrorMessage)
                    Dim columns As New System.Text.StringBuilder
                    For Each columnname As DataColumn In Results.Tables(0).Columns
                        columns.Append(columnname.ColumnName & "  ")
                    Next
                    mTaxonomyDataError.AddMessage(columns.ToString.TrimEnd)
                    For Each row As DataRow In Results.Tables(0).Rows
                        columns = New System.Text.StringBuilder
                        For Each columnname As DataColumn In Results.Tables(0).Columns
                            If row.IsNull(columnname) Then
                                columns.Append("NULL  ")
                            Else
                                columns.Append(CType(row.Item(columnname), String) & "  ")
                            End If
                        Next
                        mTaxonomyDataError.AddMessage(columns.ToString)
                    Next row
                End If
            Catch ex As Exception
                Throw New Exception("Error running " & FKProcedure.Method.Name & " validation procedure", ex)
            End Try
        End Sub

        Private Sub ValidateFKData(ByVal FKDataProcedure As FKDataProcedureDelegate, ByVal ErrorMessage As String)
            Try
                Dim Results As DataSet
                Results = FKDataProcedure.Invoke(Nothing, GetType(System.Data.DataSet))
                If Results.Tables(0).Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage(ErrorMessage)
                    Dim columns As New System.Text.StringBuilder
                    For Each columnname As DataColumn In Results.Tables(0).Columns
                        columns.Append(columnname.ColumnName & "  ")
                    Next
                    mTaxonomyDataError.AddMessage(columns.ToString.TrimEnd)
                    For Each row As DataRow In Results.Tables(0).Rows
                        columns = New System.Text.StringBuilder
                        For Each columnname As DataColumn In Results.Tables(0).Columns
                            If row.IsNull(columnname) Then
                                columns.Append("NULL  ")
                            Else
                                columns.Append(CType(row.Item(columnname), String) & "  ")
                            End If
                        Next
                        mTaxonomyDataError.AddMessage(columns.ToString)
                    Next row
                End If
            Catch ex As Exception
                Throw New Exception("Error running " & FKDataProcedure.Method.Name & " validation procedure", ex)
            End Try
        End Sub

        Private Sub ValidateDupRecord(ByVal DupProcedure As DupProcedureDelegate, ByVal ErrorMessage As String)
            Try
                Dim Results As DataSet
                Results = DupProcedure.Invoke(Nothing, GetType(System.Data.DataSet))
                If Results.Tables(0).Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage(ErrorMessage)
                    Dim columns As New System.Text.StringBuilder
                    For Each columnname As DataColumn In Results.Tables(0).Columns
                        columns.Append(columnname.ColumnName & "  ")
                    Next
                    mTaxonomyDataError.AddMessage(columns.ToString.TrimEnd)
                    For Each row As DataRow In Results.Tables(0).Rows
                        columns = New System.Text.StringBuilder
                        For Each columnname As DataColumn In Results.Tables(0).Columns
                            If row.IsNull(columnname) Then
                                columns.Append("NULL  ")
                            Else
                                columns.Append(CType(row.Item(columnname), String) & "  ")
                            End If
                        Next
                        mTaxonomyDataError.AddMessage(columns.ToString)
                    Next row
                End If
            Catch ex As Exception
                Throw New Exception("Error running " & DupProcedure.Method.Name & " validation procedure", ex)
            End Try
        End Sub

        Private Sub ValidateFKs()
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecCla, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecCty, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecFam, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecGen, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecKgm, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecOrd, "The HighDecisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHDecPhy, "The HighDecisions table contains a foreign key problem.")

            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKBRUCty, "The BRUCty table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKClaPhy, "The Phylum table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKComAou, "The Area of Use table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKComSpc, "The Species table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKCtyCty, "The Country table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDaqAqu, "The DistribAqua table contains a foreign key problem with the Country table.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDaqSpc, "The DistribAqua table contains a foreign key problem with the Species table.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDbuBru, "The DistribBRU table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDbuCty, "The DistribBRU table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDbuDct, "The DistribBRU table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDbuSpc, "The DistribBRU table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDbuSpcStatus, "The DistribBRU table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDctCty, "The DistribCTY table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDctSpc, "The DistribCTY table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDecCty, "The Decisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKDecSpc, "The Decisions table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKSynSpcSynonym, "The SynLink table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKSynSpcAccepted, "The SynLink table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKFamOrd, "The Family table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKGenFam, "The Genus table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComAou, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComCla, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComFam, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComGen, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComKgm, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComOrd, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHComPhy, "The HighCommon table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegCla, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegCty, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegFam, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegGen, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegKgm, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegLnm, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegOrd, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHLegPhy, "The HighLegal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynFamAccepted, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynFamSynonym, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynGenAccepted, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynGenSynonym, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynSpcAccepted, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKHSynSpcSynonym, "The HighSynonyms table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKLegCty, "The Legal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKLegLnm, "The Legal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKLegSpc, "The Legal table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKOrdCla, "The Order table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKPhyKgm, "The Phylum table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKQuoCty, "The Quotas table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKQuoNot, "The Quotas table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKQuoQuoSrc, "The Quotas table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKQuoQuoTrm, "The Quotas table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKQuoSpc, "The Quotas table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKSpcGen, "The Species table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKUseLou, "The Use table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKUsePar, "The Use table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKUseSpc, "The Use table contains a foreign key problem.")
            ValidateFK(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKUseUty, "The Use table contains a foreign key problem.")
        End Sub

        Private Sub ValidateDups()
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupAquatic, "The Aquatic table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupAreaOfUse, "The Area of Use table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupBRU, "The BRU table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupClassName, "The Class table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupCommonName, "The Common Name table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupCountryISO2, "The Country table contains duplicate ISO2 rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupCountryISO3, "The Country table contains duplicate ISO3 rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupDistribAqua, "The DistribAqua table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupDistribBRU, "The DistribBRU table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupDistribCty, "The DistribCty table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupFamilyName, "The Family table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupHighCommon, "The HighCommon table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupHighSynonym, "The HighSynonym table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupKingdom, "The Kingdom table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupLegalName, "The Legal table contains duplicate name rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupLevelOfUse, "The Level of Use table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupNotification, "The Notification table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupOrderName, "The Order table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupPart, "The Part table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupPhylumName, "The Phylum table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupQuotaSource, "The QuotaSource table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupQuotaTerms, "The QuotaTerms table contains duplicate rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupSpeciesNotStock, "The Species table contains duplicate non-stock rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupSpeciesStock, "The Species table contains duplicate stock rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupSynlink, "The SynLink table contains duplicate stock rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupUse, "The Use table contains duplicate stock rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupUseType, "The UseType table contains duplicate stock rows")
            ValidateDupRecord(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLDupGenusName, "The Genus table contains duplicate stock rows")
        End Sub

        Private Sub ValidateFKData()
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidBRUCtyISO2, "The BRUCty table contains an ISO2 foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDaqSpcStatus, "The DistribAqua table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDctCtyISO2, "The DistribCTY table contains an ISO2 foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDctSpcStatus, "The DistribCTY table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDecCtyISO2, "The Decisions table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHDecCtyISO2, "The HighDecisions table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDbuCtyISO2, "The DistribBru table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidDecSpcStatus, "The Decisions table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidGenFamStatus, "The Genus table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHSynSpcStatusAccepted, "The HighSyn table contains an Accepted status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHSynSpcStatusSynonym, "The HighSyn table contains a Synonym status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidQuoCtyISO2, "The Quotas table contains a foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidQuoSpcStatus, "The Quotas table contains a Species status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidSpcGenStatus, "The Species table contains a Genus status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidSpcHSynhasAccepted, "The HighSynonym table contains an Accepted status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidSpcSynonymhasAccepted, "The Species synonym contains an Accepted status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidSynSpcStatusAccepted, "The Synonym species contains an Accepted status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidSynSpcStatusSynonym, "The Synonym speceies contains a Synonym status foreign key data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidComCode, "The CommonName table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidDecCode, "The Decisions table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidDecision, "The Decisions table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidDistribAqua, "The DistribAqua table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidDistribBRU, "The DistribBRU table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidDistribCty, "The DistribCty table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidFamStatus, "The Family table contains a status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidGenStatus, "The Genus table contains a status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHComCode, "The HighCommon table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHDecCode, "The HighDecision table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHLegCode, "The HighLegal table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHLegDateListed, "The HighLegal table contains a Date listed data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHLegFamStatus, "The HighLegal table contains a Family status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHLegGenStatus, "The HighLegal table contains a Genus status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHLegListing, "The HighLegal table contains a Listing data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHLegLnmRecID, "The HighLegal table contains a Legal name data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidHLegLnmRecStatus, "The HighLegal table contains a Legal name status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHQuoCode, "The HighQuotas table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHQuoYear, "The HighQuotas table contains a Quota year data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHSynCode, "The HighSynonym table contains a code data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHSynParentCode, "The HighSynonym table contains a parent code data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidHighDecision, "The HighDecision table contains a data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLegDateListed, "The Legal table contains a date listed data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLegListing, "The Legal table contains a Listing data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLegLnmRecID, "The Legal table contains a Legal name data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidLegLnmRecStatus, "The Legal table contains a Legal name status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLFKValidLegSpcStatus, "The Legal table contains a Species status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLnmDates, "The LegalName table contains a date data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLnmLevel, "The LegalName table contains a Level data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidLnmRecStatus, "The LegalName table contains a Status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidQuoUnit, "The Quotas table contains a Unit data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidQuoYear, "The Quotas table contains a Year data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidSpcInfraRank, "The Species table contains an InfraRank data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidSpcStatus, "The Species table contains a Status data problem.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidSpcStatusInconsistent, "The Species table contains inconsistent status data.")
            ValidateFKData(AddressOf [DO].DataObjects.Sprocs.dbo_usp_TDLValidUtyRecStatus, "The UseType table contains a Status data problem.")
        End Sub

        Private Sub ValidateData()
            Try
                If mTaxonomyDataError.HasError = False Then
                    ValidateDups()
                End If
                If mTaxonomyDataError.HasError = False Then
                    ValidateFKs()
                End If
                If mTaxonomyDataError.HasError = False Then
                    ValidateFKData()
                End If
            Catch ex As Exception
                Throw New Exception("Cannot run validation routines", ex)
            End Try
        End Sub

#Region "VAQUATIC"
        Private Sub PhysicalDeleteVAQUATIC()
            Try
                If Me.mTransferDeletes.VAQUATIC.Rows.Count > 0 Then
                    Dim AquaticRegionGen As New DataObjects.Entity.TaxonomyAquaticRegion
                    Dim AquaticRegionService As DataObjects.Service.TaxonomyAquaticRegionService = AquaticRegionGen.ServiceObject
                    For Each Row As Transfer.VAQUATICRow In Me.mTransferDeletes.VAQUATIC.Rows
                        'Get the aquatic region id.
                        Dim AquaticRegion As DataObjects.Entity.TaxonomyAquaticRegion = _
                            AquaticRegionService.GetById(aquaticRegionId:=CType(Row.AQURECID, Int32), Tran:=Me.Transaction)
                        'Since each Aquatic region refers to a note this has to be deleted first.
                        If AquaticRegion.IsAquaticRegionNoteIDNull = False Then
                            Dim NoteGen As New DataObjects.Entity.Note
                            Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                            NoteService.DeleteById(noteid:=AquaticRegion.AquaticRegionNoteID, checksum:=0, Transaction:=Me.Transaction)
                        End If
                        'Delete the aquatic region.
                        AquaticRegionService.DeleteById(aquaticRegionID:=CType(Row.AQURECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVAQUATIC")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVAQUATIC", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVAQUATIC()
            Try
                If Me.mTransferInserts.VAQUATIC.Rows.Count > 0 Then
                    Dim AquaticRegionGen As New DataObjects.Entity.TaxonomyAquaticRegion
                    Dim AquaticRegionService As DataObjects.Service.TaxonomyAquaticRegionService = AquaticRegionGen.ServiceObject
                    For Each NewValue As Transfer.VAQUATICRow In Me.mTransferInserts.VAQUATIC.Rows
                        Dim InsNoteID As Object = Nothing
                        'Check if there is a note to store.
                        If NewValue.AQUNOTES.Length > 0 Then
                            'Within the physical database the aquatic region record does not contain a note but instead points to the generic notes table.
                            Dim NoteGen As New DataObjects.Entity.Note
                            Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                            'Create the note that is pointed to by this aquatic region record.
                            InsNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.AQUNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                        End If
                        'Insert the Aquatic region record.
                        AquaticRegionService.Insert(aquaticRegionID:=CType(NewValue.AQURECID, Int32), regionname:=NewValue.AQUNAME, regionsubname:=NewValue.AQUSUBNAME, regiontype:=NewValue.AQUTYPE, aquaticregionnoteid:=InsNoteID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVAQUATIC")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVAQUATIC", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVAQUATIC()
            Try
                If Me.mTransferUpdates.VAQUATIC.Rows.Count > 0 Then
                    Dim AquaticRegionGen As New DataObjects.Entity.TaxonomyAquaticRegion
                    Dim AquaticRegionService As DataObjects.Service.TaxonomyAquaticRegionService = AquaticRegionGen.ServiceObject
                    For Each NewValue As Transfer.VAQUATICRow In Me.mTransferUpdates.VAQUATIC.Rows
                        'Get the aquatic region record to be be updated.
                        Dim AquaticRegion As DataObjects.Entity.TaxonomyAquaticRegion = _
                            AquaticRegionService.GetById(aquaticRegionID:=CType(NewValue.AQURECID, Int32), Tran:=Me.Transaction)
                        'Within the physical database the aquatic region record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject

                        'Retrieve the note that is pointed to by this aquatic region record.
                        Dim Note As DataObjects.Entity.Note = NoteService.GetById(noteid:=AquaticRegion.AquaticRegionNoteID, tran:=Me.Transaction)
                        Dim UpdNoteID As Object = Nothing
                        'Check if there is a note retrieved.
                        If Note Is Nothing = True Then
                            'Check if there is a note to store.
                            If String.Compare("", NewValue.AQUNOTES) <> 0 Then
                                'There is a note to store so store it.
                                UpdNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.AQUNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction)
                            End If
                        Else
                            'Check if the the note content has changed.
                            If String.Compare(Note.Content, NewValue.AQUNOTES) <> 0 Then
                                'The note content has changed so update the note.
                                NoteService.Update(id:=Note.Id, [Date]:=Note.Date, content:=NewValue.AQUNOTES, isreadonly:=Note.IsReadOnly, important:=Note.Important, subject:=Note.Subject, modifiedby:=GetUser, modifieddate:=GetModifiedDate, createdby:=Note.CreatedBy, createddate:=Note.CreatedDate, active:=Note.Active, checksum:=Note.CheckSum, Transaction:=Me.Transaction)
                                UpdNoteID = Note.Id
                            End If
                        End If
                        'Update the Aquatic region record.
                        AquaticRegionService.Update(aquaticregionid:=CType(NewValue.AQURECID, Int32), regionname:=NewValue.AQUNAME, regionsubname:=NewValue.AQUSUBNAME, regiontype:=NewValue.AQUTYPE, aquaticregionnoteid:=UpdNoteID, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVAQUATIC")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVAQUATIC", Ex)
            End Try
        End Sub
#End Region

#Region "VAREAOFUSE"
        'TODO: Nick - add named arguments to the procedures in this region.
        Private Sub PhysicalDeleteVAREAOFUSE()
            Try
                If Me.mTransferDeletes.VAREAOFUSE.Rows.Count > 0 Then
                    Dim AreaOfUseGen As New DataObjects.Entity.TaxonomyAreaOfUse
                    Dim AreaOfUseService As DataObjects.Service.TaxonomyAreaOfUseService = AreaOfUseGen.ServiceObject
                    For Each Row As Transfer.VAREAOFUSERow In Me.mTransferDeletes.VAREAOFUSE.Rows
                        'Delete the Area of Use.
                        AreaOfUseService.DeleteById(CType(Row.AOURECID, Int32), 0, Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVAREAOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVAREAOFUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVAREAOFUSE()
            Try
                If Me.mTransferInserts.VAREAOFUSE.Rows.Count > 0 Then
                    Dim AreaOfUseGen As New DataObjects.Entity.TaxonomyAreaOfUse
                    Dim AreaOfUseService As DataObjects.Service.TaxonomyAreaOfUseService = AreaOfUseGen.ServiceObject
                    For Each Row As Transfer.VAREAOFUSERow In Me.mTransferInserts.VAREAOFUSE.Rows
                        'Insert the Area of Use.
                        AreaOfUseService.Insert(CType(Row.AOURECID, Int32), Row.AOUDESC, Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVAREAOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVAREAOFUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVAREAOFUSE()
            Try
                If Me.mTransferUpdates.VAREAOFUSE.Rows.Count > 0 Then
                    Dim AreaOfUseGen As New DataObjects.Entity.TaxonomyAreaOfUse
                    Dim AreaOfUseService As DataObjects.Service.TaxonomyAreaOfUseService = AreaOfUseGen.ServiceObject
                    For Each Row As Transfer.VAREAOFUSERow In Me.mTransferUpdates.VAREAOFUSE.Rows
                        'Update the Area of Use.
                        AreaOfUseService.Update(CType(Row.AOURECID, Int32), Row.AOUDESC, 0, Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVAREAOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVAREAOFUSE", Ex)
            End Try
        End Sub

#End Region

#Region "VCOUNTRY"
        Private Sub PhysicalDeleteVCOUNTRY()
            Try
                'This is a special case update the record and mark it as inactive instead of deleting it.
                If Me.mTransferDeletes.VCOUNTRY.Rows.Count > 0 Then
                    Dim CountryGen As New DataObjects.Entity.Country
                    Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                    For Each Row As Transfer.VCOUNTRYRow In Me.mTransferDeletes.VCOUNTRY.Rows
                        'Get the row from the database since certain values may have changed that are not supplied from UNEP.
                        Dim OldValue As DataObjects.Entity.Country
                        OldValue = CountryService.GetById(countryid:=CType(Row.CTYRECID, Int32), tran:=Me.Transaction)
                        'Update the Country.
                        CountryService.Update(id:=OldValue.Id, iso2countrycode:=OldValue.ISO2CountryCode, iso3countrycode:=OldValue.ISO3CountryCode, shortname:=OldValue.ShortName, longname:=OldValue.LongName, active:=False, countrybru:=OldValue.CountryBRU, iso3166:=OldValue.ISO3166, managementCountryid:=OldValue.ManagementCountryId, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVCountry")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVCountry", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVCOUNTRY()
            Try
                If Me.mTransferInserts.VCOUNTRY.Rows.Count > 0 Then
                    Dim CountryGen As New DataObjects.Entity.Country
                    Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                    Dim CountriesToInsertLater As New ArrayList(Me.mTransferInserts.VCOUNTRY.Rows)
                    For Each NewValue As Transfer.VCOUNTRYRow In Me.mTransferInserts.VCOUNTRY.Rows
                        'Some countries have to wait until others are inserted because of the parent - child relationship so check for that.
                        If newvalue.CTYPARENTISO3.Length > 0 Then
                            'This country has a parent so insert it later.
                            CountriesToInsertLater.Add(newvalue)
                        Else
                            'This country does not have a parent so it can be inserted now.
                            CountryService.Insert(iso2countrycode:=NewValue.CTYISO2, iso3countrycode:=NewValue.CTYISO3, shortname:=NewValue.CTYSHORT, longname:=NewValue.CTYLONG, active:=True, countrybru:=NewValue.CTYBRU, iso3166:=NewValue.CTYISO3166, managementCountryid:=NewValue.CTYPARENTISO3, Transaction:=Me.Transaction)
                        End If
                    Next
                    'Now the countries that had to wait to be inserted can be inserted but they still have to be inserted in the correct order.
                    If CountriesToInsertLater.Count > 0 Then
                        Dim MaxAttempts As Int32 = CountriesToInsertLater.Count
                        Dim Attempt As Int32 = 1
                        While CountriesToInsertLater.Count > 0 AndAlso Attempt <= MaxAttempts
                            For Each NewValue As Transfer.VCOUNTRYRow In CountriesToInsertLater
                                'Check if the parent that manages this country exists in the database.
                                Dim ParentCountry As DataObjects.Entity.Country = _
                                    CountryService.GetByIndex_IX_Country_2(iso3countrycode:=NewValue.CTYPARENTISO3, includeinactive:=False, Transaction:=Me.Transaction).Entities(0)
                                If ParentCountry Is Nothing = False Then
                                    'The parent that manages this country does exist, so the child country can be inserted.
                                    CountryService.Insert(iso2countrycode:=NewValue.CTYISO2, iso3countrycode:=NewValue.CTYISO3, shortname:=NewValue.CTYSHORT, longname:=NewValue.CTYLONG, active:=True, countrybru:=NewValue.CTYBRU, iso3166:=NewValue.CTYISO3166, managementCountryid:=NewValue.CTYPARENTISO3, Transaction:=Me.Transaction)
                                    CountriesToInsertLater.Remove(NewValue)
                                End If
                            Next
                            Attempt += 1
                        End While
                    End If
                    'Check that all the countries have been inserted.
                    If CountriesToInsertLater.Count > 0 Then
                        'Not all countries have been inserted so report the problem.
                        mTaxonomyDataError.AddMessage("No parent rows exist in the Country table for the following countries so they cannot be inserted")
                        mTaxonomyDataError.AddMessage(GetColumnsNameString(Me.mTransferInserts.VCOUNTRY.Columns))
                        For Each CountryNotInserted As Transfer.VCOUNTRYRow In CountriesToInsertLater
                            mTaxonomyDataError.AddMessage(GetColumnsDataString(CountryNotInserted))
                        Next
                    Else
                        'All countries have been inserted so continue as normal.
                        SaveTransaction("PhysicalInsertVCOUNTRY")
                    End If
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVCOUNTRY", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVCOUNTRY()
            Try
                If Me.mTransferUpdates.VCOUNTRY.Rows.Count > 0 Then
                    Dim CountryGen As New DataObjects.Entity.Country
                    Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                    For Each NewValue As Transfer.VCOUNTRYRow In Me.mTransferUpdates.VCOUNTRY.Rows
                        'Get the row from the database since certain values may have changed that are not supplied from UNEP.
                        Dim OldValue As DataObjects.Entity.Country
                        OldValue = CountryService.GetById(countryid:=CType(NewValue.CTYRECID, Int32), tran:=Me.Transaction)
                        'Update the Country.
                        CountryService.Update(id:=CType(NewValue.CTYRECID, Int32), iso2countrycode:=NewValue.CTYISO2, iso3countrycode:=NewValue.CTYISO3, shortname:=NewValue.CTYSHORT, longname:=NewValue.CTYLONG, active:=OldValue.Active, countrybru:=NewValue.CTYBRU, iso3166:=NewValue.CTYISO3166, managementCountryid:=NewValue.CTYPARENTISO3, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVCOUNTRY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVCOUNTRY", Ex)
            End Try
        End Sub
#End Region

#Region "VBRU"
        Private Sub PhysicalDeleteVBRU()
            Try
                If Me.mTransferDeletes.VBRU.Rows.Count > 0 Then
                    Dim BRUGen As New DataObjects.Entity.TaxonomyBRU
                    Dim BRUService As DataObjects.Service.TaxonomyBRUService = BRUGen.ServiceObject
                    For Each Row As Transfer.VBRURow In Me.mTransferDeletes.VBRU.Rows
                        'Delete the BRU.
                        BRUService.DeleteById(bruid:=CType(Row.BRURECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVBRU", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVBRU()
            Try
                If Me.mTransferInserts.VBRU.Rows.Count > 0 Then
                    Dim BRUGen As New DataObjects.Entity.TaxonomyBRU
                    Dim BRUService As DataObjects.Service.TaxonomyBRUService = BRUGen.ServiceObject
                    For Each Row As Transfer.VBRURow In Me.mTransferInserts.VBRU.Rows
                        'Insert the Area of Use.
                        BRUService.Insert(bruID:=CType(Row.BRURECID, Int32), wcmcname:=Row.BRUWCMCNAME, level3name:=Row.BRULEVEL3NAME, level4code:=Row.BRULEVEL4CODE, level4name:=Row.BRULEVEL4NAME, countryid:=CType(Row.BRUCTYRECID, Int32), valid:=Row.BRUVALID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVBRU", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVBRU()
            Try
                If Me.mTransferUpdates.VBRU.Rows.Count > 0 Then
                    Dim BRUGen As New DataObjects.Entity.TaxonomyBRU
                    Dim BRUService As DataObjects.Service.TaxonomyBRUService = BRUGen.ServiceObject
                    For Each Row As Transfer.VBRURow In Me.mTransferUpdates.VBRU.Rows
                        'Update BRU.
                        BRUService.Update(bruID:=CType(Row.BRURECID, Int32), wcmcname:=Row.BRUWCMCNAME, level3name:=Row.BRULEVEL3NAME, level4code:=Row.BRULEVEL4CODE, level4name:=Row.BRULEVEL4NAME, countryid:=CType(Row.BRUCTYRECID, Int32), valid:=Row.BRUVALID, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVBRU", Ex)
            End Try
        End Sub

#End Region

#Region "VLEVELOFUSE"
        Private Sub PhysicalDeleteVLEVELOFUSE()
            Try
                If Me.mTransferDeletes.VLEVELOFUSE.Rows.Count > 0 Then
                    Dim LevelOfUseGen As New DataObjects.Entity.TaxonomyLevelOfUse
                    Dim LevelOfUseService As DataObjects.Service.TaxonomyLevelOfUseService = LevelOfUseGen.ServiceObject
                    For Each Row As Transfer.VLEVELOFUSERow In Me.mTransferDeletes.VLEVELOFUSE.Rows
                        'Delete the LevelOfUse.
                        LevelOfUseService.DeleteById(id:=CType(Row.LOURECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVLEVELOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVLEVELOFUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVLEVELOFUSE()
            Try
                If Me.mTransferInserts.VLEVELOFUSE.Rows.Count > 0 Then
                    Dim LevelOfUseGen As New DataObjects.Entity.TaxonomyLevelOfUse
                    Dim LevelOfUseService As DataObjects.Service.TaxonomyLevelOfUseService = LevelOfUseGen.ServiceObject
                    For Each Row As Transfer.VLEVELOFUSERow In Me.mTransferInserts.VLEVELOFUSE.Rows
                        'Insert the Level of Use.
                        LevelOfUseService.Insert(ID:=CType(Row.LOURECID, Int32), levelofusedescription:=Row.LOUDESC, active:=True, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVLEVELOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVLEVELOFUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVLEVELOFUSE()
            Try
                If Me.mTransferUpdates.VLEVELOFUSE.Rows.Count > 0 Then
                    Dim LevelOfUseGen As New DataObjects.Entity.TaxonomyLevelOfUse
                    Dim LevelOfUseService As DataObjects.Service.TaxonomyLevelOfUseService = LevelOfUseGen.ServiceObject
                    For Each Row As Transfer.VLEVELOFUSERow In Me.mTransferUpdates.VLEVELOFUSE.Rows
                        'Get the record to be updated.
                        Dim LevelOfUseOld As DataObjects.Entity.TaxonomyLevelOfUse = LevelOfUseService.GetById(id:=CType(Row.LOURECID, Int32), tran:=Me.Transaction)
                        'Update LevelOfUse.
                        LevelOfUseService.Update(ID:=CType(Row.LOURECID, Int32), levelofusedescription:=Row.LOUDESC, active:=LevelOfUseOld.Active, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVLEVELOFUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVLEVELOFUSE", Ex)
            End Try
        End Sub
#End Region

#Region "VPART"
        Private Sub PhysicalDeleteVPART()
            Try
                If Me.mTransferDeletes.VPART.Rows.Count > 0 Then
                    Dim PartGen As New DataObjects.Entity.TaxonomyPart
                    Dim PartService As DataObjects.Service.TaxonomyPartService = PartGen.ServiceObject
                    For Each Row As Transfer.VPARTRow In Me.mTransferDeletes.VPART.Rows
                        'Delete the Part.
                        PartService.DeleteById(id:=CType(Row.PARRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVPART")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVPART", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVPART()
            Try
                If Me.mTransferInserts.VPART.Rows.Count > 0 Then
                    Dim PartGen As New DataObjects.Entity.TaxonomyPart
                    Dim PartService As DataObjects.Service.TaxonomyPartService = PartGen.ServiceObject
                    For Each Row As Transfer.VPARTRow In Me.mTransferInserts.VPART.Rows
                        'Insert the Part.
                        PartService.Insert(ID:=CType(Row.PARRECID, Int32), active:=True, partdescription:=Row.PARDESC, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVPART")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVPART", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVPART()
            Try
                If Me.mTransferUpdates.VPART.Rows.Count > 0 Then
                    Dim PartGen As New DataObjects.Entity.TaxonomyPart
                    Dim PartService As DataObjects.Service.TaxonomyPartService = PartGen.ServiceObject
                    For Each Row As Transfer.VPARTRow In Me.mTransferUpdates.VPART.Rows
                        Dim OldPart As DataObjects.Entity.TaxonomyPart = PartService.GetById(CType(Row.PARRECID, Int32))
                        'Update Part.
                        PartService.Update(ID:=CType(Row.PARRECID, Int32), Partdescription:=Row.PARDESC, active:=OldPart.Active, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVPART")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVPART", Ex)
            End Try
        End Sub
#End Region

#Region "VUSETYPE"
        Private Sub PhysicalDeleteVUSETYPE()
            Try
                If Me.mTransferDeletes.VUSETYPE.Rows.Count > 0 Then
                    Dim UseTypeGen As New DataObjects.Entity.TaxonomyUsageType
                    Dim UseTypeService As DataObjects.Service.TaxonomyUsageTypeService = UseTypeGen.ServiceObject
                    For Each Row As Transfer.VUSETYPERow In Me.mTransferDeletes.VUSETYPE.Rows
                        'Delete the UseType.
                        UseTypeService.DeleteById(id:=CType(Row.UTYRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVUSETYPE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVUSETYPE", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVUSETYPE()
            Try
                If Me.mTransferInserts.VUSETYPE.Rows.Count > 0 Then
                    Dim UseTypeGen As New DataObjects.Entity.TaxonomyUsageType
                    Dim UseTypeService As DataObjects.Service.TaxonomyUsageTypeService = UseTypeGen.ServiceObject
                    For Each Row As Transfer.VUSETYPERow In Me.mTransferInserts.VUSETYPE.Rows
                        'Insert the UseType.
                        UseTypeService.Insert(ID:=CType(Row.UTYRECID, Int32), usagetypedescription:=Row.UTYDESC, usagetypestatus:=Row.UTYRECSTATUS, active:=True, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVUSETYPE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVUSETYPE", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVUSETYPE()
            Try
                If Me.mTransferUpdates.VUSETYPE.Rows.Count > 0 Then
                    Dim UseTypeGen As New DataObjects.Entity.TaxonomyUsageType
                    Dim UseTypeService As DataObjects.Service.TaxonomyUsageTypeService = UseTypeGen.ServiceObject
                    For Each Row As Transfer.VUSETYPERow In Me.mTransferUpdates.VUSETYPE.Rows
                        Dim OldUseType As DataObjects.Entity.TaxonomyUsageType = UseTypeService.GetById(CType(Row.UTYRECID, Int32))
                        'Update UseType.
                        UseTypeService.Update(ID:=CType(Row.UTYRECID, Int32), usagetypedescription:=Row.UTYDESC, usagetypestatus:=Row.UTYRECSTATUS, active:=OldUseType.Active, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVUSETYPE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVUSETYPE", Ex)
            End Try
        End Sub
#End Region

#Region "VNOTIFICATION"
        Private Sub PhysicalDeleteVNOTIFICATION()
            Try
                If Me.mTransferDeletes.VNOTIFICATION.Rows.Count > 0 Then
                    Dim CITESNotificationGen As New DataObjects.Entity.TaxonomyCITESNotification
                    Dim CITESNotificationService As DataObjects.Service.TaxonomyCITESNotificationService = CITESNotificationGen.ServiceObject
                    For Each Row As Transfer.VNOTIFICATIONRow In Me.mTransferDeletes.VNOTIFICATION.Rows
                        'Delete the CITESNotification.
                        CITESNotificationService.DeleteById(citesnotificationid:=CType(Row.NOTRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVNOTIFICATION")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVNOTIFICATION", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVNOTIFICATION()
            Try
                If Me.mTransferInserts.VNOTIFICATION.Rows.Count > 0 Then
                    Dim NotificationGen As New DataObjects.Entity.TaxonomyCITESNotification
                    Dim NotificationService As DataObjects.Service.TaxonomyCITESNotificationService = NotificationGen.ServiceObject
                    For Each Row As Transfer.VNOTIFICATIONRow In Me.mTransferInserts.VNOTIFICATION.Rows
                        'Insert the Notification.
                        NotificationService.Insert(citesNotificationID:=CType(Row.NOTRECID, Int32), citesnotificationname:=Row.NOTNAME, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVNOTIFICATION")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVNOTIFICATION", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVNOTIFICATION()
            Try
                If Me.mTransferUpdates.VNOTIFICATION.Rows.Count > 0 Then
                    Dim NotificationGen As New DataObjects.Entity.TaxonomyCITESNotification
                    Dim NotificationService As DataObjects.Service.TaxonomyCITESNotificationService = NotificationGen.ServiceObject
                    For Each Row As Transfer.VNOTIFICATIONRow In Me.mTransferUpdates.VNOTIFICATION.Rows
                        'Update Notification.
                        NotificationService.Update(citesNotificationID:=CType(Row.NOTRECID, Int32), citesnotificationname:=Row.NOTNAME, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVNOTIFICATION")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVNOTIFICATION", Ex)
            End Try
        End Sub
#End Region

#Region "VQUOTASOURCE"
        Private Sub PhysicalDeleteVQUOTASOURCE()
            Try
                If Me.mTransferDeletes.VQUOTASOURCE.Rows.Count > 0 Then
                    Dim ExportQuotaSourceGen As New DataObjects.Entity.TaxonomyExportQuotaSource
                    Dim ExportQuotaSourceService As DataObjects.Service.TaxonomyExportQuotaSourceService = ExportQuotaSourceGen.ServiceObject
                    For Each Row As Transfer.VQUOTASOURCERow In Me.mTransferDeletes.VQUOTASOURCE.Rows
                        'Delete the ExportQuotaSource.
                        ExportQuotaSourceService.DeleteById(id:=CType(Row.QUOSRCRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVQUOTASOURCE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVQUOTASOURCE", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVQUOTASOURCE()
            Try
                If Me.mTransferInserts.VQUOTASOURCE.Rows.Count > 0 Then
                    Dim QuotaSourceGen As New DataObjects.Entity.TaxonomyExportQuotaSource
                    Dim QuotaSourceService As DataObjects.Service.TaxonomyExportQuotaSourceService = QuotaSourceGen.ServiceObject
                    For Each Row As Transfer.VQUOTASOURCERow In Me.mTransferInserts.VQUOTASOURCE.Rows
                        'Insert the QuotaSource.
                        QuotaSourceService.Insert(ID:=CType(Row.QUOSRCRECID, Int32), exportquotasource:=Row.QUOSRCSOURCE, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVQUOTASOURCE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVQUOTASOURCE", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVQUOTASOURCE()
            Try
                If Me.mTransferUpdates.VQUOTASOURCE.Rows.Count > 0 Then
                    Dim QuotaSourceGen As New DataObjects.Entity.TaxonomyExportQuotaSource
                    Dim QuotaSourceService As DataObjects.Service.TaxonomyExportQuotaSourceService = QuotaSourceGen.ServiceObject
                    For Each Row As Transfer.VQUOTASOURCERow In Me.mTransferUpdates.VQUOTASOURCE.Rows
                        'Update QuotaSource.
                        QuotaSourceService.Update(ID:=CType(Row.QUOSRCRECID, Int32), exportquotasource:=Row.QUOSRCSOURCE, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVQUOTASOURCE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVQUOTASOURCE", Ex)
            End Try
        End Sub
#End Region

#Region "VQUOTATERMS"
        Private Sub PhysicalDeleteVQUOTATERMS()
            Try
                If Me.mTransferDeletes.VQUOTATERMS.Rows.Count > 0 Then
                    Dim QuotaTermGen As New DataObjects.Entity.TaxonomyExportQuotaTerm
                    Dim QuotaTermService As DataObjects.Service.TaxonomyExportQuotaTermService = QuotaTermGen.ServiceObject
                    For Each Row As Transfer.VQUOTATERMSRow In Me.mTransferDeletes.VQUOTATERMS.Rows
                        'Delete the ExportQuotaTerm.
                        QuotaTermService.DeleteById(id:=CType(Row.QUOTRMRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVQUOTATERMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVQUOTATERMS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVQUOTATERMS()
            Try
                If Me.mTransferInserts.VQUOTATERMS.Rows.Count > 0 Then
                    Dim QuotaTermsGen As New DataObjects.Entity.TaxonomyExportQuotaTerm
                    Dim QuotaTermsService As DataObjects.Service.TaxonomyExportQuotaTermService = QuotaTermsGen.ServiceObject
                    For Each Row As Transfer.VQUOTATERMSRow In Me.mTransferInserts.VQUOTATERMS.Rows
                        'Insert the QuotaTerms.
                        QuotaTermsService.Insert(ID:=CType(Row.QUOTRMRECID, Int32), exportQuotaTerm:=Row.QUOTRMTERM, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVQUOTATERMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVQUOTATERMS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVQUOTATERMS()
            Try
                If Me.mTransferUpdates.VQUOTATERMS.Rows.Count > 0 Then
                    Dim QuotaTermsGen As New DataObjects.Entity.TaxonomyExportQuotaTerm
                    Dim QuotaTermsService As DataObjects.Service.TaxonomyExportQuotaTermService = QuotaTermsGen.ServiceObject
                    For Each Row As Transfer.VQUOTATERMSRow In Me.mTransferUpdates.VQUOTATERMS.Rows
                        'Update QuotaTerms.
                        QuotaTermsService.Update(ID:=CType(Row.QUOTRMRECID, Int32), exportQuotaTerm:=Row.QUOTRMTERM, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVQUOTATERMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVQUOTATERMS", Ex)
            End Try
        End Sub
#End Region

#Region "VLEGALNAME"
        Private Sub PhysicalDeleteVLEGALNAME()
            Try
                If Me.mTransferDeletes.VLEGALNAME.Rows.Count > 0 Then
                    Dim LegalNameGen As New DataObjects.Entity.TaxonomyLegislationName
                    Dim LegalNameService As DataObjects.Service.TaxonomyLegislationNameService = LegalNameGen.ServiceObject
                    For Each Row As Transfer.VLEGALNAMERow In Me.mTransferDeletes.VLEGALNAME.Rows
                        'Delete the ExportLegalName.
                        LegalNameService.DeleteById(legislationnameid:=CType(Row.LNMRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVLEGALNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVLEGALNAME", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVLEGALNAME()
            Try
                If Me.mTransferInserts.VLEGALNAME.Rows.Count > 0 Then
                    Dim LegalNameGen As New DataObjects.Entity.TaxonomyLegislationName
                    Dim LegalNameService As DataObjects.Service.TaxonomyLegislationNameService = LegalNameGen.ServiceObject
                    For Each Row As Transfer.VLEGALNAMERow In Me.mTransferInserts.VLEGALNAME.Rows
                        'Insert the LegalName.
                        LegalNameService.Insert(legislationnameID:=CType(Row.LNMRECID, Int32), legislationshortname:=Row.LNMSHORTDESC, legislationlongname:=Row.LNMLONGDESC, legislationlevel:=Row.LNMLEVEL, legislationdateadopted:=Row.LNMDATEADOPT, legislationdateenforced:=Row.LNMDATEENFORCE, legislationurl:=Row.LNMURL, legislationnamestatus:=Row.LNMRECSTATUS, [note]:=Row.LNMNOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVLEGALNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVLEGALNAME", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVLEGALNAME()
            Try
                If Me.mTransferUpdates.VLEGALNAME.Rows.Count > 0 Then
                    Dim LegalNameGen As New DataObjects.Entity.TaxonomyLegislationName
                    Dim LegalNameService As DataObjects.Service.TaxonomyLegislationNameService = LegalNameGen.ServiceObject
                    For Each Row As Transfer.VLEGALNAMERow In Me.mTransferUpdates.VLEGALNAME.Rows
                        'Update LegalNames.
                        LegalNameService.Update(legislationnameID:=CType(Row.LNMRECID, Int32), legislationshortname:=Row.LNMSHORTDESC, legislationlongname:=Row.LNMLONGDESC, legislationlevel:=Row.LNMLEVEL, legislationdateadopted:=Row.LNMDATEADOPT, legislationdateenforced:=Row.LNMDATEENFORCE, legislationurl:=Row.LNMURL, legislationnamestatus:=Row.LNMRECSTATUS, [note]:=Row.LNMNOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVLEGALNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVLEGALNAME", Ex)
            End Try
        End Sub
#End Region

#Region "VKINGDOM"
        Private Sub PhysicalDeleteVKINGDOM()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VKINGDOM.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Kingdom rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VKINGDOMRow In Me.mTransferDeletes.VKINGDOM.Rows
                        mTaxonomyDataError.AddMessage("KGMRECID: " & Row.KGMRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVKINGDOM", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVKINGDOM()
            Try
                If Me.mTransferInserts.VKINGDOM.Rows.Count > 0 Then
                    Dim KingdomGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim KingdomService As DataObjects.Service.TaxonomyTaxonService = KingdomGen.ServiceObject
                    For Each Row As Transfer.VKINGDOMRow In Me.mTransferInserts.VKINGDOM.Rows
                        'Insert the Kingdom.
                        KingdomService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.KGMRECID, Int32), taxontypeid:=GetKingdomTaxonTypeID, epithettype:=Nothing, taxonname:=Row.KGMNAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus("A"), parentkingdomid:=Nothing, parenttaxonid:=Nothing, parenttaxontypeid:=Nothing, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVKINGDOM")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVKINGDOM", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVKINGDOM()
            Try
                If Me.mTransferUpdates.VKINGDOM.Rows.Count > 0 Then
                    Dim KingdomGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim KingdomService As DataObjects.Service.TaxonomyTaxonService = KingdomGen.ServiceObject
                    For Each Row As Transfer.VKINGDOMRow In Me.mTransferUpdates.VKINGDOM.Rows
                        'Get the kingdom to update.
                        'TODO: Nick - Ask steve why even though I have a unique (non clustered) index there is no getbyindex function that returns a single row.
                        Dim OldKingdom As DataObjects.Entity.TaxonomyTaxon = KingdomService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.KGMRECID, Int32), taxontypeid:=GetKingdomTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Kingdoms.
                        KingdomService.Update(ID:=OldKingdom.Id, kingdomid:=OldKingdom.KingdomID, taxonid:=OldKingdom.TaxonID, taxontypeid:=OldKingdom.TaxonTypeID, epithettype:=OldKingdom.EpithetType, taxonname:=Row.KGMNAME, taxonauthor:=OldKingdom.TaxonAuthor, taxonstatusid:=OldKingdom.TaxonStatusID, parentkingdomid:=OldKingdom.ParentKingdomID, parenttaxonid:=OldKingdom.ParentTaxonID, parenttaxontypeid:=OldKingdom.ParentTaxonTypeID, distributioncomplete:=OldKingdom.DistributionComplete, citesreference:=OldKingdom.CITESReference, lineage:=OldKingdom.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVKINGDOM")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVKINGDOM", Ex)
            End Try
        End Sub
#End Region

#Region "VTAXPHYLUM"
        Private Sub PhysicalDeleteVTAXPHYLUM()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VTAXPHYLUM.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Phylum rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VTAXPHYLUMRow In Me.mTransferDeletes.VTAXPHYLUM.Rows
                        mTaxonomyDataError.AddMessage("PHYRECID: " & Row.PHYRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVTAXPHYLUM", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVTAXPHYLUM()
            Try
                If Me.mTransferInserts.VTAXPHYLUM.Rows.Count > 0 Then
                    Dim PhylumGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim PhylumService As DataObjects.Service.TaxonomyTaxonService = PhylumGen.ServiceObject
                    For Each Row As Transfer.VTAXPHYLUMRow In Me.mTransferInserts.VTAXPHYLUM.Rows
                        'Get the parent kingdom.
                        Dim ParentKingdom As DataObjects.Entity.TaxonomyTaxon = PhylumService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.PHYKGMRECID, Int32), taxontypeid:=GetKingdomTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Phylum.
                        PhylumService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.PHYRECID, Int32), taxontypeid:=GetPhylumTaxonTypeID, epithettype:=Nothing, taxonname:=Row.PHYNAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus("A"), parentkingdomid:=ParentKingdom.KingdomID, parenttaxonid:=ParentKingdom.TaxonID, parenttaxontypeid:=ParentKingdom.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVTAXPHYLUM")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVTAXPHYLUM", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVTAXPHYLUM()
            Try
                If Me.mTransferUpdates.VTAXPHYLUM.Rows.Count > 0 Then
                    Dim PhylumGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim PhylumService As DataObjects.Service.TaxonomyTaxonService = PhylumGen.ServiceObject
                    For Each Row As Transfer.VTAXPHYLUMRow In Me.mTransferUpdates.VTAXPHYLUM.Rows
                        'Get the Phylum to update.
                        Dim OldPhylum As DataObjects.Entity.TaxonomyTaxon = PhylumService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=GetKingdomID, taxonid:=CType(Row.PHYRECID, Int32), taxontypeid:=GetPhylumTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Get the parent class.
                        Dim ParentKingdom As DataObjects.Entity.TaxonomyTaxon = PhylumService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.PHYKGMRECID, Int32), taxontypeid:=GetKingdomTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Phylums.
                        PhylumService.Update(ID:=OldPhylum.Id, Kingdomid:=OldPhylum.KingdomID, taxonid:=OldPhylum.TaxonID, taxontypeid:=OldPhylum.TaxonTypeID, epithettype:=OldPhylum.EpithetType, taxonname:=Row.PHYNAME, taxonauthor:=OldPhylum.TaxonAuthor, taxonstatusid:=OldPhylum.TaxonStatusID, parentkingdomid:=ParentKingdom.KingdomID, parenttaxonid:=ParentKingdom.TaxonID, parenttaxontypeid:=ParentKingdom.TaxonTypeID, distributioncomplete:=OldPhylum.DistributionComplete, citesreference:=OldPhylum.CITESReference, lineage:=OldPhylum.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVTAXPHYLUM")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVTAXPHYLUM", Ex)
            End Try
        End Sub

#End Region

#Region "VTAXCLASS"

        Private Sub PhysicalDeleteVTAXCLASS()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VTAXCLASS.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Class rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VTAXCLASSRow In Me.mTransferDeletes.VTAXCLASS.Rows
                        mTaxonomyDataError.AddMessage("CLARECID: " & Row.CLARECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVTAXCLASS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVTAXCLASS()
            Try
                If Me.mTransferInserts.VTAXCLASS.Rows.Count > 0 Then
                    Dim ClassGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim ClassService As DataObjects.Service.TaxonomyTaxonService = ClassGen.ServiceObject
                    For Each Row As Transfer.VTAXCLASSRow In Me.mTransferInserts.VTAXCLASS.Rows
                        'Get the parent Phylum.
                        Dim ParentPhylum As DataObjects.Entity.TaxonomyTaxon = ClassService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.CLAPHYRECID, Int32), taxontypeid:=GetPhylumTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Class.
                        ClassService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.CLARECID, Int32), taxontypeid:=GetClassTaxonTypeID, epithettype:=Nothing, taxonname:=Row.CLANAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus("A"), parentkingdomid:=ParentPhylum.KingdomID, parenttaxonid:=ParentPhylum.TaxonID, parenttaxontypeid:=ParentPhylum.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVTAXCLASS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVTAXCLASS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVTAXCLASS()
            Try
                If Me.mTransferUpdates.VTAXCLASS.Rows.Count > 0 Then
                    Dim ClassGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim ClassService As DataObjects.Service.TaxonomyTaxonService = ClassGen.ServiceObject
                    For Each Row As Transfer.VTAXCLASSRow In Me.mTransferUpdates.VTAXCLASS.Rows
                        'Get the Class to update.
                        Dim OldClass As DataObjects.Entity.TaxonomyTaxon = ClassService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=GetKingdomID, taxonid:=CType(Row.CLARECID, Int32), taxontypeid:=GetClassTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Get the parent Phylum.
                        Dim ParentPhylum As DataObjects.Entity.TaxonomyTaxon = ClassService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.CLAPHYRECID, Int32), taxontypeid:=GetPhylumTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Classs.
                        ClassService.Update(ID:=OldClass.Id, Kingdomid:=OldClass.KingdomID, taxonid:=OldClass.TaxonID, taxontypeid:=OldClass.TaxonTypeID, epithettype:=OldClass.EpithetType, taxonname:=Row.CLANAME, taxonauthor:=OldClass.TaxonAuthor, taxonstatusid:=OldClass.TaxonStatusID, parentkingdomid:=ParentPhylum.KingdomID, parenttaxonid:=ParentPhylum.TaxonID, parenttaxontypeid:=ParentPhylum.TaxonTypeID, distributioncomplete:=OldClass.DistributionComplete, citesreference:=OldClass.CITESReference, lineage:=OldClass.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVTAXCLASS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVTAXCLASS", Ex)
            End Try
        End Sub

#End Region

#Region "VTAXORDER"
        Private Sub PhysicalDeleteVTAXORDER()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VTAXORDER.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Order rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VTAXORDERRow In Me.mTransferDeletes.VTAXORDER.Rows
                        mTaxonomyDataError.AddMessage("ORDRECID: " & Row.ORDRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVTAXORDER", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVTAXORDER()
            Try
                If Me.mTransferInserts.VTAXORDER.Rows.Count > 0 Then
                    Dim OrderGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim OrderService As DataObjects.Service.TaxonomyTaxonService = OrderGen.ServiceObject
                    For Each Row As Transfer.VTAXORDERRow In Me.mTransferInserts.VTAXORDER.Rows
                        'Get the parent class.
                        Dim ParentClass As DataObjects.Entity.TaxonomyTaxon = OrderService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.ORDCLARECID, Int32), taxontypeid:=GetClassTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Order.
                        OrderService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.ORDRECID, Int32), taxontypeid:=GetOrderTaxonTypeID, epithettype:=Nothing, taxonname:=Row.ORDNAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus("A"), parentkingdomid:=ParentClass.KingdomID, parenttaxonid:=ParentClass.TaxonID, parenttaxontypeid:=ParentClass.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVTAXORDER")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVTAXORDER", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVTAXORDER()
            Try
                If Me.mTransferUpdates.VTAXORDER.Rows.Count > 0 Then
                    Dim OrderGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim OrderService As DataObjects.Service.TaxonomyTaxonService = OrderGen.ServiceObject
                    For Each Row As Transfer.VTAXORDERRow In Me.mTransferUpdates.VTAXORDER.Rows
                        'Get the Order to update.
                        Dim OldOrder As DataObjects.Entity.TaxonomyTaxon = OrderService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=GetKingdomID, taxonid:=CType(Row.ORDRECID, Int32), taxontypeid:=GetOrderTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Get the parent class.
                        Dim ParentClass As DataObjects.Entity.TaxonomyTaxon = OrderService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.ORDCLARECID, Int32), taxontypeid:=GetClassTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Order.
                        OrderService.Update(ID:=OldOrder.Id, Kingdomid:=OldOrder.KingdomID, taxonid:=OldOrder.TaxonID, taxontypeid:=OldOrder.TaxonTypeID, epithettype:=OldOrder.EpithetType, taxonname:=Row.ORDNAME, taxonauthor:=OldOrder.TaxonAuthor, taxonstatusid:=OldOrder.TaxonStatusID, parentkingdomid:=ParentClass.KingdomID, parenttaxonid:=ParentClass.TaxonID, parenttaxontypeid:=ParentClass.TaxonTypeID, distributioncomplete:=OldOrder.DistributionComplete, citesreference:=OldOrder.CITESReference, lineage:=OldOrder.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVTAXORDER")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVTAXORDER", Ex)
            End Try
        End Sub

#End Region

#Region "VFAMILY"
        Private Sub PhysicalDeleteVFAMILY()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VFAMILY.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Family rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VFAMILYRow In Me.mTransferDeletes.VFAMILY.Rows
                        mTaxonomyDataError.AddMessage("FAMRECID: " & Row.FAMRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVFAMILY", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVFAMILY()
            Try
                If Me.mTransferInserts.VFAMILY.Rows.Count > 0 Then
                    Dim FamilyGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim FamilyService As DataObjects.Service.TaxonomyTaxonService = FamilyGen.ServiceObject
                    For Each Row As Transfer.VFAMILYRow In Me.mTransferInserts.VFAMILY.Rows
                        'Get the parent Order.
                        Dim ParentOrder As DataObjects.Entity.TaxonomyTaxon = FamilyService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.FAMORDRECID, Int32), taxontypeid:=GetOrderTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Family.
                        FamilyService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.FAMRECID, Int32), taxontypeid:=GetFamilyTaxonTypeID, epithettype:=Nothing, taxonname:=Row.FAMNAME, taxonauthor:=Row.FAMAUTHOR, taxonstatusID:=GetTaxonStatus(Row.FAMSTATUS), parentkingdomid:=ParentOrder.KingdomID, parenttaxonid:=ParentOrder.TaxonID, parenttaxontypeid:=ParentOrder.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVTAXFAMILY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVTAXFAMILY", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVFAMILY()
            Try
                If Me.mTransferUpdates.VFAMILY.Rows.Count > 0 Then
                    Dim FamilyGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim FamilyService As DataObjects.Service.TaxonomyTaxonService = FamilyGen.ServiceObject
                    For Each Row As Transfer.VFAMILYRow In Me.mTransferUpdates.VFAMILY.Rows
                        'Get the Family to update.
                        Dim OldFamily As DataObjects.Entity.TaxonomyTaxon = FamilyService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=GetKingdomID, taxonid:=CType(Row.FAMRECID, Int32), taxontypeid:=GetFamilyTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Get the parent Order.
                        Dim ParentOrder As DataObjects.Entity.TaxonomyTaxon = FamilyService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.FAMORDRECID, Int32), taxontypeid:=GetOrderTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Family.
                        FamilyService.Update(ID:=OldFamily.Id, Kingdomid:=OldFamily.KingdomID, taxonid:=OldFamily.TaxonID, taxontypeid:=OldFamily.TaxonTypeID, epithettype:=OldFamily.EpithetType, taxonname:=Row.FAMNAME, taxonauthor:=Row.FAMAUTHOR, taxonstatusid:=GetTaxonStatus(Row.FAMSTATUS), parentkingdomid:=ParentOrder.KingdomID, parenttaxonid:=ParentOrder.TaxonID, parenttaxontypeid:=ParentOrder.TaxonTypeID, distributioncomplete:=OldFamily.DistributionComplete, citesreference:=OldFamily.CITESReference, lineage:=OldFamily.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVTAXFAMILY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVTAXFAMILY", Ex)
            End Try
        End Sub
#End Region

#Region "VGENUS"
        Private Sub PhysicalDeleteVGENUS()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VGENUS.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Genus rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VGENUSRow In Me.mTransferDeletes.VGENUS.Rows
                        mTaxonomyDataError.AddMessage("GENRECID: " & Row.GENRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVGENUS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVGENUS()
            Try
                If Me.mTransferInserts.VGENUS.Rows.Count > 0 Then
                    Dim GenusGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim GenusService As DataObjects.Service.TaxonomyTaxonService = GenusGen.ServiceObject
                    For Each Row As Transfer.VGENUSRow In Me.mTransferInserts.VGENUS.Rows
                        'Get the parent Family.
                        Dim ParentFamily As DataObjects.Entity.TaxonomyTaxon = GenusService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.GENFAMRECID, Int32), taxontypeid:=GetFamilyTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Genus.
                        GenusService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.GENRECID, Int32), taxontypeid:=GetGenusTaxonTypeID, epithettype:=Nothing, taxonname:=Row.GENNAME, taxonauthor:=Row.GENAUTHOR, taxonstatusID:=GetTaxonStatus(Row.GENSTATUS), parentkingdomid:=ParentFamily.KingdomID, parenttaxonid:=ParentFamily.TaxonID, parenttaxontypeid:=ParentFamily.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalInsertVTAXGENUS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalInsertVTAXGENUS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVGENUS()
            Try
                If Me.mTransferUpdates.VGENUS.Rows.Count > 0 Then
                    Dim GenusGen As New DataObjects.Entity.TaxonomyTaxon
                    Dim GenusService As DataObjects.Service.TaxonomyTaxonService = GenusGen.ServiceObject
                    For Each Row As Transfer.VGENUSRow In Me.mTransferUpdates.VGENUS.Rows
                        'Get the Genus to update.
                        Dim OldGenus As DataObjects.Entity.TaxonomyTaxon = GenusService.GetByIndex_IX_TaxonomyTaxon(KingdomID:=GetKingdomID, taxonid:=CType(Row.GENRECID, Int32), taxontypeid:=GetGenusTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Get the parent Family.
                        Dim ParentFamily As DataObjects.Entity.TaxonomyTaxon = GenusService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.GENFAMRECID, Int32), taxontypeid:=GetFamilyTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
                        'Update Genus.
                        GenusService.Update(ID:=OldGenus.Id, Kingdomid:=OldGenus.KingdomID, taxonid:=OldGenus.TaxonID, taxontypeid:=OldGenus.TaxonTypeID, epithettype:=OldGenus.EpithetType, taxonname:=Row.GENNAME, taxonauthor:=Row.GENAUTHOR, taxonstatusid:=GetTaxonStatus(Row.GENSTATUS), parentkingdomid:=ParentFamily.KingdomID, parenttaxonid:=ParentFamily.TaxonID, parenttaxontypeid:=ParentFamily.TaxonTypeID, distributioncomplete:=OldGenus.DistributionComplete, citesreference:=OldGenus.CITESReference, lineage:=OldGenus.Lineage, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVTAXGENUS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVTAXGENUS", Ex)
            End Try
        End Sub
#End Region

#Region "VSPECIES"
        Private Sub PhysicalDeleteVSPECIES()
            Try
                'This is a special case. No deletes should be encountered. If there are any deletes raise an error.
                If Me.mTransferDeletes.VSPECIES.Rows.Count > 0 Then
                    mTaxonomyDataError.AddMessage("Species rows cannot be deleted from the TaxonomyTaxon table")
                    For Each Row As Transfer.VSPECIESRow In Me.mTransferDeletes.VSPECIES.Rows
                        mTaxonomyDataError.AddMessage("SPCRECID: " & Row.SPCRECID.ToString)
                    Next
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVSPECIES", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVSPECIES()
            'TODO: Nick - rework this to take into account the fake species for an epithet.
            Throw New Exception("PhysicalInsertVSPECIES not implemented yet")
            'Try
            '    Dim Epithets As New ArrayList
            '    If Me.mTransferInserts.VSPECIES.Rows.Count > 0 Then
            '        Dim SpeciesGen As New DataObjects.Entity.TaxonomyTaxon
            '        Dim SpeciesService As DataObjects.Service.TaxonomyTaxonService = SpeciesGen.ServiceObject
            '        Dim SpeciesNoteGen As New DataObjects.Entity.TaxonomyNote
            '        Dim SpeciesNoteService As DataObjects.Service.TaxonomyNoteService = SpeciesNoteGen.ServiceObject
            '        Dim NoteGen As New DataObjects.Entity.Note
            '        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
            '        For Each Row As Transfer.VSPECIESRow In Me.mTransferInserts.VSPECIES.Rows
            '            'Species rows can either be a species or an epithet. 
            '            If Row.SPCINFRAEPITHET.Length > 0 Then
            '                'This row represents a species, so insert it.
            '                'Get the parent Genus.
            '                Dim ParentGenus As DataObjects.Entity.TaxonomyTaxon = SpeciesService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.SPCGENRECID, Int32), taxontypeid:=GetGenusTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
            '                'Insert the Species.
            '                Dim Species As DataObjects.Entity.TaxonomyTaxon = SpeciesService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.SPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, epithettype:=Nothing, taxonname:=Row.SPCNAME, taxonauthor:=Row.SPCAUTHOR, taxonstatusID:=GetTaxonStatus(Row.SPCSTATUS), parentkingdomid:=ParentGenus.KingdomID, parenttaxonid:=ParentGenus.TaxonID, parenttaxontypeid:=ParentGenus.TaxonTypeID, distributioncomplete:=Row.SPCDISTRIBCOMPLETE, citesreference:=Row.SPCIDMANUAL, lineage:=Nothing, Transaction:=Me.Transaction)
            '                'Check if there is a note to store.
            '                If Row.IsSPCNOTESNull = False _
            '                AndAlso Row.SPCNOTES.Length > 0 Then
            '                    'There is a note, so store it.
            '                    Dim Note As DataObjects.Entity.Note = NoteService.Insert([Date]:=GetCreatedDate, content:=Row.SPCNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction)
            '                    SpeciesNoteService.Insert(taxonomyid:=Species.Id, noteid:=Note.Id, Transaction:=Me.Transaction)
            '                End If
            '                'Check if there is a stock name to store.
            '                If Row.IsSPCSTOCKNAMENull = False _
            '                AndAlso Row.SPCSTOCKNAME.Length > 0 Then
            '                    'There is a stock name so store it.
            '                    SpeciesService.Insert(kingdomID:=GetKingdomID, taxonid:=GetStockTaxonID(Species.TaxonID), taxontypeid:=GetStockTaxonTypeID, epithettype:=Nothing, taxonname:=Row.SPCSTOCKNAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus(Row.SPCSTATUS), parentkingdomid:=Species.KingdomID, parenttaxonid:=Species.TaxonID, parenttaxontypeid:=Species.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
            '                End If
            '            Else
            '                'This row represents an epithet, so process it later.
            '                Epithets.Add(Row)
            '            End If
            '        Next
            '        'Now insert the epithets.
            '        For Each Row As Transfer.VSPECIESRow In Epithets
            '            'This row represents an Epithet, so insert it.
            '            'Get the parent Genus or Species.
            '            Dim Parent As DataObjects.Entity.TaxonomyTaxon
            '            'Attempt to get the parent Species.
            '            Parent = SpeciesService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.SPCGENRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
            '            If Parent Is Nothing = True Then
            '                'Attempt to get the parent Genus.
            '                Parent = SpeciesService.GetByIndex_IX_TaxonomyTaxon(kingdomID:=GetKingdomID, taxonid:=CType(Row.SPCGENRECID, Int32), taxontypeid:=GetGenusTaxonTypeID, Transaction:=Me.Transaction).Entities(0)
            '            End If
            '            'Insert the Species.
            '            Dim Species As DataObjects.Entity.TaxonomyTaxon = SpeciesService.Insert(kingdomID:=GetKingdomID, taxonid:=CType(Row.SPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, epithettype:=Nothing, taxonname:=Row.SPCNAME, taxonauthor:=Row.SPCAUTHOR, taxonstatusID:=GetTaxonStatus(Row.SPCSTATUS), parentkingdomid:=ParentGenus.KingdomID, parenttaxonid:=ParentGenus.TaxonID, parenttaxontypeid:=ParentGenus.TaxonTypeID, distributioncomplete:=Row.SPCDISTRIBCOMPLETE, citesreference:=Row.SPCIDMANUAL, lineage:=Nothing, Transaction:=Me.Transaction)
            '            Dim SpeciesNoteID As Object = Nothing
            '            Dim NewNoteId As Object = Nothing
            '            'Check if there is a note to store.
            '            If Row.IsSPCNOTESNull = False _
            '            AndAlso Row.SPCNOTES.Length > 0 Then
            '                'There is a note, so store it.
            '                NewNoteId = NoteService.Insert([Date]:=GetCreatedDate, content:=Row.SPCNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
            '                SpeciesNoteService.Insert(taxonomyid:=Species.Id, NoteId:=CType(NewNoteId, Int32), Transaction:=Me.Transaction)
            '            End If
            '            'Check if there is a stock name to store.
            '            If Row.IsSPCSTOCKNAMENull = False _
            '            AndAlso Row.SPCSTOCKNAME.Length > 0 Then
            '                'There is a stock name so store it.
            '                SpeciesService.Insert(kingdomID:=GetKingdomID, taxonid:=GetStockTaxonID(Species.TaxonID), taxontypeid:=GetStockTaxonTypeID, epithettype:=Nothing, taxonname:=Row.SPCSTOCKNAME, taxonauthor:=Nothing, taxonstatusID:=GetTaxonStatus(Row.SPCSTATUS), parentkingdomid:=Species.KingdomID, parenttaxonid:=Species.TaxonID, parenttaxontypeid:=Species.TaxonTypeID, distributioncomplete:=Nothing, citesreference:=Nothing, lineage:=Nothing, Transaction:=Me.Transaction)
            '            End If

            '        Next
            '        'Check whether the parent of the epithet is a species or a genus.
            '        SaveTransaction("PhysicalInsertVTAXSPECIES")
            '    End If
            'Catch Ex As Exception
            '    Throw New Exception("Error with PhysicalInsertVTAXSPECIES", Ex)
            'End Try
        End Sub

        Private Sub PhysicalUpdateVSPECIES()
            'TODO: Nick - Implement this special case.
            Throw New Exception("PhysicalUpdateVSPECIES not implemented yet")
            'Try
            '    
            'Catch Ex As Exception
            '    Throw New Exception("Error with PhysicalDeleteVSPECIES", Ex)
            'End Try
        End Sub
#End Region

#Region "VCOMMONNAME"
        Private Sub PhysicalDeleteVCOMMONNAME()
            'Common name deletes are a special case. Update the active flag to false instead of deleting them.
            Try
                If Me.mTransferDeletes.VCOMMONNAME.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    For Each Row As Transfer.VCOMMONNAMERow In Me.mTransferDeletes.VCOMMONNAME.Rows
                        'Get the row to be updated.
                        Dim CommonNameOld As DataObjects.Entity.TaxonomyCommonName = CommonNameService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.COMRECID, Int32)}, tran:=Me.Transaction)
                        CommonNameService.Update(active:=False, sourcetable:=TaxonomyRowSourceEnum.Standard, commonnameid:=CommonNameOld.Id, name:=CommonNameOld.Name, productindicator:=CommonNameOld.ProductIndicator, kingdomID:=CommonNameOld.KingdomID, taxonid:=CommonNameOld.TaxonId, taxontypeid:=CommonNameOld.TaxonTypeID, areaofUseid:=CommonNameOld.AreaOfUseID, checksum:=CommonNameOld.CheckSum, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVCOMMONNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVCOMMONNAME", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVCOMMONNAME()
            Try
                If Me.mTransferInserts.VCOMMONNAME.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    For Each NewValue As Transfer.VCOMMONNAMERow In Me.mTransferInserts.VCOMMONNAME.Rows
                        'Insert the CommonName record.
                        CommonNameService.Insert(active:=True, sourcetable:=TaxonomyRowSourceEnum.Standard, CommonNameID:=CType(NewValue.COMRECID, Int32), name:=NewValue.COMNAME, productindicator:=NewValue.COMPRODUCTNAME, kingdomID:=GetKingdomID, taxonid:=CType(NewValue.COMSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID(), areaofuseid:=CType(NewValue.COMAOURECID, Int32), Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVCOMMONNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVCOMMONNAME", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVCOMMONNAME()
            Try
                If Me.mTransferUpdates.VCOMMONNAME.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    For Each NewValue As Transfer.VCOMMONNAMERow In Me.mTransferUpdates.VCOMMONNAME.Rows
                        'Get the CommonName record to be be updated.
                        Dim CommonName As DataObjects.Entity.TaxonomyCommonName = _
                            CommonNameService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(NewValue.COMRECID, Int32)}, tran:=Me.Transaction)
                        'Update the CommonName record.
                        CommonNameService.Update(active:=CommonName.Active, sourcetable:=TaxonomyRowSourceEnum.Standard, commonnameid:=CommonName.Id, name:=NewValue.COMNAME, productindicator:=NewValue.COMPRODUCTNAME, kingdomID:=CommonName.KingdomID, taxonid:=CommonName.TaxonId, taxontypeid:=CommonName.TaxonTypeID, areaofUseid:=CType(NewValue.COMAOURECID, Int32), checksum:=CommonName.CheckSum, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVCOMMONNAME")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVCOMMONNAME", Ex)
            End Try
        End Sub
#End Region

#Region "VHIGHCOMMON"
        Private Sub PhysicalDeleteVHIGHCOMMON()
            'Common name deletes are a special case. Update the active to false instead of deleting them.
            Try
                If Me.mTransferDeletes.VHIGHCOMMON.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    For Each Row As Transfer.VHIGHCOMMONRow In Me.mTransferDeletes.VHIGHCOMMON.Rows
                        'Get the row to be updated.
                        Dim CommonNameOld As DataObjects.Entity.TaxonomyCommonName = CommonNameService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Higher, CType(Row.HCOMRECID, Int32)}, tran:=Me.Transaction)
                        CommonNameService.Update(active:=False, sourcetable:=TaxonomyRowSourceEnum.Higher, commonnameid:=CommonNameOld.Id, name:=CommonNameOld.Name, productindicator:=CommonNameOld.ProductIndicator, kingdomID:=CommonNameOld.KingdomID, taxonid:=CommonNameOld.TaxonId, taxontypeid:=CommonNameOld.TaxonTypeID, areaofUseid:=CommonNameOld.AreaOfUseID, checksum:=CommonNameOld.CheckSum, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVHIGHCOMMON")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVHIGHCOMMON", Ex)
            End Try
        End Sub

        Private Delegate Function TaxonTypeDelegate() As Int32

        Private Sub PhysicalInsertVHIGHCOMMON()
            Try
                If Me.mTransferInserts.VHIGHCOMMON.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    Dim TaxonTypeFunction As TaxonTypeDelegate
                    For Each NewValue As Transfer.VHIGHCOMMONRow In Me.mTransferInserts.VHIGHCOMMON.Rows
                        'Insert the CommonName record.
                        Select Case NewValue.HCOMCODE.ToUpper
                            Case "GEN".ToUpper
                                TaxonTypeFunction = AddressOf GetGenusTaxonTypeID
                            Case "FAM".ToUpper
                                TaxonTypeFunction = AddressOf GetFamilyTaxonTypeID
                            Case "ORD".ToUpper
                                TaxonTypeFunction = AddressOf GetOrderTaxonTypeID
                            Case "CLA".ToUpper
                                TaxonTypeFunction = AddressOf GetClassTaxonTypeID
                            Case "PHY".ToUpper
                                TaxonTypeFunction = AddressOf GetPhylumTaxonTypeID
                            Case "KGM".ToUpper
                                TaxonTypeFunction = AddressOf GetKingdomTaxonTypeID
                            Case Else
                                Throw New Exception("HComCode " & NewValue.HCOMCODE & " is not supported")
                        End Select
                        CommonNameService.Insert(active:=True, sourcetable:=TaxonomyRowSourceEnum.Higher, CommonNameID:=CType(NewValue.HCOMRECID, Int32), name:=NewValue.HCOMNAME, productindicator:=NewValue.HCOMPRODUCTNAME, kingdomID:=GetKingdomID, taxonid:=CType(NewValue.HCOMCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), areaofuseid:=CType(NewValue.HCOMAOURECID, Int32), Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHCOMMON")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHCOMMON", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVHIGHCOMMON()
            Try
                If Me.mTransferUpdates.VHIGHCOMMON.Rows.Count > 0 Then
                    Dim CommonNameGen As New DataObjects.Entity.TaxonomyCommonName
                    Dim CommonNameService As DataObjects.Service.TaxonomyCommonNameService = CommonNameGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHCOMMONRow In Me.mTransferUpdates.VHIGHCOMMON.Rows
                        'Get the CommonName record to be be updated.
                        Dim CommonName As DataObjects.Entity.TaxonomyCommonName = _
                            CommonNameService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Higher, CType(NewValue.HCOMRECID, Int32)}, tran:=Me.Transaction)
                        'Update the CommonName record.
                        CommonNameService.Update(active:=CommonName.Active, sourcetable:=TaxonomyRowSourceEnum.Standard, commonnameid:=CommonName.Id, name:=NewValue.HCOMNAME, productindicator:=NewValue.HCOMPRODUCTNAME, kingdomID:=CommonName.KingdomID, taxonid:=CommonName.TaxonId, taxontypeid:=CommonName.TaxonTypeID, areaofUseid:=CType(NewValue.HCOMAOURECID, Int32), checksum:=CommonName.CheckSum, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHCOMMON")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHCOMMON", Ex)
            End Try
        End Sub
#End Region

#Region "VDISTRIBAQUA"
        Private Sub PhysicalDeleteVDISTRIBAQUA()
            Try
                If Me.mTransferDeletes.VDISTRIBAQUA.Rows.Count > 0 Then
                    Dim DistribAquaGen As New DataObjects.Entity.TaxonomySpeciesAquaticDistribution
                    Dim DistribAquaService As DataObjects.Service.TaxonomySpeciesAquaticDistributionService = DistribAquaGen.ServiceObject
                    For Each Row As Transfer.VDISTRIBAQUARow In Me.mTransferDeletes.VDISTRIBAQUA.Rows
                        'Get the DistribAqua id.
                        Dim DistribAqua As DataObjects.Entity.TaxonomySpeciesAquaticDistribution = _
                            DistribAquaService.GetById(speciesAquaticDistributionId:=CType(Row.DAQRECID, Int32), Tran:=Me.Transaction)
                        'Since each DistribAqua refers to a note this has to be deleted first.
                        If DistribAqua.IsSpeciesAquaticDistributionNoteIDNull = False Then
                            Dim NoteGen As New DataObjects.Entity.Note
                            Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                            NoteService.DeleteById(noteid:=DistribAqua.SpeciesAquaticDistributionNoteID, checksum:=0, Transaction:=Me.Transaction)
                        End If
                        'Delete the DistribAqua.
                        DistribAquaService.DeleteById(speciesaquaticDistributionID:=CType(Row.DAQRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVDISTRIBAQUA")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVDISTRIBAQUA", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVDISTRIBAQUA()
            Try
                If Me.mTransferInserts.VDISTRIBAQUA.Rows.Count > 0 Then
                    Dim DistribAquaGen As New DataObjects.Entity.TaxonomySpeciesAquaticDistribution
                    Dim DistribAquaService As DataObjects.Service.TaxonomySpeciesAquaticDistributionService = DistribAquaGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBAQUARow In Me.mTransferInserts.VDISTRIBAQUA.Rows
                        'Within the physical database the DistribAqua record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        Dim InsNoteID As Object = Nothing
                        'Check if there is a note to store.
                        If String.Compare("", NewValue.DAQNOTES) <> 0 Then
                            'Create the note that is pointed to by this DistribAqua record.
                            InsNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DAQNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                        End If
                        'Insert the DistribAqua record.
                        DistribAquaService.Insert(speciesaquaticdistributionid:=CType(NewValue.DAQRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.DAQSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, aquaticregionid:=CType(NewValue.DAQAQURECID, Int32), certain:=NewValue.DAQDISTRIBCERTAIN, extinct:=NewValue.DAQEXTINCT, introduced:=NewValue.DAQINTRODUCED, reintroduced:=NewValue.DAQREINTRODUCED, speciesaquaticdistributionnoteid:=InsNoteID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBAQUA")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBAQUA", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVDISTRIBAQUA()
            Try
                If Me.mTransferUpdates.VDISTRIBAQUA.Rows.Count > 0 Then
                    Dim DistribAquaGen As New DataObjects.Entity.TaxonomySpeciesAquaticDistribution
                    Dim DistribAquaService As DataObjects.Service.TaxonomySpeciesAquaticDistributionService = DistribAquaGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBAQUARow In Me.mTransferUpdates.VDISTRIBAQUA.Rows
                        'Get the DistribAqua record to be be updated.
                        Dim DistribAqua As DataObjects.Entity.TaxonomySpeciesAquaticDistribution = _
                            DistribAquaService.GetById(speciesaquaticDistributionID:=CType(NewValue.DAQRECID, Int32), Tran:=Me.Transaction)
                        'Within the physical database the DistribAqua record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        'Retrieve the note that is pointed to by this DistribAqua record.
                        Dim Note As DataObjects.Entity.Note = NoteService.GetById(noteid:=DistribAqua.SpeciesAquaticDistributionNoteID, tran:=Me.Transaction)
                        Dim UpdNoteID As Object = Nothing
                        If Note Is Nothing = True Then
                            'Check if there is a note to store.
                            If NewValue.DAQNOTES.Length > 0 Then
                                'There is a note to store so store it.
                                UpdNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DAQNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                            End If
                        Else
                            'Check if the the note content has changed.
                            If String.Compare(Note.Content, NewValue.DAQNOTES) <> 0 Then
                                'The note content has changed so update the note.
                                NoteService.Update(id:=Note.Id, [Date]:=Note.Date, content:=NewValue.DAQNOTES, isreadonly:=Note.IsReadOnly, important:=Note.Important, subject:=Note.Subject, modifiedby:=GetUser, modifieddate:=GetModifiedDate, createdby:=Note.CreatedBy, createddate:=Note.CreatedDate, active:=Note.Active, checksum:=Note.CheckSum, Transaction:=Me.Transaction)
                            End If
                            UpdNoteID = Note.Id
                        End If
                        'Update the DistribAqua record.
                        DistribAquaService.Update(speciesaquaticdistributionid:=CType(NewValue.DAQRECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.DAQSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, aquaticregionid:=CType(NewValue.DAQAQURECID, Int32), certain:=NewValue.DAQDISTRIBCERTAIN, extinct:=NewValue.DAQEXTINCT, introduced:=NewValue.DAQINTRODUCED, reintroduced:=NewValue.DAQREINTRODUCED, speciesaquaticdistributionnoteid:=UpdNoteID, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBAQUA")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBAQUA", Ex)
            End Try
        End Sub
#End Region

#Region "VDISTRIBBRU"
        Private Sub PhysicalDeleteVDISTRIBBRU()
            Try
                If Me.mTransferDeletes.VDISTRIBBRU.Rows.Count > 0 Then
                    Dim DistribBRUGen As New DataObjects.Entity.TaxonomySpeciesBRUDistribution
                    Dim DistribBRUService As DataObjects.Service.TaxonomySpeciesBRUDistributionService = DistribBRUGen.ServiceObject
                    For Each Row As Transfer.VDISTRIBBRURow In Me.mTransferDeletes.VDISTRIBBRU.Rows
                        'Get the DistribBRU id.
                        Dim DistribBRU As DataObjects.Entity.TaxonomySpeciesBRUDistribution = _
                            DistribBRUService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.DBURECID, Int32)}, tran:=Me.Transaction)
                        'Since each DistribBRU refers to a note this has to be deleted first.
                        If DistribBRU.IsNoteIDNull = False Then
                            Dim NoteGen As New DataObjects.Entity.Note
                            Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                            NoteService.DeleteById(noteid:=DistribBRU.NoteID, checksum:=0, Transaction:=Me.Transaction)
                        End If
                        'Delete the DistribBRU.
                        DistribBRUService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.DBURECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVDISTRIBBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVDISTRIBBRU", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVDISTRIBBRU()
            Try
                If Me.mTransferInserts.VDISTRIBBRU.Rows.Count > 0 Then
                    Dim DistribBRUGen As New DataObjects.Entity.TaxonomySpeciesBRUDistribution
                    Dim DistribBRUService As DataObjects.Service.TaxonomySpeciesBRUDistributionService = DistribBRUGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBBRURow In Me.mTransferInserts.VDISTRIBBRU.Rows
                        'Within the physical database the DistribBRU record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        Dim InsNoteID As Object = Nothing
                        'Check if there is a note to store.
                        If NewValue.DBUNOTES.Length > 0 Then
                            'Create the note that is pointed to by this DistribBRU record.
                            InsNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DBUNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                        End If
                        'Insert the DistribBRU record.
                        DistribBRUService.Insert(source:=TaxonomyRowSourceEnum.Standard, speciesbrudistributionid:=CType(NewValue.DBURECID, Int32), countrydistributionid:=CType(NewValue.DBUCTYRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.DBUSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, bruid:=CType(NewValue.DBUBRURECID, Int32), certain:=NewValue.DBUDISTRIBCERTAIN, extinct:=NewValue.DBUEXTINCT, introduced:=NewValue.DBUINTRODUCED, reintroduced:=NewValue.DBUREINTRODUCED, breeding:=NewValue.DBUBREEDING, vagrant:=NewValue.DBUVAGRANT, noteid:=InsNoteID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBBRU", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVDISTRIBBRU()
            Try
                If Me.mTransferUpdates.VDISTRIBBRU.Rows.Count > 0 Then
                    Dim DistribBRUGen As New DataObjects.Entity.TaxonomySpeciesBRUDistribution
                    Dim DistribBRUService As DataObjects.Service.TaxonomySpeciesBRUDistributionService = DistribBRUGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBBRURow In Me.mTransferUpdates.VDISTRIBBRU.Rows
                        'Get the DistribBRU record to be be updated.
                        Dim DistribBRU As DataObjects.Entity.TaxonomySpeciesBRUDistribution = _
                            DistribBRUService.GetById(IDColumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(NewValue.DBURECID, Int32)}, Tran:=Me.Transaction)
                        'Within the physical database the DistribBRU record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        'Retrieve the note that is pointed to by this DistribBRU record.
                        Dim Note As DataObjects.Entity.Note = NoteService.GetById(noteid:=DistribBRU.NoteID, tran:=Me.Transaction)
                        Dim UpdNoteID As Object = Nothing
                        If Note Is Nothing = True Then
                            'Check if there is a note to store.
                            If NewValue.DBUNOTES.Length > 0 Then
                                'There is a note to store so store it.
                                UpdNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DBUNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                            End If
                        Else
                            'Check if the the note content has changed.
                            If String.Compare(Note.Content, NewValue.DBUNOTES) <> 0 Then
                                'The note content has changed so update the note.
                                NoteService.Update(id:=Note.Id, [Date]:=Note.Date, content:=NewValue.DBUNOTES, isreadonly:=Note.IsReadOnly, important:=Note.Important, subject:=Note.Subject, modifiedby:=GetUser, modifieddate:=GetModifiedDate, createdby:=Note.CreatedBy, createddate:=Note.CreatedDate, active:=Note.Active, checksum:=Note.CheckSum, Transaction:=Me.Transaction)
                            End If
                            UpdNoteID = Note.Id
                        End If
                        'Update the DistribBRU record.
                        DistribBRUService.Update(source:=TaxonomyRowSourceEnum.Standard, speciesBRUdistributionid:=CType(NewValue.DBURECID, Int32), countrydistributionid:=CType(NewValue.DBUDCTRECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.DBUSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, BRUid:=CType(NewValue.DBUBRURECID, Int32), certain:=NewValue.DBUDISTRIBCERTAIN, extinct:=NewValue.DBUEXTINCT, introduced:=NewValue.DBUINTRODUCED, reintroduced:=NewValue.DBUREINTRODUCED, breeding:=NewValue.DBUBREEDING, vagrant:=NewValue.DBUVAGRANT, noteid:=UpdNoteID, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBBRU")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBBRU", Ex)
            End Try
        End Sub
#End Region

#Region "VDISTRIBCTY"
        Private Sub PhysicalDeleteVDISTRIBCTY()
            Try
                If Me.mTransferDeletes.VDISTRIBCTY.Rows.Count > 0 Then
                    Dim DistribCountryGen As New DataObjects.Entity.TaxonomySpeciesCountryDistribution
                    Dim DistribCountryService As DataObjects.Service.TaxonomySpeciesCountryDistributionService = DistribCountryGen.ServiceObject
                    For Each Row As Transfer.VDISTRIBCTYRow In Me.mTransferDeletes.VDISTRIBCTY.Rows
                        'Get the DistribCountry id.
                        Dim DistribCountry As DataObjects.Entity.TaxonomySpeciesCountryDistribution = _
                            DistribCountryService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.DCTRECID, Int32)}, Tran:=Me.Transaction)
                        'Since each DistribCountry refers to a note this has to be deleted first.
                        If DistribCountry.IsNoteIDNull = False Then
                            Dim NoteGen As New DataObjects.Entity.Note
                            Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                            NoteService.DeleteById(noteid:=DistribCountry.NoteID, checksum:=0, Transaction:=Me.Transaction)
                        End If
                        'Delete the DistribCountry.
                        DistribCountryService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.DCTRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVDISTRIBCTY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVDISTRIBCTY", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVDISTRIBCTY()
            Try
                If Me.mTransferInserts.VDISTRIBCTY.Rows.Count > 0 Then
                    Dim DistribCountryGen As New DataObjects.Entity.TaxonomySpeciesCountryDistribution
                    Dim DistribCountryService As DataObjects.Service.TaxonomySpeciesCountryDistributionService = DistribCountryGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBCTYRow In Me.mTransferInserts.VDISTRIBCTY.Rows
                        'Within the physical database the DistribCountry record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        Dim InsNoteID As Object = Nothing
                        'Check if there is a note to store.
                        If NewValue.DCTNOTES.Length > 0 Then
                            'Create the note that is pointed to by this DistribCountry record.
                            InsNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DCTNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                        End If
                        'Insert the DistribCountry record.
                        DistribCountryService.Insert(source:=TaxonomyRowSourceEnum.Standard, speciesCountrydistributionid:=CType(NewValue.DCTRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.DCTSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, certain:=NewValue.DCTDISTRIBCERTAIN, extinct:=NewValue.DCTEXTINCT, introduced:=NewValue.DCTINTRODUCED, reintroduced:=NewValue.DCTREINTRODUCED, breeding:=NewValue.DCTBREEDING, vagrant:=NewValue.DCTVAGRANT, noteid:=InsNoteID, Countryid:=CType(NewValue.DCTCTYRECID, Int32), Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBCTY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBCTY", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVDISTRIBCTY()
            Try
                If Me.mTransferUpdates.VDISTRIBCTY.Rows.Count > 0 Then
                    Dim DistribCountryGen As New DataObjects.Entity.TaxonomySpeciesCountryDistribution
                    Dim DistribCountryService As DataObjects.Service.TaxonomySpeciesCountryDistributionService = DistribCountryGen.ServiceObject
                    For Each NewValue As Transfer.VDISTRIBCTYRow In Me.mTransferUpdates.VDISTRIBCTY.Rows
                        'Get the DistribCountry record to be be updated.
                        Dim DistribCountry As DataObjects.Entity.TaxonomySpeciesCountryDistribution = _
                            DistribCountryService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(NewValue.DCTRECID, Int32)}, Tran:=Me.Transaction)
                        'Within the physical database the DistribCountry record does not contain a note but instead points to the generic notes table.
                        Dim NoteGen As New DataObjects.Entity.Note
                        Dim NoteService As DataObjects.Service.NoteService = NoteGen.ServiceObject
                        'Retrieve the note that is pointed to by this DistribCountry record.
                        Dim Note As DataObjects.Entity.Note = NoteService.GetById(noteid:=DistribCountry.NoteID, tran:=Me.Transaction)
                        Dim UpdNoteID As Object = Nothing
                        If Note Is Nothing = True Then
                            'Check if there is a note to store.
                            If NewValue.DCTNOTES.Length > 0 Then
                                'There is a note to store so store it.
                                UpdNoteID = NoteService.Insert([Date]:=GetCreatedDate, content:=NewValue.DCTNOTES, isreadonly:=True, important:=False, subject:=GetNoteSubject, modifiedby:=Nothing, modifieddate:=Nothing, createdby:=GetUser, createddate:=GetCreatedDate, active:=True, Transaction:=Me.Transaction).Id
                            End If
                        Else
                            'Check if the the note content has changed.
                            If String.Compare(Note.Content, NewValue.DCTNOTES) <> 0 Then
                                'The note content has changed so update the note.
                                NoteService.Update(id:=Note.Id, [Date]:=Note.Date, content:=NewValue.DCTNOTES, isreadonly:=Note.IsReadOnly, important:=Note.Important, subject:=Note.Subject, modifiedby:=GetUser, modifieddate:=GetModifiedDate, createdby:=Note.CreatedBy, createddate:=Note.CreatedDate, active:=Note.Active, checksum:=Note.CheckSum, Transaction:=Me.Transaction)
                            End If
                            UpdNoteID = Note.Id
                        End If
                        'Update the DistribCountry record.
                        DistribCountryService.Update(source:=TaxonomyRowSourceEnum.Standard, speciesCountrydistributionid:=CType(NewValue.DCTRECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.DCTSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, certain:=NewValue.DCTDISTRIBCERTAIN, extinct:=NewValue.DCTEXTINCT, introduced:=NewValue.DCTINTRODUCED, reintroduced:=NewValue.DCTREINTRODUCED, breeding:=NewValue.DCTBREEDING, vagrant:=NewValue.DCTVAGRANT, noteid:=UpdNoteID, countryid:=CType(NewValue.DCTCTYRECID, Int32), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDISTRIBCTY")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDISTRIBCTY", Ex)
            End Try
        End Sub
#End Region

#Region "VUSE"
        Private Sub PhysicalDeleteVUSE()
            Try
                If Me.mTransferDeletes.VUSED.Rows.Count > 0 Then
                    Dim UsageGen As New DataObjects.Entity.TaxonomyUsage
                    Dim UsageService As DataObjects.Service.TaxonomyUsageService = UsageGen.ServiceObject
                    For Each Row As Transfer.VUSEDRow In Me.mTransferDeletes.VUSED.Rows
                        'Delete the Usage.
                        UsageService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.USERECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVUSE()
            Try
                If Me.mTransferInserts.VUSED.Rows.Count > 0 Then
                    Dim UsageGen As New DataObjects.Entity.TaxonomyUsage
                    Dim UsageService As DataObjects.Service.TaxonomyUsageService = UsageGen.ServiceObject
                    For Each NewValue As Transfer.VUSEDRow In Me.mTransferInserts.VUSED.Rows
                        'Insert the Usage record.
                        UsageService.Insert(source:=TaxonomyRowSourceEnum.Standard, Usageid:=CType(NewValue.USERECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.USESPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, usagetypeid:=NewValue.USEUTYRECID, partid:=NewValue.USEPARRECID, levelofuseid:=NewValue.USELOURECID, note:=NewValue.USENOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVUSE", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVUSE()
            Try
                If Me.mTransferUpdates.VUSED.Rows.Count > 0 Then
                    Dim UsageGen As New DataObjects.Entity.TaxonomyUsage
                    Dim UsageService As DataObjects.Service.TaxonomyUsageService = UsageGen.ServiceObject
                    For Each NewValue As Transfer.VUSEDRow In Me.mTransferUpdates.VUSED.Rows
                        'Get the Usage record to be be updated.
                        Dim Usage As DataObjects.Entity.TaxonomyUsage = _
                            UsageService.GetById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(NewValue.USERECID, Int32)}, Tran:=Me.Transaction)
                        'Update the Usage record.
                        UsageService.Update(source:=TaxonomyRowSourceEnum.Standard, usageid:=CType(NewValue.USERECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.USESPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, usagetypeid:=NewValue.USEUTYRECID, partid:=NewValue.USEPARRECID, levelofuseid:=NewValue.USELOURECID, Note:=NewValue.USENOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVUSE")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVUSE", Ex)
            End Try
        End Sub
#End Region

#Region "VSYNLINK"
        Private Sub PhysicalDeleteVSYNLINK()
            Try
                If Me.mTransferDeletes.VSYNLINK.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each Row As Transfer.VSYNLINKRow In Me.mTransferDeletes.VSYNLINK.Rows
                        'Delete the Synonyms.
                        SynonymsService.DeleteById(idColumns:=New Int32() {TaxonomyRowSourceEnum.Standard, CType(Row.SYNRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVSYNLINK")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVSYNLINK", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVSYNLINK()
            Try
                If Me.mTransferInserts.VSYNLINK.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each NewValue As Transfer.VSYNLINKRow In Me.mTransferInserts.VSYNLINK.Rows
                        'Insert the Synonyms record.
                        SynonymsService.Insert(synonymSource:=TaxonomyRowSourceEnum.Standard, Synonymid:=CType(NewValue.SYNRECID, Int32), acceptedkingdomid:=GetKingdomID, acceptedtaxonid:=CType(NewValue.SYNSPCRECID, Int32), acceptedtaxontypeid:=GetSpeciesTaxonTypeID, synonymkingdomid:=GetKingdomID, synonymtaxonid:=CType(NewValue.SYNPARENTRECID, Int32), synonymtaxontypeid:=GetSpeciesTaxonTypeID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVSYNLINK")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVSYNLINK", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVSYNLINK()
            Try
                If Me.mTransferUpdates.VSYNLINK.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each NewValue As Transfer.VSYNLINKRow In Me.mTransferUpdates.VSYNLINK.Rows
                        'Update the Synonyms record.
                        SynonymsService.Update(synonymSource:=TaxonomyRowSourceEnum.Standard, synonymid:=CType(NewValue.SYNRECID, Int32), acceptedkingdomid:=GetKingdomID, acceptedtaxonId:=CType(NewValue.SYNSPCRECID, Int32), acceptedtaxontypeid:=GetSpeciesTaxonTypeID, synonymkingdomid:=GetKingdomID, synonymtaxonid:=CType(NewValue.SYNPARENTRECID, Int32), synonymtaxontypeid:=GetSpeciesTaxonTypeID, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVSYNLINK")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVSYNLINK", Ex)
            End Try
        End Sub
#End Region

#Region "VHIGHSYNONYMS"
        Private Sub PhysicalDeleteVHIGHSYNONYMS()
            Try
                If Me.mTransferDeletes.VHIGHSYNONYMS.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each Row As Transfer.VHIGHSYNONYMSRow In Me.mTransferDeletes.VHIGHSYNONYMS.Rows
                        'Delete the Synonyms.
                        SynonymsService.DeleteById(idColumns:=New Int32() {TaxonomyRowSourceEnum.Higher, CType(Row.HSYNRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVHIGHSYNONYMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVHIGHSYNONYMS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVHIGHSYNONYMS()
            Try
                If Me.mTransferInserts.VHIGHSYNONYMS.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHSYNONYMSRow In Me.mTransferInserts.VHIGHSYNONYMS.Rows
                        'Insert the Synonyms record.
                        Dim AcceptedTaxonTypeFunction As TaxonTypeDelegate = GetVHIGHSYNONYMSTaxonTypeID(NewValue.HSYNCODE)
                        Dim SynonymTaxonTypeFunction As TaxonTypeDelegate = GetVHIGHSYNONYMSTaxonTypeID(NewValue.HSYNPARENTCODE)
                        SynonymsService.Insert(synonymSource:=TaxonomyRowSourceEnum.Higher, Synonymid:=CType(NewValue.HSYNRECID, Int32), acceptedkingdomid:=GetKingdomID, acceptedtaxonid:=AcceptedTaxonTypeFunction(), acceptedtaxontypeid:=GetSpeciesTaxonTypeID, synonymkingdomid:=GetKingdomID, synonymtaxonid:=SynonymTaxonTypeFunction(), synonymtaxontypeid:=GetSpeciesTaxonTypeID, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHSYNONYMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHSYNONYMS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVHIGHSYNONYMS()
            Try
                If Me.mTransferUpdates.VHIGHSYNONYMS.Rows.Count > 0 Then
                    Dim SynonymsGen As New DataObjects.Entity.TaxonomySynonym
                    Dim SynonymsService As DataObjects.Service.TaxonomySynonymService = SynonymsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHSYNONYMSRow In Me.mTransferUpdates.VHIGHSYNONYMS.Rows
                        'Update the Synonyms record.
                        Dim AcceptedTaxonTypeFunction As TaxonTypeDelegate = GetVHIGHSYNONYMSTaxonTypeID(NewValue.HSYNCODE)
                        Dim SynonymTaxonTypeFunction As TaxonTypeDelegate = GetVHIGHSYNONYMSTaxonTypeID(NewValue.HSYNPARENTCODE)
                        SynonymsService.Update(synonymSource:=TaxonomyRowSourceEnum.Higher, synonymid:=CType(NewValue.HSYNRECID, Int32), acceptedkingdomid:=GetKingdomID, acceptedtaxonId:=CType(NewValue.HSYNCODERECID, Int32), acceptedtaxontypeid:=AcceptedTaxonTypeFunction(), synonymkingdomid:=GetKingdomID, synonymtaxonid:=CType(NewValue.HSYNPARENTRECID, Int32), synonymtaxontypeid:=SynonymTaxonTypeFunction(), checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHSYNONYMS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHSYNONYMS", Ex)
            End Try
        End Sub

        Private Function GetVHIGHSYNONYMSTaxonTypeID(ByVal ComCode As String) As TaxonTypeDelegate
            Select Case ComCode.ToUpper
                Case "SPC".ToUpper
                    Return AddressOf GetSpeciesTaxonTypeID
                Case "GEN".ToUpper
                    Return AddressOf GetGenusTaxonTypeID
                Case "FAM".ToUpper
                    Return AddressOf GetFamilyTaxonTypeID
                Case "ORD".ToUpper
                    Return AddressOf GetOrderTaxonTypeID
                Case "CLA".ToUpper
                    Return AddressOf GetClassTaxonTypeID
                Case "PHY".ToUpper
                    Return AddressOf GetPhylumTaxonTypeID
                Case "KGM".ToUpper
                    Return AddressOf GetKingdomTaxonTypeID
                Case Else
                    Throw New Exception("HSYNCODE " & ComCode & " is not supported")
            End Select
        End Function
#End Region

#Region "VDECISIONS"
        Private Sub PhysicalDeleteVDECISIONS()
            Try
                If Me.mTransferDeletes.VDECISIONS.Rows.Count > 0 Then
                    Dim DecisionGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionService As DataObjects.Service.TaxonomyDecisionService = DecisionGen.ServiceObject
                    For Each Row As Transfer.VDECISIONSRow In Me.mTransferDeletes.VDECISIONS.Rows
                        'Delete the Decision.
                        DecisionService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.DECRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVDECISIONS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVDECISIONS()
            Try
                If Me.mTransferInserts.VDECISIONS.Rows.Count > 0 Then
                    Dim DecisionGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionService As DataObjects.Service.TaxonomyDecisionService = DecisionGen.ServiceObject
                    For Each NewValue As Transfer.VDECISIONSRow In Me.mTransferInserts.VDECISIONS.Rows
                        'Insert the Decision record.
                        DecisionService.Insert(source:=TaxonomyRowSourceEnum.Standard, decisionid:=CType(NewValue.DECRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.DECSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, srgopinion:=NewValue.DECOPINION, decisiondate:=NewValue.DECDATE, article4point6importrestriction:=NewValue.DECSUBPARA46, decisionlevel:=NewValue.DECLEVEL, decisionmiscellaneous:=NewValue.DECMISC, countryid:=NewValue.DECCTYRECID, note:=NewValue.DECNOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDECISIONS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVDECISIONS()
            Try
                If Me.mTransferUpdates.VDECISIONS.Rows.Count > 0 Then
                    Dim DecisionGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionService As DataObjects.Service.TaxonomyDecisionService = DecisionGen.ServiceObject
                    For Each NewValue As Transfer.VDECISIONSRow In Me.mTransferUpdates.VDECISIONS.Rows
                        'Update the Decision record.
                        DecisionService.Update(source:=TaxonomyRowSourceEnum.Standard, decisionid:=CType(NewValue.DECRECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.DECSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, srgopinion:=NewValue.DECOPINION, decisiondate:=NewValue.DECDATE, article4point6importrestriction:=NewValue.DECSUBPARA46, decisionlevel:=NewValue.DECLEVEL, decisionmiscellaneous:=NewValue.DECMISC, countryid:=NewValue.DECCTYRECID, note:=NewValue.DECNOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVDECISIONS", Ex)
            End Try
        End Sub
#End Region

#Region "VHIGHDECISIONS"
        Private Sub PhysicalDeleteVHIGHDECISIONS()
            Try
                If Me.mTransferDeletes.VHIGHDECISIONS.Rows.Count > 0 Then
                    Dim DecisionsGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionsService As DataObjects.Service.TaxonomyDecisionService = DecisionsGen.ServiceObject
                    For Each Row As Transfer.VHIGHDECISIONSRow In Me.mTransferDeletes.VHIGHDECISIONS.Rows
                        'Delete the Decisions.
                        DecisionsService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Higher, CType(Row.HDECRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVHIGHDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVHIGHDECISIONS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVHIGHDECISIONS()
            Try
                If Me.mTransferInserts.VHIGHDECISIONS.Rows.Count > 0 Then
                    Dim DecisionsGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionsService As DataObjects.Service.TaxonomyDecisionService = DecisionsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHDECISIONSRow In Me.mTransferInserts.VHIGHDECISIONS.Rows
                        'Insert the Decisions record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHDECISIONSTaxonTypeID(NewValue.HDECCODE)
                        DecisionsService.Insert(source:=TaxonomyRowSourceEnum.Higher, Decisionid:=CType(NewValue.HDECRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.HDECCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), srgopinion:=NewValue.HDECOPINION, decisiondate:=NewValue.HDECDATE, article4point6importrestriction:=NewValue.HDECSUBPARA46, decisionlevel:=NewValue.HDECLEVEL, decisionmiscellaneous:=NewValue.HDECMISC, countryid:=NewValue.HDECCTYRECID, note:=NewValue.HDECNOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHDECISIONS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVHIGHDECISIONS()
            Try
                If Me.mTransferUpdates.VHIGHDECISIONS.Rows.Count > 0 Then
                    Dim DecisionsGen As New DataObjects.Entity.TaxonomyDecision
                    Dim DecisionsService As DataObjects.Service.TaxonomyDecisionService = DecisionsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHDECISIONSRow In Me.mTransferUpdates.VHIGHDECISIONS.Rows
                        'Update the Decisions record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHDECISIONSTaxonTypeID(NewValue.HDECCODE)
                        DecisionsService.Update(source:=TaxonomyRowSourceEnum.Higher, Decisionid:=CType(NewValue.HDECRECID, Int32), kingdomid:=GetKingdomID, taxonId:=CType(NewValue.HDECCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), srgopinion:=NewValue.HDECOPINION, decisiondate:=NewValue.HDECDATE, article4point6importrestriction:=NewValue.HDECSUBPARA46, decisionlevel:=NewValue.HDECLEVEL, decisionmiscellaneous:=NewValue.HDECMISC, countryid:=NewValue.HDECCTYRECID, note:=NewValue.HDECNOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHDECISIONS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHDECISIONS", Ex)
            End Try
        End Sub

        Private Function GetVHIGHDECISIONSTaxonTypeID(ByVal ComCode As String) As TaxonTypeDelegate
            Select Case ComCode.ToUpper
                Case "SPC".ToUpper
                    Return AddressOf GetSpeciesTaxonTypeID
                Case "GEN".ToUpper
                    Return AddressOf GetGenusTaxonTypeID
                Case "FAM".ToUpper
                    Return AddressOf GetFamilyTaxonTypeID
                Case "ORD".ToUpper
                    Return AddressOf GetOrderTaxonTypeID
                Case "CLA".ToUpper
                    Return AddressOf GetClassTaxonTypeID
                Case "PHY".ToUpper
                    Return AddressOf GetPhylumTaxonTypeID
                Case "KGM".ToUpper
                    Return AddressOf GetKingdomTaxonTypeID
                Case Else
                    Throw New Exception("HDECCODE " & ComCode & " is not supported")
            End Select
        End Function
#End Region

#Region "VLEGAL"
        Private Sub PhysicalDeleteVLEGAL()
            Try
                If Me.mTransferDeletes.VLEGAL.Rows.Count > 0 Then
                    Dim LegislationGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationService As DataObjects.Service.TaxonomyLegislationService = LegislationGen.ServiceObject
                    For Each Row As Transfer.VLEGALRow In Me.mTransferDeletes.VLEGAL.Rows
                        'Delete the Legislation.
                        LegislationService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.LEGRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVLEGAL", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVLEGAL()
            Try
                If Me.mTransferInserts.VLEGAL.Rows.Count > 0 Then
                    Dim LegislationGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationService As DataObjects.Service.TaxonomyLegislationService = LegislationGen.ServiceObject
                    For Each NewValue As Transfer.VLEGALRow In Me.mTransferInserts.VLEGAL.Rows
                        'Get the country record that links to this legislation.
                        Dim CountryGen As New DataObjects.Entity.Country
                        Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                        Dim Country As DataObjects.Entity.Country = CountryService.GetByIndex_IX_Country(iso2countrycode:=NewValue.LEGISO2, includeinactive:=False, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Legislation record.
                        LegislationService.Insert(source:=TaxonomyRowSourceEnum.Standard, Legislationid:=CType(NewValue.LEGRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.LEGSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, legislationnameid:=CType(NewValue.LEGLNMRECID, Int32), datelisted:=NewValue.LEGDATELISTED, listing:=NewValue.LEGLISTING, issplitlisted:=NewValue.LEGSPLITLISTED, hashighertaxonomyprotection:=NewValue.LEGPROTHIGHERTAX, iso2countryid:=Country.Id, miscellaneous:=NewValue.LEGMISC, note:=NewValue.LEGNOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVLEGAL", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVLEGAL()
            Try
                If Me.mTransferUpdates.VLEGAL.Rows.Count > 0 Then
                    Dim LegislationGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationService As DataObjects.Service.TaxonomyLegislationService = LegislationGen.ServiceObject
                    For Each NewValue As Transfer.VLEGALRow In Me.mTransferUpdates.VLEGAL.Rows
                        'Get the country record that links to this legislation.
                        Dim CountryGen As New DataObjects.Entity.Country
                        Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                        Dim Country As DataObjects.Entity.Country = CountryService.GetByIndex_IX_Country(iso2countrycode:=NewValue.LEGISO2, includeinactive:=False, Transaction:=Me.Transaction).Entities(0)
                        'Update the Legislation record.
                        LegislationService.Update(source:=TaxonomyRowSourceEnum.Standard, Legislationid:=CType(NewValue.LEGRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.LEGSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, legislationnameid:=CType(NewValue.LEGLNMRECID, Int32), datelisted:=NewValue.LEGDATELISTED, listing:=NewValue.LEGLISTING, issplitlisted:=NewValue.LEGSPLITLISTED, hashighertaxonomyprotection:=NewValue.LEGPROTHIGHERTAX, iso2countryid:=Country.Id, miscellaneous:=NewValue.LEGMISC, note:=NewValue.LEGNOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVLEGAL", Ex)
            End Try
        End Sub
#End Region

#Region "VHIGHLEGAL"
        Private Sub PhysicalDeleteVHIGHLEGAL()
            Try
                If Me.mTransferDeletes.VHIGHLEGAL.Rows.Count > 0 Then
                    Dim LegislationsGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationsService As DataObjects.Service.TaxonomyLegislationService = LegislationsGen.ServiceObject
                    For Each Row As Transfer.VHIGHLEGALRow In Me.mTransferDeletes.VHIGHLEGAL.Rows
                        'Delete the Legislations.
                        LegislationsService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Higher, CType(Row.HLEGRECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVHIGHLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVHIGHLEGAL", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVHIGHLEGAL()
            Try
                If Me.mTransferInserts.VHIGHLEGAL.Rows.Count > 0 Then
                    Dim LegislationsGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationsService As DataObjects.Service.TaxonomyLegislationService = LegislationsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHLEGALRow In Me.mTransferInserts.VHIGHLEGAL.Rows
                        'Get the country record that links to this legislation.
                        Dim CountryGen As New DataObjects.Entity.Country
                        Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                        Dim Country As DataObjects.Entity.Country = CountryService.GetByIndex_IX_Country(iso2countrycode:=NewValue.HLEGISO2, includeinactive:=False, Transaction:=Me.Transaction).Entities(0)
                        'Insert the Legislations record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHLEGALTaxonTypeID(NewValue.HLEGCODE)
                        LegislationsService.Insert(source:=TaxonomyRowSourceEnum.Higher, Legislationid:=CType(NewValue.HLEGRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.HLEGCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), legislationnameid:=CType(NewValue.HLEGLNMRECID, Int32), datelisted:=NewValue.HLEGDATELISTED, listing:=NewValue.HLEGLISTING, issplitlisted:=NewValue.HLEGSPLITLISTED, hashighertaxonomyprotection:=NewValue.HLEGPROTHIGHERTAX, iso2countryid:=Country.Id, miscellaneous:=Nothing, note:=NewValue.HLEGNOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHLEGAL", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVHIGHLEGAL()
            Try
                If Me.mTransferUpdates.VHIGHLEGAL.Rows.Count > 0 Then
                    Dim LegislationsGen As New DataObjects.Entity.TaxonomyLegislation
                    Dim LegislationsService As DataObjects.Service.TaxonomyLegislationService = LegislationsGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHLEGALRow In Me.mTransferUpdates.VHIGHLEGAL.Rows
                        'Get the country record that links to this legislation.
                        Dim CountryGen As New DataObjects.Entity.Country
                        Dim CountryService As DataObjects.Service.CountryService = CountryGen.ServiceObject
                        Dim Country As DataObjects.Entity.Country = CountryService.GetByIndex_IX_Country(iso2countrycode:=NewValue.HLEGISO2, includeinactive:=False, Transaction:=Me.Transaction).Entities(0)
                        'Update the Legislations record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHLEGALTaxonTypeID(NewValue.HLEGCODE)
                        LegislationsService.Update(source:=TaxonomyRowSourceEnum.Higher, Legislationid:=CType(NewValue.HLEGRECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.HLEGCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), legislationnameid:=CType(NewValue.HLEGLNMRECID, Int32), datelisted:=NewValue.HLEGDATELISTED, listing:=NewValue.HLEGLISTING, issplitlisted:=NewValue.HLEGSPLITLISTED, hashighertaxonomyprotection:=NewValue.HLEGPROTHIGHERTAX, iso2countryid:=Country.Id, miscellaneous:=Nothing, note:=NewValue.HLEGNOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHLEGAL")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHLEGAL", Ex)
            End Try
        End Sub

        Private Function GetVHIGHLEGALTaxonTypeID(ByVal ComCode As String) As TaxonTypeDelegate
            Select Case ComCode.ToUpper
                Case "SPC".ToUpper
                    Return AddressOf GetSpeciesTaxonTypeID
                Case "GEN".ToUpper
                    Return AddressOf GetGenusTaxonTypeID
                Case "FAM".ToUpper
                    Return AddressOf GetFamilyTaxonTypeID
                Case "ORD".ToUpper
                    Return AddressOf GetOrderTaxonTypeID
                Case "CLA".ToUpper
                    Return AddressOf GetClassTaxonTypeID
                Case "PHY".ToUpper
                    Return AddressOf GetPhylumTaxonTypeID
                Case "KGM".ToUpper
                    Return AddressOf GetKingdomTaxonTypeID
                Case Else
                    Throw New Exception("HLEGCODE " & ComCode & " is not supported")
            End Select
        End Function
#End Region

#Region "VQUOTAS"
        Private Sub PhysicalDeleteVQUOTAS()
            Try
                If Me.mTransferDeletes.VQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each Row As Transfer.VQUOTASRow In Me.mTransferDeletes.VQUOTAS.Rows
                        'Delete the Quotas.
                        QuotasService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Standard, CType(Row.QUORECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVQUOTAS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVQUOTAS()
            Try
                If Me.mTransferInserts.VQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each NewValue As Transfer.VQUOTASRow In Me.mTransferInserts.VQUOTAS.Rows
                        'Insert the Quotas record.
                        QuotasService.Insert(source:=TaxonomyRowSourceEnum.Standard, id:=CType(NewValue.QUORECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.QUOSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, quotayear:=NewValue.QUOYEAR, quotavolume:=NewValue.QUOEXPORTQUOTA, quotaunit:=NewValue.QUOUNIT, exportquotanotificationid:=NewValue.QUONOTRECID, countryid:=NewValue.QUOCTYRECID, exportquotatermid:=NewValue.QUOQUOTRMRECID, exportquotasourceid:=NewValue.QUOQUOSRCRECID, note:=NewValue.QUONOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVQUOTAS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVQUOTAS()
            Try
                If Me.mTransferUpdates.VQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each NewValue As Transfer.VQUOTASRow In Me.mTransferUpdates.VQUOTAS.Rows
                        'Update the Quotas record.
                        QuotasService.Update(source:=TaxonomyRowSourceEnum.Standard, id:=CType(NewValue.QUORECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.QUOSPCRECID, Int32), taxontypeid:=GetSpeciesTaxonTypeID, quotayear:=NewValue.QUOYEAR, quotavolume:=NewValue.QUOEXPORTQUOTA, quotaunit:=NewValue.QUOUNIT, exportquotanotificationid:=NewValue.QUONOTRECID, countryid:=NewValue.QUOCTYRECID, exportquotatermid:=NewValue.QUOQUOTRMRECID, exportquotasourceid:=NewValue.QUOQUOSRCRECID, note:=NewValue.QUONOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVQUOTAS", Ex)
            End Try
        End Sub
#End Region

#Region "VHIGHQUOTAS"
        Private Sub PhysicalDeleteVHIGHQUOTAS()
            Try
                If Me.mTransferDeletes.VHIGHQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each Row As Transfer.VHIGHQUOTASRow In Me.mTransferDeletes.VHIGHQUOTAS.Rows
                        'Delete the Quotas.
                        QuotasService.DeleteById(idcolumns:=New Integer() {TaxonomyRowSourceEnum.Higher, CType(Row.HQUORECID, Int32)}, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalDeleteVHIGHQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalDeleteVHIGHQUOTAS", Ex)
            End Try
        End Sub

        Private Sub PhysicalInsertVHIGHQUOTAS()
            Try
                If Me.mTransferInserts.VHIGHQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHQUOTASRow In Me.mTransferInserts.VHIGHQUOTAS.Rows
                        'Insert the Quotas record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHQUOTASTaxonTypeID(NewValue.HQUOCODE)
                        QuotasService.Insert(source:=TaxonomyRowSourceEnum.Higher, id:=CType(NewValue.HQUORECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.HQUOCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), quotayear:=NewValue.HQUOYEAR, quotavolume:=NewValue.HQUOEXPORTQUOTA, quotaunit:=NewValue.HQUOQUOUNIT, exportquotanotificationid:=NewValue.HQUONOTRECID, countryid:=NewValue.HQUOCTYRECID, exportquotatermid:=NewValue.HQUOQUOTRMRECID, exportquotasourceid:=NewValue.HQUOQUOSRCRECID, note:=NewValue.HQUONOTES, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHQUOTAS", Ex)
            End Try
        End Sub

        Private Sub PhysicalUpdateVHIGHQUOTAS()
            Try
                If Me.mTransferUpdates.VHIGHQUOTAS.Rows.Count > 0 Then
                    Dim QuotasGen As New DataObjects.Entity.TaxonomyExportQuota
                    Dim QuotasService As DataObjects.Service.TaxonomyExportQuotaService = QuotasGen.ServiceObject
                    For Each NewValue As Transfer.VHIGHQUOTASRow In Me.mTransferUpdates.VHIGHQUOTAS.Rows
                        'Update the Quotas record.
                        Dim TaxonTypeFunction As TaxonTypeDelegate = GetVHIGHQUOTASTaxonTypeID(NewValue.HQUOCODE)
                        QuotasService.Update(source:=TaxonomyRowSourceEnum.Higher, id:=CType(NewValue.HQUORECID, Int32), kingdomid:=GetKingdomID, taxonid:=CType(NewValue.HQUOCODERECID, Int32), taxontypeid:=TaxonTypeFunction(), quotayear:=NewValue.HQUOYEAR, quotavolume:=NewValue.HQUOEXPORTQUOTA, quotaunit:=NewValue.HQUOQUOUNIT, exportquotanotificationid:=NewValue.HQUONOTRECID, countryid:=NewValue.HQUOCTYRECID, exportquotatermid:=NewValue.HQUOQUOTRMRECID, exportquotasourceid:=NewValue.HQUOQUOSRCRECID, note:=NewValue.HQUONOTES, checksum:=0, Transaction:=Me.Transaction)
                    Next
                    SaveTransaction("PhysicalUpdateVHIGHQUOTAS")
                End If
            Catch Ex As Exception
                Throw New Exception("Error with PhysicalUpdateVHIGHQUOTAS", Ex)
            End Try
        End Sub

        Private Function GetVHIGHQUOTASTaxonTypeID(ByVal ComCode As String) As TaxonTypeDelegate
            Select Case ComCode.ToUpper
                Case "SPC".ToUpper
                    Return AddressOf GetSpeciesTaxonTypeID
                Case "GEN".ToUpper
                    Return AddressOf GetGenusTaxonTypeID
                Case "FAM".ToUpper
                    Return AddressOf GetFamilyTaxonTypeID
                Case "ORD".ToUpper
                    Return AddressOf GetOrderTaxonTypeID
                Case "CLA".ToUpper
                    Return AddressOf GetClassTaxonTypeID
                Case "PHY".ToUpper
                    Return AddressOf GetPhylumTaxonTypeID
                Case "KGM".ToUpper
                    Return AddressOf GetKingdomTaxonTypeID
                Case Else
                    Throw New Exception("HQUOCODE " & ComCode & " is not supported")
            End Select
        End Function
#End Region

        Private Function GetKingdomID() As Int32
            Return 1
        End Function

        Private Function GetStockTaxonTypeID() As Int32
            Return 9
        End Function

        Private Function GetEpithetTaxonTypeID() As Int32
            Return 8
        End Function

        Private Function GetSpeciesTaxonTypeID() As Int32
            Return 7
        End Function

        Private Function GetGenusTaxonTypeID() As Int32
            Return 6
        End Function

        Private Function GetFamilyTaxonTypeID() As Int32
            Return 5
        End Function

        Private Function GetOrderTaxonTypeID() As Int32
            Return 4
        End Function

        Private Function GetClassTaxonTypeID() As Int32
            Return 3
        End Function

        Private Function GetPhylumTaxonTypeID() As Int32
            Return 2
        End Function

        Private Function GetKingdomTaxonTypeID() As Int32
            Return 1
        End Function

        Private Function GetTaxonStatus(ByVal TaxonStatus As String) As Int32
            Select Case TaxonStatus
                Case "A"
                    Return 1
                Case "S"
                    Return 2
                Case "T"
                    Return 3
                Case "D"
                    Return 4
                Case "K"
                    Return 5
                Case "N"
                    Return 6
                Case "U"
                    Return 7
            End Select
        End Function

        Private Function GetUser() As Int32
            Return SystemUser.SystemUserEnum.UNEPWCMCDataLoadUser
        End Function
        Private Function GetModifiedDate() As Object
            Return System.DateTime.Now
        End Function
        Private Function GetCreatedDate() As DateTime
            Return System.DateTime.Now
        End Function
        Private Function GetNoteSubject() As String
            Return "UNEP-WCMC Note"
        End Function

        Private Function GetColumnsNameString(ByVal ColumnsCollection As DataColumnCollection, ByVal Columns() As Int32) As String
            Dim ColumnsName As New ArrayList
            For Each ColIdx As Int32 In Columns
                ColumnsName.Add(ColumnsCollection(ColIdx).ColumnName)
            Next
            Dim ColumnsArray(ColumnsName.Count - 1) As String
            ColumnsName.CopyTo(ColumnsArray)
            Return String.Join("  ", ColumnsArray)
        End Function
        Private Function GetColumnsNameString(ByVal ColumnsCollection As DataColumnCollection) As String
            Dim Columns As New ArrayList
            For Each Column As DataColumn In ColumnsCollection
                Columns.Add(Column.ColumnName)
            Next
            Dim ColumnsArray(Columns.Count - 1) As String
            Columns.CopyTo(ColumnsArray)
            Return String.Join("  ", ColumnsArray)
        End Function
        Private Function GetColumnsDataString(ByVal Row As DataRow, ByVal Columns() As Int32) As String
            Dim ColumnValues As New ArrayList
            For Each ColIdx As Int32 In Columns
                Try
                    ColumnValues.Add(CType(Row.ItemArray(ColIdx), String))
                Catch ex As Exception
                    Throw New Exception("Cannot convert column with index of " & ColIdx.ToString & " to a string value", ex)
                End Try
            Next
            Dim ColumnsValueArray(ColumnValues.Count - 1) As String
            ColumnValues.CopyTo(ColumnsValueArray)
            Return String.Join("  ", ColumnsValueArray)
        End Function
        Private Function GetColumnsDataString(ByVal Row As DataRow) As String
            Dim ColumnValues As New ArrayList
            For Each Column As Object In Row.ItemArray
                Try
                    ColumnValues.Add(CType(Column, String))
                Catch ex As Exception
                    Throw New Exception("Cannot convert columns to a string value", ex)
                End Try
            Next
            Dim ColumnsValueArray(ColumnValues.Count - 1) As String
            ColumnValues.CopyTo(ColumnsValueArray)
            Return String.Join("  ", ColumnsValueArray)
        End Function

        Private Function GetStockTaxonID(ByVal TaxonID As Long) As Int32
            Return CType(TaxonID + 1000000, Int32)
        End Function

        Private Function GetEpithetTaxonID(ByVal TaxonID As Long) As Int32
            Return CType(0 - TaxonID, Int32)
        End Function

        Private Function TableActionOrder(ByVal TableAction As TableActionOrderEnum) As String()
            Dim TableActionOrderArray As String() = New String() {"VLEGALNAME", "VQUOTATERMS", "VQUOTASOURCE", "VNOTIFICATION", "VUSETYPE", "VPART", "VLEVELOFUSE", "VAQUATIC", "VAREAOFUSE", "VCOUNTRY", "VBRU", "VKINGDOM", "VTAXPHYLUM", "VTAXCLASS", "VTAXORDER", "VFAMILY", "VGENUS", "VSPECIES", "VCOMMONNAME", "VHIGHCOMMON", "VDISTRIBAQUA", "VDISTRIBBRU", "VDISTRIBCTY", "VUSE", "VSYNLINK", "VHIGHSYNONYMS", "VDECISIONS", "VHIGHDECISIONS", "VLEGAL", "VHIGHLEGAL", "VQUOTAS", "VHIGHQUOTAS"}
            If TableAction = TableActionOrderEnum.PhysicalDelete Then
                Array.Reverse(TableActionOrderArray)
            End If
            Return TableActionOrderArray
        End Function

#End Region

        Private Const SaveInterval As Int32 = 1000

    End Class
End Namespace