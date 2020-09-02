using AE.HealthSystem.Infra.Data.Mappings;
using AE.HealthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AE.HealthSystem.Infra.Data.Context
{
    public class HealthSystemContext : DbContext
    {
        public HealthSystemContext(DbContextOptions<HealthSystemContext> options) : base(options) { }
        public HealthSystemContext() { }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>(new MedicoMap().Configure);
            modelBuilder.Entity<Paciente>(new PacienteMap().Configure);
            modelBuilder.Entity<Consulta>(new ConsultaMap().Configure);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("health-system"));
        }
    }
}
