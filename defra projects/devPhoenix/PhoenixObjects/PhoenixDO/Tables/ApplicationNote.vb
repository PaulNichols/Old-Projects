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
    
    'Entity implementation for table 'ApplicationNote'
    '*DO* add your modifications to this file
    <System.Serializable()>  _
    Public Class ApplicationNote
        Inherits Base.ApplicationNoteBase
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal applicationId As Integer, ByVal noteId As Integer)
            MyBase.New(applicationId, noteId)
        End Sub
        
        Public Sub New(ByVal applicationId As Integer, ByVal noteId As Integer, ByVal tran As System.Data.SqlClient.SqlTransaction)
            MyBase.New(applicationId, noteId, tran)
        End Sub
        
        Public Shadows Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
    End Class
End Namespace
