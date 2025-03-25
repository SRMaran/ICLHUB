<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Web_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function navigateToShimentDetails(shipmentencrypt) {
            window.location.href = `ShipmentDetails.aspx?key=${shipmentencrypt}`;
        }
    </script>
    <script src="../Template/assets/plugins/index.global.min.js"></script>

    <link rel="stylesheet" href="../Template/assets/plugins/daterangepicker/daterangepicker.css">

    <style>
        #calendar {
            width: 100%;
            height: 600px;
               
        }
     
.fc-prev-button, .fc-next-button {
    width: 25px !important;
    height: 25px !important;
    border-radius: 10px !important;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 18px;
    font-weight: bold;
    transition: all 0.3s ease-in-out;
    margin: 0 5px; /* Adds spacing between buttons */
}
.fc-button-group{
    gap:0 !important;
}
/* Previous Button (Outline Style) */
.fc-prev-button {
    background-color: white !important;
    border: 1.5px solid #1d64d6 !important;
    color: #1d64d6 !important;
}

/* Next Button (Remove Blue Background) */
.fc-next-button {
    background-color: white !important; /* Make it white like prev button */
    border: 1.5px solid #1d64d6 !important;
    color: #1d64d6 !important; /* Arrow should be blue */
     margin-left: 5px !important;
}

