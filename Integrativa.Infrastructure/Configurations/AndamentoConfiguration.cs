using Integrativa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integrativa.Infrastructure.Configurations;

public class AndamentoConfiguration : IEntityTypeConfiguration<Andamento>
{
    public void Configure(EntityTypeBuilder<Andamento> builder)
    {
        builder.ToTable("andamentos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(a => a.DataCriacao)
            .IsRequired();

        builder.Property(a => a.DataAlteracao)
            .IsRequired();

        builder.Property(a => a.UsuarioAlteracao)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => new { a.ProcessoId, a.DataCriacao });
    }
}