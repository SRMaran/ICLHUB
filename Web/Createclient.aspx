<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Createclient.aspx.cs" Inherits="Web_Createclient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <div class="login-header">
                    <h4 id="headcreate" runat="server"></h4>
                </div>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li><a href="Client.aspx">Client Details</a>/</li>
                        <li id="create" runat="server"></li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="card-box">
        <div class="row">
            <div class="col-lg-12">
                <div class="card-header">

                    <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                        <i class="mdi mdi-check-all label-icon"></i>
                        <asp:Label ID="lbl_success" runat="server"></asp:Label>
                        <a href="Createclient.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                    </div>
                    <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                        <i class="mdi mdi-block-helper label-icon"></i>
                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                        <a href="Createclient.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                    </div>
                </div>
                <div class="row">
                         <div class="col-md-4">
         <div class="form-group">
             <label>
                 User Name<span style="color: red">&nbsp;*</span>
             </label>
             <asp:DropDownList ID="ddl_User" runat="server" CssClass="form-select p-1">
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" runat="server" ControlToValidate="ddl_User" InitialValue="0" ErrorMessage="Please select  organisation." CssClass="error-message" />
         </div>
     </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>
                                Organisation Name <span style="color: red">&nbsp;*</span>
                            </label>
                            <asp:TextBox ID="txt_orgname" runat="server" CssClass="form-control " placeholder="Enter organisation name" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ControlToValidate="txt_orgname" ErrorMessage="Organisation Name is required." CssClass="error-message" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>
                                Client Code <span style="color: red">&nbsp;*</span>
                            </label>
                            <asp:TextBox ID="txt_clientcode" runat="server" CssClass="form-control " placeholder="Enter Client code" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="txt_clientcode" ErrorMessage="Client Code is required." CssClass="error-message" />
                        </div>
                    </div>


                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Address</label>
                            <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Enter your Address" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">City</label>
                            <asp:TextBox ID="txt_city" runat="server" CssClass="form-control" placeholder="Enter your City" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Country</label>
                            <asp:TextBox ID="txt_country" runat="server" CssClass="form-control" placeholder="Enter your Country" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Postcode</label>
                            <asp:TextBox ID="txt_postcode" runat="server" CssClass="form-control" placeholder="Enter your Postcode" />
                              <asp:RequiredFieldValidator ID="vldNumber" ForeColor="Red" runat="server" ControlToValidate="txt_postcode" ErrorMessage="Postcode is required." CssClass="error-message" />

                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Billing Name</label>
                            <asp:TextBox ID="txt_billingname" runat="server" CssClass="form-control" placeholder="Enter  billingname" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Billing Address</label>
                            <asp:TextBox ID="txt_baddress" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Enter billing Address" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Billing City</label>
                            <asp:TextBox ID="txt_bcity" runat="server" CssClass="form-control" placeholder="Enter billing City" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Billing Country</label>
                            <asp:TextBox ID="txt_bcountry" runat="server" CssClass="form-control" placeholder="Enter billing Country" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="adrs">Billing Postcode</label>
                            <asp:TextBox ID="txt_bpostcode" runat="server" class="form-control" placeholder="Enter billing Postcode" />
                            <asp:RegularExpressionValidator ID='RegularExpressionValidator1' ControlToValidate='txt_bpostcode' Display='Dynamic' ErrorMessage=' Not a postcode' ForeColor="Red" ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)' runat='server'>
</asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-end">
                    <div class="col-auto">
                        <div>
                            <a href="Clientgrid.aspx" class="btn btn-primary equal-width">
                                <i class="bx bx-arrow-back" aria-hidden="true"></i>&nbsp;Back
                            </a>
                            <asp:Button ID="Btn_Submit" runat="server" type="submit" class="btn btn-primary equal-width" OnClick="Btn_Submit_Click" Text="Submit"></asp:Button>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>



</asp:Content>

