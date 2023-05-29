function loadPage(pageNumber) {
    let UserName = $('#Username').val();
    let Email = $('#Email').val();
    let DOB = $('#DOB').val();
    let Gender = $('#Gender').val();
    let Lock = $('#Status').val();
    debugger
    let data = {
        UserName: UserName,
        Email: Email,
        DOB: DOB,
        Gender: Gender,
        Lock: Lock,
        PageNumber: pageNumber
    };
    debugger
    $.ajax({
        url: '/Admin/Home/EmployeeFilter',
        type: 'GET',
        data: data,
        success: function (response) {
            $('#EmpListsTable').html(response);
        },
        error: function (xhr, status, error) {
            // Handle the error, if any
        }
    });
}

$('#Search').on('click', function () {
    let UserName = $('#Username').val();
    let Email = $('#Email').val();
    let DOB = $('#DOB').val();
    let Gender = $('#Gender').val();
    let Lock = $('#Status').val();
    debugger
    let data = {
        UserName: UserName,
        Email: Email,
        DOB: DOB,
        Gender: Gender,
        Lock: Lock
    };
    $.ajax({
        url: '/Admin/Home/EmployeeFilter',
        type: 'GET',
        data: data,
        success: function (response) {
            $('#EmpListsTable').html(response);
        },
        error: function (xhr, status, error) {
            // Handle the error, if any
        }
    });
})

$("#Clear").on('click', function () {
    $('#Username').val('');
    $('#Email').val('');
    $('#DOB').val('');
    $('#Gender').val('');
    $('#Status').val('');
});

let messages = {
    EmpDeletedSuccessfully: "Employee deleted successfully.",
    EmpnotExists: "Employee does not exists.",
    EmpLocked: "Employee locked successfully.",
    EmpUnlocked: "Employee unlocked successfully.",
    ConfigSaved: "Configuration Updated Successfully."
}
function DeleteEmp(EmployeeId, currentPage) {
    let DeleteEmpId = `#Delete-${EmployeeId}`;
    $.ajax({
        url: '/Admin/Home/DeleteEmp',
        type: 'POST',
        data: { EmployeeId: EmployeeId, PageNumber: currentPage },
        success: function (response) {
            $(DeleteEmpId).modal('hide');
            $('.modal-backdrop').remove();
/*            $('#' + EmployeeId).remove();*/
            toastr.options.positionClass = 'toast-top-left';
            toastr.success(messages.EmpDeletedSuccessfully);

        },
        error: function (xhr, status, error) {
            if (xhr.status === 404) {
                console.log("Employee not found");
                toastr.error(messages.EmpnotExists);
            } else {
                console.log("AJAX error:", status, error);
                toastr.error("An error occurred. Please try again.");
            }
        }
    });
}

function Lock(EmployeeId, lockStatus, currentPage) {
    let EmpId = `#Lock-${EmployeeId}`;
    $.ajax({
        url: '/Admin/Home/LockEmp',
        type: 'POST',
        data: { EmployeeId: EmployeeId, status: lockStatus, PageNumber: currentPage },
        success: function (response) {

            $(EmpId).modal('hide');
            $('.modal-backdrop').remove();
            $('#EmpListsTable').html(response);
            debugger
            if (!lockStatus) {
                toastr.options.positionClass = 'toast-top-left';
                toastr.success(messages.EmpUnlocked);
            }
            else {
                toastr.options.positionClass = 'toast-top-left';
                toastr.success(messages.EmpLocked);
            }
        },
        error: function (xhr, status, error) {
            if (xhr.status === 404) {
                console.log("Employee not found");
                toastr.error(messages.EmpnotExists);
            } else {
                console.log("AJAX error:", status, error);
                toastr.error("An error occurred. Please try again.");
            }
        }
    });
}

$('#updateButton').on('click', function () {
    let totalAttempts = $("#Total_Attemps").val();
    let ExpDays = $("#Exp_Days").val();

    $.ajax({
        url: '/Admin/Home/Configuration',
        type: 'POST',
        data: { Total_attemps: totalAttempts, Exp_days: ExpDays },
        success: function () {
            debugger
            toastr.options.positionClass = 'toast-top-left';
            toastr.success(messages.ConfigSaved);
        },
        error: function (xhr, status, error) {
            console.log("error");
        }
    });
});