'------------------------------------------------------------------------------
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


Namespace DataObjects.Entity
    
    'Entity implementation for table 'ProgressStatusInspection'
    '*DO* add your modifications to this file
    <System.Serializable()>  _
    Public Class ProgressStatusInspection
        Inherits Base.ProgressStatusInspectionBase
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal progressStatusInspectionId As Integer)
            MyBase.New(progressStatusInspectionId)
        End Sub
        
        Public Sub New(ByVal progressStatusInspectionId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New(progressStatusInspectionId, tran)
        End Sub
        
        Public Shadows Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub

        Public Property CodeDescription() As String
            Get
                Return String.Concat(Code, ", ", Description)
            End Get
            Set(ByVal Value As String)
            End Set
        End Property
    End Class
End Namespace
