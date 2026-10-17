(function ($) {
    'use strict';

    var $form = $('#test');
    var token = $form.find('input[name="__RequestVerificationToken"]').val();
    var pendingChoices = [];

    $form.on('click', '.question.active .answer input', function () {
        var $input = $(this);
        var request = $.ajax({
            url: $form.attr('action'),
            method: 'POST',
            contentType: 'application/json',
            headers: { RequestVerificationToken: token },
            data: JSON.stringify({
                questionNumber: Number($input.data('question')),
                answerNumber: Number($input.data('answer'))
            })
        });

        // 409 means the test is already completed (e.g. in another tab); reloading shows the saved result.
        request.fail(function (xhr) {
            if (xhr.status === 409) {
                window.location.reload();
            }
        });

        pendingChoices.push(request.catch($.noop));
    });

    // site.js calls App.Test.submit after the last answer; complete the test only once every choice is saved.
    App.Test.submit = function () {
        $.when.apply($, pendingChoices).always(function () {
            $.ajax({
                url: $form.data('complete-url'),
                method: 'POST',
                headers: { RequestVerificationToken: token }
            }).always(function () {
                window.location.href = $form.data('result-url');
            });
        });
    };
})(jQuery);
