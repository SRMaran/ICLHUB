<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Bookingdetails.aspx.cs" Inherits="Web_Bookingdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        #po {
            color: #112560;
            font-family: transducer-extended, sans-serif;
            font-size: 15px;
            font-weight: 500;
            letter-spacing: 0;
            line-height: 18px;
            text-align: left;
            --tw-bg-opacity: 1;
            background-color: rgb(255 255 255 / var(--tw-bg-opacity));
            padding-top: 26px;
        }
        .modal-success-message {
  color: #1d64d6;
  font-family: proxima-nova, sans-serif;
  font-size: 20px;
  font-weight: 400;
  letter-spacing: 0;
  line-height: 24px;
}
        .success-message {
        color: #1d64d6;
        text-align:center;
    }
        .custom-modal-size {
  max-width: 700px; 
  width: 90%; 
}


    </style>
    <style>
        .popup {
            display: none; /* Hidden by default */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent background */
            z-index: 1000;
        }

        /* Popup content */
        .popup-content {
            position: absolute;
            top: 350px;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        /* Close button */
        .close {
            position: absolute;
            top: 10px;
            right: 10px;
            font-size: 10px;
            cursor: pointer;
        }
    </style>
                   <style>
        body {
            background-color: #e8f5fc;
        }
/* .blur-background {
    filter: blur(5px);
    pointer-events: none;
}*/
  .custom-alert {
    width: 420px;
    padding: 20px;
    border-radius: 12px;
    text-align: left;
    position: relative;
}

.custom-title {
    font-size: 15px;  /* Reduced size */
    font-weight: 500;
    font-family: transducer-extended, sans-serif;
    color: #112560;
    text-align: left;
    margin-bottom: 5px;
    margin-right:105px;
    letter-spacing: 0;
    line-height: 18px;
}

.custom-close {
    position: absolute;
    top: 15px;
    right: 15px;
    font-size: 18px;
    color: #7d7d7d;  /* Removed red color */
    border: none !important; /* Removes any outline */
    background: none !important; /* Ensures no extra styling */
    cursor: pointer;
}

.custom-divider {
    width: 100%;
    height: 1px;
    background-color: #ccc;
    margin: 10px 0;
}

.custom-message {
    text-align: center;
    font-size: 16px;
    color: #0056b3;
    margin-top: 10px;
}


    </style>
    <%-- Support mail--%>
 <script>
     function submitSupportMessage() {

         var message = document.getElementById("ta_support").value;
         if (message.trim() === "") {
             Swal.fire({
                 title: '<div class="custom-title">Error</div>',
                 html: `
                 <div class="custom-divider"></div>
                 <p class="custom-message">Please enter the message before submitting!</p>
             `,
                 showConfirmButton: false,
                 showCloseButton: true,
                 allowOutsideClick: false,  // Prevents closing when clicking outside
                 didOpen: () => {
                     document.body.classList.add("blur-background"); // Apply blur
                 },
                 willClose: () => {
                     document.body.classList.remove("blur-background"); // Remove blur
                 },
                 customClass: {
                     popup: "custom-alert",
                     title: "custom-title",
                     closeButton: "custom-close"
                 }
             });
             return;
         }

         $.ajax({
             type: "POST",
             url: "Bookingdetails.aspx/SupportMessage",
             data: JSON.stringify({
                 bookingId: bookingId,
                 message: message
             }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d.indexOf("Error") === -1) {
                     Swal.fire({
                         title: '<div class="custom-title">Submitted</div>',
                         html: `
              <div class="custom-divider"></div>
              <p class="custom-message">Your data has been saved successfully</p>
          `,
                         showConfirmButton: false,
                         showCloseButton: true,
                         allowOutsideClick: false,
                         didOpen: () => {
                             document.body.classList.add("blur-background"); // Apply blur
                         },
                         willClose: () => {
                             document.body.classList.remove("blur-background"); // Remove blur
                             location.reload(); // Reload page after alert is closed
                         },
                         customClass: {
                             popup: "custom-alert",
                             title: "custom-title",
                             closeButton: "custom-close"
                         }
                     });

                 } else {
                     Swal.fire({
                         icon: 'error',
                         title: 'Error!',
                         text: response.d,
                         confirmButtonText: 'OK'
                     });
                 }
             },
             error: function (xhr, status, error) {
                 console.error("Error: " + error);
                 Swal.fire({
                     icon: 'error',
                     title: 'Error!',
                     text: 'An error occurred while sending the message.',
                     confirmButtonText: 'OK'
                 });
             }
         });
     }
 </script>  

    <%-- Icon to Download button Click--%>
    <script>
        function downloadFile1(bookingId, fileName) {
            if (!bookingId || !fileName) {
                alert("Invalid booking ID or file name.");
                return;
            }
            $.ajax({
                type: "POST",
                url: "Bookingdetails.aspx/DownloadDocument",
                data: JSON.stringify({ bookingId: bookingId, fileName: fileName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (!response.d.startsWith("Error")) {
                        let byteCharacters = atob(response.d); // Decode Base64 to binary
                        let byteNumbers = new Array(byteCharacters.length);
                        for (let i = 0; i < byteCharacters.length; i++) {
                            byteNumbers[i] = byteCharacters.charCodeAt(i);
                        }
                        let byteArray = new Uint8Array(byteNumbers);
                        let blob = new Blob([byteArray], { type: "application/octet-stream" });

                        // ✅ Open file download in new tab
                        let url = window.URL.createObjectURL(blob);
                        let a = document.createElement("a");
                        a.href = url;
                        a.download = fileName; // Ensure correct file download
                        document.body.appendChild(a);
                        a.click();
                        document.body.removeChild(a);
                    } else {
                        alert(response.d); // Show error message from the server
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                    alert("An error occurred while processing your request.");
                }
            });
        }
     </script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="overflow: hidden;">

        <div class="row">
            <div class="col-md-10"></div>
            <div class="col-md-2 text-end">
                <a href="Booking.aspx" class="btn btn-primary btn-block account-btn"><i class="fas fa-undo"></i>Back</a>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Files </h4>
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
                <div class="wallet-group">
                    <div class="row">
                        <div class="col-md-6">
                            <a id="openPopup" class="active">
                                <div class="card-holder2 holder-blue">
                                    <div class="main-balance-blk">
                                        <div class="main-balance">
                                            <h3>UPLOAD</h3>
                                            <h4>File For ICL review</h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-11">
                                                <div class="premium-box">
                                                    <i class="feather-upload-cloud" style="font-size: 50px;"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-6">
                            <a id="openPopup1" class="active">
                                <div class="card-holder2 holder-blue">
                                    <div class="main-balance-blk">
                                        <div class="main-balance" style="">
                                            <h3>DOWNLOAD</h3>
                                            <h4>File related to this shipment</h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-11">
                                                <div class="premium-box">
                                                    <i class="feather-download-cloud" style="font-size: 50px;"></i>
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
        </div>
        <div class="col-sm-6">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Order Details  </h4>
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
                <asp:PlaceHolder ID="PH_Bookorder" runat="server"></asp:PlaceHolder>


            </div>
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Delivery Details  </h4>
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
                <asp:PlaceHolder ID="PH_Bookdelivery" runat="server"></asp:PlaceHolder>
            </div>

            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Shipment Details  </h4>
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

                <asp:PlaceHolder ID="PH_Bookshipdetails" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card-box">

                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Goods Details  </h4>
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
                <asp:PlaceHolder ID="PH_Bookgooddetails" runat="server"></asp:PlaceHolder>

            </div>

            <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                <i class="mdi mdi-check-all label-icon"></i>
                <asp:Label ID="lbl_success" runat="server"></asp:Label>
                <a href="CompanyCreation.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
            </div>
            <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                <i class="mdi mdi-block-helper label-icon"></i>
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                <a href="CompanyCreation.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
            </div>
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-header">
                            <h4>Support  </h4>
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
                <p>Leave a message regarding your shipment, and one of our team will be in touch.</p>
                <div class="row">

                    <div class="form-group">
                        <textarea id="ta_support" rows="5" cols="5" class="form-control" placeholder="Your Message"></textarea>
                        <asp:Label ID="lb_supporterror" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group mb-0 text-center">
                        <button type="button" class="btn btn-primary submit-btn mt-2" onclick="submitSupportMessage()">Submit</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="popupPanel" class="popup" style="overflow-y: scroll;">
            <div class="card-box">
                <div class="popup-content">
                    <div class="row text-end">
                        <div class="col-sm-10"></div>
                        <div class="col-sm-2 text-end">
                            <a class="account-delete btn btn-outline-danger" id="close1" title="Delete">X</a>
                        </div>
                    </div>
                    <div class="login-header">
                        <h4 class="text-start">Upload a file </h4>
                    </div>
                    <div>
                        <p>Use this function to upload a file to the ICL cloud. (.pdf .csv .word supported)</p>
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
                    <div class="form-group">
                        <label>Document Type</label>
                        <asp:DropDownList ID="ddl_document" runat="server" CssClass="form-select p-1" OnSelectedIndexChanged="ddl_document_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Select Document Types" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Booking Confirmation" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pre Alert" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Bill of Lading" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Airway Bill/Ocean Bill of Lading" Value="4"></asp:ListItem>
                            <asp:ListItem Text="House Waybill/Bill of Lading" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Original Bill of Lading" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Commercial Invoice" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Packing List" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Insurance Certificate" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Pysto Certificate" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Entry Print" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Customs Status Advice " Value="12"></asp:ListItem>
                            <asp:ListItem Text="Customs Autority" Value="13"></asp:ListItem>
                            <asp:ListItem Text="Invoice" Value="14"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lb_error" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Document Upload</label>
                        <asp:FileUpload ID="fu_fileupload" runat="server" CssClass="form-control" AllowMultiple="true" ClientIDMode="Static" />
                        <asp:Label ID="lb_errorfile" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group">

                        <div class="table-responsive fc-scroller fc-day-grid-container" style="overflow: hidden scroll; height: 200px; width: 600px">
                            <table class="datatable table  table-striped mb-0">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>Document</th>
                                        <th>Download</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:PlaceHolder ID="PH_Showuploadfile" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="form-group text-lg-end">
                            <asp:Button ID="btn_upload" runat="server" class="btn btn-primary submit-btn mt-2" Text="Upload" OnClick="btn_upload_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="popupPanel1" class="popup" style="overflow-y: scroll;">
            <div class="card-box">
                <div class="popup-content">
                    <div class="row text-end">
                        <div class="col-sm-10"></div>
                        <div class="col-sm-2 text-end">
                            <a class="account-delete btn btn-outline-danger" onclick="location.reload(); return false;" title="Delete">X</a>
                        </div>
                    </div>
                    <div class="login-header">
                        <h4 class="text-start">Download a file </h4>
                    </div>
                    <div>
                        <p class="text-start">Download available files related to this shipment.</p>
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
                    <div class="fc-scroller fc-day-grid-container" style="overflow: hidden scroll; height: 300px; width: 600px">
                        <asp:PlaceHolder ID="PH_bookdoc" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="form-group">
                        <div class="form-group text-lg-end">
                            <asp:Button ID="btn_download" runat="server" class="btn btn-primary submit-btn mt-2" Text="Download" OnClick="btn_download_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal -->
<div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="successModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered custom-modal-size" role="document">
    <div class="modal-content">
      <div class="modal-header" id="po">
        <h4 class="modal-title" id="successModalLabel">Submit Data</h4>
      <button type="button" class="close" 
        style="color: #1d64d6; font-size: 40px; border: none; background: none; outline: none; margin-top:10px" 
        data-dismiss="modal" 
        onclick="submitSupportMessagepopup()">
  &times;
</button>
      </div>
      <div class="modal-body text-center">
        <h4 class="success-message">Your data has been submitted successfully</h4>
      </div>
    </div>
  </div>
</div>

    </div>


    <asp:Literal ID="bookingno" runat="server" Visible="false"></asp:Literal>
    <asp:HiddenField ID="hfPopupVisible" runat="server" ClientIDMode="Static" />
    <script>
        document.getElementById("close").addEventListener("click", function () {
            document.getElementById("popupPanel1").style.display = "none";
        });
     </script>
    <script>
        document.getElementById("close1").addEventListener("click", function () {
            document.getElementById("popupPanel").style.display = "none";
        });
    </script>
    <script>
        const openPopup = document.getElementById('openPopup');
        const closePopup = document.getElementById('closePopup');
        const popupPanel = document.getElementById('popupPanel');
        // Open popup
        openPopup.addEventListener('click', () => {
            popupPanel.style.display = 'block';
        });
        // Close popup
        closePopup.addEventListener('click', () => {
            popupPanel.style.display = 'none';
        });
        // Close popup by clicking outside the content
        window.addEventListener('click', (event) => {
            if (event.target === popupPanel) {
                popupPanel.style.display = 'none';
            }
        });
    </script>
    <script>
        const openPopup1 = document.getElementById('openPopup1');
        const closePopup1 = document.getElementById('closePopup1');
        const popupPanel1 = document.getElementById('popupPanel1');
        // Open popup
        openPopup1.addEventListener('click', () => {
            popupPanel1.style.display = 'block';
        });
        // Close popup
        closePopup1.addEventListener('click', () => {
            popupPanel1.style.display = 'none';
        });
        // Close popup by clicking outside the content
        window.addEventListener('click', (event) => {
            if (event.target === popupPanel1) {
                popupPanel1.style.display = 'none';
            }
        });
    </script>
    <%-- file upload to Download button Click--%>
    <script>
        function downloadFile(bookingId, fileName) {
            if (!bookingId || !fileName) {
                alert("Invalid file name.");
                return;
            }

            $.ajax({
                type: "POST",
                url: "Bookingdetails.aspx/UploadDownloadDocument",
                data: JSON.stringify({ bookingId: bookingId, fileName: fileName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.startsWith("/Booking/")) {
                        let filePath = response.d;
                        window.open(filePath, "_blank"); // Open in new tab
                    } else {
                        alert(response.d); // Show error message
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                    alert("An error occurred while processing your request.");
                }
            });
        }

    </script>
    <%-- popup--%>
    <script>
        $(document).ready(function () {
            if ($('#hfPopupVisible').val() === "true") {
                $("#popupPanel").show(); // Reopen the popup
            }

            $("#close1").click(function () {
                $("#popupPanel").hide(); // Hide popup
                $('#hfPopupVisible').val("false"); // Reset popup state
            });
        });
    </script>
      
    <script>
       
        function submitSupportMessagepopup() {

            $("#successModal").modal("hide");
        }
    </script>
</asp:Content>

