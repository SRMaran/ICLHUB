<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="submenuUpdate.aspx.cs" Inherits="Web_submenuUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="card-box">
        <div class="row">
    <div class="col-12">
        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
            <div class="login-header">
                <h4>Sub Menu</h4>
                <p>Here’s where you keep up with update and create your Sub Menu's</p>
            </div>
            <div class="page-title-right">
                <ol class="breadcrumb m-0">
                    <li><a href="submenu.aspx">Sub Menu Details </a>/</li>

                    <li class="active" id="sub" runat="server"></li>
                </ol>
            </div>
        </div>
    </div>
</div>


        <div class="card-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label class="form-label" for="formrow-firstname-input">Menu Name <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="menuname" CssClass="form-control" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="menuname" ErrorMessage="Enter the menuname" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                     <div class="form-group">
                        <label class="form-label" for="formrow-email-input">Page Name <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="Pagename" CssClass="form-control" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Pagename" ErrorMessage="Enter the pagename" ForeColor="Red"></asp:RequiredFieldValidator>
                         </div>
                      <div class="form-group">
                        <label class="form-label" for="formrow-firstname-input">Menu List No <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="Menulist" CssClass="form-control" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Menulist" ErrorMessage="Enter the menulist" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-lg-6">
                      <div class="form-group">
                        <label class="form-label" for="formrow-firstname-input">Parent Menu Name</label>
                        <asp:DropDownList ID="parentmenuname" CssClass="form-select p-1" runat="server"></asp:DropDownList>
                          <br />
                    </div>
                      <div class="form-group">
                        <label class="form-label" for="formrow-firstname-input">Folder Name <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="foldername" CssClass="form-control" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="foldername" ErrorMessage="Enter the foldername" ForeColor="Red"></asp:RequiredFieldValidator>
                  
                    </div>
                     <div class="form-group">
                        <label class="form-label" for="formrow-firstname-input">Menu icon &nbsp;<a href="../web/icon.aspx" id="copyButton" target="_blank" class="mb-3"><i class="fas fa-copy"></i> Copy icon</a></label>
                        <asp:TextBox ID="Menuicon" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="text-end">
                        <a href="submenu.aspx" class="btn btn-primary"><i class="bx bx-arrow-back" aria-hidden="true"></i>&nbsp;Back</a>
                        <asp:LinkButton ID="submit" runat="server" class="btn btn-primary" OnClick="submit_Click">
                            <asp:Label runat="server" ID="ID_submit"></asp:Label></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

