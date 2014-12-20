//;(function ($, window, undefined){
//  'use strict';

//  $.fn.foundationAccordion = function (options) {

//    // DRY up the logic used to determine if the event logic should execute.
//    var hasHover = function(accordion) {
//      return accordion.hasClass('hover') && !Modernizr.touch
//    };

//    $(document).on('mouseenter', '.MenuFooter .MenuFooterTitleShow', function () {
//        var p = $(this).parent();

//        if (hasHover(p)) {
//          var flyout = $(this).children('.MenuItem').first();

//          $('.MenuItem', p).not(flyout).hide().parent('.MenuFooterTitleShow').removeClass('active');
//          flyout.show(0, function () {
//            flyout.parent('.MenuFooterTitleShow').addClass('active');
//          });
//        }
//      }
//    );

//    $(document).on('click.fndtn', '.MenuFooter .MenuFooterTitleShow .MenuItemTitle', function () {
//        var li = $(this).closest('.MenuFooterTitleShow'),
//            p = li.parent(),
//            windowsize = $(document).width();

//            if(windowsize < 768)
//            {
//                if(!hasHover(p)) {
//                  var flyout = li.children('.MenuItem').first();
//          
//                  if (li.hasClass('active')) {
//                    p.find('.MenuFooterTitleShow').removeClass('active').end().find('.MenuItem').hide();
//                  } else {
//                    $('.MenuItem', p).not(flyout).hide().parent('.MenuFooterTitleShow').removeClass('active');
//                    flyout.show(0, function () {
//                      flyout.parent('.MenuFooterTitleShow').addClass('active');
//                    });
//                  }
//                }
//            }

//      });

//  };

//})( jQuery, this );

