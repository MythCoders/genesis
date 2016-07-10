// This is a manifest file that'll be compiled into application.js, which will include all the files
// listed below.
//
// Any JavaScript/Coffee file within this directory, lib/assets/javascripts, vendor/assets/javascripts,
// or any plugin's vendor/assets/javascripts directory can be referenced here using a relative path.
//
// It's not advisable to add code directly here, but if you do, it'll appear at the bottom of the
// compiled file. JavaScript code in this file should be added after the last require_* statement.
//
// Read Sprockets README (https://github.com/rails/sprockets#sprockets-directives) for details
// about supported directives.
//
//= require jquery
//= require jquery_ujs
//= require turbolinks
//= require_tree .

function showTab(name, url) {
    $('#tab-content-' + name).parent().find('.tab-content').hide();
    $('#tab-content-' + name).parent().find('div.tabs a').removeClass('selected');
    $('#tab-content-' + name).show();
    $('#tab-' + name).addClass('selected');
    //replaces current URL with the "href" attribute of the current link
    //(only triggered if supported by browser)
    if ("replaceState" in window.history) {
        window.history.replaceState(null, document.title, url);
    }
    return false;
}

function moveTabRight(el) {
    var lis = $(el).parents('div.tabs').first().find('ul').children();
    var bw = $(el).parents('div.tabs-buttons').outerWidth(true);
    var tabsWidth = 0;
    var i = 0;
    lis.each(function() {
        if ($(this).is(':visible')) {
            tabsWidth += $(this).outerWidth(true);
        }
    });
    if (tabsWidth < $(el).parents('div.tabs').first().width() - bw) { return; }
    $(el).siblings('.tab-left').removeClass('disabled');
    while (i<lis.length && !lis.eq(i).is(':visible')) { i++; }
    var w = lis.eq(i).width();
    lis.eq(i).hide();
    if (tabsWidth - w < $(el).parents('div.tabs').first().width() - bw) {
        $(el).addClass('disabled');
    }
}

function moveTabLeft(el) {
    var lis = $(el).parents('div.tabs').first().find('ul').children();
    var i = 0;
    while (i < lis.length && !lis.eq(i).is(':visible')) { i++; }
    if (i > 0) {
        lis.eq(i-1).show();
        $(el).siblings('.tab-right').removeClass('disabled');
    }
    if (i <= 1) {
        $(el).addClass('disabled');
    }
}

function displayTabsButtons() {
    var lis;
    var tabsWidth;
    var el;
    var numHidden;
    $('div.tabs').each(function() {
        el = $(this);
        lis = el.find('ul').children();
        tabsWidth = 0;
        numHidden = 0;
        lis.each(function(){
            if ($(this).is(':visible')) {
                tabsWidth += $(this).outerWidth(true);
            } else {
                numHidden++;
            }
        });
        var bw = $(el).parents('div.tabs-buttons').outerWidth(true);
        if ((tabsWidth < el.width() - bw) && (lis.first().is(':visible'))) {
            el.find('div.tabs-buttons').hide();
        } else {
            el.find('div.tabs-buttons').show().children('button.tab-left').toggleClass('disabled', numHidden == 0);
        }
    });
}