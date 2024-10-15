using SistemaApoyo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaApoyo.Services
{
    public interface IChatService
    {
        Task<ChatDTO> CrearChat(ChatDTO chatDto);
        Task<IEnumerable<ChatDTO>> ObtenerChatsPorUsuarioId(int userId);
        Task<MensajeDTO> EnviarMensaje(MensajeDTO mensajeDto);
        Task<IEnumerable<MensajeDTO>> ObtenerMensajesPorChatId(int chatId, int pageNumber, int pageSize);
    }
}
