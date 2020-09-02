using System.Collections.Generic;

namespace AE.HealthSystem.Domain.Entities
{
    public class Paciente : Pessoa
    {
        public Paciente(string nome, string enfermidade)
        {
            Nome = nome;
            Enfermidade = enfermidade;
        }

        public Paciente() { }

        public string Enfermidade { get; private set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
