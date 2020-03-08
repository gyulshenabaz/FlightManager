'use strict';

$(function () {

    $.fn.slideDropdownUp = function () {
        $(this).fadeIn().css('transform', 'translateY(0)');
        return this;
    };
    $.fn.slideDropdownDown = function (movementAnimation) {

        if (movementAnimation) {
            $(this).fadeOut().css('transform', 'translateY(30px)');
        } else {
            $(this).hide().css('transform', 'translateY(30px)');
        }
        return this;
    };

    $('.navbar .dropdown').on('show.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').first().slideDropdownUp();
    });
    $('.navbar .dropdown').on('hide.bs.dropdown', function (e) {

        var movementAnimation = true;

        // if on mobile or navigation to another page
        if ($(window).width() < 992 || (e.clickEvent && e.clickEvent.target.tagName.toLowerCase() === 'a')) {
            movementAnimation = false;
        }

        $(this).find('.dropdown-menu').first().slideDropdownDown(movementAnimation);
    });

});