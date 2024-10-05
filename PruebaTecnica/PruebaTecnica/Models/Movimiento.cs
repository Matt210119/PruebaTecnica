namespace PruebaTecnica.Models;

public class Movimiento
{
    public int MovimientoId { get; set; } // Primary Key
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string TipoMovimiento { get; set; } // Ej: Deposito, Retiro.
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }

    // Relaciones
    public int CuentaId { get; set; }
    public Cuenta Cuenta { get; set; }
}

