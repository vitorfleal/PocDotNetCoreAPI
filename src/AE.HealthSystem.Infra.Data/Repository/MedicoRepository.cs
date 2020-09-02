using AE.HealthSystem.Domain.Entities;
using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Infra.Data.Context;

namespace AE.HealthSystem.Infra.Data.Repository
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(HealthSystemContext context) : base(context) { }
    }
}
