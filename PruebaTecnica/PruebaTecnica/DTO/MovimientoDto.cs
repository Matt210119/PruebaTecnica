using PruebaTecnica.Models;

namespace PruebaTecnica.DTO;

public class MovimientoDto
{
    public string TipoMovimiento { get; set; } // Ej: Deposito, Retiro.
    public decimal Valor { get; set; }
    public int CuentaId { get; set; }
}
