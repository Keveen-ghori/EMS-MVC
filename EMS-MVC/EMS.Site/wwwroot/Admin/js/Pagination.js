
function loadPage(pageNumber) {
    // Make AJAX call to the controller action method
    $.ajax({
        url: '/Home/Employee',
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
