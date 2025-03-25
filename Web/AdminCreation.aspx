<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AdminCreation.aspx.cs" Inherits="Web_AdminCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="row mb-3">
                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                    <div class="login-header">

                        <h4 id="create" runat="server"></h4>
                    </div>
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li><a href="AdminGrid.aspx">Admin Details</a>/</li>
                            <li class="breadcrumb-item active" id="Li1" runat="server">Create Admin</li>
                        </ol>
                    </div>
                </div>
            </div>
            <div class="card-box">
                <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-check-all label-icon"></i>
                    <asp:Label ID="lbl_success" runat="server"></asp:Label>
                    <a href="AdminGrid.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-block-helper label-icon"></i>
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i>
                </div>
                <div class="card-body p-4">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card-body p-4">
                                <div class="row">
                                    <div class="col-lg-12 ms-lg-auto">
                                        <div class="mt-4 mt-lg-0">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Username <span style="color: red">&nbsp;*</span></label>
                                                        <asp:TextBox ID="txt_username" runat="server" class="form-control" placeholder="Enter Username" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ControlToValidate="txt_username" ErrorMessage="Username is required." CssClass="error-message" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Style="color: red" ControlToValidate="txt_username" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Invalid Username" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Email <span style="color: red">&nbsp;*</span></label>
                                                        <asp:TextBox ID="txt_email" runat="server" class="form-control" placeholder="Enter Email Address" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txt_email" ErrorMessage="Email is required." CssClass="error-message" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_email" ErrorMessage="Invalid email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>


                                                <div class="col-md-4" id="password" runat="server" visible="true">
                                                    <div class="form-group">
                                                        <label>Password <span style="color: red">&nbsp;*</span></label>
                                                        <asp:TextBox ID="txt_password" runat="server" class="form-control" TextMode="Password" placeholder="Enter your password" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ControlToValidate="txt_password" ErrorMessage="Password is required." CssClass="error-message" />
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Contact No <span style="color: red">&nbsp;*</span></label>
                                                        <asp:TextBox ID="txt_phone" runat="server" class="form-control" placeholder="Enter your Contact No" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" runat="server" ControlToValidate="txt_phone" ErrorMessage="Contact no is required." CssClass="error-message" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txt_phone" ForeColor="Red" ErrorMessage="Invalid Phone Number"
                                                         ValidationExpression="^\d{1,15}$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Profile</label>
                                                        <asp:FileUpload class="form-control" ID="Fi_Updatepicture" runat="server" type="file" name="photo" accept="image/*" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4" id="rdstatus" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label>Status</label>
                                                        <asp:RadioButtonList ID="rd_status" runat="server" CssClass="mylist" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Text="Active" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Inactive"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row justify-content-end text-end">
                                                <div class="col-sm-9">
                                                    <div>
                                                        <a href="AdminGrid.aspx" class="btn btn-primary"><i class=" bx bx-arrow-back" aria-hidden="true"></i>&nbsp;Back</a>
                                                        <asp:Button ID="Update" runat="server" class="btn btn-primary" OnClick="Update_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</div>--%>
            </div>
        </div>
    </div>
    <%--    <script type="text/javascript">
        window.onload = function () {
            var checkBoxList = document.getElementById('<%= CList_Shipmenttype.ClientID %>');
            if (checkBoxList) {
                var items = checkBoxList.getElementsByTagName("label");
                for (var i = 0; i < items.length; i++) {
                    var label = items[i];
                    label.style.marginRight = "10px";
                    label.style.marginLeft = "10px";
                }
            }
        };
    </script>--%>
</asp:Content>

