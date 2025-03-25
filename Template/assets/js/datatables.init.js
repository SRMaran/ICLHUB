$(document).ready(function () {
    var table = $("#datatable-buttons").DataTable({
        dom: 'lBfrtip',
        scrollX: true,
        scrollY: true,
        responsive: false,
        colReorder: true,
        stateSave: false,
        lengthMenu: [
            [10, 25, 50, -1],
            ['10', '25', '50', '100']
        ],
        buttons: [
            {
                extend: "copy",
                text: "Copy",
                exportOptions: { columns: ":visible:not(:first-child)" },
            },
            {
                extend: "excel",
                text: "Excel",
                exportOptions: { columns: ":visible:not(:first-child)" },
            },
            {
                extend: "pdfHtml5",
                text: "PDF",
                orientation: "landscape",
                pageSize: "A4",
                title: document.title,
                exportOptions: { columns: ":visible:not(:first-child)" },
            },
            {
                extend: "colvis",
                text: "Customise Columns",
                collectionLayout: 'fixed columns',
                columns: ":gt(0)",
                popoverTitle: `Customise your table columns <br>Highlight Column Headers in Blue to View.
        <p id="close-popover" style="cursor:pointer; color:#1d64d6; float:right; margin-top:-20px; margin-right: 10px;  font-size: 20px;">X</p>`,
                exportOptions: { columns: ":visible:not(:first-child)" }
            }
        ],
        language: {
            lengthMenu: "Show _MENU_ entries",
            search: "",
            searchPlaceholder: "Search...",
            info: "Showing _START_ to _END_ of _TOTAL_ entries"
        }
    });
    $('.dataTables_filter label').addClass('position-relative').append('<i class="fas fa-search search-icon"></i>');

    // Move "Entries per page" dropdown to the empty div
    $(".dataTables_length").appendTo(".row .col-md-2:first");
    $(".position-relative").appendTo(".row .col-md-2:last");
    $(".position-relative").appendTo(".row .col-lg-4:last");

    // Ensure dropdown styling matches existing layout
    $(".dataTables_length select").addClass("form-select form-select-sm");
    table.buttons().container().on('click', '#close-popover', function () {
        $('.dt-button-collection').hide(); // Hides the popover
    });
});
