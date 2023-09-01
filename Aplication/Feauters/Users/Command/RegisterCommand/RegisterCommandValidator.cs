using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Feauters.Users.Command.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
               .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength}");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
                .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength}")
                .EmailAddress().WithMessage("{PropertyName} debe ser una direccion de email valida");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
              .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength}");

            RuleFor(x => x.ConfirmPassword)
           .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
           .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength}")
           .Equal(p => p.Password).WithMessage("{PropertyName} debe ser igual a password");

            RuleFor(x => x.UserName)
              .NotEmpty().WithMessage("{PropertyName} no puede ser vacio")
              .MaximumLength(30).WithMessage("{PropertyName} no debe exceder {MaxLength}");
        }
    }
}
