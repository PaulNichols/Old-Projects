'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2032
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On


Namespace DataObjects.Entity
    
    'Entity implementation for table 'Article10'
    '*DO* add your modifications to this file
    <System.Serializable()>  _
    Public Class Article10
        Inherits Base.Article10Base
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal article10Id As Integer)
            MyBase.New(article10Id)
        End Sub
        
        Public Sub New(ByVal article10Id As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New(article10Id, tran)
        End Sub
        
        Public Shadows Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub

        Shared Function GetArticle10sForApplication(ByVal permitNumber As String) As EnterpriseObjects.EntitySet
            Return Article10.GetArticle10sForApplication(permitNumber, Nothing)
        End Function

        Shared Function GetArticle10sForApplication(ByVal permitNumber As String, ByVal tran As SqlClient.SqlTransaction) As EnterpriseObjects.EntitySet
            Dim PermitHelper As New Helpers.ApplicationPermitHelper(permitNumber)
            If PermitHelper.ApplicationId > 0 Then
                Return CType(Sprocs.dbo_usp_GetArticle10sByApplication(PermitHelper.ApplicationId, PermitHelper.PermitId, tran, GetType(EnterpriseObjects.EntitySet)), EnterpriseObjects.EntitySet)
            End If
        End Function
    End Class
End Namespace
