Public Class PageLinkButton
    Inherits WebControls.LinkButton

    'Friend Shadows Event Click(ByVal StageNumber As Int32, ByVal identifier As Int32)

    'Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
    '    ' Enabled = False
    '    For Each Control As Control In Parent.Controls
    '        If TypeOf Control Is WebControls.LinkButton Then
    '            CType(Control, WebControls.LinkButton).CssClass = "directnavlinks"
    '        End If
    '    Next Control
    '    CssClass = "directnavlinksselected"

    '    '  RaiseEvent Click(CType(Attributes("StageNumber"), Int32), CType(Attributes("Identifier"), Int32))
    'End Sub


End Class