<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Resetpassword.aspx.cs" Inherits="Resetpassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />
    <title>ICL HUB</title>
    <link rel="stylesheet" type="text/css" href="Template/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/fontawesome.min.css" />
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/all.min.css" />
    <link rel="stylesheet" type="text/css" href="Template/assets/css/style.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@mdi/font/css/materialdesignicons.min.css">




    <script src="Template/assets/js/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="Template/assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <style>
        .account-box {
            padding: 25px;
        }
    </style>
    <script>
        function togglePasswordVisibility(inputId, iconId) {
            var passwordInput = document.getElementById(inputId);
            var toggleIcon = document.getElementById(iconId);

            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                toggleIcon.classList.remove('mdi-eye-outline');
                toggleIcon.classList.add('mdi-eye-off-outline');
            } else {
                passwordInput.type = 'password';
                toggleIcon.classList.remove('mdi-eye-off-outline');
                toggleIcon.classList.add('mdi-eye-outline');
            }
        }
</script>

</head>
<body class="login-page" style="overflow: hidden;">

    <div class="main-wrapper account-wrapper bg-wrapper">
        <div class="row">
            <div class="col-md-4">
                <div class="account-page ">
                    <div class="container">
                        <div class="account-logo">
                            <a href="#">
                                <img src="images/logo1.png" /></a>
                        </div>
                        <div class="account-box">

                            <div class="row">

                                <div class="login-header">
                                    <h3 class="text-center">Reset Password </h3>
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
                            <form id="form" runat="server">
                                <div class="form-group position-relative">
                                    <label>New Password</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Newpassword" runat="server" ClientIDMode="Static"
                                            TextMode="Password" required class="form-control pr-5"
                                            placeholder="Enter new password"></asp:TextBox>
                                        <span class="position-absolute end-0 top-50 translate-middle-y pe-3">
                                            <i class="mdi mdi-eye-outline" id="toggleIcon1" style="cursor: pointer; font-size: 1.2rem; color: #1d64d6;"
                                                onclick="togglePasswordVisibility('txt_Newpassword', 'toggleIcon1')"></i>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group position-relative">
                                    <label>Repeat New Password</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_Repassword" runat="server" ClientIDMode="Static"
                                            TextMode="Password" required class="form-control pr-5"
                                            placeholder="Confirm password"></asp:TextBox>
                                        <span class="position-absolute end-0 top-50 translate-middle-y pe-3">
                                            <i class="mdi mdi-eye-outline" id="toggleIcon2" style="cursor: pointer; font-size: 1.2rem; color: #1d64d6;"
                                                onclick="togglePasswordVisibility('txt_Repassword', 'toggleIcon2')"></i>
                                        </span>
                                    </div>

                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToValidate="txt_Repassword"
                                        ControlToCompare="txt_Newpassword"
                                        CssClass="ValidationError"
                                        ErrorMessage="Passwords must match"
                                        ForeColor="Red" />
                                </div>
                                <div class="form-group mb-0 text-center">
                                    <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block account-btn" Text="Submit" OnClick="btn_submit_Click"></asp:Button>
                                </div>
                                <div class="text-center register-link">
                                    <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="login.aspx">Back to Login</asp:HyperLink>
                                </div>
                            </form>

                            <div id="div_error" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                                <asp:Button runat="server" type="button" ID="click" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></asp:Button>
                                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                            </div>
                            <div id="success" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                                <asp:Button runat="server" type="button" ID="Button1" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></asp:Button>
                                <asp:Label ID="Label_success" runat="server"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <img src="images/backgroundimage%20(3).svg" alt="Logo" style="width: 103%; max-height: 70%; margin-top: -27px;" />
            </div>
        </div>
    </div>

    <div class="sidebar-overlay" data-reff></div>

    <script src="Template/assets/js/jquery-3.6.0.min.js" type="9c439a9b46d5d148a97f52d1-text/javascript"></script>

    <script src="Template/assets/js/popper.min.js" type="9c439a9b46d5d148a97f52d1-text/javascript"></script>
    <script src="Template/assets/js/bootstrap.bundle.min.js" type="9c439a9b46d5d148a97f52d1-text/javascript"></script>
    <script src="Template/assets/js/jquery.slimscroll.js" type="9c439a9b46d5d148a97f52d1-text/javascript"></script>
    <script src="Template/assets/js/app.js" type="9c439a9b46d5d148a97f52d1-text/javascript"></script>
</body>
</html>
