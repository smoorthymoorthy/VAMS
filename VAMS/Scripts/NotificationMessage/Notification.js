function ClearMsg() {
    $('#lblError').html("");
    $('#divErrormsg').hide();

    $('#lblErrorCommon').html("");
    $('#divErrormsgCommon').hide();

    $('#lblSuccess').html("");
    $('#divSuccessmsg').hide();

}

function ShowErrorMsg(Message) {
    $('#lblError').html(Message);
    $('#divErrormsg').show();
}

function ShowErrorMsgCommon(Message) {
    $('#lblErrorCommon').html(Message);
    $('#divErrormsgCommon').show();
}

function ShowErrorMsgProfile(Message) {
    $('#lblErrorProfile').html(Message);
    $('#divErrormsgProfile').show();
}

function ShowSuccessMsg(Message) {
    $('#lblSuccess').html(Message);
    $('#divSuccessmsg').show();
}

function ContentMaking(Label, Content, Msg) {
    if (Content == '') {
        return Label + " " + Msg;
    }
    else {
        return Label + " " + '<b>"' + Content + '"</b>' + Msg;
    }

}

function ContentMaking1(Label, Content, Msg) {
    if (Content == '') {
        return Label + " " + Msg;
    }
    else {
        return Label + " " + Content + Msg;
    }

}


function HeaderMaking(Label) {
    //return '<b>' + Label + '</b>';
    return Label;
}



function ShowSuccessMsgProfile(Message) {
    $('#lblSuccessProfile').html(Message);
    $('#divSuccessmsgProfile').show();
}


function AssignError(id) {
    $('#' + id).css({ "border-color": "red", "border-width": "1px", "border-style": "solid" });
}

function showMultiDropDownError(id) {
    $('#' + id).next('div').find('button').css({ "border-color": "red", "border-width": "1px", "border-style": "solid" });
}

function AssignSuccessMultiDropDown(id) {
    $('#' + id).next('div').find('button').css({ "border-color": "#ccc", "border-width": "1px", "border-style": "solid" });

}


function AssignSuccess(id) {
    $('#' + id).css({ "border-color": "#ccc", "border-width": "1px", "border-style": "solid" });
}


//function InvalidFields(id, fieldType) {
//    $('#' + id).parent('div').removeClass('icons');
//    $('#' + id).parent('div').addClass('validateicons');
//}

function ValidateFields(id, fieldType) {
    debugger;
    if (fieldType == "txticons") {
        if ($('#' + id).val() == '') {
            $('#' + id).parent('div').removeClass('icons');
            $('#' + id).parent('div').addClass('validateicons');
        }
        else if ($('#' + id).val() != '') {
            $('#' + id).parent('div').removeClass('validateicons');
            $('#' + id).parent('div').addClass('icons');
        }
    }
    else if (fieldType == "txticon") {
        if ($('#' + id).val() == '') {
            $('#' + id).parent('div').removeClass('icon');
            $('#' + id).parent('div').addClass('validateicon');
        }
        else if ($('#' + id).val() != '') {
            $('#' + id).parent('div').removeClass('validateicon');
            $('#' + id).parent('div').addClass('icon');
        }
    }
    else if (fieldType == "txt") {
        if ($('#' + id).val() == '') {
            $('#' + id).css({ "border-color": "red", "border-width": "1px", "border-style": "solid" });
        }
        else if ($('#' + id).val() != '') {
            $('#' + id).css({ "border-color": "#ccc", "border-width": "1px", "border-style": "solid" });
        }
    }
    else if (fieldType == "nddl") {
        if ($('#' + id).val() == 0) {
            $('#' + id).css({ "border-color": "red", "border-width": "1px", "border-style": "solid" });
        }
        else if ($('#' + id).val() != 0) {
            $('#' + id).css({ "border-color": "#ccc", "border-width": "1px", "border-style": "solid" });
        }
    }
    else if (fieldType == "ddl") {
        if ($('#' + id).val() == '' || $('#' + id).val() == 0 || $('#' + id).val() == null) {
            $('#select2-' + id + '-container').parent('span').removeClass('select2-selection select2-selection--single').addClass('select2-selection validateselect2-selection--single');
        }
        else {
            $('#select2-' + id + '-container').parent('span').removeClass('select2-selection validateselect2-selection--single').addClass('select2-selection select2-selection--single');
        }
    }
    else if (fieldType == "multipleddl") {
        if ($('#' + id).val() == ',' || $('#' + id).val() == 0 || $('#' + id).val() == null) {
            showMultiDropDownError(id);
        }
        else {
            AssignSuccessMultiDropDown(id);
        }
    }


}


