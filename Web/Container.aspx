<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Container.aspx.cs" Inherits="Web_Container" %>

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
    function navigateToContainerdetails(icfd_container) {

        window.location.href = `Containerdetails.aspx?key=${icfd_container}`;
    }
    </script>
    <script>   
    function handleContainerAction(element, action) {
        const data = {
            action: action, // "insert" or "delete"
            containerid: element.getAttribute('data-container')

        };
        $.ajax({
            type: "POST",
            url: "Container.aspx/InsertContainer",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "Inserted") {
                    alert("Add pinned Container!");
                    element.setAttribute('data-action', 'delete');
                } else if (response.d === "Deleted") {
                    alert("Remove pinned Container!");
                    element.setAttribute('data-action', 'insert');
                } else {
                    window.location.replace(window.location.href);
                }
            },
            error: function (error) {
                console.error("Error handling Container:", error);
                alert("An error occurred. Please try again.");
            }
        });
        }
        table.buttons().container().on('click', '#close-popover', function () {
            $('.dt-button-collection').hide(); // Hides the popover
        });
    </script>
    <%--<script>
        document.addEventListener("DOMContentLoaded", function () {
            var fromDate = document.getElementById('<%= fromDate.ClientID %>');
                var toDate = document.getElementById('<%= toDate.ClientID %>');

                fromDate.addEventListener("change", function () {
                    toDate.removeAttribute("min"); // Allow past dates
                    toDate.removeAttribute("max"); // Allow future dates
                });
            });
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementById('<%= fromDate.ClientID %>').addEventListener("change", function () {
                    console.log("Start Date Selected:", this.value);
                });

                document.getElementById('<%= toDate.ClientID %>').addEventListener("change", function () {
                console.log("End Date Selected:", this.value);
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 ">
            <div class="buy-form-crypto mb-0">

                <div class="card-box">
                    <div class="row mb-3">
                        <div class="col d-flex align-items-center">
                            <div class="login-header">
                                <h4>Containers</h4>
                                <p>Here’s where you keep up with all your containers.</p>

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
                                <asp:DropDownList ID="ddl_event" runat="server" CssClass="form-select p-1 event-types-btn" OnSelectedIndexChanged="ddl_evettype_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="All Modes" Value="0"></asp:ListItem>
                                    <asp:ListItem Text=" By Air" Value="2"></asp:ListItem>
                                    <asp:ListItem Text=" By Sea" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="By Road" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                         <div class="col-lg-4"></div>
                    

                        <div class="table-responsive" id="ShowServerdata" runat="server">
                            <table id="shipmentTable" class="datatable table table-striped mb-0 w-100">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>CTR No </th>
                                        <th>Shipment Number</th>
                                        <th>Order Ref</th>
                                        <th>Type </th>
                                        <th>Mode</th>
                                        <th>ETA</th>
                                        <th>EST Del Date/Time</th>
                                        <th>Act Del Date/Time</th>
                                        <th>Empty Return</th>
                                        <th>ATA</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
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
        var Pin = '<%= Server.HtmlEncode(Pin.Text) %>';

        if (datefilter == "0") {
            initializeDataTable(Transport, Roledatas, codedatas, Pin);


        } else {
            initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas);
        }
    });

    function initializeDataTablefilter(Transport, Startdate, Enddate, Roledatas, codedatas) {
        $("#shipmentTable").DataTable({
            dom: 'Blfrtip',
            scrollX: true,
            scrollY: true,
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
                    url: "Container.aspx/GetShipmentDetailsfilter",
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
    data-container="${row.icfd_container}"
    onclick="handleContainerAction(this, 'insert');">
</i>`;
                    }
                },

                { data: "icfd_container" },
                { data: "icfd_jobref" },
                { data: "icfd_masterref" },
                { data: "icfd_conttype" },
                { data: "icfd_contmode" },
                { data: "icfd_eta" },
                { data: "icfd_estimateddeliver" },
                { data: "icfd_actualdeliver" },
                { data: "icfd_emptyreturned" },
                { data: "icfd_daysfrometatoavailability" }
            ],
            order: [[0, "desc"]],
            rowCallback: function (row, data) {
                $(row).attr("onclick", `navigateToContainerdetails('${data.containerencrptid},${data.ShipmentNo}')`);
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
                lengthMenu: "Show MENU entries",
                search: "",
                searchPlaceholder: "Search...",
                info: "Showing START to END of TOTAL entries"
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
    function initializeDataTable(Transport, Roledatas, codedatas, Pin) {
        $("#shipmentTable").DataTable({
            dom: 'Blfrtip',
            scrollX: true,
            scrollY: true,
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
                    url: "Container.aspx/GetShipmentDetails",
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
                        Pin: Pin
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
    data-container="${row.icfd_container}"
    onclick="handleContainerAction(this, 'insert');">
</i>`;
                    }
                },

                { data: "icfd_container" },
                { data: "icfd_jobref" },
                { data: "icfd_masterref" },
                { data: "icfd_conttype" },
                { data: "icfd_contmode" },
                { data: "icfd_eta" },
                { data: "icfd_estimateddeliver" },
                { data: "icfd_actualdeliver" },
                { data: "icfd_emptyreturned" },
                { data: "icfd_daysfrometatoavailability" }
            ],
            order: [[0, "desc"]],
            rowCallback: function (row, data) {
                $(row).attr("onclick", `navigateToContainerdetails('${data.containerencrptid},${data.ShipmentNo}')`);
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

