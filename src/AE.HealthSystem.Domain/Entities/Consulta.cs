using System;

namespace AE.HealthSystem.Domain.Entities
{
    public class Consulta : BaseEntity<Consulta>
    {
        public Consulta(DateTime dataAgendamento, long medicoId, long pacienteId)
        {
            DataAgendamento = dataAgendamento;
            MedicoId = medicoId;
            PacienteId = pacienteId;
        }

        public Consulta() { }

        public DateTime DataAgendamento { get; private set; }
        public long MedicoId { get; private set; }
        public long PacienteId { get; private set; }

        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}
