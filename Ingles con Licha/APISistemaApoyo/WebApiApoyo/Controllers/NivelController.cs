using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApoyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelController : ControllerBase
    {
        private readonly INivelService _nivelService;
        private readonly ILogger<NivelController> _logger;

        public NivelController(INivelService nivelService, ILogger<NivelController> logger)
        {
            _nivelService = nivelService;
            _logger = logger;
        }

        
        [HttpGet("lista")]
        public async Task<IActionResult> GetNiveles()
        {
            try
            {
                var niveles = await _nivelService.ListaNivel();
                return Ok(niveles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de niveles.");
                return StatusCode(500, "Ocurrió un error al obtener los niveles.");
            }
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNivelPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }

            try
            {
                var niveles = await _nivelService.NivelNombre(id);

                if (niveles == null )
                {
                    _logger.LogWarning("Nivel con ID {Id} no encontrado.", id);
                    return NotFound();
                }

                return Ok(niveles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el nivel con ID {Id}.", id);
                return StatusCode(500, "Ocurrió un error al obtener el nivel.");
            }
        }

        
        [HttpPost("crear")]
        public async Task<IActionResult> CrearNivel([FromBody] NivelDTO nivelDto)
        {
            if (nivelDto == null)
            {
                return BadRequest("Los datos del nivel son inválidos.");
            }

            try
            {
                var nuevoNivel = await _nivelService.CrearNivel(nivelDto);

                if (nuevoNivel == null)
                {
                    _logger.LogWarning("No se pudo crear el nivel.");
                    return StatusCode(500, "Ocurrió un error al crear el nivel.");
                }

                return CreatedAtAction(nameof(GetNivelPorId), new { id = nuevoNivel.Idnivel }, nuevoNivel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo nivel.");
                return StatusCode(500, "Ocurrió un error al crear el nivel.");
            }
        }

        // Actualizar un nivel existente
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarNivel(int id, [FromBody] NivelDTO nivelDto)
        {
            if (nivelDto == null || id != nivelDto.Idnivel)
            {
                return BadRequest("Los datos proporcionados no son válidos.");
            }

            try
            {
                var result = await _nivelService.ActualizarNivel(nivelDto);

                if (!result)
                {
                    _logger.LogWarning("Nivel con ID {Id} no encontrado para actualización.", id);
                    return NotFound("Nivel no encontrado.");
                }

                return Ok("Nivel actualizado con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el nivel con ID {Id}.", id);
                return StatusCode(500, "Ocurrió un error al actualizar el nivel.");
            }
        }

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarNivel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }

            try
            {
                var result = await _nivelService.EliminarNivel(id);

                if (!result)
                {
                    _logger.LogWarning("Nivel con ID {Id} no encontrado para eliminación.", id);
                    return NotFound("Nivel no encontrado.");
                }

                return Ok("Nivel eliminado con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el nivel con ID {Id}.", id);
                return StatusCode(500, "Ocurrió un error al eliminar el nivel.");
            }
        }
    }
}