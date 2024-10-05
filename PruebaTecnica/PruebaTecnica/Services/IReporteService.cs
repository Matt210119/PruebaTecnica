using PruebaTecnica.DTO;

namespace PruebaTecnica.Services;

public interface IReporteService
{
    Task<ReporteEstadoCuentaDto> GenerarReporteEstadoCuentaAsync(int clienteId, DateTime fechaInicio, DateTime fechaFin);
}
