$(document).ready(function () {
    var $AttributeModal = $('#AttributeModal');
    var $AttributeForm = $('#AttributeForm');
    var $GalleryModal = $('#GalleryModal');
    var $GalleryForm = $('#GalleryForm');
    //var $DeleteCagModal = $('#DeleteCagModal');
    $('.add-attr-btn').on('click', function () {
        resetFormModal($AttributeForm);
        $('#AttributeID').val(0);
        showModalWithCustomizedTitle($AttributeModal, "Thêm thuộc tính của sản phẩm");
    });

    $('.add-gallery-btn').on('click', function () {
        resetFormModal($GalleryForm);
        $('#GalleryID').val(0);
        showModalWithCustomizedTitle($GalleryModal, "Thêm ảnh của sản phẩm");
    });

    $('.edit-attr-btn').on('click', function () {
        resetFormModal($AttributeForm);
        showModalWithCustomizedTitle($AttributeModal, "Chỉnh sửa loại sản phẩm");
        renderAttribute($(this).data('id'));
    });

    $('.edit-gallery-btn').on('click', function () {
        resetFormModal($GalleryForm);
        showModalWithCustomizedTitle($GalleryModal, "Chỉnh ảnh sản phẩm");
        renderGallery($(this).data('id'));
    });

    $('.save-attribute').on('click', function () {
        var formData = new FormData($AttributeForm[0]);
        //validation , kiểm tra hợp lệ
        $AttributeForm.validate({
            ignore: [],
            rules: {
                AttributeName: {
                    required: true,
                    maxlength: 100,
                },
                AttributeValue: {
                    required: true,
                    maxlength: 100,
                },

            },
            messages: {
                AttributeName: {
                    required: "Vui lòng nhập tên thuộc tính"
                },
                AttributeValue: {
                    required: "Vui lòng nhập giá trị thuộc tính"
                },
            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });
        //ajax lưu
        if ($AttributeForm.valid()) {
            $.ajax({
                url: "/Products/SaveAttribute",
                type: 'POST',
                enctype: 'multipart/form-data',
                processData: false,
                contentType: false,
                cache: false,
                timeout: 10000,
                data: formData,
                success: function (responseData) {
                    if (responseData != 0) {
                        var urlStr = window.location.href;
                        window.location = urlStr;
                    }
                    
                }
            });
        }
    });
    $('.save-gallery').on('click', function () {
        var formData = new FormData($GalleryForm[0]);
        //validation , kiểm tra hợp lệ
        $GalleryForm.validate({
            ignore: [],
            rules: {
               
            },
            messages: {
                
            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });
        //ajax lưu
        if ($GalleryForm.valid()) {
            $.ajax({
                url: "/Products/SaveGallery",
                type: 'POST',
                enctype: 'multipart/form-data',
                processData: false,
                contentType: false,
                cache: false,
                timeout: 10000,
                data: formData,
                success: function (responseData) {
                    if (responseData != 0) {
                        var urlStr = window.location.href;
                        window.location = urlStr;
                    }

                }
            });
        }
    });


});

function renderAttribute(AttributeID) {
    $.ajax({
        url: "/Products/GetAttribute?AttributeID=" + AttributeID,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData;
            $('#AttributeName').val(data.AttributeName);
            $('#AttributeValue').val(data.AttributeValue);
            $('#AttributeID').val(data.AttributeID);
            $('#ProductID').val(data.ProductID);
            $('#DisplayOrder').val(data.DisplayOrder);
        }
    });
}
function renderGallery(GalleryID) {
    $.ajax({
        url: "/Products/GetGallery?GalleryID=" + GalleryID,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData;
            $('#Description').val(data.Description);
            $('#GalleryID').val(data.GalleryID);
            $('#GalleryModal #DisplayOrder').val(data.DisplayOrder);
            $('#IsHidden').val(data.isHidden);
        }
    });
}
