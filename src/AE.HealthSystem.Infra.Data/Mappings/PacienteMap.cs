using AE.HealthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AE.HealthSystem.Infra.Data.Mappings
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired(true);

            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.Enfermidade)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.ToTable("Paciente");
        }
    }
}