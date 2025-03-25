using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class login : System.Web.UI.Page
{
    DataAccess DA;
    SessionCustom SC;
    string str_email = "";
    string str_status = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.DA = new DataAccess();
        Deleterop.Visible = false;
        if (!IsPostBack)
        {
            HttpCookie rememberMeCookie = Request.Cookies["RememberMe"];
            if (rememberMeCookie != null)
            {
                string username = rememberMeCookie.Values["Username"];
                txt_Username.Attributes.Add("required", "required");
                txt_Password.Attributes.Add("required", "required");
            }
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        string str_login = "select * from ICL_Users where ICL_Email=@Username and ICL_Password =@Password  COLLATE SQL_Latin1_General_CP1_CS_AS";
        SqlCommand sc = new SqlCommand(str_login);
        sc.Parameters.AddWithValue("@Username", txt_Username.Text);
        sc.Parameters.AddWithValue("@Password", txt_Password.Text);
        DataTable dt_login = this.DA.GetDataTable(sc);
        if (dt_login != null && dt_login.Rows.Count > 0)
        {
            SC.Userid = dt_login.Rows[0]["ICL_UserId"].ToString();
            string str_Admin = dt_login.Rows[0]["ICL_Email"].ToString();
            string ICL_Role = dt_login.Rows[0]["ICL_Role"].ToString();
            string ICL_Company = dt_login.Rows[0]["ICL_Company"].ToString();
            string ICL_Department = dt_login.Rows[0]["ICL_Department"].ToString();
            string userid = dt_login.Rows[0]["ICL_CompanyId"].ToString();
            string ICL_GroupName = dt_login.Rows[0]["ICL_GroupName"].ToString();
            string ICL_Clientkey = dt_login.Rows[0]["ICL_Clientkey"].ToString();
            string ICL_Status = dt_login.Rows[0]["ICL_Status"].ToString();
            string str_Image = dt_login.Rows[0]["ICL_Image"].ToString();
            string ICL_UserName = dt_login.Rows[0]["ICL_UserName"].ToString();
            if (ICL_Status == "1")
            {
                lbl_error.Text = "Incorrect Username/Password";
                lbl_error.ForeColor = System.Drawing.Color.Red;
                lbl_error.Visible = true;
            }
            else
            {
                if (ICL_Role == "1")
                {
                    string str_org = "select top 1 Org_Name,Org_ID,Org_CCode from  organisationsession_table where UserID='" + SC.Userid + "' union all select top 1 org_name,org_id,org_ccode from  ICl_Organization  where org_userid='" + SC.Userid + "'";
                    SqlCommand org = new SqlCommand(str_org);

                    DataTable dt_org = this.DA.GetDataTable(org);
                    if (dt_org.Rows.Count > 0)
                    {
                        SC.Orgname = dt_org.Rows[0]["org_id"].ToString();
                        SC.Orgnamecode = dt_org.Rows[0]["org_ccode"].ToString();
                        SC.Org_names = dt_org.Rows[0]["org_name"].ToString();
                    }
                    else
                    {
                        Deleterop.Visible = true;
                        return;
                        
                    }
                }
                if (ICL_Role == "2")
                {
                    string str_org = "select top 1 Org_Name,Org_ID,Org_CCode from  organisationsession_table where UserID='" + SC.Userid + "' union all select  org_name,org_id,org_ccode from  ICl_Organization  where org_ccode='0'";
                    SqlCommand org = new SqlCommand(str_org);

                    DataTable dt_org = this.DA.GetDataTable(org);
                    if (dt_org.Rows.Count > 0)
                    {
                        SC.Orgname = dt_org.Rows[0]["org_id"].ToString();
                        SC.Orgnamecode = dt_org.Rows[0]["org_ccode"].ToString();
                        SC.Org_names = dt_org.Rows[0]["org_name"].ToString();
                    }
                }
                SC.UserRole = ICL_Role;
                SC.username = str_Admin;
                SC.Name = ICL_UserName;
                SC.UserImage = str_Image;
                string insertLoginQuery = "INSERT INTO ICL_UserLogs (Log_UserId, Log_UserEmail, Log_UserRole, Log_UserLogIn) VALUES (@UserId, @UserName, @UserRole, @UserLogIn)";
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (SqlCommand cmdInsert = new SqlCommand(insertLoginQuery))
                {
                    cmdInsert.Parameters.AddWithValue("@UserId", SC.Userid);
                    cmdInsert.Parameters.AddWithValue("@UserName", str_Admin);
                    cmdInsert.Parameters.AddWithValue("@UserRole", ICL_Role);
                    cmdInsert.Parameters.AddWithValue("@UserLogIn", currentDate);
                    DA.ExecuteNonQuery(cmdInsert);
                }
                string selectNewQuery = "SELECT top 1 Log_ID FROM ICL_UserLogs WHERE Log_UserLogIn = '" + currentDate + "' and Log_UserId='" + SC.Userid + "'";
                SqlCommand cmdNewLogId = new SqlCommand(selectNewQuery);
                DataTable dtNewLogId = DA.GetDataTable(cmdNewLogId);
                Response.Redirect("Web/Dashboard.aspx");


            }
        }
        else
        {
            div_error.Visible = true;
            lbl_error.Text = "Incorrect Username/Password";
            lbl_error.Visible = true;
        }
    }
}