using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories;

public interface ICuentaRepository
{
    Task<IEnumerable<Cuenta>> GetAllAsync();
    Task<Cuenta> GetByIdAsync(int id);
    Task AddAsync(Cuenta cuenta);
    Task UpdateAsync(Cuenta cuenta);
    Task DeleteAsync(int id);
    Task<IEnumerable<Cuenta>> GetCuentasByClienteIdAsync(int clienteId);
}

