function iniciar() {
    nombre1 = document.getElementById("NombreUsuario");
    nombre2 = document.getElementById("Password");
    nombre1.addEventListener("input", validacion, false);
    nombre2.addEventListener("input", validacion, false);

    if (validacion()) {        
        var urlLgn = domainName + '/Usuario/Login';
        var dataObject = { CtaUsuario: $("#NombreUsuario").val(), Password: $("#Password").val() };
        $.ajax({
            url: urlLgn,
            type: "POST",
            data: dataObject,
            datatype: "json",
            success: function (result) {
                if (result.toString() == "Exito") {
                    bootbox.alert({
                        message: "Bienvenido al sistema",
                        size: 'small',
                        callback: function () {
                            window.location.href = '/home/index/';
                        }
                    })
                }
                else {
                    $("#NombreUsuario").val("");
                    $("#Password").val("");
                    $("#NombreUsuario").focus();
                    $("#mensaje").html('<div class="failed">' + result + '</div>');
                }
            },
            error: function (result) {
                $("#NombreUsuario").val("");
                $("#Password").val("");
                $("#NombreUsuario").focus();
                $("#mensaje").html('<div class="failed"> Error!! </div>');
            }
        });
    };

}
function validacion() {

    if (nombre1.value == '') {
        nombre1.setCustomValidity('Ingrese nombre de usuario');
        nombre1.style.background = '#FFDDDD';
        return false;
    } else {
        nombre1.setCustomValidity('');
        nombre1.style.background = '#FFFFFF';
    }

    if (nombre2.value == '') {
        nombre2.setCustomValidity('Ingrese la contraseña');
        nombre2.style.background = '#FFDDDD';
        return false;
    } else {
        nombre2.setCustomValidity('');
        nombre2.style.background = '#FFFFFF';
    }
    return true;
}