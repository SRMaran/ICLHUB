using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Policy;

public partial class Web_Clientgrid : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    string Userid = "";
    int Role;
    Eadaptor EA;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        EA = new Eadaptor();
        this.Userid = sc.Userid;
        sc.lablename = "Client";
        sc.headername = "Client";
        PH = new PhTemplate();
        if (!Page.IsPostBack)
        {
            this.User();
        }
    }
  
        private void User()
    {
        string str_view = "select org_id,org_userid,org_name,org_ccode,org_address,org_city,org_country,org_postcode,org_billingname,a.icl_username from ICl_Organization left outer join icl_users a on org_userid=a.icl_userid   where org_Createdby='"+ Userid + "' "; 
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable client = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(client);
        if (client.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, Client, "Client.txt", "");
        }
    }
}