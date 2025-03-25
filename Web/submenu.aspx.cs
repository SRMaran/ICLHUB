using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_submenu : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    CommonFunction CF;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        PH = new PhTemplate();
        this.CF = new CommonFunction();
        this.Submenu();
        sc.lablename = "Menu";
        sc.headername = "Menu";
    }
    private void Submenu()
    {
        string containerdetails = "select Menukey,Menuname,pagename,Menulist,FORMAT(CreatedOn, 'dd-MM-yyyy') AS Date  from ICL_Menu where parentmenuid is not null order by Menulist ASC";
        SqlCommand cmd = new SqlCommand(containerdetails);
        DataTable client = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(client);
        if (client.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Submenu, "Submenulist.txt", "");
        }
    }

    [WebMethod]
    public static string Submenudelete(string str_ControlValue)
    {

        DataAccess da = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string str_userid = SC.Userid;
        string Role_numberdetails = "delete from ICL_Menu where Menukey ='" + str_ControlValue + "';";
        SqlCommand cmd = new SqlCommand(Role_numberdetails);
        DataTable Role_datas = da.GetDataTable(cmd);
        return "success";
    }
}