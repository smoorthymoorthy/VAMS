//$(document).ready(function () {
//    clearmsg();
//});

function validate() {
    debugger;
    clearmsg();
    ValidateFields('txtUserName', 'txticons');
    ValidateFields('txtPassword', 'txticons');

    if ($("#txtUserName").val() == '') {
        //ShowErrorMsg("Enter your UserName");
        return;
    }
    if ($("#txtPassword").val() == '') {
        //ShowErrorMsg("Enter your Password");
        return;
    }

   $.ajax({
        type: "GET",
        url: baseUrl + "Api/Account/CheckLogin",
        data: { "UserName": $("#txtUserName").val(), "Password": $("#txtPassword").val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //type: "POST",
        //url: "../Account/Logindirect?UserName=" + $("#txtUserName").val() + "&Password=" + $("#txtPassword").val(),
        //url:"../home/loginredirect",
       success: function (result) {
           debugger;
           if (result.result != "") {
              
               window.location.href ="/Home/Dashboard";
            }
            else {
                errmsg("Invalid Username and Password");
              
            }
       },
       error: function (result) {
           debugger;
           alert('Error occurs!');
       },
       failure: function (result)
       {
           debugger;
            var url = "/Account/Login";
            window.location.href = url;
        }
    });
}

function clearmsg() {

    $('#lblError').html("");
    $('#divErrormsg').hide();

    $('#lblErrorCommon').html("");
    $('#divErrormsgCommon').hide();

    $('#lblSuccess').html("");
    $('#divSuccessmsg').hide();
}

//function successmsg(msg) {

//    clearmsg();
//    $('#lblSuccess').html(msg);
//    $('#divSuccessmsg').show();

//}

function errmsg(msg) {

    clearmsg();
    $('#lblError').html(msg);
    $('#divErrormsg').show();
}