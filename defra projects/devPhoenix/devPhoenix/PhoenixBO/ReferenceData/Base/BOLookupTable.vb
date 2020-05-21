Imports System.Xml
Imports System.Reflection
Imports System.IO

Namespace ReferenceData

    <Serializable()> _
    Public Class NewLookupTable

        Sub New() 
            mId = -1    'creates "hyphen" entry
        End Sub

        Sub New(ByVal id As Int32)
            mId = id
        End Sub

        Public Shared Function GetHyphen() As NewLookupTable
            Return New NewLookupTable   
        End Function

        Private Shared ReadOnly Property Root() As XmlNode
            Get
                If mLookupXml Is Nothing Then
                    Dim owner As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                    Dim input As Stream = owner.GetManifestResourceStream("uk.gov.defra.Phoenix.BO.BOLookupTable.xml")
                    mLookupXml = New XmlDocument
                    mLookupXml.Load(New StreamReader(input))
                    mRoot = mLookupXml.DocumentElement()
                End If
                Return mRoot
            End Get
        End Property

        Public Shared Function GetAll() As NewLookupTable()
            Dim list As XmlNodeList = Root.SelectNodes("LookupTable")
            Dim results(list.Count - 1) As NewLookupTable
            Dim index As Int32 = 0
            For Each element As XmlElement in list
                results(index) = New NewLookupTable(Int32.Parse(element.GetAttribute("id")))
                index += 1
            Next
            Array.Sort(results, New LookupComparer)
            Return results
        End Function

        Private Readonly Property Element() As XmlElement
            Get
                Return CType(Root.SelectSingleNode("LookupTable[@id='" + mId.ToString + "']"), XmlElement)
            End Get
        End Property

        Private Readonly Property NodeText(ByVal subPath As String) As String
            Get
                If mId = -1 Then
                    Return ""
                End If
                Return Root.SelectSingleNode("LookupTable[@id='" + mId.ToString + "']/" + subPath).InnerText
            End Get
        End Property

        Private Readonly Property NodeExists(ByVal subPath As String) As Boolean
            Get
                Return Not Root.SelectSingleNode("LookupTable[@id='" + mId.ToString + "']/" + subPath) Is Nothing
            End Get
        End Property
        
        Private mId As Int32
        Private Shared mLookupXml As XmlDocument
        Private Shared mRoot As XmlNode

        Public Overridable Property ID As Integer
            Get
                Return mId
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property IsCitesDerivative As Boolean
            Get
                Return mId = LookUpData.LookUpDataList.CITESDerivative
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property TableDescription As String
            Get
                If mId = -1 Then
                    Return "-"
                End If
                Return NodeText("TableDescription")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property ReferenceObject As String
            Get
                Return NodeText("ReferenceObject")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property TableName As String
            Get
                If mId = -1 Then
                    Return ""
                End If
                Return Element.GetAttribute("name")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property Columns As Column() 
            Get
                Dim list As XmlNodeList = Root.SelectNodes("LookupTable[@id='" + mId.ToString + "']/Column")
                Dim i As Int32 = 0
                Dim cols(list.Count - 1) As Column
            
                For Each element As XmlElement in list
                    cols(i) = New Column(element)
                    i += 1
                Next
                Return cols
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property SecurityLevel As Integer
            Get
                If mId = -1 Then
                    Return 0
                End If
                Return Int32.Parse(NodeText("SecurityLevel"))
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property AllowAddNew As Boolean
            Get
                Return NodeExists("AllowAddNew")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property AllowDelete As Boolean
            Get
                Return NodeExists("AllowDelete")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property AllowEdit As Boolean
            Get
                Return NodeExists("AllowEdit")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property ScreenNumber As String
            Get
                Return NodeText("ScreenNumber")
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property TableType As Integer
            Get
               If mId = -1 Then
                   Return 0
               End If
               Return Int32.Parse(NodeText("TableType"))
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property AllowMakeInactive As Boolean
            Get
                Return NodeExists("AllowMakeInactive")
            End Get
            Set
            End Set
        End Property
        
        Private Class LookupComparer
            Implements IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim xlook As NewLookupTable = CType(x, NewLookupTable)
                Dim ylook As NewLookupTable = CType(y, NewLookupTable)
                Return String.Compare(xlook.TableName, yLook.TableName)
            End Function
        End Class
    End Class

    <Serializable()> _
    Public Class Column
      
        Sub New()
        End Sub

        Sub New(ByVal element As XmlElement)
            mElement = element
        End Sub

        Private mElement As XmlElement
    
        Private Readonly Property NodeText(ByVal subPath As String, ByVal defaultVal As String) As String
            Get
                Dim node As XmlNode = mElement.SelectSingleNode(subPath)
                If node Is Nothing
                    Return defaultVal
                End If
                Return node.InnerText
            End Get
        End Property

        Private Readonly Property NodeExists(ByVal subPath As String) As Boolean
            Get
                Return Not mElement.SelectSingleNode(subPath) Is Nothing
            End Get
        End Property

        Public Overridable Property VisibleCol As String
            Get
                Return NodeText("VisibleCol", Nothing)
            End Get
            Set
            End Set
        End Property

        
        Public Overridable Property HeaderText As String
            Get
                Return NodeText("HeaderText", Nothing)
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property Length As Int32
            Get
                Return Int32.Parse(NodeText("Length", Nothing))
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property RegExp As String
            Get
                Return NodeText("RegExp", Nothing)
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property Message As String
            Get
                Return NodeText("Message", Nothing)
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property DefaultVal As String
            Get
                Return NodeText("Default", "")
            End Get
            Set
            End Set
        End Property

        Public Overridable Property LookupList As String
            Get
                Return NodeText("LookupList", "")
            End Get
            Set
            End Set
        End Property

        Public Overridable Property EditColumn As Int32
            Get
                Return Int32.Parse(NodeText("EditColumn", "0"))
            End Get
            Set
            End Set
        End Property
        
        Public Overridable Property RightJustified As Boolean
            Get
                Return NodeExists("RightJustified")
            End Get
            Set
            End Set
        End Property
    End Class
End Namespace
