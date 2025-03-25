using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;


public partial class ForgetPassword : System.Web.UI.Page
{
    DataAccess DA;
    CommonFunction CF;
    protected void Page_Load(object sender, EventArgs e)
	{
        this.DA = new DataAccess();
        this.CF = new CommonFunction();

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object ResetPassword(string email)
    {
        DataAccess DA = new DataAccess();
        CommonFunction CF = new CommonFunction();
        string str_Sql = "SELECT ICL_Password, ICL_Email, ICL_UserId, ICL_FirstName FROM ICL_Users WHERE ICL_Email = @Emailid";
        SqlCommand cmd = new SqlCommand(str_Sql);
        cmd.Parameters.AddWithValue("@Emailid", email);

        DataTable dt_email = DA.GetDataTable(cmd);

        if (dt_email != null && dt_email.Rows.Count > 0)
        {
            string str_name = Convert.ToString(dt_email.Rows[0]["ICL_Email"]);
            string str_userkey = Convert.ToString(dt_email.Rows[0]["ICL_UserId"]);
            string str_firstname = Convert.ToString(dt_email.Rows[0]["ICL_FirstName"]);
            //if (string.IsNullOrWhiteSpace(str_firstname))
            //{
            //    str_firstname = "ICL User";
            //}

            SqlCommand SC_Log = CF.CreateLogKey(str_userkey);
            DA.ExecuteNonQuery(SC_Log);

            string str_Query = "SELECT TOP 1 * FROM WD_Logdetail WHERE WD_createdby = @createdby ORDER BY WD_Createdon DESC";
            SqlCommand sc = new SqlCommand(str_Query);
            sc.Parameters.AddWithValue("@createdby", str_userkey);
            DataTable dt_log = DA.GetDataTable(sc);
            string str_logkey = Convert.ToString(dt_log.Rows[0]["WD_logkey"]);
            string str_link = "http://206.206.125.70/ICLHUB/ResetPassword.aspx?id=" + str_logkey;

            //string email_fun = new ForgotPassword().CF.PasswordRecovery(str_name, "password", "ICL Hub Password Reset", str_link, str_firstname);
            string email_fun = CF.PasswordRecovery(str_name, "password", "ICL Hub Password Reset", str_link, str_firstname);
            return new { status = "success", message = "Password Reset Email Sent."};
        }
        else
        {
            return new { status = "error", message = "Email address does not exist" };
        }
    }
    //protected void btnReset_Click(object sender, EventArgs e)
    //{
    //    DataAccess DA = new DataAccess();
    //    string str_Sql = " select ICL_Password ,ICL_Email ,ICL_UserId ,ICL_UserName from ICL_Users where ICL_Email =@Emailid";
    //    SqlCommand cmd = new SqlCommand(str_Sql);
    //    cmd.Parameters.AddWithValue("@Emailid", txt_email.Text);

    //    DataTable dt_email = DA.GetDataTable(cmd);

    //    if (dt_email != null && dt_email.Rows.Count > 0)
    //    {
    //        string str_password = Convert.ToString(dt_email.Rows[0]["ICL_Password"]);
    //        string str_name = Convert.ToString(dt_email.Rows[0]["ICL_Email"]);
    //        string str_userkey = Convert.ToString(dt_email.Rows[0]["ICL_UserId"]);
    //        string str_firstname = Convert.ToString(dt_email.Rows[0]["ICL_UserName"]);
    //        SqlCommand SC_Log = CF.CreateLogKey(str_userkey);
    //        DA.ExecuteNonQuery(SC_Log);

    //        string str_Query = "SELECT top 1 * FROM WD_Logdetail where WD_createdby=@createdby ORDER BY WD_Createdon DESC";
    //        SqlCommand sc = new SqlCommand(str_Query);
    //        sc.Parameters.AddWithValue("@createdby", str_userkey);
    //        DataTable dt_log = DA.GetDataTable(sc);
    //        string str_logkey = Convert.ToString(dt_log.Rows[0]["WD_logkey"]);
    //        string str_link = "http://206.206.125.70/ICLHUB/ResetPassword.aspx?id=" + str_logkey + "";
    //        //string str_link = "http://localhost:52860/ResetPassword.aspx?id=" + str_logkey + "";
    //        string email_fun = this.CF.PasswordRecovery(str_name, "password", "ICL Hub Password Reset", str_link, str_firstname);
    //        txt_email.Text = "";
    //        //Label_success.Text = "Recovery instructions sent to your email please check your email";
    //        //success.Visible = true;
    //        //div_error.Visible = false;

    //    }
    //    else
    //    {
    //        //lbl_error.Text = "Please Provide valid email address";
    //        //div_error.Visible = true;
    //        //success.Visible = false;




    //    }

    //}


}