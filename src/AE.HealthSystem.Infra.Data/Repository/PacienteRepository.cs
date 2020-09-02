using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Infra.Data.Context;

namespace AE.HealthSystem.Infra.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(HealthSystemContext context) : base(context) { }
    }
}
