using Integrativa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Integrativa.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Aqui vão os DbSets conforme você criar as entidades no Domain
    // Exemplo: public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Processo> Processos
    {
        get { return Set<Processo>(); }
    }
    public DbSet<Parte> Partes
    {
        get { return Set<Parte>(); }
    }
    public DbSet<Andamento> Andamentos
    {
        get { return Set<Andamento>(); }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica automaticamente todas as configurações de entidade (IEntityTypeConfiguration)
        // que estiverem nesse mesmo assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}