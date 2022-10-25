$(document).ready(function () {

    var $supModal = $('#SupModal');
    var $deletesup = $('#DeleteSupModal')
    var $supform = $('#supForm');


    $('.add-sup-btn').on('click', function () {
        showModalWithCustomizedTitle($supModal, "Thêm nhà cung cấp");
        resetFormModal($supform);
        $('#SupplierID').closest('.cus-add-form').addClass('d-none');
        renderCountries();
        renderCitiesByCountry(document.getElementById('Country').value);
    });
    //Show supplier form
    $('.edit-sup-btn').on('click', function () {
        showModalWithCustomizedTitle($supModal, "Chỉnh sửa nhà cung cấp");
        $('#SupplierID').closest('.cus-add-form').removeClass('d-none');
        resetFormModal($supform);
        $.ajax({
            url: "/Suppliers/Edit?id=" + $(this).data("id"),
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (responseData) {
                var supData = responseData;
                $('#SupplierID').val(supData.SupplierID);
                $('#SupplierName').val(supData.SupplierName);
                $('#ContactName').val(supData.ContactName);
                $('#Address').val(supData.Address);
                $('#PostalCode').val(supData.PostalCode);
                $('#Phone').val(supData.Phone);
                renderCountries(supData.Country);
                renderCitiesByCountry(supData.Country, supData.City);
            }
        });
    });
    //Save btn 
    $('#save-Supplier-btn').on('click', function () {
        var formData = new FormData($supform[0]);
        $supform.validate({
            ignore: [],
            rules: {
                SupplierName: {
                    required: true,
                    maxlength: 100,
                },
                ContactName: {
                    required: true,
                    maxlength: 100,
                },
                
            },
            messages: {
                SupplierName: {
                    required: "Vui lòng nhập tên nhà cung cấp"
                },
                ContactName: {
                    required: "Vui lòng nhập tên liên hệ"
                },
               
            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });

        if ($supform.valid()) {
            $.ajax({
                url: "/Suppliers/Save",
                type: 'POST',
                enctype: 'multipart/form-data',
                processData: false,
                contentType: false,
                cache: false,
                timeout: 10000,
                data: formData,
                success: function (responseData) {
                    var urlStr = window.location.href;
                    window.location = urlStr;
                }
            });
        }
    });

    $('#delete-sup-btn').on('click', function () {
        $.ajax({
            url: "/Suppliers/Delete?SupplierID=" + $(this).data('id'),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == 1) {
                    $('#DeleteSupModal').modal('hide');
                    var urlStr = window.location.href;
                    window.location = urlStr;
                } else {
                    console.log("lỗi");
                }
                
            }
        });
    });


    //show country
    $('#Country').on('change', function () {
        renderCitiesByCountry(document.getElementById('Country').value);
    });

    // Show delete form
    $('.delete-sup-form').on('click', function () {
        showModalWithCustomizedTitle($deletesup, "Xác nhận xóa nhà cung cấp");
        $('.deleteName').text($(this).data('name'));
        $("#delete-sup-btn").attr("data-id", $(this).data("id"));
    });

});
