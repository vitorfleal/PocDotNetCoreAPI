using System;
using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Consulta
{
    public class ToEntityConsultaViewModel
    {
        [Required(ErrorMessage = "A Data é requerida")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "O Médico é requerido")]
        public long MedicoId { get; set; }

        [Required(ErrorMessage = "O Paciente é requerido")]
        public long PacienteId { get; set; }
    }
}
