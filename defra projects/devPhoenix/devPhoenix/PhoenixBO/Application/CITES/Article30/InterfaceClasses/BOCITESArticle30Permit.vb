Namespace Application.CITES.Applications
    Public Class BOCITESArticle30Permit
        Inherits BOCITESImportExportPermit
        Implements IBOCITESArticle30Permit

#Region " Prelim code "
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal citesArticle10Id As Int32)
            MyClass.New()
            MyBase.LoadCITESImportExportPermit(citesArticle10Id)
        End Sub

        Friend Overrides Sub InitialiseCITESImportExportPermit(ByVal citesImportExportPermit As DataObjects.Entity.CITESImportExportPermit, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.InitialiseCITESImportExportPermit(citesImportExportPermit, tran)
            mIsTransactionSpecific = citesImportExportPermit.IsTransactionSpecific
        End Sub
#End Region

#Region " Properties "
      
#End Region

      
    End Class
End Namespace
