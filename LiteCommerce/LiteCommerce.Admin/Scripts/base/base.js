$(document).ready(function () {
    
});

// show modal mà thay đổi title dùng hàm ni
function showModalWithCustomizedTitle($selectedModal, title) {
    $selectedModal.find(".modal-title").text(title);
    $selectedModal.modal('show');
}
// reset modal khi đổ dữ liệu
function resetFormModal($formElement) {
    $formElement[0].reset();
    $formElement.find('#Country').val("");
    $formElement.validate().destroy();
}

// hiển thị country
function renderCountries(CountryName) {
    $.ajax({
        url: "/CommonApi/CountryApi/",
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var optionHtml = "";
            var countryData = responseData;
            $("#Country").empty();
            if (document.getElementById('Country').value === "") {
                optionHtml = "<option value = '' " + " selected> " + "---Chọn quốc gia---" + "</option> ";
                $("#Country").append(optionHtml);
            }
            $.each(countryData, function (i, v) {
                if (CountryName === v.CountryName) {
                    optionHtml = "<option value = " + v.CountryName + " selected> " + v.CountryName + "</option> ";
                } else {
                    optionHtml = "<option value = " + v.CountryName + "> " + v.CountryName + "</option> ";
                }

                $("#Country").append(optionHtml);
            });

        }
    });
}

//hiển thị city by countryName
function renderCitiesByCountry(countryName, cityName) {
    $.ajax({
        url: "/CommonApi/CityApi?CountryName=" + countryName,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (responseData) {
            var optionHtml = "";
            var citiesData = responseData;
            $("#City").empty();
            if (countryName === "") {
                optionHtml = "<option value = '' " + " selected> " + "---Chọn thành phố ---" + "</option> ";
                $("#City").append(optionHtml);
            }
            $.each(citiesData, function (i, v) {
                if (cityName === v.CityName) {
                    optionHtml = "<option value = " + v.CityName + " selected> " + v.CityName + "</option> ";
                } else {
                    optionHtml = "<option value = " + v.CityName + "> " + v.CityName + "</option> ";
                }
                $("#City").append(optionHtml);
            });
        }
    });
}