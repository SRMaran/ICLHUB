<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="UserGrid.aspx.cs" Inherits="Web_UserGrid" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="card-box">
         <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <div class="login-header">
                 <h4>Create User</h4>
                   <p>Here’s where you keep up with all your user</p>
             </div>
         </div>
     </div>
 </div>
        <div class="row">
            <div class="col-12">
                <div class="card-header">
                    <div class="row">
                        <div class="col-6">
                            <h4 class="card-title"></h4>
                        </div>
                        <div class="col-6 text-end">

                            <a href="UserCreation.aspx" class="btn btn-primary waves-effect waves-light">Create  <i class="fas fa-plus"></i></a>

                        </div>
                    </div>
                </div>
                     <div class="table-responsive">
                    <table id="datatable-buttons"  class="datatable table  table-striped mb-0 w-100">
                        <thead>
                            <tr>
                                <th>User Name</th>
                                <th>Organisation</th>
                                <th>Email</th>
                                <th>Phone Number</th>
                                <th>Address</th>
                                <th>Status</th>
                                <th>Created By</th>
                                <th>Created Date</th>
                                <th>Update</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="Usergrid" runat="server"></asp:PlaceHolder>
                        </tbody>

                    </table>
                </div>
                </div>
            </div>


        </div>
    </div>
    
</asp:Content>

