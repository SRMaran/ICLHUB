<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="EventBooking.aspx.cs" Inherits="Web_EventBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
           
                                  <style>
    .pinned-shipmnets-btn {
        border: 1px solid #f4b43f;
        border-radius: 7px;
        color: #112560;
        font-family: proxima-nova, sans-serif;
        font-size: 14px;
        font-weight: 400;
        height: 32px;
        letter-spacing: 0;
        line-height: 17px;
        /*max-width: 154px;*/
        opacity: 1;
        /*padding: 7px;*/
        text-align: center;
        transition: all .3s ease-out;
        width: 100%;
        z-index: 2;
    }

        .pinned-shipmnets-btn:hover {
            background-color: #f4b43f;
            border-color: #f4b43f;
            color: #fff;
        }

    .event-types-btn {
        background-color: #1d64d6;
        color: white;
        border-radius: 7px;
        cursor: pointer;
        height: 32px;
        /*max-width: 154px;*/
        opacity: 1;
        /*padding: 7px 0;*/
        width: 100%;
        z-index: 2;
    }

        .event-types-btn option {
            background-color: white;
            color: black;
        }


    .Datafilter {
        background-color: #1d64d6;
        border-radius: 7px;
        color: white;
        font-family: proxima-nova, sans-serif;
        font-size: 14px;
        font-weight: 400;
        height: 32px;
        letter-spacing: 0;
        line-height: 17px;
        /*max-width: 154px;*/
        opacity: 1;
        /*padding: 7px;*/
        transition: all .3s ease-out;
        width: 100%;
        z-index: 2;
    }

        .Datafilter option {
            background-color: white;
            color: #112560;
        }

    .pinned-row {
        background-color: #f9f871;
        font-weight: bold;
    }

    .date-range-btn {
        border: 1px solid #1d64d6;
        border-radius: 7px;
        color: #112560;
        font-family: proxima-nova, sans-serif;
        font-size: 14px;
        font-weight: 400;
        height: 32px;
        letter-spacing: 0;
        line-height: 17px;
        /*max-width: 154px;*/
        opacity: 1;
        /*padding: 7px;*/
        text-align: center;
        transition: all .3s ease-out;
        width: 100%;
    }

    .date-range-btn {
        background-color: #1d64d6;
        border-color: #1d64d6;
        color: #fff;
    }


    .dataTables_filter {
        display: inline-block;
        margin-right: 10px;
        font-size: 12px;
    }

    .dataTables_wrapper .dataTables_filter {
        display: inline-block;
        margin-right: 15px;
    }

    .dataTables_length label {
        display: flex;
        align-items: center;
        gap: 8px;
        margin-top: 20px;
    }

    .dataTables_length select {
        width: auto;
    }
</style>
        <style>
    .dataTables_filter {
        position: relative;
    }

        .dataTables_filter input {
            padding-left: 30px;
            / Space for the icon / width: 250px;
            height: 36px;
            border-radius: 5px;
            border: 1px solid #ccc;
            outline: none;
        }

                         .search-icon {
        position: absolute;
        left: 20px;
        top: 65%;
        transform: translateY(-50%);
        color: #1d64d6;
    }
    .position-relative input{
        display: flex;
align-items: center;
gap: 8px;
margin-top: 20px;
text-align: center;
border: 1px solid #1d64d6;
border-radius: 7px;
    }
