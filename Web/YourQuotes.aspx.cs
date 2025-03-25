using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class YourQuotes : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        PH = new PhTemplate();
        sc.lablename = "Quotes"; sc.headername = "Quotes";
        if (!Page.IsPostBack)
        {
            this.User();
        }
    }

    private void User()
    {
        string str_view = "select  Id,CompanyName,TransportMode,PackageType,FORMAT(CAST(createdon AS DATETIME), 'dd-MMM-yyyy')  AS createdon,ReadyToCollectDate,TargetDeliveryDate from Quotes_Details";
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable client = da.GetDataTable(cmd);
        if (client.Rows.Count > 0)
        {
            for (int i = 0; i < client.Rows.Count; i++)
            {
                string Id = client.Rows[i]["Id"].ToString();
                string companyname = client.Rows[i]["CompanyName"].ToString();
                string transportmode = client.Rows[i]["TransportMode"].ToString();
                string type = client.Rows[i]["PackageType"].ToString();
                string issuedate = client.Rows[i]["createdon"].ToString();
                string validfrom = client.Rows[i]["ReadyToCollectDate"].ToString();
                string validto = client.Rows[i]["TargetDeliveryDate"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("companyname", typeof(string));
                dt.Columns.Add("transportmode", typeof(string));
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("issuedate", typeof(string));
                dt.Columns.Add("validto", typeof(string));
                dt.Columns.Add("validfrom", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Id"] = Id;
                dr["companyname"] = companyname;
                dr["transportmode"] = transportmode;
                dr["type"] = type;
                dr["issuedate"] = issuedate;
                dr["validto"] = validto;
                dr["validfrom"] = validfrom;

                dt.Rows.Add(dr); //adding the data row in the data table. 
                DataSet ds = new DataSet();
                ds.Merge(dt);
                this.PH.LoadGridItem(ds, Quotes, "Quotesgrid.txt", "");
            }
        }
    }
}