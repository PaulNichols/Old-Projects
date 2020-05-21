<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)> _
Public Class BusinessPropertyInformation
    Inherits Attribute

    Private ReadOnly Property IsMandatory() As Boolean
        Get
            Return mIsMandatory
        End Get
    End Property
    Private mIsMandatory As Boolean

    Private ReadOnly Property DefaultValue() As Object
        Get
            Return mDefaultValue
        End Get
    End Property
    Private mDefaultValue As Object

    Public Sub New(ByVal isMandatory As Boolean, ByVal defaultValue As Object)
        MyBase.new()
        mIsMandatory = isMandatory
        mDefaultValue = defaultValue
    End Sub

    Public Sub New(ByVal isMandatory As Boolean)
        MyClass.New(isMandatory, Nothing)
    End Sub
End Class
