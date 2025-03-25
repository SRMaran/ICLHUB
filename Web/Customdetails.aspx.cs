using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Text;

public partial class Web_Customdetails : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    SessionCustom SC;
    CommonFunction CF;
    string queryId = "";
    string Custdecrpt = "";
    string userid = "";
    string dep = "";
    Eadaptor EA;
    Jsonvalue EV;


    string Str_Orderref = "";
    string Str_Date = "";
    string Str_OrganizationAddress = "";
    string Str_Shipment = "";
    string Str_ShipmentVoyage = "";
    string Str_CustomsContainerMode = "";
    string Str_AdditionalBill = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.EA = new Eadaptor();
        this.EV = new Jsonvalue();
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.SC = new SessionCustom();
        this.CF = new CommonFunction();
        this.userid = sc.Userid;


        if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
        {
            string sharelink = Request.QueryString["key"].ToString();
            string[] linksplit = sharelink.Split(',');

            string jobnumber = linksplit[0].Trim();
            string Depart = linksplit[1].Trim();

            Custdecrpt = DE.Login(jobnumber);
            dep = Depart;
            

            if (!IsPostBack)
            {
                sc.headername = "Custom";
                customno.Text = Custdecrpt;
                this.getvalue();
                this.Custgriddetails();
                this.documentdownload();
               
            }
        }
        string customid = Custdecrpt;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetServerValue", "var customId = '" + customid + "';", true);
        SC.lablename = "Customs" + " <span style='color:#112560;font-size:18px'>" + " | " + Custdecrpt + "</span>";
    }
    public static class FileHelper
    {
        public static readonly string CustomPath = @"E:\ICLHUBEdocument\CustomsDeclaration\";  // ✅ Global File Path
    }
   
    private void getvalue()
    {
        string str_xmlcode = "C:\\ICL\\CustomsDeclaration.txt";
        string contents = "";

        string consolkey = Custdecrpt;

        using (StreamReader streamReader = new StreamReader(str_xmlcode, Encoding.UTF8))
        {
            contents = streamReader.ReadToEnd();
            contents = contents.Replace("%%key%%", consolkey);
            string contentresponse = this.EA.HTTPPostXMLMessage(contents);
            XDocument xmlDoc = XDocument.Parse(contentresponse);

            

            // Read XML content
            StringReader theReader = new StringReader(contentresponse);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            // Ensure DataSet contains required tables
            if (theDataSet.Tables.Contains("Datasource"))
            {
                var columns = theDataSet.Tables["Datasource"].Columns;
                if (columns.Contains("key"))
                {
                    Str_Orderref = theDataSet.Tables["Datasource"].Rows[0]["key"].ToString();
                    if (Str_Orderref == "")
                    {
                        Str_Orderref = "NA";
                    }
                }
            }

           

            if (theDataSet.Tables.Contains("OrganizationAddress"))
            {
                var columns = theDataSet.Tables["OrganizationAddress"].Columns;
                if (columns.Contains("Declarant"))
                {
                    Str_OrganizationAddress = theDataSet.Tables["OrganizationAddress"].Rows[0]["Declarant"].ToString();
                    if (Str_OrganizationAddress == "")
                    {
                        Str_OrganizationAddress = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("Shipment"))
            {
                var columns = theDataSet.Tables["Shipment"].Columns;
                if (columns.Contains("VesselName"))
                {
                    Str_Shipment = theDataSet.Tables["Shipment"].Rows[0]["VesselName"].ToString();
                    Str_ShipmentVoyage = theDataSet.Tables["Shipment"].Rows[0]["VoyageFlightNo"].ToString();

                    if (Str_Shipment == "")
                    {
                        Str_Shipment = "NA";
                    }
                    if (Str_ShipmentVoyage == "")
                    {
                        Str_ShipmentVoyage = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("CustomsContainerMode"))
            {
                var columns = theDataSet.Tables["CustomsContainerMode"].Columns;
                if (columns.Contains("Description"))
                {
                    Str_CustomsContainerMode = theDataSet.Tables["CustomsContainerMode"].Rows[0]["Description"].ToString();

                    if (Str_CustomsContainerMode == "")
                    {
                        Str_CustomsContainerMode = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("AdditionalBill"))
            {
                var columns = theDataSet.Tables["AdditionalBill"].Columns;
                if (columns.Contains("BillNumber"))
                {
                    Str_AdditionalBill = theDataSet.Tables["AdditionalBill"].Rows[0]["BillNumber"].ToString();
                    if (Str_AdditionalBill == "")
                    {
                        Str_AdditionalBill = "NA";
                    }
                }
            } 
            
        }
    }
    private void Custgriddetails()
    {
        string str_customno = customno.Text;
        string str_Custgriddetails = "";
        if (dep == "Import")
        {
            str_Custgriddetails = "EXEC Getcustomdetailsimport @bookingId='" + str_customno + "'";
        }
        else
        {
            str_Custgriddetails = "EXEC Getcustomdetailexport @bookingId='" + str_customno + "'";
        }
        SqlCommand cmd = new SqlCommand(str_Custgriddetails);
        DataTable DT_Booking = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        if (DT_Booking.Rows.Count > 0)
        {
            DT_Booking.Columns.Add("Orderref", typeof(string));
            DT_Booking.Columns.Add("OrganizationAddress", typeof(string));
            DT_Booking.Columns.Add("Shipment", typeof(string));
            DT_Booking.Columns.Add("ShipmentVoyage", typeof(string));
            DT_Booking.Columns.Add("CustomsContainerMode", typeof(string));
            DT_Booking.Columns.Add("AdditionalBill", typeof(string));

            


            if (DT_Booking.Rows.Count > 0)
            {
                string str_customid = DT_Booking.Rows[0]["gbi_job"].ToString();
                foreach (DataRow row in DT_Booking.Rows)
                {
                    row["Orderref"] = Str_Orderref;
                    row["OrganizationAddress"] = Str_OrganizationAddress;
                    row["Shipment"] = Str_Shipment;
                    row["ShipmentVoyage"] = Str_ShipmentVoyage;
                    row["CustomsContainerMode"] = Str_CustomsContainerMode;
                    row["AdditionalBill"] = Str_AdditionalBill;

                    
                }
                ds.Merge(DT_Booking);

                this.PH.LoadGridItem(ds, PH_Custorder, "Custorder.txt", "");
                this.PH.LoadGridItem(ds, PH_Custdelivery, "Custdelivery.txt", "");
                this.PH.LoadGridItem(ds, PH_Custshipdetails, "Custshipdetails.txt", "");
                this.PH.LoadGridItem(ds, PH_Custgooddetails, "Custgoodsdetails.txt", "");
            }
            ds.Merge(DT_Booking);
        }
    }



    [WebMethod]
    public static string SupportMessage(string customId, string message)
    {
        try
        {
            CommonFunction CF = new CommonFunction();
            SessionCustom SC = new SessionCustom();
            DataAccess DA = new DataAccess();
            if (string.IsNullOrEmpty(message))
            {
                return "Error: Please enter a message.";
            }
            string query = "Select distinct gbi_job from cd_gbimportreport where  gbi_job='" + customId + "' union all Select distinct gbe_job from cd_gbexportreport where  gbe_job='" + customId + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable DT_Custom = DA.GetDataTable(cmd);
            if (DT_Custom.Rows.Count > 0)
            {
                string str_custom = DT_Custom.Rows[0]["gbi_job"].ToString();
                string userEmail = SC.username;
                string userName = SC.Name;
                string companyname = SC.Org_names;

                string str_support = "https://internationalcargologistics.com/";

                string email_fun = CF.Supportemail(userEmail, "CustomSupportMail", "Custom Supporting Mail", str_custom, userName, str_support, companyname, message);
                return "Custom SupportMail sent successfully.";
            }
            else
            {
                return "Error: Custom not found.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            string str_customno = customno.Text;
            string str_doc = ddl_document.SelectedValue;

            this.showuploadfile();
            if (!fu_fileupload.HasFile)
            {
                this.Custgriddetails();
                this.documentdownload();
                hfPopupVisible.Value = "true";
                lb_errorfile.Visible = true;
                lb_errorfile.ForeColor = System.Drawing.Color.Red;
                lb_errorfile.Text = "Please upload document";
                return;
            }
            if (str_doc == "0")
            {
                this.Custgriddetails();
                this.documentdownload();
                hfPopupVisible.Value = "true";
                lb_error.Visible = true;
                lb_error.Text = "Please select document";
                lb_error.ForeColor = System.Drawing.Color.Red;
                return;
            }
            lb_errorfile.Visible = false;
            lb_error.Visible = false;
            string originalFilename = Path.GetFileName(fu_fileupload.FileName);
            string uniqueFileName = str_customno + '_' + originalFilename;
            string savePath = Server.MapPath("~/Custom/") + uniqueFileName;
            fu_fileupload.SaveAs(savePath);
            string query = "INSERT INTO ICL_CustomDocument (cd_documenttype,cd_document,cd_customid,cd_createdby) VALUES (@cd_documenttype,@cd_document,@cd_customid,@cd_createdby)";
            DataAccess DA = new DataAccess();
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@cd_documenttype", ddl_document.SelectedValue);
            cmd.Parameters.AddWithValue("@cd_document", uniqueFileName);
            cmd.Parameters.AddWithValue("@cd_customid", str_customno);
            cmd.Parameters.AddWithValue("@cd_createdby", userid);
            DA.ExecuteNonQuery(cmd);

            this.Custgriddetails();
            this.documentdownload();
            hfPopupVisible.Value = "False";
            ddl_document.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('An error occurred while uploading the document';);</script>");
        }
    }
  
    protected void ddl_document_SelectedIndexChanged(object sender, EventArgs e)
    {
        lb_errorfile.Visible = false;
        lb_error.Visible = false;
        hfPopupVisible.Value = "true";
        this.showuploadfile();
        this.getvalue();
        this.Custgriddetails();
        this.documentdownload();

      
    }
    private void showuploadfile()
    {
        string str_document = ddl_document.SelectedValue;
        string str_customno = customno.Text;
        String str_Doc = "SELECT ROW_NUMBER() OVER (ORDER BY cd_customid) AS SerialNo,cd_documenttype,cd_document,cd_customid  from ICL_CustomDocument WHERE cd_customid='" + str_customno + "' and cd_documenttype='" + str_document + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Showuploadfile, "CustomShowuploaddoc.txt", "");
        }
    }

    //upload file to download file
    [WebMethod]
    public static string UploadDownloadDocument(string customId, string fileName)
    {
        try
        {
            string query = "select cd_documenttype,cd_document,cd_customid,cd_createdby  from ICL_CustomDocument where cd_customid=@cd_customid";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@cd_customid", customId);
            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["cd_document"].ToString() == fileName)
                    {
                        string filePath = "/Custom/" + fileName; // Relative URL
                        string serverPath = HttpContext.Current.Server.MapPath(filePath);
                        if (File.Exists(serverPath))
                        {
                            return filePath; // Return file URL
                        }
                    }
                }
                return "Error: File name does not match.";
            }
            else
            {
                return "Error: No document found for this Custom ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

   
    //icon to download file
    [WebMethod]
    public static string DownloadDocument(string customId, string fileName)
    {
        try
        {
            // ✅ Database query to find the file
            string query = "SELECT JSED_FileName, JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber AND JSED_FileName = @JSED_FileName";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", customId);
            cmd.Parameters.AddWithValue("@JSED_FileName", fileName);

            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string fileName1 = row["JSED_FileName"].ToString();
                string str_type = row["JSED_DocType"].ToString();
                string str_filenametype = str_type + "." + fileName1.ToLower();

                string filePath = Path.Combine(FileHelper.CustomPath, customId, str_filenametype);
               

                if (!File.Exists(filePath))
                {
                    return "Error: File not found on the server.";
                }

                // Read file as bytes
                byte[] fileBytes = File.ReadAllBytes(filePath);
                string base64File = Convert.ToBase64String(fileBytes);

                // Return Base64 encoded string
                return base64File;
            }
            else
            {
                return "Error: No document found for this Custom ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
    private void documentdownload()
    {
        string str_customno = customno.Text;
        String str_Doc = "SELECT JSED_FileName,JSED_ShipmentNumber,JSED_DocType,JSED_Description,JSED_Type from JobShipmentEdoc Where JSED_ShipmentNumber='" + str_customno + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {

            this.PH.LoadGridItem(ds, PH_customdoc, "docshipment.txt", "");
        }
    }

    protected void btn_download_Click(object sender, EventArgs e)
    {
        string str_customno = customno.Text;
        string query = "SELECT JSED_FileName,JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber";
        SqlCommand cmd = new SqlCommand(query);
        cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", str_customno);
        DataTable dt_table = da.GetDataTable(cmd);
        if (dt_table.Rows.Count > 0)
        {
            string zipFileName = "Documents_" + str_customno + ".zip";
            string zipFilePath = @"E:\ICLHUBEdocument\CustomsDeclaration\" + str_customno + @"\" + zipFileName;

            using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (DataRow row in dt_table.Rows)
                {
                    string fileName = row["JSED_FileName"].ToString();
                    string str_type = row["JSED_DocType"].ToString();
                    string Filetype = str_type + "." + fileName.ToLower();
                    string filePath = @"E:\ICLHUBEdocument\CustomsDeclaration\" + str_customno + @"\" + Filetype;

                    if (File.Exists(filePath))
                    {
                        ZipArchiveEntry entry = zip.CreateEntry(fileName);
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        using (Stream entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }
            }

            // ✅ Return ZIP file for download
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + zipFileName);
            Response.TransmitFile(zipFilePath);
            Response.End();
            this.showuploadfile();
            this.getvalue();
            this.Custgriddetails();
            this.documentdownload();
        }
        else
        {
            Response.Write("<script>alert('No files available for download.');</script>");
        }

    }

}



