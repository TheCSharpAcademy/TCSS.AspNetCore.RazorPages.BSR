function showConfirmation() {
    document.getElementById('deleteConfirmation').style.display = 'block';
}

function closeConfirmation() {
    document.getElementById('deleteConfirmation').style.display = 'none';
}

function deleteHome(id) {
    fetch(`/Homes/Delete/${id}`, {
        method: 'POST'
    })
        .then(response => {       
                window.location.href = '/Homes/Index';
        })
}
