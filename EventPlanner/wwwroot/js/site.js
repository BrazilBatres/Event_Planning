// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function LogOut() {
    Swal.fire({
        title: '¿Quieres cerrar sesión?',
        icon: 'question',
        confirmButtonText: 'Sí, quiero cerrar sesión',
        cancelButtonText: 'No, quiero permanecer en mi sesión',
        allowOutsideClick: true,
        showCancelButton: true
    }
    ).then((result) => {
        if (result.value) {
            var url = 'https://localhost:7158/Login/Logout';
            window.location = url;
        }
    });
}

