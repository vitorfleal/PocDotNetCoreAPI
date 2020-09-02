using AE.HealthSystem.Domain.Entities;
using FluentValidation;

namespace AE.HealthSystem.Domain.Aggregate
{
    public class PessoaValidator<T> : AbstractValidator<T> where T : Pessoa
    {
        public PessoaValidator()
        {
            RuleFor(c => c.Nome)
               .NotEmpty().WithMessage("O nome precisa ser fornecido")
               .NotNull().WithMessage("O nome precisa ser fornecido")
               .Length(2, 150).WithMessage("O nome precisa ter entre 2 e 150 caracteres");
        }
    }
}
