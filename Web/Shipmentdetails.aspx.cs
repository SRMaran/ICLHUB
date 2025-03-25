using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.IO.Compression;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Web.Services;
using System.Text;
using System.Net.NetworkInformation;
using System.Security.Permissions;


public partial class Web_Shipmentdetails : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    SessionCustom SC;
    CommonFunction CF;
    string queryId = "";
    string userid = "";
    string shipmentdecrpt = "";
    Eadaptor EA;
    string TransportMode = "";
    string ShipmentType = "";
    string lastmilestonedec = "";
    string Pickupfrom = "";
    string Destinationdelivery = "";
    string Estimatepickup = "";
    string EstimatedDelivery = "";
    string Packquantity = "";
    string IncoTermvalue = "";
    string str_Shipment = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        EA = new Eadaptor();
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.SC = new SessionCustom();
        this.CF = new CommonFunction();
        userid = sc.Userid;
        if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
        {
            this.queryId = Request.QueryString["key"].ToString();
            if (!IsPostBack)
            {
                SC.headername = "Shipments";
                shipmentdecrpt = DE.Login(queryId);
                shipNo.Text = shipmentdecrpt;
                this.getvalue();
                this.shipmentgriddetails();
                this.containergriddetails();
                this.loadshipdownload();
                this.mapdetails(shipmentdecrpt);

            }

        }
        string shipmentid = shipmentdecrpt;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetServerValue", "var shipmentId = '" + shipmentid + "';", true);
        SC.lablename = "Shipments" + " <span style='color:#112560;font-size:18px'>" + " | " + shipmentdecrpt + "</span>";
    }
    public static class FileHelper
    {
        public static readonly string ShipmentPath = @"E:\ICLHUBEdocument\ForwardingShipment\";  // ✅ Global File Path
    }
    private void getvalue()
    {
        string str_xmlcode = "C:\\ICL\\Shipment.txt";
        string contents = "";

        string consolkey = shipmentdecrpt;
        //string consolkey = "SPIMSI00276960";

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
            string PackquantityValue = "";
            string Packcode = "";
            string TransportCode = "";
            string TransportDesc = "";
            string ShipmentTypedesc = "";
            string ShipmentTypecode = "";
            DataColumnCollection columns;
            StringReader theReader = new StringReader(contentresponse);
            DataSet theDataSet = new DataSet();
            StringReader theReader1 = new StringReader(contentresponse);
            theDataSet.ReadXml(theReader1);
            int count = theDataSet.Tables.Count;
            if (theDataSet.Tables.Contains("Datasource"))
            {
                columns = theDataSet.Tables["TransportMode"].Columns;
                if (columns.Contains("Code"))
                {
                    TransportCode = theDataSet.Tables["TransportMode"].Rows[0]["Code"].ToString();
                    TransportDesc = theDataSet.Tables["TransportMode"].Rows[0]["Description"].ToString();
                    TransportMode = TransportCode + "(" + TransportDesc + ")";
                    if (TransportMode == "()")
                    {
                        TransportMode = "NA";
                    }
                }
                columns = theDataSet.Tables["ShipmentType"].Columns;
                if (columns.Contains("Code"))
                {
                    ShipmentTypecode = theDataSet.Tables["ShipmentType"].Rows[0]["Code"].ToString();
                    ShipmentTypedesc = theDataSet.Tables["ShipmentType"].Rows[0]["Description"].ToString();
                    ShipmentType = ShipmentTypecode + "(" + ShipmentTypedesc + ")";
                    if (ShipmentType == "()")
                    {
                        ShipmentType = "NA";
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
                                Destinationdelivery = row["Address1"].ToString();
                                if (Destinationdelivery == "")
                                {
                                    Destinationdelivery = "NA";
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
                    Estimatepickup = estimatePickup.ToString("dd-MMM-yyyy HH:mm");

                    string EstimatedDeliverys = theDataSet.Tables["LocalProcessing"].Rows[0]["EstimatedDelivery"].ToString();
                    DateTime estimateDeliverys = DateTime.Parse(EstimatedDeliverys);
                    EstimatedDelivery = estimateDeliverys.ToString("dd-MMM-yyyy HH:mm");
                    if (Estimatepickup == "")
                    {
                        Estimatepickup = "NA";
                    }
                    if (EstimatedDelivery == "")
                    {
                        EstimatedDelivery = "NA";
                    }
                }
                columns = theDataSet.Tables["Milestone"].Columns;
                if (columns.Contains("Description"))
                {
                    lastmilestonedec = theDataSet.Tables["Milestone"].Rows[0]["Description"].ToString();
                    if (lastmilestonedec == "")
                    {
                        lastmilestonedec = "NA";
                    }
                }
                columns = theDataSet.Tables["PackingLine"].Columns;
                if (columns.Contains("PackQty"))
                {
                    PackquantityValue = theDataSet.Tables["PackingLine"].Rows[0]["PackQty"].ToString();
                }
                columns = theDataSet.Tables["PackType"].Columns;
                if (columns.Contains("Code"))
                {
                    Packcode = theDataSet.Tables["PackType"].Rows[0]["Code"].ToString();
                }
                Packquantity = PackquantityValue + "(" + Packcode + ")";
                if (ShipmentType == "()")
                {
                    ShipmentType = "NA";
                }
              

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing XML: " + ex.Message);
        }
    }
    private void mapdetails(string shipmentdecrpt)
    {


        string Shipper = "select distinct top 1  b.icfd_container from shipmentProfilereport a left join swl_icfdreport b on a.spr_shipmentid=b.icfd_jobref where a.spr_shipmentid='" + shipmentdecrpt + "' and a.spr_modifiedon  is null";
        SqlCommand mapliveShipper = new SqlCommand(Shipper);
        DataTable dt_container = this.da.GetDataTable(mapliveShipper);

        if (dt_container.Rows.Count > 0)
        {
            string containernumber = "";
            string startLat = "";
            string startLng = "";
            string endtLat = "";
            string endtLng = "";
            string livelong = "";
            string livelat = "";
            string contain = "";

            DataTable dt_month = new DataTable();
            DataTable st_live = new DataTable();
            DataTable end_live = new DataTable();
            DataTable dt_live = new DataTable();
            DataTable dt_livemark = new DataTable();
            List<string> latimarkList = new List<string>();
            List<string> logtmarkList = new List<string>();


            containernumber = dt_container.Rows[0]["icfd_container"].ToString();


            string startrecod = "select top 1 latlng_Key,route_latitude as lat,route_longitude as lng from ICLRoute_latlng where containernumber='" + containernumber + "' order by latlng_Key asc";
            SqlCommand startlati = new SqlCommand(startrecod);
            st_live = da.GetDataTable(startlati);
            if (st_live.Rows.Count > 0)
            {
                startLat = st_live.Rows[0]["lat"].ToString();
                startLng = st_live.Rows[0]["lng"].ToString();

                string endrecod = "select top 1 latlng_Key,route_latitude as lat,route_longitude as lng from ICLRoute_latlng where containernumber='" + containernumber + "' order by latlng_Key desc";
                SqlCommand endlati = new SqlCommand(endrecod);
                end_live = da.GetDataTable(endlati);
                if (end_live.Rows.Count > 0)
                {
                    endtLat = end_live.Rows[0]["lat"].ToString();
                    endtLng = end_live.Rows[0]["lng"].ToString();


                    string liverecod = "select top 1 route_livelatitude as lat,route_livelongitude as lng from ICLRoute_livelocation where containernumber='" + containernumber + "' order by createdon desc";
                    SqlCommand livelati = new SqlCommand(liverecod);
                    dt_live = da.GetDataTable(livelati);
                    if (dt_live.Rows.Count > 0)
                    {

                        livelat = dt_live.Rows[0]["lat"].ToString();
                        livelong = dt_live.Rows[0]["lng"].ToString();


                    }
                    string livermark = "select latlng_Key,route_latitude as lat,route_longitude as lng from ICLRoute_latlng where containernumber='" + containernumber + "' order by latlng_Key asc";
                    SqlCommand livelatimark = new SqlCommand(livermark);
                    dt_livemark = da.GetDataTable(livelatimark);
                    var marklivedata = new Dictionary<string, List<string>>();

                    if (dt_livemark.Rows.Count > 0)
                    {
                        for (var c = 0; c < dt_livemark.Rows.Count; c++)
                        {
                            string Lat = dt_livemark.Rows[c]["lat"].ToString();
                            string Lng = dt_livemark.Rows[c]["lng"].ToString();

                            var dataendmarklivedata = new System.Collections.Generic.Dictionary<string, string>();
                            dataendmarklivedata.Add("lat", Lat);
                            dataendmarklivedata.Add("lng", Lng);
                            string jsonstlive = JsonConvert.SerializeObject(dataendmarklivedata);
                            latimarkList.Add(jsonstlive);
                        }
                    }
                }



                var datast = new System.Collections.Generic.Dictionary<string, string>();
                datast.Add("lat", startLat);
                datast.Add("lng", startLng);
                string jsonst = JsonConvert.SerializeObject(datast);

                var dataliv = new System.Collections.Generic.Dictionary<string, string>();
                dataliv.Add("lat", endtLat);
                dataliv.Add("lng", endtLng);
                string jsonend = JsonConvert.SerializeObject(dataliv);

                var livdata = new System.Collections.Generic.Dictionary<string, string>();
                livdata.Add("lat", livelat);
                livdata.Add("lng", livelong);
                string jsonlive = JsonConvert.SerializeObject(livdata);

                var data = new System.Collections.Generic.Dictionary<string, string>();
                data.Add("container_id", containernumber);
                data.Add("start_point", jsonst);
                data.Add("live_point", jsonlive);
                data.Add("end_point", jsonend);

                string trackdata = JsonConvert.SerializeObject(latimarkList);
                data.Add("marklinedata", trackdata);

                string json = JsonConvert.SerializeObject(data);


                mapdetailsdata.Text = HttpUtility.JavaScriptStringEncode(json);
            }
            else
            {
                string monthlabledate = "[]";
                mapdetailsdata.Text = monthlabledate;
            }
        }
        else
        {
            string monthlabledate = "[]";
            mapdetailsdata.Text = monthlabledate;
        }
    }

    private void containergriddetails()
    {
        string shipmentNo = shipNo.Text;
        string str_shipmentgriddetails = "select distinct icfd_masterref,icfd_container,icfd_contmode,icfd_conttype,icfd_jobref,icfd_jobtype, CASE  WHEN icfd_eta = CAST('1900-01-01 00:00:00.000' AS DATETIME)  THEN 'NA'  ELSE FORMAT(CAST(icfd_eta AS DATETIME), 'dd-MMM-yyyy')END as  icfd_eta,  CASE  WHEN icfd_estimateddeliver = CAST('1900-01-01 00:00:00.000' AS DATETIME)  THEN 'NA'  ELSE FORMAT(CAST(icfd_estimateddeliver AS DATETIME), 'dd-MMM-yyyy HH:mm')END as   icfd_estimateddeliver,  CASE  WHEN icfd_actualdeliver = CAST('1900-01-01 00:00:00.000' AS DATETIME)  THEN 'NA'  ELSE FORMAT(CAST(icfd_actualdeliver AS DATETIME), 'dd-MMM-yyyy HH:mm')END AS icfd_actualdeliver,  CASE  WHEN icfd_emptyreturned = CAST('1900-01-01 00:00:00.000' AS DATETIME)  THEN 'NA'  ELSE FORMAT(CAST(icfd_emptyreturned AS DATETIME), 'dd-MMM-yyyy')END AS icfd_emptyreturned FROM dbo.swl_icfdreport  where  icfd_jobref='" + shipmentNo + "'  ";
        SqlCommand cmd = new SqlCommand(str_shipmentgriddetails);
        DataTable dt_shipment = da.GetDataTable(cmd);
        if (dt_shipment.Rows.Count > 0)
        {
            dt_shipment.Columns.Add("containerencrptid", typeof(string));
            dt_shipment.Columns.Add("ShipmentNo", typeof(string));
            foreach (DataRow row in dt_shipment.Rows)
            {
                string containerid = row["icfd_container"].ToString();
                string icfd_jobref = row["icfd_jobref"].ToString();
                string containerdecrpt = DE.Register(containerid);
                string icfd_jobrefdecrpt = DE.Register(icfd_jobref);
                row["containerencrptid"] = containerdecrpt;
                row["ShipmentNo"] = icfd_jobrefdecrpt;
            }
            DataSet ds = new DataSet();
            ds.Merge(dt_shipment);
            this.PH.LoadGridItem(ds, PH_container, "shipcontainerdetails.txt", "");
        }
    }
    private void shipmentgriddetails()
    {
        string shipmentNo = shipNo.Text;
        string str_mapping = "select distinct a.description, CASE  WHEN a.date = CAST('1900-01-01 00:00:00.000' AS DATETIME)  THEN 'NA'  ELSE FORMAT(CAST(a.date AS DATETIME), 'dd-MMM-yyyy')  END as date,b.name from ICL_Events a left outer join ICL_Locations b on  a.location=b.location_key left outer join swl_icfdreport  c on a.containerNumber=c.icfd_container left outer join shipmentProfilereport d on c.icfd_jobref =d.spr_shipmentid where  c.icfd_jobref='" + shipmentNo + "' ";
        SqlCommand cmd = new SqlCommand(str_mapping);
        DataTable dt_mapdetails = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_mapdetails);
        if (dt_mapdetails.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Mapdetails, "Mapdetails.txt", "");
        }
        string str_shipmentgriddetails = "EXEC GetShipmentdetails @shipmentid= '" + shipmentNo + "' ";
        SqlCommand cmd1 = new SqlCommand(str_shipmentgriddetails);
        DataTable dt_shipment = da.GetDataTable(cmd1);

        if (dt_shipment.Rows.Count > 0)
        {

            dt_shipment.Columns.Add("TransportMode", typeof(string));
            dt_shipment.Columns.Add("ShipmentType", typeof(string));
            dt_shipment.Columns.Add("Pickupfrom", typeof(string));
            dt_shipment.Columns.Add("Destinationdelivery", typeof(string));
            dt_shipment.Columns.Add("Estimatepickup", typeof(string));
            dt_shipment.Columns.Add("EstimatedDelivery", typeof(string));
            dt_shipment.Columns.Add("Packquantity", typeof(string));
            dt_shipment.Columns.Add("lastmilestonedec", typeof(string));
            dt_shipment.Columns.Add("IncoTermvalue", typeof(string));
            foreach (DataRow row in dt_shipment.Rows)
            {
                // Decrypt container ID
                row["TransportMode"] = TransportMode;
                row["ShipmentType"] = ShipmentType;
                row["Pickupfrom"] = Pickupfrom;
                row["Destinationdelivery"] = Destinationdelivery;
                row["Estimatepickup"] = Estimatepickup;
                row["EstimatedDelivery"] = EstimatedDelivery;
                row["Packquantity"] = Packquantity;
                row["lastmilestonedec"] = lastmilestonedec;
                row["IncoTermvalue"] = IncoTermvalue;

            }
            DataSet ds1 = new DataSet();
            ds1.Merge(dt_shipment);
            lb_pickup.Text = dt_shipment.Rows[0]["originname"].ToString();
            lb_delivery.Text = dt_shipment.Rows[0]["destname"].ToString();
            this.PH.LoadGridItem(ds1, PH_shiporder, "shiporder.txt", "");
            this.PH.LoadGridItem(ds1, PH_shipdetails, "shipdetails.txt", "");
            this.PH.LoadGridItem(ds1, PH_shipdelivery, "shipdelivery.txt", "");
            this.PH.LoadGridItem(ds1, PH_shipgooddetails, "shipgooddetails.txt", "");
        }
    }

    [WebMethod]
    public static string SupportMessage(string shipmentId, string message)
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
            string query = "SELECT  spr_shipmentid FROM shipmentProfilereport WHERE spr_shipmentid = @ShipmentId  ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@ShipmentId", shipmentId);
            DataTable dt_shipment = DA.GetDataTable(cmd);
            if (dt_shipment.Rows.Count > 0)
            {
                string str_custom = dt_shipment.Rows[0]["spr_shipmentid"].ToString();
                string userEmail = SC.username;
                string userName = SC.Name;
                string companyname = SC.Org_names;

                string str_support = "https://internationalcargologistics.com/";
                string emailContent = CF.Supportemail(userEmail, "CustomSupportMail", "Custom Supporting Mail", str_custom, userName, str_support, companyname, message);

                // Return success message
                return "Shipment SupportMail sent successfully.";
            }
            else
            {
                return "Error: Shipment not found.";
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
            string shipmentNo = shipNo.Text;
            string str_doc = ddl_document.SelectedValue;
            if (fu_fileupload.HasFiles)
            {
                string uploadPath = Server.MapPath("~/Shipment/");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
            }
            if (str_doc == "0")
            {
                this.showuploadfile();
                this.shipmentgriddetails();
                this.containergriddetails();
                this.loadshipdownload();
                this.mapdetails(shipmentNo);
                hfPopupVisible.Value = "true";
                lb_error.Visible = true;
                lb_error.Text = "Please select document";
                lb_error.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (!fu_fileupload.HasFiles)
            {
                this.showuploadfile();
                this.shipmentgriddetails();
                this.containergriddetails();
                this.loadshipdownload();
                this.mapdetails(shipmentNo);
                hfPopupVisible.Value = "true";
                lb_errorfile.Visible = true;
                lb_errorfile.Text = "Please upload file";
                lb_errorfile.ForeColor = System.Drawing.Color.Red;
                return;
            }
            lb_errorfile.Visible = false;
            lb_error.Visible = false;
            string originalFilename = Path.GetFileName(fu_fileupload.FileName);
            //string fileExtension = Path.GetExtension(originalFilename).ToLower();
            string uniqueFileName = shipmentNo + '_' + originalFilename;
            string savePath = Server.MapPath("~/Shipment/") + uniqueFileName;
            fu_fileupload.SaveAs(savePath);
            string query = "INSERT INTO ICL_ShipmentDocument (sd_documenttype, sd_document, sd_shipmentid, sd_createdby) VALUES (@DocumentType, @DocumentName, @ShipmentId, @CreatedBy)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@DocumentType", ddl_document.SelectedValue);
            cmd.Parameters.AddWithValue("@DocumentName", uniqueFileName);
            cmd.Parameters.AddWithValue("@ShipmentId", shipmentNo);
            cmd.Parameters.AddWithValue("@CreatedBy", userid);
            da.ExecuteNonQuery(cmd);

            this.shipmentgriddetails();
            this.containergriddetails();
            this.loadshipdownload();
            this.mapdetails(shipmentNo);
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
        string shipmentNo = shipNo.Text;
        hfPopupVisible.Value = "true";
        this.showuploadfile();
        this.getvalue();
        this.shipmentgriddetails();
        this.containergriddetails();
        this.mapdetails(shipmentNo);
        this.loadshipdownload();
        this.mapdetails(shipmentdecrpt);
    }
    //Uploaded file to show the document//
    private void showuploadfile()
    {
        string str_document = ddl_document.SelectedValue;
        string shipmentNo = shipNo.Text;
        String str_Doc = "SELECT ROW_NUMBER() OVER (ORDER BY sd_shipmentid) AS SerialNo,sd_document,sd_shipmentid FROM ICL_ShipmentDocument WHERE sd_shipmentid='" + shipmentNo + "' and sd_documenttype='" + str_document + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds, PH_Showuploadfile, "ShipShowuploaddoc.txt", "");
        }
    }
    //uploaded file to download //
    [WebMethod]
    public static string UploadDownloadDocument(string shipmentId, string fileName)
    {
        try
        {
            string query = "SELECT sd_document FROM ICL_ShipmentDocument WHERE sd_shipmentid = @ShipmentId ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@ShipmentId", shipmentId);
            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["sd_document"].ToString() == fileName)
                    {
                        string filePath = "/Shipment/" + fileName; // Relative URL
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
                return "Error: No document found for this shipment ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
    //End upload details//


    //Start Download details//

    private void loadshipdownload()
    {
        string shipmentNo = shipNo.Text;
        String str_Doc = "SELECT JSED_FileName,JSED_ShipmentNumber,JSED_DocType,JSED_Description,JSED_Type from JobShipmentEdoc Where JSED_ShipmentNumber='" + shipmentNo + "'";
        SqlCommand cmd = new SqlCommand(str_Doc);
        DataTable dt_table = da.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_table);
        if (dt_table.Rows.Count > 0)
        {

            this.PH.LoadGridItem(ds, PH_shipdoc, "docshipment.txt", "");
        }
    }
    [WebMethod]
    public static string DownloadDocument(string shipmentId, string fileName, string type)
    {
        try
        {
            string query = "SELECT JSED_FileName, JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber AND JSED_FileName = @JSED_FileName";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", shipmentId);
            cmd.Parameters.AddWithValue("@JSED_FileName", fileName);

            DataAccess da = new DataAccess();
            DataTable dt = da.GetDataTable(cmd);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string fileName1 = row["JSED_FileName"].ToString();
                string str_type = row["JSED_DocType"].ToString();
                string str_filenametype = str_type + "." + fileName1.ToLower();
                string filePath = Path.Combine(FileHelper.ShipmentPath, shipmentId, str_filenametype);
                string destinationPath = @"E:\ICLHUBEdocument\ForwardingShipment\" + shipmentId+"\\"+ str_filenametype;


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
                return "Error: No document found for this shipment ID.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        string shipmentNo = shipNo.Text;

        string query = "SELECT JSED_FileName, JSED_DocType FROM JobShipmentEdoc WHERE JSED_ShipmentNumber = @JSED_ShipmentNumber";
        SqlCommand cmd = new SqlCommand(query);
        cmd.Parameters.AddWithValue("@JSED_ShipmentNumber", shipmentNo);

        DataTable dt_table = da.GetDataTable(cmd);
        if (dt_table.Rows.Count > 0)
        {
            string zipFileName = "Documents_" + shipmentNo + ".zip";
            string zipFilePath = @"E:\ICLHUBEdocument\ForwardingShipment\" + shipmentNo + @"\" + zipFileName;

            // ✅ Create ZIP
            using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Create))
            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (DataRow row in dt_table.Rows)
                {
                    string fileName = row["JSED_FileName"].ToString();
                    string str_type = row["JSED_DocType"].ToString();
                    string Filetype = str_type + "." + fileName.ToLower();
                    string filePath = @"E:\ICLHUBEdocument\ForwardingShipment\" + shipmentNo + @"\" + Filetype;

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
            this.getvalue();
            this.shipmentgriddetails();
            this.containergriddetails();
            this.loadshipdownload();
            this.mapdetails(shipmentdecrpt);
        }
        else
        {
            Response.Write("<script>alert('No files available for download.');</script>");
        }
    }
}
    // End Download details//

