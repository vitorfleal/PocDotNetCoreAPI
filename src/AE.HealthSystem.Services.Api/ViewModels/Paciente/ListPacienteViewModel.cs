using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Paciente
{
    public class ListPacienteViewModel
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Enfermidade { get; set; }
    }
}
