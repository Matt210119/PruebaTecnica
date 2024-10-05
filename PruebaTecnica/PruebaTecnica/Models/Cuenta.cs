namespace PruebaTecnica.Models;

public class Cuenta
{
    public int CuentaId { get; set; } // Primary Key
    public string NumeroCuenta { get; set; } // Número de cuenta
    public string TipoCuenta { get; set; } // Tipo de cuenta (Ej: Ahorros, Corriente)
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; }

    // Relaciones
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
}
