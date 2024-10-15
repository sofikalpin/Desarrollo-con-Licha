using SistemaApoyo.Model;
using Microsoft.AspNetCore.Mvc;
using SistemaApoyo.BLL.Servicios.Contrato;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SistemaApoyo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenService _examenService;
        private readonly ILogger<ExamenController> _logger;

        public ExamenController(IExamenService examenService, ILogger<ExamenController> logger)
        {
            _examenService = examenService;
            _logger = logger;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var examenes = await _examenService.ListaExamen();
                return Ok(new { mensaje = "Todo correcto", Response = examenes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la lista de exámenes.");
                return StatusCode(500, new { mensaje = "No se pudo cargar la lista", ex.Message });
            }
        }

        [HttpGet("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { mensaje = "ID no válido." });
            }

            try
            {
                var examen = await _examenService.ObtenerExamenAsync(id);
                if (examen == null)
                {
                    return NotFound(new { mensaje = "Examen no encontrado." });
                }
                return Ok(new { mensaje = "Todo correcto", Response = examen });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el examen con ID {id}.");
                return StatusCode(500, new { mensaje = "No se pudo cargar el examen", ex.Message });
            }
        }

        [HttpGet("nombre/{nombre}/id/{id}")]
        public async Task<IActionResult> GetExamenPorNombre(string nombre, int id)
        {
            if (string.IsNullOrWhiteSpace(nombre) || id <= 0)
            {
                return BadRequest(new { mensaje = "El nombre o ID no son válidos." });
            }

            try
            {
                var examenes = await _examenService.ExamenNombre(nombre, id);
                return Ok(new { mensaje = "Todo correcto", Response = examenes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener exámenes por nombre: {nombre} y ID: {id}.");
                return StatusCode(500, new { mensaje = "Error al obtener los exámenes", ex.Message });
            }
        }
    }
}