
using System.Web.UI;

public partial class Progress : UserControl
{
    private string progressMessage = "Please wait, thinking really really hard...";


    public string Message
    {
        set { progressMessage = value; }
        get { return progressMessage; }
    }


    public bool ShowImage
    {
        set { imgProgress.Visible = value; }

    }




}