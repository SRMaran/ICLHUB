<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="Employee_ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        body {
            background: #f7f7ff;
            margin-top: 20px;
        }

        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            border: 0 solid transparent;
            border-radius: .25rem;
            margin-bottom: 1.5rem;
            box-shadow: 0 2px 6px 0 rgb(218 218 253 / 65%), 0 2px 6px 0 rgb(206 206 238 / 54%);
        }

        .me-2 {
            margin-right: .5rem !important;
        }
        /*avatar*/
        .avatar-option {
            margin: 10px;
            cursor: pointer;
        }

        .avatar-img {
            width: 94px;
            height: 94px;
            border-radius: 50%;
            border: 3px solid transparent;
            transition: 0.3s;
        }

            .avatar-img:hover, .avatar-option.selected img {
                border-color: #1d64d6;
            }
            .profile-image {
    border: 4px solid #e8f5fc; 
}

    </style>
  <script type="text/javascript">
      function selectAvatar(fileName) {
          let selectedAvatar;

          if (fileName.startsWith("ICL_AVATARS")) {
              selectedAvatar = "../Template/assets/img/Avatar/" + fileName;
          } else {
              selectedAvatar = "../images/" + fileName;
          }

          document.getElementById('<%= hf_SelectedAvatar.ClientID %>').value = selectedAvatar;
         document.getElementById('<%= img_profile.ClientID %>').src = selectedAvatar;
     }

      document.addEventListener("DOMContentLoaded", function () {
          let avatars = document.querySelectorAll(".avatar-option img");
          avatars.forEach(avatar => {
              avatar.addEventListener("click", function () {
                  let selectedAvatar = this.src.replace(window.location.origin, ".."); 
                  document.getElementById('<%= hf_SelectedAvatar.ClientID %>').value = selectedAvatar;
              });
          });
      });
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-sm-7 col-6">
            <div class="login-header">
                <h4>Profile Information</h4>
            </div>
        </div>
        <div class="col-sm-5 col-6 text-end m-b-30">
        </div>
    </div>
    <div class="row">
        <div class="col-sm-1"></div>
        <div class="col-sm-10">
            <div class="card-header">
                <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-check-all label-icon"></i>
                    <asp:Label ID="lbl_success" runat="server"></asp:Label>
                    <a href="ProfilePage.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-block-helper label-icon"></i>
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <a class="account-delete btn btn-outline-danger" href="ProfilePage.aspx" title="Delete">&times;</a>
                </div>
            </div>
        </div>
        <div class="col-sm-1"></div>
    </div>
    <div class="container">
        <div class="main-body">
            <div class="row">
                <!-- Profile Card (First Card) -->
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex flex-column align-items-center text-center">
                                <asp:Image ID="img_profile" runat="server" CssClass="profile-image rounded-circle p-1" Width="110" />

                                <div class="mt-3">
                                    <h4>
                                        <asp:Label ID="lb_name" runat="server"></asp:Label></h4>

                                </div>
                            </div>


                            <div class="card-header">
                                <h4>Avatar</h4>
                                <p>Choose an avatar in place of your profile picture.</p>
                            </div>
                        </div>
                            <div class="card-body text-center">
        <div class="d-flex flex-wrap justify-content-center">
            <asp:Repeater ID="rptAvatars" runat="server">
                <ItemTemplate>
    <a href="javascript:void(0);" class="avatar-option" onclick='selectAvatar("<%# Eval("AvatarFileName") %>")'>
        <img src='<%# "../Template/assets/img/Avatar/" + Eval("AvatarFileName") %>' class="avatar-img" />
    </a>
</ItemTemplate>

            </asp:Repeater>
        </div>
    </div>

    <!-- Hidden Field to Store Selected Avatar -->
    <asp:HiddenField ID="hf_SelectedAvatar" runat="server" />
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">



                        <div class="card-header">
                            <h4>Personal Details</h4>
                            <p>This will be displayed on your profile, as well as used for form submissions and requests.</p>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <!-- Left Column -->
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label>First Name <span style="color: red">&nbsp;*</span></label>
                                        <asp:TextBox ID="txt_firstname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ControlToValidate="txt_firstname" ErrorMessage="First name is required." CssClass="error-message" />
                                    </div>

                                    <div class="mb-3">
                                        <label>Email</label>
                                        <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="mb-3">
                                        <label>Address</label>
                                        <asp:TextBox ID="txt_address" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="mb-3">
                                        <label>Image</label>
                                        <asp:FileUpload ID="Fi_Updatepicture" runat="server" CssClass="form-control" accept="image/*" />
                                    </div>
                                </div>

                                <!-- Right Column -->
                                <div class="col-md-6">
                                    <div class="mb-3">
                                        <label>Last Name</label>
                                        <asp:TextBox ID="txt_lastname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="mb-3">
                                        <label>Phone</label>
                                        <asp:TextBox ID="txt_phone" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="mb-3">
                                        <label>Postcode</label>
                                        <asp:TextBox ID="txt_postcode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <!-- Notification Alerts -->
                            <div class="row mb-3">
                                <div class="col-12">
                                    <div id="div1" runat="server" class="alert alert-success alert-dismissible fade show" role="alert" visible="false">
                                        <i class="mdi mdi-check-all label-icon"></i>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                        <a href="Profile.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                                    </div>
                                    <div id="div2" runat="server" class="alert alert-danger alert-dismissible fade show" role="alert" visible="false">
                                        <i class="mdi mdi-block-helper label-icon"></i>
                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                        <i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group mb-0 text-lg-end">
                                    <asp:Button ID="btn" runat="server" CssClass="btn btn-primary submit-btn mt-2" Text="Update" OnClick="btn_Submit_Click" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

