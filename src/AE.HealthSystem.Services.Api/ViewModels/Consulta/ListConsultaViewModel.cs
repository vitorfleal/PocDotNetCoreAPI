using AE.HealthSystem.Services.Api.ViewModels.Medico;
using AE.HealthSystem.Services.Api.ViewModels.Paciente;
using System;
using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Consulta
{
    public class ListConsultaViewModel
    {
        public ListConsultaViewModel()
        {
            Medico = new ListMedicoViewModel();
            Paciente = new ListPacienteViewModel();
        }

        public long Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public long MedicoId { get; set; }
        public long PacienteId { get; set; }

        public ListMedicoViewModel Medico { get; set; }
        public ListPacienteViewModel Paciente { get; set; }
    }
}
