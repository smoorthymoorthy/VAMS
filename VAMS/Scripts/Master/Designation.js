
$(document).ready(function () {
    LoadCompanyDropdown();
    LoadDepartment();
    LoadDesignation();
    LoadDepartmentPopup();
});

function locationchange() {
    LoadDepartment();
    LoadDesignation();
}

function LoadDepartment() {
    var CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Department/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $('#ddldepartment').html('');
            if (response.length > 0) {
                $.each(response, function (key, val) {
                    $('#ddldepartment').append("<option value=" + val.DepartmentID + ">" + val.DepartmentName + "</option>");
               });
            }
        },
        failure: function (response) {
            console.log('failure' + response.responseText);
        },
        error: function (response) {
            console.log('error' + response.responseText);
        }
    });
}


function showModal() {
    ClearMsg();
    ClearData();
    $('#hdnDesignationID').val(0);
    $('#myModal').modal('show');
    LoadDepartmentPopup();
    $('#ddldepartmentpopup').val($('#ddldepartment :selected').val());

}
function ClearData() {
    $('#hdnDesignationID').val(0);
    $('#txtDesignation').val('');
    $('#txtDesignationShort').val('');
    AssignSuccess("txtDesignation");
    AssignSuccess("txtDesignationShort");
}

function LoadDepartmentPopup(){
    var CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Department/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $('#ddldepartmentpopup').html('');
            if (response.length > 0) {
                $.each(response, function (key, val) {
                    $('#ddldepartmentpopup').append("<option value=" + val.DepartmentID + ">" + val.DepartmentName + "</option>");
                });
            }
         },
        failure: function (response) {
            console.log('failure' + response.responseText);
        },
        error: function (response) {
            console.log('error' + response.responseText);
        }
    });


}

function LoadDesignation() {
       var table = '';
        var tr = '';
        var CompanyID = $('#ddlCompany').val();
        var DepartmentID = $('#ddldepartment').val(); 
         $.ajax({
            type: "GET",
             url: baseUrl + "API/Designation/GetAll",
            data: { "ID": 0, "CompanyID": CompanyID, "DepartmentID": DepartmentID},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                if (response.length > 0) {
                    table = table + '<table class="display nowrap table-responsive" id="example" style="width:100%"><thead><th>SI.NO<th>Designation Name</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead><tbody>'
                    $.each(response, function (key, val) {
                        tr = tr + '<tr><td>' + parseInt(parseInt(key) + parseInt(1)) + '</td >';
                        tr = tr + '<td>' + val.DesignationName + '</td >';
                        tr = tr + '<td>' + val.DesignationShortName + '</td>';
                        tr = tr + '<td ><a class="btn btn-primary" onclick="Edit(' + val.DesignationID + ')"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a></td>';
                        tr = tr + '<td ><a class="btn  btn-danger" onclick="Delete(' + val.DesignationID + ',\'' + val.DesignationName + '\')"><i class="fa fa-trash" aria-hidden="true"></i></td></tr>';
                    });
                    table = table + tr + '</tbody></table>';
                    $('#dvTable').html('');
                    $('#dvTable').html(table);
                    $('#example').DataTable({
                        "aoColumnDefs": [
                            { 'bSortable': false, 'aTargets': [3, 4] }
                        ],
                        "scrollX": true,
                        "scrollY": true,
                        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    });
                }
                else {
                    table = table + '<table class="display nowrap table-responsive" id="example" style="width:100%"><thead><th>SI.NO<th>Department Name</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead><tbody>'
                    table = table + tr + '</tbody></table>';
                    $('#dvTable').html('');
                    $('#dvTable').html(table);
                    $('#example').DataTable({
                        "aoColumnDefs": [
                            { 'bSortable': false, 'aTargets': [3, 4] }
                        ],
                        "scrollX": true,
                        "scrollY": true,
                        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    });
                }

            },
            failure: function (response) {
                console.log('failure' + response.responseText);
            },
            error: function (response) {
                console.log('error' + response.responseText);
            }
        });
    
}




