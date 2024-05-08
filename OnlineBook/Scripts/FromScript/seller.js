$(document).ready(function () {
    var urlVar = getUrlVars();
    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }

   /*  loadHistoryTable();*/
    GenarateSellerlNumber();

    $("#textFind").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/api/Selers/Search",
                data: { query: request.term },
                type: "GET"
            }).done(function (data) {
                response($.map(data, function (item) {
                    return { label: item.idNo + " " + item.name, value: item.idNo }
                }));
            });
        },
        minLength: 2,
        select: function (e, ui) {

            $.get("/api/Selers/SearchAutoComplete", { pNumber: ui.item.value })
                .done(function (data) {
                    //console.log(pData);
                    // window.pId = data.id;
                    $("#Id").val(data.id);
                    $("#SearchName").val(data.name);
                    $("#searchPhoneNo").val(data.phoneNo);

                    loadHistoryTable(data.id);

                });

        }
    });

});
function GenarateSellerlNumber() {

    $.get("/api/Selers/CreateSellerId",
        function (data) {
            if (data !== null) {
                $("#IdNo").val(data);
            }
        });
}
$(document.body).on("click",
    "#btnSubmit",
    function () {
        var vm = {};
        var id = $("#Id").val();
        vm.idNo = $("#IdNo").val();
        vm.name = $("#Name").val();
        vm.gender = $("#Gender").val();
        vm.joinjngDate = $("#JoinjngDate").val();
        vm.birithDate = $("#BirithDate").val();
        vm.meritialStatus = $("#MeritialStatus").val();
        vm.email = $("#Email").val();
        vm.phoneNo = $("#PhoneNo").val();
        vm.address = $("#Address").val();

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Selers",
                data: vm,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        refressForm();
                        loadHistoryTable(e);
                        GenarateSellerlNumber();

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
                url: "/api/Selers/" + id,
                data: vm,
                type: "PUT",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Update Success", "Success!!!");
                        refressForm();
                        loadHistoryTable(e);
                        GenarateSellerlNumber();

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

$(document.body).on("click",
    "#btnRefresh",
    function () {
        refressForm();
    });


function refressForm() {
    $("#Name").val("");
    $("#Gender").val("");
    $("#JoinjngDate").val("");
    $("#BirithDate").val("");
    $("#MeritialStatus").val("");
    $("#Email").val("");
    $("#PhoneNo").val("");
    $("#Address").val("");
    $("#SearchName").val("");
    $("#searchPhoneNo").val("");
    $("#textFind").val("");
    
}
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
    refressForm();
});

function loadHistoryTable(id) {

    $("#sellerList").DataTable().destroy();

    $("#sellerList").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Selers/GetInfoById?id="+id,
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
