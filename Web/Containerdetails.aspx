<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Containerdetails.aspx.cs" Inherits="Web_Containerdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
     <script>
     function submitSupportMessage() {
         var message = document.getElementById("ta_support").value;

         if (message.trim() === "") {
             alert("Please enter a message.");
             return;
         }

         $.ajax({
             type: "POST",
             url: "Containerdetails.aspx/SupportMessage",
             data: JSON.stringify({
                 shipmentId: shipmentId,
                 message: message
             }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d.indexOf("Error") === -1) {
                     $("#successModal").modal("show");
                     document.getElementById("ta_support").value = "";
                 } else {
                     alert(response.d); // Show error message
                 }
             },
             error: function (xhr, status, error) {
                 console.error("Error: " + error);
                 alert("An error occurred while sending the message.");
             }
         });
     }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-10"></div>
        <div class="col-md-2 text-end">
            <a href="Container.aspx" class="btn btn-primary btn-block account-btn"><i class="fas fa-undo"></i>Back</a>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="card-box">
                <div class="row">
                    <div class="login-header">
                        <h4>Container Details </h4>
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
                <asp:PlaceHolder ID="PH_containerviewdetails" runat="server"></asp:PlaceHolder>
            </div>
            <div class="card-box">
                <div class="row">
                    <div class="login-header">
                        <h4>Delivery Details  </h4>
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
                <asp:PlaceHolder ID="PH_containerviewdelivery" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card-box">
                <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-check-all label-icon"></i>
                    <asp:Label ID="lbl_success" runat="server"></asp:Label>
                    <a  onclick="location.reload(); return false;"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-block-helper label-icon"></i>
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <a  onclick="location.reload(); return false;" ><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div class="row">
                    <div class="login-header">
                        <h4>Support</h4>
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
                <p>Leave a message regarding your container, and one of our team will be in touch.</p>
                <div class="row">
                    <div class="form-group">
                        <textarea id="ta_support"  rows="5" cols="5" class="form-control" required="required" placeholder="Your Message"></textarea>
                    </div>
                    <div class="form-group">
                        <div class="form-group mb-0 text-lg-end">
                        <button type="button" class="btn btn-primary submit-btn mt-2" onclick="submitSupportMessage()">Submit</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
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
    <asp:Literal ID="containerno" runat="server" Visible="false"></asp:Literal>
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
        function submitSupportMessage() {
            var message = document.getElementById("ta_support").value;
            var containerno = '<%= containerno.Text %>';
            if (message.trim() === "" || containerno.trim() === "") {
                Swal.fire({
                    title: '<div class="custom-title">Error</div>',
                    html: `
               <div class="custom-divider"></div>
               <p class="custom-message">Please enter the message before submitting!</p>
           `,
                    showConfirmButton: false,
                    showCloseButton: true,
                    allowOutsideClick: false,
                    didOpen: () => {
                        document.body.classList.add("blur-background");
                    },
                    willClose: () => {
                        document.body.classList.remove("blur-background");
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
                url: "Containerdetails.aspx/SupportMessage",
                data: JSON.stringify({
                    containerno: containerno,
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
                                document.body.classList.add("blur-background");
                            },
                            willClose: () => {
                                document.body.classList.remove("blur-background");
                                location.reload();
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
</asp:Content>

