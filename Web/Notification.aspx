<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Web_Notification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
    margin-right:200px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <div class="card-box">
                <div class="login-header">
                    <h4>Notification</h4>
                    <p>we may still send you important notifications about your account outside of your notification settings.</p>
                </div>
            <div class="security-group">
    <div class="verification-blk lock-clr">
        <div class="security-box">
            <i class="fa fa-ship"></i>
        </div>
        <div>
            <h4>Shipments</h4>
            <p>These are notifications for shipments when shipments will be created, updated, or deleted.</p>
        </div>
        <div class="material-switch security-switch">
            <input id="shipments" type="checkbox" class="update-setting" data-setting="shipments">
            <label for="shipments" class="badge-active"></label>
        </div>
    </div>
</div>

<div class="security-group">
    <div class="verification-blk phone-clr">
        <div class="security-box">
            <i class="fa fa-book"></i>
        </div>
        <div>
            <h4>Bookings</h4>
            <p>These are notifications for bookings when bookings will be created, updated, or deleted.</p>
        </div>
        <div class="material-switch security-switch">
            <input id="bookings" type="checkbox" class="update-setting" data-setting="booking">
            <label for="bookings" class="badge-active"></label>
        </div>
    </div>
</div>

<div class="security-group">
    <div class="verification-blk mail-clr">
        <div class="security-box">
            <i class="fa fa-book"></i>
        </div>
        <div>
            <h4>Custom</h4>
            <p>These are notifications for custom when custom will be created, updated, or deleted.</p>
        </div>
        <div class="material-switch security-switch">
            <input id="customs" type="checkbox" class="update-setting" data-setting="customs">
            <label for="customs" class="badge-active"></label>
        </div>
    </div>
</div>

<div class="security-group">
    <div class="verification-blk mail-clr">
        <div class="security-box">
            <img src="../Template/assets/img/icon/security-icon-03.svg">
        </div>
        <div>
            <h4>Email notifications</h4>
            <p>These email notifications will be sent according to the frequency chosen.</p>
        </div>
        <div class="material-switch security-switch">
            <div class="form-check form-check-inline">
                <input id="emailNotificationsOff" class="form-check-input update-setting" type="radio" name="emailNotification" data-setting="email_notifications" value="0">
                <label class="form-check-label">Off</label>
            </div>
            <div class="form-check form-check-inline">
                <input id="emailNotificationsPerChange" class="form-check-input update-setting" type="radio" name="emailNotification" data-setting="email_notifications" value="1">
                <label class="form-check-label">Per Change</label>
            </div>
        </div>
    </div>
</div>

            </div>
        </div>
    </div>

   
 <script>
     $(document).ready(function () {
         $(".update-setting").change(function () {
             var settingType = $(this).attr("data-setting");
             var value = $(this).is(":checked") ? 0 : 1;
             // For email notifications (radio buttons)
             if (settingType === "email_notifications") {
                 value = $("input[name='emailNotification']:checked").val();
             }
             $.ajax({
                 type: "POST",
                 url: "Notification.aspx/UpdateSetting",
                 data: JSON.stringify({ settingType: settingType, value: value }),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     var result = response.d;

                     Swal.fire({
                         title: '<div class="custom-title">Notification Updated</div>',
                         html: `
                        <div class="custom-divider"></div>
                        <p class="custom-message">${result.message}</p>
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
                             closeButton: "custom-close",
                         }
                     });
                 },
                 error: function (xhr, status, error) {
                     console.error(xhr.responseText);
                 }
             });
         });
     });

 </script>
</asp:Content>

