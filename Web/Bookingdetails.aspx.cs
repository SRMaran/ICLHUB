using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO.Compression;
using System.IO;
using System.Web.Services;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

public partial class Web_Bookingdetails : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    SessionCustom SC;
    CommonFunction CF;
    Eadaptor EA;
    string queryId = "";
    string userid = "";
    string Bookdecrpt = "";
    string containerNumber = "";
    string Decryption = "";
    string Conttype = "";
    string Pickupfrom = "";
    string Estimateddelivery = "";
    string Estimatepickup = "";
    string EstimatedDelivery = "";
    string Incoterm = "";
    string ContainerMode = "";
    string AdditionalTerms = "";
    string BookingConformed = "";
    string Originportcode = "";
    string Originportname = "";
    string Destinationportname  = "";
    string Destinationportcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.SC = new SessionCustom();
        this.CF = new CommonFunction();
        this.EA = new Eadaptor();
        userid = sc.Userid;
        if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
        {
            this.queryId = Request.QueryString["key"].ToString();
            Bookdecrpt = DE.Login(queryId);
            SC.headername = "Booking";
            SC.lablename = "Booking" + " <span style='color:#112560;font-size:18px'>" + " | " + Bookdecrpt + "</span>";
            if (!IsPostBack)
            {
                bookingno.Text = Bookdecrpt;
                this.getvalue();
                this.Bookgriddetails();
                this.loadbookdownload();

            }
            string Bookingid = Bookdecrpt;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetServerValue", "var bookingId = '" + Bookingid + "';", true);
        }
    }
    public static class FileHelper
    {
        public static readonly string BookingPath = @"E:\ICLHUBEdocument\ForwardingBooking\";  // ✅ Global File Path
    }
    private void getvalue()
    {
        string str_xmlcode = "C:\\ICL\\Booking.txt";
        string contents = "";
        string consolkey = Bookdecrpt;
        using (StreamReader streamReader = new StreamReader(str_xmlcode, Encoding.UTF8))
        {
            contents = streamReader.ReadToEnd();
            contents = contents.Replace("%%key%%", consolkey);
            string contentresponse = EA.HTTPPostXMLMessage(contents);

            MapXmlResponse(contentresponse);
        }
    }
    private void MapXmlResponse(string contentresponse)
    {
        try
        {
            DataColumnCollection columns;
            StringReader theReader = new StringReader(contentresponse);
            DataSet theDataSet = new DataSet();
            StringReader theReader1 = new StringReader(contentresponse);
            theDataSet.ReadXml(theReader1);
            int count = theDataSet.Tables.Count;

            if (theDataSet.Tables.Contains("Datasource"))
            {


                columns = theDataSet.Tables["ShipmentIncoTerm"].Columns;
                if (columns.Contains("Code"))
                {
                    Incoterm = theDataSet.Tables["ShipmentIncoTerm"].Rows[0]["Code"].ToString();
                    if (Incoterm == "")
                    {
                        Incoterm = "NA";
                    }
                }

                columns = theDataSet.Tables["ContainerMode"].Columns;
                if (columns.Contains("Code"))
                {
                    ContainerMode = theDataSet.Tables["ContainerMode"].Rows[0]["Code"].ToString();
                    if (ContainerMode == "")
                    {
                        ContainerMode = "NA";
                    }
                }

                columns = theDataSet.Tables["Shipment"].Columns;
                if (columns.Contains("AdditionalTerms"))
                {
                    AdditionalTerms = theDataSet.Tables["Shipment"].Rows[0]["AdditionalTerms"].ToString();
                    if (AdditionalTerms == "")
                    {
                        AdditionalTerms = "NA";
                    }
                }

                columns = theDataSet.Tables["Date"].Columns;
                if (columns.Contains("Value"))
                {
                    string BookingConformeds = theDataSet.Tables["Date"].Rows[0]["Value"].ToString();
                    DateTime estimatePickup = DateTime.Parse(BookingConformeds);
                    BookingConformed = estimatePickup.ToString("dd-MMM-yyyy");


                    if (Incoterm == "")
                    {
                        Incoterm = "NA";
                    }
                }



                columns = theDataSet.Tables["Container"].Columns;
                if (columns.Contains("ContainerNumber"))
                {
                    containerNumber = theDataSet.Tables["Container"].Rows[0]["ContainerNumber"].ToString();
                    if (containerNumber == "")
                    {
                        containerNumber = "NA";
                    }
                }
                columns = theDataSet.Tables["ContainerType"].Columns;
                if (columns.Contains("Code"))
                {
                    Conttype = theDataSet.Tables["ContainerType"].Rows[0]["Code"].ToString();

                    if (Conttype == "")
                    {
                        Conttype = "NA";
                    }
                    
                }

                columns = theDataSet.Tables["Shipment"].Columns;
                if (columns.Contains("GoodsDescription"))
                {
                    Decryption = theDataSet.Tables["Shipment"].Rows[0]["GoodsDescription"].ToString();

                    
                    if (Decryption == "")
                    {
                        Decryption = "NA";
                    }
                }
                columns = theDataSet.Tables["PortOfOrigin"].Columns;
                if (columns.Contains("Code"))
                {
                    Originportcode = theDataSet.Tables["PortOfOrigin"].Rows[0]["Code"].ToString();


                    if (Originportcode == "")
                    {
                        Originportcode = "NA";
                    }
                }
                columns = theDataSet.Tables["PortOfOrigin"].Columns;
                if (columns.Contains("Name"))
                {
                    Originportname = theDataSet.Tables["PortOfOrigin"].Rows[0]["Name"].ToString();


                    if (Originportname == "")
                    {
                        Originportname = "NA";
                    }
                }
                columns = theDataSet.Tables["PortOfDestination"].Columns;
                if (columns.Contains("Name"))
                {
                    Destinationportname = theDataSet.Tables["PortOfDestination"].Rows[0]["Name"].ToString();


                    if (Destinationportname == "")
                    {
                        Destinationportname = "NA";
                    }
                }
                columns = theDataSet.Tables["PortOfDestination"].Columns;
                if (columns.Contains("Code"))
                {
                    Destinationportcode = theDataSet.Tables["PortOfDestination"].Rows[0]["Code"].ToString();


                    if (Destinationportcode == "")
                    {
                        Destinationportcode = "NA";
                    }
                }
                columns = theDataSet.Tables["OrganizationAddress"].Columns;
                if (columns.Contains("AddressType"))
                {
                    var table = theDataSet.Tables["OrganizationAddress"];

                    if (table.Columns.Contains("AddressType") && table.Columns.Contains("Address1"))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            string addressType = row["AddressType"].ToString();

                            if (addressType == "ConsignorDocumentaryAddress")
                            {
                                Pickupfrom = row["Address1"].ToString();
                                if (Pickupfrom == "")
                                {
                                    Pickupfrom = "NA";
                                }
                            }
                            else if (addressType == "ConsigneeDocumentaryAddress")
                            {
                                Estimateddelivery = row["Address1"].ToString();
                                if (Estimateddelivery == "")
                                {
                                    Estimateddelivery = "NA";
                                }
                            }
                        }
                    }
                }
                columns = theDataSet.Tables["LocalProcessing"].Columns;
                if (columns.Contains("EstimatedPickup"))
                {
                   string Estimatepickups = theDataSet.Tables["LocalProcessing"].Rows[0]["EstimatedPickup"].ToString();
                    DateTime estimatePickup = DateTime.Parse(Estimatepickups);
                    Estimatepickup = estimatePickup.ToString("dd-MMM-yyyy");

                    string EstimatedDeliverys = theDataSet.Tables["LocalProcessing"].Rows[0]["EstimatedDelivery"].ToString();
                    DateTime estimateDelivery = DateTime.Parse(EstimatedDeliverys);
                    EstimatedDelivery = estimateDelivery.ToString("dd-MMM-yyyy");

                    if (Estimatepickup == "")
                    {
                        Estimatepickup = "NA";
                    }
                    if (EstimatedDelivery == "")
                    {
                        EstimatedDelivery = "NA";
                    }
                }


                //Estimateddelivery = theDataSet.Tables["Datasource"].Rows[0]["key"].ToString();
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error parsing XML: " + ex.Message);
        }
    }


    private void Bookgriddetails()
    {
        string str_bookingno = bookingno.Text;
        this.CF = new CommonFunction();
        string str_Bookgriddetails = "EXEC GetBookingdetails @BookingID='" + str_bookingno + "'";
        SqlCommand cmd = new SqlCommand(str_Bookgriddetails);
        DataTable DT_Booking = da.GetDataTable(cmd);

        if (DT_Booking.Rows.Count > 0)
        {
            DT_Booking.Columns.Add("Incoterm", typeof(string));
            DT_Booking.Columns.Add("ContainerMode", typeof(string));
            DT_Booking.Columns.Add("AdditionalTerms", typeof(string));
            DT_Booking.Columns.Add("BookingConformed", typeof(string));
            DT_Booking.Columns.Add("Container", typeof(string));
            DT_Booking.Columns.Add("Conttype", typeof(string));
            DT_Booking.Columns.Add("ContDescription", typeof(string));
            DT_Booking.Columns.Add("DeliveryPickupfrom", typeof(string));
            DT_Booking.Columns.Add("Deliverydestination", typeof(string));
            DT_Booking.Columns.Add("Estimatepickup", typeof(string));
            DT_Booking.Columns.Add("EstimatedDelivery", typeof(string));
            DT_Booking.Columns.Add("Originportcode", typeof(string));
            DT_Booking.Columns.Add("Originportname", typeof(string));
            DT_Booking.Columns.Add("Destinationportcode", typeof(string)); 
            DT_Booking.Columns.Add("Destinationportname", typeof(string)); 
            foreach (DataRow row in DT_Booking.Rows)
            {
                // Decrypt container ID
                row["Incoterm"] = Incoterm;
                row["ContainerMode"] = ContainerMode;
                row["AdditionalTerms"] = AdditionalTerms;
                row["BookingConformed"] = BookingConformed;
                row["Container"] = containerNumber;
                row["Conttype"] = Conttype;
                row["ContDescription"] = Decryption;
                row["DeliveryPickupfrom"] = Pickupfrom;
                row["Deliverydestination"] = Estimateddelivery;
                row["Estimatepickup"] = Estimatepickup;
                row["EstimatedDelivery"] = EstimatedDelivery;
                row["Originportcode"] = Originportcode;
                row["Originportname"] = Originportname;
                row["Destinationportcode"] = Destinationportcode;
                row["Destinationportname"] = Destinationportname;
            }
            DataSet ds = new DataSet();
            ds.Merge(DT_Booking);
            string str_bookingid = DT_Booking.Rows[0]["eb_bookingid"].ToString();
            this.PH.LoadGridItem(ds, PH_Bookorder, "Bookorder.txt", "");
            this.PH.LoadGridItem(ds, PH_Bookdelivery, "Bookdelivery.txt", "");
            this.PH.LoadGridItem(ds, PH_Bookshipdetails, "Bookshipdetails.txt", "");
            this.PH.LoadGridItem(ds, PH_Bookgooddetails, "Bookgoodsdetails.txt", "");
        }
    }

    [WebMethod]
    public static string SupportMessage(string bookingId, string message)
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
            string query = "select eb_bookingid from eb_profilereport where eb_bookingid='" + bookingId + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable DT_Booking = DA.GetDataTable(cmd);
            if (DT_Booking.Rows.Count > 0)
            {
                string str_custom = DT_Booking.Rows[0]["eb_bookingid"].ToString();
                string userEmail = SC.username;
                string userName = SC.Name;
                string companyname = SC.Org_names;
                string str_support = "https://internationalcargologistics.com/";

                string email_fun = CF.Supportemail(userEmail, "CustomSupportMail", "Custom Supporting Mail", str_custom, userName, str_support, companyname, message);
                return "Booking SupportMail sent successfully.";
            }
            else
            {
                return "Error: Booking not found.";
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
            string str_doc = ddl_document.SelectedValue;
            string str_bookingno = bookingno.Text;
            string str_documentname = ddl_document.SelectedItem.Text;
            this.showuploadfile();
            if (!fu_fileupload.HasFile)
            {
                this.Bookgriddetails();
                this.loadbookdownload();
                hfPopupVisible.Value = "true";
                lb_errorfile.Visible = true;
                lb_errorfile.ForeColor = System.Drawing.Color.Red;
                lb_errorfile.Text = "Please upload document";
                return;
            }
            if (str_doc == "0")
            {
                this.Bookgriddetails();
                this.loadbookdownload();
                hfPopupVisible.Value = "true";
                lb_error.Visible = true;
                lb_error.Text = "Please select document";
                lb_error.ForeColor = System.Drawing.Color.Red;
                return;
            }
            lb_errorfile.Visible = false;
            lb_error.Visible = false;
            string originalFilename = Path.GetFileName(fu_fileupload.FileName);
             string uniqueFileName = str_bookingno + '_' + originalFilename;
            string savePath = Server.MapPath("~/Booking/") + uniqueFileName;
            fu_fileupload.SaveAs(savePath);
            string query = "INSERT INTO ICL_BookingDocument (bd_documenttype, bd_document, bd_bookingid, bd_createdby) VALUES (@bd_documenttype, @bd_document, @bd_bookingid, @bd_createdby)";
            DataAccess DA = new DataAccess();
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@bd_documenttype", ddl_document.SelectedValue);
            cmd.Parameters.AddWithValue("@bd_document", uniqueFileName);
            cmd.Parameters.AddWithValue("@bd_bookingid", str_bookingno);
            cmd.Parameters.AddWithValue("@bd_createdby", userid);
            DA.ExecuteNonQuery(cmd);
            this.Bookgriddetails();
            this.loadbookdownload();
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
        this.loadbookdownload();
        this.getvalue();
        this.showuploadfile();
        this.Bookgriddetails();
      
    }
    private void showuploadfile()
    {
        string str_document = ddl_document.SelectedValue;
        string str_bookingno = bookingno.Text;
        String str_Doc = "SELECT ROW_NUMBER() OVER (ORDER BY bd_bookingid) AS SerialNo,bd_documenttype,bd_document,bd_bookingid from ICL_BookingDocument  WHERE bd_bookingid='" + str_bookingno + "' and bd_documenttype='" + str_document + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Showuploadfile, "BookShowuploaddoc.txt", "");
        }
    }
    //upload file to download file
    [WebMethod]
    public static string UploadDownloadDocument(string bookingId, string fileName)
    {
        try
        {
            string query = "select bd_documenttype,bd_document,bd_bookingid,bd_createdby from ICL_BookingDocument where bd_bookingid=@bd_bookingid";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@bd_bookingid", bookingId);
            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["bd_document"].ToString() == fileName)
                    {
                        string filePath = "/Booking/" + fileName; // Relative URL
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
                return "Error: No document found for this Booking ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    private void loadbookdownload()
    {
        string str_bookingno = bookingno.Text;
        String str_Doc = "SELECT JSED_FileName,JSED_ShipmentNumber,JSED_DocType,JSED_Description,JSED_Type from JobShipmentEdoc Where JSED_ShipmentNumber='" + str_bookingno + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {

            this.PH.LoadGridItem(ds, PH_bookdoc, "docshipment.txt", "");

        }
    }
    //Button to download all file


    protected void btn_download_Click(object sender, EventArgs e)
    {
        string str_bookingno = bookingno.Text;
       
        string query = "SELECT JSED_FileName, JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber ";
        SqlCommand cmd = new SqlCommand(query);
        cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", str_bookingno);
        DataTable dt_table = da.GetDataTable(cmd);
        if (dt_table.Rows.Count > 0)
        {
            string zipFileName = "Documents_" + str_bookingno + ".zip";
            string zipFilePath = @"E:\ICLHUBEdocument\ForwardingBooking\" + str_bookingno + @"\" + zipFileName;

            using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (DataRow row in dt_table.Rows)
                {
                    string fileName = row["JSED_FileName"].ToString();
                    string str_type = row["JSED_DocType"].ToString();
                    string Filetype = str_type + "." + fileName.ToLower();
                    string filePath = @"E:\ICLHUBEdocument\ForwardingBooking\" + str_bookingno + @"\" + Filetype;

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
            this.Bookgriddetails();
            this.loadbookdownload();
        }
        else
        {
            Response.Write("<script>alert('No files available for download.');</script>");
        }
    }

    //icon to download file
    [WebMethod]
    public static string DownloadDocument(string bookingId, string fileName)
    {
        try
        {
            string query = "SELECT JSED_FileName, JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber AND JSED_FileName = @JSED_FileName";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", bookingId);
            cmd.Parameters.AddWithValue("@JSED_FileName", fileName);
            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string fileName1 = row["JSED_FileName"].ToString();
                string str_type = row["JSED_DocType"].ToString();
                string str_filenametype = str_type + "." + fileName1.ToLower();

                string filePath = Path.Combine(FileHelper.BookingPath, bookingId, str_filenametype);


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
                return "Error: No document found for this booking ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }


}
