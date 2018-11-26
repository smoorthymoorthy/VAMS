
$(document).ready(function () {
   
    LoadCompanyDropdown();
    LoadDepartment();
    LoadDesignation();
    LoadGrade(); 
    //loaddesignation();
});


function locationchange() {
    LoadDepartment();
    LoadDesignation();
}

function Departmentchange() {
    LoadDesignation();
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



function LoadDesignation() {
    var CompanyID = $('#ddlCompany').val();
    var DepartmentID = $('#ddldepartment').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Designation/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID, "DepartmentID": DepartmentID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $('#ddldesignation').html('');
            if (response.length > 0) {
                $.each(response, function (key, val) {
                    $('#ddldesignation').append("<option value=" + val.DesignationID + ">" + val.DesignationName + "</option>");
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
    $('#hdnGradeID').val(0);
    $('#myModal').modal('show');
    LoadDepartmentPopup();
    $('#ddldepartmentpopup').val($('#ddldepartment :selected').val());
    LoadDesignationPopup();
    $('#ddldesignationpopup').val($('#ddldesignation :selected').val());

}

function ClearData() {
    $('#hdnGradeID').val(0);
    $('#ddldesignationpopup').val(0);
    $('#ddldepartmentpopup').val(0);
    $('#txtGrade').val('');
    $('#txtGradeShort').val('');
    AssignSuccess("txtGrade");
    AssignSuccess("txtGradeShort");
}

function LoadDepartmentPopup() {
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

function LoadDesignationPopup() {
    var CompanyID = $('#ddlCompany').val();
    var DepartmentID = $('#ddldepartmentpopup').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Designation/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID, "DepartmentID": DepartmentID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $('#ddldesignationpopup').html('');
            $('#ddldesignationpopup').append("<option value=0>" + all + "</option>");
            if (response.length > 0) {
                $.each(response, function (key, val) {
                    $('#ddldesignationpopup').append("<option value=" + val.DesignationID + ">" + val.DesignationName + "</option>");
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





function LoadGrade() {
   
    var table = '';
    var tr = '';
    var CompanyID = $('#ddlCompany').val();
    var DepartmentID = $('#ddldepartment').val();
    var DesignationID = $('#ddldesignation').val();
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Grade/GetAll",
        data: { "ID": 0, "CompanyID": CompanyID, "DepartmentID": DepartmentID,"DesignationID":DesignationID},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
           
            if (response.length > 0) {
                table = table + '<table class="display nowrap table-responsive" id="example" style="width:100%"><thead><th>SI.NO<th>Designation Name</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead><tbody>'
                $.each(response, function (key, val) {
                    tr = tr + '<tr><td>' + parseInt(parseInt(key) + parseInt(1)) + '</td >';
                    tr = tr + '<td>' + val.GradeName + '</td >';
                    tr = tr + '<td>' + val.GradeShortName + '</td>';
                    tr = tr + '<td ><a class="btn btn-primary" onclick="Edit(' + val.GradeID + ')"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a></td>';
                    tr = tr + '<td ><a class="btn  btn-danger" onclick="Delete(' + val.GradeID + ',\'' + val.GradeName + '\')"><i class="fa fa-trash" aria-hidden="true"></i></td></tr>';
                });
                table = table + tr + '</tbody></table>';
                $('#dvTable').html('');
                $('#dvTable').html(table);
                $('#example').DataTable({
                    "scrollY": "400px",
                    "scrollCollapse": true,
                    'aoColumnDefs': [{
                        'bSortable': false,
                        'aTargets': [-2, -1] /* 1st one, start by the right */
                    }]
                });
            }
            else {
                table = table + '<table class="display nowrap table-responsive" id="example" style="width:100%"><thead><th>SI.NO<th>Department Name</th><th>Short Name</th><th>Edit</th><th>Delete</th></thead><tbody>'
                table = table + tr + '</tbody></table>';
                $('#dvTable').html('');
                $('#dvTable').html(table);
                $('#example').DataTable({
                    "scrollY": "400px",
                    "scrollCollapse": true,
                    'aoColumnDefs': [{
                        'bSortable': false,
                        'aTargets': [-2, -1] /* 1st one, start by the right */
                    }]
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
    ValidateFields('ddldepartmentpopup', 'ddl');
    ValidateFields('ddldesignationpopup', 'ddl');
    ValidateFields('txtGrade', 'txt');
    ValidateFields('txtGradeShort', 'txt');

    if ($('#ddldepartmentpopup').val() == 0) {
        return;
    }

    if ($('#ddldesignationpopup').val() == 0) {
        return;
    }

    if ($('#txtGrade').val() == '') {
        return;
    }
    if ($('#txtGradeShort').val() == '') {
        return;
    }

    var Grade = new Object();
    Grade.GradeID = $('#hdnGradeID').val();
    Grade.DepartmentID = $('#ddldepartmentpopup').val();
    Grade.DesignationID = $('#ddldesignationpopup').val();
    Grade.GradeName = $('#txtGrade').val();
    Grade.GradeShortName = $('#txtGradeShort').val();
    Grade.CompanyID = $('#ddlCompany').val();
    $.ajax({
        type: "POST",
        url: baseUrl + "API/Grade/InsertUpdateGrade",
        data: JSON.stringify(Grade),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
           if (response == -1) {
                Content = ContentMaking(GradeLang, Grade.GradeName, NotSaveLang);
                ShowErrorMsg(Content);
            }
            else if (response == 1) {

                if ($('#hdnBuildingID').val() == "0") {
                    Content = ContentMaking(GradeLang, Grade.GradeName, SaveMsgLang);
                    ShowSuccessMsg(Content);
                }
                if ($('#hdnBuildingID').val() != "0") {
                    Content = ContentMaking(GradeLang, Grade.GradeName, UpdateMsgLang);
                    ShowSuccessMsg(Content);
                }
                LoadGrade(); 
                ClearData();
                $('#myModal').modal('hide');
            }
            else {
                Content = ContentMaking(GradeLang, Grade.GradeName, AlreadyExistMsgLang);
                ShowErrorMsg(Content);
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
    var DesignationID = $('#ddldesignation').val();
   
    $.ajax({
        type: "GET",
        url: baseUrl + "API/Grade/GetAll",
        data: { "ID": ID, "CompanyID": CompanyID, "DepartmentID": DepartmentID, "DesignationID": DesignationID },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
           
            if (response.length > 0) {
                $('#txtGrade').val(response[0].GradeName);
                $('#txtGradeShort').val(response[0].GradeShortName);
                $('#hdnGradeID').val(response[0].GradeID);
                LoadDepartmentPopup();
                $('#ddldepartmentpopup').val(response[0].DepartmentID);
                LoadDesignationPopup();
                $('#ddldesignationpopup').val(response[0].DesignationID);
                ValidateFields('txtGrade', 'txt');
                ValidateFields('txtGradeShort', 'nddl');
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

function Delete(ID, Grade) {
    Header = HeaderMaking(GradeLang);
    Content = ContentMaking(GradeLang, Building, DeleteConfirmMsgLang);
    bootbox.confirm({
        title: "Grade",
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
                    url: baseUrl + "API/Grade/Delete",
                    data: { "ID": ID },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        if (response == 1) {
                            Content = ContentMaking(GradeLang, Grade, DeleteMsgLang);
                            ShowSuccessMsg(Content);
                            LoadGrade(); 
                            ClearData();
                        }
                        else {
                            Content = ContentMaking(GradeLang, Grade, AlreadyUsedMsgLang);
                            ShowErrorMsgCommon(Content);
                            
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


