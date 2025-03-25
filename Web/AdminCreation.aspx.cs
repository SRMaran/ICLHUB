using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.ComponentModel.Design;
using System.IdentityModel.Protocols.WSTrust;

public partial class Web_AdminCreation : System.Web.UI.Page
{
    SessionCustom SC;
    DataAccess da;
    PhTemplate PH;
    string str_email = "";
    int Role;
    string str_id = "";
    string str_userkey = "";
    private string str_newid;
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.str_userkey = SC.Userid;
        this.str_email = SC.username;
        this.da = new DataAccess();
        PH = new PhTemplate();
        SC.lablename = "Admin";
        SC.headername = "Admin";

        if (Request.QueryString["id"] != null)
        {
            txt_email.ReadOnly = true;
            rdstatus.Visible = true;

            userid = Request.QueryString["id"].ToString();
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                if (!IsPostBack)
                {
                    password.Visible = false;
                    Li1.InnerHtml = "Update Admin";
                    create.InnerText = "Update Admin";
                    PopulateFormDataForUpdate(id);
                    Update.Text = "Update";
                }
            }
        }
        else
        {
            if (!IsPostBack)
            {
                create.InnerText = "Create Admin";
                Update.Text = "Submit";
                Li1.InnerHtml = "Create Admin";
            }
        }
    }

    private void PopulateFormDataForUpdate(int id)
    {
        string Update = "select ICL_UserId,ICL_UserName,ICL_Email,ICL_MobileNo from ICL_Users  where ICL_Role=0 and ICL_UserId='" + id + "'";
        DataTable update_dt = da.GetDataTable(Update);
        if (update_dt.Rows.Count > 0)
        {
           txt_username.Text = update_dt.Rows[0]["ICL_UserName"].ToString();
            txt_email.Text = update_dt.Rows[0]["ICL_Email"].ToString();
            txt_phone.Text = update_dt.Rows[0]["ICL_MobileNo"].ToString();
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            string usercheck = "select ICL_UserId from ICL_Users where ICL_Email='" + txt_email.Text + "'";
            DataTable dt_user = da.GetDataTable(usercheck);
            string str_path = "";
            if (Request.QueryString["id"] != null)
            {
                string updateQuery1 = "UPDATE ICL_Users SET  ICL_MobileNo=@ICL_MobileNo,ICL_Email=@ICL_Email,ICL_Image=@ICL_Image,ICL_Createdby=@ICL_Createdby,ICL_UserName=@ICL_UserName,ICL_Role=@ICL_Role,ICL_Status=@ICL_Status WHERE ICL_UserId=@ICL_UserId;";

                using (SqlCommand updateCmd1 = new SqlCommand(updateQuery1))
                {
                    // Set parameters for the UPDATE
                    updateCmd1.Parameters.AddWithValue("@ICL_userName", txt_username.Text);
                    updateCmd1.Parameters.AddWithValue("@ICL_MobileNo", txt_phone.Text);
                    updateCmd1.Parameters.AddWithValue("@ICL_Email", txt_email.Text);
                    updateCmd1.Parameters.AddWithValue("@ICL_Createdby", this.str_userkey);
                    updateCmd1.Parameters.AddWithValue("@ICL_Status",rd_status.SelectedValue);
                    string filename1 = Path.GetFileName(updateQuery1);
                    if (filename1 != "")
                    {
                        string extension = Path.GetExtension(Fi_Updatepicture.FileName);
                        this.str_newid = this.str_userkey + extension;
                        str_path = Server.MapPath("~/Images/") + this.str_newid;
                        Fi_Updatepicture.SaveAs(str_path);
                    }
                    if (this.str_newid != "")
                    {
                        updateCmd1.Parameters.AddWithValue("@ICL_Image", this.str_newid);
                    }
                    else
                    {
                        updateCmd1.Parameters.AddWithValue("@ICL_Image", DBNull.Value);
                    }

                    updateCmd1.Parameters.AddWithValue("@ICL_Role", 0);
                    updateCmd1.Parameters.AddWithValue("@ICL_UserId", userid);
                    da.ExecuteNonQuery(updateCmd1);
                    lbl_success.Text = "Updated successfully";
                    div_success.Visible = true;
                    lbl_success.Visible = true;
                    div_error.Visible = false;
                    Response.Redirect("~/Web/Admingrid.aspx");

                }
            }
            else
            {
                if (dt_user.Rows.Count == 0)
                {
                    string insert1 = "insert into ICL_Users(ICL_Password,ICL_MobileNo,ICL_Email,ICL_Createdby,ICL_Image,ICL_UserName,ICL_Role,ICL_Status)" +
                                "values(@ICL_Password,@ICL_MobileNo,@ICL_Email,@ICL_Createdby,@ICL_Image,@ICL_UserName,@ICL_Role,@ICL_Status)";
                    SqlCommand cmd = new SqlCommand(insert1);
                    string filename1 = Path.GetFileName(insert1);
                    if (filename1 != "")
                    {
                        string extension = Path.GetExtension(Fi_Updatepicture.FileName);
                        this.str_newid = this.str_userkey + extension;
                        str_path = Server.MapPath("~/Images/") + this.str_newid;
                        Fi_Updatepicture.SaveAs(str_path);
                    }
                    cmd.Parameters.AddWithValue("@ICL_Username", txt_username.Text);
                    cmd.Parameters.AddWithValue("@ICL_Password", txt_password.Text);
                    cmd.Parameters.AddWithValue("@ICL_MobileNo", txt_phone.Text);
                    cmd.Parameters.AddWithValue("@ICL_Email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@ICL_Createdby", this.str_userkey);
                    if (this.str_newid != "")
                    {
                        cmd.Parameters.AddWithValue("@ICL_Image", this.str_newid);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ICL_Image", this.str_newid);
                    }
                    cmd.Parameters.AddWithValue("@ICL_Status",0 );
                    cmd.Parameters.AddWithValue("@ICL_Role", 0);

                    da.ExecuteNonQuery(cmd);
                    lbl_success.Text = "Submitted successfully";
                    div_success.Visible = true;
                    lbl_success.Visible = true;
                    div_error.Visible = false;
                    Response.Redirect("~/Web/Admingrid.aspx");
                }
                else
                {
                    lbl_error.Text = "This user is already exists";
                    div_error.Visible = true;
                    lbl_error.Visible = true;
                    div_success.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            lbl_error.Text = "Something went to wrong. Please contact support team";
            div_error.Visible = true;
            div_success.Visible = false;
        }
    }

}
