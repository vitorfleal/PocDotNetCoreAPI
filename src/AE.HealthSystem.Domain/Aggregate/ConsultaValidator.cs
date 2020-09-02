using AE.HealthSystem.Domain.Entities;
using FluentValidation;
using System;

namespace AE.HealthSystem.Domain.Aggregate
{
    public class ConsultaValidator<T> : AbstractValidator<T> where T : Consulta
    {
        public ConsultaValidator()
        {
            RuleFor(c => c.DataAgendamento)
                .NotNull().WithMessage("A data da consulta precisa ser fornecida")
                .GreaterThan(DateTime.Now).WithMessage("A data da consulta não deve ser menor que a data atual");

            RuleFor(c => c.MedicoId)
                .NotNull().WithMessage("O médico precisa ser fornecido");

            RuleFor(c => c.PacienteId)
                .NotNull().WithMessage("O paciente precisa ser fornecido");
        }
    }
}
