<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="Web_Tracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .hidden {
            display: none;
        }
    </style>
    <style>
.holder-blueTracking:hover {
    border-color: green;
    cursor: pointer;
}
.holder-successTracking:hover {
    border-color: skyblue;
    cursor: pointer;
}
.holder-infoTracking:hover {
    border-color: orange;
    cursor: pointer;
}
.holder-darkblueTracking:hover {
    border-color: darkblue;
    cursor: pointer;
}
</style>
     <script>
         function navigateToShimentDetails(shipmentid) {
             window.location.href = `Shipmentdetails.aspx?key=${shipmentid}`;
         }
     </script>
 <script>
    
     window.initMap = function () {
         var mapOptions = {
             center: { lat: 0, lng: 0 },
             zoom: 2,
             styles: [
                 {
                     featureType: "water",
                     elementType: "geometry",
                     stylers: [{ color: "#c9c9c9" }] // Blue color for oceans
                    

                 },
                 {
                     featureType: "landscape",
                     elementType: "geometry",
                     stylers: [{ color: "#f5f5f5" }] // White color for continents
                 }
             ]
         };
         var map = new google.maps.Map(document.getElementById("map"), mapOptions);
     };

         function loadShipmentDetails(shipmentId) {

         mapdetails(shipmentId);

        fetch('Tracking.aspx/GetShipmentDetails', {
             method: 'POST',
             headers: {
                 'Content-Type': 'application/json; charset=utf-8'
             },
             body: JSON.stringify({ shipmentId: shipmentId })
         })
             .then(response => response.json())
             .then(data => {
                 if (data.d) {
                     const shipmentDetails = JSON.parse(data.d); /

                  
                     document.getElementById("<%= lb_pickup.ClientID %>").innerText = shipmentDetails.Origin;
            document.getElementById("<%= lb_delivery.ClientID %>").innerText = shipmentDetails.Destination;

        } 
    })     .catch(error => {
                 console.error("Error fetching shipment details:", error);
             });
     }
         function mapdetails(ShipmentNo) {
         var associateArray = ShipmentNo; 

         $.ajax({
             type: "POST",
             url: "Tracking.aspx/mapdetails", 
             data: JSON.stringify({ str_ControlValue: associateArray }),
             contentType: "application/json; charset=utf-8", 
             dataType: "json", 
             success: OnSuccessCall,
             error: OnErrorCall 
         });
         }
         function OnSuccessCall(response) {

             let responseData;

             try {
                 responseData = typeof response.d === "string" ? JSON.parse(response.d) : response.d;
                 console.log("Parsed responseData:", responseData);
             } catch (error) {
                 console.error("JSON parsing error:", error);
                 return;
             }
             if (responseData && responseData.Result) {
                 let resultData = typeof responseData.Result === "string" ? JSON.parse(responseData.Result) : responseData.Result;

                 let marklinedata = resultData?.marklinedata;
                 let Status = resultData?.Status;
                 let live_point = resultData?.live_point;
                 let start_point = resultData?.start_point;
                 let end_point = resultData?.end_point;


                 if (Status === "success") {
                     var map = new google.maps.Map(document.getElementById('map'), {
                         center: { lat: 0, lng: 0 },
                         zoom: 2,
                         styles: [
                             {
                                 featureType: "water",
                                 elementType: "geometry",
                                 stylers: [{ color: "#c9c9c9" }] // Blue color for oceans


                             },
                             {
                                 featureType: "landscape",
                                 elementType: "geometry",
                                 stylers: [{ color: "#f5f5f5" }] // White color for continents
                             }
                         ]
                     });
                     addLatLong(map, marklinedata, live_point, start_point, end_point);
                 }
                 else {
                     var map = new google.maps.Map(document.getElementById('map'), {
                         center: { lat: 0, lng: 0 },
                         zoom: 2,
                         styles: [
                             {
                                 featureType: "water",
                                 elementType: "geometry",
                                 stylers: [{ color: "#c9c9c9" }] // Blue color for oceans


                             },
                             {
                                 featureType: "landscape",
                                 elementType: "geometry",
                                 stylers: [{ color: "#f5f5f5" }] // White color for continents
                             }
                         ]
                     });

                 };
             } else {
                 console.error("Invalid responseData structure:", responseData);
             }

             
         }
         function OnErrorCall(response) {
            
             console.error("Error occurred: ", response);
     }
     function addLatLong(map, marklinedata, live_point, start_point, end_point) {


         if (typeof marklinedata === "string") {
             try {

                 marklinedata = JSON.parse(marklinedata);
             } catch (error) {
                 console.error("Error parsing marklinedata string:", error);
                 return;
             }
         }


         if (!Array.isArray(marklinedata)) {
             console.error("marklinedata is not an array:", marklinedata);
             return;
         }


         marklinedata = marklinedata.map((item, index) => {
             if (typeof item === "string") {
                 try {

                     return JSON.parse(item);
                 } catch (err) {
                     console.error("Invalid JSON in marklinedata item:", item, err);
                     return null;
                 }
             } else {

                 return item;
             }
         }).filter(Boolean);

         console.log("Parsed marklinedata:", marklinedata);

         const starts = JSON.parse(start_point);
         const ends = JSON.parse(end_point);
         const live = JSON.parse(live_point);

         var stlat = {
             lat: parseFloat(starts.lat),
             lng: parseFloat(starts.lng)
         };
         var endtat = {
             lat: parseFloat(ends.lat),
             lng: parseFloat(ends.lng)
         };

         var livelat = {
             lat: parseFloat(live.lat),
             lng: parseFloat(live.lng)
         };

         

         var startMarker = new google.maps.Marker({
             position: stlat,
             map: map,
             icon: {
                 url: '../Template/assets/in_transit.png',
                 scaledSize: new google.maps.Size(10, 10)

             }
         });
         var liveMarker = new google.maps.Marker({
             position: livelat,
             map: map,
             icon: {
                 url: '../Template/assets/red-marker.gif',
                 scaledSize: new google.maps.Size(40, 40),
                 anchor: new google.maps.Point(22, 21)
               
             }
         });
      

         var livelatlngs = marklinedata.map((data) => ({
             lat: parseFloat(data.lat),
             lng: parseFloat(data.lng)
         }));

         var endMarker = new google.maps.Marker({
             position: endtat,
             map: map,
             icon: {
                 url: '../Template/assets/in_transit.png',
                 scaledSize: new google.maps.Size(10, 10)
             }
         });

         var lineCoordinates1 = [stlat, ...livelatlngs];
         var lineCoordinates2 = [...livelatlngs, endtat];

         var polyline1 = new google.maps.Polyline({
             path: lineCoordinates1,
             geodesic: true,
             strokeColor: '#5156be',
             strokeOpacity: 1.0,
             strokeWeight: 3
         });

         var polyline2 = new google.maps.Polyline({
             path: lineCoordinates2,
             geodesic: true,
             strokeColor: '#5156be',
             strokeOpacity: 0.1,
             strokeWeight: 3,
             icons: [{
                 icon: {
                     path: 'M 0,-1 0,1',
                     strokeOpacity: 5.1,
                     scale: 4
                 },
                 offset: '0',
                 repeat: '0px'
             }]
         });

         polyline1.setMap(map);
         polyline2.setMap(map);


         var bounds = new google.maps.LatLngBounds();
         lineCoordinates2.forEach((point) => bounds.extend(point));
         lineCoordinates1.forEach((point) => bounds.extend(point));
         map.fitBounds(bounds);




         var minZoom = 2;
         if (map.getZoom() > minZoom) {
             map.setZoom(minZoom);
         } else {
             console.log("Zoom level is already at or below the minimum allowed.");
         }
     }
    
 </script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const searchBox = document.getElementById("searchBox");
            const shipmentItems = document.querySelectorAll("#shipments a");

           
            searchBox.addEventListener("keyup", function () {
                const searchTerm = this.value.toLowerCase(); 

                shipmentItems.forEach(item => {
                   
                    const textContent = item.textContent.toLowerCase();

                  
                    if (textContent.includes(searchTerm)) {
                        item.classList.remove("hidden");
                    } else {
                        item.classList.add("hidden");
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="card-box">

            <div class="row">
                <div class="col-sm-12">
                    <div class="login-header">
                        <h4>Key</h4>
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
                <div class="col-md-6">
                    <a id="Origin" runat="server" class="active">
                        <div class="card-holder1 holder-blueTracking">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="premium-box1" style="margin-right: 1px;">
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="main-balance-blk">
                                        <div class="main-balance">
                                            <h6>At Origin Port:
                                                <span class="text-success">
                                                    <asp:Label ID="lb_origin" runat="server"></asp:Label></span>
                                            </h6>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-6">
                    <a id="Transit" runat="server">
                        <div class="card-holder1 holder-successTracking">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="premium-box2 " style="margin-right: 1px;">
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="main-balance-blk">
                                        <div class="main-balance" style="text-align: center">
                                            <h6>In Transit:
                                            <span class="text-info">
                                                <asp:Label ID="lb_transit" runat="server"></asp:Label></span>
                                            </h6>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-6">
                    <a id="destination" runat="server">
                        <div class="card-holder1 holder-infoTracking">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="premium-box3" style="margin-right: 1px;">
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="main-balance-blk">
                                        <div class="main-balance" style="text-align: center">
                                            <h6>At Destination Port:
                                            <span class="text-warning">
                                                <asp:Label ID="lb_destination" runat="server"></asp:Label></span>
                                            </h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-6">
                    <a id="gateout" runat="server">
                        <div class="card-holder1 holder-darkblueTracking">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="premium-box4 " style="margin-right: 1px;">
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="main-balance-blk">
                                        <div class="main-balance" style="text-align: center">
                                            <h6>Gate Out:
                                             <span class="text-blue-dark">
                                                 <asp:Label ID="lb_gateout" runat="server"></asp:Label></span>
                                            </h6>
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
    <div class="card-box">
        <div class="row">
            <div class="col-sm-12">
                <div class="login-header">
                    <h4>Map Details </h4>
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
                            <br />
                     <br />
                 <div class="row">
    <div id="map" style=" width:1250px; height:400px; overflow: hidden; transform: translate3d(0.9px, -35.8px, 0px)"></div>
</div>
                  
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Goods Details </h4>
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

                <div class="recent-date">
                    <div class="row">
                        <div class="form-group">
                            <input type="text" id="searchBox" class="form-control" placeholder="Search by shipment" />
                            <span class="profile-views">
                                <img src="../Template/assets/img/icon/search-normal.svg" /></span>
                        </div>
                    </div>
                </div>
                <div class="fc-scroller fc-day-grid-container" style="overflow: hidden scroll; height: 400px;">
                    <asp:PlaceHolder ID="PH_Goodsdetails" runat="server"></asp:PlaceHolder>
                </div>
                <div class="topnav-dropdown-footer"></div>
            </div>
        </div>

        <div class=" col-md-6 ">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Tracking Details </h4>
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
                    <div class="card-holder holder-warning">
                        <div class="main-balance-blk">
                            <div class="main-balanceTrack1">
                                <h4>Pickup Location</h4>
                                <p class="content mt-2">
                                    <asp:Label ID="lb_pickup" runat="server"></asp:Label></p>
                            </div>
                            <i class="feather-map-pin" style="color: blue; font-size: 35px;"></i>
                        </div>
                    </div>
                    <div class="card-holder holder-successTrack ">
                        <div class="main-balance-blk">
                            <div class="main-balanceTrack">
                                <h4>Delivery Location</h4>
                                <p class="content mt-2">
                                    <asp:Label ID="lb_delivery" runat="server"></asp:Label></p>
                            </div>
                            <i class="feather-map-pin" style="color: #5fca99; font-size: 35px;"></i>
                        </div>
                    </div>

                    <div class="email-content">
                        <div class="table-responsive" >
                            <table class="table table-inbox table-hover" id="eventTable">
                                <tbody>
                                    <asp:PlaceHolder ID="PH_Mapdetails" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />

  
</asp:Content>


