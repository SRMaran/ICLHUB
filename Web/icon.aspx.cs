using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_icon : System.Web.UI.Page
{
    SessionCustom sc;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        sc.headername = "Icon";


    }
}