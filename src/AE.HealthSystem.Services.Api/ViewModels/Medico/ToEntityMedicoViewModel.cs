using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Medico
{
    public class ToEntityMedicoViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "O Nome é requerido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A especialidade é requerido")]
        public string Especialidade { get; set; }
    }
}
