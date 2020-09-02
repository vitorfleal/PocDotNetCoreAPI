using AE.HealthSystem.Domain.Entities;
using FluentValidation;

namespace AE.HealthSystem.Domain.Aggregate
{
    public class MedicoValidator<T> : PessoaValidator<Medico>
    {
        public MedicoValidator()
        {
            Include(new PessoaValidator<Pessoa>());
            RuleFor(c => c.Especialidade)
                    .NotEmpty().WithMessage("A especialidade do médico precisa ser fornecido")
                    .Length(2, 100).WithMessage("A especialidade do médico ter entre 2 e 100 caracteres");
        }
    }
}
