using Integrativa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integrativa.Infrastructure.Configurations;

public class ProcessoConfiguration : IEntityTypeConfiguration<Processo>
{
    public void Configure(EntityTypeBuilder<Processo> builder)
    {
        builder.ToTable("processos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Numero)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Assunto)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.DataCriacao)
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.DataAlteracao)
            .IsRequired();

        builder.Property(p => p.UsuarioAlteracao)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.Numero)
            .IsUnique();

        builder.HasMany(p => p.Partes)
            .WithOne()
            .HasForeignKey(x => x.ProcessoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Andamentos)
            .WithOne()
            .HasForeignKey(x => x.ProcessoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Diz ao EF Core para popular os campos privados (_partes, _andamentos)
        // em vez de exigir um setter público
        builder.Metadata.FindNavigation(nameof(Processo.Partes))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(Processo.Andamentos))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}