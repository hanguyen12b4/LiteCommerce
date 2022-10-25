function getListOrder(page) {
    var searchCondition = $("#formSearchInput").serializeArray();
    searchCondition.push({ name: "page", value: page });
    $.ajax({
        url: "/Orders/List",
        type: "POST",
        data: searchCondition,
        success: function (data) {
            $("#listOrder").empty();
            $("#listOrder").html(data);
        }
    });
}
$(document).ready(function () {
    getListOrder(1);
    $("#formSearchInput").submit(function (e) {
        e.preventDefault();
        getListOrder(1);
    });

    $('.refresh-btn').on('click', function () {
        var $formSearchInput = $('#formSearchInput');
        resetFormModal($formSearchInput);
    });
});