</style>
   
    <script>
               function navigateToBookingDetails(Bookencrptid) {
                   window.location.href = `Bookingdetails.aspx?key=${Bookencrptid}`;}
           </script>
    <script>
    function handleStarHover(element) {
    element.style.color = "orange";
}
function handleStarOut(element) {
    element.style.color = "gold"; 
}</script>
    <script>   
        function handleBookingAction(element, action) {
          
            const data = {
                action: action, // "insert" or "delete"
                bookid: element.getAttribute('data-bookingid')
            };
            $.ajax({
                type: "POST",
                url: "EventBooking.aspx/InsertBooking",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "Inserted") {
                        alert("Add pinned Booking!");
                        element.setAttribute('data-action', 'delete');
                    } else if (response.d === "Deleted") {
                        alert("Remove pinned Booking!");
                        element.setAttribute('data-action', 'insert');
                    } else {
                        window.location.replace(window.location.href);
                    }
                },
                error: function (error) {
                    console.error("Error handling Booking:", error);
                    alert("An error occurred. Please try again.");
                }
            });
        }
    </script>
      <script>
       
 document.addEventListener("DOMContentLoaded", function () {
     document.getElementById('<%= Txt_Fromdate.ClientID %>').addEventListener("change", function () {
             console.log("Start Date Selected:", this.value);
         });

     document.getElementById('<%= Txt_Todate.ClientID %>').addEventListener("change", function () {
         console.log("End Date Selected:", this.value);
     });
 });
          table.buttons().container().on('click', '#close-popover', function () {
              $('.dt-button-collection').hide(); // Hides the popover
          });
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <div class="row">
            <div class="row mb-12">
                <div class="col d-flex align-items-center">
                    <div class="login-header">
                             <h4 >
                                 <asp:Label ID="lableid" runat="server"></asp:Label>
                                                                                          <a ID="evenbooking" 
   runat="server" 
   CssClass="btn btn-outline-primary" 
   Style="color: white; background-color: #112560; border-radius: 16px; padding: 5px 20px;" 
   Visible="false">
   Go to Shipments at origin port
</a>

                                </h4>
                                                       
                        <p id="lablepara" runat="server"></p>
                  
                       <%-- <div class="form-group">
                            

                          <%--  <a href="Shipment.aspx" class="btn btn-outline-primary  me-1 mb-1" id="evenbooking" runat="server" visible="false">Go to Shiments at origin port</a>
                        </div>--%>
                    </div>
                </div>
            </div>
    </div>
      <div class="card-box">
       <div class="row"><div class="col-md-2"></div>
    <div class="col-md-2">
        <div class="form-group">
            <br />
            <asp:Button ID="btnpinship" runat="server" CssClass="btn btn-outline-warning me-1 mb-1 pinned-shipmnets-btn" Text="Pinned Shipments" Style="font-size: 14px; margin-top: 5px;" OnClick="btnpinship_Click"></asp:Button>
        </div>
    </div>
    <div class="col-md-2" >
        <div class="form-group">
            <label>Mode Of Transport :</label>
            <asp:DropDownList ID="DD_status" runat="server" CssClass="form-select p-1 Datafilter" OnSelectedIndexChanged="DD_status_SelectedIndexChanged" AutoPostBack="true">
               <asp:ListItem Text="All Modes" Value="0"></asp:ListItem>
 <asp:ListItem Text="By Air" Value="2"></asp:ListItem>
 <asp:ListItem Text="By Sea" Value="3"></asp:ListItem>
 <asp:ListItem Text="By Road" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-md-2">
        <div class="form-group">
            <label for="DD_Datefilter">Date Filter :</label>
            <asp:DropDownList runat="server" ID="DD_Datefilter" CssClass="form-select p-1 Datafilter" SelectionMode="Multiple" OnSelectedIndexChanged="DD_Datefilter_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </div>
                                                                                                            <div class="col-md-2">
                            <button type="button" class="btn text-white mt-4 date-range-btn" data-bs-toggle="modal" data-bs-target="#dateRangeModal">
    Date Range
</button>


                            <!-- Date Range Modal -->
                            <div class="modal fade" id="dateRangeModal" tabindex="-1" aria-labelledby="dateRangeModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content p-3" style="border-radius: 12px;">
                                        <div class="modal-header">
                                            <h4 class="modal-title" id="dateRangeModalLabel" style="color: #112560;">Filter data on date range</h4>

                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" style="color: #1d64d6">
                                            </button>
                                         
                                        </div>
          

                                         <div class="modal-body">
                <p >Choose a start and an end date which will filter based upon ETD</p>

                                        <!-- Date Selection Fields -->
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="Txt_Fromdate" class="fw-semibold">Start Date</label>
                                                <div class="input-group w-100"">
                                                    <asp:TextBox type="date" runat="server" class="form-control p-2 custom-style" ID="Txt_Fromdate" placeholder="mm/dd/yyyy"  style="width: 100%;" />
                                                </div>

                                            </div>
                                            <div class="form-group mt-3">
                                                <label for="Txt_Todate" class="fw-semibold">End Date</label>
                                                <div class="input-group w-100"">
                                                    <asp:TextBox type="date" runat="server" class="form-control p-2 custom-style" ID="Txt_Todate" placeholder="mm/dd/yyyy"  style="width: 100%;" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="mt-4">
                                            <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn text-white w-100 fw-bold" 
    Style="background-color: #1d64d6; border-radius: 8px;" OnClick="btnApply_Click" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
           <div class="col-md-2"></div>
    </div>
    <%--<div class="col-md-2">
        <div id="datatable-buttons_filter" class="dataTables_filter"><label>Search:<input type="search" class="form-control form-control-sm" placeholder="" aria-controls="datatable-buttons"></label>
            <input type="search" class="form-control form-control-sm" placeholder="" aria-controls="datatable-buttons"></div>
        </div>--%>
   
  <div class="table-responsive" runat="server">
