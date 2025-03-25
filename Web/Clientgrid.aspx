<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Clientgrid.aspx.cs" Inherits="Web_Clientgrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <style>.table > :not(caption) > * > * {
    padding: .4rem .5rem; */
    background-color: var(--bs-table-bg);
    border-bottom-width: 1px;
    box-shadow: inset 0 0 0 9999px var(--bs-table-accent-bg);
}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <div class="card-box">
         <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <div class="login-header">
                 <h4>Client Details</h4>
                   <p>Here’s where you keep up with all your client.</p>
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
                                <a href="createclient.aspx" class="btn btn-primary waves-effect waves-light">Create  <i class="fas fa-plus"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <table id="datatable-buttons" class="datatable table w-100   equal-width-table font-size-12 table-striped mb-0">
                        <thead>
                            <tr>
                                <th>Organization Name</th>
                                <th>Client Code</th>
                                <th>User</th>
                                <th>Address</th>
                                <th>Billing Name</th>
                                <th>Update</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="Client" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- end cardaa -->
        </div>
        <!-- end col -->
    </div>
</asp:Content>

