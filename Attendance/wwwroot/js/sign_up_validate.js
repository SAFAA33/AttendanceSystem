(function () {
    'use strict'
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')
    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
           
                form.classList.add('was-validated')
            }, false)
        })
})()

var password = document.getElementById("password")
    , confirm_password = document.getElementById("confirm_password");

function validatePassword() {
    if (password.value != confirm_password.value) {
        confirm_password.setCustomValidity("Passwords Don't Match");
    } else {
        confirm_password.setCustomValidity('');
    }
}

password.onchange = validatePassword;
confirm_password.onkeyup = validatePassword;

var password2 = document.getElementById("password2")
    , confirm_password2 = document.getElementById("confirm_password2");

function validatePassword2() {
    if (password2.value != confirm_password2.value) {
        confirm_password2.setCustomValidity("Passwords Don't Match");
    } else {
        confirm_password2.setCustomValidity('');
    }
}

password2.onchange = validatePassword2;
confirm_password2.onkeyup = validatePassword2;