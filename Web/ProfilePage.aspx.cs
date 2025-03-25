using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;
using System.Linq;


public partial class Employee_ProfilePage : System.Web.UI.Page
{
    string str_userkey = "";
    string str_UserProfileImage = "";
    string userimage = "";
    DataAccess DA;
    SessionCustom SC;
    CommonFunction CF;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.DA = new DataAccess();
        str_userkey = SC.Userid;
        SC.lablename = "Profile";
        SC.headername = "Profile";
        if (!IsPostBack)
        {
            BindAvatars();
            this.assignuserdetails();

            if (Session["ProfileUpdateSuccess"] != null)
            {
                lbl_success.Text = Session["ProfileUpdateSuccess"].ToString();
                lbl_success.ForeColor = System.Drawing.Color.Green;
                div_success.Visible = true;

                // Clear the session after displaying message
                Session["ProfileUpdateSuccess"] = null;
            }

        }
    }

    private void assignuserdetails()
    {
        string str_viewquery = "SELECT ICL_UserId, CONCAT(ICL_FirstName, ' ', ICL_LastName) AS name, ICL_FirstName, ICL_LastName, ICL_MobileNo, ICL_Email, ICL_Image, ICL_UserName, ICL_Role, ICL_Address, ICL_Postcode FROM ICL_Users WHERE ICL_UserId = @ICL_UserId";

        SqlCommand cmd = new SqlCommand(str_viewquery);
        cmd.Parameters.AddWithValue("@ICL_UserId", str_userkey);

        DataTable dt = DA.GetDataTable(cmd);
        if (dt.Rows.Count > 0)
        {
            string Name = dt.Rows[0]["name"].ToString();
            string firstname = dt.Rows[0]["ICL_FirstName"].ToString();
            string lastname = dt.Rows[0]["ICL_LastName"].ToString();
            string Email = dt.Rows[0]["ICL_Email"].ToString();
            string Phone = dt.Rows[0]["ICL_MobileNo"].ToString();
            string Address = dt.Rows[0]["ICL_Address"].ToString();
            string postcode = dt.Rows[0]["ICL_Postcode"].ToString();
            string storedImage = dt.Rows[0]["ICL_Image"].ToString();

            lb_name.Text = Name;
            txt_firstname.Text = firstname;
            txt_lastname.Text = lastname;
            txt_email.Text = Email;
            txt_phone.Text = Phone;
            txt_address.Text = Address;
            txt_postcode.Text = postcode;

            // Preserve the existing image
            if (!string.IsNullOrEmpty(storedImage))
            {
                if (storedImage.StartsWith("ICL_AVATARS"))
                {
                    str_UserProfileImage = "../Template/assets/img/Avatar/" + storedImage;
                }
                else
                {
                    str_UserProfileImage = "~/images/" + storedImage;
                }
            }
            else
            {
                str_UserProfileImage = "~/images/user.jpg"; // Default profile picture
            }

            SC.UserImage = str_UserProfileImage;

            // Update profile image preview
            if (!string.IsNullOrEmpty(hf_SelectedAvatar.Value))
            {
                img_profile.ImageUrl = hf_SelectedAvatar.Value;
            }
            else
            {
                img_profile.ImageUrl = str_UserProfileImage;
            }
        }
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string str_newid = "";
            string str_path = "";
            string Modified_on = DateTime.UtcNow.ToString();
            string selectedAvatar = hf_SelectedAvatar.Value;
            string fileNames = Path.GetFileName(selectedAvatar);

            // Retrieve existing image before updating
            string checkQuery = "SELECT ICL_Image FROM ICL_Users WHERE ICL_UserId = @ICL_UserId";
            SqlCommand checkCmd = new SqlCommand(checkQuery);
            checkCmd.Parameters.AddWithValue("@ICL_UserId", str_userkey);
            DataTable dt = DA.GetDataTable(checkCmd);
            string existingImage = (dt.Rows.Count > 0) ? dt.Rows[0]["ICL_Image"].ToString() : "";

            string str_sql = "UPDATE ICL_Users SET ICL_MobileNo=@ICL_MobileNo, ICL_Image=@ICL_Image, ICL_Address=@ICL_Address, ICL_FirstName=@ICL_FirstName, ICL_LastName=@ICL_LastName, ICL_Postcode=@ICL_Postcode, ICL_ModifiedOn=@ICL_ModifiedOn WHERE ICL_UserId=@ICL_UserId";
            SqlCommand CMD = new SqlCommand(str_sql);
            CMD.Parameters.AddWithValue("@ICL_UserId", str_userkey);

            // Check if a new image is uploaded
            string filename = Path.GetFileName(Fi_Updatepicture.FileName);
            if (!string.IsNullOrEmpty(filename))
            {
                string extension = Path.GetExtension(filename);
                str_newid = str_userkey + extension;
                str_path = Server.MapPath("~/images/") + str_newid;
                Fi_Updatepicture.SaveAs(str_path);
            }

            CMD.Parameters.AddWithValue("@ICL_FirstName", txt_firstname.Text);
            CMD.Parameters.AddWithValue("@ICL_LastName", txt_lastname.Text);
            CMD.Parameters.AddWithValue("@ICL_MobileNo", txt_phone.Text);

            // Preserve image if not updated
            if (!string.IsNullOrEmpty(str_newid))
            {
                CMD.Parameters.AddWithValue("@ICL_Image", str_newid);
            }
            else if (!string.IsNullOrEmpty(selectedAvatar))
            {
                CMD.Parameters.AddWithValue("@ICL_Image", fileNames);
            }
            else
            {
                CMD.Parameters.AddWithValue("@ICL_Image", existingImage);
            }

            CMD.Parameters.AddWithValue("@ICL_Address", txt_address.Text);
            CMD.Parameters.AddWithValue("@ICL_Postcode", txt_postcode.Text);
            CMD.Parameters.AddWithValue("@ICL_ModifiedOn", Modified_on);
            DA.ExecuteNonQuery(CMD);


            Session["ProfileUpdateSuccess"] = "Profile Updated Successfully";
            //this.assignuserdetails();
            Response.Redirect(Request.Url.AbsoluteUri);

            //lbl_success.Text = "Profile Updated Successfully";
            //lbl_success.ForeColor = System.Drawing.Color.Green;
            //div_success.Visible = true;


        }
        catch (Exception ex)
        {
            lbl_error.Text = "Please Contact Admin: " + ex.Message;
            lbl_error.ForeColor = System.Drawing.Color.Red;
            div_error.Visible = true;
        }
    }




    private void BindAvatars()
    {
        List<string> avatarList = new List<string>
    {
        "ICL_AVATARS-02.jpg",
        "ICL_AVATARS-03.jpg",
        "ICL_AVATARS-04.jpg",
        "ICL_AVATARS-05.jpg",
        "ICL_AVATARS-06.jpg",
        "ICL_AVATARS-07.jpg",
        "ICL_AVATARS-08.jpg",
        "ICL_AVATARS-09.jpg",
        "ICL_AVATARS-10.jpg",
        "ICL_AVATARS-11.jpg",
        "ICL_AVATARS-12.jpg",
        "ICL_AVATARS-13.jpg",
        "ICL_AVATARS-14.jpg",
        "ICL_AVATARS-15.jpg",
        "ICL_AVATARS-16.jpg",
        "ICL_AVATARS-17.jpg",
        "ICL_AVATARS-18.jpg",
        "ICL_AVATARS-19.jpg"
    };

        // Convert list to DataTable or Anonymous Object List
        var avatarData = avatarList.Select(avatar => new { AvatarFileName = avatar }).ToList();

        rptAvatars.DataSource = avatarData;
        rptAvatars.DataBind();
    }

}