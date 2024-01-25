function showConfirmation() {
    document.getElementById('deleteConfirmation').style.display = 'block';
}

function closeConfirmation() {
    document.getElementById('deleteConfirmation').style.display = 'none';
}

function deleteHome(id) {
    fetch('/HomeDetail/' + id, {
        method: 'POST'
    })
        .then(response => {
            window.location.href = '/Index';
        })
        .catch(error => {
            // This catch block will handle network errors and log them to the console
            console.error('Fetch error:', error);
        });
}
