
$(document).ready(function () {
    LoadMenu();   
    LoadCompanyDropdown();
 
});

function LoadMenu() {
    $.ajax({
        type: "GET",
        url: baseUrl + "Menu/Loadmenu",
        async: false,
        dataType: "json",
        success: function (result) {
            debugger;
            $("#LstMenu").html(result.divs);
        },
        failure: function (result) {
        }
    });
}

function LoadCompanyDropdown() {
    $.ajax({
        type: "GET",
        url: baseUrl + "Menu/dropdowncompany",
        //data: "../Master/dropdown?companytid=" + id,
        dataType: "json",
        async: false,
        success: (function (result) {
            $.each(result.result, function (i, value) {
                $('#ddlCompany').append('<option value ="' + value.CompanyID + '">' + value.CompanyName + '</option>');
            });
            $('#ddlCompany').val(result.value);
            LoadLocationDropdown();
          
        })
    });
}


function LoadLocationDropdown() {
    var companyid = $('#ddlCompany').val();

    $.ajax({
        type: "GET",
        //url: "../Master/dropdownlocation",
        url: baseUrl + "Menu/dropdownlocation?CompanyID=" + companyid,
        dataType: "json",
        async: false,
        success: (function (data) {
            $('#ddlLocation').empty();
            $.each(data.result, function (i, value) {
                $('#ddlLocation').append('<option value ="' + value.LocationID + '">' + value.LocationName + '</option>');
            });
            $('select option[value="1"]').attr("selected", true);
        })
    });
}

