using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaApoyo.DTO;
using SistemaApoyo.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.Hubs;

namespace SistemaApoyo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, IHubContext<ChatHub> chatHubContext, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _chatHubContext = chatHubContext;
            _logger = logger;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearChat([FromBody] ChatDTO chatDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var chatCreado = await _chatService.CrearChat(chatDto);
                return CreatedAtAction(nameof(ObtenerChatsPorUsuarioId), new { userId = chatDto.Idusuario1 }, chatCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el chat.");
                return StatusCode(500, "Ocurrió un error al crear el chat.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ChatDTO>>> ObtenerChatsPorUsuarioId(int userId)
        {
            var chats = await _chatService.ObtenerChatsPorUsuarioId(userId);
            return Ok(chats);
        }

        [HttpPost("mensaje/enviar")]
        public async Task<IActionResult> EnviarMensaje([FromBody] MensajeDTO mensajeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mensajeEnviado = await _chatService.EnviarMensaje(mensajeDto);

                // Enviar el mensaje a través de SignalR
                await _chatHubContext.Clients.User(mensajeDto.Idusuario.ToString()).SendAsync("RecibirMensaje", mensajeDto.Contenido);

                return Ok(mensajeEnviado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el mensaje.");
                return StatusCode(500, "Ocurrió un error al enviar el mensaje.");
            }
        }

        [HttpGet("{chatId}/mensajes")]
        public async Task<ActionResult<IEnumerable<MensajeDTO>>> ObtenerMensajesPorChatId(int chatId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var mensajes = await _chatService.ObtenerMensajesPorChatId(chatId, pageNumber, pageSize);
            return Ok(mensajes);
        }
    }
}
