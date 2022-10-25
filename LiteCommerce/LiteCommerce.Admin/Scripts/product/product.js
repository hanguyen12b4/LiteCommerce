$(document).ready(function () {

    var $formProduct = $('#formProduct');
    if (window.location.pathname.includes("/Products/Edit")) {
        renderCategories($('#CategoryID').attr('value'));
        renderSupplierByCateID(0, $('#SupplierID').attr('value'));
    }else if( window.location.pathname.includes("/Products/AddProduct")){
        var d = false;
        renderCategories($('#CategoryID').attr('value'),d);
        renderSupplierByCateID(0, $('#SupplierID').attr('value'),d);
    }else {
        getProducts(1);
        renderCategories();
        renderSupplierByCateID(0);
        $("#formSearchInput").submit(function (e) {
            e.preventDefault();
            getProducts(1);
        });
        //show supplier
    }
    $('#CategoryID').on('change', function () {
        renderSupplierByCateID(document.getElementById('CategoryID').value);
    });

    $('.delete-btn').on('click', function () {
        console.log($('#ProductID').attr("Value"))
        $.ajax({
            url: "/Products/Delete?id=" + $('#ProductID').val(),
            type: 'POST',
            success: function (responseData) {
                if (responseData == true) {
                    var urlStr = window.location.host;
                    window.location = "http://" + urlStr + "/Products/Index";
                }
                
            }
        });
    });

    $('.saveproduct-btn').on('click', function() {
        var formData = new FormData($formProduct[0]);
        $formProduct.validate({
            ignore: [],
            rules: {
                ProductName: {
                    required: true,
                    maxlength: 100,
                },
                Unit: {
                    required: true,
                },
                Price: {
                    required: true,
                },

            },
            messages: {
                ProductName: {
                    required: "Vui lòng nhập tên nhà cung cấp",
                    maxlength: "Tên sản phẩm không quá 100 kí tự"
                },
                Unit: {
                    required: "Vui lòng nhập đơn vị "
                },
                Price: {
                    required: "Vui lòng nhập giá "
                },

            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });

        if ($formProduct.valid()) {
            $.ajax({
                url: "/Products/SaveProduct",
                type: 'POST',
                enctype: 'multipart/form-data',
                processData: false,
                contentType: false,
                cache: false,
                timeout: 10000,
                data: formData,
                success: function (responseData) {
                    if (responseData == true) {
                        alert("Lưu thành công");
                    }
                    if (responseData == false) {
                        alert("Lưu thất bại");
                    }
                    if (typeof(responseData) == 'number') {
                        var urlStr = window.location.host;
                        window.location = "http://"+urlStr + "/Products/Edit/" + responseData;
                    }
                }
            });
        }
    });


});


function getProducts(page) {
    var searchCondition = $("#formSearchInput").serializeArray();
    searchCondition.push({ name: "page", value: page });
    $.ajax({
        url: "/Products/List",
        type: "POST",
        data: searchCondition,
        success: function (data) {
            $("#listProducts").empty();
            $("#listProducts").html(data);
        }
    });
}

function renderCategories(CategoryID,d) {
    $.ajax({
        url: "/Categories/getAllCategories",
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var optionHtml = "";
            var categData = responseData;
            $("#CategoryID").empty();
            if (document.getElementById('CategoryID').value === "" && !window.location.pathname.includes("/Products/Edit")
                && !window.location.pathname.includes("/Products/AddProduct") && !window.location.pathname.includes("/Products/Delete")) {
                optionHtml = "<option value = '0' " + " selected> " + "---Tất cả loại hàng ---" + "</option> ";
                $("#CategoryID").append(optionHtml);
            }
            $.each(categData, function (i, v) {
                if (CategoryID == v.CategoryID) {
                    optionHtml = "<option value = " + v.CategoryID + " selected> " + v.CategoryName + "</option> ";
                } else {
                    optionHtml = "<option value = " + v.CategoryID + "> " + v.CategoryName + "</option> ";
                }
                
                $("#CategoryID").append(optionHtml);
            });
        }
    });
}

function renderSupplierByCateID(CategoryID,SupplierID,d) {
    $.ajax({
        url: "/Products/ListSupplierByCategID?CategoryID="+ CategoryID,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var optionHtml = "";
            var supData = responseData;
            $("#SupplierID").empty();
            if (document.getElementById('SupplierID').value === "" && !window.location.pathname.includes("/Products/Edit")
                && !window.location.pathname.includes("/Products/AddProduct") && !window.location.pathname.includes("/Products/Delete")) {
                optionHtml = "<option value = '0' " + " selected> " + "---Tất cả nhà cung cấp ---" + "</option> ";
                $("#SupplierID").append(optionHtml);
            }
            $.each(supData, function (i, v) {
                if (SupplierID == v.SupplierID) {
                    optionHtml = "<option value = " + v.SupplierID + " selected> " + v.SupplierName + "</option> ";
                } else {
                    optionHtml = "<option value = " + v.SupplierID + "> " + v.SupplierName + "</option> ";
                }
                
                $("#SupplierID").append(optionHtml);
            });
        }
    });
}