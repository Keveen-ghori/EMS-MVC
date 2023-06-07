function loadPartialView(employeeId) {
    $.ajax({
        url: '/Admin/Home/GetEmployeesByIdApi',
        type: 'GET',
        data: { EmployeeId: employeeId },
        success: function (data) {
            debugger;
            $('#EditModel').html(data);
            $("#EditEmpApi-" + employeeId).modal("show");
        },
        error: function (error, xhr, status) {
            console.log(error, shr.status);
        }
    });
}

function UpdateEmpApi(EmployeeId) {
    // Serialize the form data
    let formData = $('#UpdateEmpApi-' + EmployeeId).serialize();

    // Make an AJAX request
    $.ajax({
        url: '/Admin/Home/SaveEmployeesByApi',
        type: 'POST',
        data: formData,
        success: function (response) {
            console.log(response);
            if (response.success) {
                debugger;
                alert('Employee saved successfully!');
            } else {
                alert('Failed to save employee.');
                debugger;
                $('#EditModel').html(response);
                $("#EditEmpApi-" + EmployeeId).modal("show");

            }
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert('An error occurred while saving the employee.');
        }
    });
}


function DeleteEmpApi(EmployeeId) {
    $.ajax({
        url: '/Admin/Home/DeleteEmpByIdApi',
        type: 'DELETE',
        data: { EmployeeId: EmployeeId },
        success: function (data) {
            debugger
            if (data.success) {
                alert('Employee deleted successfully!');
                $("#DeleteEmpApi-" + EmployeeId).modal("hide");
                location.reload();
            }
            else {
                alert('Failed to delete employee.');
                location.reload();
            }
        },
        error: function (error, xhr, status) {
            debugger
            console.log(xhr.status);
        }
    });
}