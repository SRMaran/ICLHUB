using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

public partial class Web_AddGroup : System.Web.UI.Page
{
    SessionCustom SC;
    DataAccess da;
    SqlConnection con;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.da = new DataAccess();
        this.con = new SqlConnection();
        SC.headername = "Role";
        SC.lablename = "Role" + "<span style='color:#112560;font-size:18px'> | Role Creation</span>";
        if (!IsPostBack)
        {
            PopulateCheckBoxList();
            if (Request.QueryString["id"] != null)
            {
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    create.InnerText = "Update Role";
                    grp.InnerText = "Update Role";
                    PopulateFormDataForUpdate(id);
                    Btn_Submit.Text = "Update";
                }
            }
            else
            {
                create.InnerText = "Create Role";
                grp.InnerText = "Create Role";
                Btn_Submit.Text = "Submit";
            }
        }
    }
    private void PopulateFormDataForUpdate(int id)
    {
        string query = "select  GM_PK,GM_GroupName,GM_Role,GM_Description,b.Menuname from ICL_GroupMaster inner join ICL_SubMaster a on GM_PK=a.SM_GroupId " +
            "inner join ICL_Menu b on a.SM_MenuId=b.Menukey where GM_PK='" + id + "' ";
        using (SqlCommand cmd = new SqlCommand(query))
        {
            cmd.Parameters.AddWithValue("@ID", id);
            DataTable dt = da.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    txt_group.Text = row["GM_GroupName"].ToString();
                    ddlOptions.SelectedValue = row["GM_Role"].ToString();
                    des.Text = row["GM_Description"].ToString();
                    foreach (ListItem item in CheckBoxList.Items)
                    {
                        if (item.Value == row["Menuname"].ToString())
                        {
                            item.Selected = true;

                        }
                    }
                }
            }
        }
    }
    private void PopulateCheckBoxList()
    {
        string check = "select Menuname from ICL_Menu";
        SqlCommand cmd = new SqlCommand(check);
        DataTable dt_checkbox = da.GetDataTable(cmd);

        if (dt_checkbox.Rows.Count > 0)
        {
            for (int i = 0; i < dt_checkbox.Rows.Count; i++)
            {
                ListItem item = new ListItem();
                item.Text = dt_checkbox.Rows[i]["Menuname"].ToString();
                CheckBoxList.Items.Add(item);
            }
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] != null)
            {
                Updategroup();
            }
            else
            {
                string select = "select * from ICL_GroupMaster where GM_Role='" + ddlOptions.SelectedValue + "'";
                DataTable selected = da.GetDataTable(select);
                if (selected.Rows.Count==0)
                {
                    string insert = "insert into ICL_GroupMaster(GM_GroupName,GM_Role,GM_Description)values(@GM_GroupName,@GM_Role,@GM_Description)";
                    SqlCommand cmd = new SqlCommand(insert);
                    cmd.Parameters.AddWithValue("@GM_GroupName", txt_group.Text);
                    cmd.Parameters.AddWithValue("@GM_Role", ddlOptions.SelectedValue);
                    cmd.Parameters.AddWithValue("@GM_Description", des.Text);
                    da.ExecuteNonQuery(cmd);
                    string groupvalue = "select GM_PK from ICL_GroupMaster where GM_Role='" + ddlOptions.SelectedValue + "'";
                    DataTable dt1 = da.GetDataTable(groupvalue);
                    if(dt1.Rows.Count>0)
                    {
                        string G_value = dt1.Rows[0]["GM_PK"].ToString();
                        foreach (ListItem item in CheckBoxList.Items)
                        {
                            if (item.Selected)
                            {
                                string page = item.Value;
                                string checkboxvalue = "select Menukey from ICL_Menu where Menuname='" + page + "'";
                                DataTable dt = da.GetDataTable(checkboxvalue);
                                string CB_value = dt.Rows[0]["Menukey"].ToString();

                                string check = "select * from ICL_SubMaster where SM_GroupId='" + G_value + "' and SM_MenuId='" + CB_value + "' ";
                                DataTable checkbox = da.GetDataTable(check);
                                if (checkbox.Rows.Count == 0)
                                {
                                    string insert1 = "insert into ICL_SubMaster(SM_GroupId,SM_MenuId)values(@SM_GroupId,@SM_MenuId)";
                                    SqlCommand cmd1 = new SqlCommand(insert1);
                                    cmd1.Parameters.AddWithValue("@SM_GroupId", G_value);
                                    cmd1.Parameters.AddWithValue("@SM_MenuId", CB_value);
                                    da.ExecuteNonQuery(cmd1);
                                }
                            }
                        }
                        lbl_success.Text = "Role Submitted Successfully.";
                        div_success.Visible = true;
                        lbl_success.Visible = true;
                        div_error.Visible = false;

                    }
                    
                }
                else
                {
                    lbl_error.Text = "This Role name is already exists";
                    div_error.Visible = true;
                    lbl_error.Visible = true;
                    div_success.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lbl_error.Text = "Something went wrong.Please try again later";
            div_error.Visible = true;
            lbl_error.Visible = true;
            div_success.Visible = false;
        }
    }
    private void Updategroup()
    {
        string update = "Update ICL_GroupMaster Set GM_GroupName=@GM_GroupName,GM_Role=@GM_Role,GM_Description=@GM_Description,GM_ModifiedOn=getdate() where GM_Role='" + ddlOptions.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(update);
        cmd.Parameters.AddWithValue("@GM_GroupName", txt_group.Text);
        cmd.Parameters.AddWithValue("@GM_Role", ddlOptions.SelectedValue);
        cmd.Parameters.AddWithValue("@GM_Description", des.Text);
        da.ExecuteNonQuery(cmd);
        string groupvalue = "select GM_PK from ICL_GroupMaster where GM_Role='" + ddlOptions.SelectedValue + "'";
        DataTable dt1 = da.GetDataTable(groupvalue);
        string G_value = dt1.Rows[0]["GM_PK"].ToString();
        string deletecheck = "Select * from ICL_SubMaster where SM_GroupId='" + G_value + "'";
        DataTable dt_delete = da.GetDataTable(deletecheck);
        if (dt_delete.Rows.Count > 0)
        {
            string delete = "Delete from ICL_SubMaster where SM_GroupId='" + G_value + "'";
            SqlCommand del = new SqlCommand(delete);
            da.ExecuteNonQuery(del);
        }
        foreach (ListItem item in CheckBoxList.Items)
        {
            if (item.Selected)
            {
                string page = item.Value;
                string checkboxvalue = "select Menukey from ICL_Menu where Menuname='" + page + "'";
                DataTable dt = da.GetDataTable(checkboxvalue);
                string CB_value = dt.Rows[0]["Menukey"].ToString();

                string check = "select * from ICL_SubMaster where SM_GroupId='" + G_value + "' and SM_MenuId='" + CB_value + "' ";
                DataTable checkbox = da.GetDataTable(check);
                if (checkbox.Rows.Count == 0)
                {
                    string insert1 = "insert into ICL_SubMaster(SM_GroupId,SM_MenuId)values(@SM_GroupId,@SM_MenuId)";
                    SqlCommand cmd1 = new SqlCommand(insert1);
                    cmd1.Parameters.AddWithValue("@SM_GroupId", G_value);
                    cmd1.Parameters.AddWithValue("@SM_MenuId", CB_value);
                    da.ExecuteNonQuery(cmd1);
                }
            }
        }
        lbl_success.Text = "Role updated successfully.";
        div_success.Visible = true;
        lbl_success.Visible = true;
        div_error.Visible = false;
    }
}