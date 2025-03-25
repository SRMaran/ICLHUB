using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

public partial class Web_CreateOrganisation : System.Web.UI.Page
{
    SessionCustom SC;
    DataAccess da;
    PhTemplate PH;
    CommonFunction CF;
    int Role;
    string str_userid = "";
    string str_id = "";
    string str_userkey = "";
    string str_userRole = "";
    private string str_newid;
    int id;
    string strqueryid;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.da = new DataAccess();
        this.CF = new CommonFunction();
        str_userkey = SC.Userid;
        str_userRole = SC.UserRole;
        PH = new PhTemplate();


        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
        {
            txt_orgname.ReadOnly = true;
            txt_clientcode.ReadOnly = true;
            this.strqueryid = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                this.loaduser();
                SC.headername = "Organisation";
                SC.lablename = "Organisation" + "<span style='color:gray;font-size:18px'> | Organisation Update</span>";
                this.loadassignvalue();
                Btn_Submit.Text = "Update";
                headcreate.InnerText = "Update Organisation";
                create.InnerHtml = "Update Organisation";
            }
        }

        else
        {
            if (!IsPostBack)
            {
                this.loaduser();
                SC.lablename = "Organisation" + "<span style='color:gray;font-size:18px'> | Organisation Creation</span>";
                create.InnerHtml = "Create Organisation";
                Btn_Submit.Text = "Submit";
                headcreate.InnerText = "Create Organisation";
            }
        }
    }

    private void loaduser()
    {
        string str_user = "select ICL_UserName,ICL_UserId from ICL_Users  where ICL_Role IN (1, 2)";
        SqlCommand cmd = new SqlCommand(str_user);
        DataSet ds = da.GetDataSet(cmd);
        if (ds != null && ds.Tables.Count > 0)
        {
            ddl_user.DataSource = ds.Tables[0];
            ddl_user.DataTextField = "ICL_UserName";
            ddl_user.DataValueField = "ICL_UserId";
            ddl_user.DataBind();
            ddl_user.Items.Insert(0, new ListItem("Select user", "0"));
        }
       
    }

    private void loadassignvalue()
    {
        String str_key = "select org_name,org_ccode,org_address,org_userid,org_city,org_country,org_postcode,org_billingname,org_baddress,org_bcity,org_bcountry,org_bpostcode from ICl_Organization  where org_id='" + strqueryid + "' ";
        SqlCommand cmd = new SqlCommand(str_key);
        DataTable dt_assign = da.GetDataTable(cmd);
        if (dt_assign.Rows.Count > 0)
        {
            ddl_user.SelectedValue = dt_assign.Rows[0]["org_userid"].ToString();
            txt_orgname.Text = dt_assign.Rows[0]["org_name"].ToString();
            txt_clientcode.Text = dt_assign.Rows[0]["org_ccode"].ToString();
            txt_address.Text = dt_assign.Rows[0]["org_address"].ToString();
            txt_city.Text = dt_assign.Rows[0]["org_city"].ToString();
            txt_country.Text = dt_assign.Rows[0]["org_country"].ToString();
            txt_postcode.Text = dt_assign.Rows[0]["org_postcode"].ToString();
            txt_billingname.Text = dt_assign.Rows[0]["org_billingname"].ToString();
            txt_baddress.Text = dt_assign.Rows[0]["org_baddress"].ToString();
            txt_bcity.Text = dt_assign.Rows[0]["org_bcity"].ToString();
            txt_bcountry.Text = dt_assign.Rows[0]["org_bcountry"].ToString();
            txt_bpostcode.Text = dt_assign.Rows[0]["org_bpostcode"].ToString();

        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime date = DateTime.Now;
            string org_name = "";
            string org_ccode = "";
            string str_txtorg = txt_orgname.Text;
            string str_txtccode = txt_clientcode.Text;
           
            if (Request.QueryString["id"] != null)
            {
                string updatequery = "UPDATE ICl_Organization SET org_userid=@org_userid,org_name=@org_name,org_ccode=@org_ccode,org_address=@org_address,org_city=@org_city,org_country=@org_country,org_postcode=@org_postcode,org_billingname=@org_billingname,org_baddress=@org_baddress,org_bcity=@org_bcity,org_bcountry=@org_bcountry,org_bpostcode=@org_bpostcode,org_modifiedby=@org_modifiedby,org_modifiedon=@org_modifiedon WHERE org_id=@org_id";
                SqlCommand updatecmd = new SqlCommand(updatequery);
                updatecmd.Parameters.AddWithValue("@org_id", strqueryid);
                updatecmd.Parameters.AddWithValue("@org_userid", ddl_user.SelectedValue);
                updatecmd.Parameters.AddWithValue("@org_name", txt_orgname.Text);
                updatecmd.Parameters.AddWithValue("@org_ccode", txt_clientcode.Text);
                updatecmd.Parameters.AddWithValue("@org_address", txt_address.Text);
                updatecmd.Parameters.AddWithValue("@org_city", txt_city.Text);
                updatecmd.Parameters.AddWithValue("@org_country", txt_country.Text);
                updatecmd.Parameters.AddWithValue("@org_postcode", txt_postcode.Text);
                updatecmd.Parameters.AddWithValue("@org_billingname", txt_billingname.Text);
                updatecmd.Parameters.AddWithValue("@org_baddress", txt_baddress.Text);
                updatecmd.Parameters.AddWithValue("@org_bcity", txt_bcity.Text);
                updatecmd.Parameters.AddWithValue("@org_bcountry", txt_bcountry.Text);
                updatecmd.Parameters.AddWithValue("@org_bpostcode", txt_bpostcode.Text);
                updatecmd.Parameters.AddWithValue("@org_modifiedby", str_userRole);
                updatecmd.Parameters.AddWithValue("@org_modifiedon", date);
                da.ExecuteNonQuery(updatecmd);
                lbl_success.Text = "Updated successfully";
                div_success.Visible = true;
                lbl_success.Visible = true;
                div_error.Visible = false;
                Response.Redirect("~/Web/Organisationgrid.aspx");
            }

            else
            {
                string usercheck = "select org_name,org_ccode from ICl_Organization where org_ccode='" + txt_clientcode.Text + "' ";
                DataTable dt_user = da.GetDataTable(usercheck);
                if (dt_user.Rows.Count > 0)
                {
                    org_name = dt_user.Rows[0]["org_name"].ToString();
                    org_ccode = dt_user.Rows[0]["org_ccode"].ToString();
                }
                if (dt_user.Rows.Count == 0)
                {
                    string sqlquery = "INSERT INTO ICl_Organization (org_userid,org_name,org_ccode,org_address,org_city,org_country,org_postcode,org_billingname,org_baddress,org_bcity,org_bcountry,org_bpostcode,org_createdby)" +
            "VALUES (@org_userid,@org_name,@org_ccode,@org_address,@org_city,@org_country,@org_postcode,@org_billingname,@org_baddress,@org_bcity,@org_bcountry,@org_bpostcode,@org_createdby)";
                    SqlCommand cmd = new SqlCommand(sqlquery);
                    cmd.Parameters.AddWithValue("@org_userid", ddl_user.SelectedValue);
                    cmd.Parameters.AddWithValue("@org_name", txt_orgname.Text);
                    cmd.Parameters.AddWithValue("@org_ccode", txt_clientcode.Text);
                    cmd.Parameters.AddWithValue("@org_address", txt_address.Text);
                    cmd.Parameters.AddWithValue("@org_city", txt_city.Text);
                    cmd.Parameters.AddWithValue("@org_country", txt_country.Text);
                    cmd.Parameters.AddWithValue("@org_postcode", txt_postcode.Text);
                    cmd.Parameters.AddWithValue("@org_billingname", txt_billingname.Text);
                    cmd.Parameters.AddWithValue("@org_baddress", txt_baddress.Text);
                    cmd.Parameters.AddWithValue("@org_bcity", txt_bcity.Text);
                    cmd.Parameters.AddWithValue("@org_bcountry", txt_bcountry.Text);
                    cmd.Parameters.AddWithValue("@org_bpostcode", txt_bpostcode.Text);
                    cmd.Parameters.AddWithValue("@org_createdby", str_userRole);
                    da.ExecuteNonQuery(cmd);
                    string str_user = ddl_user.SelectedValue;
                    if (str_user != "0" || str_user != "" || str_user != null)
                    {
                        string getuserid = "select ICL_UserId,ICL_UserName,ICL_Email from ICL_Users where ICL_UserId='" + str_user + "'";
                        System.Data.DataTable dt_userid = da.GetDataTable(getuserid);
                        if (dt_userid.Rows.Count > 0)
                        {
                            string str_mail = dt_userid.Rows[0]["ICL_Email"].ToString();
                            string ussername = dt_userid.Rows[0]["ICL_UserName"].ToString();
                            string str_userkey = dt_userid.Rows[0]["ICL_UserId"].ToString();
                            string str_link = "http://206.206.125.70/ICLHUB/createpassword.aspx?id=" + str_userkey + "";
                            //string email_fun = this.CF.PasswordRecovery(str_mail, "Createpassword", "Create Password Recovery", str_link, ussername);
                        }
                    }
                    lbl_success.Text = "Organisation Created Successfully";
                    div_success.Visible = true;
                    lbl_success.Visible = true;
                    div_error.Visible = false;
                    Response.Redirect("~/Web/Organisationgrid.aspx");

                }
                else
                {
                    lbl_error.Text = "This Organisation Already Exists";
                    div_error.Visible = true;
                    lbl_error.Visible = true;
                    div_success.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = "An error occurred: " + ex.Message;
            div_error.Visible = true;
            div_success.Visible = false;
        }
    }

}

