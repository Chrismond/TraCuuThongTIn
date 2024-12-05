$(document).ready(function () {
    if (window.matchMedia("(min-width: 992px)").matches) {
        /* the viewport is at least 992 pixels wide */
        $('.mega-menu, .mega-title, .mega-no-sub, .mega-gioithieu').hover(function () {
            $('.mega-sub-menu').hide();
            $('.menu-gioi-thieu').show();
            $(this).parent().css('height', 'auto');
            //$('.wsmenu>.wsmenu-list>li>.wsmegamenu').css('height', 'auto');
        })
        $(".mega-has-sub").hover(function () {
            $('.menu-gioi-thieu').hide();
            $('.mega-sub-menu').hide();
            $('#' + $(this).data("id")).show();
            //if ($('#' + $(this).data("id")).height() > $(this).parent().height()) {
            //    $(this).parent().css('height', $('#' + $(this).data("id")).height());                
            //}
            $(this).parent().css('min-height', 'calc(100vh - 200px)');
            //$('div.mega-sub-menu > ul.sub-menu').css('min-height', 'calc(100vh - 171px)');
            $('div.mega-sub-menu > ul.sub-menu').css('height', $(this).parent().css('height'));
        }, function () {
            //$('.mega-sub-menu').hide();
            //$('.menu-gioi-thieu').show();
        });
    } else {
        /* the viewport is less than 992 pixels wide */
        //$(".mega-has-sub").hover(function (event) {
        //    $('.menu-gioi-thieu').hide();
        //    $('.mega-sub-menu').hide();
        //    var hide = false;
        //    if ($(this).find('ul.sub-menu').is(":hidden")) {
        //        hide = true;
        //    }
        //    $(this).parent().find('ul.sub-menu').hide();
        //    if (hide) {
        //        $(this).find('ul.sub-menu').show();
        //    }
        //    else {
        //        $(this).find('ul.sub-menu').hide();
        //    }
        //    event.preventDefault();
        //}, function () {
        //    //$('.mega-sub-menu').hide();
        //    //$('.menu-gioi-thieu').show();
        //});
        $(".mega-has-sub a").click(function (event) {
            $('.menu-gioi-thieu').hide();
            $('.mega-sub-menu').hide();
            //alert($(this).parent().data("id"));
            $('.mega-sub-mobile').html("");
            $(this).parent().find('div.mega-sub-mobile').html($('#' + $(this).parent().data("id")).html());
            $(this).parent().find('ul.sub-menu').show();
            /*
            var hide = false;
            if ($(this).parent().find('ul.sub-menu').is(":hidden")) {
                hide = true;
            }
            $(this).parent().parent().find('ul.sub-menu').hide();
            if (hide) {
                $(this).parent().find('ul.sub-menu').show();
            }
            else {
                $(this).parent().find('ul.sub-menu').hide();
            }
            */
            event.preventDefault();
        });
    }
    
});
