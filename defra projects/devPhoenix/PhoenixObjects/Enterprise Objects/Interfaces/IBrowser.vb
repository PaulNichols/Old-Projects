Imports System.Windows.Forms

Public Interface IBrowser

    Property Caption() As String
    Property Status() As String

    Sub HandleException(ByVal ex As Exception)
    Function Alert(ByVal message As String) As DialogResult
    Function Alert(ByVal message As String, ByVal icon As MessageBoxIcon) As DialogResult
    Function Alert(ByVal message As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
    Sub AddMenuItem(ByVal subApp As ISubApplication, ByVal path As String, ByVal item As MenuItem)

End Interface
