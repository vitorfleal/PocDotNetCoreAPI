using System.ComponentModel.DataAnnotations;

namespace AE.HealthSystem.Services.Api.ViewModels.Medico
{
    public class ListMedicoViewModel
    {
        [Key]
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
    }
}
