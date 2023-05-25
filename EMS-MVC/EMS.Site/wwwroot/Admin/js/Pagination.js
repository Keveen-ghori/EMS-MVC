

function loadPage(pageNumber) {
    debugger
    $.ajax({
        url: '/Admin/Home/EmployeeFilter',
        type: 'GET',
        data: { PageNumber: pageNumber},
        success: function (response) {
            $('#EmpListsTable').html(response);
        },
        error: function (xhr, status, error) {
            // Handle the error, if any
        }
    });
}


