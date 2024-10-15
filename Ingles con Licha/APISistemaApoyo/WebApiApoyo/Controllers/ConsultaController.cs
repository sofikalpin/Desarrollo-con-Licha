using Microsoft.AspNetCore.Mvc;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ConsultaController : ControllerBase
{
    private readonly IConsultaService _consultaService;
    private readonly ILogger<ConsultaController> _logger;

    public ConsultaController(IConsultaService consultaService, ILogger<ConsultaController> logger)
    {
        _consultaService = consultaService;
        _logger = logger;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetConsultas()
    {
        try
        {
            var consultas = await _consultaService.ListaConsultas();
            return Ok(consultas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la lista de consultas.");
            return StatusCode(500, "Ocurrió un error al obtener las consultas.");
        }
    }

    
    [HttpGet("titulo/{titulo}/id/{id}")]
    public async Task<IActionResult> GetConsultaPorTitulo(string titulo, int id)
    {
        if (string.IsNullOrWhiteSpace(titulo) || id <= 0)
        {
            return BadRequest("El título o ID no son válidos.");
        }

        try
        {
            var consulta = await _consultaService.ConsultaPorTitulo(titulo, id);

            if (consulta == null)
            {
                _logger.LogWarning("Consulta con título {Titulo} y ID {Id} no encontrada.", titulo, id);
                return NotFound();
            }

            return Ok(consulta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la consulta por título o ID.");
            return StatusCode(500, "Ocurrió un error al obtener la consulta.");
        }
    }

   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsulta(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var consulta = await _consultaService.ObtenerConsultaAsync(id);

            if (consulta == null)
            {
                _logger.LogWarning("Consulta con ID {Id} no encontrada.", id);
                return NotFound();
            }

            return Ok(consulta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la consulta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al obtener la consulta.");
        }
    }

    
    [HttpPost]
    public async Task<IActionResult> PostConsulta([FromBody] ConsultaDTO consultaDto)
    {
        if (consultaDto == null)
        {
            return BadRequest("Los datos de la consulta son inválidos.");
        }

        try
        {
            var resultado = await _consultaService.CrearConsulta(consultaDto);

            if (!resultado)
            {
                _logger.LogWarning("No se pudo crear la consulta.");
                return StatusCode(500, "Ocurrió un error al crear la consulta.");
            }

            return Ok("Consulta creada con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear una nueva consulta.");
            return StatusCode(500, "Ocurrió un error al crear la consulta.");
        }
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConsulta(int id, [FromBody] ConsultaDTO consultaDto)
    {
        if (id != consultaDto.Idconsulta)
        {
            return BadRequest("El ID de la consulta no coincide con el ID proporcionado.");
        }

        try
        {
            var resultado = await _consultaService.ActualizarConsulta(consultaDto);

            if (!resultado)
            {
                _logger.LogWarning("Consulta con ID {Id} no encontrada para actualización.", id);
                return NotFound();
            }

            return Ok("Consulta actualizada con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la consulta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al actualizar la consulta.");
        }
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsulta(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var resultado = await _consultaService.EliminarConsulta(id);

            if (!resultado)
            {
                _logger.LogWarning("Consulta con ID {Id} no encontrada para eliminación.", id);
                return NotFound();
            }

            return Ok("Consulta eliminada con éxito.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la consulta con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al eliminar la consulta.");
        }
    }
}