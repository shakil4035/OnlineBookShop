$(document).ready(function () {

    //var urlVar = getUrlVars();

    //var id = urlVar["id"];

    //if (id > 0) {
    //    getData(id);
    //}
    loadHistoryTable();
});

$(document.body).on("click",
    "#btnSubmit",
    function () {
        var vm = {};
        var id = $("#Id").val();
        vm.name = $("#Name").val();
        vm.gender = $("#Gender").val();
        vm.country = $("#Country").val();
        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Authors",
                data: vm,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        refressForm();
                        loadHistoryTable();

                    } else {
                        toastr.warning("Save Fail", "Warning!!!");
                    }
                },
                error: function (request, status, error) {
                    var response = jQuery.parseJSON(request.responseText);
                    toastr.error(response.message, "Error");
                }
            });
        } else {
            vm.id = id;

            $.ajax({
                url: "/api/Authors/" + id,
                data: vm,
                type: "PUT",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Update Success", "Success!!!");
                        refressForm();
                        loadHistoryTable();
                    } else {
                        toastr.warning("Update Fail.", "Warning!!!");
                    }
                },
                error: function (request, status, error) {
                    var response = jQuery.parseJSON(request.responseText);
                    toastr.error(response.message, "Error");
                }
            });

        }
    });

function refressForm() {
    $("#Id").val('');
    $("#Name").val('');
    $("#Gender").val('');
    $("#Country").val('');
}

$(document.body).on("click",
    "#btnRefresh",
    function () {
        refressForm();
    });

function loadHistoryTable() {

    $("#AuthorHistory").DataTable().destroy();

    $("#AuthorHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Authors",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "gender"
            },
            {
                data: "country"
            },

            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' data-id=" + data + " ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
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
                    url: "/api/Authors/" + id,
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
    $.get("/api/Authors/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Gender").val(data.gender);
            $("#Country").val(data.country);
        });
}