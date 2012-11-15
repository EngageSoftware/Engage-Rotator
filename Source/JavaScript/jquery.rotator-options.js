/*global jQuery */
(function ($) {
    'use strict';
    
    $.fn.toggleUrlTracking = function () {
        var isSectionOpen = function ($section) {
            return $section.find('input[type="hidden"]').val() === 'true';
        };
        
        return this.each(function () {
            var $enclosingSection = $(this),
                $toggleLink = $('.view-url-tracking', $enclosingSection),
                $urlTrackingSection = $('.urlTracking', $enclosingSection),
                $isOpenedHiddenField = $enclosingSection.find('input[type="hidden"]');

            $urlTrackingSection.filter(function () {
                return !isSectionOpen($enclosingSection);
            }).hide();

            $toggleLink
                .filter(function () {
                    return !isSectionOpen($enclosingSection);
                }).addClass('expand-link')
                .end().filter(function () {
                    return isSectionOpen($enclosingSection);
                }).addClass('collapse-link')
                .end().click(function (event) {
                    event.preventDefault();
                    $urlTrackingSection.slideToggle();
                    $toggleLink.toggleClass('expand-link collapse-link');
                    $isOpenedHiddenField.val($toggleLink.hasClass('collapse-link'));
                });
        });
    };
}(jQuery));