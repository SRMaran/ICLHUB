using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.Services;

public partial class Web_Organisationgrid : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    string Userid = "";
    string str_userRole = "";
    int Role;
    Eadaptor EA;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        EA = new Eadaptor();
        this.Userid = sc.Userid;
        str_userRole = sc.UserRole;
        sc.lablename = "Organisation";
        sc.headername = "Organisation";
        PH = new PhTemplate();
        if (!Page.IsPostBack)
        {
            this.User();
        }
    }
  
        private void User()
    {
        string str_view = "select  a.org_name,a.org_id,a.org_ccode,a.org_address,a.org_city,a.org_country,a.org_postcode, a.org_billingname,STRING_AGG(b.ICL_UserName, ', ') AS ICL_UserName from ICl_Organization a left outer join icl_users b on a.org_userid=b.ICL_UserId GROUP BY a.org_name,a.org_id,a.org_ccode,a.org_address,a.org_city,a.org_country,a.org_postcode, a.org_billingname order by org_name asc"; 
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable client = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(client);
        if (client.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Organisation, "Organisation.txt", "");
        }
    }
    [WebMethod]
    public static string orgdelete(string str_delkey)
    {
        DataAccess DA = new DataAccess();
        string response = "0";
        try
        {
            string check = "select * from ICl_Organization where org_id='" + str_delkey + "'";
            SqlCommand cmdd = new SqlCommand(check);
            DataTable dt = DA.GetDataTable(cmdd);

            if (dt.Rows.Count > 0)
            {
                SqlCommand command = new SqlCommand("delete FROM ICl_Organization where org_id='" + str_delkey + "'");
                DA.ExecuteNonQuery(command);
                response = "1";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }
        return response;
    }
}