$(document).ready(function () {

    var $ShipperModal = $('#ShipperModal');
    var $ShipperForm = $('#ShipperForm');
    var $DeleteShipperModal = $('#DeleteShipperModal');
    // Show add form
    $('.add-shipper-btn').on('click', function () {
        resetFormModal($ShipperForm);
        showModalWithCustomizedTitle($ShipperModal, "Thêm nhà vận chuyển");
    });
    //Show edit form
    $('.edit-shipper-btn').on('click', function () {
        resetFormModal($ShipperForm);
        showModalWithCustomizedTitle($ShipperModal, "Chỉnh sửa nhà vận chuyển");
        renderShipper($(this).data('id'));
    });
    //save category
    $('.save-shipper').on('click', function () {
        var formData = new FormData($ShipperForm[0]);
        $ShipperForm.validate({
            ignore: [],
            rules: {
                ShipperName: {
                    required: true,
                    maxlength: 100,
                },
                Phone: {
                    required: true,
                    maxlength: 100,
                },

            },
            messages: {
                ShipperName: {
                    required: "Vui lòng nhập tên nhà vận chuyển"
                },
                Phone: {
                    required: "Vui lòng nhập số điện thoại"
                },

            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });

        if ($ShipperForm.valid()) {
            $.ajax({
                url: "/Shippers/Save",
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
    //Show delete form
    $('.delete-shipper-btn').on('click', function () {
        $('#DeleteShipperModal').modal('show');
        $('#name').text($(this).data('name'));
        $(".acept-delete-shipper").attr("data-id", $(this).data("id"));
    });

    $('.acept-delete-shipper').on('click', function () {
        $.ajax({
            url: "/Shippers/Delete?ShipperID=" + $(this).data('id'),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == 1) {
                    $('#DeleteShipperModal').modal('hide');
                    var urlStr = window.location.href;
                    window.location = urlStr;
                } else {
                    console.log("lỗi");
                }

            }
        });
    });
});


function renderShipper(shipperID) {
    $.ajax({
        url: "/Shippers/Edit?id=" + shipperID,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData;
            $('#ShipperID').val(data.ShipperID);
            $('#ShipperName').val(data.ShipperName);
            $('#Phone').val(data.Phone);

        }
    });
}