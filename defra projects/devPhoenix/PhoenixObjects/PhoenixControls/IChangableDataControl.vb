
Public Interface IChangableDataControl
    Property Changed() As Boolean
    Sub ClearChanged()
    Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
End Interface
