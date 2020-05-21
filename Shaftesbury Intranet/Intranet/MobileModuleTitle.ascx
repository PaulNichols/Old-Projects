<%@ Control Language="vb" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Import Namespace="ASPNET.StarterKit.Portal" %>

<%--

    The MobileModuleTitle User Control is responsible for displaying the title of
    each portal module within the mobile portal. It include device-specific
    templates for richer rendering of the title on Pocket PCs.

--%>

<script runat="server">

   Public Text As String

   '*********************************************************************
   '
   ' Page_Load Event Handler
   '
   ' The Page_Load event handler executes after the user control is loaded
   ' and inserted into the control tree.
   '
   ' The Page_Load event handler checks to see if
   '
   '*********************************************************************

   Sub Page_Load(sender As Object, e As EventArgs)

      If Text Is Nothing Then

         ' If the Text property has not been explicitly specified,
         ' walk the parent control chain to find a MobilePortalModuleControl,
         ' and obtain the title from the corresponding module.
         Dim _module As MobilePortalModuleControl = Nothing
         Dim _control As Control = Me

         While _module Is Nothing

            _control = _control.Parent

            If Not _control Is Nothing

                Try
                    _module = CType(_control, MobilePortalModuleControl)
                    Text = _module.ModuleTitle
                Catch
                    ' assignment failed -- _control is wrong type
                    ' don't set Text
                End Try

            End If

         End While

      End If

      ' Databind the User control.
      DataBind()

   End Sub


</script>

<mobile:Panel runat="server">
    <DeviceSpecific>
        <Choice Filter="isJScript">
            <ContentTemplate>
                <font face="Verdana" size="-1" color="#666633"><b>
                        <%# Text %>
                    </b></font>
                <br>
                <hr noshade size="1pt" color="#666633">
            </ContentTemplate>
        </Choice>
    </DeviceSpecific>
    <mobile:Label runat="server" ForeColor="#666633" Font-Size="Large" Font-Bold="True">
        <%# Text %>
    </mobile:Label>
</mobile:Panel>
