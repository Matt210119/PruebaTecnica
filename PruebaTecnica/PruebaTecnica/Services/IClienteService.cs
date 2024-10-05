using PruebaTecnica.Models;

namespace PruebaTecnica.Services;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByIdAsync(int id);
    Task AddAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
}

