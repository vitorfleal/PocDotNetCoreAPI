using AE.HealthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AE.HealthSystem.Infra.Data.Mappings
{
    public class ConsultaMap : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired(true);

            builder.Property(e => e.DataAgendamento)
               .HasColumnType("datetime")
               .IsRequired();

            builder.ToTable("Consulta");

            builder.HasOne(e => e.Medico)
                .WithMany(o => o.Consultas)
                .HasForeignKey(e => e.MedicoId);

            builder.HasOne(e => e.Paciente)
                .WithMany(o => o.Consultas)
                .HasForeignKey(e => e.PacienteId);
        }
    }
}