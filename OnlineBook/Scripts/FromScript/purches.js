﻿$(document).ready(function () {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    //Get Designations
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

    $(document.body).on("keypress",
        "#name",
        function () {
            $("#name").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/api/Books/Search",
                        data: { query: request.term },
                        type: "GET"
                    }).done(function (data) {
                        response($.map(data,
                            function (item) {
                                return { label: item.bookTitel, value: item.id };
                            }));
                    });
                },
                minLength: 1,
                select: function (e, ui) {
                    $.get("/api/Books/SearchAutoCompleteid", { bid: ui.item.value })
                        .done(function (data) {
                            if (data !== null) {
                                $("#BookId").val(data.id);
                                $("#name").val(data.bookTitel);

                            }
                        });
                }
            });
        });

});

$(document.body).on("change", "#price,#quantity,#Discount", function () {
    var price = $("#price").val();
    var quantity = $("#quantity").val();
    var lineTotal = price * quantity;
    $("#lineTotal").val(lineTotal);
});

function refressForm() {
    $("#Id").val('');
    $("#BookId").val('');
    $("#name").val('');
    $("#price").val('');
    $("#quantity").val('');
    $("#lineTotal").val('');
    $("#unit").val('');
    $("#Date").val('');
    $("#AuthorId").val('');
    $("#CustomerName").val('');
}



/*Add Item Table*/
var sl = 1;
$(document.body).on("click", "#btnAdd", function () {
    var BookId = $("#BookId").val();
    var BookName = $("#name").val();
    var price = $("#price").val();
    var quantity = $("#quantity").val();
    var lineTotal = $("#lineTotal").val();
    var unit = $("#unit").val();

    if (BookName == "") {
        toastr.warning("please give the required fields");
        return;
    }
    function refressForms() {
        $("#Id").val('');
        $("#BookId").val('');
        $("#name").val('');
        $("#price").val('');
        $("#quantity").val('');
        $("#lineTotal").val('');
        $("#unit").val('');
    }
    refressForms();
    var dtlTable = $('#itemTable').DataTable();
    var dtls = dtlTable.rows().data();


    $("#itemTable").DataTable().destroy();
    var t = $("#itemTable").DataTable({
        retrieve: false,
        paging: false,
        searching: false,
        info: false,
        order: [[2, 'desc']],
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]
    });

    t.row.add([
        BookId,
        BookName,
        unit,
        price,
        quantity,
        lineTotal,
        /*"<a class='btn btn-danger btn-sm js-item-delete' data-id=" + BookId + " ><i class='fa fa-trash fa-x ' aria-hidden='false'></i></a>"*/
    ]).draw(false);
    sl++;
    //clearAddItemTable();
    $("#BookId").val("").trigger('change');


    var total = 0;
    $("#itemTable tbody tr").each(function () {
        var lineTotal = parseFloat($(this).find('td:eq(4)').text());
        if (!isNaN(lineTotal)) {
            total += lineTotal;
        }
    });
    $("#TotalBill").val(total);
    var payable = total;
    $("#Payable").val(payable);
});

$(document.body).on("change", "#DiscountPercentage", function () {
    var total = parseFloat($("#TotalBill").val()) ;
    var discountPercentage = parseFloat($("#DiscountPercentage").val());
    if (discountPercentage == "" || discountPercentage == null ||isNaN(discountPercentage)) {
        $("#DiscountPercentage").val(0);
        discountPercentage = 0;
    }
    /*if condition same kaj kore */
    /*var discountPercentage = parseFloat($("#DiscountPercentage").val().length > 0 ? parseFloat($("#DiscountPercentage").val()) : 0);*/
    var discount = (total / 100) * discountPercentage;
    /*var newTotal = total - discount;*/
    var payable = total - discount;
    
    $("#Payable").val(payable);
    $("#Discount").val(discount);
    /*$("#TotalBill").val(newTotal);*/
});

$(document.body).on("change", "#TaxPercentage", function () {
    var total = parseFloat($("#TotalBill").val()); 
    var taxPercentage = parseFloat($("#TaxPercentage").val());
    if (taxPercentage == "" || taxPercentage == null || isNaN(taxPercentage)) {
        $("#TaxPercentage").val(0);
        taxPercentage = 0;
    }
    var tax = (total / 100) * taxPercentage;
    var discount = $("#Discount").val();
    /*var newTotal = total - discount;*/
    var payable = (total - discount)+tax;
    $("#Payable").val(payable);
    $("#Tax").val(tax);
});
//function clearAddItemTable() {
//    $("#Id").val("");
//    $("#quantity").val("");
//    $("#unitName").val("");
//    $("#name").val("");0
//    $("#remark").val("");
//    $("#StockQnt").val("");
////}
$(document.body).on("click", "#btnClear", function () {
    $('#itemTable').DataTable().clear().draw();
    $('#TotalBill').val('');
    $('#Payable').val('');
    $('#DiscountPercentage').val('');
    $('#Discount').val('');
    $('#TaxPercentage').val('');
    $('#Tax').val('');
    refressForm();
});


//$('#itemTable tbody').on('click', '.js-item-delete', function () {
//    $('#itemTable').DataTable()
//        .row($(this).parents('tr'))
//        .remove()
//        .draw();
//});

$(document.body).on("click", "#btnSubmit", function () {

    var dto = {
        PurchesDetail: []
    };
    var id = $("#Id").val();
    dto.customerName = $("#CustomerName").val();
    dto.date = $("#Date").val();
    dto.authorId = $("#AuthorId").val();
    dto.totalBill = $('#TotalBill').val();
    dto.payable = $('#Payable').val();
    dto.discountPercentage = $('#DiscountPercentage').val();
    dto.discount = $('#Discount').val();
    dto.taxPercentage = $('#TaxPercentage').val();
    dto.tax = $('#Tax').val();

    var dtlTable = $('#itemTable').DataTable();
    var dtls = dtlTable.rows().data();


    for (var r = 0; r < dtls.length; r++) {
        dto.PurchesDetail.push({
            ItemId: dtls.cell(r, 0).data(),
            ItemName: dtls.cell(r, 1).data(),
            Unit: dtls.cell(r, 2).data(),
            Price: dtls.cell(r, 3).data(),
            Quantity: dtls.cell(r, 4).data(),
            lineTotal: dtls.cell(r, 5).data()

        });
    }

    if (id == "" || id == 0 || id == null) {
        $.ajax({
            url: "/api/Purches",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Data Save Success", "Success!!!");
                    refressForm();


                } else {
                    toastr.warning("Data Save Fail.", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.errorMassage, "Error");
            }
        });
    } else {
        dto.id = id;

        $.ajax({
            url: "/api/Purches/" + id,
            data: dto,
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Data Update Success", "Success!!!");
                    refreshForm();

                } else {
                    toastr.warning("Data Update Fail.", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.errorMassage, "Error");
            }
        });
    }
});

function getData(id) {
    $.get("/api/Purches/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#CustomerName").val(data.customerName);
            $("#AuthorId").val(data.authorId);
            $("#Date").val(data.date);
        });
}



