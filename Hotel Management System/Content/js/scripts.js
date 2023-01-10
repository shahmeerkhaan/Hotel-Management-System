window.addEventListener('DOMContentLoaded', event => {
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            offset: 74,
        });
    };
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });
});
function Choosee()
{
    var data = document.getElementById("RoomData").value;
    var NoofRooms = document.getElementById("NoofRooms").value;
    if (data == "Single_Room") {
        document.getElementById("Rent").value = 5000 * NoofRooms;
    }
    else if (data == "Double_Room") {
        document.getElementById("Rent").value = 8000 * NoofRooms;
    }
    else if (data == "Triple_Room") {
        document.getElementById("Rent").value = 10000 * NoofRooms;
    }
    else if (data == "Quad_Room") {
        document.getElementById("Rent").value = 13000 * NoofRooms;
    }
    else if (data == "King_Room") {
        document.getElementById("Rent").value = 15000 * NoofRooms;
    }
}
$('.select2').select2();

function ConfirmDate()
{
    var D1 = new Date(document.getElementById("DateIn").value);
    var D2 = new Date(document.getElementById("DateOut").value);
    var difference = D1 - D2;
    var days = difference / (24 * 3600 * 1000);
    $("#DOI").val(Math.abs(days.toString()));
}

function Slider() {
    $('.dropdown').on('show.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').slideDown(400);
    });

    $('.dropdown').on('hide.bs.dropdown', function (e) {
        $(this).find('.dropdown-menu').slideUp(400);
    });
    /*$(".dropdown-menu").slideToggle();*/
}
////$(document).click(function () {
////    $(".dropdown-menu").hide();
////});
////$(".dropdown-menu").click(function (e) {
////    e.stopPropagation();
//});