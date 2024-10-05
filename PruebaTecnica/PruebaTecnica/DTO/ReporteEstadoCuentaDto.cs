namespace PruebaTecnica.DTO;

public class ReporteEstadoCuentaDto
{
    public string ClienteNombre { get; set; }
    public List<CuentaReporteDto> Cuentas { get; set; }
}

