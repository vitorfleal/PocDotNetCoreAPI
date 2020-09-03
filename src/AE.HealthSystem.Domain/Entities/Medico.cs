using System.Collections.Generic;

namespace AE.HealthSystem.Domain.Entities
{
    public class Medico : Pessoa
    {
        public Medico(string nome, string especialidade)
        {
            Nome = nome;
            Especialidade = especialidade;
        }

        public Medico() {}

        public string Especialidade{ get; private set; }

        public virtual ICollection<Consulta> Consultas { get; set; }

    }
}
