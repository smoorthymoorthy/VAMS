
$(document).ready(function () {
    LoadCompanyDropdown();
    LoadDepartment();
});

function LocationChange() {
    ClearMsg();
    LoadDepartment();
}

function LoadDepartment() {
    var table = '';
    var tr = '';
    var CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Department/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            if (response.length > 0) {
                table = table + '<table class="display nowrap table-responsive" id="example" style="width:100%"><thead><th>SI.NO<th>Department Name</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead><tbody>'
                $.each(response, function (key, val) {
                    tr = tr + '<tr><td>' + parseInt(parseInt(key) + parseInt(1)) + '</td >';
                    tr = tr + '<td>' + val.DepartmentName + '</td >';
                    tr = tr + '<td>' + val.DepartmentShortName + '</td>';
                    tr = tr + '<td ><a class="btn btn-primary" onclick="Edit(' + val.DepartmentID + ')"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a></td>';
                    //tr = tr + '<td ><a class="btn  btn-danger" onclick="Delete(' + val.ID + ')"><i class="fa fa-trash" aria-hidden="true"></i></td></tr>';
                    tr = tr + '<td ><a class="btn  btn-danger" onclick="Delete(' + val.DepartmentID + ',\'' + val.DepartmentName + '\')"><i class="fa fa-trash" aria-hidden="true"></i></td></tr>';
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

function showModal() {
    ClearMsg();
    ClearData();
    $('#hdnDepartmentID').val(0);
    $('#myModal').modal('show');

}

function ClearData() {
    $('#hdnDepartmentID').val(0);
    $('#txtDepartment').val('');
    $('#txtDepartmentShort').val('');
    AssignSuccess("txtDepartment");
    AssignSuccess("txtDepartmentShort");
}

function Save() {
    ClearMsg();
    ValidateFields('txtDepartment', 'txt');
    ValidateFields('txtDepartmentShort', 'txt');

    if ($('#txtDepartment').val() == '') {
        return;
    }
    if ($('#txtDepartmentShort').val() == '') {
        return;
    }

    var Department = new Object();
    Department.DepartmentID = $('#hdnDepartmentID').val();
    Department.DepartmentName = $('#txtDepartment').val();
    Department.DepartmentShortName = $('#txtDepartmentShort').val();
    Department.CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "POST",
        url: baseUrl + "API/Department/InsertUpdateDept",
        data: JSON.stringify(Department),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async:false,
        success: function (response) {
            if (response == -1) {
                Content = ContentMaking(BuildingLang, Department.DepartmentName, NotSaveLang);
                Content = ContentMaking("Department is not saved")
                ShowErrorMsg(Content);
            }
            else if (response == 1) {

                if ($('#hdnBuildingID').val() == "0") {
                    Content = ContentMaking(BuildingLang, Department.DepartmentName, SaveMsgLang);
                    ShowSuccessMsg(Content);
                }
                if ($('#hdnBuildingID').val() != "0") {
                    Content = ContentMaking(BuildingLang, Department.DepartmentName, UpdateMsgLang);
                    //Content = ContentMaking("Department updated successfully");
                    ShowSuccessMsg(Content);
                }

                //ShowSuccessMsg("Building saved successfully.");
                LoadDepartment();
                ClearData();
                $('#myModal').modal('hide');
            }
            else {
                //Content = ContentMaking(BuildingLang, Building.Name, AlreadyExistMsgLang);
                Content = ContentMaking("Department already exist");
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
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Department/GetAll",
        data: { "ID": ID, "CompanyID": CompanyID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            if (response.length > 0) {
                $('#txtDepartment').val(response[0].DepartmentName);
                $('#txtDepartmentShort').val(response[0].DepartmentShortName);
                $('#hdnDepartmentID').val(response[0].DepartmentID);

                ValidateFields('txtDepartment', 'txt');
                ValidateFields('txtDepartmentShort', 'nddl');
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
                    url: baseUrl + "API/Department/Delete",
                    data: { "ID": ID },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == 1) {
                            //Content = ContentMaking(BuildingLang, Building, DeleteMsgLang);
                            //ShowSuccessMsg(Content);
                            ShowSuccessMsg("Department was deleted successfully.");
                            LoadDepartment();
                            ClearData();
                        }
                        else {
                            //Content = ContentMaking(BuildingLang, Building, AlreadyUsedMsgLang);
                            //ShowErrorMsgCommon(Content);
                            ShowSuccessMsg("Department already used");
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


