<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="submenu.aspx.cs" Inherits="Web_submenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>.table > :not(caption) > * > * {
    padding: .4rem .5rem; */
    background-color: var(--bs-table-bg);
    border-bottom-width: 1px;
    box-shadow: inset 0 0 0 9999px var(--bs-table-accent-bg);
}
.dataTables_filter {
    display: inline-block;
    margin-right: 10px;
    font-size: 12px; 
}

.dataTables_wrapper .dataTables_filter {
    display: inline-block;
    margin-right: 15px;
}
.dataTables_length label {
    display: flex;
    align-items: center;
    gap: 8px;
    margin-top:20px;
}

.dataTables_length select {
    width: auto;
}
     </style>
        <style>
    .dataTables_filter {
        position: relative;
    }

        .dataTables_filter input {
            padding-left: 30px;
            / Space for the icon / width: 250px;
            height: 36px;
            border-radius: 5px;
            border: 1px solid #ccc;
            outline: none;
        }

    .search-icon {
        position: absolute;
        left: 20px;
        top: 50%;
        transform: translateY(-50%);
        color: #1d64d6;
    }
</style>
    <script>
        function Submenudelete(Submit, Menukey) {
            $.ajax({
                type: "POST",
                url: "submenu.aspx/Submenudelete",
                data: "{str_ControlValue:'" + Menukey + "'}",
                /*  data: JSON.stringify({ palletData: contain.join(',') }),*/

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    OnSuccessCall(response);
                },
                error: function (response) {
                    OnErrorCall(response);
                }
            });

            function OnSuccessCall(response) {
                alert("Successfully Delete");
                window.location.reload(true);
            }

            function OnErrorCall(response) {
                // Handle error
                console.error("Error:", response);
            }
        }
       
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 ">
            <div class="buy-form-crypto mb-0">
                   
            

                <div class="card-box">
                     <div class="row mb-3">
     <div class="col d-flex align-items-center">
        <div class="login-header">
             <h4>Sub Menu Details</h4>
             <p>Here’s where you keep up with all your menu's</p>
         </div>
     </div>
 </div>
                    <div class="row">
                        <div class="col-6">
                            
                        </div>
                        <div class="col-6 d-flex justify-content-end">
                            <div class="icons-list">
                                <a href="submenuUpdate.aspx" class="btn btn-primary">Add New <i class="fas fa-plus"></i></a>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                         <table id="datatable-buttons" class="datatable table w-100 equal-width-table font-size-12 table-striped mb-0">
                     <thead>
                                <tr>
                                    <th>Menu Name</th>
                                    <th>Page Name</th>
                                    <th>ListOn</th>
                                    <th>Date</th>
                                    <th>Update</th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="PH_Submenu" runat="server"></asp:PlaceHolder>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

