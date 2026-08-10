using Microsoft.EntityFrameworkCore;

namespace Integrativa.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Aqui vão os DbSets conforme você criar as entidades no Domain
    // Exemplo: public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica automaticamente todas as configurações de entidade (IEntityTypeConfiguration)
        // que estiverem nesse mesmo assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}