async function makeTransaction(event) {
    event.preventDefault();

    var makeTransactionFormData = {
        SelectedUserAccountId: parseInt(document.getElementById("SelectedUserAccountId").value),
        SelectedPaymentMethodId: parseInt(document.getElementById("SelectedPaymentMethodId").value),
        RecipientCardNumber: document.getElementById("RecipientCardNumber").value,
        MoneyAmount: parseFloat(document.getElementById("MoneyAmount").value.replace(/,/g, '.')),
    };

    const response = await fetch('/Transaction/CreateTransaction', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(makeTransactionFormData)
    });

    const responseBody = await response.json();
    Swal.fire({
        title: 'Уведомление',
        text: responseBody.message,
        icon: response.ok ? 'success' : 'error',
        confirmButtonColor: "#042b76",
        background: '#fff',
        color: 'black',
    }).then(() => {
        location.reload
    });
  
}