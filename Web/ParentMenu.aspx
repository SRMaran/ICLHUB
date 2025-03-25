<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ParentMenu.aspx.cs" Inherits="Web_ParentMenu" %>

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
    <div class="col-sm-12">
        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
            <div class="login-header"> <h4>Parent Menu Details</h4>
                  <p>Here’s where you keep up with all your parentmenu details</p>
            </div>
            <div class="page-title-right">
                <ol class="breadcrumb">
                    <li><a href="UserGrid.aspx">Dashboard</a></li>
                    <li class=" active">/ Parent Menu Details</li>
                </ol>
            </div>
        </div>
                <div class="row">
                    <div class="col-6">
                        <h4 class="page-title"></h4>
                    </div>
                    <div class="col-6 d-flex justify-content-end">
                        <div class="icons-list">
                            <a href="Addparentmenu.aspx" class="btn btn-primary waves-effect waves-light">Add New <i class="fas fa-plus"></i></a>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <table id="datatable-buttons" class="datatable table w-100 equal-width-table font-size-12 table-striped">
                        <thead>
                            <tr>
                                <th>Parent Menu Name</th>
                                <th>Menu Descripition</th>
                                <th>Createdon</th>
                                <th>Update</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="menu" runat="server"></asp:PlaceHolder>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script>
        function gridtabledelete(Submit, Menukey) {
            $.ajax({
                type: "POST",
                url: "ParentMenu.aspx/gridtabledelete",
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

