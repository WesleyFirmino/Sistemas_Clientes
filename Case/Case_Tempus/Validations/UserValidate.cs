using CaseTempus.Models;
using FluentValidation;
using System;

namespace CaseTempus.Validations
{
    public class UserValidate : AbstractValidator<UserViewModel>
    {
        public UserValidate()
        {
            DateTime tomorow = DateTime.Now;
            DateTime limit = DateTime.Now.AddYears(-130);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Este campo é obrigatório")
                .NotNull().WithMessage("Este campo é obrigatório")
                .MaximumLength(150).WithMessage("Este campo deve contar no máximo 150 caracteres");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("Este campo é obrigatório")
                .Length(11).WithMessage("Este campo deve contar 11 caracteres")
                .NotNull().WithMessage("Este campo é obrigatório");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Este campo é obrigatório")
                .LessThan(tomorow).WithMessage("Valor máximo para  campo é a data atual")
                .GreaterThan(limit).WithMessage("Data inválida");            
        }
    }
}
