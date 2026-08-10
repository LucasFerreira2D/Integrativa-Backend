using Integrativa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integrativa.Infrastructure.Configurations;

public class ParteConfiguration : IEntityTypeConfiguration<Parte>
{
    public void Configure(EntityTypeBuilder<Parte> builder)
    {
        builder.ToTable("partes");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.TipoParte)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.DataAlteracao)
            .IsRequired();

        builder.Property(p => p.UsuarioAlteracao)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.ProcessoId);
    }
}