using PruebaTecnica.Models;

namespace PruebaTecnica.Services;

public interface ICuentaService
{
    Task<IEnumerable<Cuenta>> GetAllAsync();
    Task<Cuenta> GetByIdAsync(int id);
    Task AddAsync(Cuenta cuenta);
    Task UpdateAsync(Cuenta cuenta);
    Task DeleteAsync(int id);
}

