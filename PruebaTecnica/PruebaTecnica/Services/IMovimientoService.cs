using PruebaTecnica.DTO;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services;

public interface IMovimientoService
{
    Task<IEnumerable<Movimiento>> GetAllAsync();
    Task<Movimiento> GetByIdAsync(int id);
    Task AddAsync(MovimientoDto movimientoDto);
    
}

