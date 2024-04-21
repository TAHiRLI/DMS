// Mobile Menu Toggle Button JavaScript
function myFunction() {
  var x = document.getElementById("mobile-menu");
  if (x.style.display === "none") {
    x.style.display = "block";
  } else {
    x.style.display = "none";
  }
}



// LANGUAGE CHANGER

$("#dd span").change(function (e) {
  console.log("dropdown change event is fire : " + $(this).text());
});
$("#de span").change(function (e) {
  console.log("dropdown change event is fire : " + $(this).text());
});
(function ($) {
  $.fn.dropdown = function () {
    var el = $(this);
    el.addClass("dropdown");
    var holder = $("span.holder", el);
    var opts_con = $("ul", el);
    var opts = $("ul li", el);
    var val = "";
    opts_con.prepend("<span class='arrow_up'></span>");

    el.on("click", function () {
      opts_con.toggleClass("active")

    });

    opts.on("click", function () {
      holder.text($(this).text());
      holder.change();
    });
  }

}(jQuery));
$("#dd").dropdown();



// PORTFOLIO SLIDER
$('.slider').slick({
  dots: true,
  autoplay: true,
  infinite: false,
  speed: 1000,
  slidesToShow: 4,
  slidesToScroll: 2,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 820,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
  ]
});



// HEADER SLIDER
let swiper = new Swiper('.swiper', {
  loop: true,
  watchSlidesProgress: true,
  autoplay: {
    speed: 3000
  },
  pagination: {
    el: '.swiper-pagination',
  },
  navigation: {
    nextEl: '.swiper-button-next',
    prevEl: '.swiper-button-prev',
  },
  scrollbar: {
    el: '.swiper-scrollbar',
  },
  on: {
    slideChangeTransitionEnd: function (s) {
      i = s.progress;
      params = s.params;
      console.log("progress" + i);
      if (i >= 1) {
        swiper.destroy(false, false);
        params.autoplay = false;
        swiper = new Swiper('.swiper', params);
      }
    }
  }
});

// FORM BUTTON
document.getElementById("contact-submit").onclick = function (event) {
  event.preventDefault();

  var formFields = document.querySelectorAll('#contact input');

  var isFormValid = Array.from(formFields).every(function (field) {
    return field.value.trim() !== '';
  });

  if (isFormValid) {
    swal("Successfully!");

  } else {
    swal("Please fill in all fields!");
  }
};

function setSelectedService(serviceId) {
  document.cookie = `selectedService=${serviceId}; path=/`;
}