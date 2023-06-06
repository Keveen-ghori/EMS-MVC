function loadPartialView(employeeId) {
    $.ajax({
        url: '/Home/GetEmployeesByApi',
        type: 'GET',
        data: { EmployeeId: employeeId },
        success: function (partialViewHtml) {
            $('#EditEmpApi-' + employeeId).find('.modal-body').html(partialViewHtml);
            $('#EditEmpApi-' + employeeId).modal('show');
        },
        error: function () {
            console.log(error);
        }
    });
}
