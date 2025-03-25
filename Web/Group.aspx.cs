using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Web_Group : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    string usernamee = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.usernamee = sc.username;
        PH = new PhTemplate();
        sc.lablename = "Role";
        sc.headername = "Role";
        if (!Page.IsPostBack)
        {
            this.Group();
        }
    }

    private void Group()
    {
        string str_view = "select GM_PK,GM_GroupName,GM_Role,GM_Description from ICL_GroupMaster";
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable Group = da.GetDataTable(cmd);
        if (Group.Rows.Count > 0)
        {
            for (int i = 0; i < Group.Rows.Count; i++)
            {

                string GM_PK = Group.Rows[i]["GM_PK"].ToString();
                string GM_GroupName = Group.Rows[i]["GM_GroupName"].ToString();
                string GM_Role = Group.Rows[i]["GM_Role"].ToString();
                string GM_Description = Group.Rows[i]["GM_Description"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("GM_GroupName", typeof(string));
                dt.Columns.Add("GM_Role", typeof(string));
                dt.Columns.Add("GM_Description", typeof(string));
                dt.Columns.Add("GM_PK", typeof(string));

                DataRow dr = dt.NewRow();
                dr["GM_GroupName"] = GM_GroupName;
                dr["GM_Role"] = GM_Role;
                dr["GM_Description"] = GM_Description;
                dr["GM_PK"] = GM_PK;

                dt.Rows.Add(dr); //adding the data row in the data table. 
                DataSet ds = new DataSet();
                ds.Merge(dt);

                if (ds.Tables[0].Columns.Contains("GM_Role"))
                    ds.Tables[0].Columns.Add("Role");
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    int activetype = Convert.ToInt16(dr["GM_Role"].ToString());
                    if (activetype == 0)
                        dr1["Role"] = "Administrator ";
                    else if (activetype == 1)
                        dr1["Role"] = "Account Manager ";
                    else if (activetype == 2)
                        dr1["Role"] = "ICL Administrator ";

                }
                this.PH.LoadGridItem(ds, Groupid, "Group.txt", "");
            }
        }
    }
    [WebMethod]
    public static string Groupdelete(string str_PjtCategorykey)
    {
        DataAccess DA = new DataAccess();
        string response = "0";
        try
        {
            string check = "select * from ICL_GroupMaster where GM_Pk='" + str_PjtCategorykey + "'";
            SqlCommand cmdd = new SqlCommand(check);
            DataTable dt = DA.GetDataTable(cmdd);

            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("delete FROM ICL_GroupMaster where GM_Pk='" + str_PjtCategorykey + "'");
                SqlCommand cmd1 = new SqlCommand("delete FROM ICL_SubMaster where SM_GroupId='" + str_PjtCategorykey + "'");
                SqlCommand cmd2 = new SqlCommand("Update ICL_Users Set ICL_Groupname=0 where ICL_Groupname='" + str_PjtCategorykey + "'");
                DA.ExecuteNonQuery(cmd);
                DA.ExecuteNonQuery(cmd1);
                DA.ExecuteNonQuery(cmd2);
                response = "1";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
        return response;
    }
}