function sendForm(e, id, data) {
    e.preventDefault();
    Swal.fire({
        title: `Estas seguro de eliminar ${data}?`,
        text: "¡No podrás revertir esto!",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: 'linear-gradient(90deg, #1CB5E0 0%, #000851 100%)',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            const formulario = document.getElementById(`formulario${id}`);
            formulario.submit();
        }
    })
}