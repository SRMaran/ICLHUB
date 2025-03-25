using System;
using System.Data;
using System.Data.SqlClient;

public partial class Web_Changepassword : System.Web.UI.Page
{
    SessionCustom SC;
    DataAccess DA;
    string userkey = "";
    string userRole = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.DA = new DataAccess();
        this.userkey = SC.Userid;
        this.userRole = SC.UserRole.ToString();
        SC.lablename = "Change Password"; SC.headername = "Change Password";
    }



    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string val = txt_Newpassword.Text;
        string old = txt_oldpassword.Text;
        DateTime date = DateTime.Now;
        string getdate = date.ToString();
        string changePass = "";


        DataTable dtValue = this.DA.GetDataTable("SELECT ICL_Password FROM ICL_Users where ICL_UserId='" + userkey + "'");
        if (dtValue.Rows.Count > 0)
        {
            string oldPass = dtValue.Rows[0]["ICL_Password"].ToString();
            if (oldPass == old)
            {
                changePass = "UPDATE ICL_Users SET ICL_Password=@ICL_Password, ICL_ModifiedOn=@ICL_ModifiedOn WHERE ICL_UserId='" + userkey + "'";
                SqlCommand CMD = new SqlCommand(changePass);
                CMD.Parameters.AddWithValue("@ICL_Password", val);
                CMD.Parameters.AddWithValue("@ICL_ModifiedOn", getdate);
                DA.ExecuteNonQuery(CMD);
                div_success.Visible = true;
                lbl_success.Text = "Password change successfully";

            }
            else
            {
                lbl_error.Visible = true;
                lbl_error.Text = "Invalid old password";
                return;
            }
        }

    }
}

