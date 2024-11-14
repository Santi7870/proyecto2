// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function CrearAlerta() {

    alert("La compra se ha realizado exitosamente");


}
function crearAlertaComprar() {
    var respuesta = confirm("¿Estás seguro de que deseas comprar este producto?");

    if (respuesta) {
        // Si el usuario confirma, redirigir al controlador para la compra directa
        comprarDirecto();
    }
}



