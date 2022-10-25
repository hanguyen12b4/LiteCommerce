$(document).ready(function () {

    var $EmpModal = $('#EmpModal');
    var $DeleteEmpModal = $('#DeleteEmpModal');
    var $EmpForm = $('#EmpForm');


    $('.add-emp-btn').on('click', function () {
        showModalWithCustomizedTitle($EmpModal, "Thêm mới nhân viên");
        resetFormModal($EmpForm);
    });
    //Show supplier form
    $('.edit-emp-btn').on('click', function () {
        showModalWithCustomizedTitle($EmpModal, "Chỉnh sửa thông tin nhân viên");
        resetFormModal($EmpForm);
        $.ajax({
            url: "/Employees/Edit?id=" + $(this).data("id"),
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (responseData) {
                var employeeData = responseData;
                var date = new Date(parseInt(employeeData.BirthDate.substr(6)));
                res = date.toISOString();
                res = res.substr(0, 10);
                //var date = JSON.parse(employeeData.BirthDate);
                $('#EmployeeID').val(employeeData.EmployeeID);
                $('#FirstName').val(employeeData.FirstName);
                $('#LastName').val(employeeData.LastName);
                // Date1687269937
                $('#BirthDate').val(res);
                $('#Email').val(employeeData.Email);
                $('#Password').val(employeeData.Password);
                $('#Notes').text(employeeData.Notes);
            }
        });
    });
    $('.save-emp').on('click', function () {
        var formData = new FormData($EmpForm[0]);
        $EmpForm.validate({
            ignore: [],
            rules: {
                FirstName: {
                    required: true,
                    maxlength: 100,
                },
                LastName: {
                    required: true,
                    maxlength: 100,
                },
                BirthDate: {
                    required: true,
                },

            },
            messages: {
                FirstName: {
                    required: "Vui lòng nhập tên nhà cung cấp"
                },
                LastName: {
                    required: "Vui lòng nhập tên liên hệ"
                },
                BirthDate: {
                    required: "Vui lòng nhập ngày sinh"
                },

            },
            errorElement: "div",
            errorClass: "error-message-invalid"
        });

        if ($EmpForm.valid()) {
            $.ajax({
                url: "/Employees/Save",
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

    $('.delete-emp-btn').on('click', function () {
        showModalWithCustomizedTitle($DeleteEmpModal, "Xác nhận xóa nhân viên");
        $('#name').text($(this).data('name'));
        $(".acept-delete-emp").attr("data-id", $(this).data("id"));
    });


    $('.acept-delete-emp').on('click', function () {
        $.ajax({
            url: "/Employees/Delete?EmployeeID=" + $(this).data('id'),
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == 1) {
                    $('#DeleteEmpModal').modal('hide');
                    var urlStr = window.location.href;
                    window.location = urlStr;
                } else {
                    console.log("lỗi");
                }

            }
        });
    });

});

