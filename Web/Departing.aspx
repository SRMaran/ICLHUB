<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Departing.aspx.cs" Inherits="Web_Departing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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


    

     .pinned-row {
         background-color: #f9f871;
         font-weight: bold;
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
                function navigateToShimentDetails(shipencryptid) {
                    window.location.href = `Shipmentdetails.aspx?key=${shipencryptid}`;
                }
            </script>
    <script>   
        function handleShipmentAction(element, action) {
            const data = {
                action: action, // "insert" or "delete"
                shipmentid: element.getAttribute('data-shipmentid')

            };
            $.ajax({
                type: "POST",
                url: "Departing.aspx/InsertShipment",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "insert") {
                        alert("Add pinned shipment!");
                        element.setAttribute('data-action', 'delete');
                    } else if (response.d === "delete") {
                        alert("Remove pinned shipment!");
                        element.setAttribute('data-action', 'insert');
                    } else {
                        window.location.replace(window.location.href);
                    }
                },
                error: function (error) {
                    console.error("Error handling shipment:", error);
                    alert("An error occurred. Please try again.");
                }
            });
        }
        table.buttons().container().on('click', '#close-popover', function () {
            $('.dt-button-collection').hide(); // Hides the popover
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="row">
      <div class="col-md-12">
          <div class="buy-form-crypto mb-0">

              <div class="card-box">
                  <div class="row mb-3">
                      <div class="col d-flex align-items-center">
                          <div class="login-header">
                              <h4>
                                  <asp:Label ID="lb_shipment" runat="server" text="Departing Today"></asp:Label>

                                  <a id="evenbooking"
                                      runat="server"
                                      visible="false">
                                  </a>
                              </h4>
                              <%--<p id="transitpara" runat="server">
                                  Here's where you keep up with all your bookings in departing today.
              <asp:Label ID="lb_pragraph" runat="server"></asp:Label>
                              </p>--%>
                          </div>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-md-2"></div>
                      <div class="col-md-2">
                          <div class="form-group">
                              <br />
                              <asp:Button ID="btnpinship" runat="server" CssClass="btn btn-outline-warning me-1 mb-1 pinned-shipmnets-btn" Text="Pinned Shipments" Style="font-size: 14px; margin-top: 5px;" OnClick="btnpinship_Click"></asp:Button>
                          </div>
                      </div>
                      <div class="col-md-2">
                          <div class="form-group">
                              <label>Mode Of Transport :</label>
                              <asp:DropDownList ID="ddl_evettype" runat="server" CssClass="form-select p-1 event-types-btn " OnSelectedIndexChanged="ddl_evettype_SelectedIndexChanged" AutoPostBack="true">
                                  <asp:ListItem Text="All Modes" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="By Air" Value="1"></asp:ListItem>
                                  <asp:ListItem Text="By Sea" Value="2"></asp:ListItem>
                                  <asp:ListItem Text="By Road" Value="3"></asp:ListItem>
                              </asp:DropDownList>
                          </div>
                      </div>
                      <div class="col-lg-4"></div>
                      
                  </div>
                  <div class="table-responsive">
                      <table id="shipmentTable" class="datatable table  table-striped mb-0 w-100">
                          <thead>
                              <tr>
                                  <th></th>
                                  <th>Mode</th>
                                  <th>Container Mode </th>
                                  <th>Shipment ID</th>
                                  <th>Order Ref </th>
                                  <th>Shipper</th>
                                  <th>Orgin Port</th>
                                  <th>ETD</th>
                                  <th>Consignor </th>
                                  <th>Consignee </th>
                                  <th>Destination Port</th>
                                  <th>ETA</th>
                                  <th>Vessel </th>
                                  <th>Bill </th>
                                  <th>Goods Description</th>
                                  <th>Estimated Delivery Date</th>
                                  <th>Actual Delivery Date</th>
                                  <th>Voy./Flight </th>
                                  <th>Estimated Pickup</th>
                                  <th>Actual Pickup</th>
                                  <th>Containers</th>
                                  <th>Carrier</th>
                                  <th>TEU</th>
                                  <th>Weight</th>
                                  <th>Volume</th>
                              </tr>
                          </thead>
                          <tbody>
                              <%--<asp:PlaceHolder ID="PH_shipment" runat="server"></asp:PlaceHolder>--%>
                          </tbody>
                      </table>
                  </div>
              </div>
          </div>
      </div>
  </div>
         <asp:Literal ID="Roledata" runat="server" Visible="false"></asp:Literal>
 <asp:Literal ID="codedata" runat="server" Visible="false"></asp:Literal>
 <asp:Literal ID="Transport" runat="server" Visible="false"></asp:Literal>
 <asp:Literal ID="datefilters" runat="server" Visible="false"></asp:Literal>
 <asp:Literal ID="Startdates" runat="server" Visible="false"></asp:Literal>
 <asp:Literal ID="Enddates" runat="server" Visible="false"></asp:Literal>
<asp:Literal ID="Pin" runat="server" Visible="false"></asp:Literal>
        <script>

            $(document).ready(function () {
                var Roledatas = '<%= Roledata.Text %>';
                var codedatas = '<%= codedata.Text %>';
                var Transport = '<%= Transport.Text %>';
                var datefilter = '<%= datefilters.Text %>';
            var Startdate = '<%= Startdates.Text %>';
            var Enddate = '<%= Enddates.Text %>';
            var Pin = '<%= Pin.Text %>';

                if (datefilter == "0") {
                    initializeDataTable(Transport, Roledatas, codedatas, Pin);


                } else {
                    initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas);
                }
            });
            function initializeDataTable(Transport, Roledatas, codedatas, Pin) {
                $("#shipmentTable").DataTable({
                    dom: 'Blfrtip',
                    scrollX: true,
                    scrollY: true,
                    ordering: true,
                    processing: true,
                    serverSide: true,
                    responsive: false,
                    colReorder: true,  // Enable column reordering
                    stateSave: false,    // Save column order state
                    ajax: function (data, callback, settings) {
                        let orderColumnIndex = data.order[0].column;
                        let orderDirection = data.order[0].dir;
                        let orderColumn = data.columns[orderColumnIndex].data;

                        $.ajax({
                            type: "POST",
                            url: "Departing.aspx/GetShipmentDetails",
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
                                orderColumn: orderColumn,
                                orderDir: orderDirection,
                                Pin: Pin,
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
    data-shipmentid="${row.spr_shipmentid}"
    onclick="handleShipmentAction(this, 'insert');">
</i>`;
                            }
                        },

                        {
                            data: "spr_tran",
                            render: function (data, type, row) {
                                return `   <td><i class="fa fa-${row.spr_tran}"></i></td>`;
                            }
                        },
                        { data: "spr_mode" },
                        { data: "spr_shipmentid" },
                        { data: "spr_shippersreference" },
                        { data: "spr_consignorname" },
                        { data: "spr_origin" },
                        { data: "spr_originetd", orderable: true },
                        { data: "spr_consignorname" },
                        { data: "spr_consigneename" },
                        { data: "spr_destination" },
                        { data: "spr_destinationeta" },
                        { data: "spr_vessel" },
                        { data: "spr_houseref" },
                        { data: "spr_goodsdescription" },
                        { data: "spr_estcartagedelivery" },
                        { data: "spr_actualcartagedelivery" },
                        { data: "spr_flightvoyage" },
                        { data: "spr_pickupby" },
                        { data: "spr_actualpickup" },
                        { data: "icfd_container" },
                        { data: "spr_carriername" },
                        { data: "spr_teu" },
                        { data: "spr_weight" },
                        { data: "spr_volume" }
                    ],
                    order: [[0, "desc"]],
                    rowCallback: function (row, data) {
                        $(row).attr("onclick", `navigateToShimentDetails('${data.shipencryptid}')`);
                        $(row).css("cursor", "pointer");
                    },
                    pageLength: 10,
                    lengthMenu: [[10, 25, 50], ['10', '25', '50']],
                    buttons: [
                        {
                            extend: "copy",
                            text: "Copy",
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                        },
                        {
                            extend: "excel",
                            text: "Excel",
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                        },
                        {
                            extend: "pdfHtml5",
                            text: "PDF",
                            orientation: "landscape",
                            pageSize: "A4",
                            title: document.title,
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                            customize: function (doc) {

                                var columnCount = $("#datatable-buttons thead tr th:visible").length;

                                if (columnCount <= 6) {
                                    doc.pageSize = "A3";
                                } else {
                                    doc.pageSize = "A4";
                                }

                                if (doc.pageSize = "A3") {
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
                                else {

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
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            }
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
                $(".position-relative").appendTo(".row .col-lg-4:first");
                // Ensure dropdown styling matches existing layout
                $(".dataTables_length select").addClass("form-select form-select-sm");
            }
            function initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas) {
                $("#shipmentTable").DataTable({
                    dom: 'Blfrtip',
                    scrollX: true,
                    scrollY: true,
                    ordering: true,
                    processing: true,
                    serverSide: true,
                    responsive: false,
                    colReorder: true,  // Enable column reordering
                    stateSave: false,    // Save column order state
                    ajax: function (data, callback, settings) {
                        let orderColumnIndex = data.order[0].column;
                        let orderDirection = data.order[0].dir;
                        let orderColumn = data.columns[orderColumnIndex].data;
                        $.ajax({
                            type: "POST",
                            url: "Departing.aspx/GetShipmentDetailsfilter",
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
    data-shipmentid="${row.spr_shipmentid}"
    onclick="handleShipmentAction(this, 'insert');">
</i>`;
                            }
                        },

                        {
                            data: "spr_tran",
                            render: function (data, type, row) {
                                return `   <td><i class="fa fa-${row.spr_tran}"></i></td>`;
                            }
                        },
                        { data: "spr_mode" },
                        { data: "spr_shipmentid" },
                        { data: "spr_shippersreference" },
                        { data: "spr_consignorname" },
                        { data: "spr_origin" },
                        { data: "spr_originetd" },
                        { data: "spr_consignorname" },
                        { data: "spr_consigneename" },
                        { data: "spr_destination" },
                        { data: "spr_destinationeta" },
                        { data: "spr_vessel" },
                        { data: "spr_houseref" },
                        { data: "spr_goodsdescription" },
                        { data: "spr_estcartagedelivery" },
                        { data: "spr_actualcartagedelivery" },
                        { data: "spr_flightvoyage" },
                        { data: "spr_pickupby" },
                        { data: "spr_actualpickup" },
                        { data: "icfd_container" },
                        { data: "spr_carriername" },
                        { data: "spr_teu" },
                        { data: "spr_weight" },
                        { data: "spr_volume" }
                    ],

                    rowCallback: function (row, data) {
                        $(row).attr("onclick", `navigateToShimentDetails('${data.shipencryptid}')`);
                        $(row).css("cursor", "pointer");
                    },
                    pageLength: 10,
                    lengthMenu: [[10, 25, 50], ['10', '25', '50']],
                    buttons: [
                        {
                            extend: "copy",
                            text: "Copy",
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                        },
                        {
                            extend: "excel",
                            text: "Excel",
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                        },
                        {
                            extend: "pdfHtml5",
                            text: "PDF",
                            orientation: "landscape",
                            pageSize: "A4",
                            title: document.title,
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            },
                            customize: function (doc) {

                                var columnCount = $("#datatable-buttons thead tr th:visible").length;

                                if (columnCount <= 6) {
                                    doc.pageSize = "A3";
                                } else {
                                    doc.pageSize = "A4";
                                }

                                if (doc.pageSize = "A3") {
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
                                else {

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
                            exportOptions: {
                                columns: ":visible:not(:first-child)"
                            }
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
                $(".position-relative").appendTo(".row .col-lg-4:first");
                // Ensure dropdown styling matches existing layout
                $(".dataTables_length select").addClass("form-select form-select-sm");
            }

        </script>
</asp:Content>

