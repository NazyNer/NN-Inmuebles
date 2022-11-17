function eliminarCasa(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar esta casa?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Casa/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'La casa se elimino de la lista',
                'success'
            );
        }
        else {
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel,
                swalWithBootstrapButtons.fire(
                    'Cancelado',
                    'Dejaste la casa en la lista',
                    'error'
                )
        }
    })
}

function eliminarCliente(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Estas segur@?',
        text: "Está seguro de eliminar este cliente?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminar!',
        cancelButtonText: 'No, cancelar!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "../../Cliente/DeleteConfirmed/" + id;
            swalWithBootstrapButtons.fire(
                'Eliminado!',
                'El cliente se elimino de la lista',
                'success'
            );
        }
        else {
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel,
                swalWithBootstrapButtons.fire(
                    'Cancelado',
                    'Dejaste el cliente en la lista',
                    'error'
                )
        }
    })
}