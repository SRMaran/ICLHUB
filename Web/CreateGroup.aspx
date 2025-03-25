<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="Web_AddGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        window.onload = function () {
            var checkBoxList = document.getElementById('<%= CheckBoxList.ClientID %>');
            var labels = checkBoxList.getElementsByTagName('label');

            for (var i = 0; i < labels.length - 1; i++) {
                labels[i].insertAdjacentHTML('afterend', ' '); // Add space after each label
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <div class="login-header">
                    <h4 id="create" runat="server"></h4>
                </div>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li><a href="Group.aspx">Role Details</a>/</li>
                        <li id="grp" runat="server" class=" active"></li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card-box">
                <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-check-all label-icon"></i>
                    <asp:Label ID="lbl_success" runat="server"></asp:Label>
                    <a href="Group.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-block-helper label-icon"></i>
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i>
                </div>

                <div class="card-body p-4">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Role <span style="color: red">&nbsp;*</span></label>
                                <asp:DropDownList ID="ddlOptions" CssClass="form-select p-1" runat="server">
                                    <asp:ListItem Text="Select Role" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Admin" Value="0"></asp:ListItem>
                                     <asp:ListItem Text="ICL Admin" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="User" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="Rolevalidator" ForeColor="Red" runat="server" ControlToValidate="ddlOptions" InitialValue="" ErrorMessage="Please select a role." CssClass="error-message" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Permission <span style="color: red">&nbsp;*</span></label>
                                <asp:TextBox ID="txt_group" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="groupnamevalidator" ForeColor="Red" runat="server" ControlToValidate="txt_group" ErrorMessage="Permission is required." CssClass="error-message" />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox type="text" TextMode="MultiLine" runat="server" class="form-control" ID="des" placeholder="Enter Description" Style="height: 100px;" />
                                </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>PageName <span style="color: red">&nbsp;*</span></label>
                                <div class="form-group">
                                    <asp:CheckBoxList ID="CheckBoxList" AutoPostBack="false"
                                        RepeatColumns="4"
                                        CssClass="checkbox" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                        ErrorMessage="Please select at least one option from the list."
                                        ClientValidationFunction="validateCheckBoxList"
                                        ValidateEmptyText="true" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-end text-end">
                        <div class="col-sm-9">
                            <div>
                                <a href="Group.aspx" class="btn btn-primary"><i class=" bx bx-arrow-back" aria-hidden="true"></i>&nbsp;Back</a>
                                <asp:Button ID="Btn_Submit" runat="server" type="submit" class="btn btn-primary" OnClick="Btn_Submit_Click" Text="Submit"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function validateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById('<%= CheckBoxList.ClientID %>');
            var checkBoxes = checkBoxList.getElementsByTagName("input");
            var isChecked = false;

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type === "checkbox" && checkBoxes[i].checked) {
                    isChecked = true;
                    break;
                }
            }

            args.IsValid = isChecked;
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var checkBoxListItem = document.getElementById('<%= CheckBoxList.ClientID %>');
            if (checkBoxListItem) {
                var items = checkBoxListItem.getElementsByTagName("label");
                for (var i = 0; i < items.length; i++) {
                    var label = items[i];
                    label.style.marginRight = "10px";
                    label.style.marginLeft = "3px";
                }
            }
        };
    </script>
</asp:Content>

