async function linkCard(event, number) {
    event.preventDefault();
    Swal.fire({
        title: "Уведомление",
        text: "Вы действительно хотите создать карту для этого счёта?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#042b76",
        cancelButtonColor: "#800707",
        confirmButtonText: "Да, создать!",
        cancelButtonText: "Отмена",
        background: 'white',
        iconColor: 'red',
        color: 'black',
    }).then(async (result) => {
        event.preventDefault();

        if (result.isConfirmed) {
            var form = document.getElementById("linkCardForm-" + number);
            var id = parseInt(document.getElementById("linkCardId-" + number).value);

            const response = await fetch('/Card/CreateCardForAccount', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(id)
            });

            const responseBody = await response.json();

            Swal.fire({
                title: 'Уведомление',
                text: responseBody.message,
                icon: response.ok ? 'success' : 'error',
                background: 'white',
                confirmButtonColor: "#042b76",
                color: 'black',
            }).then(() => {
                location.reload()
            });
        }
    });
}