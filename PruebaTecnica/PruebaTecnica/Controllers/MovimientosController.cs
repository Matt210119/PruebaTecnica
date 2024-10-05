using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientoController : ControllerBase
{
    private readonly IMovimientoService _movimientoService;

    public MovimientoController(IMovimientoService movimientoService)
    {
        _movimientoService = movimientoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
    {
        return Ok(await _movimientoService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
    {
        var movimiento = await _movimientoService.GetByIdAsync(id);
        if (movimiento == null) return NotFound();
        return Ok(movimiento);
    }

    [HttpPost]
    public async Task<IActionResult> AddMovimiento([FromBody] MovimientoDto movimientoDto)
    {
        try
        {
            await _movimientoService.AddAsync(movimientoDto);
            return Ok(new { message = "Movimiento registrado exitosamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

