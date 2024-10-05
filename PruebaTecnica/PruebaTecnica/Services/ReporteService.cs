using PruebaTecnica.DTO;
using PruebaTecnica.Repositories;

namespace PruebaTecnica.Services;

public class ReporteService : IReporteService
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly IClienteRepository _clienteRepository;

    public ReporteService(ICuentaRepository cuentaRepository, IMovimientoRepository movimientoRepository, IClienteRepository clienteRepository)
    {
        _cuentaRepository = cuentaRepository;
        _movimientoRepository = movimientoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<ReporteEstadoCuentaDto> GenerarReporteEstadoCuentaAsync(int clienteId, DateTime fechaInicio, DateTime fechaFin)
    {
        // Obtener el cliente
        var cliente = await _clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null)
        {
            throw new Exception("Cliente no encontrado");
        }

        // Obtener las cuentas del cliente
        var cuentas = await _cuentaRepository.GetCuentasByClienteIdAsync(clienteId);

        // Preparar la respuesta
        var reporte = new ReporteEstadoCuentaDto
        {
            ClienteNombre = cliente.Nombre,
            Cuentas = new List<CuentaReporteDto>()
        };

        foreach (var cuenta in cuentas)
        {
            // Obtener movimientos en el rango de fechas
            var movimientos = await _movimientoRepository.GetMovimientosByCuentaAndFechaAsync(cuenta.CuentaId, fechaInicio, fechaFin);

            // Agregar la información de la cuenta y sus movimientos al reporte
            var cuentaReporteDto = new CuentaReporteDto
            {
                NumeroCuenta = cuenta.CuentaId,
                TipoCuenta = cuenta.TipoCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Movimientos = movimientos.Select(m => new MovimientoReporteDto
                {
                    Fecha = m.Fecha,
                    TipoMovimiento = m.TipoMovimiento,
                    Valor = m.Valor,
                    Saldo = m.Saldo
                }).ToList()
            };

            reporte.Cuentas.Add(cuentaReporteDto);
        }

        return reporte;
    }
}

