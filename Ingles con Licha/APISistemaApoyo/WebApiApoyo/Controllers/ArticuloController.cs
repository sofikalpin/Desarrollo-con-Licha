using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.Servicios;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using System;
using System.Threading.Tasks;

namespace SistemaApoyo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;
        private readonly ILogger<ArticuloController> _logger;

        public ArticuloController(IArticuloService articuloService, ILogger<ArticuloController> logger)
        {
            _articuloService = articuloService;
            _logger = logger;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Lista()
        {
            try
            {
                var lista = await _articuloService.ListaArticulo();
                return Ok(new { mensaje = "Todo correcto", Response = lista });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la lista de artículos.");
                return StatusCode(500, new { mensaje = "No se pudo cargar", ex.Message });
            }
        }

        [HttpGet("nombre/{nombre}/id/{id}")]
        public async Task<IActionResult> GetArticuloPorNombre(string nombre, int id)
        {
            if (string.IsNullOrWhiteSpace(nombre) || id <= 0)
            {
                return BadRequest("El nombre o ID no son válidos.");
            }

            try
            {
                var articulos = await _articuloService.ArticuloNombre(nombre, id);
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en obtener la actividad por nombre o ID.");
                return StatusCode(500, "Ocurrió un error al obtener la actividad.");
            }
        }

    
        
    }
}
