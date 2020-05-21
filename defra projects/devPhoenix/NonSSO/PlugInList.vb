Namespace Configuration
    Public Class PlugInList
        Implements ICloneable

        Private _signOn As String = ""

        Public Property SignOn() As String
            Get
                SignOn = _signOn
            End Get
            Set(ByVal Value As String)
                _signOn = Value
            End Set
        End Property

        Private Function LoadAndCreateInstance(ByVal strAssembly As String, ByVal strClass As String) As Object
            ' load the assembly
            Dim ass As Reflection.Assembly
            ass = ass.Load(strAssembly)

            If Not ass Is Nothing Then
                ' load an instance of the class
                Dim obj As Object = ass.CreateInstance(strClass, True)

                If Not obj Is Nothing Then
                    Return obj
                Else
                    Throw New Exception("Cannot load class '" & strClass & "' from assembly '" & strAssembly & "'")
                End If
            Else
                Throw New Exception("Cannot load assembly '" & strAssembly & "'")
            End If
        End Function

        Public ReadOnly Property SignOnObj() As SignOn.ISignOn
            Get
                If Not _signOn Is Nothing AndAlso _signOn.Trim.Length > 0 Then

                    ' split the name
                    Dim parts() As String = _signOn.Split(","c)
                    If parts.Length = 2 Then

                        ' extract and trim the parts
                        Dim strAssembly As String = parts(1).Trim
                        Dim strClass As String = parts(0).Trim

                        ' load and create
                        Dim obj As Object = LoadAndCreateInstance(strAssembly, strClass)

                        ' check and cast
                        If Not obj Is Nothing Then
                            Return CType(obj, SignOn.ISignOn)
                        End If

                        Return Nothing
                    Else
                        Throw New Exception("SignOn must be of the the format 'class, assembly'")
                    End If
                End If
            End Get
        End Property

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Dim pll As New PlugInList
            pll.SignOn = Me.SignOn

            Return pll
        End Function
    End Class
End Namespace