jQuery(function($) {
    $.fn.extend({
        getParentRotator: function() {
            return this.parents('.engage-rotator-container').find('.rotate-wrap');
        }
    });

    $('.rotator-pause').click(function() {
        $(this)
            .addClass('rotator-pause-on')
            .getParentRotator().cycle('pause');

        $('.rotator-play')
            .removeClass('rotator-play-on');
    });

    $('.rotator-play').click(function() {
        $(this)
            .addClass('rotator-play-on')
            .getParentRotator().cycle('resume');

        $('rotator-pause')
            .removeClass('rotator-pause-on');
    });

    // *= is contains
    $('div[class*="pager-item-"]').click(function() {
        var cssClasses = $(this).attr('class');

        // the item has a CSS class like "pager-item-15" where 15 is the index it's associated with
        // index is found using a regular expression: word-boundary, pager-item-, one or more digits (captured group), word-boundary
        var indexMatches = /\bpager-item-(\d+)\b/.exec(cssClasses);

        // indexMatches is an array of matches, 
        // the first item being all the text matched by the regular expression, 
        // the next being the capture group (the digits in parenthesis)
        if (indexMatches && indexMatches.length === 2) {
            var contentItemIndex = parseInt(indexMatches[1], 10);
            $(this).getParentRotator().cycle(contentItemIndex);
        }
    });
});