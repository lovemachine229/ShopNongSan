
$(document).ready(function() {
    $(function() {
        $(".menu_content").tabs({
            event: "mouseover"
        });
    });

    //
    // $('.slider2').bxSlider({
    //     auto: true,
    //     slideWidth: 170,
    //     mode: 'horizontal',
    //     minSlides: 1,
    //     maxSlides: 5,
    //     moveSlides: 1,
    //     slideMargin: 20,
    //     pager: false, controls: true
    //
    // });
    // $('.slider3').bxSlider({
    //     auto: true,
    //     slideWidth: 102,
    //     mode: 'horizontal',
    //     minSlides: 1,
    //     maxSlides: 2,
    //     moveSlides: 1,
    //     slideMargin: 2,
    //     pager: false, controls: false
    //
    // });

    jQuery(document).ready(function() {
        var offset = 220;
        var duration = 500;
        jQuery(window).scroll(function() {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('.back-to-top').fadeIn(duration);
            } else {
                jQuery('.back-to-top').fadeOut(duration);
            }
        });

        jQuery('.back-to-top').click(function(event) {
            event.preventDefault();
            jQuery('html, body').animate({scrollTop: 0}, duration);
            return false;
        });
    });
    jQuery('#datetimepicker').datetimepicker({
        //  format:'d/m/Y',
        lang: 'de',
        i18n: {
            de: {
                months: [
                    'Januar', 'Februar', 'März', 'April',
                    'Mai', 'Juni', 'Juli', 'August',
                    'September', 'Oktober', 'November', 'Dezember',
                ],
                dayOfWeek: [
                    "Sun", "Mon", "Tue", "Wed",
                    "Thu", "Fri", "Sat",
                ]
            }
        },
        timepicker: false,
        format: 'd-m-Y'
    });
    //video
    $('.slider3 li a').click(function(){
			var url_video=$(this).attr('href');
			$('.top_video iframe').attr('src',url_video);
			return false;
		});
                $('.tin_vip1').hover(function(){
			var url_video=$(this).attr('val');
			$('.all_news').attr('href',url_video);
			return false;
		});
                $('.tin_vip2').hover(function(){
			var url_video=$(this).attr('val');
			$('.all_news').attr('href',url_video);
			return false;
		});

});
/*   $('.tin_vip1').hover(function() {
 $('.tin_vip1').removeClass('aaa');
 $(this).addClass('aaa');
 });*/