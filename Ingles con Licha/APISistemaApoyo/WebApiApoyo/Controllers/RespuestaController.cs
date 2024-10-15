using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.Model.Models;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class RespuestaController : ControllerBase
{
    private readonly IRespuestaService _respuestaService;
    private readonly ILogger<RespuestaController> _logger;

    public RespuestaController(IRespuestaService respuestaService, ILogger<RespuestaController> logger)
    {
        _respuestaService = respuestaService;
        _logger = logger;
    }

    
    [HttpGet]
    public IActionResult GetRespuestas()
    {
        try
        {
            var respuestas = _respuestaService.ObtenerRespuestas();
            return Ok(respuestas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las respuestas.");
            return StatusCode(500, "Ocurrió un error al obtener las respuestas.");
        }
    }

    
    [HttpGet("{id}")]
    public IActionResult GetRespuesta(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var respuesta = _respuestaService.ObtenerRespuestaPorId(id);
            if (respuesta == null)
            {
                _logger.LogWarning("Respuesta con ID {Id} no encontrada.", id);
                return NotFound();
            }

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la respuesta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al obtener la respuesta.");
        }
    }

    
    [HttpPost]
    public IActionResult CrearRespuesta([FromBody] Respuesta nuevaRespuesta)
    {
        if (nuevaRespuesta == null)
        {
            return BadRequest("Los datos de la respuesta son inválidos.");
        }

        try
        {
            _respuestaService.CrearRespuesta(nuevaRespuesta);
            return CreatedAtAction(nameof(GetRespuesta), new { id = nuevaRespuesta.Idrespuesta }, nuevaRespuesta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear una nueva respuesta.");
            return StatusCode(500, "Ocurrió un error al crear la respuesta.");
        }
    }

    
    [HttpPut("{id}")]
    public IActionResult ActualizarRespuesta(int id, [FromBody] Respuesta respuestaActualizada)
    {
        if (respuestaActualizada == null || id != respuestaActualizada.Idrespuesta)
        {
            return BadRequest("Los datos proporcionados no son válidos.");
        }

        try
        {
            var respuestaExistente = _respuestaService.ObtenerRespuestaPorId(id);
            if (respuestaExistente == null)
            {
                _logger.LogWarning("No se pudo encontrar una respuesta con ID {Id} para actualizar.", id);
                return NotFound();
            }

            _respuestaService.ActualizarRespuesta(respuestaActualizada);
            return Ok("Respuesta actualizada con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la respuesta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al actualizar la respuesta.");
        }
    }

    
    [HttpDelete("{id}")]
    public IActionResult EliminarRespuesta(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var respuestaExistente = _respuestaService.ObtenerRespuestaPorId(id);
            if (respuestaExistente == null)
            {
                _logger.LogWarning("No se pudo encontrar una respuesta con ID {Id} para eliminar.", id);
                return NotFound();
            }

            _respuestaService.EliminarRespuesta(id);
            return Ok("Respuesta eliminada con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la respuesta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al eliminar la respuesta.");
        }
    }
}