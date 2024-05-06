function deleteEvent(id) {
    if (confirm("Are you sure you want to delete Announcement #" + id + "?")) {
        $.ajax({
            url: 'admin/CalendarManager/DeleteEvent?id=' + id, type: 'post',
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