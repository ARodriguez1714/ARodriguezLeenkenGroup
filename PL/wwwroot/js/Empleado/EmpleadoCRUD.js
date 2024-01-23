$(document).ready(function () {
    GetAll();
    EstadoGetAll();
    $('#tblEmpleado').DataTable();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:35149/api/Empleado/getall',
        success: function (result) //Ok 200
        {
            $('#tblEmpleado tbody').empty();
            $.each(result.objects, function (i, empleado) {
                var fila =
                    '<tr>'
                    + '<td class="text-center"><button class="btn btn-warning" onclick="GetById(' + empleado.idEmpleado + ')"><i class="bi bi-pencil"></i></button></td>'
                    + '<td class="text-center">' + empleado.idEmpleado + '</td>'
                    + '<td class="text-center">' + empleado.numeroNomina + '</td>'
                    + '<td class="text-center">' + empleado.nombre + '</td>'
                    + '<td class="text-center">' + empleado.apellidoPaterno + '</td>'
                    + '<td class="text-center">' + empleado.apellidoMaterno + '</td>'
                    + '<td class="text-center">' + empleado.estado.idEstado + '</td>'
                    + '<td class="text-center">' + empleado.estado.nombre + '</td>'
                    + '<td class="text-center"><button class="btn btn-danger" onclick="Delete(' + empleado.idEmpleado + ')"><i class="bi bi-trash3"></i></button></td>'
                    + '</tr>';
                $('#tblEmpleado tbody').append(fila);
            });
        },
        error: function (result) {
            alert('Error en la consulta.');
        }
    });
};

function EstadoGetAll() {
    $("#ddlEstados").empty();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:35149/api/Estado/getall',
        success: function (result) {
            $('#ddlEstados').append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, estado) {
                var select =
                    '<option value="' + estado.idEstado + '">' + estado.nombre + '</option>';
                $("#ddlEstados").append(select);
            });
        }
    });
}

function Add(empleado) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'POST',
        url: 'http://localhost:35149/api/Empleado/add',
        dataType: 'json',
        data: JSON.stringify(empleado),
        success: function (result) {
            $('#modalDoneAdd').modal('show');
            $('#modal').modal('hide');
        },
        error: function (result) {
            alert('Error en la consulta. Estatus: ' + result.responseJSON.status + ' Error: ' + result.responseJSON.title);
        }
    });
};

function GetById(idEmpleado) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:35149/api/Empleado/getbyid/' + idEmpleado,
        success: function (result) {
            $('#txtIdEmpleado').val(result.object.idEmpleado);
            $('#txtNumeroNomina').val(result.object.numeroNomina);
            $('#txtNombre').val(result.object.nombre);
            $('#txtApellidoP').val(result.object.apellidoPaterno);
            $('#txtApellidoM').val(result.object.apellidoMaterno);
            $('#ddlEstados').val(result.object.estado.idEstado);
            $('#modal').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta. Estatus: ' + result.responseJSON.status + ' Error: ' + result.responseJSON.title);
        }
    });
};

function Update(empleado) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: 'PUT',
        url: 'http://localhost:35149/api/Empleado/update',
        dataType: 'json',
        data: JSON.stringify(empleado),
        success: function (result) {
            $('#modalDoneUpdate').modal('show');
            $('#modal').modal('hide');
        },
        error: function (result) {
            alert('Error en la consulta. Estatus: ' + result.responseJSON.status + ' Error: ' + result.responseJSON.title);
        }
    });
}

function Delete(idEmpleado) {
    if (confirm('¿Estas seguro de eliminar al empleado?')) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:35149/api/Empleado/delete/' + idEmpleado,
            success: function (result) {
                $('#modalDelete').modal('show');
            },
            error: function (result) {
                alert('Error en la consulta. Estatus: ' + result.status + ' Error: ' + result.statusText);
            }
        });
    };
};


function closeModal() {
    $("#txtIdEmpleado").val("");
    $("#txtNumeroNomina").val("");
    $("#txtNombre").val("");
    $("#txtApellidoP").val("");
    $("#txtApellidoM").val("");
    $('#ddlEstados').val(0);
};

function GetModal() {
    $('#modal').modal('show');
};

function Guardar() {
    var empleado =
    {
        "idEmpleado": Number($('#txtIdEmpleado').val()),
        "numeroNomina": $('#txtNumeroNomina').val(),
        "nombre": $('#txtNombre').val(),
        "apellidoPaterno": $('#txtApellidoP').val(),
        "apellidoMaterno": $('#txtApellidoM').val(),
        "estado": {
            "idEstado": Number($('#ddlEstados').val()),
            "nombre": "string",
            "estados": [
                "string"
            ]
        },
        "empleados": [
            "string"
        ]
    }

    if (empleado.idEmpleado == 0) {
        Add(empleado);
    } else {
        Update(empleado);
    }
}