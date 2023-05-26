﻿$(document).ready(function () {
    document.querySelector("#menu_bar").onclick = function () {
        let element = document.querySelector(".leftMenu");
        element.classList.toggle("openMenu");

        let closeAccordion = document.getElementsByClassName("dropdown");
        let i = 0;
        for (i; i < closeAccordion.length; i++) {
            closeAccordion[i].classList.remove("active");
        }
    };

    let dropdown = document.getElementsByClassName("dropdown");
    let i = 0;
    for (i; i < dropdown.length; i++) {
        dropdown[i].onclick = function () {
            this.classList.toggle("active");
        };
    }

    $("#menu_bar").click(function () {
        $(".main-header span:first").toggleClass("first");
        $(".content-wrapper").toggleClass("content-wrapper-mid");
        $(".tooltip_nav p").toggleClass("hide");
    });

});
$(document).ready(function () {
    let path = window.location.pathname;

    $('ul.leftMenuList li').removeClass('active');

    $('ul.leftMenuList li').each(function () {
        let link = $(this).find('a').attr('href');
        if (link && link === path) {
            $(this).addClass('active');
        }
    });
});

