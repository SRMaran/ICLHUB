<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgetPassword" %>

<!DOCTYPE html>
<html lang="en" data-bs-theme="dark">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0">
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico">
    <link rel="stylesheet" type="text/css" href="Template/assets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/fontawesome.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/all.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/css/style.css">
    <link href="Template/assets/css/bootstrap.min.css" rel="stylesheet" />
    <title>ICL HUB</title>
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
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div class="main-wrapper account-wrapper bg-wrapper">
            <div class="row">
                <div class="col-md-4">
                    <div class="account-page">
                        <div class="account-center">
                            <div class="account-logo">
                                <a href="#">
                                    <img src="images/logo1.png" alt="Logo"></a>

                            </div>
                            <div class="account-box">
                                <div class="login-header">
                                    <h4 class="text-center">Forgot Password</h4>
                                </div>
                                <br />
                                <div class="form-signin" action="#">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:TextBox ID="txt_email" runat="server" required="" CssClass="form-control" Placeholder="Enter Email *" AutoCompleteType="Email"></asp:TextBox>
                                        </div>
                                        <br />
                                        <div class="form-group text-center">
                                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary account-btn" Text="Reset Password"  OnClientClick="return triggerResetPassword();"/>
                                        </div>
                                        <div class="text-center">
                                                  <a href="login.aspx">Back to Login</a>
                                            <%--<asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="login.aspx">Back to Login</asp:HyperLink>--%>
                                        </div>
                                    </div>
                                    <div id="div_error" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                                        <a href="ForgotPassword.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                    </div>
                                    <div id="success" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                                        <asp:Label ID="Label_success" runat="server"></asp:Label>
                                        <a href="ForgotPassword.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-8">
                    <img src="images/backgroundimage%20(3).svg" alt="Logo" style="width: 103%; max-height: 70%; margin-top: -27px;">
                </div>
            </div>
        </div>
        <div class="sidebar-overlay" data-reff></div>
        <script src="Template/assets/js/jquery-3.6.0.min.js" type="166b723c8416f31a02192d0c-text/javascript"></script>
        <script src="Template/assets/js/popper.min.js" type="166b723c8416f31a02192d0c-text/javascript"></script>
        <script src="Template/assets/js/bootstrap.bundle.min.js" type="166b723c8416f31a02192d0c-text/javascript"></script>
        <script src="Template/assets/js/jquery.slimscroll.js" type="166b723c8416f31a02192d0c-text/javascript"></script>
        <script src="Template/assets/js/app.js" type="166b723c8416f31a02192d0c-text/javascript"></script>
        <script src="Template/assets/plugins/sweetalert/sweetalert2.all.min.js"></script>
        <script src="Template/assets/plugins/sweetalert/sweetalerts.min.js"></script>
        <script src="Template/Template/Template/cdn-cgi/scripts/7d0fa10a/cloudflare-static/rocket-loader.min.js" data-cf-settings="166b723c8416f31a02192d0c-|49" defer></script>
    </form>

    <script src="path/to/sweetalert.min.js"></script>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script>
<%--    function triggerResetPassword() {
        var email = document.getElementById("<%= txt_email.ClientID %>").value;

        if (email.trim() === "") {
            Swal.fire({
                icon: "warning",
                title: "Reset Password",
                text: "Password Reset Email Sent",
                confirmButtonClass: "btn btn-primary",
                buttonsStyling: false
            });
            return false; 
        }

        // ✅ Show loading SweetAlert
        Swal.fire({
            title: "Processing...",
            text: "Please wait while we verify your email",
            icon: "info",
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        // ✅ Call the backend via AJAX (using PageMethods or WebMethod)
        $.ajax({
            type: "POST",
            url: "ForgotPassword.aspx/ResetPassword",  // WebMethod in the same page
            data: JSON.stringify({ email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var result = response.d; // Get response from WebMethod

                Swal.fire({
                    icon: result.status, // "success" or "error"
                    title: "Reset Password",
                    text: result.message,
                    confirmButtonClass: "btn btn-primary",
                    buttonsStyling: false
                });
            },
            error: function () {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Something went wrong! Please try again later.",
                    confirmButtonClass: "btn btn-primary",
                    buttonsStyling: false
                });
            }
        });

        return false; 
    }--%>
<%--    function triggerResetPassword() {
        var email = document.getElementById("<%= txt_email.ClientID %>").value;

        if (email.trim() === "") {
            Swal.fire({
                title: '<div class="custom-title">Reset Password</div>',
                html: `
                <div class="custom-divider"></div>
                <p class="custom-message">Password Reset Email Sent</p>
            `,
                showConfirmButton: false,
                showCloseButton: true,
                customClass: {
                    popup: "custom-alert",
                    title: "custom-title",
                    closeButton: "custom-close",
                }
            });
            return false;
        }

        $.ajax({
            type: "POST",
            url: "ForgotPassword.aspx/ResetPassword",
            data: JSON.stringify({ email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var result = response.d;

                Swal.fire({
                    title: '<div class="custom-title">Reset Password</div>',
                    html: `
                    <div class="custom-divider"></div>
                    <p class="custom-message">${result.message}</p>
                `,
                    showConfirmButton: false,
                    showCloseButton: true,
                    customClass: {
                        popup: "custom-alert",
                        title: "custom-title",
                        closeButton: "custom-close",
                    }
                });
            },
            error: function () {
                Swal.fire({
                    title: '<div class="custom-title">Reset Password</div>',
                    html: `
                    <div class="custom-divider"></div>
                    <p class="custom-message">Email address does not exist</p>
                `,
                    showConfirmButton: false,
                    showCloseButton: true,
                    customClass: {
                        popup: "custom-alert",
                        title: "custom-title",
                        closeButton: "custom-close",
                    }
                });
            }
        });

        return false;
    }--%>
    function triggerResetPassword() {
        var email = document.getElementById("<%= txt_email.ClientID %>").value;

        if (email.trim() === "") {
            Swal.fire({
                title: '<div class="custom-title">Reset Password</div>',
                html: `
                <div class="custom-divider"></div>
                <p class="custom-message">Please enter the Email</p>
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
                    closeButton: "custom-close",
                }
            });
            return false;
        }

        $.ajax({
            type: "POST",
            url: "ForgotPassword.aspx/ResetPassword",
            data: JSON.stringify({ email: email }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var result = response.d;

                Swal.fire({
                    title: '<div class="custom-title">Reset Password</div>',
                    html: `
                    <div class="custom-divider"></div>
                    <p class="custom-message">${result.message}</p>
                `,
                    showConfirmButton: false,
                    showCloseButton: true,
                    allowOutsideClick: false, // Prevents closing when clicking outside
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
            error: function () {
                Swal.fire({
                    title: '<div class="custom-title">Reset Password</div>',
                    html: `
                    <div class="custom-divider"></div>
                    <p class="custom-message">Email address does not exist</p>
                `,
                    showConfirmButton: false,
                    showCloseButton: true,
                    allowOutsideClick: false, // Prevents closing when clicking outside
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
            }
        });

        return false;
    }

</script>

</body>
</h
