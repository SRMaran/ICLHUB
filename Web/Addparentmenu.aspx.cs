using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Web_Addparentmenu : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess DA;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.DA = new DataAccess();

        sc.lablename = "Menu";
        sc.headername = "Parentmenu";
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    PopulateFormDataForUpdate(id);
                    submit.Text = "Update";
                    create.InnerText = "Update Parent Menu";
                   
                }
                else
                {
                    // Handle invalid id here, such as displaying an error message
                }
            }
            else
            {
                submit.Text = "Submit";
                create.InnerText = "Create Parent Menu";
                }
        }
    } // Remove this extra curly brace

    private void PopulateFormDataForUpdate(int id)
    {
        string query = "SELECT Menudescription, Menuname, menulist, menuicon FROM ICL_Menu WHERE Menukey = @ID";

        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("@ID", id);

            DataTable dt = DA.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    Description.Text = row["Menudescription"].ToString();
                    parentmenuname.Text = row["Menuname"].ToString();
                    Menulist.Text = row["menulist"].ToString();
                    Menuicon.Text = row["menuicon"].ToString();

                    // Additional processing for each row if needed
                }
            } 
            else
            {

            }

        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            // It's an update operation
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string query = "UPDATE ICL_Menu SET Menudescription = @Description, Menuname = @Name, menulist = @List, menuicon = @Icon, modifiedon=getdate(),Menutype=0 WHERE Menukey = @ID";

            using (SqlCommand cmdUpdate = new SqlCommand(query))
            {
                cmdUpdate.Parameters.AddWithValue("@Description", Description.Text);
                cmdUpdate.Parameters.AddWithValue("@Name", parentmenuname.Text);
                cmdUpdate.Parameters.AddWithValue("@List", Menulist.Text);
                cmdUpdate.Parameters.AddWithValue("@Icon", Menuicon.Text);
                cmdUpdate.Parameters.AddWithValue("@ID", id);
                DA.ExecuteNonQuery(cmdUpdate);
                Response.Redirect("~/Web/ParentMenu.aspx");
            }
        }
        else
        {
            string query = "INSERT INTO ICL_Menu (Menudescription, Menuname, menulist, menuicon, Menutype) VALUES (@Description, @Name, @List, @Icon, @Menutype)";

            using (SqlCommand cmdInsert = new SqlCommand(query))
            {
                cmdInsert.Parameters.AddWithValue("@Description", Description.Text);
                cmdInsert.Parameters.AddWithValue("@Name", parentmenuname.Text);
                cmdInsert.Parameters.AddWithValue("@List", Menulist.Text);
                cmdInsert.Parameters.AddWithValue("@Icon", Menuicon.Text);
                cmdInsert.Parameters.AddWithValue("@Menutype",0);
                DA.ExecuteNonQuery(cmdInsert);
                Response.Redirect("~/Web/ParentMenu.aspx");
            }
        }
    }


}
