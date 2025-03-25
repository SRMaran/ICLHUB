using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Routing;
using System.Text;
using System.Xml.Linq;
using System.Web.UI;
using System.Text.Json.Nodes;
public partial class Web_Tracking : System.Web.UI.Page
{
    DataAccess DA;
    SessionCustom SC;
    CommonFunction CF;
    Decrypt DE;

    PhTemplate PH;
    string userrole = "";
    string str_userid = "";
    string str_Org = "";
    string str_Orgcode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.DA = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();



        SC.lablename = "Tracking";
        SC.headername = "Tracking";
        Origin.HRef = "EventBooking.aspx?id=TRAKINGBOOKINGEVENT";
        Transit.HRef = "Originshipment.aspx?id=TRANSITSHIPMENT";
        destination.HRef = "Originshipment.aspx?id=DESSHIPMENT";
        gateout.HRef = "Originshipment.aspx?id=GATEOUTSHIPMENT";

        userrole = SC.UserRole;
        str_userid = SC.Userid;

        if (userrole != "0")
        {
            str_Org = SC.Orgname;
            str_Orgcode = SC.Orgnamecode;

        }
        //this.milestonedetails();

        this.Loadtracking();

        Transit.HRef = "Originshipment.aspx?id=TRANSITSHIPMENT";
        destination.HRef = "Originshipment.aspx?id=DESSHIPMENT";
        Origin.HRef = "EventBooking.aspx?id=Orginport";
    }

    private void milestonedetails()
    {
        string jsonFilePath = "D:\\json.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        Root root = JsonConvert.DeserializeObject<Root>(jsonData);


        foreach (var shippingLine in root.Data)
        {
            string insertQuery = @"
                INSERT INTO ShippingLines (Name, Active, CT, BL, BK, BL_CT, BK_CT, Maintenance, SCAC_Codes, Prefixes)
                VALUES (@Name, @Active, @CT, @BL, @BK, @BL_CT, @BK_CT, @Maintenance, @SCAC_Codes, @Prefixes)";

            using (SqlCommand cmd = new SqlCommand(insertQuery))
            {
                cmd.Parameters.AddWithValue("@Name", shippingLine.Name);
                cmd.Parameters.AddWithValue("@Active", shippingLine.Active);
                cmd.Parameters.AddWithValue("@CT", shippingLine.Active_Types.Ct);
                cmd.Parameters.AddWithValue("@BL", shippingLine.Active_Types.Bl);
                cmd.Parameters.AddWithValue("@BK", shippingLine.Active_Types.Bk);
                cmd.Parameters.AddWithValue("@BL_CT", shippingLine.Active_Types.Bl_Ct);
                cmd.Parameters.AddWithValue("@BK_CT", shippingLine.Active_Types.Bk_Ct);
                cmd.Parameters.AddWithValue("@Maintenance", shippingLine.Maintenance);
                cmd.Parameters.AddWithValue("@SCAC_Codes", string.Join(",", shippingLine.Scac_Codes));
                cmd.Parameters.AddWithValue("@Prefixes", string.Join(",", shippingLine.Prefixes));
                DA.ExecuteNonQuery(cmd);
            }
        }
    }


    public class ShippingLine
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public ActiveTypes Active_Types { get; set; }
        public bool Maintenance { get; set; }
        public List<string> Scac_Codes { get; set; }
        public List<string> Prefixes { get; set; }
    }

    public class ActiveTypes
    {
        public bool Ct { get; set; }
        public bool Bl { get; set; }
        public bool Bk { get; set; }
        public bool Bl_Ct { get; set; }
        public bool Bk_Ct { get; set; }
    }

    public class Root
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ShippingLine> Data { get; set; }
    }
    private void Loadtracking()
    {

        string str_shipment = "";
        string str_goodsdetails = "";
        if (userrole =="0")
        {


            str_goodsdetails = "SELECT spr_shipmentid AS ShipmentNumber, spr_origin, spr_destination, FORMAT(spr_originetd, 'dd-MMM-yyyy') AS ETD FROM shipmentProfilereport WHERE spr_originetd BETWEEN DATEADD(MONTH, -1, CAST(GETDATE() AS DATE)) AND CAST(GETDATE() AS DATE)  ORDER BY spr_originetd DESC";

        }
        else if (userrole=="2")
        {
            switch (str_Orgcode)
            {
                  case "0":
                    str_goodsdetails = "SELECT spr_shipmentid AS ShipmentNumber, spr_origin, spr_destination, FORMAT(spr_originetd, 'dd-MMM-yyyy') AS ETD FROM shipmentProfilereport WHERE spr_originetd BETWEEN DATEADD(MONTH, -1, CAST(GETDATE() AS DATE)) AND CAST(GETDATE() AS DATE)  ORDER BY spr_originetd DESC";
                    break;
                default:
                    str_goodsdetails = "SELECT spr_shipmentid AS ShipmentNumber, spr_origin, spr_destination, FORMAT(spr_originetd, 'dd-MMM-yyyy') AS ETD FROM shipmentProfilereport WHERE spr_originetd BETWEEN DATEADD(MONTH, -1, CAST(GETDATE() AS DATE)) AND CAST(GETDATE() AS DATE) and spr_localclientcode like '%" + str_Orgcode + "%' ORDER BY spr_originetd DESC";
                    break;
            }
            }
        else
        {
            str_goodsdetails = "SELECT spr_shipmentid AS ShipmentNumber, spr_origin, spr_destination, FORMAT(spr_originetd, 'dd-MMM-yyyy') AS ETD FROM shipmentProfilereport WHERE spr_originetd BETWEEN DATEADD(MONTH, -1, CAST(GETDATE() AS DATE)) AND CAST(GETDATE() AS DATE) and spr_localclientcode like '%" + str_Orgcode + "%' ORDER BY spr_originetd DESC";
        }
        SqlCommand cmd = new SqlCommand(str_goodsdetails);
        DataTable dt_goodsdetails = DA.GetDataTable(cmd);
        
        if (dt_goodsdetails.Rows.Count > 0)
        {
            if (!dt_goodsdetails.Columns.Contains("shipmentid"))
            {
                dt_goodsdetails.Columns.Add("shipmentid", typeof(string));
            }
            foreach (DataRow row in dt_goodsdetails.Rows)
            {
                str_shipment = row["ShipmentNumber"].ToString();
                string shipmentdecrpt = DE.Register(str_shipment);
                row["shipmentid"] = shipmentdecrpt;  // ✅ Ensure every row gets an encrypted ID
                lb_pickup.Text = row["spr_origin"].ToString();
                lb_delivery.Text = row["spr_destination"].ToString();



            }
            DataSet ds = new DataSet();
            ds.Merge(dt_goodsdetails);
            this.PH.LoadGridItem(ds, PH_Goodsdetails, "Trackinggoods.txt", "");
        } 
        string str_mapping = "select distinct a.description,FORMAT(a.date, 'dd-MMM-yyyy HH:mm') AS date,b.name from ICL_Events a left outer join ICL_Locations b on  a.location=b.location_key left outer join swl_icfdreport  c on a.containerNumber=c.icfd_container  where c.icfd_jobref='" + str_shipment + "'";
        SqlCommand cmd1 = new SqlCommand(str_mapping);
        DataTable dt_mapdetails = DA.GetDataTable(cmd1);
        DataSet ds1 = new DataSet();
        ds1.Merge(dt_mapdetails);
        if (dt_mapdetails.Rows.Count > 0)
        {
            this.PH.LoadGridItem(ds1, PH_Mapdetails, "Mapdetails.txt", "");
        }
        string str_trackingquery = "";
        switch (userrole)
        {
            case "0":

                str_trackingquery = "EXEC GetShipmentCounts";
                break;
            case "2":
                switch (str_Orgcode)
                {
                    case "0":
                        str_trackingquery = "EXEC GetShipmentCounts";
                        break;
                    default:
                        str_trackingquery = "EXEC GetShipmentCountscustomer   @localclientcode = '" + str_Orgcode + "'";
                        break;
                }
                break;
            case "1":
                str_trackingquery = "EXEC GetShipmentCountscustomer   @localclientcode = '" + str_Orgcode + "'";
                break;
        }

        SqlCommand cmdtrackcount = new SqlCommand(str_trackingquery);
        DataTable dt = DA.GetDataTable(cmdtrackcount);
        if (dt.Rows.Count > 0)
        {
            //lb_gateout.Text = dt.Rows[0]["Gateout"].ToString();
            lb_gateout.Text = "0";
            lb_origin.Text = dt.Rows[0]["AtOriginPortCount"].ToString();
            lb_transit.Text = dt.Rows[0]["AtTransitPortCount"].ToString();
            lb_destination.Text = dt.Rows[0]["AtDestinationPortCount"].ToString();
        }
    }
    [WebMethod]
    public static string GetShipmentDetails(string shipmentId)
    {
        try
        {
            DataAccess DA = new DataAccess();
            string strMapping = @"SELECT DISTINCT  a.description, FORMAT(a.date, 'dd-MMM-yyyy HH:mm') AS date,b.name  FROM ICL_Events a LEFT JOIN ICL_Locations b ON a.location = b.location_key  LEFT JOIN swl_icfdreport c ON a.containerNumber = c.icfd_container  WHERE c.icfd_jobref = @ShipmentId";
            SqlCommand cmdMapping = new SqlCommand(strMapping);
            cmdMapping.Parameters.AddWithValue("@ShipmentId", shipmentId);
            DataTable dtMapDetails = DA.GetDataTable(cmdMapping);
            string query = @"SELECT  spr_shipmentid,spr_origin,spr_destination  FROM shipmentProfilereport  WHERE spr_shipmentid = @ShipmentId";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@ShipmentId", shipmentId);
            DataTable dtGoodsDetails = DA.GetDataTable(cmd);
            if (dtGoodsDetails.Rows.Count > 0)
            {
                var shipmentDetails = new
                {
                    Origin = dtGoodsDetails.Rows[0]["spr_origin"].ToString(),
                    Destination = dtGoodsDetails.Rows[0]["spr_destination"].ToString(),
                    EventMapping = dtMapDetails.AsEnumerable().Select(row => new
                    {
                        Description = row["description"].ToString(),
                        Date = row["date"].ToString(),
                        Name = row["name"].ToString()
                    }).ToList()
                };

                return new JavaScriptSerializer().Serialize(shipmentDetails);
            }
            else
            {
                return new JavaScriptSerializer().Serialize(new { error = "No shipment found for the provided ID." });
            }
        }
        catch (Exception ex)
        {
            return new JavaScriptSerializer().Serialize(new { error = ex.Message });
        }
    }

    private static Page GetCurrentPage(DataTable locationsTable, DataTable eventsTable)
    {
        DateTime today = DateTime.Now;

        if (locationsTable.Rows.Count > 0)
        {
            for (var c = locationsTable.Rows.Count - 1; c >= 0; c--)
            {
                string id = locationsTable.Rows[c]["id"].ToString();
                string name = locationsTable.Rows[c]["name"].ToString();
                string state = locationsTable.Rows[c]["state"].ToString();
                string country = locationsTable.Rows[c]["country"].ToString();
                string locade = locationsTable.Rows[c]["locade"].ToString();
                string lat = locationsTable.Rows[c]["lat"].ToString();
                string lng = locationsTable.Rows[c]["lng"].ToString();

                if (eventsTable.Rows.Count > 0)
                {
                    for (var i = 0; i < eventsTable.Rows.Count; i++)
                    {
                        string Orderid = eventsTable.Rows[i]["Orderid"].ToString();
                        string location = eventsTable.Rows[i]["location"].ToString();
                        string description = eventsTable.Rows[i]["description"].ToString();
                        string Date = eventsTable.Rows[i]["Date"].ToString();

                        if (id == location)
                        {

                        }
                    }
                    }
                }

            }
        return null;
    }

        [WebMethod]
    public static async Task<string> mapdetails(string str_ControlValue)

    {
        string liveLat = "";
        string liveLng = "";
        string jsonlive = "";
        string jsonst = "";
        string jsonend = "";
        string Str_carrier = "";

        PhTemplate PH = new PhTemplate();

        List<string> latimarkList = new List<string>();
        try
        {
            string str_shipment = str_ControlValue;
          
            DataAccess DA = new DataAccess();
                //string Shipper = "select top 1 icfd_container from swl_icfdreport where icfd_jobref='SPIMSI00279389'";
                string Shipper = "select top 1 icfd_container from swl_icfdreport where icfd_jobref='"+ str_shipment + "'";
            SqlCommand mapliveShipper = new SqlCommand(Shipper);
            DataTable dt_container = DA.GetDataTable(mapliveShipper);

            if (dt_container.Rows.Count > 0)
            {

                string carrier = "SELECT a.SCAC_Codes,a.name FROM ShippingLines a inner JOIN shipmentProfilereport b ON b.spr_carriername LIKE '%' + a.name + '%' where b.spr_shipmentid='"+ str_shipment + "'";
                SqlCommand maplivedt_carrier = new SqlCommand(carrier);
                DataTable dt_carrier = DA.GetDataTable(maplivedt_carrier);

                if (dt_carrier.Rows.Count > 0)
                {
                    string dt_carriercode = dt_carrier.Rows[0]["SCAC_Codes"].ToString();
                    string[] slipcode = dt_carriercode.Split(',');
                    string scaccode = slipcode.Length > 0 ? slipcode[0].Trim() : "";
                    string containernumber = dt_container.Rows[0]["icfd_container"].ToString();

                    string apiUrl = "https://tracking.searates.com/tracking?api_key=N5J3-9K1S-742F-Y0WV-GMAL&number=" + containernumber + "&type=CT&force_update=false&route=false&ais=false";
                    string apiurlroute = "https://tracking.searates.com/route?api_key=N5J3-9K1S-742F-Y0WV-GMAL&number=" + containernumber + "&type=CT&sealine="+ scaccode + "";


                    

                    

                    using (HttpClient client = new HttpClient())
                    {


                        HttpClient clientapi = new HttpClient();
                        var requestapi = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                        var responseapi = clientapi.SendAsync(requestapi).Result;
                        responseapi.EnsureSuccessStatusCode();
                        string responseContentapi = responseapi.Content.ReadAsStringAsync().Result;

                        var jsonObjectapi = JObject.Parse(responseContentapi);
                       // JObject routeArrayapi = (JObject)jsonObjectapi["data"]["metadata"];

                        DataTable metadataTable = ConvertMetadataToDataTable(jsonObjectapi["data"]["metadata"]);
                        DataTable locationsTable = ConvertArrayToDataTable((JArray)jsonObjectapi["data"]["locations"], "Locations");
                        DataTable facilitiesTable = ConvertArrayToDataTable((JArray)jsonObjectapi["data"]["facilities"], "Facilities");
                        DataTable vesselsTable = ConvertArrayToDataTable((JArray)jsonObjectapi["data"]["vessels"], "Vessels");
                        DataTable containersTable = ConvertArrayToDataTable((JArray)jsonObjectapi["data"]["containers"], "Containers");

                        DataTable chargesTable = ConvertChargesToDataTable((JArray)jsonObjectapi["data"]["containers"]);
                        DataTable eventsTable = ConvertEventsToDataTable((JArray)jsonObjectapi["data"]["containers"]);

                        Page currentPage = GetCurrentPage(locationsTable, eventsTable) as Page;

                        var request = new HttpRequestMessage(HttpMethod.Get, apiurlroute);
                        var response = client.SendAsync(request).Result;
                        response.EnsureSuccessStatusCode();
                        string responseContent = response.Content.ReadAsStringAsync().Result;

                        var jsonObject = JObject.Parse(responseContent);

                     
                        JArray routeArray = (JArray)jsonObject["data"]["route"];
                        JArray Livepin = (JArray)jsonObject["data"]["pin"];
                        if (routeArray != null)
                        {
                            JArray pathArray = (JArray)routeArray[0]["path"];

                            DataTable live_datatable = new DataTable();
                            live_datatable.Columns.Add("type", typeof(double));
                            live_datatable.Columns.Add("Latitude", typeof(double));
                            live_datatable.Columns.Add("Longitude", typeof(double));

                            if (Livepin != null && Livepin.Count == 2) 
                            {
                                live_datatable.Rows.Add((double)Livepin[0], (double)Livepin[1]);
                            }

                            if (live_datatable.Rows.Count > 0)
                            {
                                liveLat = live_datatable.Rows[0]["Latitude"].ToString();
                                liveLng = live_datatable.Rows[0]["Longitude"].ToString();
                            }

                            var livdata = new System.Collections.Generic.Dictionary<string, string>();
                            livdata.Add("lat", liveLat);
                            livdata.Add("lng", liveLng);
                            jsonlive = JsonConvert.SerializeObject(livdata);

                            DataTable dt = new DataTable();
                            dt.Columns.Add("Latitude", typeof(double));
                            dt.Columns.Add("Longitude", typeof(double));

                            foreach (JArray coord in pathArray)
                            {
                                dt.Rows.Add((double)coord[0], (double)coord[1]);
                            }

                            int lastIndex = dt.Rows.Count - 1;

                            if (lastIndex >= 0)
                            {
                                string StLat = dt.Rows[0]["Latitude"].ToString();
                                string StLng = dt.Rows[0]["Longitude"].ToString();
                                string EndLat = dt.Rows[lastIndex]["Latitude"].ToString();
                                string EndLng = dt.Rows[lastIndex]["Longitude"].ToString();

                                var datast = new System.Collections.Generic.Dictionary<string, string>();
                                datast.Add("lat", StLat);
                                datast.Add("lng", StLng);
                                jsonst = JsonConvert.SerializeObject(datast);

                                var dataliv = new System.Collections.Generic.Dictionary<string, string>();
                                dataliv.Add("lat", EndLat);
                                dataliv.Add("lng", EndLng);
                                jsonend = JsonConvert.SerializeObject(dataliv);
                            }

                            for (var c = 0; c < dt.Rows.Count; c++)
                            {
                                string Lat = dt.Rows[c]["Latitude"].ToString();
                                string Lng = dt.Rows[c]["Longitude"].ToString();

                                var dataendmarklivedata = new System.Collections.Generic.Dictionary<string, string>();
                                dataendmarklivedata.Add("lat", Lat);
                                dataendmarklivedata.Add("lng", Lng);
                                string jsonstlive = JsonConvert.SerializeObject(dataendmarklivedata);
                                latimarkList.Add(jsonstlive);
                            }



                            var data = new Dictionary<string, object>();
                            data.Add("start_point", jsonst);
                            data.Add("live_point", jsonlive);
                            data.Add("end_point", jsonend);
                            data.Add("marklinedata", latimarkList);
                            data.Add("Status", "success");


                            string json = JsonConvert.SerializeObject(data);
                            return json;
                        }
                        else
                        {
                            var data = new System.Collections.Generic.Dictionary<string, string>();
                            data.Add("start_point", "");
                            data.Add("live_point", "");
                            data.Add("end_point", "");
                            data.Add("marklinedata", "");
                            data.Add("Status", "Empty");
                            string json = JsonConvert.SerializeObject(data);
                            return json;
                        }
                    }
                }
                else
                {

                    var data = new System.Collections.Generic.Dictionary<string, string>();
                    data.Add("start_point", "");
                    data.Add("live_point", "");
                    data.Add("end_point", "");
                    data.Add("marklinedata", "");
                    data.Add("Status", "Empty");
                    string json = JsonConvert.SerializeObject(data);
                    return json;
                }
            }
            else
            {
                var data = new System.Collections.Generic.Dictionary<string, string>();
                data.Add("start_point", "");
                data.Add("live_point", "");
                data.Add("end_point", "");
                data.Add("marklinedata", "");
                data.Add("Status", "Empty");
                string json = JsonConvert.SerializeObject(data);
                return json;
            }
        }
        catch (Exception ex)
        {
            var data = new System.Collections.Generic.Dictionary<string, string>();
            data.Add("start_point", "");
            data.Add("live_point", "");
            data.Add("end_point", "");
            data.Add("marklinedata", "");
            data.Add("Status", "Empty");
            string json = JsonConvert.SerializeObject(data);
            return json;
            

        }
    }
    static DataTable ConvertMetadataToDataTable(JToken metadata)
    {
        DataTable dt = new DataTable("Metadata");
        if (metadata != null)
        {
            foreach (JProperty prop in metadata)
            {
                dt.Columns.Add(prop.Name, typeof(string));
            }

            DataRow row = dt.NewRow();
            foreach (JProperty prop in metadata)
            {
                row[prop.Name] = prop.Value.ToString();
            }
            dt.Rows.Add(row);
        }
   

        return dt;
    }

    static DataTable ConvertArrayToDataTable(JArray jsonArray, string tableName)
    {
        DataTable dt = new DataTable(tableName);
        if (jsonArray != null)
        {
            if (jsonArray.Count > 0)
            {
                foreach (JProperty prop in jsonArray[0])
                {
                    dt.Columns.Add(prop.Name, typeof(string));
                }

                foreach (JObject obj in jsonArray)
                {
                    DataRow row = dt.NewRow();
                    foreach (var prop in obj.Properties()) // Corrected line
                    {
                        row[prop.Name] = prop.Value != null ? prop.Value.ToString() : ""; // Updated null check
                    }
                    dt.Rows.Add(row);
                }

                return dt;


            }
        }
        return dt;
    }

    static void PrintDataTable(DataTable dt)
    {
        foreach (DataColumn col in dt.Columns)
        {
            Console.Write(col.ColumnName + "\t");
        }
        Console.WriteLine();

        foreach (DataRow row in dt.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
    }


    // Convert Charges to DataTable
    static DataTable ConvertChargesToDataTable(JArray containersArray)
    {
        DataTable dt = new DataTable("Charges");
        dt.Columns.Add("ContainerNumber");
        dt.Columns.Add("ChargeType");
        dt.Columns.Add("FreeDays");
        dt.Columns.Add("DaysInCharge");
        if (containersArray != null)
        {
            foreach (JObject container in containersArray)
            {
                string containerNumber = container["number"].ToString() ?? "";

                JObject charges = (JObject)container["charges"];
                if (charges != null)
                {
                    foreach (var chargeType in charges.Properties())
                    {
                        JObject chargeDetails = (JObject)chargeType.Value;
                        DataRow row = dt.NewRow();
                        row["ContainerNumber"] = containerNumber;
                        row["ChargeType"] = chargeType.Name;
                        row["FreeDays"] = chargeDetails["free_days"].ToString() ?? "0";
                        row["DaysInCharge"] = chargeDetails["days_in_charge"].ToString() ?? "0";
                        dt.Rows.Add(row);
                    }
                }
            }
        }
        return dt;
    }

    // Convert Events to DataTable
    static DataTable ConvertEventsToDataTable(JArray containersArray)
    {
        DataTable dt = new DataTable("Events");
        dt.Columns.Add("ContainerNumber");
        dt.Columns.Add("OrderId");
        dt.Columns.Add("Location");
        dt.Columns.Add("Facility");
        dt.Columns.Add("Description");
        dt.Columns.Add("EventType");
        dt.Columns.Add("EventCode");
        dt.Columns.Add("Status");
        dt.Columns.Add("Date");
        dt.Columns.Add("Actual");
        dt.Columns.Add("TransportType");
        dt.Columns.Add("Vessel");
        dt.Columns.Add("Voyage");

        if (containersArray != null)
        {
            foreach (JObject container in containersArray)
            {


                string containerNumber = container["number"].ToString() ?? "";

                // Safely get the events array
                JArray eventsArray = container["events"] as JArray ?? new JArray();

                foreach (JObject eventObj in eventsArray)
                {
                    DataRow row = dt.NewRow();
                    row["ContainerNumber"] = containerNumber;
                    row["OrderId"] = eventObj["order_id"].ToString() ?? "";
                    row["Location"] = eventObj["location"].ToString() ?? "";
                    row["Facility"] = eventObj["facility"].ToString() ?? "";
                    row["Description"] = eventObj["description"].ToString() ?? "";
                    row["EventType"] = eventObj["event_type"].ToString() ?? "";
                    row["EventCode"] = eventObj["event_code"].ToString() ?? "";
                    row["Status"] = eventObj["status"].ToString() ?? "";
                    row["Date"] = eventObj["date"].ToString() ?? "";
                    row["Actual"] = eventObj["actual"].ToString() ?? "";
                    row["TransportType"] = eventObj["transport_type"].ToString() ?? "";
                    row["Vessel"] = eventObj["vessel"].ToString() ?? "";
                    row["Voyage"] = eventObj["voyage"].ToString() ?? "";

                    dt.Rows.Add(row);
                }
            }
        }

        return dt;
    }
}