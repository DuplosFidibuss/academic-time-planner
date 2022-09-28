export function showModalById(modalId){
    showModal(getModalById(modalId))
}

export function hideModalById(modalId){
    hideModal(getModalById(modalId))
}

export function getModalById(modalId){
    let myModalEl = document.querySelector('#' + modalId)
    let modal = bootstrap.Modal.getOrCreateInstance(myModalEl) // Returns a Bootstrap modal instance
    return modal;
}

export function showModal(modal){
    modal.show();
}

export function hideModal(modal){
    modal.hide();
}