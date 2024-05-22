function doActionWithConfirmation(event, parameters) {
    event.preventDefault();
    formId = parameters.formId;
    Swal.fire({
        title: "Уведомление",
        title: "Вы действительно хотите " + parameters.confirmationTitle,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#042b76",
        cancelButtonColor: "#800707",
        confirmButtonText: "Да!",
        cancelButtonText: "Отмена",
        background: 'white',
        iconColor: 'red',
        color: 'black',
    }).then(async (result) => {
        if (result.isConfirmed) {
            const data = $('#' + formId).serialize()
            $.ajax({
                type: 'POST',
                url: parameters.url,
                data: data,
                success: function (response) {
                    Swal.fire({
                        title: 'Уведомление!',
                        text: response,
                        icon: 'success',
                        confirmButtonColor: "#0d6efd",
                        cancelButtonColor: "#0d6efd",
                        confirmButtonText: 'Окей',
                        background: '#fff',
                        color: 'black'
                    }).then(() => {
                        location.reload()
                    });
                },
                error: function (response) {
                    Swal.fire({
                        title: 'Уведомление!',
                        text: response.responseJSON.errorMessage,
                        icon: 'error',
                        confirmButtonColor: "#0d6efd",
                        confirmButtonText: 'Окей',
                        background: '#fff',
                        color: 'black'
                    }).then(() => {
                        location.reload()
                    });
                }
            })
        }
    })
}