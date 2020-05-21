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
    
    'Entity implementation for table 'Country'
    '*DO* add your modifications to this file
    <System.Serializable()>  _
    Public Class Country
        Inherits Base.CountryBase
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
        
        Public Sub New(ByVal countryId As Integer)
            MyBase.New(countryId)
        End Sub
        
        Public Shadows Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub

        Function GetRelatedRegions() As EntitySet.UKCountrySet
            If Id > 0 Then
                Return GetRelatedRegions(Me.Id)
            Else
                Return Nothing
            End If
        End Function

        Shared Function GetRelatedRegions(ByVal countryId As Int32) As EntitySet.UKCountrySet
            Return Entity.UKCountry.GetForCountry(countryId)
        End Function

     
    End Class
End Namespace