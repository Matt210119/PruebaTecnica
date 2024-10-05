using PruebaTecnica.Models;
using PruebaTecnica.Repositories;

namespace PruebaTecnica.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _cuentaRepository;

    public CuentaService(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<IEnumerable<Cuenta>> GetAllAsync()
    {
        return await _cuentaRepository.GetAllAsync();
    }

    public async Task<Cuenta> GetByIdAsync(int id)
    {
        return await _cuentaRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Cuenta cuenta)
    {
        await _cuentaRepository.AddAsync(cuenta);
    }

    public async Task UpdateAsync(Cuenta cuenta)
    {
        await _cuentaRepository.UpdateAsync(cuenta);
    }

    public async Task DeleteAsync(int id)
    {
        await _cuentaRepository.DeleteAsync(id);
    }
}

