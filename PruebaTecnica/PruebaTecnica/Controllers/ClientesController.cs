using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.DTO;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        return Ok(await _clienteService.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] CreateClienteDto createClienteDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Generar ClienteId único
            int clienteId;
            Cliente clienteB = null;
            do
            {
                clienteId = new Random().Next(1, 100000);  // Generar un valor entre 1 y 100000
                clienteB = await _clienteService.GetByIdAsync(clienteId);
            }
            while (clienteB != null);  // Verificar si ya existe en la base de datos

            // Mapeo del DTO a la entidad Cliente
            var cliente = new Cliente
            {
                ClienteId = clienteId,
                Nombre = createClienteDto.Nombre,
                Genero = createClienteDto.Genero,
                Edad = createClienteDto.Edad,
                Identificacion = createClienteDto.Identificacion,
                Direccion = createClienteDto.Direccion,
                Telefono = createClienteDto.Telefono,
                Contrasena = createClienteDto.Contraseña,
                Estado = createClienteDto.Estado
            };

            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
        }
        catch (Exception ex)
        {
            // Manejo de la excepción y retorno de un error 500
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "Ocurrió un error al procesar la solicitud.",
                details = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutCliente(int id, [FromBody] UpdateClienteDto updateClienteDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteExistente = await _clienteService.GetByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound(new { message = "Cliente no encontrado" });
            }

            // Actualizar los campos del cliente
            clienteExistente.Nombre = updateClienteDto.Nombre;
            clienteExistente.Genero = updateClienteDto.Genero;
            clienteExistente.Edad = updateClienteDto.Edad;
            clienteExistente.Identificacion = updateClienteDto.Identificacion;
            clienteExistente.Direccion = updateClienteDto.Direccion;
            clienteExistente.Telefono = updateClienteDto.Telefono;
            clienteExistente.Contrasena = updateClienteDto.Contraseña;
            clienteExistente.Estado = updateClienteDto.Estado;

            await _clienteService.UpdateAsync(clienteExistente);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                message = "Ocurrió un error al procesar la solicitud.",
                details = ex.Message
            });
        }
    }

    // Método existente para obtener un cliente por id
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound(new { message = "Cliente no encontrado" });
        }

        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCliente(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound(new { message = "Cliente no encontrado" });
        }

        await _clienteService.DeleteAsync(id);
        return NoContent();
    }
}

