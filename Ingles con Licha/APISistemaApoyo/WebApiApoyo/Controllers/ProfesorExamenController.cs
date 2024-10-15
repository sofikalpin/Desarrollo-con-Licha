using Microsoft.AspNetCore.Mvc;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.API.Utilidad;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApoyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfeExamenController : ControllerBase
    {
        private readonly IProfesorExamen _profesorExamen;
        private readonly ILogger<ProfeExamenController> _logger;

        public ProfeExamenController(IProfesorExamen profesorExamen, ILogger<ProfeExamenController> logger)
        {
            _profesorExamen = profesorExamen;
            _logger = logger;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<ExamenDTO>> Crear(ExamenDTO examen)
        {
            var rsp = new Response<ExamenDTO>();
            try
            {
                if (examen == null)
                {
                    rsp.status = false;
                    rsp.msg = "Datos del examen no proporcionados";
                    return BadRequest(rsp);
                }

                var examenCreado = await _profesorExamen.Crear(examen);
                rsp.status = true;
                rsp.value = examenCreado;
                rsp.msg = "Examen creado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el examen.");
                rsp.status = false;
                rsp.msg = $"Error al crear el examen: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(ExamenDTO examen)
        {
            var rsp = new Response<bool>();
            try
            {
                var resultado = await _profesorExamen.Editar(examen);
                rsp.status = resultado;

                if (!resultado)
                {
                    rsp.msg = "El examen no fue encontrado.";
                    return NotFound(rsp);
                }

                rsp.msg = "Examen actualizado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el examen.");
                rsp.status = false;
                rsp.msg = $"Error al editar el examen: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ExamenDTO>>();

            try
            {
                var examenes = await _profesorExamen.Lista();
                rsp.status = true;
                rsp.value = examenes;
                rsp.msg = "Exámenes listados correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar los exámenes.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al listar los exámenes: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpGet("ListaExamen/{idExamen:int}")]
        public async Task<ActionResult<ExamenDTO>> ListaExamen(int idExamen)
        {
            var rsp = new Response<ExamenDTO>();

            try
            {
                var listaExamenes = await _profesorExamen.Lista(idExamen);

                if (listaExamenes == null || !listaExamenes.Any())
                {
                    rsp.status = false;
                    rsp.msg = "Examen no encontrado.";
                    return NotFound(rsp);
                }

                rsp.status = true;
                rsp.value = listaExamenes.FirstOrDefault(e => e.id_Examen == idExamen);
                rsp.msg = "Examen encontrado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el examen.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al obtener el examen: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpDelete("DeleteExamen/{idExamen:int}")]
        public async Task<IActionResult> DeleteExamen(int idExamen)
        {
            var rsp = new Response<bool>();

            try
            {
                var resultado = await _profesorExamen.Eliminar(idExamen);
                rsp.status = resultado;

                if (!resultado)
                {
                    rsp.msg = "El examen no fue encontrado.";
                    return NotFound(rsp);
                }

                rsp.msg = "Examen eliminado correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el examen.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al eliminar el examen: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }
    }
}