using FluentValidation;
using Seventh.DGuard.Application.Commands;
using System;
using System.Net;

namespace Seventh.DGuard.Application.CommandValidators
{
    public class CreateServerCommandValidator : AbstractValidator<CreateServerCommand>
    {
        public CreateServerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome é obrigatório");

            RuleFor(x => x.Port)
                .GreaterThan(UInt16.MinValue)
                .WithMessage("Porta informada deve ser maior que 0");

            RuleFor(x => x.IP)
                .NotEmpty()
                .WithMessage("IP é obrigatório")
                .Must(x => IPAddress.TryParse(x, out _))
                .WithMessage("IP inválido");
        }
    }
}