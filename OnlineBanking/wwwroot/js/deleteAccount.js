async function deleteAccount(event, number) {
    event.preventDefault();
    Swal.fire({
        title: "Вы действительно хотите удалить этот счёт?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#042b76",
        cancelButtonColor: "#800707",
        confirmButtonText: "Да, удалить!",
        cancelButtonText: "Отмена",
        background: '#fff',
        iconColor: 'red',
        color: 'black',
    }).then(async (result) => {
        event.preventDefault();

        if (result.isConfirmed) {
            var form = document.getElementById("deleteAccountForm-" + number);
            var id = document.getElementById("id-" + number).value;
            var balance = document.getElementById("balance-" + number).value.replace(/,/g, '.');

            var deleteFormData = {
                Id: parseInt(id),
                BalanceAmount: parseFloat(balance)
            };

            const response = await fetch('/Account/DeleteAccountById', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(deleteFormData)
            });

            const responseBody = await response.json();
            Swal.fire({
                title: 'Уведомление',
                text: response.ok ? 'Ваш счёт удалён' : responseBody.message,
                icon: response.ok ? 'success' : 'error',
                confirmButtonColor: "#042b76",
                background: '#fff',
                color: 'black',
            });
        }
    });
}