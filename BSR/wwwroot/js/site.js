function showConfirmation() {
    document.getElementById('deleteConfirmation').style.display = 'block';
}

function closeModal() {
    document.getElementById('deleteConfirmation').style.display = 'none';
}

function deleteHome(id) {
    fetch('/HomeDetail/' + id, {
        method: 'POST'
    })
        .then(response => {
             window.location.href = '/Index';
        })
}
