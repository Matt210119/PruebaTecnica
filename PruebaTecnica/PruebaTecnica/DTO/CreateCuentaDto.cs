namespace PruebaTecnica.DTO;

public class CreateCuentaDto
{
    public int ClienteId { get; set; }
    public string NumeroCuenta { get; set; }
    public string TipoCuenta { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; }
}

