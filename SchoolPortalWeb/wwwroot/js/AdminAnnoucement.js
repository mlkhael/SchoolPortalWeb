function deletePost(id) {
    if (confirm("Are you sure you want to delete Announcement #" + id + "?")) {
        $.ajax({
            url: 'Announcement/DeletePost?id=' + id, type: 'post',
            success: function (response) {
                alert(response)
                location.reload(true)
            }
        });
    }
}