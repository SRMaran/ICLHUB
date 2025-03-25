<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Newquote.aspx.cs" Inherits="Web_Newquote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
            <style>
        body {
            background-color: #e8f5fc;
        }
/* .blur-background {
    filter: blur(5px);
    pointer-events: none;
}*/
  .custom-alert {
    width: 420px;
    padding: 20px;
    border-radius: 12px;
    text-align: left;
    position: relative;
}

.custom-title {
    font-size: 15px;  /* Reduced size */
    font-weight: 500;
    font-family: transducer-extended, sans-serif;
    color: #112560;
    text-align: left;
    margin-bottom: 5px;
    margin-right:105px;
    letter-spacing: 0;
    line-height: 18px;
}

.custom-close {
    position: absolute;
    top: 15px;
    right: 15px;
    font-size: 18px;
    color: #7d7d7d;  /* Removed red color */
    border: none !important; /* Removes any outline */
    background: none !important; /* Ensures no extra styling */
    cursor: pointer;
}

.custom-divider {
    width: 100%;
    height: 1px;
    background-color: #ccc;
    margin: 10px 0;
}

.custom-message {
    text-align: center;
    font-size: 16px;
    color: #0056b3;
    margin-top: 10px;
}


    </style>

 <style>
     .placeholder-custom::placeholder {
    color: #f5f5f5; /* Change this to your desired color */
    opacity: 1; /* Ensures full opacity for placeholder text */
}

/* Fallback for older versions of IE */
.placeholder-custom:-ms-input-placeholder {
    color: #f5f5f5;
}

/* Fallback for Edge */
.placeholder-custom::-ms-input-placeholder {
    color: #f5f5f5;
}
 </style>
        <style>
        body {
            background-color: #e8f5fc;
        }
/* .blur-background {
    filter: blur(5px);
    pointer-events: none;
}*/
  .custom-alert {
    width: 420px;
    padding: 20px;
    border-radius: 12px;
    text-align: left;
    position: relative;
}

.custom-title {
    font-size: 15px;  /* Reduced size */
    font-weight: 500;
    font-family: transducer-extended, sans-serif;
    color: #112560;
    text-align: left;
    margin-bottom: 5px;
    margin-right:105px;
    letter-spacing: 0;
    line-height: 18px;
}

.custom-close {
    position: absolute;
    top: 15px;
    right: 15px;
    font-size: 18px;
    color: #7d7d7d;  /* Removed red color */
    border: none !important; /* Removes any outline */
    background: none !important; /* Ensures no extra styling */
    cursor: pointer;
}

.custom-divider {
    width: 100%;
    height: 1px;
    background-color: #ccc;
    margin: 10px 0;
}

