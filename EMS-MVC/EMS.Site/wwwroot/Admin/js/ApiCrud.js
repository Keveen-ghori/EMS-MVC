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

function CreateEmpApi() {
    $.ajax({
        url: '/Admin/Home/CreateEmpByApiPartial',
        type: 'GET',
        success: function (data) {
            debugger;
            $('#addEmp').html(data);
            $("#AddEmpModal").modal("show");
        },
        error: function (error, xhr, status) {
            console.log(error, xhr.status);
        }
    });
}

function UpdateEmpApi(event, EmployeeId) {

    event.preventDefault();

    // Serialize the form data
    let formData = $('#UpdateEmpApi-' + EmployeeId).serialize();

    // Make an AJAX request
    $.ajax({
        url: '/Admin/Home/SaveEmployeesByApi',
        type: 'PUT',
        data: formData,
        success: function (response) {
            console.log(response);
            alert('Employee saved successfully!');

            $('#EditModel').html(response);
            $("#EditEmpApi-" + EmployeeId).modal("hide");
            console.log(response);
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

function AddNewEmpApi(event) {

    event.preventDefault();

    // Serialize the form data
    let formData = $('#CreateEmployeeModalForm').serialize();

    // Make an AJAX request
    $.ajax({
        url: '/Admin/Home/CreateEmpByApi',
        type: 'POST',
        data: formData,
        success: function (response) {
            debugger;
            console.log(response);
            alert('Employee saved successfully!');
            $("#AddEmpModal").modal("hide");

        },
        error: function (xhr, status, error) {
            debugger;
            console.log(error);
            alert('An error occurred while saving the employee.');
        }
    });
}

