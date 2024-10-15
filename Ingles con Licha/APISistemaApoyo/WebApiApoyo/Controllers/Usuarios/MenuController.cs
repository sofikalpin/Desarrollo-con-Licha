using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;
    private readonly ILogger<MenuController> _logger;

    public MenuController(IMenuService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }

    
    [HttpGet("lista/{idUsuario}")]
    public async Task<IActionResult> GetMenus(int idUsuario)
    {
        if (idUsuario <= 0)
        {
            return BadRequest("El ID del usuario no es válido.");
        }

        try
        {
            var menus = await _menuService.Lista(idUsuario);
            return Ok(menus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la lista de menús para el usuario con ID {IdUsuario}.", idUsuario);
            return StatusCode(500, "Ocurrió un error al obtener los menús.");
        }
    }
}