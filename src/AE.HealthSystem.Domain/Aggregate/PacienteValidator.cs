using AE.HealthSystem.Domain.Entities;
using FluentValidation;

namespace AE.HealthSystem.Domain.Aggregate
{
    public class PacienteValidator<T> : PessoaValidator<Paciente>
    {
        public PacienteValidator()
        {
            RuleFor(c => c.Enfermidade)
                    .NotEmpty().WithMessage("A enfermidade precisa ser fornecida")
                    .Length(2, 100).WithMessage("A enfermidade ter entre 2 e 100 caracteres");
        }
    }
}
