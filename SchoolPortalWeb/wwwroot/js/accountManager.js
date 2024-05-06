function deleteUser(id) {
    if (confirm("Are you sure you want to delete this User: " + id + "?\n")) {
        $.ajax({
            url: '/admin/AccountManager/DeleteUser?id=' + id, type: 'post',
            success: function (response) {
                alert(response)
                location.reload(true)
            }
        });
    }
}

function clearInputs() {
    alert("response")
}