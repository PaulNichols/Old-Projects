Namespace Application.Search
    <Serializable()> _
    Public Class SearchResults
        Friend Enum ColumnHeaderType
            One
            Two
            Three
            Four
            Five
            Six
            Seven
            Eight
            Nine
            Ten
            Eleven
            Twelve
            Thirteen
            Fourteen
            Fifteen
            Sixteen
            Seventeen
            Eighteen
            Nineteen
            Twenty
            TwentyOne
            TwentyTwo
            TwentyThree
        End Enum

        Public Sub New()
        End Sub

        Friend Sub New(ByVal data As Data.BaseSearchData(), ByVal type As ColumnHeaderType)
            mData = data
            CreateColumnInfo(type)
        End Sub

        Private Sub CreateColumnInfo(ByVal type As ColumnHeaderType)
            Select Case type
                Case ColumnHeaderType.One
                    ReDim mColumnInfo(14)
                    mColumnInfo(0) = New ApplicationSearchColumns("Permit/Certificate ID", "PermitId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(5) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(6) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(7) = New ApplicationSearchColumns("P/D", "PD")
                    mColumnInfo(8) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(9) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(10) = New ApplicationSearchColumns("Paid", "Paid")
                    mColumnInfo(11) = New ApplicationSearchColumns("SA Adv", "SAAdvice")
                    mColumnInfo(12) = New ApplicationSearchColumns("Referred", "Referred")
                    mColumnInfo(13) = New ApplicationSearchColumns("Inspec Adv", "InspectorateAdvice")
                    mColumnInfo(14) = New ApplicationSearchColumns("Re-Issued", "ReIssued")

                    mColumnInfo(0).LinkType = LinkTypes.Permit

                Case ColumnHeaderType.Two
                    ReDim mColumnInfo(9)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(5) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(6) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(7) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(8) = New ApplicationSearchColumns("No. of Permits", "NumberOfPermits")
                    mColumnInfo(9) = New ApplicationSearchColumns("Paid", "Paid")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Three
                    ReDim mColumnInfo(10)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(2) = New ApplicationSearchColumns("No. of Permits for Species", "NumberOfPermits")
                    mColumnInfo(3) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(4) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(6) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(7) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(8) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(9) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(10) = New ApplicationSearchColumns("Paid", "Paid")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Four
                    ReDim mColumnInfo(7)
                    mColumnInfo(0) = New ApplicationSearchColumns("Notification ID", "NotificationReference")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Of Import", "DateOfImport")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(6) = New ApplicationSearchColumns("P/D", "PD")
                    mColumnInfo(7) = New ApplicationSearchColumns("Quantity", "Quantity")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Five
                    ReDim mColumnInfo(9)
                    mColumnInfo(0) = New ApplicationSearchColumns("Customs Reference", "CustomsRef")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Seized", "DateSeized")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(6) = New ApplicationSearchColumns("P/D", "PD")
                    mColumnInfo(7) = New ApplicationSearchColumns("Quantity", "Quantity")
                    mColumnInfo(8) = New ApplicationSearchColumns("Active", "Active")
                    mColumnInfo(9) = New ApplicationSearchColumns("Linked", "Linked")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Six
                    ReDim mColumnInfo(12)
                    mColumnInfo(0) = New ApplicationSearchColumns("Permit/Certificate ID", "PermitId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(6) = New ApplicationSearchColumns("Country Of Origin", "CountryOfOrigin")
                    mColumnInfo(7) = New ApplicationSearchColumns("Quantity", "Quantity")
                    mColumnInfo(8) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(9) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(10) = New ApplicationSearchColumns("SA Adv", "SAAdvice")
                    mColumnInfo(11) = New ApplicationSearchColumns("Referred", "Referred")
                    mColumnInfo(12) = New ApplicationSearchColumns("Inspec Adv", "InspectorateAdvice")

                    mColumnInfo(0).LinkType = LinkTypes.Permit

                Case ColumnHeaderType.Seven
                    ReDim mColumnInfo(8)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("No. of Permits", "NumberOfPermits", False)
                    mColumnInfo(2) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(5) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(6) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(7) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(8) = New ApplicationSearchColumns("Assigned To", "AssignedTo")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Eight
                    ReDim mColumnInfo(9)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(2) = New ApplicationSearchColumns("No. of Permits for Species", "NumberOfPermits", False)
                    mColumnInfo(3) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(4) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(6) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(7) = New ApplicationSearchColumns("Country Of Origin", "CountryOfOrigin")
                    mColumnInfo(8) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(9) = New ApplicationSearchColumns("Assigned To", "AssignedTo")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Nine
                    ReDim mColumnInfo(8)
                    mColumnInfo(0) = New ApplicationSearchColumns("Permit/Certificate ID", "PermitId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(6) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(7) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(8) = New ApplicationSearchColumns("Inspec Adv", "InspectorateAdvice")

                    mColumnInfo(0).LinkType = LinkTypes.Permit

                Case ColumnHeaderType.Ten
                    ReDim mColumnInfo(7)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("No. of Permits", "NumberOfPermits", False)
                    mColumnInfo(2) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(5) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(6) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(7) = New ApplicationSearchColumns("Assigned To", "AssignedTo")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Eleven
                    ReDim mColumnInfo(8)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(2) = New ApplicationSearchColumns("No. of Permits for Species", "NumberOfPermits", False)
                    mColumnInfo(3) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(4) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(6) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(7) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(8) = New ApplicationSearchColumns("Assigned To", "AssignedTo")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Twelve
                    ReDim mColumnInfo(7)
                    mColumnInfo(0) = New ApplicationSearchColumns("Notification ID", "NotificationReference")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(7) = New ApplicationSearchColumns("Quantity", "Quantity")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Thirteen
                    ReDim mColumnInfo(7)
                    mColumnInfo(0) = New ApplicationSearchColumns("Customs Reference", "CustomsRef")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Seized", "DateSeized")
                    mColumnInfo(2) = New ApplicationSearchColumns("Party Name", "PartyName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Party Id", "PartyId")
                    mColumnInfo(4) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(5) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(6) = New ApplicationSearchColumns("Quantity", "Quantity")
                    mColumnInfo(7) = New ApplicationSearchColumns("Active", "Active")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Fourteen
                    ReDim mColumnInfo(7)
                    mColumnInfo(0) = New ApplicationSearchColumns("Permit/Certificate ID", "PermitId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(3) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(4) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(5) = New ApplicationSearchColumns("P/D", "PD")
                    mColumnInfo(6) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(7) = New ApplicationSearchColumns("Paid", "Paid")

                    mColumnInfo(0).LinkType = LinkTypes.Permit

                Case ColumnHeaderType.Fifteen
                    ReDim mColumnInfo(6)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Permit Type", "PermitType")
                    mColumnInfo(3) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(4) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(5) = New ApplicationSearchColumns("No. of Permits", "NumberOfPermits")
                    mColumnInfo(6) = New ApplicationSearchColumns("Paid", "Paid")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Sixteen
                    ReDim mColumnInfo(6)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationNumber")
                    mColumnInfo(1) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(2) = New ApplicationSearchColumns("No. of Permits for Species", "NumberOfPermits")
                    mColumnInfo(3) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(4) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(5) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(6) = New ApplicationSearchColumns("Paid", "Paid")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Seventeen
                    ReDim mColumnInfo(4)
                    mColumnInfo(0) = New ApplicationSearchColumns("Notification ID", "NotificationReference")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Received", "DateReceived")
                    mColumnInfo(2) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(3) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Quantity", "Quantity")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Eighteen
                    ReDim mColumnInfo(4)
                    mColumnInfo(0) = New ApplicationSearchColumns("Customs Reference", "CustomsRef")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Seized", "DateSeized")
                    mColumnInfo(2) = New ApplicationSearchColumns("Country Code", "ISOCode")
                    mColumnInfo(3) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Quantity", "Quantity")

                    mColumnInfo(0).LinkType = LinkTypes.Notification

                Case ColumnHeaderType.Nineteen
                    ReDim mColumnInfo(8)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Request Submitted", "RequestSubmittedDate")
                    mColumnInfo(2) = New ApplicationSearchColumns("Keeper ID", "KeeperId")
                    mColumnInfo(3) = New ApplicationSearchColumns("Keepers Name", "KeeperName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Specimen ID Type", "SpecimenType")
                    mColumnInfo(6) = New ApplicationSearchColumns("Specimen ID Number", "SpecimenId")
                    mColumnInfo(7) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(8) = New ApplicationSearchColumns("Assigned To", "AssignedTo")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.Twenty
                    ReDim mColumnInfo(5)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Request Submitted", "RequestSubmittedDate")
                    mColumnInfo(2) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Specimen ID Type", "SpecimenType")
                    mColumnInfo(4) = New ApplicationSearchColumns("Specimen ID Number", "SpecimenId")
                    mColumnInfo(5) = New ApplicationSearchColumns("Status", "Status")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.TwentyOne
                    ReDim mColumnInfo(9)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Request Submitted", "RequestSubmittedDate")
                    mColumnInfo(2) = New ApplicationSearchColumns("Keeper ID", "KeeperId")
                    mColumnInfo(3) = New ApplicationSearchColumns("Keepers Name", "KeeperName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Hatch Date", "HatchDate")
                    mColumnInfo(6) = New ApplicationSearchColumns("Number Of Eggs", "NumberOfEggs")
                    mColumnInfo(7) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(8) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(9) = New ApplicationSearchColumns("Inspec Adv", "InspectorateAdvice")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.TwentyTwo
                    ReDim mColumnInfo(9)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Referred", "DateReferred")
                    mColumnInfo(2) = New ApplicationSearchColumns("Keeper ID", "KeeperId")
                    mColumnInfo(3) = New ApplicationSearchColumns("Keepers Name", "KeeperName")
                    mColumnInfo(4) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(5) = New ApplicationSearchColumns("Hatch Date", "HatchDate")
                    mColumnInfo(6) = New ApplicationSearchColumns("Number Of Eggs", "NumberOfEggs")
                    mColumnInfo(7) = New ApplicationSearchColumns("Status", "Status")
                    mColumnInfo(8) = New ApplicationSearchColumns("Assigned To", "AssignedTo")
                    mColumnInfo(9) = New ApplicationSearchColumns("Inspec Adv", "InspectorateAdvice")

                    mColumnInfo(0).LinkType = LinkTypes.Application

                Case ColumnHeaderType.TwentyThree
                    ReDim mColumnInfo(5)
                    mColumnInfo(0) = New ApplicationSearchColumns("Application ID", "ApplicationId")
                    mColumnInfo(1) = New ApplicationSearchColumns("Date Request Submitted", "RequestSubmittedDate")
                    mColumnInfo(2) = New ApplicationSearchColumns("Scientific Name", "ScientificName")
                    mColumnInfo(3) = New ApplicationSearchColumns("Hatch Date", "HatchDate")
                    mColumnInfo(4) = New ApplicationSearchColumns("Number Of Eggs", "NumberOfEggs")
                    mColumnInfo(5) = New ApplicationSearchColumns("Status", "CustomerStatus")

                    mColumnInfo(0).LinkType = LinkTypes.Application
            End Select
        End Sub

        Public Property Data() As Data.BaseSearchData()
            Get
                Return mData
            End Get
            Set(ByVal Value As Data.BaseSearchData())
                mData = Value
            End Set
        End Property
        Private mData As Data.BaseSearchData()

        Public Property ColumnInfo() As ApplicationSearchColumns()
            Get
                Return mColumnInfo
            End Get
            Set(ByVal Value As ApplicationSearchColumns())
                mColumnInfo = Value
            End Set
        End Property
        Private mColumnInfo As ApplicationSearchColumns()

        Friend Shared ReadOnly Property OrderColSQL(ByVal type As ColumnHeaderType, ByVal columnIndex As Int32, ByVal direction As Application.Search.ApplicationSearchCriteriaBase.SortDirection) As String
            Get
                Dim SortOrder As String
                If direction = Application.Search.ApplicationSearchCriteriaBase.SortDirection.Asc Then
                    SortOrder = "ASC"
                Else
                    SortOrder = "DESC"
                End If

                Dim OrderSQL As String
                Select Case type
                    Case ColumnHeaderType.One
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId, DisplayNumber, SequenceNumber"
                            Case 2 : OrderSQL = "OrderDate"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "PermitType"
                            Case 6 : OrderSQL = "ISOCode"
                            Case 7 : OrderSQL = "ScientificName"
                            Case 8 : OrderSQL = "PD"
                            Case 9 : OrderSQL = "PermitStatus"
                            Case 10 : OrderSQL = "AssignedTo"
                            Case 11 : OrderSQL = "Paid"
                            Case 12 : OrderSQL = "SAAdvice"
                            Case 13 : OrderSQL = "Referred"
                            Case 14 : OrderSQL = "InspectionAdvice"
                            Case 15 : OrderSQL = "ReIssued"
                        End Select
                    Case ColumnHeaderType.Two
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "OrderDate"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "PermitType"
                            Case 6 : OrderSQL = "ISOCode"
                            Case 7 : OrderSQL = "PermitStatus"
                            Case 8 : OrderSQL = "AssignedTo"
                            Case 9 : OrderSQL = "NumberOfPermits"
                            Case 10 : OrderSQL = "Paid"
                        End Select
                    Case ColumnHeaderType.Three
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "ScientificName"
                            Case 3 : OrderSQL = "NumberOfPermits"
                            Case 4 : OrderSQL = "OrderDate"
                            Case 5 : OrderSQL = "DisplayName"
                            Case 6 : OrderSQL = "PartyId"
                            Case 7 : OrderSQL = "PermitType"
                            Case 8 : OrderSQL = "ISOCode"
                            Case 9 : OrderSQL = "PermitStatus"
                            Case 10 : OrderSQL = "AssignedTo"
                            Case 11 : OrderSQL = "Paid"
                        End Select
                    Case ColumnHeaderType.Four
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "DateOfImport"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "ISOCode"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "PD"
                            Case 8 : OrderSQL = "Qty"
                        End Select
                    Case ColumnHeaderType.Five
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "DateOfImport"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "ISOCode"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "PD"
                            Case 8 : OrderSQL = "Qty"
                            Case 9 : OrderSQL = "Active"
                            Case 10 : OrderSQL = "Linked"
                        End Select
                    Case ColumnHeaderType.Six
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "DateReferred"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "PermitType"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "CountryOfOrigin"
                            Case 8 : OrderSQL = "Quantity"
                            Case 9 : OrderSQL = "PermitStatus"
                            Case 10 : OrderSQL = "AssignedTo"
                            Case 11 : OrderSQL = "SAAdvice"
                            Case 12 : OrderSQL = "Referred"
                            Case 13 : OrderSQL = "InspectionAdvice"
                        End Select
                    Case ColumnHeaderType.Seven
                        If columnIndex = 0 Then columnIndex = 3
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "NumberOfPermits"
                            Case 3 : OrderSQL = "DateReferred"
                            Case 4 : OrderSQL = "DisplayName"
                            Case 5 : OrderSQL = "PartyId"
                            Case 6 : OrderSQL = "PermitType"
                            Case 7 : OrderSQL = "ISOCode"
                            Case 8 : OrderSQL = "PermitStatus"
                            Case 9 : OrderSQL = "AssignedTo"
                        End Select
                    Case ColumnHeaderType.Eight
                        If columnIndex = 0 Then columnIndex = 4
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "ScientificName"
                            Case 3 : OrderSQL = "NumberOfPermits"
                            Case 4 : OrderSQL = "DateReferred"
                            Case 5 : OrderSQL = "DisplayName"
                            Case 6 : OrderSQL = "PartyId"
                            Case 7 : OrderSQL = "PermitType"
                            Case 8 : OrderSQL = "CountryOfOrigin"
                            Case 9 : OrderSQL = "PermitStatus"
                            Case 10 : OrderSQL = "AssignedTo"
                        End Select
                    Case ColumnHeaderType.Nine
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId, DisplayNumber, SequenceNumber"
                            Case 2 : OrderSQL = "DateReferred"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "PermitType"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "PermitStatus"
                            Case 8 : OrderSQL = "AssignedTo"
                            Case 9 : OrderSQL = "InspectionAdvice"
                        End Select
                    Case ColumnHeaderType.Ten
                        If columnIndex = 0 Then columnIndex = 3
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "NumberOfPermits"
                            Case 3 : OrderSQL = "DateReferred"
                            Case 4 : OrderSQL = "DisplayName"
                            Case 5 : OrderSQL = "PartyId"
                            Case 6 : OrderSQL = "PermitType"
                            Case 7 : OrderSQL = "PermitStatus"
                            Case 8 : OrderSQL = "AssignedTo"
                        End Select
                    Case ColumnHeaderType.Eleven
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "ScientificName"
                            Case 3 : OrderSQL = "NumberOfPermits"
                            Case 4 : OrderSQL = "DateReferred"
                            Case 5 : OrderSQL = "DisplayName"
                            Case 6 : OrderSQL = "PartyId"
                            Case 7 : OrderSQL = "PermitType"
                            Case 8 : OrderSQL = "PermitStatus"
                            Case 9 : OrderSQL = "AssignedTo"
                        End Select
                    Case ColumnHeaderType.Twelve
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "RecievedDate"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "ISOCode"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "Qty"
                        End Select
                    Case ColumnHeaderType.Thirteen
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "DateOfImport"
                            Case 3 : OrderSQL = "DisplayName"
                            Case 4 : OrderSQL = "PartyId"
                            Case 5 : OrderSQL = "ISOCode"
                            Case 6 : OrderSQL = "ScientificName"
                            Case 7 : OrderSQL = "Qty"
                            Case 8 : OrderSQL = "Active"
                        End Select
                    Case ColumnHeaderType.Fourteen
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId, DisplayNumber, SequenceNumber"
                            Case 2 : OrderSQL = "OrderDate"
                            Case 3 : OrderSQL = "PermitType"
                            Case 4 : OrderSQL = "ISOCode"
                            Case 5 : OrderSQL = "ScientificName"
                            Case 6 : OrderSQL = "PD"
                            Case 7 : OrderSQL = "PermitStatus"
                            Case 8 : OrderSQL = "Paid"
                        End Select
                    Case ColumnHeaderType.Fifteen
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "OrderDate"
                            Case 3 : OrderSQL = "PermitType"
                            Case 4 : OrderSQL = "ISOCode"
                            Case 5 : OrderSQL = "PermitStatus"
                            Case 6 : OrderSQL = "NumberOfPermits"
                            Case 7 : OrderSQL = "Paid"
                        End Select
                    Case ColumnHeaderType.Sixteen
                        If columnIndex = 0 Then columnIndex = 2
                        Select Case columnIndex
                            Case 1 : OrderSQL = "ApplicationId"
                            Case 2 : OrderSQL = "ScientificName"
                            Case 3 : OrderSQL = "NumberOfPermits"
                            Case 4 : OrderSQL = "OrderDate"
                            Case 5 : OrderSQL = "ISOCode"
                            Case 6 : OrderSQL = "PermitStatus"
                            Case 7 : OrderSQL = "Paid"
                        End Select
                    Case ColumnHeaderType.Seventeen
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "RecievedDate"
                            Case 3 : OrderSQL = "ISOCode"
                            Case 4 : OrderSQL = "ScientificName"
                            Case 5 : OrderSQL = "Qty"
                        End Select
                    Case ColumnHeaderType.Eighteen
                        If columnIndex = 0 Then columnIndex = 1
                        Select Case columnIndex
                            Case 1 : OrderSQL = "NotificationRef"
                            Case 2 : OrderSQL = "DateOfImport"
                            Case 3 : OrderSQL = "ISOCode"
                            Case 4 : OrderSQL = "ScientificName"
                            Case 5 : OrderSQL = "Qty"
                        End Select
                End Select

                If Not OrderSQL Is Nothing AndAlso OrderSQL.Length > 0 Then
                    OrderSQL = String.Concat(" Order By ", OrderSQL, " ", SortOrder)
                End If

                'If type = ColumnHeaderType.One AndAlso columnIndex = 1 Then
                '    OrderSQL &= ", DisplayNumber, SequenceNumber"
                'End If

                Return OrderSQL
            End Get
        End Property
        Public Property ColumnOrderSQL() As String()
            Get
                Return mColumnOrderSQL
            End Get
            Set(ByVal Value As String())
                mColumnOrderSQL = Value
            End Set
        End Property
        Private mColumnOrderSQL As String()
    End Class
End Namespace
