$(document).ready(function () {
    loadHistoryTable();
});

function loadHistoryTable() {
    $("#SellerTable").DataTable().destroy();

    $("#SellerTable").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Selers",
            dataSrc: ""
        },
        columns: [
            {
                data: "idNo"
            },
            {
                data: "name"
            },
            {
                data: "gender"
            },
            {
                data: "phoneNo"
            },
            

            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' href='/Sellers/SellerEntry?id=" + data + "' ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
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
                    url: "/api/Selers/" + id,
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

function getData(id) {
    $.get("/api/Selers/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#IdNo").val(data.idNo);
            $("#Name").val(data.name);
            $("#Gender").val(data.gender);
            $("#JoinjngDate").val(data.joinjngDate);
            $("#BirithDate").val(data.birithDate);
            $("#MeritialStatus").val(data.meritialStatus);
            $("#Email").val(data.email);
            $("#PhoneNo").val(data.phoneNo);
            $("#Address").val(data.address);
            $("#SearchName").val(data.name);
            $("#searchPhoneNo").val(data.phoneNo);
        });
}

