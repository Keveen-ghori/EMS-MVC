

function loadPage(pageNumber) {
    let UserName = $('#Username').val();
    let Email = $('#Email').val();
    let DOB = $('#DOB').val();
    let Gender = $('#Gender').val();
    let IsLocked = $('#Status').val();
    debugger
    let data = {
        UserName: UserName,
        Email: Email,
        DOB: DOB,
        Gender: Gender,
        IsLocked: IsLocked,
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
    let IsLocked = $('#Status').val();
    debugger
    let data = {
        UserName: UserName,
        Email: Email,
        DOB: DOB,
        Gender: Gender,
        IsLocked: IsLocked
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