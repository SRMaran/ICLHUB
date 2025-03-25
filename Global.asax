<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {

        //Application["dbconnect"] = "Data Source=119.82.69.168;Initial Catalog=ICLHUB;User ID=sa;Password=Data@2023$";
        //Application["importconnect"] = "Data Source=119.82.69.168;Initial Catalog=ICLHUB;User ID=sa;Password=Data@2023$";
        Application["dbconnect"] = "Data Source=206.206.125.70;Initial Catalog=ICLHUB;User ID=sa;Password=123_ICLHub32";
        Application["importconnect"] = "Data Source=206.206.125.70;Initial Catalog=ICLHUB;User ID=sa;Password=123_ICLHub32";
        //Application["webroot"] = "http://3.86.86.20/warehouse/dashborad/Default.aspx";
        Application["email"] = "memesworldnetwork@gmail.com";
        Application["emaildisplayname"] = "Meme’s World";

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //HttpContext.Current.Response.Redirect("~/Default.aspx");
        HttpContext.Current.Response.Redirect("~/LogOut.aspx");

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends.
        if (System.Web.HttpContext.Current != null)
{
    System.Web.HttpContext.Current.Response.Redirect("~/LogOut.aspx", true);
}
else
{
    System.Web.HttpRuntime.UnloadAppDomain(); // Force application restart
}

        //HttpContext.Current.Response.Redirect("~/LogOut.aspx");
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer
        // or SQLServer, the event is not raised.

    }

</script>