/* Hover Effects */
.fc-prev-button:hover, .fc-next-button:hover{
    background-color: #1d64d6 !important;
/*    color: white !important;
*/    border-color: #1d64d6 !important;
}
.fc-prev-button:hover .fc-icon,
.fc-next-button:hover .fc-icon {
    color: white !important; /* Arrow turns white */
}
/* Center the arrow icon properly */
.fc-prev-button .fc-icon,
.fc-next-button .fc-icon {
    font-size: 18px !important;
    display: flex;
    align-items: center;
    justify-content: center;
    color:#1d64d6;
    margin:-8px;
}
.fc-header-toolbar fc-toolbar fc-toolbar{
        margin-top:1.5em !important;
     

}
.fc .fc-toolbar.fc-header-toolbar {
                        margin-bottom:0 !important;
                        margin-left:15px;
gap:140px;
margin-top:5px;
}
.fc-header-toolbar.fc-toolbar.fc-toolbar-ltr .fc-toolbar-chunk:nth-child(2) {
    display: none;
}

    </style>



    <style>
         .shipment-item a {
       color: #112560;
       font-weight:400;
       font-family: proxima-nova, sans-serif;
        font-size:16px;
        display: flex;
    align-items: center;
    justify-content: space-between;
   }
        .shipment-text{
            
    padding: 0 0 0;

        }
        .shipment-text p:last-child {
            margin: 0;
        }
         .shipment-list {
     list-style: none;
     padding: 0;
 }

     .shipment-list li {
         display: flex;
         align-items: center;
         justify-content: space-between;
         border-bottom: 1px solid #eaeaea;
         padding-top:20px;
         padding-bottom:20px;
     }

        .shipment-circle {
            color: #1d64d6;
            height: 220px;
            width: 220px;
            border-radius: 50%;
            background: conic-gradient( #1d64d6, #5fca99, #6ac7f2, #f4b43f );
            position: relative;
            margin: auto;
            text-align: center;
            font-family: transducer-extended, sans-serif;
            font-weight: 600;
            justify-content: center;
            display: flex;
            flex-direction: column;
            z-index: 1;
            opacity:1;
            letter-spacing:0;
        }

            .shipment-circle::after {
                content: '';
                position: absolute;
                width: 210px;
                height: 210px;
                background: white;
                border-radius: 50%;
                border: 2px;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: -1;
            }

       .shipment-item {
           width: calc(100% - 40px)
       }

            .card-box{
                padding:25px !important;
            }

                .shipment-list li:last-child {
                    border-bottom: none;
                }

        .shipment-icon {
            height: 30px;
            width: 30px;
            border-radius: 100%;
            display: block;
            
        }

        .blue {
            background-color: #1d64d6;
        }

        .green {
            background-color: #5fca99;
        }

        .lightblue {
            background-color: #6ac7f2;
        }

        .orange {
            background-color: #f4b43f;
        }
        .shipment-arrow {
    color: #03a9f4;
    font-size: 16px;
    line-height: 1;
    display: block;
    margin-top: -21px;
    margin-left: 15vw; /* Adjust based on screen width */
}

        .activetext{
            display: block;
            font-size: 10px;
            line-height:12px;
            font-weight: 400;
    color: #112560;
        }
        .activecount{
    display: block;
    font-size: 22px;
    line-height:26px
}
        

        .shipment-circle span:last-child {
            display: block;
            color: #35446F;
        }

        /*.chart-container {
            width: 100%;
            height: 205px;
        }*/
  .chart-container {
    width: 100% !important;
    height: 150px;
}
canvas {
    width: 100% !important;
/*    height: auto !important;
*/}


 .custom-dropdown {
    appearance: none; /* Hides default dropdown styling */
    -webkit-appearance: none;
    -moz-appearance: none;
    
    /* Custom dropdown arrow */
    background-image: url('../Template/assets/down-arrow-svgrepo-com%20(1).svg');
    background-repeat: no-repeat;
    background-position: right 10px center;

    padding-right: 35px; /* Space for the arrow */
    background-color: white; /* Set background color */
    
    border: none !important; /* Remove border */
    outline: none !important; /* Remove focus outline */
    box-shadow: none !important; /* Remove focus glow */
}

/* Ensure dropdown remains clean when clicked */
.custom-dropdown:focus,
.custom-dropdown:active {
    background-color: white !important;
    border: none !important;
    box-shadow: none !important;
}

/* Remove blue highlight from selected options */
.custom-dropdown option:checked,
.custom-dropdown option:hover {
    background-color: transparent !important;
}



        .fc .fc-button-primary:disabled {
            background-color: #1d64d6;
            border-color: #1d64d6;
            color: white;
        }
        .fc .fc-button-primary {
            background-color: #1d64d6;
            border-color: #1d64d6;
            color: white;
        }
        .fc.fc-toolbar-title {
            font-family: proxima-nova, sans-serif;
            font-size: 14px;
            margin: 0px;
        }

        .fc-toolbar h2 {
            font-family: transducer-extended, sans-serif;
            font-size: 15px;
            font-weight: 500;
            line-height: 18px;
          text-transform: uppercase;
          letter-spacing: 0;
          text-align:left;
          color:#112560s;
        }


      div.card-box.notification {
    padding-bottom: 40px !important;
}


    </style>
            <style>
        .arrow-icon-wrapper .arrow-icon {
    position: relative;
    width: 100%;
}

.arrow-icon-wrapper .arrow-icon .triangleline {
    background: linear-gradient(90deg, rgba(244, 251, 255, 0) 0, #c3d8f6 23%, #1d64d6);
    height: 2px;
    position: relative;
    width: 100%;
    margin-bottom: -4px; /* Adjust spacing to ensure alignment with arrow */
}

.arrow-icon-wrapper .arrow-icon .arrow {
    border: solid #1d64d6;
    border-width: 0 2px 2px 0;
    display: inline-block;
    padding: 4px; /* Adjust size of arrowhead */
    position: absolute;
    right: 0;
    top: -4px; /* Adjust to align vertically */
    transform: rotate(-45deg);
}



    .mb-4 {
        padding-bottom: 0.5rem !important;
            margin-bottom: 0rem !important;
    }



    </style>

    <script src="../Template/assets/js/chart.js"></script>
    <style>
        .delivery-event {
            background-color: skyblue !important;
            color: white;
        }

        .shipped-event {
            background-color: red !important;
            color: white;
        }

        .fc-popover .fc-popover-body {
            max-height: 150px;
            overflow-y: auto;
        }

        .fc-popover .fc-popover-body {
            padding: 10px;
        }
        .text-count{
            font-weight: 600;
        }
    </style>
    <script src="../Template/assets/js/chart.js"></script>
    <style>
        .delivery-event {
            background-color: skyblue !important;
            color: white;
        }

        .shipped-event {
            background-color: red !important;
            color: white;
        }

        .fc-popover .fc-popover-body {
            max-height: 150px;
            overflow-y: auto;
        }

        .fc-popover .fc-popover-body {
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="overflow: hidden;">
        <div class="col-sm-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="login-header">
                            <h4>Daily Update</h4>
                        </div>
                    </div>
                </div>
                <div class="chat-footer-box">
                    <div class="discussion-sent">
                        <div class="row gx-2">
                            <div class="col-lg-12">
                                <div class="footer-discussion">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <a id="ETA" runat="server">
                            <div class="card-holder holder-blue">
                                <div class="row">
                                    <div class="main-balance-blk">
                                        <div class="main-balance">
                                            <h1>
                                                <asp:Label ID="lb_etaupdate" runat="server"></asp:Label></h1>
                                            <h4>ARRIVING TODAY</h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-10">
                                                <div class="premium-box">
                                                    <i class="feather-clock" style="font-size: 40px;"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a id="ETD" runat="server">
                            <div class="card-holder holder-blue">
                                <div class="row">
                                    <div class="main-balance-blk">
                                        <div class="main-balance">
                                            <h1>
                                                <asp:Label ID="lb_etd" runat="server"></asp:Label></h1>
                                            <h4>DEPARTING TODAY</h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-10">
                                                <div class="premium-box ">
                                                    <i class="feather-map-pin" style="font-size: 40px;"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a id="ATD" runat="server">
                            <div class="card-holder  holder-blue">
                                <div class="row">
                                    <div class="main-balance-blk">
                                        <div class="main-balance">
                                            <h1>
                                                <asp:Label ID="lb_delivery" runat="server"></asp:Label></h1>
                                            <h4>AWAITING DELIVERY</h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-10">
                                                <div class="premium-box">
                                                    <i class="fa fa-truck" style="font-size: 40px;"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card-box">
                <div class="row">
                    <!-- Header -->
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Active Shipments</h4>
                        </div>
                    </div>
                </div>
                <div class="chat-footer-box">
                    <div class="discussion-sent">
                        <div class="row gx-2">
                            <div class="col-lg-12">
                                <div class="footer-discussion">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Chart and Details Section -->
                <div class="row gx-4 mt-3">
                    <!-- Left: Circular Chart -->
                    <div class="col-lg-6 d-flex justify-content-center align-items-center" style="padding:0">
                        <div class="shipment-circle">
                            <asp:Label ID="lb_Active" CssClass="activecount" runat="server" Text="Active shipments:0"></asp:Label>
                            <div class="activetext">Active Shipments</div>

                        </div>
                    </div>
                    <!-- Right: Shipment Details -->
                    <div class="col-lg-6">
                        <ul class="shipment-list">
                            <li>
                                 <div class="shipment-icon blue"></div>
                                <div class="shipment-item">
                                  <a  class="shipment-label" >
                                      <div class="shipment-text">
                                            All Shipments:
<asp:Label ID="lb_shipment" runat="server" CssClass="text-count" Text="All Shipment:0"></asp:Label>
                                      </div>
                                     </a>
                                </div>
                            </li>

<li>
    <div class="shipment-icon green"></div>
    <div class="shipment-item">
        <a class="shipment-label"  id="Orginport" runat="server">
            <div class="shipment-text">At Origin Port:
                <asp:Label ID="lb_Origin" runat="server" CssClass="text-count"></asp:Label>
            </div>
                            <img src="../Template/assets/right-arrow.png" style="display: block; color:#03a9f4" />
        </a>
    </div>
</li>
<li>
    <div class="shipment-icon lightblue"></div>
    <div class="shipment-item">
        <a class="shipment-label" id="Intransit" runat="server" >
            <div class="shipment-text">In Transit: 
                <asp:Label ID="lb_Transit" runat="server" CssClass="text-count"></asp:Label>
            </div>
                            <img src="../Template/assets/right-arrow.png" style="display: block; color:#03a9f4" />

        </a>
    </div>
</li>
<li>
    <div class="shipment-icon orange"></div>
    <div class="shipment-item">
        <a class="shipment-label" id="Dest" runat="server" >
            <div class="shipment-text">At Destination
                <p>Port:<asp:Label ID="lb_destination" runat="server" CssClass="text-count"></asp:Label></p>
            </div>
            <img src="../Template/assets/right-arrow.png" style="display: block; color:#03a9f4";/>
        </a>
    </div>
</li>




                        </ul>
                    </div>
                </div>
            </div>
            <div class="card-box chart">
                <div class="row align-items-center">
                    <div class="col-md-12">
                        <div class="login-header">
                            <h4>Origin Arrivals</h4>
                        </div>
                        <div class="dropdowns-container d-flex">      
                            <select class="form-select custom-dropdown me-2" id="dropdown1">
                                <option value="1 month">1 Month</option>
                                <option value="2 month">2 Months</option>
                                <option value="6 month">6 Months</option>
                                <option value="12 month">12 Months</option>
                            </select>
                              <i class="icon ion-ios7-download dropdown-icon"></i>
                            <asp:DropDownList ID="DD_origin" runat="server" CssClass="form-select custom-dropdown me-2">
                            </asp:DropDownList>
                        </div>



                    </div>
                </div>
               

<div class="row">
    <div class="col-12">
        <div class="chart-container">
            <canvas id="myChart"></canvas>
        </div>
    </div>
</div>
    </div>

            </div>
        <div class="col-sm-6">
            <div class="card-box notification">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Notifications</h4>
                        </div>
                    </div>
                </div>

                <div class="chat-footer-box">
                    <div class="discussion-sent">
                        <div class="row gx-2">
                            <div class="col-lg-12">
                                <div class="footer-discussion">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="fc-scroller fc-day-grid-container" style="overflow: hidden scroll; height: 500px;">
                    <div>
                        <asp:PlaceHolder ID="PH_Notification" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-6">
                    <a id="Confirmedbooking" runat="server">
                        <div class="alert alert-info alert-dismissible fade show" role="alert">
                            <div class="row">
                                <div class="col-md-8">
                                    <h1 style="color: #03fc0b;">
                                        <asp:Label ID="lb_booking" runat="server" style="color: #60CC9C;"></asp:Label>
                                    </h1>
                                    <h4 style="color: #112560">CONFIRMED BOOKINGS</h4>
                                </div>
                                <div class="col-md-4">
                                    <div class="premium-box box-green">
                                        <i class="feather-check"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-6">
                    <a href="YourQuotes.aspx">
                        <div class="alert alert-info alert-dismissible fade show" role="alert">
                            <div class="row">
                                <div class="col-md-8">
                                    <h1 style="color: #fcb603" >
                                        <asp:Label ID="lb_quotes" runat="server" style="color: #fcb603"></asp:Label>
                                    </h1>
                                    <h4 style="color: #112560">PENDING QUOTES</h4>
                                </div>
                                <div class="col-md-4">
                                    <div class="premium-box box-orange">
                                        <i class="feather-tag"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <div class="card-box">
            <div class="row  align-items-stretch">
                <div class="col-lg-7">
                   <div class="row mt-3">
        <div class="button-group">
            <asp:Button ID="shipped" runat="server" CssClass="custom-badge btn btn-outline-info" Text="Shipped"></asp:Button>
            <asp:Button ID="arrival" runat="server" CssClass="custom-badge btn btn-outline-warning" Text="Arriving"></asp:Button>
            <asp:Button ID="delivery" runat="server" CssClass="custom-badge btn btn-outline-info" Text="Delivery"></asp:Button>
            <asp:Button ID="allshipment" runat="server" CssClass="custom-badge btn btn-primary" Text="All Shipments"></asp:Button>
        </div>
    </div>  
                    <div class="row mt-6 fc-scroller fc-day-grid-container" style="overflow: hidden scroll; height: 600px;">
                        <div class="col-md-12">
                            <div id="calendar" class="flex-grow-1"></div>
                        </div>
                    </div>
                </div>

<div class="col-sm-5">
    <div class="row">
        <div class="col-sm-12">
            <div class="login-header">
                <h4>Upcoming Events</h4>
            </div>
        </div>
    </div>
    <!-- Match height with calendar -->
    <div class="row pagination-box fc-day-grid-container" style="height: 600px; align-items: stretch;">
        <div class="col-md-1"></div>
        <div class="alert alert-info alert-dismissible fade show waves-effect waves-light" 
             style="border-radius: 25px; --bs-alert-bg: #fafeff; height: auto; max-height: 600px;">         
            <!-- Scrollable event container -->
            <div id="eventContainer" style="  padding-right: 10px;"></div>
            <!-- Pagination should always be visible -->
        </div>
                    <div id="paginationContainer" class="mt-3"></div>
    </div> 
</div>
            </div>
        </div>
    </div>
    <asp:Literal ID="lt_barchart" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="lt_barchartmonth" runat="server" Visible="false"></asp:Literal>
    <asp:HiddenField ID="hiddenOriginPercent" runat="server" />
    <asp:HiddenField ID="hiddenTransitPercent" runat="server" />
    <asp:HiddenField ID="hiddenDestinationPercent" runat="server" />
    <asp:HiddenField ID="hiddenTotalPercent" runat="server" />
    <asp:HiddenField ID="hiddenOtherPercent" runat="server" />
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            applyShipmentCircleStyle();
        });


        function applyShipmentCircleStyle() {
            const originField = document.getElementById('<%=hiddenOriginPercent.ClientID %>');
            const transitField = document.getElementById('<%=hiddenTransitPercent.ClientID %>');
            const destinationField = document.getElementById('<%=hiddenDestinationPercent.ClientID %>');


            if (originField && transitField && destinationField) {
                const originPercent = parseFloat(originField.value || 0);
                const transitPercent = parseFloat(transitField.value || 0);
                const destinationPercent = parseFloat(destinationField.value || 0);


                const circle = document.querySelector(".shipment-circle");
                if (circle) {

                    circle.style.background = `conic-gradient(
            #5fca99 ${originPercent}%,
            #6ac7f2 ${transitPercent}%, 
            #f4b43f ${destinationPercent}% 100%
        )`;
                }
            } else {
                console.error('One or more hidden fields not found');
            }
        }
    </script>



    <script>
        let allEvents = [];
        let calendarInitialized = false; // Flag to ensure calendar is initialized only once

        function initializeCalendar(events) {
            const calendarEl = document.getElementById('calendar');

            if (!calendarEl) {
                console.error("Calendar element not found");
                return;
            }

            const calendar = new FullCalendar.Calendar(calendarEl, {
                initialDate: new Date(),
                editable: true,
                selectable: true,
                businessHours: true,
                dayMaxEvents: true,
                events: events,

                headerToolbar: {
                    right: 'prev,next', // Removes the 'today' button
                    left: 'title',
                },

                eventDidMount: function (info) {
                    const eventElement = info.el;
                    const eventDot = eventElement.querySelector('.fc-daygrid-event-dot');
                    if (eventDot) eventDot.remove();

                    const eventTime = eventElement.querySelector('.fc-event-time');
                    if (eventTime) eventTime.remove();

                    eventElement.querySelector('.fc-event-title').style.backgroundColor = info.event.extendedProps.color;
                    eventElement.querySelector('.fc-event-title').innerHTML = info.event.title;
                },

                eventClick: function (info) {
                    const containerNumber = info.event.extendedProps.shipencrypt;
                    if (containerNumber) {
                        window.location.href = `Shipmentdetails.aspx?key=${containerNumber}`;
                    } else {
                        alert('Shipment number not found for this event');
                    }
                }
            });

            calendar.render();
            window.myCalendar = calendar; // Store the calendar instance globally
        }

        function filterCalendarEvents(eventType) {
            if (!window.myCalendar) {
                console.error("Calendar instance not initialized.");
                return;
            }

            let filteredEvents = eventType === 'all' ? allEvents : allEvents.filter(event => event.extendedProps.eventType === eventType);

            console.log("Filtered Events:", filteredEvents); // Debugging

            window.myCalendar.removeAllEvents(); // Remove all events
            window.myCalendar.addEventSource(filteredEvents); // Add filtered events
        }

        function loadCalendarData() {
            $.ajax({
                url: 'Dashboard.aspx/FetchShipmentData',
                type: 'POST',
                contentType: 'application/json',
                success: function (data) {
                    if (Array.isArray(data.d)) {
                        allEvents = data.d.map(event => ({
                            title: event.Containerno,
                            start: event.EventDate,
                            extendedProps: {
                                shipencrypt: event.shipencrypt,
                                eventType: event.EventType,
                                containerNumber: event.Containerno,
                                color: event.EventType === "arrival" ? "orange" :
                                    event.EventType === "delivery" ? "skyblue" :
                                        event.EventType === "shipped" ? "skyblue" : "gray"
                            }
                        }));

                        if (!calendarInitialized) {
                            initializeCalendar(allEvents); // Initialize calendar only once
                        } else {
                            filterCalendarEvents('all'); // Update with all events if data reload is needed
                        }
                    } else {
                        console.error("Unexpected data format:", data.d);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                    console.error("Status: " + status);
                    console.error("Response: ", xhr.responseText);
                }
            });
        }

        document.addEventListener('DOMContentLoaded', function () {
            loadCalendarData(); // Load all data once on page load

            // Button click event listeners for filtering
            document.getElementById('<%= shipped.ClientID %>')?.addEventListener('click', function (event) {
                event.preventDefault(); // Prevent page reload
                filterCalendarEvents('shipped');
            });

            document.getElementById('<%= delivery.ClientID %>')?.addEventListener('click', function (event) {
                event.preventDefault(); // Prevent page reload
                filterCalendarEvents('delivery');
            });

            document.getElementById('<%= arrival.ClientID %>')?.addEventListener('click', function (event) {
                event.preventDefault(); // Prevent page reload
                filterCalendarEvents('arrival');
            });

            document.getElementById('<%= allshipment.ClientID %>')?.addEventListener('click', function (event) {
                event.preventDefault(); // Prevent page reload
                filterCalendarEvents('all');
            });
        });
    </script>


        <script>
            let currentPage = 1;
            const itemsPerPage = 5;
            let eventsData = [];

            // Function to fetch upcoming events via AJAX
            function loadUpcomingEvents() {
                $.ajax({
                    type: "POST",
                    url: "Dashboard.aspx/GetUpcomingEvents",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        eventsData = Array.isArray(response.d) ? response.d : JSON.parse(response.d);
                        renderPage();
                    },
                    error: function (error) {
                        console.error("Error fetching events:", error);
                    }
                });
            }

            // Function to render the events for the current page
            function renderPage() {
                const eventContainer = $("#eventContainer");
                eventContainer.empty();

                // Calculate start and end index for the current page
                const startIndex = (currentPage - 1) * itemsPerPage;
                const endIndex = Math.min(startIndex + itemsPerPage, eventsData.length);

                // Iterate through the current page's events
                for (let i = startIndex; i < endIndex; i++) {
                    const event = eventsData[i];
                    if (event.Shipped) {
                        eventContainer.append(createEventCard(event, "Shipped", "#6ac7f2"));
                    }
                    if (event.Arriving) {
                        eventContainer.append(createEventCard(event, "Arriving", "orange"));
                    }


                }

                // Render pagination controls
                renderPagination();
            }

            // Function to create a single event card
            function createEventCard(event, status, color) {
                return `
            <a href="#" class="eventCardTemplate mb-4 d-block">
                <div class="row align-items-center">
                    <div class="col-md-5">
                        <p class="mb-0 text-start">${event.JobRef || "N/A"}</p>
                    </div>
                    <div class="col-md-1">
                        <p class="mb-0 text-center" style="color: ${color}; font-weight: bold;">${status}</p>
                    </div>
                    <div class="col-md-3 offset-md-3">
                        <p class="mb-0 text-end">${event[status] || "N/A"}</p>
                    </div>
                </div>
                <div class="flex items-center justify-between list-item-footer mt-1">
                    <span class="text-darkblue flex-grow-1" style="border-top: 2px solid #00008b; height: 2px;"></span>
                    <div class="arrow-icon-wrapper">
                        <div class="arrow-icon">
                            <div class="triangleline"></div>
                            <i class="arrow arrow-right"></i>
                        </div>
                    </div>
                    <span class="text-darkblue flex-grow-1" style="border-top: 2px solid #00008b; height: 2px;"></span>
                </div>
            </a>`;
            }

            // Function to render pagination controls
            // Function to render pagination controls with sliding window
            function renderPagination() {
                const paginationContainer = $("#paginationContainer");
                paginationContainer.empty();
                const totalPages = Math.ceil(eventsData.length / itemsPerPage);
                const maxVisiblePages = 3; // Number of pages to display at once
                const startPage = Math.max(currentPage - Math.floor(maxVisiblePages / 2), 1);
                const endPage = Math.min(startPage + maxVisiblePages - 1, totalPages);

                // Create a container with flexbox for alignment
                const flexContainer = $(`
        <div class="d-flex justify-content-between align-items-center">
            <span class="showing-text">Showing ${Math.min((currentPage - 1) * itemsPerPage + 1, eventsData.length)} to ${Math.min(currentPage * itemsPerPage, eventsData.length)} of ${eventsData.length} entries</span>
            <ul class="pagination mb-0"></ul>
        </div>
    `);

                const paginationControls = flexContainer.find(".pagination");

                // Add "Previous" button only if not on the first page
                if (currentPage > 1) {
                    paginationControls.append(`
            <li class="page-item">
                <a class="page-link" href="#" onclick="changePage(${currentPage - 1}, event)">
                    <span aria-hidden="true">&laquo;</span>
                </a>
            </li>
        `);
                }

                // Page Numbers
                for (let i = startPage; i <= endPage; i++) {
                    paginationControls.append(`
            <li class="page-item ${i === currentPage ? 'active' : ''}">
                <a class="page-link" href="#" onclick="changePage(${i}, event)">${i}</a>
            </li>
        `);
                }

                // Add "Next" button only if not on the last page
                if (currentPage < totalPages) {
                    paginationControls.append(`
            <li class="page-item">
                <a class="page-link" href="#" onclick="changePage(${currentPage + 1}, event)">
                    <span aria-hidden="true">&raquo;</span>
                </a>
            </li>
        `);
                }

                paginationContainer.append(flexContainer);
            }
            // Function to handle page change
            function changePage(page, event) {
                if (event) {
                    event.preventDefault(); // Prevent the default anchor behavior
                }
                // Check for valid page numbers
                if (page < 1 || page > Math.ceil(eventsData.length / itemsPerPage)) {
                    return;
                }
                // Update the current page
                currentPage = page;
                // Render the updated events data and pagination
                renderPage(); // Call the correct rendering function
            }
            // Load upcoming events on page ready
            $(document).ready(function () {
                loadUpcomingEvents();
            });
        </script>

    <script >
        document.addEventListener("DOMContentLoaded", function () {
            let dropdown = document.getElementById('dropdown1');
            dropdown.value = '12 month'; // Set default value to 12 months

            // Trigger change event to apply default filter
            dropdown.dispatchEvent(new Event('change'));
    console.log('Triggering fetchFilteredData with "All"...');
    fetchFilteredData("All"); // Load initial data
});

let myChart;
let allData = []; // Numeric data array
let allLabels = []; // Labels (dates/months)
let selectedOrigin = 'All'; // Default origin
let selectedFilter = "12 month"; // Default month range

// Function to fetch filtered data based on selected origin
function fetchFilteredData(origin) {
    selectedOrigin = origin; // Update selected origin
    const data = { origin: selectedOrigin };

    fetch('Dashboard.aspx/GetChartData', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    .then(response => response.json()) // Parse JSON response
    .then(result => {
        console.log("Full Response:", result);

        // Extract data from ASP.NET WebMethod response
        let responseData = JSON.parse(result.d); // Use `d` property for ASP.NET response

        // Ensure correct parsing of labels (handling single-quoted JSON issue)
        try {
            allLabels = JSON.parse(responseData.labels.replace(/'/g, '"'));
        } catch (error) {
            console.error("Error parsing labels:", error);
            allLabels = [];
        }
        
        allData = responseData.data; // Numeric values

        console.log("Parsed Labels:", allLabels);
        console.log("Parsed Data:", allData);

        // Initialize chart or update it
        if (Array.isArray(allData) && Array.isArray(allLabels)) {
            if (!myChart) {
                initializeChart();
            } else {
                updateChart();
            }
        } else {
            console.error("Invalid format: Expected arrays for labels and data.");
        }
    })
    .catch(error => {
        console.error("Error fetching chart data:", error);
    });
}

// Function to initialize the chart
function initializeChart() {
    const ctx = document.getElementById('myChart').getContext('2d');
    myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: allLabels,
            datasets: [{
                data: allData,
                backgroundColor: ['#def4ff', '#b6e0f5', '#def4ff', '#b6e0f5'],
                borderWidth: 0
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    displayColors: false,
                    callbacks: {
                        title: tooltipItems => tooltipItems[0].label,
                        label: tooltipItem => `${selectedOrigin} Shipments: ${tooltipItem.raw}`
                    }
                }
            },
            scales: {
                x: {
                    grid: { display: false },
                    ticks: { maxRotation: 0, minRotation: 0, font: { size: 10, weight: "bold" } }
                },
                y: {
                    grid: { display: false },
                    ticks: { beginAtZero: true, font: { size: 10, weight: "bold" } }
                }
            }
        }
    });
}

// Function to update the chart
function updateChart() {
    myChart.data.labels = allLabels;
    myChart.data.datasets[0].data = allData;
    myChart.update();
}

// Handle origin dropdown change
const ddOrigin = document.getElementById('<%= DD_origin.ClientID %>');
        ddOrigin.addEventListener('change', function () {
            const selectedOriginValue = this.options[this.selectedIndex].text;
            fetchFilteredData(selectedOriginValue);
        });

        // Handle month range selection
        document.getElementById('dropdown1').addEventListener('change', function () {
            selectedFilter = this.value;
            let monthCount = getMonthCount(selectedFilter);

            // Get filtered data and update chart
            const filtered = filterData(monthCount);
            myChart.data.labels = filtered.labels;
            myChart.data.datasets[0].data = filtered.data;
            myChart.update();
        });

        // Function to determine month count based on selected range
        function getMonthCount(filter) {
            switch (filter) {
                case '1 month': return 1;
                case '2 month': return 2;
                case '6 month': return 6;
                case '12 month':
                default: return 12;
            }
        }

        // Function to filter data based on month count
        function filterData(monthCount) {
            let currentDate = new Date();
            let filteredData = [];
            let filteredLabels = [];

            for (let i = monthCount - 1; i >= 0; i--) {
                let targetMonth = new Date(currentDate.getFullYear(), currentDate.getMonth() - i, 1);
                let monthLabel = targetMonth.toLocaleString('default', { month: 'short', year: 'numeric' });

                console.log("Generated Label in filterData:", monthLabel);

                let index = allLabels.indexOf(monthLabel);
                if (index !== -1) {
                    filteredData.push(allData[index]);
                    filteredLabels.push(allLabels[index]);
                }
            }

            return { data: filteredData, labels: filteredLabels };
        }

    </script>
</asp:Content>


