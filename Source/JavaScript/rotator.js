/*globals jQuery */
(function ($) {
    'use strict';
    
    $(function () {
        $.fn.extend({
            getParentRotators: function () {
                return this.closest('.engage-rotator-container').find('.rotate-wrap');
            }
        });

        $('.rotator-pause').click(function () {
            $(this)
                .addClass('rotator-pause-on')
                .getParentRotators().cycle('pause');

            $('.rotator-play')
                .removeClass('rotator-play-on');
        });

        $('.rotator-play').click(function () {
            $(this)
                .addClass('rotator-play-on')
                .getParentRotators().cycle('resume');

            $('.rotator-pause')
                .removeClass('rotator-pause-on');
        });

        // *= is contains
        $('div[class*="pager-item-"]').click(function () {
            var cssClasses = $(this).attr('class'),

            // the item has a CSS class like "pager-item-15" where 15 is the index it's associated with
            // index is found using a regular expression: word-boundary, pager-item-, one or more digits (captured group), word-boundary
               indexMatches = /\bpager-item-(\d+)\b/.exec(cssClasses),
               slideIndex;

            // indexMatches is an array of matches, 
            // the first item being all the text matched by the regular expression, 
            // the next being the capture group (the digits in parenthesis)
            if (indexMatches && indexMatches.length === 2) {
                slideIndex = parseInt(indexMatches[1], 10);
                $(this).getParentRotators().cycle(slideIndex);
            }
        });

        $('.total-slide-count').each(function (index, elem) {
            var slideCount = $(elem).getParentRotators().first().children().length;
            $(elem).html(slideCount);
        });
    });

    $.fn.cycle.defaults.after = function (nextElement, lastElement, opts) {
        var $currentContainer = opts.$cont.closest('.engage-rotator-container');
        $currentContainer.find('.current-slide-index').html(opts.currSlide + 1);
        $currentContainer.find('div.pager-item-on').removeClass('pager-item-on');
        $currentContainer.find('div.pager-item-' + opts.currSlide).addClass('pager-item-on');
    };
}(jQuery));