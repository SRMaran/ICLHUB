<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Organisationgrid.aspx.cs" Inherits="Web_Organisationgrid" %>

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
     <script type="text/javascript">
         function fn_DeleteOrg(str_Del_key) {
             if (confirm("Are you sure,you want to delete this?")) {
                 $.ajax({
                     type: "POST",
                     url: "Organisationgrid.aspx/orgdelete",
                     data: "{ str_delkey: '" + str_Del_key + "'}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     async: "true",
                     cache: "false",

                     success: function (data, status) {
                         // On success
                         var response = ["success", data];
                         var ResponseData = response[1].d;
                         var ResponseStatus = ResponseData.split("&&&")[0];
                         if (ResponseStatus == "1") {
                             alert(" This organization has been deleted ");
                             location.reload();
                             return;

                             return;
                         }
                         else {
                             alert("Sorry, unable to delete this, please try after sometime.");
                             HideLoadingScreen();
                             return;
                         }
                     },
                     error: function (xhr, status, error) {
                         alert("Sorry, unable to delete this, please try after sometime.");
                         HideLoadingScreen();
                         return;
                     }
                 });
             }
             else {
                 HideLoadingScreen();
                 return;
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <div class="card-box">
         <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <div class="login-header">
                 <h4>Organisation Details</h4>
                   <p>Here’s where you keep up with all your organisation .</p>
             </div>
            
         </div>
     </div>
 </div>
        <div class="row">
            <div class="col-12">
                <div class="card-header">
                    <div class="row">
                        <div class="col-6">
                        </div>
                        <div class="col-6 d-flex justify-content-end">
                            <div class="icons-list">
                                <a href="CreateOrganisation.aspx" class="btn btn-primary waves-effect waves-light">Create  <i class="fas fa-plus"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <table id="datatable-buttons" class="datatable table w-100   equal-width-table font-size-12 table-striped mb-0">
                        <thead>
                            <tr>
                                <th>Organisation Name</th>
                                <th>Client Code</th>
                                <th>Account Manager</th>
                                <th>Address</th>
                                <th>Billing Name</th>
                                <th>Update</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="PH_Organisation" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- end cardaa -->
        </div>
        <!-- end col -->
    </div>
                
</asp:Content>

