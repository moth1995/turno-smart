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

handleFormSubmission('#edit-form', '#modal-edit-content'); //post
handleFormSubmission('#edit-btn', '#modal-edit-content'); //get

handleFormSubmission('#delete-btn', '#modal-delete-content'); //get
handleFormSubmission('#delete-form', '#modal-delete-content'); //post

handleFormSubmission('#details-btn', '#modal-details-content'); //get

handleFormSubmission('#create-btn', '#modal-create-content'); //get
handleFormSubmission('#create-form', '#modal-create-content'); //post


document.addEventListener('DOMContentLoaded', function () {
    var editMedicoModal = document.getElementById('modal-edit');
    // Limpiar datos del modal al cerrarlo
    editMedicoModal.addEventListener('hidden.bs.modal', function () {
        document.getElementById('modal-edit-content').innerHTML = '';
        // Destruir el modal
        var modalInstance = bootstrap.Modal.getInstance(editMedicoModal);
        modalInstance.dispose();
    });

    editMedicoModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var medicoId = button.getAttribute('data-id');

        // Llama a tu API para obtener los datos del médico
        fetch(`/Medico/Edit/${medicoId}`)
            .then(response => response.json())
            .then(data => {
                var modalContent = `
                    <div class="modal-header">
                        <h5 class="modal-title" id="editMedicoModalLabel">Editar Médico</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <form id="editMedicoForm">
                            <input type="hidden" id="medicoId" name="Id" value="${data.id}" />
                            <div class="mb-3">
                                <label for="nombre" class="form-label">Nombre</label>
                                <input type="text" class="form-control" id="nombre" name="Nombre" value="${data.nombre}" />
                            </div>
                            <div class="mb-3">
                                <label for="apellido" class="form-label">Apellido</label>
                                <input type="text" class="form-control" id="apellido" name="Apellido" value="${data.apellido}" />
                            </div>
                            <div class="mb-3">
                                <label for="telefono" class="form-label">Teléfono</label>
                                <input type="text" class="form-control" id="telefono" name="Telefono" value="${data.telefono}" />
                            </div>
                            <div class="mb-3">
                                <label for="email" class="form-label">Email</label>
                                <input type="email" class="form-control" id="email" name="Email" value="${data.email}" />
                            </div>
                            <div class="mb-3">
                                <label for="resena" class="form-label">Reseña</label>
                                <textarea class="form-control" id="resena" name="Reseña">${data.reseña || ''}</textarea>
                            </div>
                            <div class="mb-3">
                                <label for="imagen" class="form-label">Imagen</label>
                                <input type="text" class="form-control" id="imagen" name="Imagen" value="${data.imagen}" required />
                            </div>
                            <div class="mb-3">
                                <label for="matricula" class="form-label">Matricula</label>
                                <input type="matricula" class="form-control" id="matricula" name="Matricula" value="${data.matricula}" />
                            </div>
                            <div class="mb-3">
                                <label for="especialidad" class="form-label">Especialidad</label>
                                <select class="form-control" id="especialidad" name="IdEspecialidad">
                                    ${data.especialidad.map(especialidad => `
                                        <option value="${especialidad.value}" ${especialidad.value == data.idEspecialidad ? 'selected' : ''}>${especialidad.text}</option>
                                    `).join('')}
                                </select>
                            </div>
                            <button type="submit" class="btn btn-primary">Guardar cambios</button>
                        </form>
                    </div>
                `;
                document.getElementById('modal-edit-content').innerHTML = modalContent;

                // Añadir evento de submit al nuevo formulario
                document.getElementById('editMedicoForm').addEventListener('submit', function (event) {
                    event.preventDefault();
                    var formData = new FormData(this);
                    var jsonData = {};

                    formData.forEach((value, key) => {
                        jsonData[key] = value;
                    });

                    fetch(`/Medico/Edit`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json',
                            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                        },
                        body: JSON.stringify(jsonData)
                    })
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Error en la respuesta del servidor');
                            }
                            return response.json();
                        })
                        .then(data => {
                            if (data.success) {
                                toastr.success('Información de médico actualizada correctamente.');
                                setTimeout(() => {
                                    location.reload();
                                }, 2000); // Espera 2 segundos antes de recargar la página
                            } else {
                                alert('Error al guardar los cambios: ' + data.message);
                            }
                        })
                        .catch(error => {
                            alert('Error al guardar los cambios: ' + error.message);
                        });
                });
            });
    });
});
