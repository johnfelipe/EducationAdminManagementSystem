var colors = ["#7d95e5", "#fe9253", "#81f76f", "#eeef52"];
var n = 0;

$(".sparkle-module").mouseenter(
    function () {
        var color = colors[n];
        if (color != undefined)
        {
            $(this).css("background-color", color);
            n++;
        }
        else
        {
            n = 0;
            $(this).css("background-color", colors[n]);
            n++;
        }
    }
    ).mouseleave(
    function ()
    { $(this).css("background-color", "#eeeeee"); })