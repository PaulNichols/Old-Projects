VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmNWCheck 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Network Neighborhood Browser"
   ClientHeight    =   5205
   ClientLeft      =   5760
   ClientTop       =   4260
   ClientWidth     =   4170
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5205
   ScaleWidth      =   4170
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton Command1 
      Caption         =   "&Select"
      Height          =   375
      Left            =   840
      TabIndex        =   1
      Top             =   4680
      Width           =   2415
   End
   Begin MSComctlLib.ImageList imlNWImages 
      Left            =   1320
      Top             =   2880
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   13
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":08CA
            Key             =   "directory"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":0C1C
            Key             =   "root"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":0F6E
            Key             =   "group"
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":12C0
            Key             =   "ndscontainer"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":1612
            Key             =   "network"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":1964
            Key             =   "server"
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":1CB6
            Key             =   "tree"
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":2008
            Key             =   "domain"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":235A
            Key             =   "share"
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":26AC
            Key             =   "adminshare"
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":29FE
            Key             =   "printer"
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":2B10
            Key             =   "folder"
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "Form1.frx":2E62
            Key             =   "file"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.TreeView tvwNetwork 
      Height          =   4575
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   4095
      _ExtentX        =   7223
      _ExtentY        =   8070
      _Version        =   393217
      HideSelection   =   0   'False
      Indentation     =   176
      LabelEdit       =   1
      Style           =   7
      FullRowSelect   =   -1  'True
      Appearance      =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
   End
End
Attribute VB_Name = "frmNWCheck"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private NetRoot As NetResource, sSelected As String


Private Sub NodeExpand(Node As MSComctlLib.Node)
' Distinguish between expansion of a network object or a file system folder as seen over the network

Dim FSO As Scripting.FileSystemObject
Dim NWFolder As Scripting.Folder
Dim FilX As Scripting.File, DirX As Scripting.Folder
Dim tNod As Node, isFSFolder As Boolean

' Remove the fake node used to force the treeview to show the "+" icon
tvwNetwork.Nodes.Remove Node.Key + "_FAKE"

' If this node is marked as a share is it a proper networked directory?
' need to make this check since NDS marks some containers (wrongly, in my opinion) as shares when they're not applicable to
' file system directories (i.e. the two containers demarking NDS and Novell FileServers are marked as shares)
If Node.SelectedImage = "share" Then
    On Error Resume Next
    Set FSO = New FileSystemObject
    Set NWFolder = FSO.GetFolder(Node.Key)
    If Err <> 0 Then isFSFolder = False Else isFSFolder = True
    On Error GoTo 0
End If

