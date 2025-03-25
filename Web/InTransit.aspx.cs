using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Web_InTransit : System.Web.UI.Page
{
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    CommonFunction CF;
    SessionCustom SC;
    string Startdate = "";
    string Enddate = "";
    string EventType = "";
    string datefilter = "";
    DateTime currentDate = DateTime.Now;
    string userid = "";
    string userrole = "";
    string str_Orgcode = "";
    string str_type = "";
    string pin_data = "0";
    string str_Org = "0";
    string selectvalue = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.CF = new CommonFunction();
        this.SC = new SessionCustom();
        SC.lablename = "Shipments";
        SC.headername = "Shipments";
        this.userid = SC.Userid;
        userrole = SC.UserRole;
        EventType = ddl_evettype.SelectedValue;
        if (userrole != "0")
        {
            str_Org = SC.Orgname;
            str_Orgcode = SC.Orgnamecode;
            Roledata.Text = userrole;
            codedata.Text = str_Orgcode;
            Transport.Text = "0";
            datefilters.Text = "0";
            Pin.Text = "0";
        }
        else
        {
            Transport.Text = "0";
            datefilters.Text = "0";
            Roledata.Text = this.userrole;
            Pin.Text = "0";

        }
        if (Request.QueryString["id"] != null)
        {
            str_type = Request.QueryString["id"].ToString();
        }
        if (!IsPostBack)
        {
            string currentDateString = currentDate.ToString("yyyy-MM-dd");
            toDate.Text = currentDateString;
            fromDate.Text = currentDateString;
            Startdate = fromDate.Text;
            Enddate = toDate.Text;
            evenbooking.Visible = true;
            this.CF.DaterangeFilter1(DD_Datefilter);
        }
        
        evenbooking.HRef = "EventBooking.aspx?id=TRANSITBOOKING";
    }

    protected void DD_Datefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime last30DaysDate = currentDate.AddDays(-29);
        DateTime last7DaysDate = currentDate.AddDays(-6);
        DateTime last60DaysDate = currentDate.AddDays(-59);
        DateTime lastyesterday = currentDate.AddDays(-1);
        string currentDateString = currentDate.ToString("yyyy-MM-dd");
        string last30DaysDateString = last30DaysDate.ToString("yyyy-MM-dd");
        datefilter = DD_Datefilter.SelectedValue;
        if (datefilter == "0")
        {
            Startdate = fromDate.Text;
            Enddate = toDate.Text;
        }
        else if (datefilter == "1")
        {
            Startdate = currentDateString;
            Enddate = currentDateString;
            toDate.Text = currentDateString;
            fromDate.Text = currentDateString;
        }
        else if (datefilter == "2")
        {
            Startdate = lastyesterday.ToString("yyyy-MM-dd");
            Enddate = lastyesterday.ToString("yyyy-MM-dd");
            toDate.Text = lastyesterday.ToString("yyyy-MM-dd");
            fromDate.Text = lastyesterday.ToString("yyyy-MM-dd");
        }
        else if (datefilter == "3")
        {
            Startdate = last7DaysDate.ToString("yyyy-MM-dd");
            Enddate = currentDateString;
            toDate.Text = currentDateString;
            fromDate.Text = last7DaysDate.ToString("yyyy-MM-dd");
        }
        else if (datefilter == "4")
        {
            Startdate = last30DaysDateString;
            Enddate = currentDateString;
            toDate.Text = currentDateString;
            fromDate.Text = last30DaysDateString;
        }
        else if (datefilter == "5")
        {
            Startdate = last60DaysDate.ToString("yyyy-MM-dd");
            Enddate = currentDateString;
            toDate.Text = currentDateString;
            fromDate.Text = last60DaysDate.ToString("yyyy-MM-dd");
        }
        EventType = ddl_evettype.SelectedValue;
        selectvalue = "1";
        DD_Datefilter.SelectedValue = "0";
        datefilters.Text = "1";
        Startdates.Text = Startdate;
        Enddates.Text = Enddate;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTablefilter('" + EventType + "," + Startdate + "," + Enddate + "," + this.userrole + "," + str_Org + "');", true);
    }

    protected void ddl_evettype_SelectedIndexChanged(object sender, EventArgs e)
    {
        datefilter = DD_Datefilter.SelectedValue;
        Startdate = fromDate.Text;
        Enddate = toDate.Text;
        EventType = ddl_evettype.SelectedValue;
        selectvalue = "1";
        Transport.Text = EventType;
        Pin.Text = pin_data;
        userrole = SC.UserRole;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + EventType + "," + userrole + "," + str_Org + "');", true);
    }
    [WebMethod]
    public static string InsertShipment(string action, string shipmentid)
    {
        string query = "";
        string checkshipvalue = "";
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string userid = SC.Userid;
        string str_Orgcode = SC.Orgnamecode;
        string shipmentcheckcheck = "select spr_shipmentid from ICL_PinnedTransit where spr_shipmentid='" + shipmentid + "'";
        DataTable dt_shipment = DA.GetDataTable(shipmentcheckcheck);
        if (dt_shipment.Rows.Count > 0)
        {
            checkshipvalue = dt_shipment.Rows[0]["spr_shipmentid"].ToString();
        }
        if (checkshipvalue == "")
        {
            action = "Inserted";
            query = "insert into ICL_PinnedTransit(spr_shipmentid,ps_createdby,ClientCode)values(@spr_shipmentid,@ps_createdby,@ClientCode) ";
        }
        else
        {
            action = "Deleted";
            query = "DELETE FROM ICL_PinnedTransit WHERE spr_shipmentid=@spr_shipmentid";
        }
        try
        {
            SqlCommand cmd = new SqlCommand(query);

            cmd.Parameters.AddWithValue("@spr_shipmentid", shipmentid);
            cmd.Parameters.AddWithValue("@ps_createdby", userid);
            cmd.Parameters.AddWithValue("@ClientCode", str_Orgcode);
            DA.ExecuteNonQuery(cmd);
            return action;
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    protected void btnpinship_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string bookingId = btn.CommandArgument;

        List<string> pinnedRows = Session["PinnedRows"] as List<string> ?? new List<string>();

        if (pinnedRows.Contains(bookingId))
        {
            pinnedRows.Remove(bookingId);
            pin_data = "0";
            btn.CssClass = "btn btn-outline-warning me-1 mb-1 pinned-shipmnets-btn";
        }
        else
        {
            pinnedRows.Add(bookingId);
            pin_data = "1";
            btn.CssClass = "btn btn-warning me-1 mb-1 pinned-shipmnets-btn ";
        }

        Session["PinnedRows"] = pinnedRows;
        selectvalue = "0";
        datefilter = DD_Datefilter.SelectedValue;
        EventType = ddl_evettype.SelectedValue;
        selectvalue = "1";
        Transport.Text = EventType;
        Pin.Text = pin_data;
        userrole = SC.UserRole;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + EventType + "," + userrole + "," + str_Org + "');", true);
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        datefilter = DD_Datefilter.SelectedValue;
        Startdate = fromDate.Text;
        Enddate = toDate.Text;
        EventType = ddl_evettype.SelectedValue;
        selectvalue = "1";
        DD_Datefilter.SelectedValue = "0";
        datefilters.Text = "1";
        Startdates.Text = Startdate;
        Enddates.Text = Enddate;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTablefilter('" + EventType + "," + Startdate + "," + Enddate + "," + this.userrole + "," + str_Org + "');", true);
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetailsfilter(int start, int length, int draw, string searchValue, string Transport, string Startdate, string Enddate, string Role, string orderColumn, string Clientcode)
    {
        try
        {
            DataAccess da = new DataAccess();
            PhTemplate PH = new PhTemplate();
            Decrypt DE = new Decrypt();
            CommonFunction CF = new CommonFunction();
            SessionCustom SC = new SessionCustom();
            DataTable dt = new DataTable();
            string controldata = "";
            string ascanddesc = "";
            int totalRecords;

            using (SqlConnection conn = new SqlConnection(da.ConnectionString.ToString()))
            {
                conn.Open();

                if (Role == "0")
                {
                    controldata = "1";

                }
                if (Role == "2")
                {
                    if (Clientcode == "0")
                    {
                        controldata = "1";
                    }
                    else
                    {
                        controldata = "2";
                    }
                }
                else
                {
                    controldata = "2";
                }
                if (orderColumn == "icon")
                {
                    ascanddesc = "@controldata='" + controldata + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "'";
                }
                if (searchValue == "")
                {
                    // Get total record count
                    string countQuery = "EXEC GetAtTransitPortCountdatefilter @Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@controldata='" + controldata + "',@Clientcode='" + Clientcode + "',@DD_status='" + Transport + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetAtTransitPortCountdate @Start='" + start + "', @Length='" + length + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@DD_status='" + Transport + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }
                else
                {
                    // Get total record count
                    string countQuery = "EXEC GetAtTransitPortCountdatesearch @DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@search='" + searchValue + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetAtTransitPortCountdatefilterSearch @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@search='" + searchValue + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataAdapter das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("shipencryptid", typeof(string));
                    dt.Columns.Add("icon", typeof(string));
                    dt.Columns.Add("spr_tran", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string shipmentid = row["spr_shipmentid"].ToString();
                        row["shipencryptid"] = DE.Register(shipmentid);
                        string Currentdate = row["ps_createdon"].ToString();
                        if (Currentdate == "" || Currentdate == null)
                        {
                            row["icon"] = "feather-star";
                        }
                        else
                        {
                            row["icon"] = "fa fa-star";
                        }
                        string spr_trans = row["spr_trans"].ToString();
                        if (spr_trans == "SEA")
                        {
                            row["spr_tran"] = "ship";
                        }
                        if (spr_trans == "AIR")
                        {
                            row["spr_tran"] = "plane";
                        }
                        if (spr_trans == "ROA")
                        {
                            row["spr_tran"] = "truck";
                        }
                    }
                }

                var shipments = dt.AsEnumerable().Select(row => new
                {
                    icon = row["icon"].ToString(),
                    spr_tran = row["spr_tran"].ToString(),
                    shipencryptid = row["shipencryptid"].ToString(),
                    spr_shipmentid = row["spr_shipmentid"].ToString(),
                    spr_mode = row["spr_mode"].ToString(),
                    spr_shippersreference = row["spr_shippersreference"].ToString(),
                    spr_consignorname = row["spr_consignorname"].ToString(),
                    spr_origin = row["spr_origin"].ToString(),
                    spr_originetd = row["spr_originetd"].ToString(),
                    spr_consigneename = row["spr_consigneename"].ToString(),
                    spr_destination = row["spr_destination"].ToString(),
                    spr_destinationeta = row["spr_destinationeta"].ToString(),
                    spr_vessel = row["spr_vessel"].ToString(),
                    spr_houseref = row["spr_houseref"].ToString(),
                    spr_goodsdescription = row["spr_goodsdescription"].ToString(),
                    spr_estcartagedelivery = row["spr_estcartagedelivery"].ToString(),
                    spr_actualcartagedelivery = row["spr_actualcartagedelivery"].ToString(),
                    spr_flightvoyage = row["spr_flightvoyage"].ToString(),
                    spr_pickupby = row["spr_pickupby"].ToString(),
                    spr_actualpickup = row["spr_actualpickup"].ToString(),
                    icfd_container = row["icfd_container"].ToString(),
                    spr_carriername = row["spr_carriername"].ToString(),
                    spr_teu = row["spr_teu"].ToString(),
                    spr_weight = row["spr_weight"].ToString(),
                    spr_volume = row["spr_volume"].ToString()
                }).ToList();

                return new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = shipments
                };
            }

        }
        catch (Exception ex)
        {
            return new { error = ex.Message };
        }

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetails(int start, int length, int draw, string searchValue, string Transport, string Role, string Clientcode, string orderColumn, string orderDir, string Pin)
    {
        try
        {

            DataAccess da = new DataAccess();
            PhTemplate PH = new PhTemplate();
            Decrypt DE = new Decrypt();
            CommonFunction CF = new CommonFunction();
            SessionCustom SC = new SessionCustom();
            DataTable dt = new DataTable();
            SqlDataAdapter das = new SqlDataAdapter();
            string controldata = "";
            string ascanddesc = "";
            int totalRecords;

            using (SqlConnection conn = new SqlConnection(da.ConnectionString.ToString()))
            {
                conn.Open();

                if (Role == "0")
                {
                    controldata = "1";

                }
                if (Role == "2")
                {
                    if (Clientcode == "0")
                    {
                        controldata = "1";
                    }
                    else
                    {
                        controldata = "2";
                    }
                }
                else
                {
                    controldata = "2";
                }
                if (orderColumn == "icon")
                {
                    ascanddesc = "@controldata='" + controldata + "',@SORT='spr_originetd',@DESC_ASC='" + orderDir + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "',@SORT='" + orderColumn + "',@DESC_ASC='" + orderDir + "'";
                }
                if (searchValue == "")
                {
                    string countQuery = "EXEC GetAtTransitPortCount @DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetAtTransitPortCountData @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@Pin='" + Pin + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }
                else
                {
                    string countQuery = "EXEC GetAtTransitPortSearch @search='" + searchValue + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetAtTransitPortdatasearch @search='" + searchValue + "',@Start='" + start + "',@Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("shipencryptid", typeof(string));
                    dt.Columns.Add("icon", typeof(string));
                    dt.Columns.Add("spr_tran", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        string shipmentid = row["spr_shipmentid"].ToString();
                        row["shipencryptid"] = DE.Register(shipmentid);

                        string Currentdate = row["ps_createdon"].ToString();
                        if (Currentdate == "" || Currentdate == null)
                        {
                            row["icon"] = "feather-star";
                        }
                        else
                        {
                            row["icon"] = "fa fa-star";
                        }
                        string spr_trans = row["spr_trans"].ToString();
                        if (spr_trans == "SEA")
                        {
                            row["spr_tran"] = "ship";
                        }
                        if (spr_trans == "AIR")
                        {
                            row["spr_tran"] = "plane";
                        }
                        if (spr_trans == "ROA")
                        {
                            row["spr_tran"] = "truck";
                        }
                    }
                }

                var shipments = dt.AsEnumerable().Select(row => new
                {
                    icon = row["icon"].ToString(),
                    spr_tran = row["spr_tran"].ToString(),
                    shipencryptid = row["shipencryptid"].ToString(),
                    spr_shipmentid = row["spr_shipmentid"].ToString(),
                    spr_mode = row["spr_mode"].ToString(),
                    spr_shippersreference = row["spr_shippersreference"].ToString(),
                    spr_consignorname = row["spr_consignorname"].ToString(),
                    spr_origin = row["spr_origin"].ToString(),
                    spr_originetd = row["spr_originetd"].ToString(),
                    spr_consigneename = row["spr_consigneename"].ToString(),
                    spr_destination = row["spr_destination"].ToString(),
                    spr_destinationeta = row["spr_destinationeta"].ToString(),
                    spr_vessel = row["spr_vessel"].ToString(),
                    spr_houseref = row["spr_houseref"].ToString(),
                    spr_goodsdescription = row["spr_goodsdescription"].ToString(),
                    spr_estcartagedelivery = row["spr_estcartagedelivery"].ToString(),
                    spr_actualcartagedelivery = row["spr_actualcartagedelivery"].ToString(),
                    spr_flightvoyage = row["spr_flightvoyage"].ToString(),
                    spr_pickupby = row["spr_pickupby"].ToString(),
                    spr_actualpickup = row["spr_actualpickup"].ToString(),
                    icfd_container = row["icfd_container"].ToString(),
                    spr_carriername = row["spr_carriername"].ToString(),
                    spr_teu = row["spr_teu"].ToString(),
                    spr_weight = row["spr_weight"].ToString(),
                    spr_volume = row["spr_volume"].ToString()
                }).ToList();

                return new
                {
                    draw = draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = shipments
                };

            }

        }
        catch (Exception ex)
        {
            return new { error = ex.Message };
        }

    }
}