<%@ Import namespace="Microsoft.Practices.EnterpriseLibrary.Caching"%>
<%--<%@ Import namespace="System.Runtime.Remoting"%>
<%@ Import namespace="System.Runtime.Remoting.Channels"%>
<%@ Import namespace="System.Runtime.Remoting.Channels.Tcp"%>--%>
<%@ Import namespace="System.Web.Configuration"%>
<%@ Application Language="C#" %>

<script runat="server">

    
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
            //try
            //{
            //    TcpClientChannel chan = new TcpClientChannel();
            //    ChannelServices.RegisterChannel(chan, true);
            //    RemotingConfiguration.Configure(
            //        WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath).FilePath, true);
            //}
            //catch
            //{
            //}
        
        
      
        

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //// Code that runs when an unhandled error occurs
        //Exception objErr = Server.GetLastError().GetBaseException();
        //string url = Server.UrlEncode(Request.Url.ToString());
        //string messsage = Server.HtmlEncode(objErr.Message.ToString());
        //string stackTrace=Server.HtmlEncode(objErr.StackTrace.ToString());
     
        //Server.ClearError();
        //Session.Add("Url",url);
        //Session.Add("Message", messsage);
        //Session.Add("Stack", stackTrace);
        //Response.Redirect("~/error.aspx");

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>

