$(document).ready(function () {
    $(window).scroll(function () {
        var distanceFromTop = $(document).scrollTop();
        if (distanceFromTop >= $('#header').height()) {
            $('#sticky').addClass('fixed');
        }
        else {
            $('#sticky').removeClass('fixed');
        }
    });
});