using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Reflection;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;

public partial class Web_cus : System.Web.UI.Page
{
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    CommonFunction CF;
    SessionCustom SC;
    string userrole = "";
    string str_userid = "";
    string datefilter = "";
    string Startdate = "";
    string Enddate = "";
    string Eventtype = "";
    DateTime currentDate = DateTime.Now;
    string tablename = "";
    string str_Orgcode = "";
    string str_Org = "";
    string value = "0";
    string filterdata = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.da = new DataAccess();
        this.PH = new PhTemplate();
        this.DE = new Decrypt();
        this.CF = new CommonFunction();
        this.SC = new SessionCustom();
        SC.lablename = "Customs";
        SC.headername = "Customs";
        userrole = SC.UserRole;
        str_userid = SC.Userid;

        if (userrole != "0")
        {
            str_Org = SC.Orgname;
            str_Orgcode = SC.Orgnamecode;
            Roledata.Text = userrole;
            codedata.Text = str_Orgcode;
            Transport.Text = "0";
            pin.Text = "0";
        }
        else
        {
            Transport.Text = "0";
            pin.Text = "0";
            Roledata.Text = userrole;



        }
        Eventtype = ddl_typeevent.SelectedValue;
        
    }



    protected void ddl_evettype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Eventtype = ddl_typeevent.SelectedValue;
        filterdata = "1";
        Roledata.Text = SC.UserRole;
        codedata.Text = SC.Orgnamecode;
        Transport.Text = Eventtype;

        pin.Text = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + userrole + "," + codedata + "," + pin.Text + "');", true);
    }
    [WebMethod]
    public static string InsertCustom(string action, string customjob)
    {
        string query = "";
        string str_Orgcode = "";
        string checkshipvalue = "";
        DataAccess DA = new DataAccess();
        SessionCustom SC = new SessionCustom();
        string userid = SC.Userid;
        str_Orgcode = SC.Orgnamecode;
        string shipmentcheckcheck = "select gbi_job from ICL_Pinnedcustoms where gbi_job='" + customjob + "'";
        DataTable dt_shipment = DA.GetDataTable(shipmentcheckcheck);
        if (dt_shipment.Rows.Count > 0)
        {
            checkshipvalue = dt_shipment.Rows[0]["gbi_job"].ToString();
        }

        if (checkshipvalue == "")
        {
            action = "Inserted";
            query = "insert into ICL_Pinnedcustoms(gbi_job,pc_createdby,ClientCode)values(@gbi_job,@pc_createdby,@ClientCode) ";

        }
        else
        {
            action = "Deleted";
            query = "DELETE FROM ICL_Pinnedcustoms WHERE gbi_job=@gbi_job";
        }
        try
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@gbi_job", customjob);
            cmd.Parameters.AddWithValue("@ClientCode", customjob);
            cmd.Parameters.AddWithValue("@pc_createdby", userid);

            DA.ExecuteNonQuery(cmd);
            return "Success";
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
            value = "0";
            btn.CssClass = "btn btn-outline-warning me-1 mb-1 pinned-shipmnets-btn";
        }
        else
        {
            pinnedRows.Add(bookingId);
            value = "1";
            btn.CssClass = "btn btn-warning me-1 mb-1 pinned-shipmnets-btn ";
        }
        Eventtype = ddl_typeevent.SelectedValue;
        Roledata.Text = SC.UserRole;
        codedata.Text = SC.Orgnamecode;
        Transport.Text = Eventtype;

        pin.Text = value;
        Session["PinnedRows"] = pinnedRows;
        filterdata = "0";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "callAjax", "initializeDataTable('" + Eventtype + "," + userrole + "," + codedata + ","+ pin.Text + "');", true);

    }
    //protected void btnApply_Click(object sender, EventArgs e)
    //{
    //    datefilter = DD_Datefilter.SelectedValue;
    //    Startdate = fromDate.Text;
    //    Enddate = toDate.Text;
    //    Eventtype = ddl_typeevent.SelectedValue;
    //    filterdata = "1";
       
    //}

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetShipmentDetails(int start, int length, int draw, string searchValue, string Transport, string Role, string Clientcode, string orderColumn, string orderDir,string pin)
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
                    ascanddesc = "@controldata='" + controldata + "',@SORT='gbi_etdorigin',@DESC_ASC='" + orderDir + "'";
                }
                else
                {
                    ascanddesc = "@controldata='" + controldata + "',@SORT='" + orderColumn + "',@DESC_ASC='" + orderDir + "'";
                }
                if (searchValue == "")
                {
                    // Get total record count
                    string countQuery = "EXEC getcustomeralldata @DD_status='" + Transport + "',@localclientcode='" + Clientcode + "',@role='" + controldata + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC getcustomsRowdatas @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@localclientcode='" + Clientcode + "'," + ascanddesc + ",@pin='"+ pin + "'";
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
                    string countQuery = "EXEC getcustomeralldatasearch @DD_status='" + Transport + "',@localclientcode='" + Clientcode + "',@role='" + controldata + "',@search='" + searchValue + "'";
                    SqlCommand countCmd = new SqlCommand(countQuery, conn);
                    object countResult = countCmd.ExecuteScalar();
                    totalRecords = countResult != null ? Convert.ToInt32(countResult) : 0;
                    string query = "EXEC getcustomsRowdatasearch @Start='" + start + "', @Length='" + length + "',@DD_status='" + Transport + "',@localclientcode='" + Clientcode + "'," + ascanddesc + ",@search='" + searchValue + "'";
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
                    dt.Columns.Add("Custencrptid", typeof(string));
                    dt.Columns.Add("department", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        string Custid = row["jobID"].ToString();
                        string depid = row["Dep"].ToString();
                        string custdecrpt = DE.Register(Custid);
                        string depiddecrpt = DE.Register(depid);
                        row["Custencrptid"] = custdecrpt;
                        row["department"] = depiddecrpt;

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
                    //mode = row["mode"].ToString(),
                    Custencrptid = row["Custencrptid"].ToString(),
                    department = row["Dep"].ToString(),
                    gbi_job = row["jobID"].ToString(),
                    Incoterm = row["Incoterm"].ToString(),
                    ETA = row["ETA"].ToString(),
                    gbi_entrytype1 = row["gbi_entrytype1"].ToString(),
                    gbi_origin15 = row["gbi_origin15"].ToString(),
                    gbi_etdorigin = row["gbi_etdorigin"].ToString(),
                    gbi_destination17 = row["gbi_destination17"].ToString(),
                    gbi_arrivaldate = row["gbi_arrivaldate"].ToString(),
                    gbi_entrynumbers = row["gbi_entrynumbers"].ToString(),
                    gbi_status = row["gbi_status"].ToString(),
                    gbi_statusdescription = row["gbi_statusdescription"].ToString(),
                    gbi_goodsdescription = row["gbi_goodsdescription"].ToString(),
                    gbi_housebill = row["gbi_housebill"].ToString(),
                    gbi_clearancedate = row["gbi_clearancedate"].ToString(),
                    clearancecommenceddate = row["gbi_clearancecommenceddate"].ToString(),
                    gbi_supplier = row["gbi_supplier"].ToString(),
                    gbi_importer = row["gbi_importer"].ToString(),
                    gbi_contmode = row["gbi_contmode"].ToString(),
                    gbi_packs6 = row["gbi_packs6"].ToString(),
                    gbi_vessel21 = row["gbi_vessel21"].ToString(),
                    gbi_voyflight = row["gbi_voyflight"].ToString(),
                    gbi_masterbill = row["gbi_masterbill"].ToString(),
                    gbi_weight = row["gbi_weight"].ToString(),
                    gbi_vol = row["gbi_vol"].ToString(),
                    gbi_branch = row["gbi_branch"].ToString(),
                    transmode = row["transmode"].ToString()

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




