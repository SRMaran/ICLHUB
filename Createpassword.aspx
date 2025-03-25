<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Createpassword.aspx.cs" Inherits="Createpassword" %>

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
    <style>
        .account-box {
            padding: 25px;
        }
    </style>
</head>
<body class="login-page" style="overflow: hidden;">

    <div class="main-wrapper account-wrapper bg-wrapper">
        <div class="row">
            <div class="col-md-4">
                <div class="account-page ">
                    <div class="container">
                        <div class="account-logo">
                            <a href="#">
                                <img src="images/logo1.png" alt /></a>
                        </div>
                        <div class="account-box">
                            <div class="row">

                                <div class="login-header">
                                    <h3 class="text-center">Create Password </h3>
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

                                <div class="form-group">
                                    <label>New Password</label>
                                    <asp:TextBox ID="txt_Newpassword" runat="server" required="" TextMode="Password" class="form-control" placeholder="Password *"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>New Repeat Password</label>
                                    <asp:TextBox ID="txt_Repassword" runat="server" TextMode="Password" required="" class="form-control" placeholder="Confirm Password *"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToValidate="txt_Repassword"
                                        CssClass="ValidationError"
                                        ControlToCompare="txt_Newpassword"
                                        ErrorMessage="Password must be the same"
                                        ForeColor="Red" />
                                </div>
                                <div class="form-group mb-0 text-center">
                                    <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block account-btn" Text="submit" OnClick="btn_submit_Click"></asp:Button>
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
