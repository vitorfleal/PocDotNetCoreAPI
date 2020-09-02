using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.ValueObject;
using System.Collections.Generic;

namespace AE.HealthSystem.Domain.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        IEnumerable<Consulta> ObterConsultasPorNomePessoa(string nome, Person person);
    }
}
