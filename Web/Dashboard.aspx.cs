using Newtonsoft.Json;
using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Controls.Primitives;
using System.Text.Json;
using System.Windows.Media;
using System.Web.Script.Serialization;
using System.Linq;

public partial class Web_Dashboard : System.Web.UI.Page
{
    SessionCustom SC;
    CommonFunction CF;
    DataAccess DA;
    PhTemplate PH;
    Decrypt DE;
    string Startdate = "";
    string Enddate = "";
    string str_Email = "";
    string str_userRole = "";
    string str_userid = "";
    string str_Org = "";
    string datefilter = "";
    string str_Orgcode = "";
    DateTime currentDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.SC = new SessionCustom();
        this.CF = new CommonFunction();
        this.PH = new PhTemplate();
        this.DA = new DataAccess();
        this.DE = new Decrypt();
        str_Email = SC.username;
        str_userRole = SC.UserRole;
        str_userid = SC.Userid;
        SC.lablename = "Dashboard";
        SC.headername = "Dashboard";
        if (str_userRole != "0")
        {
            str_Org = SC.Orgname;
            str_Orgcode = SC.Orgnamecode;


        }

        if (!IsPostBack)
        {
            MasterPage master = (MasterPage)this.Master;
            //string selectedValue = master();
            this.widget();
            this.Loadorigin();
            this.notification();
            this.quotedetails();
            //this.GetChartData();



        }
        ETA.HRef = "Arriving.aspx";
        ETD.HRef = "Departing.aspx";
        ATD.HRef = "Awaiting.aspx";
        Intransit.HRef = "InTransit.aspx";
        Dest.HRef = "Originshipment.aspx";
        Confirmedbooking.HRef = "Booking.aspx";
        Orginport.HRef = "EventBooking.aspx?id=Orginport";
        // GetUpcomingEvents();
    }


    [WebMethod]
    public static string GetUpcomingEvents()
    {
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();

        string str_Email = SC.username;
        string str_userRole = SC.UserRole;
        string str_userid = SC.Userid;
        string query = string.Empty;


        if (str_userRole == "0")
        {
            query = @"SELECT DISTINCT icfd_jobref, icfd_eta AS Arriving, 
               CASE 
                   WHEN icfd_estimateddeliver IS NOT NULL AND icfd_estimateddeliver <> '1900-01-01 00:00:00.000' 
                   THEN icfd_estimateddeliver 
                   WHEN icfd_actualdeliver IS NOT NULL AND icfd_actualdeliver <> '1900-01-01 00:00:00.000' 
                   THEN icfd_actualdeliver 
                   ELSE NULL 
               END AS shipped 
               FROM swl_icfdreport 
               WHERE TRY_CONVERT(DATETIME,icfd_eta) <= GETDATE() order by icfd_eta asc";
        }
        else if (str_userRole == "2")
        {
            string str_Orgcode = SC.Orgnamecode;
            if (str_Orgcode == "0")
            {
                query = @"SELECT DISTINCT icfd_jobref, icfd_eta AS Arriving, 
               CASE 
                   WHEN icfd_estimateddeliver IS NOT NULL AND icfd_estimateddeliver <> '1900-01-01 00:00:00.000' 
                   THEN icfd_estimateddeliver 
                   WHEN icfd_actualdeliver IS NOT NULL AND icfd_actualdeliver <> '1900-01-01 00:00:00.000' 
                   THEN icfd_actualdeliver 
                   ELSE NULL 
               END AS shipped 
               FROM swl_icfdreport 
               WHERE TRY_CONVERT(DATETIME,icfd_eta)  <= GETDATE() order by icfd_eta asc";
            }
            else
            {
                query = @"SELECT DISTINCT a.icfd_jobref, a.icfd_eta AS Arriving,
               CASE 
                   WHEN a.icfd_estimateddeliver IS NOT NULL AND a.icfd_estimateddeliver <> '1900-01-01 00:00:00.000' 
                   THEN a.icfd_estimateddeliver 
                   WHEN a.icfd_actualdeliver IS NOT NULL AND a.icfd_actualdeliver <> '1900-01-01 00:00:00.000' 
                   THEN a.icfd_actualdeliver 
                   ELSE NULL 
               END AS shipped 
               FROM swl_icfdreport a 
               WHERE TRY_CONVERT(DATETIME, a.icfd_eta)  <= GETDATE()
                   AND a.icfd_clientcode like '%" + str_Orgcode + "%' order by a.icfd_eta asc";
            }

        }
        else if (str_userRole == "1")
        {
            string str_Orgcode = SC.Orgnamecode;
            query = @"SELECT DISTINCT a.icfd_jobref, a.icfd_eta AS Arriving,
               CASE 
                   WHEN a.icfd_estimateddeliver IS NOT NULL AND a.icfd_estimateddeliver <> '1900-01-01 00:00:00.000' 
                   THEN a.icfd_estimateddeliver 
                   WHEN a.icfd_actualdeliver IS NOT NULL AND a.icfd_actualdeliver <> '1900-01-01 00:00:00.000' 
                   THEN a.icfd_actualdeliver 
                   ELSE NULL 
               END AS shipped 
               FROM swl_icfdreport a 
               WHERE  TRY_CONVERT(DATETIME, a.icfd_eta)  <= GETDATE()
                   AND a.icfd_clientcode like '%" + str_Orgcode + "%' order by a.icfd_eta asc";
        }
        SqlCommand cmd = new SqlCommand(query);
        DataTable dt = DA.GetDataTable(cmd);
        List<object> events = new List<object>();
        foreach (DataRow row in dt.Rows)
        {
            string eventType = row["Arriving"] != DBNull.Value ? "Arriving" : row["shipped"] != DBNull.Value ? "Shipped" : null;
            events.Add(new
            {
                JobRef = row["icfd_jobref"].ToString(),
                Shipped = row["shipped"] != DBNull.Value ? Convert.ToDateTime(row["shipped"]).ToString("dd-MMM-yyyy") : null,

                Arriving = row["Arriving"] != DBNull.Value ? Convert.ToDateTime(row["Arriving"]).ToString("dd-MMM-yyyy") : null,
                EventType = eventType
            });
        }

        return new JavaScriptSerializer().Serialize(events);
    }

    private void quotedetails()
    {
        string dateOnly = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
        string str_quotedetails = "select count(*)as quotescount from Quotes_Details where CAST(CreatedOn AS DATE)  = '" + dateOnly + "' and ModifiedOn is null";
        SqlCommand cmd = new SqlCommand(str_quotedetails);
        DataTable dt_quotedetails = DA.GetDataTable(cmd);
        DataSet ds = new DataSet();
        ds.Merge(dt_quotedetails);
        if (dt_quotedetails.Rows.Count > 0)
        {
            lb_quotes.Text = dt_quotedetails.Rows[0]["quotescount"].ToString();
        }
    }

    private void notification()
    {
        string str_goodsdetails = "";
        if (str_userRole == "0")
        {
            str_goodsdetails = "select DISTINCT a.containernumber,b.icfd_jobref,c.spr_origin,c.spr_destination,FORMAT(c.spr_originetd, 'dd-MMM-yyyy') AS  spr_originetd from ICLRoute_livelocation a inner join swl_icfdreport b on a.containernumber=b.icfd_container inner join shipmentProfilereport c on b.icfd_jobref=c.spr_shipmentid where  a.route_livelatitude is not null and a.route_livelongitude is not null and c.spr_modifiedon is null ";
        }
        else if (str_userRole == "2")
        {
            if (str_Orgcode == "0")
            {
                str_goodsdetails = "select DISTINCT a.containernumber,b.icfd_jobref,c.spr_origin,c.spr_destination,FORMAT(c.spr_originetd, 'dd-MMM-yyyy') AS  spr_originetd from ICLRoute_livelocation a inner join swl_icfdreport b on a.containernumber=b.icfd_container inner join shipmentProfilereport c on b.icfd_jobref=c.spr_shipmentid where  a.route_livelatitude is not null and a.route_livelongitude is not null and c.spr_modifiedon is null ";
            }
            else
            {
                str_goodsdetails = "select DISTINCT a.containernumber,b.icfd_jobref,c.spr_origin,c.spr_destination,FORMAT(c.spr_originetd, 'dd-MMM-yyyy') AS spr_originetd from ICLRoute_livelocation a inner  join swl_icfdreport b on a.containernumber = b.icfd_container inner " +
" join shipmentProfilereport c on b.icfd_jobref = c.spr_shipmentid   where  a.route_livelatitude is not null and a.route_livelongitude is not null and b.icfd_clientcode = '" + str_Orgcode + "' and c.spr_modifiedon is null";
            }
        }
        else
        {
            str_goodsdetails = "select DISTINCT a.containernumber,b.icfd_jobref,c.spr_origin,c.spr_destination,FORMAT(c.spr_originetd, 'dd-MMM-yyyy') AS spr_originetd from ICLRoute_livelocation a inner  join swl_icfdreport b on a.containernumber = b.icfd_container inner " +
 " join shipmentProfilereport c on b.icfd_jobref = c.spr_shipmentid  where  a.route_livelatitude is not null and a.route_livelongitude is not null and b.icfd_clientcode = '" + str_Orgcode + "' and c.spr_modifiedon is null";
        }
        SqlCommand cmd = new SqlCommand(str_goodsdetails);
        DataTable dt_goodsdetails = DA.GetDataTable(cmd);
        if (dt_goodsdetails.Rows.Count > 0)
        {
            dt_goodsdetails.Columns.Add("shipmentencrypt", typeof(string));

            foreach (DataRow row in dt_goodsdetails.Rows)
            {
                string shipid = row["icfd_jobref"].ToString();
                string shipdecrpt = DE.Register(shipid);
                row["shipmentencrypt"] = shipdecrpt;
            }
            DataSet ds = new DataSet();
            ds.Merge(dt_goodsdetails);
            this.PH.LoadGridItem(ds, PH_Notification, "dashnotification.txt", "");
        }
    }
    private void widget()
    {
        string str_query = "";
        string str_ship = "";
        string dateOnly = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");

        if (str_userRole == "0")
        {
            str_query = "EXEC GetwidgetAdmin  @Todaydate = '" + dateOnly + "'";
            str_ship = "EXEC GetShipmentCounts ";

        }
        else if (str_userRole == "2")
        {
            if (str_Orgcode == "0")
            {
                str_query = "EXEC GetwidgetAdmin  @Todaydate = '" + dateOnly + "'";
                str_ship = "EXEC GetShipmentCounts ";
            }
            else
            {
                str_query = "EXEC Getwidgetclient  @clientcode = '" + str_Orgcode + "',@Todaydate = '" + dateOnly + "'";

                str_ship = "EXEC GetShipmentCountscustomer   @localclientcode = '" + str_Orgcode + "'";
            }
        }
        else
        {
            str_query = "EXEC Getwidgetclient  @clientcode = '" + str_Orgcode + "',@Todaydate = '" + dateOnly + "'";

            str_ship = "EXEC GetShipmentCountscustomer   @localclientcode = '" + str_Orgcode + "'";

        }
        SqlCommand cmd = new SqlCommand(str_query);
        DataTable dt = DA.GetDataTable(cmd);
        if (dt.Rows.Count > 0)
        {
            lb_etaupdate.Text = dt.Rows[0]["etacount"].ToString();
            lb_etd.Text = dt.Rows[0]["etdcount"].ToString();
            lb_delivery.Text = dt.Rows[0]["deliverycount"].ToString();
            lb_booking.Text = dt.Rows[0]["bookingcount"].ToString();
        }
        SqlCommand cmd1 = new SqlCommand(str_ship);
        DataTable dt1 = DA.GetDataTable(cmd1);
        int total = Convert.ToInt32(dt1.Rows[0]["TotalCount"]);
        int origin = Convert.ToInt32(dt1.Rows[0]["AtOriginPortCount"]);
        int transit = Convert.ToInt32(dt1.Rows[0]["AtTransitPortCount"]);
        int destination = Convert.ToInt32(dt1.Rows[0]["AtDestinationPortCount"]);

        lb_Active.Text = total.ToString();
        lb_shipment.Text = total.ToString();
        lb_Origin.Text = origin.ToString();
        lb_Transit.Text = transit.ToString();
        lb_destination.Text = destination.ToString();


        double originPercent = (origin / (double)total) * 100;
        double transitPercent = (transit / (double)total) * 100;
        double destinationPercent = (destination / (double)total) * 100;

        double allShipmentPercent = 100 - (originPercent + transitPercent + destinationPercent);

        hiddenOriginPercent.Value = originPercent.ToString();
        hiddenTransitPercent.Value = transitPercent.ToString();
        hiddenDestinationPercent.Value = destinationPercent.ToString();
        hiddenTotalPercent.Value = allShipmentPercent.ToString();

    }
    public class ShipmentEvent
    {
        public string shipencrypt { get; set; }
        public string Containerno { get; set; }
        public string EventDate { get; set; }
        public string EventType { get; set; }

    }
    List<ShipmentEvent> shipmentEvents = new List<ShipmentEvent>();
    [WebMethod]
    public static List<ShipmentEvent> FetchShipmentData()
    {
        List<ShipmentEvent> shipmentEvents = new List<ShipmentEvent>();
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        CommonFunction CF = new CommonFunction();


        Decrypt DE = new Decrypt();
        try
        {
            string str_Org = "";
            string str_Email = SC.username;
            string str_userRole = SC.UserRole;
            string str_userid = SC.Userid;
            string str_Orgcode = "";

            if (str_userRole != "0")
            {
                str_Org = SC.Orgname;
                str_Orgcode = SC.Orgnamecode;

            }

            string ShipmentEventquery = "";
            if (str_userRole == "0")
            {
                ShipmentEventquery = "EXEC GetcalendarAdmin";

            }
            else if (str_userRole == "2")
            {
                str_Orgcode = SC.Orgnamecode;
                if (str_Orgcode == "0")
                {
                    ShipmentEventquery = "EXEC GetcalendarAdmin";
                }
                else
                {
                    ShipmentEventquery = "EXEC Getcalendarclient @clientCode='" + str_Orgcode + "'";
                }
            }
            else
            {
                ShipmentEventquery = "EXEC Getcalendarclient @clientCode='" + str_Orgcode + "'";

            }
            SqlCommand command = new SqlCommand(ShipmentEventquery);
            DataTable dt = DA.GetDataTable(command);
            dt.Columns.Add("shipencrptid", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                string shipid = row["icfd_jobref"].ToString();
                string Shipdecrpt = DE.Register(shipid);
                row["shipencrptid"] = Shipdecrpt;
                shipmentEvents.Add(new ShipmentEvent
                {
                    shipencrypt = row["shipencrptid"].ToString(),
                    Containerno = row["icfd_jobref"] is DBNull ? string.Empty : row["icfd_jobref"].ToString(),
                    EventDate = row["arrival"] is DBNull ? string.Empty : Convert.ToDateTime(row["arrival"]).ToString(("yyyy-MM-dd HH:mm:ss")),
                    EventType = "arrival"
                });

                // Explicit null check for 'delivery'
                if (row["delivery"] != DBNull.Value && row["delivery"] != null)
                {
                    shipmentEvents.Add(new ShipmentEvent
                    {
                        shipencrypt = row["shipencrptid"].ToString(),
                        Containerno = row["icfd_jobref"].ToString(),
                        EventDate = row["delivery"] is DBNull ? string.Empty : Convert.ToDateTime(row["delivery"]).ToString(("yyyy-MM-dd HH:mm:ss")),
                        EventType = "delivery"
                    });
                }

                // Explicit null check for 'shipped'
                if (row["shipped"] != DBNull.Value && row["shipped"] != null)
                {
                    shipmentEvents.Add(new ShipmentEvent
                    {
                        shipencrypt = row["shipencrptid"].ToString(),
                        Containerno = row["icfd_jobref"].ToString(),
                        EventDate = row["shipped"] is DBNull ? string.Empty : Convert.ToDateTime(row["shipped"]).ToString(("yyyy-MM-dd HH:mm:ss")),
                        EventType = "shipped"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went to wrong please contact support team");
        }
        return shipmentEvents;
    }

    private void Loadorigin()
    {
        string str_grouplist = "";
        if (str_userRole == "0")
        {
            str_grouplist = "select distinct spr_origin from shipmentProfilereport";
        }
        else if (str_userRole == "2")
        {
            if (str_Orgcode == "0")
            {
                str_grouplist = "select distinct spr_origin from shipmentProfilereport";
            }
            else
            {
                str_grouplist = "select distinct spr_origin from shipmentProfilereport  where spr_localclientcode like '%" + str_Orgcode + "%'";
            }
        }
        else
        {
            str_grouplist = "select distinct spr_origin from shipmentProfilereport  where spr_localclientcode like '%" + str_Orgcode + "%'";
        }
        SqlCommand command = new SqlCommand(str_grouplist);
        DataSet ds = DA.GetDataSet(command);
        if (ds != null && ds.Tables.Count > 0)
        {
            DD_origin.DataSource = ds.Tables[0];
            DD_origin.DataTextField = "spr_origin";
            DD_origin.DataValueField = "spr_origin";
            DD_origin.DataBind();
            DD_origin.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    //private void GetChartData()
    //{

    //    try
    //    {
    //        string str_query = "";
    //        DateTime dt_date = DateTime.Now;
    //        int month = dt_date.Month;
    //        int year = dt_date.Year;
    //        string getmonthwisedate = CF.getMonths(month, year);
    //        string getmonthwisemonth = CF.getMonthsname(month, year);

    //        if (str_userRole == "0")
    //        {
    //            if (DD_origin.SelectedValue == "0")
    //            {
    //                str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a ";

    //            }
    //            else
    //            {
    //                str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a where a.spr_origin='" + DD_origin.SelectedValue + "'";

    //            }
    //        }
    //        else if (str_userRole == "2")
    //        {
    //            if (str_Orgcode == "0")
    //            {
    //                if (DD_origin.SelectedValue == "0")
    //                {
    //                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a ";

    //                }
    //                else
    //                {
    //                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a where a.spr_origin='" + DD_origin.SelectedValue + "'";

    //                }
    //            }
    //            else
    //            {
    //                if (DD_origin.SelectedValue == "0")
    //                {
    //                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport  a  where a.spr_localclientcode = '" + str_Orgcode + "'";

    //                }
    //                else
    //                {
    //                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a  where a.spr_origin='" + DD_origin.SelectedValue + "' and a.spr_localclientcode='" + str_Orgcode + "' ";

    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (DD_origin.SelectedValue == "0")
    //            {
    //                str_query = "select " + getmonthwisedate + " from  shipmentProfilereport  a  where a.spr_localclientcode='" + str_Orgcode + "' ";

    //            }
    //            else
    //            {
    //                str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a  where a.spr_origin='" + DD_origin.SelectedValue + "' and a.spr_localclientcode='" + str_Orgcode + "' ";

    //            }
    //        }
    //        SqlCommand cmd = new SqlCommand(str_query);
    //        DataTable dt_barchart = DA.GetDataTable(cmd);
    //        if (dt_barchart.Rows.Count > 0)
    //        {
    //            string str_json = Convertdatabtable(dt_barchart);


    //            List<int> numbers = System.Text.Json.JsonSerializer.Deserialize<List<int>>(str_json);

    //            numbers.Reverse();

    //            string reversedJson = System.Text.Json.JsonSerializer.Serialize(numbers);

    //            lt_barchart.Text = JsonConvert.SerializeObject(reversedJson);
    //            string monthwisedata = "[" + getmonthwisemonth + "]";



    //            lt_barchartmonth.Text = JsonConvert.SerializeObject(monthwisedata);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    [WebMethod]
    public static string GetChartData(string origin)
    {
        SessionCustom SC = new SessionCustom();
        CommonFunction CF = new CommonFunction();
        DataAccess DA = new DataAccess();

        try
        {
            string str_userRole = SC.UserRole;
            string str_Orgcode = SC.Orgnamecode;


            string str_query = "";
            DateTime dt_date = DateTime.Now;
            int month = dt_date.Month;
            int year = dt_date.Year;
            string getmonthwisedate = CF.getMonths(month, year);
            string getmonthwisemonth = CF.getMonthsname(month, year);
            if (str_userRole == "0")
            {
                if (origin == "All")
                {
                    str_query = "SELECT " + getmonthwisedate + " FROM shipmentProfilereport a";
                }
                else
                {
                    str_query = "SELECT " + getmonthwisedate + " FROM shipmentProfilereport a WHERE a.spr_origin = @origin";
                }
            }
            //        {
            else if (str_userRole == "2")
            {
                if (str_Orgcode == "0")
                {
                    if (origin == "All")
                    {
                        str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a ";

                    }
                    else
                    {
                        str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a where a.spr_origin=@origin";

                    }
                }
                else
                {
                    if (origin == "All")
                    {
                        str_query = "select " + getmonthwisedate + " from  shipmentProfilereport  a  where a.spr_localclientcode = '" + str_Orgcode + "'";

                    }
                    else
                    {
                        str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a  where a.spr_origin=@origin and a.spr_localclientcode='" + str_Orgcode + "' ";

                    }
                }
            }
            else
            {
                if (origin == "All")
                {
                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport  a  where a.spr_localclientcode='" + str_Orgcode + "' ";

                }
                else
                {
                    str_query = "select " + getmonthwisedate + " from  shipmentProfilereport a  where a.spr_origin=@origin and a.spr_localclientcode='" + str_Orgcode + "' ";

                }
            }

            SqlCommand cmd = new SqlCommand(str_query);
            cmd.Parameters.AddWithValue("@origin", origin);  // Add the parameter to the query

            // Get the data
            DataTable dt_barchart = DA.GetDataTable(cmd);
            if (dt_barchart.Rows.Count > 0)
            {
                // Convert DataTable to JSON and get the values as a List of integers
                string str_json = Convertdatabtable(dt_barchart);
                List<int> numbers = System.Text.Json.JsonSerializer.Deserialize<List<int>>(str_json);
                numbers.Reverse();  // Reverse the data to match the correct order

                // Declare data (list of integers) and labels (array of months)
                var data = numbers;

                // Create the labels with single quotes around each month, but remove extra quotes
                var labels = "[" + getmonthwisemonth + "]";

                // Prepare the JSON response with the origin
                var response = new { data = data, labels = labels };
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                return "[]";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }



    private static string Convertdatabtable(DataTable dt_barchart)
    {
        var jsonObject = JsonConvert.SerializeObject(dt_barchart, Formatting.Indented);

        var listOfDicts = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(jsonObject);

        if (listOfDicts != null && listOfDicts.Count > 0)
        {
            var values = listOfDicts[0].Values;
            return JsonConvert.SerializeObject(values);
        }

        return "[]";
    }





}
