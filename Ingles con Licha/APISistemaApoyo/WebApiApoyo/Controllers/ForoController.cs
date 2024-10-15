using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ForoController : ControllerBase
{
    private readonly IForoService _foroService;
    private readonly ILogger<ForoController> _logger;

    public ForoController(IForoService foroService, ILogger<ForoController> logger)
    {
        _foroService = foroService;
        _logger = logger;
    }

    
    [HttpPost]
    public async Task<IActionResult> CrearForo([FromBody] ForoDTO foroDto)
    {
        if (foroDto == null)
        {
            return BadRequest("Los datos del foro son inválidos.");
        }

        try
        {
            var foroCreado = await _foroService.CrearForo(foroDto);

            if (foroCreado == null)
            {
                _logger.LogWarning("No se pudo crear el foro.");
                return StatusCode(500, "Ocurrió un error al crear el foro.");
            }

            return Ok(foroCreado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear un nuevo foro.");
            return StatusCode(500, "Ocurrió un error al crear el foro.");
        }
    }

    
    [HttpGet]
    public async Task<IActionResult> ObtenerForos([FromQuery] string criterio)
    {
        try
        {
            Expression<Func<Foro, bool>> filtro = null;
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtro = f => f.Nombre.Contains(criterio);  
            }

            var foros = await _foroService.ObtenerForos(filtro);
            return Ok(foros);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la lista de foros.");
            return StatusCode(500, "Ocurrió un error al obtener los foros.");
        }
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerForoPorId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var foro = await _foroService.ObtenerForoPorId(id);

            if (foro == null)
            {
                _logger.LogWarning("Foro con ID {Id} no encontrado.", id);
                return NotFound();
            }

            return Ok(foro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el foro con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al obtener el foro.");
        }
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarForo(int id, [FromBody] ForoDTO foroDto)
    {
        if (foroDto == null || id != foroDto.IdForo)
        {
            return BadRequest("Los datos proporcionados no son válidos.");
        }

        try
        {
            await _foroService.ActualizarForo(foroDto);
            return Ok("Foro actualizado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar el foro con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al actualizar el foro.");
        }
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarForo(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            await _foroService.EliminarForo(id);
            return Ok("Foro eliminado con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el foro con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al eliminar el foro.");
        }
    }
}