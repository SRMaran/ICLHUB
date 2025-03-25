using System;
using System.Data;
using System.Data.SqlClient;
public partial class Createpassword : System.Web.UI.Page
{

    DataAccess DA;
    string str_userkey = "";
    string str_Key = "";
    string str_values = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DA = new DataAccess();
        if (Request.QueryString["id"] == null || Request.QueryString["id"] == "") return;
        str_Key = Request.QueryString["id"];
        DateTime now = DateTime.UtcNow;
        string str_datetime = now.ToString();
        string str_Sql = "select * from ICL_Users where ICL_UserId=@ICL_UserId";
        SqlCommand cmd = new SqlCommand(str_Sql);
        cmd.Parameters.AddWithValue("@ICL_UserId", str_Key);
        DataTable dt_email = DA.GetDataTable(cmd);
        if (dt_email != null && dt_email.Rows.Count > 0)
        {
            string str_createdon = Convert.ToString(dt_email.Rows[0]["ICL_Createdon"]);
            this.str_userkey = Convert.ToString(dt_email.Rows[0]["ICL_UserId"]);
            var createdon = DateTime.Parse(str_createdon).AddMinutes(30);
            if (now > createdon)
            {
                Response.Redirect("~/LinkExpired.aspx");
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string newPassword = txt_Repassword.Text;
        string confirmPassword = txt_Newpassword.Text;
        if (newPassword == confirmPassword)
        {
            string str_Sqlupdate = "UPDATE ICL_Users SET ICL_Password =@ICL_Password,ICL_ModifiedOn=@ICL_ModifiedOn where ICL_UserId=@ICL_UserId";
            SqlCommand cmd = new SqlCommand(str_Sqlupdate);
            cmd.Parameters.AddWithValue("@ICL_Password", confirmPassword);
            cmd.Parameters.AddWithValue("@ICL_UserId", this.str_userkey);
            cmd.Parameters.AddWithValue("@ICL_ModifiedOn", DateTime.Now);
            this.DA.ExecuteNonQuery(cmd);
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            lbl_error.Text = "Password confirmation failed. The passwords do not match.";
            div_error.Visible = true;
            success.Visible = false;
        }
    }

}