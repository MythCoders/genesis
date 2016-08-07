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