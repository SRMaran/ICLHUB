using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;


public partial class Web_ParentMenu : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        PH = new PhTemplate();
        sc.lablename = "Menu";
        sc.headername = "Menu";
        if (!Page.IsPostBack)
        {
            this.menu1();

        } 

    }

    private void menu1()
    {
        string str_view = "Select Menukey,Menudescription,Menuname,FORMAT(CreatedOn, 'dd-MM-yyyy') AS createdon from ICL_Menu where parentmenuid is null ";
        SqlCommand cmd = new SqlCommand(str_view);
        DataTable client = da.GetDataTable(cmd);
        if (client.Rows.Count > 0)
        {
            for (int i = 0; i < client.Rows.Count; i++)
            {

                string description = client.Rows[i]["Menudescription"].ToString();
                string name = client.Rows[i]["Menuname"].ToString();
                string create = client.Rows[i]["createdon"].ToString();
                string menukey = client.Rows[i]["Menukey"].ToString();


                DataTable dt = new DataTable();
                dt.Columns.Add("Menudescription", typeof(string));
                dt.Columns.Add("Menuname", typeof(string));
                dt.Columns.Add("createdon", typeof(string));
                dt.Columns.Add("Menukey", typeof(string));


                DataRow dr = dt.NewRow();
                dr["Menudescription"] = description;
                dr["Menuname"] = name;
                dr["createdon"] = create;
                dr["Menukey"] = menukey;

                dt.Rows.Add(dr); //adding the data row in the data table. 
                DataSet ds = new DataSet();
                ds.Merge(dt);
                this.PH.LoadGridItem(ds, menu, "Parentmenu.txt", "");
            }
        }
    }
    [WebMethod]
    public static string gridtabledelete(string str_ControlValue)
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