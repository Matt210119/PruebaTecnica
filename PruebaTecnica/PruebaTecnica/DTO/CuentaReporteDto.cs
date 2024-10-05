namespace PruebaTecnica.DTO;

public class CuentaReporteDto
{
    public string NumeroCuenta { get; set; }
    public string TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public List<MovimientoReporteDto> Movimientos { get; set; }
}

