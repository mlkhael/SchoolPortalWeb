$(document).ready(function () {
    getList();
});


function getList() {
    $.ajax({
        url: '/admin/calendarmanagerpublic/getevents', type: 'get', dataType: 'json', contentType: 'application/json;charset=utf-8',
        success: function (response) {

            var eventList = [];
            for (let i = 0; i < response.length; i++) {

                eventList.push({
                    "id": response[i].id,
                    "title": response[i].title,
                    "start": response[i].startDate,
                    "end": response[i].endDate,
                    "allDay": response[i].isAllDay,
                })
            }

            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                initialDate: Date.now(),
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                events: eventList
            });

            calendar.render();

        }
    })
}
