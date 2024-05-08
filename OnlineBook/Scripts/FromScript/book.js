$(document).ready(function () {

    loadHistoryTable();

    $.get("/api/Authors", function (data) {
        var $el = $("#AuthorId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>',
                {
                    value: key.id,
                    text: key.name
                }));
        });
    });

    $.get("/api/Categorys", function (data) {
        var $el = $("#CategoryId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>',
                {
                    value: key.id,
                    text: key.name
                }));
        });
    });
});

$(document.body).on("click",
    "#btnSubmit",
    function () {
        var vm = {};
        var id = $("#Id").val();
        vm.bookTitel = $("#BookTitel").val();
        vm.authorId = $("#AuthorId").val();
        vm.CategoryId = $("#CategoryId").val();
        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Books",
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
                url: "/api/Books/" + id,
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
    $("#BookTitel").val('');
    $("#AuthorId").val('');
    $("#CategoryId").val('');
}

$(document.body).on("click",
    "#btnRefresh",
    function () {
        refressForm();
    });


function loadHistoryTable() {

    $("#BooksHistory").DataTable().destroy();

    $("#BooksHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Books",
            dataSrc: ""
        },
        columns: [
            {
                data: "bookTitel"
            },
            {
                data: "author.name"
            },
            {
                data: "category.name"
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
                    url: "/api/Books/" + id,
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
    $.get("/api/Books/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#BookTitel").val(data.bookTitel);
            $("#AuthorId").val(data.authorId);
            $("#CategoryId").val(data.categoryId);
        });
}