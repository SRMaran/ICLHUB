<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<html lang="en" data-bs-theme="dark">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0">
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico">
    <title>ICL HUB</title>
    <link rel="stylesheet" type="text/css" href="Template/assets/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/fontawesome.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/plugins/fontawesome/css/all.min.css">
    <link rel="stylesheet" type="text/css" href="Template/assets/css/style.css">


    <script src="Template/assets/js/jquery-3.6.0.min.js" type="text/javascript"></script>
     <script src="Template/assets/js/bootstrap.bundle.min.js" type="text/javascript"></script>



    <script>
        function visibility() {
            var x = document.getElementById("<%= txt_Password.ClientID %>");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
    <style>
    .ds-none {
        display: block !important
    }

    @media (max-width: 992px) {
        .ds-sm-block {
            display: none !important
        }
    }

    @media (max-width: 1280px) {

        .ds-sm-block {
            display: none !important
        }

        .col-md-4 {
            margin: 0 auto;
            display: flex;
            justify-content: center;
        }
    }

    @media (max-width: 1024px) {

        .ds-sm-block {
            display: none !important
        }

        .col-md-4 {
            margin: 0 auto;
            display: flex;
            justify-content: center;
        }
    }

  </style>
        <style>                .close {    color: black;    font-size: 16px;    transition: color 0.3s ease-in-out; }.close:hover {        border-color: white;    color: red;    cursor: pointer; }               .bg-danger {                   height: 55px;               }               .modal-title {                   font-size: 16px;               }               .bg-dangers {
    height: 55px;
}
.bg-dangers, .badge-dangers {
    background-color: #1d64d6 !important;
}    </style>
</head>
<body style="overflow: hidden;">
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
                            <div class="row">
                                <div class="login-header">
                                    <h4 class="text-center">Log In Details </h4>
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
                            <form id="form1" runat="server" class="form-signin">
                                <div class="form-group">
                                    <label>
                                        Username or Email*</label>
                                    <asp:TextBox ID="txt_Username" runat="server" class="form-control" required="" placeholder="Username *"></asp:TextBox>
                                    <span class="profile-views">
                                        <img src="Template/assets/img/icon/lock-icon-01.svg" /></span>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Password*</label>
                                    <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" class="form-control" required="" placeholder="Password *"></asp:TextBox>
                                    <span class="profile-views">
                                        <img src="Template/assets/img/icon/lock-icon-02.svg" onclick="visibility()" /></span>
                                </div>

                                <div class="text-center">
                                    <a href="ForgotPassword.aspx">I’ve forgotten my username or password</a>
                                </div>
                                <br />
                                <div class="form-group text-center">
                                    <asp:Button ID="btn_Submit" runat="server" class="btn btn-primary account-btn" OnClick="btn_Submit_Click" Text="Login"></asp:Button>
                                </div>
                                <div class="form-group">
                                    <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" visible="false">
                                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
                                        <a href="login.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-md-8 ds-none ds-sm-block">
                <img src="images/backgroundimage%20(3).svg" alt="Logo" style="        width: 103%;
        max-height: 70%;
        margin-top: -27px;">
            </div>

              
        </div>

    <div id="Deleterop" runat="server"  class="modal fade show" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header bg-dangers">
                <h6 class="modal-title text-white">Please note</h6>
                <button type="button" class="close" onclick="closedriv()" data-bs-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <div class="swal2-icon swal2-warning swal2-animate-warning-icon" style="display: flex;"></div>
                <p class="text-center" style="font-size: 20px;">
                    The organisation is not assigned to the client.
                </p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" onclick="closedriv()" data-bs-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>


    </div>
    <div class="sidebar-overlay" data-reff></div>
    <script src="Template/assets/js/jquery-3.6.0.min.js" type="a08ce20259470dc71facf060-text/javascript"></script>
    <script src="Template/assets/js/popper.min.js" type="a08ce20259470dc71facf060-text/javascript"></script>
    <script src="Template/assets/js/bootstrap.bundle.min.js" type="a08ce20259470dc71facf060-text/javascript"></script>
    <script src="Template/assets/js/jquery.slimscroll.js" type="a08ce20259470dc71facf060-text/javascript"></script>
    <script src="Template/assets/js/app.js" type="a08ce20259470dc71facf060-text/javascript"></script>

   <script>
       $(document).ready(function () {
           $('#Deleterop').modal('show'); // Auto-show modal on page load
       });

       function closedriv() {
           $('#Deleterop').modal('hide');
       }
</script>
</body>
</html>