function ClearValidation() {
    $('input').css("border-color", "grey");
    //$('.validateicons').addClass('icons').removeClass('validateicons');
    //$('.select2-selection validateselect2-selection--single').addClass('select2-selection select2-selection--single').removeClass('.select2-selection validateselect2-selection--single');
}


function ChangeMultiSelectHeader(id, value) {
    $('#' + id).next('div').find('button').attr('title', value);
    $('#' + id).next('div').find('button').find('span').html(value);
}


function Showpages() {
   
    var role = $('#hdnUserRoleID').val();
    if (role == 3) {
        
        //$('.MenuCapatilize').hide();
        //$('.Capatilize').hide();
        $('#ltrlDashboard').hide();
        $('#iddashboard').hide();
        $('#ltrlCompanySettings').hide();
        $('#idClient').hide();
        $('#idLocation').hide();
        $('#idBuilding').hide();
        $('#idDepartment').hide();
        $('#idComplianceScore').hide();
        $('#idDepartmentCategoryMapping').hide();
        $('#ltrlUsersSettings').hide();
        $('#idUsers').hide();
        $('#idRole').hide();
        $('#ltrlComplianceSettings').hide();
        $('#idComplainceCategory').hide();
        $('#idComplainceSubCategory').hide();
        $('#idComplianceDetails').hide();
        $('#iduserprofile').hide();
        $('#idlogout').hide();
        $('#idComplainceSubCategory').hide();
        $('#idComplianceDetails').hide();
        $('#idComplianceRepository').hide();
        $('#ltrlComplianceRepository').hide();
        
        $('#quesionpageid').show();
        $('#iduserprofile').hide();
        $('#idlogout').show();
    }
    else {
        //$('.MenuCapatilize').show();
        //$('.Capatilize').show();
        $('#quesionpageid').hide();
        $('#quesionpageid').show();
        $('#iduserprofile').show();
        $('#ltrlDashboard').show();
        $('#iddashboard').show();
        $('#ltrlCompanySettings').show();
        $('#idClient').show();
        $('#idLocation').show();
        $('#idBuilding').show();
        $('#idDepartment').show();
        $('#idComplianceScore').show();
        $('#idDepartmentCategoryMapping').show();
        $('#ltrlUsersSettings').show();
        $('#idUsers').show();
        $('#idRole').show();
        $('#ltrlComplianceSettings').show();
        $('#idComplainceCategory').show();
        $('#idComplainceSubCategory').show();
        $('#idComplianceDetails').show();
        $('#iduserprofile').show();
        $('#idlogout').show();
        $('#idComplainceSubCategory').show();
        $('#idComplianceDetails').show();
        $('#idComplianceRepository').show();
        $('#ltrlComplianceRepository').show();
                
    }
}


function ShowPagescompany() {
    debugger;
    var role = $('#hdnUserRoleID').val();
    if (role == 1) {
        $('#quesionpageid').hide();
    }
    else {
        $('#quesionpageid').show();
    }
}

function EmailValidate(ID)
{
    //var numericExpression = /^w.+@[a-zA-Z_-]+?.[a-zA-Z]{2,3}$/;
    //var numericExpression = "/^[w-.+]+@[a-zA-Z0-9.-]+.[a-zA-z0-9]{2,4}$/";
    var numericExpression=/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; 
    var elem = $('#'+ID).val();
    if (elem.match(numericExpression))
        return true;
    else
        $('#txtEmailID').css({ "border-color": "red", "border-width": "1px", "border-style": "solid" });
    return false;
}