using CrmFinanceiro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmFinanceiro.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<FinanceiroCaixa> TitulosFinanceiros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<HistoricoAcao> HistoricoAcoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FinanceiroCaixa>()
            .Property(f => f.Valor)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Pessoa>()
            .Property(p => p.CpfCnpj)
            .HasMaxLength(14);

        modelBuilder.Entity<Pessoa>()
            .Property(p => p.Nome)
            .HasMaxLength(250);
    }
}
