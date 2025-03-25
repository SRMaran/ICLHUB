using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;

using System.Web.UI;
using WebLabel = System.Web.UI.WebControls.Label;

using System.Activities.Statements;
using System.Security.Policy;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web;
using Newtonsoft.Json.Linq;
public partial class Web_UserCreation : System.Web.UI.Page
{
    SessionCustom SC;
    DataAccess da;
    PhTemplate PH;
    CommonFunction CF;
    string usernamee = "";
    string str_userid = "";
    string str_userkey = "";
    int id;
    string str_lbvalue = "";
    string str_userRole = "";
    string selectdRole = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.str_userkey = SC.Userid;
        this.da = new DataAccess();
        this.CF = new CommonFunction();
        PH = new PhTemplate();
        str_userRole = SC.UserRole;
        str_userid = SC.Userid;
        SC.headername = "User";
        SC.lablename = "User" + "<span style='color:#112560;font-size:18px'> | User Creation</span>";

        if (Session["SelectedValues"] == null)
        {
            Session["SelectedValuess"] = new List<Tuple<string, string>>();
            Session["SelectedValues"] = new List<Tuple<string, string>>();
        }

        if (Request.QueryString["id"] != null)
        {
            btnBack.Visible = true;
            back.Visible = false;
            str_userid = Request.QueryString["id"].ToString();
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                if (!IsPostBack)
                {
                    PopulateCheckBoxList();
                    //txt_email.ReadOnly = true;
                    rdstatus.Visible = true;
                    create.InnerHtml = "Update User";
                    headcreate.InnerText = "Update User";
                    PopulateFormDataForUpdate(id);
                    btnSubmit.Text = "Update";
                    headcreate.InnerText = "Update User";

                }
            }
        }
        else
        {

            btnBack.Visible = false;
            back.Visible = true;
            if (!IsPostBack)
            {


                List<Tuple<string, string>> selectedItemssql = Session["SelectedValuess"] as List<Tuple<string, string>>;
                List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>>;
                selectedItemssql.Clear();
                selectedItems.Clear();
                PopulateCheckBoxList();
                rdstatus.Visible = false;
                create.InnerHtml = "Create User";
                headcreate.InnerText = "Create User";
                btnSubmit.Text = "Submit";
                headcreate.InnerText = "Create User";
            }
        }

    }
    private void PopulateCheckBoxList()
    {
        string check = "select org_name,org_id,org_ccode from ICl_Organization order by org_name Asc";
        SqlCommand cmd = new SqlCommand(check);
        DataSet ds = da.GetDataSet(cmd);
        if (ds != null && ds.Tables.Count > 0)
        {
            lb_orgname.DataSource = ds.Tables[0];
            lb_orgname.DataTextField = "org_name";
            lb_orgname.DataValueField = "org_id";
            lb_orgname.DataBind();
            lb_orgname.Items.Insert(0, new ListItem("Select Organisation", "0"));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            selectdRole = UserRole.SelectedValue;
            string strmail = txt_email.Text;
            DateTime date = DateTime.Now;
            string usercheck = "select ICL_Email from ICL_Users where ICL_Email='" + txt_email.Text + "'";
            System.Data.DataTable dt_user = da.GetDataTable(usercheck);
            if (Request.QueryString["id"] != null)
            {
                this.updatequery();
            }
            else
            {
                if (dt_user.Rows.Count == 0)
                {
                    string sqlquery = "INSERT INTO ICL_Users (ICL_username,ICL_FirstName,ICL_Email,ICL_MobileNo,ICL_Address,ICL_postcode,ICL_Createdby,ICL_Status,ICL_role,ICL_Password)" +
          "VALUES (@ICL_username,@ICL_FirstName,@ICL_Email,@ICL_MobileNo,@ICL_Address,@ICL_postcode,@ICL_Createdby,@ICL_Status,@ICL_role,@ICL_Password)";
                    SqlCommand cmd = new SqlCommand(sqlquery);
                    cmd.Parameters.AddWithValue("@ICL_username", txt_username.Text);
                    cmd.Parameters.AddWithValue("@ICL_FirstName", txt_username.Text);
                    cmd.Parameters.AddWithValue("@ICL_Email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@ICL_MobileNo", txt_phone.Text);
                    cmd.Parameters.AddWithValue("@ICL_role", selectdRole);
                    cmd.Parameters.AddWithValue("@ICL_Status", 0);
                    cmd.Parameters.AddWithValue("@ICL_Address", txt_address.Text);
                    cmd.Parameters.AddWithValue("@ICL_postcode", txt_postcode.Text);
                    cmd.Parameters.AddWithValue("@ICL_Createdby", str_userid);
                    cmd.Parameters.AddWithValue("@ICL_Password", txt_password.Text);
                    da.ExecuteNonQuery(cmd);
                    string groupvalue = "select ICL_UserId from ICL_Users where ICL_Email='" + txt_email.Text + "'";
                    System.Data.DataTable dt1 = da.GetDataTable(groupvalue);
                    string user_value = dt1.Rows[0]["ICL_UserId"].ToString();
                    List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>>;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            string str_selectvalue = item.Item1;
                            string update = "update  ICL_organization set org_userid=@org_userid where org_id='" + str_selectvalue + "'";
                            SqlCommand cmd2 = new SqlCommand(update);
                            cmd2.Parameters.AddWithValue("@org_userid", user_value);
                            da.ExecuteNonQuery(cmd2);
                            string insert1 = "insert into ICL_userorganization(ur_userid,ur_orgid)values(@ur_userid,@ur_orgid)";
                            SqlCommand cmd1 = new SqlCommand(insert1);
                            cmd1.Parameters.AddWithValue("@ur_userid", user_value);
                            cmd1.Parameters.AddWithValue("@ur_orgid", str_selectvalue);
                            da.ExecuteNonQuery(cmd1);
                        }
                        selectedItems.Clear();
                    }

                    string str_mail = txt_email.Text;
                    string ussername = txt_username.Text;
                    string getuserid = "select ICL_UserId from ICL_Users where ICL_Email='" + txt_email.Text + "'";
                    System.Data.DataTable dt_userid = da.GetDataTable(getuserid);
                    if (dt_userid.Rows.Count > 0)
                    {
                        string str_userkey = dt_userid.Rows[0]["ICL_UserId"].ToString();
                        string str_link = "http://206.206.125.70/ICLHUB/createpassword.aspx?id=" + str_userkey + "";
                        string email_fun = this.CF.PasswordRecovery(str_mail, "Createpassword", "Create Password Recovery", str_link, ussername);

                    }
                    lbl_success.Text = "User created successfully.";
                    div_success.Visible = true;
                    lbl_success.Visible = true;
                    div_error.Visible = false;
                    Response.Redirect("~/Web/usergrid.aspx");
                    selectedItems.Clear();
                }
                else
                {
                    lbl_error.Text = "This user is already assigned to this organisation";
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

    private void updatequery()
    {
        selectdRole = UserRole.SelectedValue;
        string strmail = txt_email.Text;
        DateTime date = DateTime.Now;
        string updateQuery1 = "UPDATE ICL_Users SET  ICL_username=@ICL_username,ICL_Email=@ICL_Email,ICL_MobileNo=@ICL_MobileNo,ICL_Address=@ICL_Address,ICL_postcode=@ICL_postcode,ICL_Status=@ICL_Status,ICL_role=@ICL_role,ICL_ModifiedOn=@ICL_ModifiedOn,ICL_Modifiedby=@ICL_Modifiedby,ICL_Password=@ICL_Password where ICL_UserId='" + str_userid + "'";
        SqlCommand cmd = new SqlCommand(updateQuery1);
        cmd.Parameters.AddWithValue("@ICL_username", txt_username.Text);
        cmd.Parameters.AddWithValue("@ICL_Email", txt_email.Text);
        cmd.Parameters.AddWithValue("@ICL_MobileNo", txt_phone.Text);
        cmd.Parameters.AddWithValue("@ICL_role", selectdRole);
        cmd.Parameters.AddWithValue("@ICL_Status", rd_status.SelectedValue);
        cmd.Parameters.AddWithValue("@ICL_Address", txt_address.Text);
        cmd.Parameters.AddWithValue("@ICL_postcode", txt_postcode.Text);
        cmd.Parameters.AddWithValue("@ICL_Modifiedby", str_userRole);
        cmd.Parameters.AddWithValue("@ICL_ModifiedOn", date);
        cmd.Parameters.AddWithValue("@ICL_UserId", str_userid);
        cmd.Parameters.AddWithValue("@ICL_Password", txt_password.Text);
        da.ExecuteNonQuery(cmd);
        string groupvalue = "select ICL_UserId from ICL_Users where ICL_Email='" + txt_email.Text + "'";
        System.Data.DataTable dt1 = da.GetDataTable(groupvalue);
        string user_value = dt1.Rows[0]["ICL_UserId"].ToString();
        string deletecheck = "Select ur_orgid from ICL_userorganization where ur_userid='" + user_value + "'";
        System.Data.DataTable dt_delete = da.GetDataTable(deletecheck);
        if (dt_delete.Rows.Count > 0)
        {
            foreach (DataRow row in dt_delete.Rows)
            {
                string orgid = row["ur_orgid"].ToString();
                string update = "update  ICL_organization set org_userid=@org_userid where org_id='" + orgid + "'";
                SqlCommand cmd2 = new SqlCommand(update);
                cmd2.Parameters.AddWithValue("@org_userid", DBNull.Value);
                da.ExecuteNonQuery(cmd2);
            }
            string delete = "Delete from ICL_userorganization where ur_userid='" + user_value + "'";
            SqlCommand del = new SqlCommand(delete);
            da.ExecuteNonQuery(del);
        }
        List<Tuple<string, string>> selectedItemssql = Session["SelectedValuess"] as List<Tuple<string, string>>;
        List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>>;
        List<Tuple<string, string>> mergedList = selectedItemssql
            .Union(selectedItems)
            .ToList();
        if (mergedList != null && mergedList.Count == 0)
        {
            List<Tuple<string, string>> selectedItemss = Session["OrganizationNames"] as List<Tuple<string, string>>;
            mergedList = selectedItemss;
        }
        if (mergedList != null && mergedList.Count > 0)
        {
            foreach (var item in mergedList)
            {
                string str_selectvalue = item.Item1;
                string update = "update  ICL_organization set org_userid=@org_userid where org_id='" + str_selectvalue + "'";
                SqlCommand cmd2 = new SqlCommand(update);
                cmd2.Parameters.AddWithValue("@org_userid", user_value);
                da.ExecuteNonQuery(cmd2);
                string insert1 = "insert into ICL_userorganization(ur_userid,ur_orgid)values(@ur_userid,@ur_orgid)";
                SqlCommand cmd1 = new SqlCommand(insert1);
                cmd1.Parameters.AddWithValue("@ur_userid", user_value);
                cmd1.Parameters.AddWithValue("@ur_orgid", str_selectvalue);
                da.ExecuteNonQuery(cmd1);
            }
            mergedList.Clear();
            //Session.Remove("SelectedValues");
        }

        lbl_success.Text = "User updated successfully";
        div_success.Visible = true;
        lbl_success.Visible = true;
        div_error.Visible = false;
        Response.Redirect("~/Web/usergrid.aspx", false);
        Context.ApplicationInstance.CompleteRequest(); // Stops further processing safely
    }
    //protected void CB_orgname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectdropdown = lb_orgname.SelectedValue;
    //    selectdRole = UserRole.SelectedValue;
    //    if (selectdRole == "0")
    //    {           
    //        Clientvalue.Visible = true;
    //        Clientvalue.ForeColor = System.Drawing.Color.Red;
    //        Clientvalue.Text = "Role is required";
    //        return;
    //    }
    //    Clientvalue.Visible = false;
    //    lb_organization.Visible = false;
    //    List<Tuple<string, string>> selectedItems = Session[" "] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();
    //    List<Tuple<string, string>> selectedItemssql = Session["OrganizationNames"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();
    //    string selectedValue = lb_orgname.SelectedValue;
    //    string selectedText = lb_orgname.SelectedItem.Text;
    //    if (!selectedItems.Any(item => item.Item1 == selectedValue))
    //    {
    //        selectedItems.Add(new Tuple<string, string>(selectedValue, selectedText));
    //    }
    //    var mergedItems = selectedItems.Concat(selectedItemssql)
    //                                   .GroupBy(item => item.Item1)
    //                                   .Select(group => group.First())
    //                                   .ToList();
    //    Session["SelectedValues"] = mergedItems;
    //    Session["OrganizationNames"] = mergedItems;
    //    ph_orgname.Controls.Clear();
    //    foreach (var item in mergedItems)
    //    {
    //        WebLabel lbl = new WebLabel
    //        {
    //            Text = item.Item2,
    //            CssClass = "text-white"
    //        };
    //        HtmlGenericControl divContainer = new HtmlGenericControl("div");
    //        divContainer.Attributes["class"] = "custom-badge alert alert-danger alert-dismissible alert-label-icon label-arrow fade show btn btn-outline-warning me-1 mb-1";
    //        divContainer.Controls.Add(lbl);
    //        HtmlGenericControl closeButton = new HtmlGenericControl("a");
    //        closeButton.Attributes["class"] = "btn-close1";
    //        closeButton.Attributes["data-bs-dismiss"] = "alert";
    //        closeButton.Attributes["aria-label"] = "Close";
    //        closeButton.Attributes["href"] = "javascript:void(0);";
    //        closeButton.Attributes["onclick"] = "removeLabel(this, '" + item.Item1 + "');";
    //        divContainer.Controls.Add(closeButton);
    //        ph_orgname.Controls.Add(divContainer);
    //    }
    //    //this.PopulateFormDataForUpdate(id);
    //}

    //protected void CB_orgname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectedRole = UserRole.SelectedValue;

    //    // Validate User Role Selection
    //    if (selectedRole == "0")
    //    {
    //        Clientvalue.Visible = true;
    //        Clientvalue.ForeColor = System.Drawing.Color.Red;
    //        Clientvalue.Text = "Role is required";
    //        return;
    //    }
    //    Clientvalue.Visible = false;
    //    lb_organization.Visible = false;

    //    // Get session values
    //    List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();

    //    // If user role is changed, reset everything and add the first organization
    //    if (Session["PreviousUserRole"] == null || Session["PreviousUserRole"].ToString() != selectedRole)
    //    {
    //        selectedItems.Clear(); // Remove all previous selections
    //        if (lb_orgname.Items.Count > 0)
    //        {
    //            string firstValue = lb_orgname.Items[0].Value;
    //            string firstText = lb_orgname.Items[0].Text;
    //            selectedItems.Add(new Tuple<string, string>(firstValue, firstText));
    //            lb_orgname.SelectedValue = firstValue; // Ensure dropdown reflects selection
    //        }
    //    }
    //    else
    //    {
    //        // Handle new organization selection without removing previous ones
    //        string selectedValue = lb_orgname.SelectedValue;
    //        string selectedText = lb_orgname.SelectedItem.Text;
    //        if (!selectedItems.Any(item => item.Item1 == selectedValue))
    //        {
    //            // Remove first only if it's still there and a new one is selected
    //            if (selectedItems.Count > 0 && selectedItems[0].Item1 == lb_orgname.Items[0].Value)
    //            {
    //                selectedItems.RemoveAt(0);
    //            }
    //            selectedItems.Add(new Tuple<string, string>(selectedValue, selectedText));
    //        }
    //    }
    //    // Save updated selections
    //    Session["SelectedValues"] = selectedItems;
    //    Session["PreviousUserRole"] = selectedRole;

    //    // Update UI
    //    ph_orgname.Controls.Clear();
    //    foreach (var item in selectedItems)
    //    {
    //        WebLabel lbl = new WebLabel
    //        {
    //            Text = item.Item2,
    //            CssClass = "text-white"
    //        };

    //        HtmlGenericControl divContainer = new HtmlGenericControl("div");
    //        divContainer.Attributes["class"] = "custom-badge alert alert-danger alert-dismissible alert-label-icon label-arrow fade show btn btn-outline-warning me-1 mb-1";
    //        divContainer.Controls.Add(lbl);

    //        HtmlGenericControl closeButton = new HtmlGenericControl("a");
    //        closeButton.Attributes["class"] = "btn-close1";
    //        closeButton.Attributes["data-bs-dismiss"] = "alert";
    //        closeButton.Attributes["aria-label"] = "Close";
    //        closeButton.Attributes["href"] = "javascript:void(0);";
    //        closeButton.Attributes["onclick"] = "removeLabel(this, '" + item.Item1 + "');";

    //        divContainer.Controls.Add(closeButton);
    //        ph_orgname.Controls.Add(divContainer);
    //    }
    //    this.PopulateFormDataForUpdate(id);
    //}

    //protected void CB_orgname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectedRole = UserRole.SelectedValue;

    //    // Validate User Role Selection
    //    if (selectedRole == "0")
    //    {
    //        Clientvalue.Visible = true;
    //        Clientvalue.ForeColor = System.Drawing.Color.Red;
    //        Clientvalue.Text = "Role is required";
    //        return;
    //    }
    //    Clientvalue.Visible = false;
    //    lb_organization.Visible = false;

    //    // Get session values
    //    List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();

    //    // **CHECK IF UPDATE MODE IS ACTIVE**
    //    bool isUpdateMode = (Session["IsUpdateMode"] != null && (bool)Session["IsUpdateMode"] == true);

    //    // **Clear selections if UserRole is changed**
    //    if (Session["PreviousUserRole"] == null || Session["PreviousUserRole"].ToString() != selectedRole)
    //    {
    //        /* selectedItems.Clear();*/ // Remove all previous selections

    //        if (!isUpdateMode && lb_orgname.Items.Count > 0)
    //        {
    //            // Select the first organization ONLY if NOT updating
    //            string firstValue = lb_orgname.Items[0].Value;
    //            string firstText = lb_orgname.Items[0].Text;
    //            selectedItems.Add(new Tuple<string, string>(firstValue, firstText));
    //            lb_orgname.SelectedValue = firstValue; // Ensure dropdown reflects selection
    //        }
    //    }

    //    else
    //    {
    //        // Handle new organization selection without removing previous ones
    //        string selectedValue = lb_orgname.SelectedValue;
    //        string selectedText = lb_orgname.SelectedItem.Text;

    //        //if (!selectedItems.Any(item => item.Item1 == selectedValue))
    //        //{
    //        //    // Remove first only if it's still there and a new one is selected
    //        //    if (selectedItems.Count > 0 && selectedItems[0].Item1 == lb_orgname.Items[0].Value)
    //        //    {
    //        //        selectedItems.RemoveAt(0);
    //        //    }
    //        //    selectedItems.Add(new Tuple<string, string>(selectedValue, selectedText));
    //        //}
    //        if (!selectedItems.Any(item => item.Item1 == selectedValue)) // Only add if not already selected
    //        {
    //            selectedItems.Add(new Tuple<string, string>(selectedValue, selectedText));
    //        }
    //    }

    //    // Save updated selections
    //    Session["SelectedValues"] = selectedItems;
    //    Session["PreviousUserRole"] = selectedRole;

    //    // **UPDATE UI**
    //    ph_orgname.Controls.Clear();
    //    foreach (var item in selectedItems)
    //    {
    //        WebLabel lbl = new WebLabel
    //        {
    //            Text = item.Item2,
    //            CssClass = "text-white"
    //        };

    //        HtmlGenericControl divContainer = new HtmlGenericControl("div");
    //        divContainer.Attributes["class"] = "custom-badge alert alert-danger alert-dismissible alert-label-icon label-arrow fade show btn btn-outline-warning me-1 mb-1";
    //        divContainer.Controls.Add(lbl);

    //        HtmlGenericControl closeButton = new HtmlGenericControl("a");
    //        closeButton.Attributes["class"] = "btn-close1";
    //        closeButton.Attributes["data-bs-dismiss"] = "alert";
    //        closeButton.Attributes["aria-label"] = "Close";
    //        closeButton.Attributes["href"] = "javascript:void(0);";
    //        closeButton.Attributes["onclick"] = "removeLabel(this, '" + item.Item1 + "');";

    //        divContainer.Controls.Add(closeButton);
    //        ph_orgname.Controls.Add(divContainer);
    //    }

    //    // **HANDLE UPDATE MODE**
    //    if (isUpdateMode)
    //    {
    //        PopulateFormDataForUpdate(id);
    //    }
    //}
    protected void CB_orgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedRole = UserRole.SelectedValue;

        // Validate User Role Selection
        if (selectedRole == "0")
        {
            Clientvalue.Visible = true;
            Clientvalue.ForeColor = System.Drawing.Color.Red;
            Clientvalue.Text = "Role is required";
            return;
        }
        Clientvalue.Visible = false;
        lb_organization.Visible = false;

        // Get session values
        List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();

        // Check if update mode is active
        bool isUpdateMode = Session["IsUpdateMode"] != null && (bool)Session["IsUpdateMode"] == true;

        // **CASE 1: UserRole Change → Reset Everything and Select First Organization**
        if (Session["PreviousUserRole"] == null || Session["PreviousUserRole"].ToString() != selectedRole)
        {
            selectedItems.Clear(); // Remove all previous selections

            if (lb_orgname.Items.Count > 0)
            {
                // Select the first organization by default
                string firstValue = lb_orgname.Items[0].Value;
                string firstText = lb_orgname.Items[0].Text;
                selectedItems.Add(new Tuple<string, string>(firstValue, firstText));
                lb_orgname.SelectedValue = firstValue;
            }
        }
        else
        {
            // **CASE 2: Selecting Organizations in Update Mode**
            string selectedValue = lb_orgname.SelectedValue;
            string selectedText = lb_orgname.SelectedItem.Text;

            if (!selectedItems.Any(item => item.Item1 == selectedValue))
            {
                selectedItems.Add(new Tuple<string, string>(selectedValue, selectedText));
            }

            // **Remove only the first default value if a new selection is made**
            if (selectedItems.Count > 1 && selectedItems[0].Item1 == lb_orgname.Items[0].Value)
            {
                selectedItems.RemoveAt(0);
            }
        }

        // Save updated selections
        Session["SelectedValues"] = selectedItems;
        Session["PreviousUserRole"] = selectedRole;

        // **Update UI**
        ph_orgname.Controls.Clear();
        foreach (var item in selectedItems)
        {
            WebLabel lbl = new WebLabel
            {
                Text = item.Item2,
                CssClass = "text-white"
            };

            HtmlGenericControl divContainer = new HtmlGenericControl("div");
            divContainer.Attributes["class"] = "custom-badge alert alert-danger alert-dismissible alert-label-icon label-arrow fade show btn btn-outline-warning me-1 mb-1";
            divContainer.Controls.Add(lbl);

            HtmlGenericControl closeButton = new HtmlGenericControl("a");
            closeButton.Attributes["class"] = "btn-close1";
            closeButton.Attributes["data-bs-dismiss"] = "alert";
            closeButton.Attributes["aria-label"] = "Close";
            closeButton.Attributes["href"] = "javascript:void(0);";
            closeButton.Attributes["onclick"] = "removeLabel(this, '" + item.Item1 + "');";

            divContainer.Controls.Add(closeButton);
            ph_orgname.Controls.Add(divContainer);
        }

        // **Handle Update Mode**
        if (isUpdateMode)
        {
            PopulateFormDataForUpdate(id);
        }
    }

    private void PopulateFormDataForUpdate(int id)
    {
        string query = "SELECT a.ICL_username, a.ICL_Email, a.ICL_MobileNo, a.ICL_Address, a.ICL_Status, a.ICL_postcode,a.ICL_role,a.ICL_Password, b.ur_orgid, c.org_name " +
                       "FROM ICL_Users a " +
                       "LEFT JOIN ICL_userorganization b ON b.ur_userid = a.ICL_UserId " +
                       "left JOIN ICL_Organization c ON b.ur_orgid = c.org_id " +
                       "WHERE a.ICL_UserId = @UserId";

        SqlCommand cmd = new SqlCommand(query);
        cmd.Parameters.AddWithValue("@UserId", id);
        DataTable dt = da.GetDataTable(cmd);

        if (dt.Rows.Count > 0)
        {
            txt_username.Text = dt.Rows[0]["ICL_username"].ToString();
            txt_email.Text = dt.Rows[0]["ICL_Email"].ToString();
            txt_phone.Text = dt.Rows[0]["ICL_MobileNo"].ToString();
            txt_address.Text = dt.Rows[0]["ICL_Address"].ToString();
            txt_postcode.Text = dt.Rows[0]["ICL_postcode"].ToString();
            txt_password.Text = dt.Rows[0]["ICL_Password"].ToString();
            rd_status.SelectedValue = dt.Rows[0]["ICL_Status"].ToString();
            UserRole.SelectedValue = dt.Rows[0]["ICL_role"].ToString();


            List<Tuple<string, string>> selectedItems = Session["OrganizationNames"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();
            selectedItems.Clear();

            // Add values from the database
            foreach (DataRow row in dt.Rows)
            {
                string orgId = row["ur_orgid"].ToString();
                string orgName = row["org_name"].ToString();
                selectedItems.Add(new Tuple<string, string>(orgId, orgName));
            }

            // Update session
            Session["OrganizationNames"] = selectedItems;

            // Update UI
            ph_orgname.Controls.Clear();
            foreach (var item in selectedItems)
            {
                WebLabel lbl = new WebLabel
                {
                    Text = item.Item2,
                    CssClass = "text-white"
                };

                HtmlGenericControl divContainer = new HtmlGenericControl("div");
                divContainer.Attributes["class"] = "custom-badge alert alert-danger alert-dismissible alert-label-icon label-arrow fade show btn btn-outline-warning me-1 mb-1";
                divContainer.Controls.Add(lbl);

                HtmlGenericControl closeButton = new HtmlGenericControl("a");
                closeButton.Attributes["class"] = "btn-close1";
                closeButton.Attributes["data-bs-dismiss"] = "alert";
                closeButton.Attributes["aria-label"] = "Close";
                closeButton.Attributes["href"] = "javascript:void(0);";
                closeButton.Attributes["onclick"] = "removeLabel(this, '" + item.Item1 + "');";
                divContainer.Controls.Add(closeButton);

                ph_orgname.Controls.Add(divContainer);
            }


        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        List<Tuple<string, string>> selectedItemssql = Session["SelectedValuess"] as List<Tuple<string, string>>;
        List<Tuple<string, string>> selectedItems = Session["SelectedValues"] as List<Tuple<string, string>>;
        selectedItemssql.Clear();
        selectedItems.Clear();
        Response.Redirect("./UserGrid.aspx");
    }
    [System.Web.Services.WebMethod]
    public static void RemoveSessionValue(string value)
    {
        var context = HttpContext.Current;
        if (context != null)
        {
            // Retrieve the session values
            var selectedItems = context.Session["SelectedValues"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();
            var selectedItemssql = context.Session["OrganizationNames"] as List<Tuple<string, string>> ?? new List<Tuple<string, string>>();

            // Remove the item with the matching value
            selectedItems.RemoveAll(item => item.Item1 == value);
            selectedItemssql.RemoveAll(item => item.Item1 == value);

            // Merge and update session
            var mergedItems = selectedItems
                .Concat(selectedItemssql)
                .GroupBy(item => item.Item1)
                .Select(group => group.First())
                .ToList();

            context.Session["SelectedValues"] = mergedItems;
            context.Session["OrganizationNames"] = mergedItems;
        }
    }
    protected void Back_Click(object sender, EventArgs e)
    {
        var context = HttpContext.Current;
        if (context != null)
        {
            context.Session["SelectedValues"] = null;
            context.Session["SelectedValuess"] = null;
        }
        Response.Redirect("UserGrid.aspx");
    }

}