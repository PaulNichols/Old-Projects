<%@ Application Language="C#" %>

<script RunAt="server">

    
    
    void Application_Start(object sender, EventArgs e)
    {
        //---------------------------------------------------------------
        // Assign the Medium Trust Loader
        //---------------------------------------------------------------        
        EntitySpaces.Interfaces.esProviderFactory.Factory =
            new EntitySpaces.LoaderMT.esDataProviderFactory();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {

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

