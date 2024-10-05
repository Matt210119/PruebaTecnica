using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly AppDbContext _context;

    public MovimientoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movimiento>> GetAllAsync()
    {
        return await _context.Movimientos.Include(m => m.Cuenta).ToListAsync();
    }

    public async Task<Movimiento> GetByIdAsync(int id)
    {
        return await _context.Movimientos.FindAsync(id);
    }

    public async Task AddAsync(Movimiento movimiento)
    {
        await _context.Movimientos.AddAsync(movimiento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Movimiento movimiento)
    {
        _context.Entry(movimiento).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var movimiento = await _context.Movimientos.FindAsync(id);
        if (movimiento != null)
        {
            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movimiento>> GetMovimientosByCuentaAndFechaAsync(int cuentaId, DateTime fechaInicio, DateTime fechaFin)
    {
        return await _context.Movimientos
                                 .Where(m => m.CuentaId == cuentaId
                                          && m.Fecha.Date >= fechaInicio.Date
                                          && m.Fecha.Date <= fechaFin.Date)
                                 .ToListAsync();
    }

    // Nuevo método para obtener el último movimiento por cuenta
    public async Task<Movimiento> GetUltimoMovimientoByCuentaIdAsync(int cuentaId)
    {
        return await _context.Movimientos
                             .Where(m => m.CuentaId == cuentaId)
                             .OrderByDescending(m => m.MovimientoId) // Ordenar por fecha descendente
                             .FirstOrDefaultAsync(); // Obtener el primer (último) movimiento
    }
}

