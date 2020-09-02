using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Paciente
{
    public class ToEntityPacienteViewModel
    {
        public long Id{ get; set; }
        [Required(ErrorMessage = "O Nome é requerido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A enfermidade é requerido")]
        public string Enfermidade { get; set; }
    }
}
