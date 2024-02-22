function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modalId = parameters.modalId;
    const modalTitle = parameters.modalTitle
    const modal = $('#' + modalId);

    if (url === undefined) {
        alert('Извиняемся.. Возникла ошибка!')
        return;
    }

    var ajaxParams = {
        type: 'GET',
        url: url,
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.find(".modal-title").html(modalTitle);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    };

    if (id !== undefined) {
        ajaxParams.data = { "id": id };
    }

    $.ajax(ajaxParams);
};