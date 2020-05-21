Imports uk.gov.defra.Phoenix.DO.DataObjects

Namespace Application.Letter.Reports

    <Serializable()> _
    Public Class SemiCompleteReminder
        Inherits Letter

        Private mCertificates(-1) As CertificateData
        Private mCertificateRanges(-1) As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal permitInfoIds() As Int32, ByVal ssoUserId As Long)
            MyBase.New(ssoUserId)
            LoadSemiCompleteReminder(permitInfoIds)
        End Sub

        Private Sub LoadSemiCompleteReminder(ByVal permitInfoIds() As Int32)
            Dim season As String = GetSeason(permitInfoIds)
            SetApplication(GetApplicationId(permitInfoIds))
            mBeginText = "THE CONVENTION ON INTERNATIONAL TRADE IN ENDANGERED SPECIES OF WILD " & _
                         "FAUNA AND FLORA (CITES) - EC REGULATIONS 338/97 AND 1808/2001" & nl & nl & _
                         "SEMI-COMPLETE ARTICLE 10 – SPECIMEN SPECIFIC CERTIFICATES" & nl & nl
            mIntroText = "I refer to your semi-complete Specimen Specific Certificates, " & _
                         "listed below, issued to you for the " & season & " breeding season."
            mValidYearText = season & "."
            mEndText =   "The Department requires accurate compliance with the terms and " & _
                         "conditions governing the use of semi-complete Certificates.  Failure to " & _
                         "adhere to these may lead to the withdrawal of the facility for you " & _
                         "to use semi-complete Certificates." & nl & nl & _
                         "If you have any queries, please do not hesitate to contact me." & nl & nl & _
                         "Yours sincerely"
            FillStandardElements()
            FillCertificateRanges(permitInfoIds)
        End Sub

        Private Function GetSeason(ByVal permitInfoIds() As Int32) As String
            If Not permitInfoIds Is Nothing AndAlso permitInfoIds.Length > 0 Then
                Dim history As EntitySet.PermitHistorySet = BOPermitHistory.GetHistory(permitInfoIds(0))
                Dim info As New BOPermitInfo(permitInfoIds(0))
                If Not history Is Nothing
                    For Each record As Entity.PermitHistory in history
                        If record.PermitStatusId = BOPermitInfo.PermitStatusTypes.Issued Then
                            Return record.ChangeDate.ToString("yyyy")
                        End If
                    Next
                End If
                'belt and braces
                If info.IssueDate Is Nothing Then
                    Return info.CreatedDate.ToString("yyyy")
                End If
                CType(info.IssueDate, Date).ToString("yyyy")
            End If
            Return "<unknown>"
        End Function

        Private Sub FillCertificateRanges(ByVal permitInfoIds() As Int32)
            Dim i As Int32 = 0
            Dim j As Int32 = 0
            ReDim mCertificates(permitInfoIds.Length - 1) 
            ReDim mCertificateRanges(permitInfoIds.Length - 1) 
            For Each infoId As Int32 In permitInfoIds
                mCertificates(i) = GetCertificateData(infoId)
                i += 1
            Next
            Array.Sort(mCertificates, New CertificateDataComparer)
            i = 0
            While i < mCertificates.Length
                Dim lastNum As String = mCertificates(i).PermitNum
                While i < mCertificates.Length AndAlso lastNum = mCertificates(i).PermitNum
                    Dim firstSeq As Int32 = mCertificates(i).Sequence
                    Dim lastSeq As Int32 = firstSeq
                    While i < mCertificates.Length AndAlso lastNum = mCertificates(i).PermitNum AndAlso lastSeq + 1 >= mCertificates(i).Sequence
                        lastSeq = mCertificates(i).Sequence
                        i += 1
                    End While
                    mCertificateRanges(j) = GetRange(lastNum, firstseq, lastseq)
                    j += 1
                End While
            End While
            Redim Preserve mCertificateRanges(j - 1)
        End Sub

        Private Function GetRange(ByVal num As String, ByVal first As Int32, ByVal last As Int32) As String
            Dim result As String = num + "/" + first.ToString().PadLeft(3, "0"c)
            If last > first Then
                result += "-" + num.Split("/".ToCharArray())(1) + "/" + last.ToString().PadLeft(3, "0"c)
            End If
            Return result
        End Function

        Private Function GetCertificateData(ByVal infoId As Int32) As CertificateData
            Dim info As New BOPermitInfo(infoId)
            Dim permit As New BOPermit(info.PermitId)
            Dim num As String = permit.PermitNumber.ToString().PadLeft(2, "0"c)
            Return New CertificateData(permit.ApplicationId.ToString() + "/" + num, info.SequenceNumber)
        End Function

        Public Function GetReportData(ByVal returnDS As DataSet) As ReportDataResults

            Dim newRow As DataRow = returnDS.Tables("BOSemiCompleteReminderLetter").NewRow()
            With newRow

                .Item("Id") = 1
                .Item("FromAddress") = Me.FromAddress
                .Item("ToAddress") = Me.ToAddress
                .Item("IssuedDate") = Me.IssuedDate
                .Item("Salutation") = Me.Salutation
                .Item("BeginText") = Me.BeginText
                .Item("IntroText") = Me.IntroText
                .Item("ValidYearText") = Me.ValidYearText
                .Item("EndText") = Me.EndText
                .Item("DirectLineText") = Me.DirectLineText
                .Item("SignatureText") = Me.SignatureText
                .Item("FaxText") = Me.FaxText
                .Item("EMailText") = Me.EmailText

            End With
            returnDS.Tables("BOSemiCompleteReminderLetter").Rows.Add(newRow)
            returnDS.AcceptChanges()

            For Each range As String In mCertificateRanges
                newRow = returnDS.Tables("BOSemiCompleteReminderLetterDetails").NewRow()
                With newRow
                    .Item("Id") = 1 ' Hard Coded for Crystal Sub Report Purposes
                    .Item("PermitNoText") = range
                End With
                returnDS.Tables("BOSemiCompleteReminderLetterDetails").Rows.Add(newRow)
                returnDS.AcceptChanges()
            Next

            Return New ReportDataResults(returnDS, "")
        End Function

        Public ReadOnly Property BeginText As String
            Get
                Return mBeginText
            End Get
        End Property
        Private mBeginText As String

        Public ReadOnly Property IntroText As String
            Get
                Return mIntroText
            End Get
        End Property
        Private mIntroText As String

        Public ReadOnly Property ValidYearText As String
            Get
                Return mValidYearText
            End Get
        End Property
        Private mValidYearText As String

        Public ReadOnly Property EndText As String
            Get
                Return mEndText
            End Get
        End Property
        Private mEndText As String

        Private Class CertificateData
            Sub New(ByVal permitNum As String, ByVal sequence As Int32)
                mPermitNum = permitNum
                mSequence = sequence
            End Sub

            Private mPermitNum As String
            Private mSequence As Int32

            Public ReadOnly Property PermitNum As String
                Get
                    Return mPermitNum
                End Get
            End Property

            Public ReadOnly Property Sequence As Int32
                Get
                    Return mSequence
                End Get
            End Property
        End Class

        Private Class CertificateDataComparer
            Implements IComparer

            Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xval As CertificateData = CType(x, CertificateData)
                Dim yval As CertificateData = CType(y, CertificateData)
                Dim result As Integer = String.Compare(xval.PermitNum, yval.PermitNum)
                If result = 0 Then
                    result = Compare(xval.Sequence, yval.Sequence)
                End If
                Return result
            End Function

            Private Function Compare(ByVal x As Integer, ByVal y As Integer) As Integer
                If x < y Then Return -1
                If x > y Then Return 1
                Return 0
            End Function
        End Class
    End Class
End Namespace