function Save() {
    ClearMsg();
    ValidateFields('ddldepartment', 'ddl');
    ValidateFields('txtDesignation', 'txt');
    ValidateFields('txtDesignationShort', 'txt');

    if ($('#ddldepartment').val() == 0) {
        return;
    }
    if ($('#txtDesignation').val() == '') {
        return;
    }
    if ($('#txtDesignationShort').val() == '') {
        return;
    }

    var Designation = new Object();
    Designation.DepartmentID = $('#ddldepartment').val();
    Designation.DesignationID = $('#hdnDesignationID').val();
    Designation.DesignationName = $('#txtDesignation').val();
    Designation.DesignationShortName = $('#txtDesignationShort').val();
    Designation.CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "POST",
        url: baseUrl + "API/Designation/InsertUpdateDesignation",
        data: JSON.stringify(Designation),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == -1) {
                //Content = ContentMaking(BuildingLang, Building.Name, NotSaveLang);
                Content = ContentMaking("Designation is not saved")
                ShowErrorMsg(Content);
                //ShowErrorMsg("Record was not saved successfully.");
            }
            else if (response == 1) {

                if ($('#hdnBuildingID').val() == "0") {
                    //Content = ContentMaking(BuildingLang, Building.Name, SaveMsgLang);
                    Content = ContentMaking("Designation saved successfully");
                    ShowSuccessMsg(Content);
                }
                if ($('#hdnBuildingID').val() != "0") {
                    //Content = ContentMaking(BuildingLang, Building.Name, UpdateMsgLang);
                    Content = ContentMaking("Designation updated successfully");
                    ShowSuccessMsg(Content);
                }

                //ShowSuccessMsg("Building saved successfully.");
                LoadDesignation();
                ClearData();
                $('#myModal').modal('hide');
            }
            else {
                //Content = ContentMaking(BuildingLang, Building.Name, AlreadyExistMsgLang);
                Content = ContentMaking("Designation already exist");
                ShowErrorMsg(Content);
                //ShowErrorMsg(response);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function Edit(ID) {
    var CompanyID = $('#ddlCompany').val();
    var DepartmentID = $('#ddldepartment').val(); 
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Designation/GetAll",
        data: { "ID": ID, "CompanyID": CompanyID, "DepartmentID": DepartmentID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response.length > 0) {
                $('#txtDesignation').val(response[0].DesignationName);
                $('#txtDesignationShort').val(response[0].DesignationShortName);
                $('#hdnDesignationID').val(response[0].DesignationID);
                Loaddeptpopup();
                $('#ddldepartmentpopup').val(response[0].DepartmentID);
                ValidateFields('txtDesignation', 'txt');
                ValidateFields('txtDesignationShort', 'nddl');
                $('#myModal').modal('show');
            }
        },
        failure: function (response) {
            console.log('failure' + response.responseText);
        },
        error: function (response) {
            console.log('error' + response.responseText);
        }
    });
    ClearMsg();
}

function Delete(ID, Building) {
    //Header = HeaderMaking(BuildingLang);
    //Content = ContentMaking(BuildingLang, Building, DeleteConfirmMsgLang);
    bootbox.confirm({
        title: "Department",
        message: "Do u want to delete this?",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i>Cancel'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Confirm'
            }
        },
        callback: function (result) {
            if (result == true) {
                ClearMsg();
                $.ajax({
                    type: "GET",
                    url: baseUrl + "API/Designation/Delete",
                    data: { "ID": ID },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async:false,
                    success: function (response) {
                        if (response == 1) {
                            //Content = ContentMaking(BuildingLang, Building, DeleteMsgLang);
                            //ShowSuccessMsg(Content);
                            ShowSuccessMsg("Designation was deleted successfully.");
                            LoadDesignation();
                            ClearData();
                        }
                        else {
                            //Content = ContentMaking(BuildingLang, Building, AlreadyUsedMsgLang);
                            //ShowErrorMsgCommon(Content);
                            ShowSuccessMsg("Designation already used");
                        }
                    },
                    failure: function (response) {
                        console.log('failure' + response.responseText);
                    },
                    error: function (response) {
                        console.log('error' + response.responseText);
                    }
                });
            }
        }
    });



}


