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
//= require_tree ./angle/
//= require jquery.cookie
//= require jquery_ujs
//= require turbolinks

$('[data-change-theme]').on('click', function (e) {
    var element = $(this);
    if (element.is('a')) {
        e.preventDefault();
    }
    $.cookie('theme', element.data('changeTheme'), {expires: 365});
    location.reload();
});

function setupAjaxIndicator() {
    $(document).bind('ajaxSend', function (event, xhr, settings) {
        if ($('.ajax-loading').length === 0 && settings.contentType != 'application/octet-stream') {
            $('#ajax-indicator').show();
        }
    });
    $(document).bind('ajaxStop', function () {
        $('#ajax-indicator').hide();
    });
}

function updateUserActiveItems(clickedItem) {
    var schoolId = $("#SIS.SCHOOL_ID").val();
    var schoolYearId = $("#SIS.SCHOOL_YEAR_ID").val();

    $.ajax({
        url: '/update_school',
        type: 'post',
        contentType: "application/x-www-form-urlencoded",
        data: '{"schoolId":"schoolId", "program":"EXPLORE"}',
        success: function (data, status) {
            console.log("Success!!");
            console.log(data);
            console.log(status);
        },
        error: function (xhr, desc, err) {
            console.log(xhr);
            console.log("Desc: " + desc + "\nErr:" + err);
        }
    });
}

var warnLeavingUnsavedMessage;
function warnLeavingUnsaved(message) {
    warnLeavingUnsavedMessage = message;
    $(document).on('submit', 'form', function () {
        $('textarea').removeData('changed');
        $('textbos').removeData('changed');
    });
    $(document).on('change', 'textarea', function () {
        $(this).data('changed', 'changed');
    });
    $(document).on('change', 'textbos', function () {
        $(this).data('changed', 'changed');
    });
    window.onbeforeunload = function () {
        var warn = false;
        $('textarea').blur().each(function () {
            if ($(this).data('changed')) {
                warn = true;
            }
        });
        $('textbox').blur().each(function () {
            if ($(this).data('changed')) {
                warn = true;
            }
        });
        if (warn) {
            return warnLeavingUnsavedMessage;
        }
    };
}

function showVerticalNavItem() {
    $('#tab-content-' + name).parent().find('.tabs').hide();
    $('#tab-content-' + name).parent().find('div.tabs-menu a').removeClass('selected');
    $('#tab-content-' + name).show();
    $('#tab-' + name).addClass('selected');
    //replaces current URL with the "href" attribute of the current link
    //(only triggered if supported by browser)
    if ("replaceState" in window.history) {
        window.history.replaceState(null, document.title, url);
    }
    return false;
}

function showHorizontalNavItem() {
    $('#tab-content-' + name).parent().find('.tabs').hide();
    $('#tab-content-' + name).parent().find('div.tabs-menu a').removeClass('aui-nav-selected');
    $('#tab-content-' + name).show();
    $('#tab-' + name).addClass('aui-nav-selected');
    //replaces current URL with the "href" attribute of the current link
    //(only triggered if supported by browser)
    if ("replaceState" in window.history) {
        window.history.replaceState(null, document.title, url);
    }
    return false;
}

function showTab(name, url) {
    $('#tab-content-' + name).parent().find('.tabs').hide();
    $('#tab-content-' + name).parent().find('div.tabs-menu a').removeClass('selected');
    $('#tab-content-' + name).show();
    $('#tab-' + name).addClass('selected');
    //replaces current URL with the "href" attribute of the current link
    //(only triggered if supported by browser)
    if ("replaceState" in window.history) {
        window.history.replaceState(null, document.title, url);
    }
    return false;
}

function loadAdvancedSearch() {
    // ADVANCED SEARCH
    var spinning = false;

    AJS.$("#adv-search-link").click(function () {
        AJS.dialog2("#adv-search-dialog").show();
    });

    AJS.$("#adv-search-close-button").click(function (e) {
        e.preventDefault();
        AJS.dialog2("#adv-search-dialog").hide();
    });

    AJS.$('#quick-search-query').on('keyup', function () {

        if (!spinning) {
            AJS.$(this).text('Stop Spinning!');
            AJS.$('.button-spinner').spin();
            spinning = true;
        } else {
            AJS.$(this).text('Do Something');
            AJS.$('.button-spinner').spinStop();
            spinning = false;
        }
    });
}