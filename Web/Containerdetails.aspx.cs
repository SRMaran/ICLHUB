using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Activities.Statements;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Web.Services.Description;
using System.Web.Services;


public partial class Web_Containerdetails : System.Web.UI.Page
{
    SessionCustom sc;
    DataAccess da;
    PhTemplate PH;
    Decrypt DE;
    SessionCustom SC;
    CommonFunction CF;
    string queryId = "";
    string Containerdecrpt = "";
    string shipment = "";
    string shipmentdecrpt = "";
    string consolenumber = "";
    string carrinumber = "";
    string loading = "";
    string Discharge = "";
    DataTable jsondatatble = new DataTable();
    Eadaptor EA;
    Jsonvalue EV;
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

        if (Request.QueryString["key"] != null && Request.QueryString["key"] != "")
        {

            string sharelink = Request.QueryString["key"].ToString();
            string[] linksplit = sharelink.Split(',');

            this.queryId = linksplit[0].Trim();
            shipment = linksplit[1].Trim();

            if (!IsPostBack)
            {
                Containerdecrpt = DE.Login(queryId);
                shipmentdecrpt = DE.Login(shipment);
                containerno.Text = Containerdecrpt;
                this.Containergriddetails();

                SC.headername = "Containers";
                SC.lablename = "Containers" + " <span style='color:#112560;font-size:18px'>" + " | " + Containerdecrpt + "</span>";
            }
        }
    }






    private void Containergriddetails()
    {
        string str_xmlcode = "C:\\ICL\\Shipment.txt";
        string contents = "";

        string consolkey = shipmentdecrpt;
        //string consolkey = "SPIMSI00276960";

        using (StreamReader streamReader = new StreamReader(str_xmlcode, Encoding.UTF8))
        {
            contents = streamReader.ReadToEnd();
            contents = contents.Replace("%%key%%", consolkey);
            string contentresponse = this.EA.HTTPPostXMLMessage(contents);
            XDocument xmlDoc = XDocument.Parse(contentresponse);

            string Str_consol = "";
            string Str_carrier = "";
            string Str_Portofloading = "";
            string Actualdate = "";
            string Estimateddate = "";
            string Str_PortofDischarge = "";

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
                    Str_consol = theDataSet.Tables["Datasource"].Rows[0]["key"].ToString();

                    if (Str_consol == "")
                    {
                        Str_consol = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("Carrier"))
            {
                var columns = theDataSet.Tables["Carrier"].Columns;
                if (columns.Contains("CompanyName"))
                {
                    Str_carrier = theDataSet.Tables["Carrier"].Rows[0]["CompanyName"].ToString();
                    if (Str_carrier == "")
                    {
                        Str_carrier = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("PortOfLoading"))
            {
                var columns = theDataSet.Tables["PortOfLoading"].Columns;
                if (columns.Contains("Name"))
                {
                    string Str_Portofloading1 = theDataSet.Tables["PortOfLoading"].Rows[0]["Name"].ToString();
                    string Str_portloadingcode1 = theDataSet.Tables["PortOfLoading"].Rows[0]["Code"].ToString();
                    Str_Portofloading = Str_Portofloading1 + "(" + Str_portloadingcode1 + ")";
                    if (Str_Portofloading == "()")
                    {
                        Str_Portofloading = "NA";
                    }
                }
            }

            if (theDataSet.Tables.Contains("PortOfDischarge"))
            {
                var columns = theDataSet.Tables["PortOfDischarge"].Columns;
                if (columns.Contains("Name"))
                {
                    string Str_PortofDischarge1 = theDataSet.Tables["PortOfDischarge"].Rows[0]["Name"].ToString();
                    string Str_portdischargecode1 = theDataSet.Tables["PortOfDischarge"].Rows[0]["Code"].ToString();

                    Str_PortofDischarge = Str_PortofDischarge1 + "(" + Str_portdischargecode1 + ")";
                    if (Str_PortofDischarge == "()")
                    {
                        Str_PortofDischarge = "NA";
                    }
                }
            }
            if (theDataSet.Tables.Contains("Milestone"))
            {
                var columns = theDataSet.Tables["Milestone"].Columns;
                if (columns.Contains("ActualDate"))
                {
                    Actualdate = theDataSet.Tables["Milestone"].Rows[0]["ActualDate"].ToString();
                    Estimateddate = theDataSet.Tables["Milestone"].Rows[0]["EstimatedDate"].ToString();

                    if (Estimateddate == "")
                    {
                        Estimateddate = "NA";
                    }
                    if (Actualdate == "")
                    {
                        Actualdate = "NA";
                    }
                }
            }



            string str_containerno = containerno.Text;
        this.CF = new CommonFunction();
        string str_Containergriddetails = "EXEC Getcontainerdetails @containerID= '" + str_containerno + "'";
        SqlCommand cmd = new SqlCommand(str_Containergriddetails);
        DataTable dt_shipment = da.GetDataTable(cmd);
        DataSet ds = new DataSet();

        if (dt_shipment.Rows.Count > 0)
        {
            dt_shipment.Columns.Add("carrier", typeof(string));
            dt_shipment.Columns.Add("console", typeof(string));
            dt_shipment.Columns.Add("loading", typeof(string));
            dt_shipment.Columns.Add("discharge", typeof(string));
            dt_shipment.Columns.Add("Actualdate", typeof(string));
            dt_shipment.Columns.Add("Estimateddate", typeof(string));

          
            foreach (DataRow row in dt_shipment.Rows)
            {
                row["carrier"] = Str_carrier;
                row["console"] = Str_consol;
                row["loading"] = Str_Portofloading;
                row["discharge"] = Str_PortofDischarge;
                row["Actualdate"] = Actualdate;
                row["Estimateddate"] = Estimateddate;
            }
            ds.Merge(dt_shipment);

            string str_container = dt_shipment.Rows[0]["icfd_container"].ToString();
            SC.lablename = "Containers" + " <span style='color:gray;font-size:18px'>" + " | " + str_container + "</span>";
            this.PH.LoadGridItem(ds, PH_containerviewdetails, "containerviewdetails.txt", "");
            this.PH.LoadGridItem(ds, PH_containerviewdelivery, "containerviewdelivery.txt", "");
        }
    }
    }
    //protected void btn_submit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string str_containerno = containerno.Text;
    //        string str_support = ta_support.InnerHtml;

    //        if (str_support == "")
    //        {

    //        }
    //        string str_Containergriddetails = "SELECT  icfd_iD,icfd_masterref,icfd_container,icfd_contmode,icfd_conttype,icfd_jobref,icfd_jobtype,icfd_eta,icfd_estimateddeliver,icfd_actualdeliver,icfd_emptyreturned FROM dbo.swl_icfdreport where icfd_container= '" + str_containerno + "'";
    //        SqlCommand cmd = new SqlCommand(str_Containergriddetails);
    //        DataTable dt_shipment = da.GetDataTable(cmd);
    //        DataSet ds = new DataSet();
    //        ds.Merge(dt_shipment);
    //        if (dt_shipment.Rows.Count > 0)
    //        {
    //            string str_cont = dt_shipment.Rows[0]["icfd_container"].ToString();
    //            string str_mail = sc.username;

    //            string str_name = sc.Name;
    //            string email_fun = CF.Supportemail(str_mail, "ContainerSupportMail", "Message from ICL HUB Container Supporting Mail", str_cont, str_name, str_support);
    //            this.Containergriddetails();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        div_success.Visible = false;
    //        div_error.Visible = true;
    //        lbl_success.Text = ex.Message;
    //    }
    //}
    [WebMethod]
    public static string SupportMessage(string containerno, string message)
    {
        try
        {
            CommonFunction CF = new CommonFunction();
            SessionCustom SC = new SessionCustom();
            DataAccess DA = new DataAccess();

            if (string.IsNullOrWhiteSpace(containerno))
            {
                return "Error: Please enter a container number.";
            }

            string str_Containergriddetails = "SELECT icfd_iD, icfd_masterref, icfd_container, icfd_contmode, icfd_conttype, icfd_jobref, icfd_jobtype, icfd_eta, icfd_estimateddeliver, icfd_actualdeliver, icfd_emptyreturned FROM dbo.swl_icfdreport WHERE icfd_container = @containerno";

            SqlCommand cmd = new SqlCommand(str_Containergriddetails);
            cmd.Parameters.AddWithValue("@containerno", containerno); // Prevent SQL Injection

            DataTable dt_shipment = DA.GetDataTable(cmd);

            if (dt_shipment.Rows.Count > 0)
            {
                string str_cont = dt_shipment.Rows[0]["icfd_container"].ToString();
                string str_mail = SC.username;
                string companyname = SC.Org_names;
                string str_support = "https://internationalcargologistics.com/";
                string str_name = SC.Name;

                string email_fun = CF.Supportemail(str_mail, "ContainerSupportMail", "Message from ICL HUB Container Supporting Mail", str_cont, str_name, str_support, companyname, message);

                return "Success: Message sent successfully.";
            }
            else
            {
                return "Error: No data found for the provided container number.";
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}