// animation
new WOW().init();



$(document).ready(function(){

// smooth state
// smooth
$(function(){
  'use strict';
  var $page = $('body'),
      options = {
        debug: true,
        prefetch: true,
        cacheLength: 2,
        onStart: {
          duration: 250, // Duration of our animation
          render: function ($container) {
            // Add your CSS animation reversing class
            $container.addClass('is-exiting');
            // Restart your animation
            smoothState.restartCSSAnimations();
          }
        },
        onReady: {
          duration: 0,
          render: function ($container, $newContent) {
            // Remove your CSS animation reversing class
            $container.removeClass('is-exiting');
            // Inject the new content
            $container.html($newContent);
          }
        }
      },
      smoothState = $page.smoothState(options).data('smoothState');
});


 $('[data-toggle="popover"]').popover();
 $('[data-toggle="tooltip"]').tooltip();

// time counter
$(window).ready( function() {
    var time = $('.time-counter').data("timer");
    setInterval( function() {
        time--;
        $('.time-counter').html('00.' + time);
        if (time === 0) {
            location.reload();
        }    
    }, 1000 );
});


//Date picker
$('.datepicker').datepicker({
  autoclose: true,
    format: 'dd M yyyy'
});

// scrollbbar
jQuery(document).ready(function(){
  jQuery('.scrollbar-inner').scrollbar();
});


// select plus search
 $('.selectpicker').select2({
     width: 'element'
 });


// ticket details


$(".dash-boxes-inner.li-grey a, .btn-backto").click(function() {
  $(".dashboard").toggleClass('close');
    $(".ticket-details").toggleClass('open');  
    $("html, body").animate({scrollTop: 0}, 500);
    $(".loading").delay(500).fadeOut();
});

$(".btn-backto").click(function() {
    $(".loading").addClass("shows");
});



});