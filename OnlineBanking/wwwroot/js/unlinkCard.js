async function linkCard(event, number) {
    event.preventDefault();
    Swal.fire({
        title: "Уведомление",
        text: "Вы действительно хотите удалить карту для этого счёта?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#042b76",
        cancelButtonColor: "#800707",
        confirmButtonText: "Да, удалить!",
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

            location.reload()
        }
    });
}