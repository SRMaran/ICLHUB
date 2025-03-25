<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="UserCreation.aspx.cs" Inherits="Web_UserCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        // Function to allow only numbers
        function validateNumbers() {
            const textboxes = document.querySelectorAll(".number-only"); // Select all textboxes with class 'number-only'
            let isValid = true;
            textboxes.forEach((textbox) => {
                const value = textbox.value;
                if (!/^\d*$/.test(value)) { // Regex for digits only
                    isValid = false;
                    textbox.value = value.replace(/\D/g, ''); // Remove non-numeric characters
                }
            });            ``
        }
        document.addEventListener("DOMContentLoaded", function () {
            const textboxes = document.querySelectorAll(".number-only");
            textboxes.forEach((textbox) => {
                textbox.addEventListener("input", validateNumbers); // Validate on input
                textbox.addEventListener("keypress", function (event) {
                    const charCode = event.which || event.keyCode;

                    // Allow only number keys
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        event.preventDefault();
                    }
                });
            });
        });
    </script>
    <script>
        function removeLabel(element, value) {
            console.log("Removing value:", value);
            const labelContainer = element.parentElement;
            fetch('UserCreation.aspx/RemoveSessionValue', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ value:value }) // Ensure correct JSON structure
            })
                .then(response => {
                    if (response.ok) {
                        labelContainer.remove(); // Remove the label
                        console.log('Value removed from session successfully.');
                    } else {
                        console.error('Failed to remove value from session.');
                    }
                })
                .catch(error => console.error('Error:', error));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="row">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                    <div class="login-header">
                        <h4 id="headcreate" runat="server"></h4>
                    </div>
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li><a href="UserGrid.aspx">User Details</a> / </li>
                            <li id="create" runat="server"></li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-box">
            <div class="card-header">

                <div id="div_success" runat="server" class="alert alert-success alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-check-all label-icon"></i>
                    <asp:Label ID="lbl_success" runat="server"></asp:Label>
                    <a href="UserCreation.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
                <div id="div_error" runat="server" class="alert alert-danger alert-dismissible alert-label-icon label-arrow fade show" role="alert" visible="false">
                    <i class="mdi mdi-block-helper label-icon"></i>
                    <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    <a href="UserCreation.aspx"><i class="btn-close" data-bs-dismiss="alert" aria-label="Close"></i></a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>UserName<span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="txt_username" runat="server" CssClass="form-control" Placeholder="Enter Username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_username" ErrorMessage="Enter the Username" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Email<span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="txt_email" runat="server" type="email" CssClass="form-control" Placeholder="Enter Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_email" ErrorMessage="Enter the Emailid" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexValidatorEmail" runat="server" ControlToValidate="txt_email" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Invalid email format." EnableClientScript="false" CssClass="text-danger"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Phone Number <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="txt_phone" runat="server" class="form-control" placeholder="Enter Phone Number" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_phone" ErrorMessage="Enter the Phonenumber" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txt_phone" ForeColor="Red" ErrorMessage="Invalid Phone Number"
                          ValidationExpression="^\d{1,15}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Address</label>
                        <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" Placeholder="Enter Address"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Postcode <span style="color: red">&nbsp;*</span></label>
                        <asp:TextBox ID="txt_postcode" runat="server" CssClass="form-control" Placeholder="Enter Postcode"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="vldNumber" ForeColor="Red" runat="server" ControlToValidate="txt_postcode" ErrorMessage="Postcode is required." CssClass="error-message" />
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
                 <div class="col-md-4">
     <div class="form-group">
         <label>User Role<span style="color: red">&nbsp;*</span></label>
         <div class="form-group">
             <asp:DropDownList ID="UserRole" runat="server" CssClass="form-select p-1" AutoPostBack="true" OnSelectedIndexChanged="CB_orgname_SelectedIndexChanged">
                 <asp:ListItem Text="--Select Role--" Value="0" Selected="false" Enabled="true"></asp:ListItem>
                 <asp:ListItem Text="ICL Admin" Value="2"></asp:ListItem>
                 <asp:ListItem Text="Client" Value="1"></asp:ListItem>
             </asp:DropDownList>
             <asp:Label ID="Clientvalue" runat="server" Visible="false"></asp:Label>
         </div>
         <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
     </div>
 </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Organisation Name <span style="color: red">&nbsp;*</span></label>
                        <div class="form-group">
                            <asp:DropDownList ID="lb_orgname" runat="server" CssClass="form-select p-1" AutoPostBack="true" OnSelectedIndexChanged="CB_orgname_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lb_organization" runat="server" Visible="false"></asp:Label>

                        </div>
                        <asp:PlaceHolder ID="ph_orgname" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
               
            <div class="col-md-4">
    <div class="form-group">
        <label>Password<span style="color: red">&nbsp;*</span></label>
        <asp:TextBox ID="txt_password" runat="server" Type="password"  CssClass="form-control" Placeholder="Enter Password"></asp:TextBox>
    </div>
</div>
            
            </div>
            <div class="row justify-content-end">
                <div class="col-auto">
                    <a href="UserGrid.aspx" class="btn btn-primary equal-width" runat="server" id="back" visible="false">Back</a>
                    <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary equal-width" Visible="false" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary equal-width" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

