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
    var myModal = new bootstrap.Modal(document.getElementById('registrationModal'));
    myModal.show();
}

function openLoginModal() {
    var myModal = new bootstrap.Modal(document.getElementById('loginModal'));
    myModal.show();
}

function openForgotPasswordModal() {
    var myModal = new bootstrap.Modal(document.getElementById('forgotPasswordModal'));
    myModal.show();
}

function onRegistrationModalLoginBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('registrationModal'));
    myModal.hide();
    openLoginModal();
}

function onLoginModalRegisterBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('loginModal'));
    myModal.hide();
    openRegistrationModal();
}

function onLoginModalForgotPasswordBtnClick() {
    var myModal = bootstrap.Modal.getInstance(document.getElementById('loginModal'));
    myModal.hide();
    openForgotPasswordModal();
}

