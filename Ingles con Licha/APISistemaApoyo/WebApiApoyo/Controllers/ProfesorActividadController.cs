using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.API.Utilidad;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiApoyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorActividadController : ControllerBase
    {
        private readonly IProfesorActividad _profesorActividadService;
        private readonly ILogger<ProfesorActividadController> _logger;

        public ProfesorActividadController(IProfesorActividad profesorActividadService, ILogger<ProfesorActividadController> logger)
        {
            _profesorActividadService = profesorActividadService;
            _logger = logger;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear(ActividadDTO actividad)
        {
            var rsp = new Response<ActividadDTO>();
            try
            {
                if (actividad == null)
                {
                    rsp.status = false;
                    rsp.msg = "Datos de la actividad no proporcionados.";
                    _logger.LogWarning("Intento de crear actividad sin datos.");
                    return BadRequest(rsp);
                }

                var actividadCreada = await _profesorActividadService.Crear(actividad);
                rsp.status = true;
                rsp.value = actividadCreada;
                rsp.msg = "Actividad creada exitosamente.";
                _logger.LogInformation("Actividad creada exitosamente con ID: {ActividadId}", actividadCreada.Idactividad);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Error al crear la actividad: {ex.Message}";
                _logger.LogError(ex, "Error al crear la actividad.");
                return StatusCode(500, rsp);
            }
            return Ok(rsp);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar(ActividadDTO actividad)
        {
            var rsp = new Response<bool>();
            try
            {
                if (actividad == null)
                {
                    rsp.status = false;
                    rsp.msg = "Datos de la actividad no proporcionados.";
                    _logger.LogWarning("Intento de editar actividad sin datos.");
                    return BadRequest(rsp);
                }

                rsp.value = await _profesorActividadService.Editar(actividad);
                rsp.status = rsp.value;

                if (!rsp.value)
                {
                    rsp.msg = "La actividad no fue encontrada.";
                    _logger.LogWarning("Actividad no encontrada para edición con ID: {ActividadId}", actividad.Idactividad);
                    return NotFound(rsp);
                }
                rsp.msg = "Actividad actualizada exitosamente.";
                _logger.LogInformation("Actividad actualizada exitosamente con ID: {ActividadId}", actividad.Idactividad);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Error al editar la actividad: {ex.Message}";
                _logger.LogError(ex, "Error al editar la actividad.");
                return StatusCode(500, rsp);
            }
            return Ok(rsp);
        }

        [HttpGet("lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ActividadDTO>>();
            try
            {
                rsp.value = await _profesorActividadService.Lista();
                rsp.status = true;
                rsp.msg = "Actividades listadas correctamente.";
                _logger.LogInformation("Lista de actividades obtenida correctamente.");
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Ocurrió un error: {ex.Message}";
                _logger.LogError(ex, "Error al listar actividades.");
                return StatusCode(500, rsp);
            }
            return Ok(rsp);
        }

        [HttpGet("lista/{idActividad:int}")]
        public async Task<IActionResult> ObtenerActividadPorId(int idActividad)
        {
            var rsp = new Response<ActividadDTO>();
            try
            {
                var actividad = await _profesorActividadService.ObtenerPorId(idActividad); // Cambiado a ObtenerPorId
                if (actividad == null)
                {
                    rsp.status = false;
                    rsp.msg = "Actividad no encontrada.";
                    _logger.LogWarning("Actividad no encontrada con ID: {ActividadId}", idActividad);
                    return NotFound(rsp);
                }

                rsp.value = actividad;
                rsp.status = true;
                rsp.msg = "Actividad obtenida correctamente.";
                _logger.LogInformation("Actividad obtenida con éxito con ID: {ActividadId}", idActividad);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Ocurrió un error: {ex.Message}";
                _logger.LogError(ex, "Error al obtener actividad por ID.");
                return StatusCode(500, rsp);
            }
            return Ok(rsp);
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.value = await _profesorActividadService.Eliminar(id);
                rsp.status = rsp.value;

                if (!rsp.value)
                {
                    rsp.msg = "Actividad no encontrada.";
                    _logger.LogWarning("Actividad no encontrada para eliminación con ID: {ActividadId}", id);
                    return NotFound(rsp);
                }

                rsp.msg = "Actividad eliminada correctamente.";
                _logger.LogInformation("Actividad eliminada correctamente con ID: {ActividadId}", id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = $"Ocurrió un error: {ex.Message}";
                _logger.LogError(ex, "Error al eliminar actividad.");
                return StatusCode(500, rsp);
            }
            return Ok(rsp);
        }
    }
}