If Node.SelectedImage = "folder" Or (Node.SelectedImage = "share" And isFSFolder = True) Then
    ' This node is a filesystem folder seen via a network UNC path
    ' Use FileSystemObjects to get files and directories since network objects (generally) can't see these
    '
    Set FSO = New Scripting.FileSystemObject
    Set NWFolder = FSO.GetFolder(Node.Key)  ' The node's key holds the UNC path to the directory
    ' Enumerate the files in this folder
    ' To save any more confusion I'm not querying the system to get an icon for each file and executable
    ' If there's a demand I'll do a modified version, but for the moment I'm just using a generic file icon
    For Each FilX In NWFolder.Files
        tvwNetwork.Nodes.Add Node.Key, tvwChild, Node.Key + "\" + FilX.Name, FilX.Name, "file", "file"
    Next
    ' Enumerate the folders
    For Each DirX In NWFolder.SubFolders
        Set tNod = tvwNetwork.Nodes.Add(Node.Key, tvwChild, Node.Key + "\" + DirX.Name, DirX.Name, "folder", "folder")
        tvwNetwork.Nodes.Add tNod.Key, tvwChild, tNod.Key + "_FAKE", "FAKE", "folder", "folder"
        tNod.Tag = "N"
    Next
    Node.Tag = "Y"
Else
    ' Search up through the tree, noting the node keys so that we can then locate the NetResource object
    ' under NetRoot.
    Dim pS As String, kPath() As String, nX As NetResource, i As Integer, tX As NetResource
    Set tNod = Node ' Start at the node that was expanded
    Do While Not tNod.Parent Is Nothing ' Proceed up the tree using parent references, each time saving the node key to the string pS
        pS = tNod.Key + "|" + pS
        Set tNod = tNod.Parent
    Loop
    ' String pS is now of the form "<Node Key>|<Node Key>|<Node Key>"
    ' Split this into an array using the VB6 Split function
    kPath = Split(pS, "|")
    Set nX = NetRoot
    ' Now loop through this array, this time following down the tree of NetResource objects from NetRoot to the child NetResource object that corresponds to
    ' the node the user clicked
    For i = 0 To UBound(kPath) - 1
        Set nX = nX.Children(kPath(i))
    Next
    ' Now that we know both the node and the corresponding NetResource we can enumerate the children and add the nodes
    For Each tX In nX.Children
        Set tNod = tvwNetwork.Nodes.Add(nX.RemoteName, tvwChild, tX.RemoteName, tX.ShortName, LCase(tX.ResourceTypeName), LCase(tX.ResourceTypeName))
        tNod.Tag = "N"
        ' Add fake nodes to all new nodes except when they're printers (you can always be sure a printer never has children)
        If tX.ResourceType <> Printer Then tvwNetwork.Nodes.Add tX.RemoteName, tvwChild, tX.RemoteName + "_FAKE", "FAKE", "server", "server"
    Next
    tvwNetwork.Refresh  ' Refresh the view
    Node.Tag = "Y"  ' Set the tag to "Y" to denote that this node has been expanded and populated
End If

End Sub


Private Sub Command1_Click()
Dim i As Long

sSelected = ""
For i = 1 To tvwNetwork.Nodes.Count
    If tvwNetwork.Nodes(i).Selected Then
        If tvwNetwork.Nodes(i).SelectedImage = "server" Then sSelected = tvwNetwork.Nodes(i).Text
        Exit For
    End If
Next i
If sSelected <> "" Then
    Unload Me
Else
    MsgBox "Please select a machine from one of the domains"
End If
End Sub

Private Sub Form_Resize()
tvwNetwork.Width = ScaleWidth
tvwNetwork.Height = ScaleHeight - (2 * Command1.Height)
End Sub

Public Function LoadForm(owner As Form) As String
' Centre the form on the screen
'Top = (Screen.Height - Height) / 2
'Left = (Screen.Width - Width) / 2
Screen.MousePointer = vbHourglass
Dim nX As NetResource, nodX As Node
tvwNetwork.ImageList = imlNWImages
Set NetRoot = New NetResource   ' Create a new NetResource object. By default it will be the network root
Set nodX = tvwNetwork.Nodes.Add(, , "_ROOT", "Entire Network", "root", "root")  ' Add a node into the tree for it
nodX.Tag = "Y"  ' Set populated flag to "Y" since we populate this one immediately
' Populate the top level of objects under "Entire Network"
For Each nX In NetRoot.Children
    Set nodX = tvwNetwork.Nodes.Add("_ROOT", tvwChild, nX.RemoteName, nX.ShortName, LCase(nX.ResourceTypeName), LCase(nX.ResourceTypeName))
    nodX.Tag = "N"  ' We haven't populated the nodes underneath this one yet, so set its flag to "N"
    tvwNetwork.Nodes.Add nodX.Key, tvwChild, nodX.Key + "_FAKE", "FAKE", "server", "server" ' Create a fake node under it so that the treeview gives the "+" symbol
    nodX.EnsureVisible
Next
Screen.MousePointer = vbDefault
' You can't get printers at this level, so there's no point in enumerating the NWPrinters collections yet
Show vbModal, owner
LoadForm = sSelected
End Function

Private Sub tvwNetwork_DblClick()
'Command1_Click
End Sub

Private Sub tvwNetwork_Expand(ByVal Node As MSComctlLib.Node)
If Node.Tag = "N" Then
    NodeExpand Node
End If
End Sub



