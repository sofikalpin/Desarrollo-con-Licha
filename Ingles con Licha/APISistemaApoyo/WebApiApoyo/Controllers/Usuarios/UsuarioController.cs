﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaApoyo.API.Utilidad;
using SistemaApoyo.BLL.Servicios;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
    {
        _usuarioService = usuarioService;
        _logger = logger;
    }

    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
        var rsp = new Response<List<UsuarioDTO>>();
        try
        {
            rsp.status = true;
            rsp.value = await _usuarioService.Lista();
        }
        catch (Exception ex)
        {
            rsp.status = false;
            _logger.LogError(ex, "Error al obtener la lista de usuarios.");
        }
        return Ok(rsp);
    }

    [HttpPost]
    [Route("IniciarSesion")]
    public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
    {
        var rsp = new Response<SesionDTO>();
        try
        {
           
            rsp.status = true;
            rsp.value = await _usuarioService.ValidarCredenciales(login.Correo, login.ContrasenaHash);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            _logger.LogError(ex, "Error al iniciar sesión.");
        }
        return Ok(rsp);
    }
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
    {
        var rsp = new Response<UsuarioDTO>();
        try
        {

            rsp.status = true;
            rsp.value = await _usuarioService.Crear(usuario);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            _logger.LogError(ex, "Error al guardar el usuario.");
        }
        return Ok(rsp);
    }
    [HttpPut]
    [Route("Editar")]
    public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
    {
        var rsp = new Response<bool>();
        try
        {

            rsp.status = true;
            rsp.value = await _usuarioService.Editar(usuario);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            _logger.LogError(ex, "Error al editar el usuario.");
        }
        return Ok(rsp);
    }
    [HttpDelete]
    [Route("Eliminar(id:int)")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var rsp = new Response<bool>();
        try
        {

            rsp.status = true;
            rsp.value = await _usuarioService.Eliminar(id);
        }
        catch (Exception ex)
        {
            rsp.status = false;
            _logger.LogError(ex, "Error al eliminar el usuario.");
        }
        return Ok(rsp);
    }
}
