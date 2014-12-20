/*
 * 	Easy Zoom 1.0 - jQuery plugin
 *	written by Alen Grakalic	
 *	http://cssglobe.com/post/9711/jquery-plugin-easy-image-zoom
 *
 *	Copyright (c) 2011 Alen Grakalic (http://cssglobe.com)
 *	Dual licensed under the MIT (MIT-LICENSE.txt)
 *	and GPL (GPL-LICENSE.txt) licenses.
 *
 *	Built for jQuery library
 *	http://jquery.com
 *
 */
 
 /*
 
 Required markup sample
 
 <a href="large.jpg"><img src="small.jpg" alt=""></a>
 
 */

(function ($) {
    if (!/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        $.fn.easyZoom = function (options, zoomID, controlID) {

            var defaults = {
                id: zoomID,
                parent: 'body',
                append: true,
                preload: 'Loading...',
                error: 'There has been a problem with loading the image.'
            };
            var lense = '#' + controlID + '_len';
            $(lense).css({ display: 'none' });
            var obj;
            var img = new Image();
            var loaded = false;
            var found = true;
            var timeout;
            var w1, w2, h1, h2, rw, rh;
            var len_width, len_height;
            var over = false;
            var options = $.extend(defaults, options);

            this.each(function () {

                obj = this;
                // works only for anchors
                var tagName = this.tagName.toLowerCase();
                if (tagName == 'a') {

                    var href = $(this).attr('href');
                    img.src = href + '?' + (new Date()).getTime() + ' =' + (new Date()).getTime();
                    $(img).error(function () { found = false; })
                    img.onload = function () {
                        loaded = true;
                        img.onload = function () { };
                    };

                    $(this)
                        .css('cursor', 'cursor')
                        .click(function (e) { e.preventDefault(); })
                        .mouseover(function (e) { start(e, lense, zoomID); })
                        .mouseout(function () { hide(lense); })
                        .mousemove(function (e) { move(e, lense, zoomID); })
                };

            });

            function start(e) {
                hide();
                var zoom = $('<div id="' + options.id + '">' + options.preload + '</div>');

                if (options.append) { zoom.appendTo(options.parent) } else { zoom.prependTo(options.parent) };
                if (!found) {
                    error();
                } else {
                    if (loaded) {
                        show(e, lense, zoomID);
                    } else {
                        loop(e, lense, zoomID);
                    };
                };
            };

            function loop(e, lense, zoomID) {
                if (loaded) {
                    show(e, lense, zoomID);
                    clearTimeout(timeout);
                } else {
                    timeout = setTimeout(function () { loop(e) }, 200);
                };
            };

            function show(e, lense, zoomID) {
                over = true;
                $(img).css({ 'position': 'absolute', 'top': '0', 'left': '0' });
                $('#' + options.id).html('').append(img);

                // "w1 and h1" are width and height of small image
                w1 = $('img', obj).width();
                h1 = $('img', obj).height();

                // "w2 and h2" are width and height of css easyzoom
                w2 = $('#' + options.id).width();
                h2 = $('#' + options.id).height();

                // "w3 and h3" are width and height of big image
                w3 = $(img).width();
                h3 = $(img).height();

                w4 = $(img).width() - w2;
                h4 = $(img).height() - h2;

                rw = w4 / w1;
                rh = h4 / h1;


                len_height = (h1 * w2) / h3;
                len_width = (w1 * w2) / w3;

                move(e, lense, zoomID);
            };

            function hide(lense) {
                over = false;
                $('#' + options.id).remove();
                $(lense).css({ display: 'none' });
            };

            function error() {
                $('#' + options.id).html(options.error);
            };

            function move(e, lense, zoomID) {
                if (over) {
                    // target image movement
                    var p = $('img', obj).offset();
                    var pl = e.pageX - p.left;

                    var pt = e.pageY - p.top;
                    var xl = pl * rw;
                    var xt = pt * rh;
                    xl = (xl > w4) ? w4 : xl;
                    xt = (xt > h4) ? h4 : xt;

                    $('#' + options.id + ' img').css({ 'left': xl * (-1), 'top': xt * (-1) });



                    //------------- Len -----------------------------------
                    // "yl and yt" use for Create center box to mouse
                    var yl = e.pageX - len_width / 2 - p.left;
                    var yt = e.pageY - len_height / 2 - p.top;

                    if (pl > (w1 - len_width / 2))// right
                        yl = w1 - len_width - 1;

                    if (pt > (h1 - len_height / 2))// buttom
                        yt = h1 - len_height;

                    if (pt < (len_height / 2))// top
                        yt = -1;

                    if (pl < (len_width / 2))// left
                        yl = -1;

                    if (zoomID == 'easy_zoom1') {
                        yl = yl + 27;
                        yt = yt + 14;
                    }



                    $('#' + options.id + ' img').css({ 'max-width': 'none' });
                    $(lense).css({
                        display: 'block',
                        left: yl,
                        top: yt,
                        width: len_width,
                        height: len_height,
                    });
                    //$('#'+ options.id + ' img').css({'left':yl*(-1),'top':yt*(-1)});
                    //-------------------------------------------------------
                };
            };

        };

    }
})
(jQuery);
