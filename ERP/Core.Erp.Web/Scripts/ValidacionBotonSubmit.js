$(document).on('submit', 'form', function () {
    var buttons = $(this).find('[type="submit"]');
    if ($(this).valid()) {
        buttons.each(function (btn) {
            $(buttons[btn]).prop('disabled', true);
        });
    } else {
        buttons.each(function (btn) {
            $(buttons[btn]).prop('disabled', false);
        });
    }
});

function Post() {
    $("#PostForm").submit();
}

$(document).on('submit', 'form', function () {
    var buttons = $(this).find('[id="ButtonPost"]');
    if ($(this).valid()) {
        buttons.each(function (btn) {
            $(buttons[btn]).prop('disabled', true);
        });
    } else {
        buttons.each(function (btn) {
            $(buttons[btn]).prop('disabled', false);
        });
    }
});