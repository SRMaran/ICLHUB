using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Web_Container : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    Decrypt DE;
    PhTemplate PH;
    CommonFunction CF;
    string userrole = "";
    string str_userid = "";
    string datefilter = "";
    string Startdate = "";
    string Enddate = "";
    string Eventtype = "";
    string str_Org = "";
    string str_Orgcode = "";
    string pin_data = "0";
    string selectvalue = "0";
    string str_container = "";
   DateTime currentDate = DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.sc = new SessionCustom();
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.CF = new CommonFunction();
        sc.lablename = "Containers";
        sc.headername = "Containers";
        userrole = sc.UserRole;
        str_userid = sc.Userid;

        if (userrole != "0")
        {
            str_Org = sc.Orgname;
            str_Orgcode = sc.Orgnamecode;
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
            Pin.Text = "0";
            Roledata.Text = userrole;
        }
        Eventtype = ddl_event.SelectedValue;
        if (Request.QueryString["id"] != null)
        {
            str_container = Request.QueryString["id"].ToString();
        }
        if (!IsPostBack)
        {
            //toDate.Attributes.Remove("min");
            //toDate.Attributes.Remove("max");

            //toDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime today = DateTime.Now;
            DateTime last7Days = today.AddDays(-6);
            //fromDate.Text = last7Days.ToString("yyyy-MM-dd");
            //fromDate.Attributes["max"] = today.ToString("yyyy-MM-dd");
            //Startdate = fromDate.Text;
            //Enddate = toDate.Text;
            //this.CF.DaterangeFilter(DD_Datefilter);
        }
    }
    
  
    protected void ddl_evettype_SelectedIndexChanged(object sender, EventArgs e)
    {
       /* datefilter = DD_Datefilter.SelectedValue*/;
        //Startdate = fromDate.Text;
        //Enddate = toDate.Text;
        Eventtype = ddl_event.SelectedValue;
        selectvalue = "1";
        Transport.Text = Eventtype;
        Pin.Text = pin_data;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + this.userrole + "," + str_Org + "," + Pin.Text + "');", true);

    }
    [WebMethod]
    public static string InsertContainer(string action, string containerid)
    {
        string query = "";
        string str_Orgcode = "";
        string checkshipvalue = "";
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string userid = SC.Userid;
        str_Orgcode = SC.Orgnamecode;
        string shipmentcheckcheck = "select icfd_container from ICl_PinnedContainer where icfd_container='" + containerid + "'";
        DataTable dt_shipment = DA.GetDataTable(shipmentcheckcheck);
        if (dt_shipment.Rows.Count > 0)
        {
            checkshipvalue = dt_shipment.Rows[0]["icfd_container"].ToString();
        }

        if (checkshipvalue == "")
        {
            action = "Inserted";
            query = "insert into ICl_PinnedContainer(icfd_container,pc_createdby,ClientCode)values(@icfd_container,@pc_createdby,@ClientCode) ";

        }
        else
        {
            action = "Deleted";
            query = "DELETE FROM ICl_PinnedContainer WHERE icfd_container=@icfd_container";
        }
        try
        {
            SqlCommand cmd = new SqlCommand(query);
         
            cmd.Parameters.AddWithValue("@icfd_container", containerid);
            cmd.Parameters.AddWithValue("@ClientCode", str_Orgcode);
            cmd.Parameters.AddWithValue("@pc_createdby", userid);

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
    public static object GetShipmentDetails(int start, int length, int draw, string searchValue, string Transport, string Role, string Clientcode, string orderColumn, string orderDir,string Pin)
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
                    ascanddesc = "@controldata='" + controldata + "',@SORT='icfd_eta',@DESC_ASC='" + orderDir + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "',@SORT='" + orderColumn + "',@DESC_ASC='" + orderDir + "'";
                }
                if (searchValue == "")
                {
                    // Get total record count
                    string countQuery = "EXEC get_year_of_containerRowCount @DD_status='" + Transport + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC Get_year_of_ContainerRowData @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Clientcode='" + Clientcode + "'," + ascanddesc + ",@Pin='"+ Pin + "'";
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
                    string countQuery = "EXEC Get_Search_Rowcount @search='" + searchValue + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC Get_Search_container_Rowdata @search='" + searchValue + "',@Start='" + start + "',@Length='" + length + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("icon", typeof(string));
                    dt.Columns.Add("containerencrptid", typeof(string));
                    dt.Columns.Add("ShipmentNo", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        // Decrypt container ID
                        string containerid = row["icfd_container"].ToString();
                        string icfd_jobref = row["icfd_jobref"].ToString();
                        string containerdecrpt = DE.Register(containerid);
                        string icfd_jobrefdecrpt = DE.Register(icfd_jobref);
                        row["containerencrptid"] = containerdecrpt;
                        row["ShipmentNo"] = icfd_jobrefdecrpt;

                        string Currentdate = row["pc_createdon"].ToString();
                        string icon = row["icon"].ToString();
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
                    containerencrptid = row["containerencrptid"].ToString(),
                    ShipmentNo = row["ShipmentNo"].ToString(),
                    icfd_container = row["icfd_container"].ToString(),
                    icfd_jobref = row["icfd_jobref"].ToString(),
                    icfd_masterref = row["icfd_masterref"].ToString(),
                    icfd_conttype = row["icfd_conttype"].ToString(),
                    icfd_contmode = row["icfd_contmode"].ToString(),
                    icfd_eta = row["icfd_eta"].ToString(),
                    icfd_estimateddeliver = row["icfd_estimateddeliver"].ToString(),
                    icfd_actualdeliver = row["icfd_actualdeliver"].ToString(),
                    icfd_emptyreturned = row["icfd_emptyreturned"].ToString(),
                    icfd_daysfrometatoavailability = row["icfd_daysfrometatoavailability"].ToString()

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
        Eventtype = ddl_event.SelectedValue;
        Transport.Text = Eventtype;
        Pin.Text = pin_data;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + this.userrole + "," + str_Org + "," + Pin.Text + "');", true);



    }

    //filter
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetailsfilter(int start, int length, int draw, string searchValue, string Transport,string Startdate,string Enddate, string Role, string Clientcode, string orderColumn)
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
                    string countQuery = "EXEC get_year_of_containerRowCountdatefilter @DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC Get_year_of_ContainerRowDatadatefilter @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "',@Clientcode='" + Clientcode + "'," + ascanddesc + "";
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
                    string countQuery = "EXEC Get_Search_Rowcountsearch @search='" + searchValue + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC Get_Search_container_Rowdatasearch @search='" + searchValue + "',@Start='" + start + "',@Length='" + length + "',@Clientcode='" + Clientcode + "',@controldata='" + controldata + "',@Startdate='" + Startdate + "',@Enddate='" + Enddate + "'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        das = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        das.Fill(dt);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("icon", typeof(string));
                    dt.Columns.Add("containerencrptid", typeof(string));
                    dt.Columns.Add("ShipmentNo", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        // Decrypt container ID
                        string containerid = row["icfd_container"].ToString();
                        string icfd_jobref = row["icfd_jobref"].ToString();
                        string containerdecrpt = DE.Register(containerid);
                        string icfd_jobrefdecrpt = DE.Register(icfd_jobref);
                        row["containerencrptid"] = containerdecrpt;
                        row["ShipmentNo"] = icfd_jobrefdecrpt;

                        string Currentdate = row["pc_createdon"].ToString();
                        string icon = row["icon"].ToString();
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
                    containerencrptid = row["containerencrptid"].ToString(),
                    ShipmentNo = row["ShipmentNo"].ToString(),
                    icfd_container = row["icfd_container"].ToString(),
                    icfd_jobref = row["icfd_jobref"].ToString(),
                    icfd_masterref = row["icfd_masterref"].ToString(),
                    icfd_conttype = row["icfd_conttype"].ToString(),
                    icfd_contmode = row["icfd_contmode"].ToString(),
                    icfd_eta = row["icfd_eta"].ToString(),
                    icfd_estimateddeliver = row["icfd_estimateddeliver"].ToString(),
                    icfd_actualdeliver = row["icfd_actualdeliver"].ToString(),
                    icfd_emptyreturned = row["icfd_emptyreturned"].ToString(),
                    icfd_daysfrometatoavailability = row["icfd_daysfrometatoavailability"].ToString()

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