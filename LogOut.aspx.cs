using System;
using System.Data.SqlClient;
using System.Web;
using System.Xml;

public partial class LogOut : System.Web.UI.Page
{
   // private DataAccess DA;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        //DA = new DataAccess();
    }
   
}