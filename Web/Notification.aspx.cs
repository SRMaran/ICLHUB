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
using System.Web.Script.Services;
using System.Configuration;
public partial class Web_Notification : System.Web.UI.Page
{
    DataAccess DA;
    SessionCustom SC;
    PhTemplate PH;
    string str_userid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DA = new DataAccess();
        this.SC = new SessionCustom();
        this.PH = new PhTemplate();
        SC.lablename = "Notification";
        SC.headername = "Notification";
        this.SC = new SessionCustom();
        str_userid = SC.Userid;
        if (!IsPostBack) // Load settings on first page load
        {
            loadnotification();
        }
    }
    private void loadnotification()
    {
        string checkQuery = "SELECT shipments, booking, customs, email_notifications FROM icl_notification WHERE userkey = @UserId";
        SqlCommand checkCmd = new SqlCommand(checkQuery);
        checkCmd.Parameters.AddWithValue("@UserId", str_userid);
        DataTable dt_user = DA.GetDataTable(checkCmd);

        bool shipmentsActive = false;
        bool bookingActive = false;
        bool customsActive = false;
        int emailNotifications = 1;
        

        if (dt_user.Rows.Count > 0)
        {
            shipmentsActive = dt_user.Rows[0]["shipments"] != DBNull.Value && Convert.ToInt32(dt_user.Rows[0]["shipments"]) == 1;
            bookingActive = dt_user.Rows[0]["booking"] != DBNull.Value && Convert.ToInt32(dt_user.Rows[0]["booking"]) == 1;
            customsActive = dt_user.Rows[0]["customs"] != DBNull.Value && Convert.ToInt32(dt_user.Rows[0]["customs"]) == 1;
            emailNotifications = Convert.ToInt32(dt_user.Rows[0]["email_notifications"]);
        }
        // Set checkboxes and radio buttons using JavaScript
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetCheckboxes", @"
        $(document).ready(function(){
            $('#shipments').prop('checked', " + (shipmentsActive ? "false" : "true") + @");
            $('#bookings').prop('checked', " + (bookingActive ? "false" : "true") + @");
            $('#customs').prop('checked', " + (customsActive ? "false" : "true") + @");
            // Email notification radio buttons
            $('input[name=emailNotification][value=" + emailNotifications + @"]').prop('checked', true);
        });
    ", true);
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object UpdateSetting(string settingType, string value)
    {
        string setcolumn = "";

        if (settingType == "booking") setcolumn = "booking";
        else if (settingType == "customs") setcolumn = "customs";
        else if (settingType == "shipments") setcolumn = "shipments";
        else if (settingType == "email_notifications") setcolumn = "email_notifications";

        string str_userid = "";
        SessionCustom SC = new SessionCustom();
        DataAccess DA = new DataAccess();
        str_userid = SC.Userid;

        try
        {
            string checkQuery = "SELECT USERKEY FROM icl_notification WHERE userkey = '"+ str_userid + "'";
            SqlCommand checkCmd = new SqlCommand(checkQuery);
            DataTable dt_user = DA.GetDataTable(checkCmd);

            if (dt_user.Rows.Count == 0)
            {
                string insertQuery = "INSERT INTO icl_notification (userkey, shipments, booking, customs, email_notifications, created_on) " +
                                     "VALUES (@UserId, 0, 0, 0, 1, GETDATE())";
                SqlCommand insertCmd = new SqlCommand(insertQuery);
                insertCmd.Parameters.AddWithValue("@UserId", str_userid);
                DA.ExecuteNonQuery(insertCmd);
            }

            string updateQuery = "UPDATE icl_notification SET " + setcolumn + " = @Value WHERE userkey = @UserId";
            SqlCommand updateCmd = new SqlCommand(updateQuery);
            updateCmd.Parameters.AddWithValue("@UserId", str_userid);
            updateCmd.Parameters.AddWithValue("@Value", value);
            DA.ExecuteNonQuery(updateCmd);

            return new { status = "success", message = "Update Successful!" };
        }
        catch (Exception ex)
        {
            return new { status = "error", message = "Error: " + ex.Message };
        }
    }

    
}
