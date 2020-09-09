
(function ($) {
    "use strict";

    var input = $('.validate-input .input100');

    $('.validate-form').on('submit', function (e) {
        var check = true;


        if (validate(input[0]) == false) {
            showValidate(input[0]);
            check = false;
        }
        else if (validate(input[1]) == false) {
            showValidate(input[1]);
            check = false;
        }
        else {

            e.preventDefault();
            $.ajax({
                type: "POST",
                url: "/Home/CreateUser",
                data: { email: $("#emailInput").val(), password: $("#passwordInput").val() },
                beforeSend: function () {
                    $(".statusTxt").text("Loading...");
                    $(".login100-form-btn").attr("disabled", true);
                },
                success: function (result) {
                    $(".login100-form-btn").attr("disabled", false);
                    var serverResponse = JSON.parse(result);
                    if (serverResponse.success) {
                        if (serverResponse.login_role == "user") {
                            window.location.replace("/Home/CarTrackingPanel");
                        }
                        else {
                            window.location.replace("/Home/AdminPanel");
                        }

                        $.cookie('email', serverResponse.email, { expires: 30 / 1440, path: '/' });

                    }
                    else {
                        $(".statusTxt").text("Email or Password is Invalid!");
                        $(".login100-form-btn").attr("disabled", fakse);

                    }
                },
                error: function () {
                    $(".statusTxt").text("Error! Please check your Internet");
                    $(".login100-form-btn").attr("disabled", false);

                },
                timeout: function () {
                    $(".statusTxt").text("Request timeout try again!");
                    $(".login100-form-btn").attr("disabled", false);
                },


            });
        }

        return check;
    });


    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if ($(input).val().trim() == '') {
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }



})(jQuery);

