using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;

public partial class Web_Arriving : System.Web.UI.Page
{

    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    CommonFunction CF;
    SessionCustom SC;
    string Startdate = "";
    string Enddate = "";
    string EventType = "";
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

        if (!IsPostBack)
        {
            string currentDateString = currentDate.ToString("yyyy-MM-dd");
            evenbooking.Visible = true;
           
        }
        evenbooking.HRef = "EventBooking.aspx?id=Orginport";
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
            string dateOnly = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");

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
                    // Get total record count
                    string countQuery = "EXEC GetAdminOrgETADATA @Todaydate='" + dateOnly + "', @DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetAdminClientOrgETASHIPMENT @Todaydate='" + dateOnly + "',@Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@Pin='" + Pin + "'";
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
                    string countQuery = "EXEC GetClientOrgETASHIPMENTSEARCH @Todaydate='" + dateOnly + "',@search='" + searchValue + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC GetClientOrgETASERACHDATA @Todaydate='" + dateOnly + "',@search='" + searchValue + "',@Start='" + start + "',@Length='" + length + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
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
    protected void ddl_evettype_SelectedIndexChanged(object sender, EventArgs e)
    {

        EventType = ddl_evettype.SelectedValue;
        selectvalue = "1";
        evenbooking.Visible = false;
        Transport.Text = EventType;
        Pin.Text = pin_data;
        userrole = SC.UserRole;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + EventType + "," + userrole + "," + str_Org + "');", true);
    }
    [WebMethod]
    public static string InsertShipment(string action,string shipmentid)
    {
        string query = "";
        string checkshipvalue = "";
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string userid = SC.Userid;
      string  str_Orgcode = SC.Orgnamecode;
        string shipmentcheckcheck = "select spr_shipmentid from ICL_PinnedArriving where spr_shipmentid='" + shipmentid + "'";
        DataTable dt_shipment = DA.GetDataTable(shipmentcheckcheck);
        if (dt_shipment.Rows.Count > 0)
        {
            checkshipvalue = dt_shipment.Rows[0]["spr_shipmentid"].ToString();
        }
        if (checkshipvalue == "")
        {
            action = "Inserted";
            query = "insert into ICL_PinnedArriving(spr_shipmentid,ps_createdby,ClientCode)values(@spr_shipmentid,@ps_createdby,@ClientCode) ";
        }
        else
        {
            action = "Deleted";
            query = "DELETE FROM ICL_PinnedArriving WHERE spr_shipmentid=@spr_shipmentid";
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
        Enddate = Enddates.Text;
        EventType = ddl_evettype.SelectedValue;
        Transport.Text = EventType;
        Pin.Text = pin_data;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + EventType + "," + this.userrole + "," + str_Org + "," + Pin.Text + "');", true);

    }
}