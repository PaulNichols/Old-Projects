'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  iBOReportOfflinePayment.vb
''  Implementation of the Interface iBOReportOfflinePayment
''  Generated by Enterprise Architect
''  Created on:      30-Sep-2004 11:32:59
''  Original author: Paul Wade
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



Option Explicit On
Option Strict On

Public Interface iBOReportPayment

    Property Details() As String

    Property RemittanceAdvice() As Boolean

    Property NumberOfApplications() As Integer

End Interface ' iBOReportOfflinePayment
