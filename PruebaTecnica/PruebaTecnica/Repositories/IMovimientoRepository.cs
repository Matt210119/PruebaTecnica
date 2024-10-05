using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories;

public interface IMovimientoRepository
{
    Task<IEnumerable<Movimiento>> GetAllAsync();
    Task<Movimiento> GetByIdAsync(int id);
    Task AddAsync(Movimiento movimiento);
    Task UpdateAsync(Movimiento movimiento);
    Task DeleteAsync(int id);
    Task<IEnumerable<Movimiento>> GetMovimientosByCuentaAndFechaAsync(int cuentaId, DateTime fechaInicio, DateTime fechaFin);
    Task<Movimiento> GetUltimoMovimientoByCuentaIdAsync(int cuentaId);
}

