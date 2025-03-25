using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Web_Booking : System.Web.UI.Page
{
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    CommonFunction CF;
    SessionCustom SC;
    string userRole = "";
    string datefilter = "";
    string Startdate = "";
    string Enddate = "";
    string Eventtype = "";
    string str_userid = "";
    string str_Org = "";
    string str_booking = "";
    string str_Orgcode = "";
    string selectvalue = "0";
    string pin_data = "0";
    DateTime currentDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.CF = new CommonFunction();
        this.SC = new SessionCustom();
        DateTime currentDate = DateTime.Now;
        SC.lablename = "Bookings";
        SC.headername = "Bookings";
        this.str_userid = SC.Userid;
        this.userRole = SC.UserRole;

        if (this.userRole != "0")
        {
            str_Org = SC.Orgname;
            str_Orgcode = SC.Orgnamecode;
            Roledata.Text = this.userRole;
            codedata.Text = str_Orgcode;
            Transport.Text = "0";
            datefilters.Text = "0";
            Pin.Text = "0";

        }
        else
        {
            Transport.Text = "0";
            datefilters.Text = "0";
            Roledata.Text = this.userRole;
            Pin.Text = "0";

        }

        Eventtype = DD_status.SelectedValue;
        if (Request.QueryString["id"] != null)
        {
            str_booking = Request.QueryString["id"].ToString();
        }
        if (!IsPostBack)
        {

            string currentDateString = currentDate.ToString("yyyy-MM-dd");
            Txt_Todate.Text = currentDateString;
            Txt_Fromdate.Text = currentDateString;
            DateTime today = DateTime.Now;
            DateTime last7Days = today.AddDays(-6);
            Txt_Fromdate.Text = last7Days.ToString("yyyy-MM-dd");
            Txt_Fromdate.Attributes["max"] = today.ToString("yyyy-MM-dd");
            Startdate = Txt_Fromdate.Text;
            Enddate = Txt_Todate.Text;
            this.CF.DaterangeFilter(DD_Datefilter);
        }
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
            Startdate = Txt_Fromdate.Text;
            Enddate = Txt_Todate.Text;
        }
        else if (datefilter == "1")
        {
            Startdate = currentDateString;
            Enddate = currentDateString;
            Txt_Todate.Text = currentDateString;
            Txt_Fromdate.Text = currentDateString;
        }
        else if (datefilter == "2")
        {
            Startdate = lastyesterday.ToString("yyyy-MM-dd");
            Enddate = lastyesterday.ToString("yyyy-MM-dd");
            Txt_Todate.Text = lastyesterday.ToString("yyyy-MM-dd");
            Txt_Fromdate.Text = lastyesterday.ToString("yyyy-MM-dd");
        }
        else if (datefilter == "3")
        {
            Startdate = last7DaysDate.ToString("yyyy-MM-dd");
            Enddate = currentDateString;
            Txt_Todate.Text = currentDateString;
            Txt_Fromdate.Text = last7DaysDate.ToString("yyyy-MM-dd");
        }
        else if (datefilter == "4")
        {
            Startdate = last30DaysDateString;
            Enddate = currentDateString;
            Txt_Todate.Text = currentDateString;
            Txt_Fromdate.Text = last30DaysDateString;
        }
        else if (datefilter == "5")
        {
            Startdate = last60DaysDate.ToString("yyyy-MM-dd");
            Enddate = currentDateString;
            Txt_Todate.Text = currentDateString;
            Txt_Fromdate.Text = last60DaysDate.ToString("yyyy-MM-dd");
        }
        datefilter = DD_Datefilter.SelectedValue;
        datefilters.Text = "1";
        Startdates.Text = Startdate;
        Enddates.Text = Enddate;
        str_Orgcode = SC.Orgnamecode;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTablefilter('" + Eventtype + "," + Startdate + "," + Enddate + "," + this.userRole + "," + str_Org + "');", true);
        // this.BookingGrid();
    }

    protected void DD_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        datefilter = DD_Datefilter.SelectedValue;
        Startdate = Txt_Fromdate.Text;
        Enddate = Txt_Todate.Text;
        Eventtype = DD_status.SelectedValue;
        Transport.Text = Eventtype;
        selectvalue = "1";
        Pin.Text = pin_data;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + this.userRole + "," + str_Org + "," + Pin.Text + "');", true);
        //this.BookingGrid();
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
        Enddate = Txt_Todate.Text;
        Eventtype = DD_status.SelectedValue;
        Transport.Text = Eventtype;
        Pin.Text = pin_data;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + this.userRole + "," + str_Org + "," + Pin.Text + "');", true);

    }
    [WebMethod]
    public static string InsertBooking(string action, string bookid)
    {
        string query = "";
        string str_Orgcode = "";
        string checkshipvalue = "";
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string userid = SC.Userid;
        str_Orgcode = SC.Orgnamecode;
        string shipmentcheckcheck = "select eb_bookingid from ICL_PinnedBooking where eb_bookingid='" + bookid + "'";
        DataTable dt_shipment = DA.GetDataTable(shipmentcheckcheck);
        if (dt_shipment.Rows.Count > 0)
        {
            checkshipvalue = dt_shipment.Rows[0]["eb_bookingid"].ToString();
        }

        if (checkshipvalue == "")
        {
            action = "Inserted";
            query = "insert into ICL_PinnedBooking(eb_bookingid,pb_createdby,ClientCode)values(@eb_bookingid,@pb_createdby,@ClientCode) ";

        }
        else
        {
            action = "Deleted";
            query = "DELETE FROM ICL_PinnedBooking WHERE eb_bookingid=@eb_bookingid";
        }
        try
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@eb_bookingid", bookid);
            cmd.Parameters.AddWithValue("@ClientCode", str_Orgcode);
            cmd.Parameters.AddWithValue("@pb_createdby", userid);
            DA.ExecuteNonQuery(cmd);
            return "Success";
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetails(int start, int length, int draw, string searchValue, string Transport, string Role, string Clientcode, string Pins, string orderColumn, string orderDir)
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
                    ascanddesc = "@controldata='" + controldata + "',@SORT='eb_bookingetd',@DESC_ASC='" + orderDir + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "',@SORT='" + orderColumn + "',@DESC_ASC='" + orderDir + "'";
                }
                if (searchValue == "")
                {
                    // Get total record count
                    string countQuery = "EXEC GetConfirmedbookingcount @DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@Pin='" + Pins + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetConfirmedbookingRowData @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@Pin='" + Pins + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }
                else
                {
                    // Get total record count
                    string countQuery = "EXEC GetConfirmedbookingcountSearch @DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@Pin='" + Pins + "',@search='" + searchValue + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetConfirmedbookingRowDatasearch @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@Pin='" + Pins + "',@search='" + searchValue + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {

                    dt.Columns.Add("Bookencrptid", typeof(string));
                    dt.Columns.Add("icon", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        string Bookid = row["eb_bookingid"].ToString();
                        string Bookdecrpt = DE.Register(Bookid);
                        row["Bookencrptid"] = Bookdecrpt;

                        string Currentdate = row["pb_createdon"].ToString();
                        if (Currentdate == "" || Currentdate == null)
                        {
                            row["icon"] = "feather-star";
                        }
                        else
                        {
                            row["icon"] = "fa fa-star";
                        }
                    }
                }

                var shipments = dt.AsEnumerable().Select(row => new
                {
                    icon = row["icon"].ToString(),
                    Bookencrptid = row["Bookencrptid"].ToString(),
                    eb_bookingid = row["eb_bookingid"].ToString(),
                    eb_shippersref = row["eb_shippersref"].ToString(),
                    mode = row["mode"].ToString(),
                    eb_mode = row["eb_mode"].ToString(),
                    eb_origin = row["eb_origin"].ToString(),
                    eb_bookingetd = row["eb_bookingetd"].ToString(),
                    eb_consignorname = row["eb_consignorname"].ToString(),
                    eb_consigneename = row["eb_consigneename"].ToString(),
                    eb_dest = row["eb_dest"].ToString(),
                    eb_bookingeta = row["eb_bookingeta"].ToString(),
                    eb_carriername = row["eb_carriername"].ToString(),
                    eb_vessel = row["eb_vessel"].ToString(),
                    eb_voyageflight = row["eb_voyageflight"].ToString(),
                    eb_booked = row["eb_booked"].ToString(),
                    eb_weight = row["eb_weight"].ToString(),
                    eb_volume = row["eb_volume"].ToString(),
                    eb_teu = row["eb_teu"].ToString(),
                    eb_concount = row["eb_concount"].ToString(),
                    eb_packs = row["eb_packs"].ToString(),
                    eb_type = row["eb_type"].ToString(),
                    eb_cargodescription = row["eb_cargodescription"].ToString()


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
    protected void btnApply_Click(object sender, EventArgs e)
    {
        datefilter = DD_Datefilter.SelectedValue;
        Startdate = Txt_Fromdate.Text;
        Enddate = Txt_Todate.Text;
        Eventtype = DD_status.SelectedValue;
        DD_Datefilter.SelectedValue = "0";
        selectvalue = "1";
        datefilters.Text = "1";
        Startdates.Text = Startdate;
        Enddates.Text = Enddate;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTablefilter('" + Eventtype + ","+ Startdate+"," + Enddate + "," +  this.userRole + "," + str_Org + "');", true);
    }
    ///date filter
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetailsfilter(int start, int length, int draw, string searchValue, string Transport,string Startdate,string Enddate,string Role, string orderColumn, string Clientcode)
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
                    ascanddesc = "@controldata='" + controldata + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "'";
                }
                if (searchValue == "")
                {
                    // Get total record count
                    string countQuery = "EXEC GetConfirmedbookingcountdatefilter @DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetConfirmedbookingRowDatadatefilter @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "'," + ascanddesc + "";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }
                else
                {
                    // Get total record count
                    string countQuery = "EXEC GetConfirmedbookingcountsearchdatefilter @DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@search='" + searchValue + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetConfirmedbookingRowDatasearchdatefilter @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@search='" + searchValue + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {

                    dt.Columns.Add("Bookencrptid", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        string Bookid = row["eb_bookingid"].ToString();
                        string Bookdecrpt = DE.Register(Bookid);
                        row["Bookencrptid"] = Bookdecrpt;
                    }
                }

                var shipments = dt.AsEnumerable().Select(row => new
                {
                    icon = row["pb_createdon"].ToString(),
                    Bookencrptid = row["Bookencrptid"].ToString(),
                    eb_bookingid = row["eb_bookingid"].ToString(),
                    eb_shippersref = row["eb_shippersref"].ToString(),
                    mode = row["mode"].ToString(),
                    eb_mode = row["eb_mode"].ToString(),
                    eb_origin = row["eb_origin"].ToString(),
                    eb_bookingetd = row["eb_bookingetd"].ToString(),
                    eb_consignorname = row["eb_consignorname"].ToString(),
                    eb_consigneename = row["eb_consigneename"].ToString(),
                    eb_dest = row["eb_dest"].ToString(),
                    eb_bookingeta = row["eb_bookingeta"].ToString(),
                    eb_carriername = row["eb_carriername"].ToString(),
                    eb_vessel = row["eb_vessel"].ToString(),
                    eb_voyageflight = row["eb_voyageflight"].ToString(),
                    eb_booked = row["eb_booked"].ToString(),
                    eb_weight = row["eb_weight"].ToString(),
                    eb_volume = row["eb_volume"].ToString(),
                    eb_teu = row["eb_teu"].ToString(),
                    eb_concount = row["eb_concount"].ToString(),
                    eb_packs = row["eb_packs"].ToString(),
                    eb_type = row["eb_type"].ToString(),
                    eb_cargodescription = row["eb_cargodescription"].ToString()


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