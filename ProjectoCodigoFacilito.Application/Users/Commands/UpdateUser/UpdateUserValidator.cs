﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoCodigoFacilito.Application.Users.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El id es requerido");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("La dirección de correo electrónico es obligatoria.")
                .EmailAddress().WithMessage("Se requiere una dirección de correo electrónico válida.")
                .Must(email => email.Contains("@") && email.EndsWith(".com"))
                .WithMessage("La dirección de correo electrónico no es válida.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.Role).NotEmpty().WithMessage("El rol no es requerido");
        }
    }
}
