using PruebaTecnica.DTO;
using PruebaTecnica.Models;
using PruebaTecnica.Repositories;

namespace PruebaTecnica.Services;

public class MovimientoService : IMovimientoService
{
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ICuentaRepository _cuentaRepository;

    public MovimientoService(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository)
    {
        _movimientoRepository = movimientoRepository;
        _cuentaRepository = cuentaRepository;
    }

    public async Task<IEnumerable<Movimiento>> GetAllAsync()
    {
        return await _movimientoRepository.GetAllAsync();
    }

    public async Task<Movimiento> GetByIdAsync(int id)
    {
        return await _movimientoRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(MovimientoDto movimientoDto)
    {
        // Obtener la cuenta y validar que existe
        var cuenta = await _cuentaRepository.GetByIdAsync(movimientoDto.CuentaId);
        var ultimoMovimiento = await _movimientoRepository.GetUltimoMovimientoByCuentaIdAsync(movimientoDto.CuentaId);
        if (cuenta == null)
        {
            throw new Exception("Cuenta no encontrada");
        }

        // Crear la entidad Movimiento con los datos del DTO
        var movimiento = new Movimiento
        {
            CuentaId = movimientoDto.CuentaId,
            TipoMovimiento = movimientoDto.TipoMovimiento,
            Valor = movimientoDto.Valor
        };

        // Actualizar el saldo dependiendo del tipo de movimiento
        if (movimiento.TipoMovimiento == "Deposito")
        {
            movimiento.Saldo = (ultimoMovimiento == null) ? cuenta.SaldoInicial + movimiento.Valor : ultimoMovimiento.Saldo + movimiento.Valor;
        }
        else if (movimiento.TipoMovimiento == "Retiro")
        {
            movimiento.Saldo = (ultimoMovimiento == null) ? cuenta.SaldoInicial - movimiento.Valor : ultimoMovimiento.Saldo - movimiento.Valor;

            // Verificar si hay fondos suficientes
            if (movimiento.Saldo < 0)
            {
                throw new Exception("Saldo No Disponible");
            }
        }
        else
        {
            throw new Exception("Tipo de movimiento no válido");
        }

        // Guardar el movimiento
        await _movimientoRepository.AddAsync(movimiento);
    }
}

