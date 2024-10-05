using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly AppDbContext _context;

    public CuentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cuenta>> GetAllAsync()
    {
        return await _context.Cuentas.Include(c => c.Cliente).ToListAsync();
    }

    public async Task<Cuenta> GetByIdAsync(int id)
    {
        return await _context.Cuentas.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.CuentaId == id);
    }

    public async Task AddAsync(Cuenta cuenta)
    {
        await _context.Cuentas.AddAsync(cuenta);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cuenta cuenta)
    {
        _context.Entry(cuenta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cuenta = await _context.Cuentas.FindAsync(id);
        if (cuenta != null)
        {
            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Cuenta>> GetCuentasByClienteIdAsync(int clienteId)
    {
        return await _context.Cuentas
                             .Where(c => c.ClienteId == clienteId)
                             .ToListAsync();
    }
}

