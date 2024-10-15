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
    public class ProfesorArticuloController : ControllerBase
    {
        private readonly IProfesorArticulo _profesorArticulo;
        private readonly ILogger<ProfesorArticuloController> _logger;

        public ProfesorArticuloController(IProfesorArticulo profesorArticulo, ILogger<ProfesorArticuloController> logger)
        {
            _profesorArticulo = profesorArticulo;
            _logger = logger;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<ArticuloDTO>> Crear(ArticuloDTO articulo)
        {
            var rsp = new Response<ArticuloDTO>();

            try
            {
                if (articulo == null)
                {
                    rsp.status = false;
                    rsp.msg = "Datos del artículo no proporcionados";
                    return BadRequest(rsp);
                }

                var articuloCreado = await _profesorArticulo.Crear(articulo);
                rsp.status = true;
                rsp.value = articuloCreado;
                rsp.msg = "Artículo creado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el artículo.");
                rsp.status = false;
                rsp.msg = $"Error al crear el artículo: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(ArticuloDTO articulo)
        {
            var rsp = new Response<bool>();

            try
            {
                var resultado = await _profesorArticulo.Editar(articulo);
                rsp.status = resultado;

                if (!resultado)
                {
                    rsp.msg = "El artículo no fue encontrado.";
                    return NotFound(rsp);
                }

                rsp.msg = "Artículo actualizado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el artículo.");
                rsp.status = false;
                rsp.msg = $"Error al editar el artículo: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ArticuloDTO>>();

            try
            {
                var lista = await _profesorArticulo.Lista();
                rsp.status = true;
                rsp.value = lista;
                rsp.msg = "Artículos listados correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar los artículos.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al listar los artículos: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpGet("Lista/{idArticulo:int}")]
        public async Task<ActionResult<ArticuloDTO>> Lista(int idArticulo)
        {
            var rsp = new Response<ArticuloDTO>();

            try
            {
                var listaArticulos = await _profesorArticulo.Lista(idArticulo);

                if (listaArticulos == null || !listaArticulos.Any())
                {
                    rsp.status = false;
                    rsp.msg = "Artículo no encontrado.";
                    return NotFound(rsp);
                }

                rsp.status = true;
                rsp.value = listaArticulos.FirstOrDefault(a => a.Idarticulo == idArticulo);
                rsp.msg = "Artículo encontrado con éxito";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el artículo.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al obtener el artículo: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }

        [HttpDelete("Eliminar/{idArticulo:int}")]
        public async Task<IActionResult> Eliminar(int idArticulo)
        {
            var rsp = new Response<bool>();

            try
            {
                var resultado = await _profesorArticulo.Eliminar(idArticulo);
                rsp.status = resultado;

                if (!resultado)
                {
                    rsp.msg = "El artículo no fue encontrado.";
                    return NotFound(rsp);
                }

                rsp.msg = "Artículo eliminado correctamente";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el artículo.");
                rsp.status = false;
                rsp.msg = $"Ocurrió un error al eliminar el artículo: {ex.Message}";
                return StatusCode(500, rsp);
            }

            return Ok(rsp);
        }
    }
}