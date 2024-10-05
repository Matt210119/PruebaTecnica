using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet]
    public async Task<ActionResult<ReporteEstadoCuentaDto>> GetReporteEstadoCuenta(int clienteId, DateTime fechaInicio, DateTime fechaFin)
    {
        try
        {
            var reporte = await _reporteService.GenerarReporteEstadoCuentaAsync(clienteId, fechaInicio, fechaFin);
            return Ok(reporte);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

