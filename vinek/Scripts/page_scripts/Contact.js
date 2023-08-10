jQuery(function ($) {
    "use strict";

    $('.contact_us_form').on('submit', function (e) {

        e.preventDefault();
        $.ajax({
            type: "POST",
            url: "/Home/SendEmail",
            data: { fromAddress: $("#email").val(), subject: $("#subject").val(), message: $("#message").val() },
            beforeSend: function () {
                $(".statusTxt").text("Loading...");
                //$(".login100-form-btn").attr("disabled", true);
            },
            success: function (result) {
                //$(".login100-form-btn").attr("disabled", false);
                var serverResponse = JSON.parse(result);
                
                $(".statusTxt").text("Your feedback has been received we will get back to you");
                
            },
            error: function () {
                $(".statusTxt").text("Error! Please check your Internet");

            },
            timeout: function () {
                $(".statusTxt").text("Request timeout try again!");
            },


        });


        return false;
    });

});

