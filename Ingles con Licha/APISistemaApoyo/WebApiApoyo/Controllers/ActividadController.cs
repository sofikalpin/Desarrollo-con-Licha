using Microsoft.AspNetCore.Mvc;
using SistemaApoyo.BLL.Servicios;
using SistemaApoyo.BLL.Servicios.Contrato;

[Route("api/[controller]")]
[ApiController]
public class ActividadController : ControllerBase
{
    private readonly IActividadService _actividadService;
    private readonly ILogger<ActividadController> _logger;

    public ActividadController(IActividadService actividadService, ILogger<ActividadController> logger)
    {
        _actividadService = actividadService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetActividades()
    {
        try
        {
            var actividades = await _actividadService.ListaActividad();
            return Ok(actividades);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en obtener la lista de actividades.");
            return StatusCode(500, "Ocurrió un error al obtener las actividades.");
        }
    }

    [HttpGet("nombre/{nombre}/id/{id}")]
    public async Task<IActionResult> GetActividadPorNombre(string nombre, int id)
    {
        if (string.IsNullOrWhiteSpace(nombre) || id <= 0)
        {
            return BadRequest("El nombre o ID no son válidos.");
        }

        try
        {
            var actividades = await _actividadService.ActividadNombre(nombre, id);
            return Ok(actividades);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en obtener la actividad por nombre o ID.");
            return StatusCode(500, "Ocurrió un error al obtener la actividad.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActividad(int id)
    {
        if (id <= 0)
        {
            return BadRequest("El ID proporcionado no es válido.");
        }

        try
        {
            var actividad = await _actividadService.ObtenerActividadAsync(id);

            if (actividad == null)
            {
                _logger.LogWarning("Actividad con ID {Id} no encontrada.", id);
                return NotFound();
            }

            return Ok(actividad);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la actividad con ID {Id}.", id);
            return StatusCode(500, "Ocurrió un error al obtener la actividad.");
        }
    }



}