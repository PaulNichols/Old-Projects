using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Discovery.RequestManagerClient;
using Discovery.Web.UI;

public partial class Test : Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //RequestManagerClientMSMQ requestManagerClientMSMQ = new RequestManagerClientMSMQ();

        ////set the queue
        //requestManagerClientMSMQ.QueueName = ConfigurationManager.AppSettings["OptrakOutQueueName"];

        ////create a request message and set the body (Header, message and Footer)
        //RequestMessage requestMessage = new RequestMessage("test");
        //requestMessage.DestinationSystem = "WH";
        //requestMessage.SourceSystem = "MS";
        //requestMessage.Type = "Optrak";
        //requestMessage.Name = "Test";

        ////send message
        //requestManagerClientMSMQ.Send(requestMessage);
    }
}
