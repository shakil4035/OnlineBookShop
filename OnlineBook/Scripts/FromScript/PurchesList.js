$(document).ready(function () {
    loadHistoryTable();
});

function loadHistoryTable() {
    $("#purchesTable").DataTable().destroy();

    $("#purchesTable").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Purches",
            dataSrc: ""
        },
        columns: [
            {
                data: "customerName"
            },
            {
                data: "author.name"
            },
            {
                data: "date"
            },
       
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' href='/Purches/New?id=" + data + "' ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
                }
            },

            {
                data: "id",
                render: function (data) {
                    return "<a class='btn-link js-delete'  data-id=" + data + "><i class='fa fa-trash fa-2x' aria-hidden='true' style='color: #d9534f;'></i></a>";
                }
            }
        ]
    });
}

$(document.body).on("click",
    ".js-edit",
    function () {
        refressForm();
        var button = $(this);
        var id = button.attr("data-id");
        getData(id);

    });


$(document.body).on("click", ".js-delete", function () {
    var button = $(this);
    var id = button.attr("data-id");
    bootbox.confirm("Are You Delete This Data?",
        function (result) {
            if (result) {
                $.ajax({
                    url: "/api/Purches/" + id,
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                        toastr.success("Delete Success");
                    },
                    error: function (request, status, error) {
                        var response = jQuery.parseJSON(request.responseText);
                        toastr.error(response.message, "Error");
                    }
                });
            }
        });
});



