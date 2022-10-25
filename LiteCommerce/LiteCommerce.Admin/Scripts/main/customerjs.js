$(document).ready(function () {

    var $CustomerModal = $('#CustomerModal');
    var $DeleteCustomerModal = $('#DeleteCustomerModal')
    var $customerForm = $('#customer-form');


    $('.add-customer').on('click', function () {
        showModalWithCustomizedTitle($CustomerModal, "Thêm mới khách hàng");
        resetFormModal($customerForm);
        renderCountries();
        renderCitiesByCountry(document.getElementById('Country').value);
    });
    //Show supplier form
    $('.edit-customer').on('click', function () {
        showModalWithCustomizedTitle($CustomerModal, "Chỉnh sửa thông tin khách hàng");
        resetFormModal($customerForm);
        $.ajax({
            url: "/Customers/Edit?id=" + $(this).data("id"),
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (responseData) {
                var customerData = responseData;
                $('#CustomerID').val(customerData.CustomerID);
                $('#CustomerName').val(customerData.CustomerName);
                $('#ContactName').val(customerData.ContactName);
                $('#Address').val(customerData.Address);
                $('#PostalCode').val(customerData.PostalCode);
                $('#Phone').val(customerData.Phone);
                $('#Email').val(customerData.Email);
                $('#Password').val(customerData.Password);
                renderCountries(customerData.Country);
                renderCitiesByCountry(customerData.Country, customerData.City);
            }
        });
    });
    //Save btn 
    $('.save-customer').on('click', function () {
        var formData = new FormData($customerForm[0]);
        $customerForm.validate({
            ignore: [],
            rules: {
                CustomerName: {
                    required: true,
                    maxlength: 100,
                },
                ContactName: {
                    required: true,
                    maxlength: 100,
                },

            },
            messages: {
                CustomerName: {
                    required: "Vui lòng nhập tên khách hàng"
                },
                ContactName: {
                    required: "Vui lòng nhập tên liên hệ"
                },

            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });

        if ($customerForm.valid()) {
            $.ajax({
                url: "/Customers/Save",
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

    $('.acept-delete-customer').on('click', function () {
        $.ajax({
            url: "/Customers/Delete?CustomerID=" + $(this).data('id'),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == 1) {
                    $('#DeleteCustomerModal').modal('hide');
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
        showModalWithCustomizedTitle($supModal, "Thêm nhà cung cấp");
        renderCitiesByCountry(document.getElementById('Country').value);
    });

    // Show delete form
    $('.delete-customer').on('click', function () {
        showModalWithCustomizedTitle($DeleteCustomerModal, "Xác nhận xóa khách hàng");
        $('#name').text($(this).data('name'));
        $(".acept-delete-customer").attr("data-id", $(this).data("id"));
    });

});