<table id="BookingTables" class="datatable table  table-striped mb-0 w-100">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Booking ID</th>
                                        <th>Order Ref</th>
                                        <th>Transport Mode</th>
                                        <th>Container Mode </th>
                                        <th>Origin Port</th>
                                        <th>Booking ETD</th>
                                        <th>Consignor Name</th>
                                        <th>Consignee Name</th>
                                        <th>Destination Port</th>
                                        <th>Destination ETA</th>
                                        <th>Carrier Name</th>
                                        <th>Vessel</th>
                                        <th>Voyage/Flight</th>
                                        <th>Booked Date</th>
                                        <th>Weight</th>
                                        <th>Volume</th>
                                        <th>TEU</th>
                                        <th>Container Count</th>
                                        <th>Packs</th>
                                        <th>Type</th>
                                        <th>Cargo Description</th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
</div>


        <asp:Literal ID="Roledata" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="codedata" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Transport" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="datefilters" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Startdates" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Enddates" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Pin" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="str_t" runat="server" Visible="false"></asp:Literal>

<script>
   $(document).ready(function () {
        var Roledatas = '<%= Server.HtmlEncode(Roledata.Text) %>';
        var codedatas = '<%= Server.HtmlEncode(codedata.Text) %>';
        var Transport = '<%= Server.HtmlEncode(Transport.Text) %>';
        var datefilter = '<%= Server.HtmlEncode(datefilters.Text) %>';
        var Startdate = '<%= Server.HtmlEncode(Startdates.Text) %>';
        var Enddate = '<%= Server.HtmlEncode(Enddates.Text) %>';
        var Pin = '<%= Server.HtmlEncode(Pin.Text) %>';
       var str_t = '<%= Server.HtmlEncode(str_t.Text) %>';
       if (datefilter === "0") {
           initializeDataTable(Transport, Roledatas, codedatas, Pin,str_t);
       }
       else {
           initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas, str_t);
       }
    });

    function initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas, str_t) {

        $('#BookingTables').DataTable({
            dom: 'Blfrtip',
            scrollX: true,
            scrollY: true,
            processing: true,
            serverSide: true,
            responsive: false,
            colReorder: true,
            stateSave: false,
            ajax: function (data, callback, settings) {
                let orderColumnIndex = data.order[0].column;
                let orderDirection = data.order[0].dir;
                let orderColumn = data.columns[orderColumnIndex].data;

                $.ajax({
                    type: "POST",
                    url: "EventBooking.aspx/GetShipmentDetailsfilter",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        start: data.start,
                        length: data.length,
                        draw: data.draw,
                        searchValue: data.search.value,
                        Transport: Transport,
                        Startdate: Startdate,
                        Enddate: Enddate,
                        Role: Roledatas,
                        Clientcode: codedatas,
                        name: str_t,
                        orderColumn: orderColumn
                    }),
                    success: function (response) {
                        if (response.d) {
                            let jsonData = typeof response.d === "string" ? JSON.parse(response.d) : response.d;
                            callback({
                                draw: jsonData.draw,
                                recordsTotal: jsonData.recordsTotal,
                                recordsFiltered: jsonData.recordsFiltered,
                                data: jsonData.data
                            });
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Error loading shipment data:", xhr.responseText);
                    }
                });
            },

            columns: [
                {
                    data: "icon",
                    render: function (data, type, row) {
                        return `<i class="${row.icon}" 
       style="color: orange; cursor: pointer;"
       data-container="${row.eb_bookingid}"
       onclick="handleBookingAction(this, 'insert');">
   </i>`;
                    }
                },
                { data: "eb_bookingid" },
                { data: "eb_shippersref" },
                {
                    data: "mode",
                    render: function (data, type, row) {
                        return `<i class="fa fa-${row.mode}"></i>`;
                    }
                },
                { data: "eb_mode" },
                { data: "eb_origin" },
                { data: "eb_bookingetd" },
                { data: "eb_consignorname" },
                { data: "eb_consigneename" },
                { data: "eb_dest" },
                { data: "eb_bookingeta" },
                { data: "eb_carriername" },
                { data: "eb_vessel" },
                { data: "eb_voyageflight" },
                { data: "eb_booked" },
                { data: "eb_weight" },
                { data: "eb_volume" },
                { data: "eb_teu" },
                { data: "eb_concount" },
                { data: "eb_packs" },
                { data: "eb_type" },
                { data: "eb_cargodescription" },
            ],
            order: [[0, "desc"]],
            rowCallback: function (row, data) {
                $(row).attr("onclick", `navigateToBookingDetails('${data.Bookencrptid}')`);
                $(row).css("cursor", "pointer");
            },
            pageLength: 10,
            lengthMenu: [[10, 25, 50], ['10', '25', '50']],
            buttons: [
                {
                    extend: "copy",
                    text: "Copy",
                    exportOptions: { columns: ":visible:not(:first-child)" }
                },
                {
                    extend: "excel",
                    text: "Excel",
                    exportOptions: { columns: ":visible:not(:first-child)" }
                },
                {
                    extend: "pdfHtml5",
                    text: "PDF",
                    orientation: "landscape",
                    pageSize: "A4",
                    title: document.title,
                    exportOptions: { columns: ":visible:not(:first-child)" },
                    customize: function (doc) {
                        var columnCount = $("#datatable-buttons thead tr th:visible").length;
                        if (columnCount <= 6) {
                            doc.pageSize = "A3";
                        } else {
                            doc.pageSize = "A4";
                        }

                        if (doc.pageSize === "A3") {
                            doc.content[1].margin = [20, 10, 20, 10];
                            doc.styles.title = {
                                alignment: "center",
                                fontSize: 14,
                                bold: true
                            };
                            doc.styles.tableHeader = {
                                fillColor: "#4CAF50",
                                color: "white",
                                alignment: "center"
                            };
                        }
                    }
                },
                {
                    extend: "colvis",
                    text: "Customise Columns",
                    collectionLayout: 'fixed columns',
                    columns: ":gt(0)",
                    popoverTitle: `Customise your table columns <br>Highlight Column Headers in Blue to View.
<p id="close-popover" style="cursor:pointer; color:#1d64d6; float:right; margin-top:-20px; margin-right: 10px;  font-size: 20px;">X</p>`,
                    exportOptions: { columns: ":visible:not(:first-child)" }
                }
            ],
            language: {
                lengthMenu: "Show _MENU_ entries",
                search: "",
                searchPlaceholder: "Search...",
                info: "Showing _START_ to _END_ of _TOTAL_ entries"
            }
        });

        // Add search icon
        $('.dataTables_filter label').addClass('position-relative').append('<i class="fas fa-search search-icon"></i>');


    }
    function initializeDataTable(Transport, Roledatas, codedatas, Pin, str_t) {

        $('#BookingTables').DataTable({
            dom: 'Blfrtip',
            scrollX: true,
            scrollY: true,
            processing: true,
            serverSide: true,
            responsive: false,
            colReorder: true,
            stateSave: false,
            ajax: function (data, callback, settings) {
                let orderColumnIndex = data.order[0].column;
                let orderDirection = data.order[0].dir;
                let orderColumn = data.columns[orderColumnIndex].data;

                $.ajax({
                    type: "POST",
                    url: "EventBooking.aspx/GetShipmentDetails",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({
                        start: data.start,
                        length: data.length,
                        draw: data.draw,
                        searchValue: data.search.value,
                        Transport: Transport,
                        Role: Roledatas,
                        Clientcode: codedatas,
                        Pins: Pin,
                        name: str_t,
                        orderColumn: orderColumn,
                        orderDir: orderDirection
                    }),
                    success: function (response) {
                        if (response.d) {
                            let jsonData = typeof response.d === "string" ? JSON.parse(response.d) : response.d;
                            callback({
                                draw: jsonData.draw,
                                recordsTotal: jsonData.recordsTotal,
                                recordsFiltered: jsonData.recordsFiltered,
                                data: jsonData.data
                            });
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Error loading shipment data:", xhr.responseText);
                    }
                });
            },

            columns: [
                {
                    data: "icon",
                    render: function (data, type, row) {
                        return `<i class="${row.icon}" 
         style="color: orange; cursor: pointer;"
         data-bookingid="${row.eb_bookingid}"
         onclick="handleBookingAction(this, 'insert');">
     </i>`;
                    }
                },
                { data: "eb_bookingid" },
                { data: "eb_shippersref" },
                {
                    data: "mode",
                    render: function (data, type, row) {
                        return `<i class="fa fa-${row.mode}"></i>`;
                    }
                },
                { data: "eb_mode" },
                { data: "eb_origin" },
                { data: "eb_bookingetd" },
                { data: "eb_consignorname" },
                { data: "eb_consigneename" },
                { data: "eb_dest" },
                { data: "eb_bookingeta" },
                { data: "eb_carriername" },
                { data: "eb_vessel" },
                { data: "eb_voyageflight" },
                { data: "eb_booked" },
                { data: "eb_weight" },
                { data: "eb_volume" },
                { data: "eb_teu" },
                { data: "eb_concount" },
                { data: "eb_packs" },
                { data: "eb_type" },
                { data: "eb_cargodescription" },
            ],
            order: [[0, "desc"]],
            rowCallback: function (row, data) {
                $(row).attr("onclick", `navigateToBookingDetails('${data.Bookencrptid}')`);
                $(row).css("cursor", "pointer");
            },
            pageLength: 10,
            lengthMenu: [[10, 25, 50], ['10', '25', '50']],
            buttons: [
                {
                    extend: "copy",
                    text: "Copy",
                    exportOptions: { columns: ":visible:not(:first-child)" }
                },
                {
                    extend: "excel",
                    text: "Excel",
                    exportOptions: { columns: ":visible:not(:first-child)" }
                },
                {
                    extend: "pdfHtml5",
                    text: "PDF",
                    orientation: "landscape",
                    pageSize: "A4",
                    title: document.title,
                    exportOptions: { columns: ":visible:not(:first-child)" },
                    customize: function (doc) {
                        var columnCount = $("#datatable-buttons thead tr th:visible").length;
                        if (columnCount <= 6) {
                            doc.pageSize = "A3";
                        } else {
                            doc.pageSize = "A4";
                        }

                        if (doc.pageSize === "A3") {
                            doc.content[1].margin = [20, 10, 20, 10];
                            doc.styles.title = {
                                alignment: "center",
                                fontSize: 14,
                                bold: true
                            };
                            doc.styles.tableHeader = {
                                fillColor: "#4CAF50",
                                color: "white",
                                alignment: "center"
                            };
                        }
                    }
                },
                {
                    extend: "colvis",
                    text: "Customise Columns",
                    collectionLayout: 'fixed columns',
                    columns: ":gt(0)",
                    popoverTitle: `Customise your table columns <br>Highlight Column Headers in Blue to View.
<p id="close-popover" style="cursor:pointer; color:#1d64d6; float:right; margin-top:-20px; margin-right: 10px;  font-size: 20px;">X</p>`,
                    exportOptions: { columns: ":visible:not(:first-child)" }
                }
            ],
            language: {
                lengthMenu: "Show _MENU_ entries",
                search: "",
                searchPlaceholder: "Search...",
                info: "Showing _START_ to _END_ of _TOTAL_ entries"
            }
        });

        // Add search icon
        $('.dataTables_filter label').addClass('position-relative').append('<i class="fas fa-search search-icon"></i>');
        // Move "Entries per page" dropdown to the empty div
        $(".dataTables_length").appendTo(".row .col-md-2:first");
        $(".position-relative").appendTo(".row .col-md-2:first");
        // Ensure dropdown styling matches existing layout
        $(".dataTables_length select").addClass("form-select form-select-sm");


    }
</script>


</asp:Content>