.custom-message {
    text-align: center;
    font-size: 16px;
    color: #0056b3;
    margin-top: 10px;
}


    </style>

    <script>
        function allowZeroOnly(input) {
            input.value = input.value.replace(/[^0.]/g, ''); // Remove non-zero characters
            const errorMsg = document.getElementById("errorField6");

            //// Check if the value is anything other than "0"
            //if (input.value !== "0" && input.value !== "") {
            //    errorMsg.style.display = "inline"; // Show error message
            //    input.value = ""; // Clear invalid input
            //} else {
            //    errorMsg.style.display = "none"; // Hide error message if valid
            //}
        }
    </script>

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
        });

    }

    // Attach event listeners to textboxes
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
    function validateTextbox(textbox, errorId) {
        const errorSpan = document.getElementById(errorId);
        errorSpan.style.display = "none";

        textbox.addEventListener("blur", function () {
            if (textbox.value.trim() === "") {
                errorSpan.style.display = "inline";
            } else {
                errorSpan.style.display = "none";
            }
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <div class="row">
        <div class="col-md-6">
            <div class="card-box">
                <div class="row">
                    <div class="login-header">
                        <h4>Customer Details</h4>
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

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="validationDefault01">First Name</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox 
                                ID="txtFirstName"  CssClass="form-control" Placeholder="First Name" runat="server" Required="true"  onclick="validateTextbox(this, 'errorField8')">
                            </asp:TextBox>
                             <span id="errorField8" style="color: red; display: none;">please enter your firstname.</span>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="validationDefault02">Surname</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtSurname" CssClass="form-control" runat="server" Placeholder="Surname"  ReadOnly="false"  onclick="validateTextbox(this, 'errorField9')"></asp:TextBox>
                            <span id="errorField9" style="color: red; display: none;">please enter your surname.</span>
                        </div>
                    </div> 
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Company Name</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtcompanyname" CssClass="form-control" runat="server" Placeholder="Company Name"  onclick="validateTextbox(this, 'errorField10')"></asp:TextBox>
                          <span id="errorField10" style="color: red; display: none;">please enter your companyname.</span>
                        </div>
                    </div>
                              
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Email</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtEmail" CssClass="form-control"  runat="server" onclick="validateTextbox(this, 'errorField11')"></asp:TextBox>
                          <span id="errorField11" style="color: red; display: none;">please enter your Email.</span>

                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Contact Number</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtContactNumber" CssClass="form-control number-only"  runat="server" onclick="validateTextbox(this, 'errorField12')"></asp:TextBox>
                            <span id="errorField12" style="color: red; display: none;">please enter your contact number.</span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-box">
                <div class="row">
                    <div class="login-header">
                        <h4>Delivery Details</h4>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Transport Mode</label>
                            <span style="color: red;">*</span>
                            <asp:DropDownList ID="ddlTransportMode" runat="server" CssClass="form-select p-1">
                                <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                <asp:ListItem Text="Road" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Air" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Sea" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="rfvTransport"
                                runat="server"
                                ControlToValidate="ddlTransportMode"
                                InitialValue=""
                                ErrorMessage="Transport mode is required"
                                ForeColor="Red"
                                Display="Dynamic" />
                             
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Ready to Collect Date</label>
                                <asp:TextBox ID="txtStartingDate" type="date" class="form-control p-2 custom-style" runat="server"  placeholder="MM/DD/YYYY"></asp:TextBox>
                               <span id="errorField13" style="color: red; display: none;">Starting date is required.</span>
                            </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Target Delivery Date</label>
                                <asp:TextBox ID="txtEndingDate"  type="date" class="form-control p-2 custom-style" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                <span id="errorField114" style="color: red; display: none;">Ending date is required.</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-6">
            <div class="card-box">
                <div class="row">
                    <div class="login-header">
                        <h4>Goods Details</h4>
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

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>
                                Volume</label>
                            <span class="profile-views">cm</span>
                            <asp:TextBox ID="txtVolume" runat="server" type="Volume" class="form-control number-only" required="" placeholder="Volume"  
     onclick="validateTextbox(this, 'errorField1')" ></asp:TextBox>
                            <span id="errorField1" style="color: red; display: none;">please enter volume</span>
                                
                        </div>
                    </div>
                     
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Weight</label>
                            <asp:TextBox ID="txtWeight" CssClass="form-control number-only" runat="server" placeholder="Weight" onclick="validateTextbox(this, 'errorField2')" ></asp:TextBox>
                            <span class="profile-views">kg</span>
                             <span id="errorField2" style="color: red; display: none;">please enter weight.</span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                Width <span style="color: red;">*</span></label>
                            <asp:TextBox ID="txtWidth" runat="server" type="Width" class="form-control number-only" required="" placeholder="Width" onclick="validateTextbox(this, 'errorField3')"></asp:TextBox>
                            <span class="profile-views">cm</span>
                            <span id="errorField3" style="color: red; display: none;">please enter Width.</span>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Length</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtLength" CssClass="form-control number-only" runat="server" placeholder="Length" onclick="validateTextbox(this, 'errorField4')"></asp:TextBox>
                            <span class="profile-views">cm</span>
                             <span id="errorField4" style="color: red; display: none;">please enter Length.</span>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Height</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtHeight" CssClass="form-control number-only" runat="server" placeholder="Height" onclick="validateTextbox(this, 'errorField5')"></asp:TextBox>
                            <span class="profile-views">cm</span>
                             <span id="errorField5" style="color: red; display: none;">please enter Height.</span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Dimensions</label>
                            <span style="color: red;">*</span>
                            <asp:TextBox ID="txtDimensions" CssClass="form-control number-only" runat="server" placeholder="Dimensions"  oninput="allowZeroOnly(this)"></asp:TextBox>
                            <span class="profile-views">cm<sup>3</sup></span>
                             <span id="errorField6" style="color: red; display: none;">please enter Dimensions.</span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Package Type</label>
                            <asp:DropDownList ID="ddlPackageType" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                <asp:ListItem Text="Bag" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Bulk Bag" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Break Bulk" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Bale,Compressed" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Bale,Uncompressed" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Bundle" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Bottle" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Box" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Basket" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Case" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Coil" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Cradle" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Crate" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Carton" Value="15"></asp:ListItem>
                                <asp:ListItem Text="Cylinder" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Dozen" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Drum" Value="18"></asp:ListItem>
                                <asp:ListItem Text="Envelope" Value="19"></asp:ListItem>
                                <asp:ListItem Text="Gross" Value="20"></asp:ListItem>
                                <asp:ListItem Text="Keg" Value="21"></asp:ListItem>
                                <asp:ListItem Text="Mix" Value="22"></asp:ListItem>
                                <asp:ListItem Text="Pail" Value="23"></asp:ListItem>
                                <asp:ListItem Text="Piece" Value="24"></asp:ListItem>
                                <asp:ListItem Text="Package" Value="25"></asp:ListItem>
                                <asp:ListItem Text="Pallet" Value="26"></asp:ListItem>
                                <asp:ListItem Text="Reel" Value="27"></asp:ListItem>
                                <asp:ListItem Text="Roll" Value="28"></asp:ListItem>
                                <asp:ListItem Text="Sheet" Value="29"></asp:ListItem>
                                <asp:ListItem Text="Skid" Value="30"></asp:ListItem>
                                <asp:ListItem Text="Spool" Value="31"></asp:ListItem>
                                <asp:ListItem Text="Tote" Value="32"></asp:ListItem>
                                <asp:ListItem Text="Tube" Value="33"></asp:ListItem>
                                <asp:ListItem Text="Unit" Value="34"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                ID="rfvPackage"
                                runat="server"
                                ControlToValidate="ddlPackageType"
                                InitialValue="0"
                                ErrorMessage="Package type id required"
                                ForeColor="Red"
                                Display="Dynamic" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>QTY</label>
                            <asp:TextBox ID="txtQTY" CssClass="form-control number-only" runat="server" placeholder="QTY" onclick="validateTextbox(this, 'errorField7')"></asp:TextBox>
                             <span id="errorField7" style="color: red; display: none;">Qty is required.</span>
                        </div>
                    </div>
              
                  <div class="col-md-6"></div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label>Customs Clearance Requirements</label>
                        <asp:DropDownList ID="ddlCustomsClearanceRequirements" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                             <asp:ListItem Text="None" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Export" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Import" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Export and Import" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                       
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label>INCOTERMS</label>
                        <asp:DropDownList ID="ddlIncoterms" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="FAS(Free Alonside Ship)" Value="1"></asp:ListItem>
                            <asp:ListItem Text="EXW(Ex Works)" Value="2"></asp:ListItem>
                            <asp:ListItem Text="FCA(Seller)" Value="3"></asp:ListItem>
                            <asp:ListItem Text="FCA(Named Place)" Value="4"></asp:ListItem>
                            <asp:ListItem Text="FOB(Free On Board)" Value="5"></asp:ListItem>
                            <asp:ListItem Text="CFR(Cost And Frieght)" Value="6"></asp:ListItem>
                            <asp:ListItem Text="CPT(Carriage Paid To)" Value="7"></asp:ListItem>
                            <asp:ListItem Text="CIF(Cost,Insurance and Frieght)" Value="8"></asp:ListItem>
                            <asp:ListItem Text="CIP(Carrier and Insurance Paid)" Value="9"></asp:ListItem>
                            <asp:ListItem Text="DDP(Delivered Duty Paid)" Value="10"></asp:ListItem>
                            <asp:ListItem Text="DAP(Delivered at Place)" Value="11"></asp:ListItem>
                            <asp:ListItem Text="DPU(Delivered at Place Unloaded)" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                      
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label>Does this shipment contain hazardous goods?</label>
                        <div>

                            <asp:RadioButtonList ID="HazardousGoods" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Text="&nbsp&nbspYes&nbsp" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="&nbspNo"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label>Out of gauge?</label>

                        <asp:RadioButtonList ID="OutOfGauge" runat="server" CssClass="mylist" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Text="&nbsp&nbspYes&nbsp" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="&nbsp No"></asp:ListItem>
                        </asp:RadioButtonList>

                    </div>
                </div>
            </div>
      </div>

        <div class="card-box">
            <div class="row">
                <div class="login-header">
                    <h4>Other Details</h4>
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
            <div class="row">
                <div class="col-md-7">
                    <div class="form-group">
                        <label>Additional comments or special instructions</label>
                        <asp:TextBox ID="txtComment" runat="server" Rows="5" cols="5" CssClass="form-control" Placeholder="Enter your comments here..."></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label>Upload Supporting Files</label><br />
                        <asp:FileUpload ID="Fi_Updatepicture" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button ID="btnUpload" runat="server" Text="Submit Quote" CssClass="btn btn-primary submit-btn mt-2" OnClick="btnUpload_Click1" />
                    </div>
                </div>
            </div>
        </div>
              </div>
    </div>
    
</asp:Content>

