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
        vm.description = $("#Description").val();
        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Categorys",
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
                url: "/api/Categorys/" + id,
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
    $("#Description").val('');
}

$(document.body).on("click",
    "#btnRefresh",
    function () {
        refressForm();
    });

function loadHistoryTable() {

    $("#CategoryHistory").DataTable().destroy();

    $("#CategoryHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Categorys",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "description"
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
                    url: "/api/Categorys/" + id,
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
    refressForm();
});

function getData(id) {
    $.get("/api/Categorys/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Description").val(data.description);
        });
}