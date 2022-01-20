function clock() {
    var date = new Date();
    var time = [date.getHours(), date.getMinutes(), date.getSeconds()];
    var dayOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]
    var days = date.getDay();
    if (time[0] < 10) { time[0] = "0" + time[0]; }
    if (time[1] < 10) { time[1] = "0" + time[1]; }
    if (time[2] < 10) { time[2] = "0" + time[2]; }
    var current_time = [time[0], time[1], time[2]].join(':');
    $("#clock").html(current_time);
    $("#dayOfWeek").html(dayOfWeek[days]);
    setTimeout(clock, 1000);
}
clock();

