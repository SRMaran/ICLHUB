<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Addparentmenu.aspx.cs" Inherits="Web_Addparentmenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">


            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <div class="login-header">
                    <h4>Parent Menu</h4>
                </div>
                <h4></h4>
                <div class="page-title-right">
                    <ol class="breadcrumb">
                        <li><a href="parentmenu.aspx">Parent Menu Details</a> /</li>
                        <li class=" active" runat="server" id="create"></li>
                    </ol>
                </div>
            </div>
            <div class="card-box">
                <div class="card-header">
                    <h5 class="page-title font-size-14 mb-2" id="createhead" runat="server"><i class="mdi mdi-arrow-right text-primary me-1"></i></h5>
                </div>
                <div class="card-body p-4">
                    <div class="row">
                        <div class="col-lg-6">
                             <div class="form-group">
                                <label class="form-label" for="formrow-menuname-input">Parent Menu Name <span style="color: red">&nbsp;*</span></label>
                                <asp:TextBox runat="server" type="text" class="form-control" ID="parentmenuname" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="parentmenuname" ErrorMessage="Enter parentname" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="formrow-description-input">Menu Description</label>
                                <asp:TextBox runat="server" type="text" class="form-control" ID="Description" placeholder="Enter the Description" TextMode="MultiLine"/>
                            </div>
                        </div>
                        <div class="col-lg-6">
                             <div class="form-group">
                                <label class="form-label" for="formrow-listname-input">Menu List No <span style="color: red">&nbsp;*</span></label>
                                <asp:TextBox runat="server" type="text" class="form-control" ID="Menulist" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Menulist" ErrorMessage="Enter menulist" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group">
                                <label class="form-label" for="formrow-menuicon-input">Menu icon <span style="color: red">&nbsp;*</span> &nbsp;&nbsp;<a href="../web/icon.aspx" target="_blank" id="copyButton" class="primary"><i class="fas fa-copy"></i>Copy icon</a></label>
                                <asp:TextBox runat="server" type="text" class="form-control" ID="Menuicon" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Menuicon" ErrorMessage="Enter menuicon" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="text-end">
                                <!-- Back button -->
                                <a href="../web/ParentMenu.aspx" class="btn btn-primary mt-2"><i class="bx bx-arrow-back" aria-hidden="true"></i>&nbsp;Back</a>
                                <!-- Submit button -->
                                <asp:LinkButton ID="submit" runat="server" type="submit" class="btn btn-primary mt-2" OnClick="submit_Click"><i class='bx bxs-navigation'></i> Submit</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

