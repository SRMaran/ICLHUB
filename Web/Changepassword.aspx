<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Changepassword.aspx.cs" Inherits="Web_Changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div class="account-page">
                <div class="account-center">

                    <div class="account-box">
                        <div class="form-group">
                        <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" visible="false">
                            <i class="mdi mdi-check-all label-icon"></i>
                            <asp:Label ID="lbl_success" runat="server"></asp:Label>
                            <a href="../login.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                        </div></div>
                        <div class="login-header">
                            <h3 class="account-title text-center" >Change Password</h3>
                        </div>
                        <br />
                        <div class="form-group">
                            <asp:TextBox ID="txt_oldpassword" runat="server"  required="" class="form-control" placeholder="Old Password *"></asp:TextBox>
                            <asp:Label ID="lbl_error" runat="server" ForeColor="Red" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Newpassword" runat="server" required="" class="form-control" placeholder="New Password *"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Repassword" runat="server" required="" class="form-control" placeholder="Confirm Password *"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server"
                                ControlToValidate="txt_Repassword"
                                CssClass="ValidationError"
                                ControlToCompare="txt_Newpassword"
                                ErrorMessage="Password must be the same"
                                ForeColor="Red" />
                        </div>
                        <div class="form-group mb-0 text-center">
                            <asp:Button ID="btn_submit" runat="server" class="btn btn-primary btn-block account-btn" Text="Submit" OnClick="btn_submit_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



