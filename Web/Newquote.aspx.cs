using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_Newquote : System.Web.UI.Page
{

    DataAccess DA;
    SessionCustom SC;
    CommonFunction CF;
    string str_userkey = "";
    string str_Org = "";
    string str_Orgcode = "";
    string str_userRole = "";
    private string str_newid;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.DA = new DataAccess();
        this.CF = new CommonFunction();
        this.str_userkey = SC.Userid;
        str_userRole = SC.UserRole;
        txtFirstName.Text = SC.UserFullname;
        txtEmail.Text = SC.username;
        str_Orgcode = SC.Orgnamecode;
        str_Org = SC.Org_names;
        if (str_userRole != "0")
        {
            str_Org = SC.Org_names;
            str_Orgcode = SC.Orgnamecode;
            txtcompanyname.Text = str_Org;

        }

        SC.lablename = "Quotes <span style='color:#112560;font-size:18px'>  | NEW QUOTES  </span>";
        SC.headername = "Quotes";


    }

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            int status1 = 0;
            string str_path = "";
            string email_to = "";
            string Reference = "";

            string insertQuery = "INSERT INTO Quotes_Details " +
                         "(FirstName, Surename, CompanyName, EmailAddress, ContactNumber, TransportMode, ReadyToCollectDate, " +
                         "TargetDeliveryDate, Volume, Weight, Width, Length, Height, Dimensions, PackageType, Quantity, " +
                         "CustomsClearance, Incoterms, HazardousGoods, OutOfGauge, AdditionalComments, UploadFile, " +
                         "CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) " +
                         "VALUES (@FirstName, @Surename, @CompanyName, @EmailAddress, @ContactNumber, @TransportMode, @ReadyToCollectDate, " +
                         "@TargetDeliveryDate, @Volume, @Weight, @Width, @Length, @Height, @Dimensions, @PackageType, @Quantity, " +
                         "@CustomsClearance, @Incoterms, @HazardousGoods, @OutOfGauge, @AdditionalComments, @UploadFile, " +
                         "@CreatedOn, @CreatedBy, @ModifiedOn, @ModifiedBy)";

            using (SqlCommand insertCmd = new SqlCommand(insertQuery))
            {
                insertCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                insertCmd.Parameters.AddWithValue("@Surename", txtSurname.Text);
                insertCmd.Parameters.AddWithValue("@CompanyName", txtcompanyname.Text);
                insertCmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);
                insertCmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text);
                insertCmd.Parameters.AddWithValue("@TransportMode", ddlTransportMode.Text);
                insertCmd.Parameters.AddWithValue("@ReadyToCollectDate", txtStartingDate.Text);
                insertCmd.Parameters.AddWithValue("@TargetDeliveryDate", txtEndingDate.Text);
                insertCmd.Parameters.AddWithValue("@Volume", txtVolume.Text);
                insertCmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                insertCmd.Parameters.AddWithValue("@Width", txtWidth.Text);
                insertCmd.Parameters.AddWithValue("@Length", txtLength.Text);
                insertCmd.Parameters.AddWithValue("@Height", txtHeight.Text);
                insertCmd.Parameters.AddWithValue("@Dimensions", txtDimensions.Text);
                insertCmd.Parameters.AddWithValue("@PackageType", ddlPackageType.SelectedValue);
                insertCmd.Parameters.AddWithValue("@Quantity", txtQTY.Text);
                insertCmd.Parameters.AddWithValue("@CustomsClearance", ddlCustomsClearanceRequirements.SelectedValue);
                insertCmd.Parameters.AddWithValue("@Incoterms", ddlIncoterms.SelectedValue);
                insertCmd.Parameters.AddWithValue("@AdditionalComments", txtComment.Text);
                insertCmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);


                if (OutOfGauge.SelectedItem.Text == "Yes")
                {
                    status = 0;
                }
                else { status = 1; }
                insertCmd.Parameters.AddWithValue("@OutOfGauge", status);
                if (HazardousGoods.SelectedItem.Text == "Yes")
                {
                    status1 = 0;
                }
                else { status1 = 1; }
                insertCmd.Parameters.AddWithValue("@HazardousGoods", status1);

                // Handle file upload
                string filename2 = Path.GetFileName(insertQuery);
                if (!string.IsNullOrEmpty(Fi_Updatepicture.FileName))
                {
                    string extension = Path.GetExtension(Fi_Updatepicture.FileName);
                    string originalFileName = Path.GetFileNameWithoutExtension(Fi_Updatepicture.FileName); // Get only filename without extension
                    this.str_newid = this.str_userkey + originalFileName + extension; // Correct concatenation
                    str_path = Server.MapPath("~/images/") + this.str_newid;
                    Fi_Updatepicture.SaveAs(str_path);

                    // Storing only the filename (string)
                    insertCmd.Parameters.AddWithValue("@UploadFile", this.str_newid);
                }


                else
                {
                    insertCmd.Parameters.AddWithValue("@UploadFile", DBNull.Value);
                }


                insertCmd.Parameters.AddWithValue("@CreatedBy", this.str_userkey);
                insertCmd.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);
                insertCmd.Parameters.AddWithValue("@ModifiedBy", this.str_userkey);

                DA.ExecuteNonQuery(insertCmd);

                //string usercheck = "select org_name,org_ccode from ICl_Organization where org_ccode='" + txt_clientcode.Text + "' ";
                string usercheck = "SELECT u.icl_email, q.id FROM ICL_Users u JOIN ICL_Organization o ON u.icl_userid = o.org_userid JOIN Quotes_Details q ON q.companyname = '" + str_Org + "' WHERE o.org_name = '" + str_Org + "'";
                DataTable dt_user = DA.GetDataTable(usercheck);
                if (dt_user.Rows.Count > 0)
                {
                    email_to = dt_user.Rows[0]["icl_email"].ToString();
                    Reference = "#ICL-FZQC-" + dt_user.Rows[0]["id"].ToString();
                }
                string userName = SC.Name;
                string message = "";
                string companyname = SC.Org_names;
                string str_support = "https://internationalcargologistics.com/";
                string emailContent = CF.Supportemail(email_to, "QuotesSupportMail", "Quote Creation Mail", Reference, userName, str_support, companyname, message);
                string script = @"
                        Swal.fire({
                title: '<div class=""custom-title"">Submitted</div>',
                html: `
                <div class=""custom-divider""></div>
                <p class=""custom-message"">Your data has been saved successfully</p>
            `,
                showConfirmButton: false,
                showCloseButton: true,
                allowOutsideClick: false,  // Prevents closing when clicking outside
                didOpen: () => {
                    document.body.classList.add(""blur-background""); // Apply blur
                },
                willClose: () => {
                    document.body.classList.remove(""blur-background""); // Remove blur
                     window.location.href = 'YourQuotes.aspx'; 
                },
                customClass: {
                    popup: ""custom-alert"",
                    title: ""custom-title"",
                    closeButton: ""custom-close"",
                }
            });

        ";

                // Register the script to execute after postback
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
            }

        }
        catch (Exception ex)
        {
            string script = @"
                        Swal.fire({
                title: '<div class=""custom-title"">error</div>',
                html: `
                <div class=""custom-divider""></div>
                <p class=""custom-message"">something wrong your data not saved!</p>
            `,
                showConfirmButton: false,
                showCloseButton: true,
                allowOutsideClick: false,  // Prevents closing when clicking outside
                didOpen: () => {
                    document.body.classList.add(""blur-background""); // Apply blur
                },
                willClose: () => {
                    document.body.classList.remove(""blur-background""); // Remove blur
                },
                customClass: {
                    popup: ""custom-alert"",
                    title: ""custom-title"",
                    closeButton: ""custom-close"",
                }
            });

        ";

            // Register the script to execute after postback
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

        }


    }
}