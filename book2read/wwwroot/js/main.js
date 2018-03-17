$(document).ready(function() {
    $.ajax({
        url: "/home/readbook2"
    })
        .done(function(data) {          
            for (var i = 0; i < data.length; i++) {
                var a = $('<a>');
                a.attr('href', data[i].url);
                a.text(data[i].con);
                $('ul#list').append(a);
                $('ul#list').append("<br>");
            }
        });
});