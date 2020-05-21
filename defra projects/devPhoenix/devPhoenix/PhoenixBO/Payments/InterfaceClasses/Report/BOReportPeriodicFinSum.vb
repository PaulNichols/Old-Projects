Imports System.IO
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Collection
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Service
Imports uk.gov.defra.Phoenix.DO.DataObjects.Views.Entity
Imports uk.gov.defra.Phoenix.BO.Application
Imports uk.gov.defra.Phoenix.BO.Application.ApplicationTypes

Public Class BOReportPeriodicFinSum
    Public Sub New()
        MyBase.New()
    End Sub

    Public Shared Function PeriodicFinSumReportData(ByVal fromDate As Date, ByVal toDate As Date, ByVal schema As String) As ReportDataResults

        'get the DS ready
        Dim ReturnDS As New DataSet
        'create a stream to put the info into
        Dim io As New IO.StringReader(schema)

        'set-up the DS schema
        ReturnDS.ReadXmlSchema(io)
        'tidy ...
        io.Close()
        io = Nothing

        'create a new row using the ds schema
        Dim NewRow As DataRow = ReturnDS.Tables("BOPeriodicFinSum").NewRow()
        Dim PermitRef As String
        Dim cashData As ICollection = SearchCashService.GetCashByDateRange(fromDate, toDate)
        Dim cardData As ICollection = SearchCardService.GetCardsByDateRange(fromDate, toDate)
        Dim chequeData As ICollection = SearchChequeService.GetChequesByDateRange(fromDate, toDate)
        Dim postalData As ICollection = SearchPostalOrderService.GetPostalOrdersByDateRange(fromDate, toDate)
        Dim paidAppData As ICollection = SearchPaidApplicationService.GetPaidApplicationsByDateRange(fromDate, toDate)
        Dim reductionData As ICollection = SearchFeeReductionService.GetFeeReductionsByDateRange(fromDate, toDate)
        Dim refundData As ICollection = SearchRefundService.GetRefundsByDateRange(fromDate, toDate)
        Dim cashInfo As Pair = Accumulate(cashData)
        Dim cardInfo As Pair = Accumulate(cardData)
        Dim chequeInfo As Pair = Accumulate(chequeData)
        Dim postalInfo As Pair = Accumulate(postalData)
        Dim totalInfo As Pair = Accumulate(cashInfo, cardInfo, chequeInfo, postalInfo)
        Dim cashLink As Pair = Accumulate(Filter(cashData, AddressOf CheckApplicationCount))
        Dim cardLink As Pair = Accumulate(Filter(cardData, AddressOf CheckApplicationCount))
        Dim chequeLink As Pair = Accumulate(Filter(chequeData, AddressOf CheckApplicationCount))
        Dim postalLink As Pair = Accumulate(Filter(postalData, AddressOf CheckApplicationCount))
        Dim totalLink As Pair = Accumulate(cashLink, cardLink, chequeLink, postalLink)
        Dim englishPaid As Pair = Accumulate(Filter(paidAppData, AddressOf IsEnglishBird), "ApplicationId")
        Dim welshPaid As Pair = Accumulate(Filter(paidAppData, AddressOf IsWelshBird), "ApplicationId")
        Dim scottishPaid As Pair = Accumulate(Filter(paidAppData, AddressOf IsScottishBird), "ApplicationId")
        Dim birdPaid As Pair = Accumulate(Filter(paidAppData, AddressOf IsBird), "ApplicationId")
        Dim citesPaid As Pair = Accumulate(Filter(paidAppData, AddressOf IsNotBird), "ApplicationId")
        Dim allPaid As Pair = Accumulate(birdPaid, citesPaid)
        Dim chequeRefund As Pair = Accumulate(Filter(refundData, AddressOf IsManualRefund))
        Dim gatewayRefund As Pair = Accumulate(Filter(refundData, AddressOf IsOnlineRefund))
        Dim totalRefund As Pair = Accumulate(chequeRefund, gatewayRefund)
        Dim chequeAdjust As Pair = Accumulate(Filter(refundData, AddressOf IsChequeAdjustment))
        Dim cashAdjust As Pair = Accumulate(Filter(refundData, AddressOf IsCashAdjustment))
        Dim postalAdjust As Pair = Accumulate(Filter(refundData, AddressOf IsPostalAdjustment))
        Dim cardAdjust As Pair = Accumulate(Filter(refundData, AddressOf IsCardAdjustment))
        Dim totalAdjust As Pair = Accumulate(cashAdjust, cardAdjust, chequeAdjust, postalAdjust)
        Dim englishWaiver As Pair = Accumulate(Filter(reductionData, AddressOf IsEnglishBird), "ApplicationId")
        Dim welshWaiver As Pair = Accumulate(Filter(reductionData, AddressOf IsWelshBird), "ApplicationId")
        Dim scottishWaiver As Pair = Accumulate(Filter(reductionData, AddressOf IsScottishBird), "ApplicationId")
        Dim birdWaiver As Pair = Accumulate(Filter(reductionData, AddressOf IsBird), "ApplicationId")
        Dim citesWaiver As Pair = Accumulate(Filter(reductionData, AddressOf IsNotBird), "ApplicationId")
        Dim allWaiver As Pair = Accumulate(birdWaiver, citesWaiver)

        With NewRow
            .Item("TotPayment_Cheque_Number") = chequeInfo.Total
            .Item("TotPayment_Cash_Number") = cashInfo.Total
            .Item("TotPayment_PostalOrder_Number") = postalInfo.Total
            .Item("TotPayment_Gateway_Number") = cardInfo.Total
            .Item("TotPayment_Total_Number") = totalInfo.Total
            .Item("TotPayLinkedApp_Cheque_Number") = chequeLink.Total
            .Item("TotPayLinkedApp_Cash_Number") = cashLink.Total
            .Item("TotPayLinkedApp_PostalOrder_Number") = postalLink.Total
            .Item("TotPayLinkedApp_Gateway_Number") = cardLink.Total
            .Item("TotPayLinkedApp_Total_Number") = totalLink.Total
            .Item("TotPaidApp_England_Number") = englishPaid.Total
            .Item("TotPaidApp_Wales_Number") = welshPaid.Total
            .Item("TotPaidApp_Scotland_Number") = scottishPaid.Total
            .Item("TotPaidApp_BirdRegSubTotal_Number") = birdPaid.Total
            .Item("TotPaidApp_CITES_Number") = citesPaid.Total
            .Item("TotPaidApp_Total_Number") = allPaid.Total
            .Item("TotRefunds_RmdCheque_Number") = chequeRefund.Total
            .Item("TotRefunds_Gateway_Number") = gatewayRefund.Total
            .Item("TotRefunds_Total_Number") = totalRefund.Total
            .Item("TotAdjustment_Cheque_Number") = chequeAdjust.Total
            .Item("TotAdjustment_Cash_Number") = cashAdjust.Total
            .Item("TotAdjustment_PostalOrder_Number") = postalAdjust.Total
            .Item("TotAdjustment_Gateway_Number") = cardAdjust.Total
            .Item("TotAdjustment_Total_Number") = totalAdjust.Total
            .Item("TotFeeWaiver_England_Number") = englishWaiver.Total
            .Item("TotFeeWaiver_Wales_Number") = welshWaiver.Total
            .Item("TotFeeWaiver_Scotland_Number") = scottishWaiver.Total
            .Item("TotFeeWaiver_BirdRegSubTotal_Number") = birdWaiver.Total
            .Item("TotFeeWaiver_CITES_Number") = citesWaiver.Total
            .Item("TotFeeWaiver_Total_Number") = allWaiver.Total

            .Item("TotPayment_Cheque_Amount") = chequeInfo.Cash
            .Item("TotPayment_Cash_Amount") = cashInfo.Cash
            .Item("TotPayment_PostalOrder_Amount") = postalInfo.Cash
            .Item("TotPayment_Gateway_Amount") = cardInfo.Cash
            .Item("TotPayment_Total_Amount") = totalInfo.Cash
            .Item("TotPayLinkedApp_Cheque_Amount") = chequeLink.Cash
            .Item("TotPayLinkedApp_Cash_Amount") = cashLink.Cash
            .Item("TotPayLinkedApp_PostalOrder_Amount") = postalLink.Cash
            .Item("TotPayLinkedApp_Gateway_Amount") = cardLink.Cash
            .Item("TotPayLinkedApp_Total_Amount") = totalLink.Cash
            .Item("TotPaidApp_England_Amount") = englishPaid.Cash
            .Item("TotPaidApp_Wales_Amount") = welshPaid.Cash
            .Item("TotPaidApp_Scotland_Amount") = scottishPaid.Cash
            .Item("TotPaidApp_BirdRegSubTotal_Amount") = birdPaid.Cash
            .Item("TotPaidApp_CITES_Amount") = citesPaid.Cash
            .Item("TotPaidApp_Total_Amount") = allPaid.Cash
            .Item("TotRefunds_RmdCheque_Amount") = chequeRefund.Cash
            .Item("TotRefunds_Gateway_Amount") = gatewayRefund.Cash
            .Item("TotRefunds_Total_Amount") = totalRefund.Cash
            .Item("TotAdjustment_Cheque_Amount") = chequeAdjust.Cash
            .Item("TotAdjustment_Cash_Amount") = cashAdjust.Cash
            .Item("TotAdjustment_PostalOrder_Amount") = postalAdjust.Cash
            .Item("TotAdjustment_Gateway_Amount") = cardAdjust.Cash
            .Item("TotAdjustment_Total_Amount") = totalAdjust.Cash
            .Item("TotFeeWaiver_England_Amount") = englishWaiver.Cash
            .Item("TotFeeWaiver_Wales_Amount") = welshWaiver.Cash
            .Item("TotFeeWaiver_Scotland_Amount") = scottishWaiver.Cash
            .Item("TotFeeWaiver_BirdRegSubTotal_Amount") = birdWaiver.Cash
            .Item("TotFeeWaiver_CITES_Amount") = citesWaiver.Cash
            .Item("TotFeeWaiver_Total_Amount") = allWaiver.Cash
        End With

        'add the row to the dataset
        ReturnDS.Tables("BOPeriodicFinSum").Rows.Add(NewRow)


        'return the datset containing the single row
        Dim reportDataResults As New reportDataResults(ReturnDS, "")
        Return reportDataResults

    End Function

    Delegate Function IncludeItem(ByVal item As Object) As Boolean

    Private Shared Function CheckApplicationCount(ByVal item As Object) As Boolean
        Return CType(GetValue(item, "ApplicationCount"), Int32) > 0
    End Function

    Private Shared Function IsEnglishBird(ByVal item As Object) As Boolean
        Return IsBird(item) AndAlso CType(GetValue(item, "UKCountryName"), String) = "England"
    End Function

    Private Shared Function IsWelshBird(ByVal item As Object) As Boolean
        Return IsBird(item) AndAlso CType(GetValue(item, "UKCountryName"), String) = "Wales"
    End Function

    Private Shared Function IsScottishBird(ByVal item As Object) As Boolean
        Return IsBird(item) AndAlso CType(GetValue(item, "UKCountryName"), String) = "Scotland"
    End Function

    Private Shared Function IsBird(ByVal item As Object) As Boolean
        Dim typeId As Object = GetValue(item, "ApplicationTypeId")
        If Not typeId Is Nothing Then
            Select Case CType(typeId, ApplicationTypes)
                Case BirdAdd, BirdAdult, BirdChick, BirdDup, BirdFate, BirdTrans 
                    Return True 
            End Select
        End If
        Return False
    End Function

    Private Shared Function IsNotBird(ByVal item As Object) As Boolean
        Return Not IsBird(item)
    End Function

    Private Shared Function IsManualRefund(ByVal item As Object) As Boolean
        Return CType(GetValue(item, "RefundType"), Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Manual
    End Function

    Private Shared Function IsOnlineRefund(ByVal item As Object) As Boolean
        Return CType(GetValue(item, "RefundType"), Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Online
    End Function

    Private Shared Function IsAdjustment(ByVal item As Object) As Boolean
        Return CType(GetValue(item, "RefundType"), Payments.BORefund.RefundCode) = Payments.BORefund.RefundCode.Amendment
    End Function

    Private Shared Function IsType(ByVal item As Object, ByVal type As Payments.BOPayment.PaymentMethod) As Boolean
        Return CType(GetValue(item, "PaymentMethodId"), Payments.BOPayment.PaymentMethod) = type
    End Function

    Private Shared Function IsChequeAdjustment(ByVal item As Object) As Boolean
        Return IsAdjustment(item) AndAlso IsType(item, Payments.BOPayment.PaymentMethod.Cheque)
    End Function


    Private Shared Function IsCashAdjustment(ByVal item As Object) As Boolean
        Return IsAdjustment(item) AndAlso IsType(item, Payments.BOPayment.PaymentMethod.Cash)
    End Function

    Private Shared Function IsPostalAdjustment(ByVal item As Object) As Boolean
        Return IsAdjustment(item) AndAlso IsType(item, Payments.BOPayment.PaymentMethod.PostalOrder)
    End Function

    Private Shared Function IsCardAdjustment(ByVal item As Object) As Boolean
        Return IsAdjustment(item) AndAlso IsType(item, Payments.BOPayment.PaymentMethod.Card)
    End Function

    Private Shared Function Filter(ByRef collection As ICollection, ByVal method As IncludeItem) As Object()
        Dim results(collection.Count - 1) As Object
        Dim index As Int32 = 0

        For Each item As Object In collection
            If method.Invoke(item) Then
                results(index) = item
                index += 1
            End If
        Next
        ReDim Preserve results(index - 1)
        Return results
    End Function

    Private Shared Function Accumulate(ByRef collection As ICollection) As Pair
        Return Accumulate(collection, "PaymentReference")
    End Function


    Private Shared Function Accumulate(ByRef collection As ICollection, ByVal keyName As String) As Pair
        Dim results As New Pair
        Dim oldReference As String = ""

        For Each item As Object In collection
            Dim amount As Decimal = CType(GetValue(item, "Amount"), Decimal)
            Dim reference As String = CType(GetValue(item, keyName), String)
            results.Amount += amount
            If reference <> oldReference Then
                oldReference = reference
                results.Count += 1
            End If
        Next
        Return results
    End Function

    Private Shared Function Accumulate(ByVal ParamArray args() As Pair) As Pair
        Dim result As New Pair
        For Each item As Pair In args
            result.Amount += item.Amount
            result.Count += item.Count
        Next
        Return result
    End Function

    Private Shared Function GetValue(ByVal item As Object, ByVal name As String) As Object
        Return item.GetType().GetProperty(name).GetValue(item, Nothing)
    End Function

    Private Class Pair
        Public Amount As New Decimal(0)
        Public Count As Int32 = 0
        Public ReadOnly Property Cash() As String
            Get
                Return Amount.ToString("c")
            End Get
        End Property
        Public ReadOnly Property Total() As String
            Get
                Return Count.ToString()
            End Get
        End Property
    End Class
End Class
