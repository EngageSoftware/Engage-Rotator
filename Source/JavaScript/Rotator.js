jQuery(function($) {
    $('.rotator-pause').click(function() {
        $(this)
            .addClass('rotator-pause-on')
            .parents('.engage-rotator-container').find('.rotate-wrap').cycle('pause');

        $('.rotator-play')
            .removeClass('rotator-play-on');
    });

    $('.rotator-play').click(function() {
        $(this)
            .addClass('rotator-play-on')
            .parents('.engage-rotator-container').find('.rotate-wrap').cycle('resume');

        $('rotator-pause')
            .removeClass('rotator-pause-on');
    });
});