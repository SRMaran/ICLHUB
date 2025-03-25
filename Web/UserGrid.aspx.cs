using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Web_UserGrid : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    string userid= "";
    string str_userRole = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.userid = sc.Userid;
        str_userRole = sc.UserRole;
        PH = new PhTemplate();
        sc.lablename = "User"; sc.headername = "User";
        if (!Page.IsPostBack)
        {
            this.Usergriddetails();
        }
    }
    private void Usergriddetails()
    {

        string str_view = "SELECT a.ICL_username, a.ICL_UserId, a.ICL_Email, a.ICL_MobileNo, a.ICL_Address, c.ICL_username AS ICL_Createdby, a.ICL_Status, a.ICL_postcode, CONVERT(VARCHAR(11), a.ICL_CreatedOn, 106) AS ICL_CreatedOn, STUFF( (SELECT ', ' + d.org_name FROM ICL_userorganization b INNER JOIN ICL_Organization d ON b.ur_orgid = d.org_id WHERE b.ur_userid = a.ICL_UserId FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '' ) AS org_name FROM ICL_Users a LEFT JOIN ICL_Users c ON a.ICL_Createdby = c.ICL_UserId; ";
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable User = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(User);
        if (User.Rows.Count > 0)
        { if (ds.Tables[0].Columns.Contains("ICL_Status"))
            ds.Tables[0].Columns.Add("ActiveText");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int activetype = Convert.ToInt16(dr["ICL_Status"].ToString());
                if (activetype == 0)
                    dr["ActiveText"] = "<span class='custom-badge status-green'>Active</span>";
                else if (activetype == 1)
                    dr["ActiveText"] = "<span class='custom-badge status-red'>InActive</span>"; 
            }
            this.PH.LoadGridItem(ds, Usergrid, "User.txt", "");
            }
        }
}