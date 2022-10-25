$(document).ready(function () {

    var $CategoryModal = $('#CategoryModal');
    var $CategoryForm = $('#CategoryForm');
    var $DeleteCagModal = $('#DeleteCagModal');
    // Show add form, show add modal
    $('#add-category-btn').on('click', function () {
        resetFormModal($CategoryForm);
        showModalWithCustomizedTitle($CategoryModal, "Thêm loại sản phẩm");
    });
    //Show edit form
    $('.edit-category-btn').on('click', function () {
        resetFormModal($CategoryForm);
        showModalWithCustomizedTitle($CategoryModal, "Chỉnh sửa loại sản phẩm");
        renderCategory($(this).data('id'));
    });
    //save category
    $('.save-category').on('click' , function () {
        var formData = new FormData($CategoryForm[0]);
        //validation , kiểm tra hợp lệ
        $CategoryForm.validate({
            ignore: [],
            rules: {
                CategoryName: {
                    required: true,
                    maxlength: 100,
                },
                Description: {
                    required: true,
                    maxlength: 100,
                },

            },
            messages: {
                CategoryName: {
                    required: "Vui lòng nhập tên loại sản phẩm"
                },
                Description: {
                    required: "Vui lòng nhập mô tả loại sản phầm"
                },

            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });
        //ajax lưu
        if ($CategoryForm.valid()) {
            $.ajax({
                url: "/Categories/Save",
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
    $('.delete-category-btn').on('click', function () {
        $('#DeleteCagModal').modal('show');
        $('#name').text($(this).data('name'));
        $(".delete-cag-btn").attr("data-id", $(this).data("id"));
    });
    //ajax xóa
    $('.delete-cag-btn').on('click', function () {
        $.ajax({
            url: "/Categories/Delete?CategoryID=" + $(this).data('id'),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == 1) {
                    $('#CategoryModal').modal('hide');
                    var urlStr = window.location.href;
                    window.location = urlStr;
                } else {
                    console.log("lỗi");
                }

            }
        });
    });
});


function renderCategory(categoryId) {
    $.ajax({
        url: "/Categories/Edit?id=" + categoryId,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var data = responseData;
            $('#CategoryID').val(data.CategoryID);
            $('#CategoryName').val(data.CategoryName);
            $('#Description').val(data.Description);
        }
    });
}