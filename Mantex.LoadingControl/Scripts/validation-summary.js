;
(function ($) {
    $(document).ready(function () {
        $(".validation-summary-errors").removeClass("validation-summary-errors");
        $(".input-validation-error").removeClass("input-validation-error").parent().addClass("has-error");
    });
}(jQuery));