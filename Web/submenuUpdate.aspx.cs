using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_submenuUpdate : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    CommonFunction CF;
    string parentdrop = "";
    string str_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        PH = new PhTemplate();
        this.CF = new CommonFunction();
        sc.lablename = "Menu";
        sc.headername = "Menu";
        if (Request.QueryString["id"] == "" || Request.QueryString["id"] == null)
        {
            if (!Page.IsPostBack)
            {
                ID_submit.Text = "Submit";
                sub.InnerText = "Create Sub Menu";
                this.parentmenu();
                parentdrop = parentmenuname.SelectedValue;
            }
        }
        else
        {
            this.str_id = Request.QueryString["id"].ToString();

            ID_submit.Text = "Update";
            sub.InnerText = "Update Sub Menu";

            if (!Page.IsPostBack)
            {
                this.parentmenu();
                parentdrop = parentmenuname.SelectedValue;
                this.submenugetvalue();
            }
        }
    }

    private void submenugetvalue()
    {
        string str_company = "select Menuname,pagename,Menulist,parentmenuid,Foldername,Menuicon from ICL_Menu where Menukey ='" + this.str_id + "'";
        SqlCommand command = new SqlCommand(str_company);
        DataTable reader = da.GetDataTable(command);


        if (reader.Rows.Count > 0)
        {

            string parentmenuid = reader.Rows[0]["parentmenuid"].ToString();
            string parentmenu = "";
            string str_parnt = "select Menuname from ICL_Menu where Menukey ='" + parentmenuid + "'";
            SqlCommand parnt = new SqlCommand(str_parnt);
            DataTable DT_parnt = da.GetDataTable(parnt);

            menuname.Text = reader.Rows[0]["Menuname"].ToString();
            Pagename.Text = reader.Rows[0]["pagename"].ToString();
            Menulist.Text = reader.Rows[0]["Menulist"].ToString();
            if (DT_parnt.Rows.Count > 0)
            {
                parentmenu = DT_parnt.Rows[0]["Menuname"].ToString();
                parentmenuname.SelectedValue = parentmenu;
            }


            foldername.Text = reader.Rows[0]["Foldername"].ToString();
            Menuicon.Text = reader.Rows[0]["Menuicon"].ToString();

        }

    }

    private void parentmenu()
    {
        string str_company = "select Menuname from ICL_Menu where parentmenuid is null ";
        SqlCommand command = new SqlCommand(str_company);
        DataTable reader = da.GetDataTable(command);
        parentmenuname.DataSource = reader;
        parentmenuname.DataTextField = "Menuname";
        parentmenuname.DataValueField = "Menuname";
        parentmenuname.DataBind();
        parentmenuname.Items.Insert(0, new ListItem(" Select a Parent Menu", "0"));
    }


    protected void submit_Click(object sender, EventArgs e)
    {
        parentdrop = parentmenuname.SelectedValue;

        string menunames = menuname.Text;
        string Pagenames = Pagename.Text;
        string Menulists = Menulist.Text;
        string parentmenunames = parentmenuname.Text;
        string foldernames = foldername.Text;
        string Menuicons = Menuicon.Text;

        string menunameiD = "";

        string str_company = "select Menukey from ICL_Menu where Menuname ='" + parentdrop + "'";
        SqlCommand command = new SqlCommand(str_company);
        DataTable reader = da.GetDataTable(command);
        if (reader.Rows.Count > 0)
        {
            for (int i = 0; i < reader.Rows.Count; i++)
            {
                menunameiD = reader.Rows[i]["Menukey"].ToString();
            }
        }
        string str_parnt = "select * from ICL_Menu where Menukey ='" + this.str_id + "' ";
        SqlCommand parnt = new SqlCommand(str_parnt);
        DataTable DT_parnt = da.GetDataTable(parnt);

        if (DT_parnt.Rows.Count == 0)
        {
            string str_menuInsertQuery = "INSERT INTO ICL_Menu (Menuname,pagename,Menulist,parentmenuid,Foldername,Menuicon,Menutype) " +
            "VALUES (@menu,@page,@list,@parid,@folder,@icon,@Menutype)";
            SqlCommand cmd = new SqlCommand(str_menuInsertQuery);
            cmd.Parameters.AddWithValue("@menu", menunames);
            cmd.Parameters.AddWithValue("@page", Pagenames);
            cmd.Parameters.AddWithValue("@list", Menulists);
            cmd.Parameters.AddWithValue("@parid", menunameiD);
            cmd.Parameters.AddWithValue("@folder", foldernames);
            cmd.Parameters.AddWithValue("@icon", Menuicons);
            cmd.Parameters.AddWithValue("@Menutype", 1);
            da.ExecuteNonQuery(cmd);
            Response.Redirect("~/Web/submenu.aspx");
        }
        else
        {
            string str_menuupdatequery = "UPDATE ICL_Menu SET Menuname=@menu,pagename=@page,Menulist=@list," +
                                  "parentmenuid=@parid,Foldername=@folder,Menuicon=@icon,modifiedon=getdate(),Menutype=1 " +
                                  "WHERE Menukey ='" + this.str_id + "'";
            SqlCommand cmd = new SqlCommand(str_menuupdatequery);
            cmd.Parameters.AddWithValue("@menu", menunames);
            cmd.Parameters.AddWithValue("@page", Pagenames);
            cmd.Parameters.AddWithValue("@list", Menulists);
            cmd.Parameters.AddWithValue("@parid", menunameiD);
            cmd.Parameters.AddWithValue("@folder", foldernames);
            cmd.Parameters.AddWithValue("@icon", Menuicons);
            da.ExecuteNonQuery(cmd);
            Response.Redirect("~/Web/submenu.aspx");
        }
    }
}