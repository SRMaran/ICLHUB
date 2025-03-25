using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_AdminGrid : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    string usernamee = "";
    int Role;
    string userid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.usernamee = sc.username;
        this.userid = sc.Userid;
        PH = new PhTemplate();
        sc.lablename = "Admin";
        sc.headername = "Admin";
        if (!Page.IsPostBack)
        {
            this.Admin();
        }
    }
    private void Admin()
    {
        string str_view = "select ICL_UserId,ICl_UserName,ICL_email,FORMAT(ICL_createdon, 'dd-MM-yyyy') as ICL_createdon,ICl_Status from ICL_Users  where ICL_Role=0";
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable Group = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(Group);
        if (Group.Rows.Count > 0)
        {
            if (ds.Tables[0].Columns.Contains("ICL_Status"))
                ds.Tables[0].Columns.Add("ActiveText");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int activetype = Convert.ToInt16(dr["ICL_Status"].ToString());
                if (activetype == 0)
                    dr["ActiveText"] = "<span class='custom-badge status-green'>Active</span>";
                else if (activetype == 1)
                    dr["ActiveText"] = "<span class='custom-badge status-red'>InActive</span>"; ;
            }
            this.PH.LoadGridItem(ds, Admingrid, "Admin.txt", "");
        }
    }
    [WebMethod]
    public static string Groupdelete(string str_PjtCategorykey)
    {
        DataAccess DA = new DataAccess();
        string response = "0";
        try
        {
            string check = "select * from ICL_Users where ICL_UserId='" + str_PjtCategorykey + "'";
            SqlCommand cmdd = new SqlCommand(check);
            DataTable dt = DA.GetDataTable(cmdd);

            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("delete FROM ICL_Users where ICL_UserId='" + str_PjtCategorykey + "'");
                DA.ExecuteNonQuery(cmd);
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