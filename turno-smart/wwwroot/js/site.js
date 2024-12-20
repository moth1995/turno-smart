// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Cuando se hace clic en el botón de toggle, cambiar el estado de expansión
document.querySelector('.navbar-toggler').addEventListener('click', function () {
    var navbarCollapse = document.getElementById('basic-navbar-nav');
    var expanded = navbarCollapse.classList.contains('show');
    var navbarToggler = this;

    // Toggle de la clase 'show' y el atributo 'aria-expanded'
    navbarToggler.setAttribute('aria-expanded', !expanded);
    navbarCollapse.classList.toggle('show');
});

function openRegistrationModal() {
    var myModal = new bootstrap.Modal(document.getElementById('modal-registration'));
    myModal.show();
}

function openLoginModal() {
    var myModal = new bootstrap.Modal(document.getElementById('modal-login'));
    myModal.show();
}

function openForgotPasswordModal() {
    var myModal = new bootstrap.Modal(document.getElementById('forgotPasswordModal'));
    myModal.show();
}

function onRegistrationModalLoginBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('modal-registration'));
    myModal.hide();
    openLoginModal();
}

function onLoginModalRegisterBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('modal-login'));
    myModal.hide();
    openRegistrationModal();
}

function onLoginModalForgotPasswordBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('modal-login'));
    myModal.hide();
    openForgotPasswordModal();
}

$(document).on('hidden.bs.modal', '.modal', function () {
    $(this).find('.modal-content').empty();
});


function handleFormSubmission(formSelector, resultContainerSelector) {
    $(document).on('submit', formSelector, function (event) {
        event.preventDefault();
        const form = $(this);
        console.log(form.attr('action'));
        console.log(form.attr('method'));
        console.log(form.serialize());
        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {
                if (result.redirectUrl !== undefined) {
                    window.location.replace(result.redirectUrl);
                } else {
                    $(resultContainerSelector).html(result);
                }
            },
            error: function () {
                alert('Error al procesar la solicitud.');
            }
        });
    });
}

handleFormSubmission('#registrationForm', '#modal-registration-content'); //post
handleFormSubmission('#register-btn', '#modal-registration-content'); //get

handleFormSubmission('#login-btn', '#modal-login-content'); //post
handleFormSubmission('#loginForm', '#modal-login-content'); //get

//handleFormSubmission('#TurnoEditForm', '#modal-edit-content'); //post
handleFormSubmission('#edit-form', '#modal-edit-content'); //post
handleFormSubmission('#edit-btn', '#modal-edit-content'); //get

handleFormSubmission('#turno-delete-form', '#modal-delete-content'); //post
handleFormSubmission('#delete-btn', '#modal-delete-content'); //get


