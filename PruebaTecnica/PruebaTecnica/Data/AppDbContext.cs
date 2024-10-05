using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar la herencia TPT (Table-per-Type)
        modelBuilder.Entity<Persona>()
            .ToTable("Personas");

        modelBuilder.Entity<Cliente>()
            .ToTable("Clientes")
            .HasIndex(c => c.ClienteId)
            .IsUnique(); // ClienteId como único, pero no PK

        // Configurar Cuenta (PK y relaciones)
        modelBuilder.Entity<Cuenta>()
            .HasKey(c => c.CuentaId);
        modelBuilder.Entity<Cuenta>()
            .HasOne(c => c.Cliente)
            .WithMany()
            .HasForeignKey(c => c.ClienteId);

        // Configurar Movimiento (PK y relaciones)
        modelBuilder.Entity<Movimiento>()
            .HasKey(m => m.MovimientoId);
        modelBuilder.Entity<Movimiento>()
            .HasOne(m => m.Cuenta)
            .WithMany()
            .HasForeignKey(m => m.CuentaId);
    }
}
