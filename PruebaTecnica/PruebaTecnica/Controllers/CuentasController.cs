using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentasController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
    {
        return Ok(await _cuentaService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cuenta>> GetCuenta(int id)
    {
        var cuenta = await _cuentaService.GetByIdAsync(id);
        if (cuenta == null) return NotFound();
        return Ok(cuenta);
    }

    [HttpPost]
    public async Task<ActionResult<Cuenta>> PostCuenta(CreateCuentaDto createCuentaDto)
    {
        // Crear la entidad Cuenta desde el DTO
        var cuenta = new Cuenta
        {
            ClienteId = createCuentaDto.ClienteId,
            NumeroCuenta = createCuentaDto.NumeroCuenta,
            TipoCuenta = createCuentaDto.TipoCuenta,
            SaldoInicial = createCuentaDto.SaldoInicial,
            Estado = createCuentaDto.Estado
        };

        // Llamar al servicio para agregar la cuenta
        await _cuentaService.AddAsync(cuenta);

        // Retornar la cuenta creada con su ID
        return CreatedAtAction(nameof(GetCuenta), new { id = cuenta.CuentaId }, cuenta);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> PutCuenta(int id, UpdateCuentaDto updateCuentaDto)
    {
        // Obtener la cuenta existente
        var existingCuenta = await _cuentaService.GetByIdAsync(id);
        if (existingCuenta == null) return NotFound();

        // Actualizar los campos permitidos en la cuenta existente
        existingCuenta.TipoCuenta = updateCuentaDto.TipoCuenta;
        existingCuenta.Estado = updateCuentaDto.Estado;

        // Llamar al servicio para actualizar la cuenta
        await _cuentaService.UpdateAsync(existingCuenta);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCuenta(int id)
    {
        await _cuentaService.DeleteAsync(id);
        return NoContent();
    }
}

