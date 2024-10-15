using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaApoyo.Services
{
    public class ChatService : IChatService
    {
        private readonly IGenericRepository<SistemaApoyo.Model.Models.Chat> _chatRepository;
        private readonly IGenericRepository<Mensaje> _mensajeRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public ChatService(IGenericRepository<SistemaApoyo.Model.Models.Chat> chatRepository,
                           IGenericRepository<Mensaje> mensajeRepository,
                           IGenericRepository<Usuario> usuarioRepository,
                           IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mensajeRepository = mensajeRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ChatDTO> CrearChat(ChatDTO chatDto)
        {
            
            if (await _usuarioRepository.Obtener(u => u.Idusuario == chatDto.Idusuario1) == null ||
                await _usuarioRepository.Obtener(u => u.Idusuario == chatDto.Idusuario2) == null)
            {
                throw new Exception("Uno o ambos usuarios no existen.");
            }

            
            var chat = _mapper.Map<SistemaApoyo.Model.Models.Chat>(chatDto);

            
            await _chatRepository.Crear(chat);

            return _mapper.Map<ChatDTO>(chat);
        }

        public async Task<IEnumerable<ChatDTO>> ObtenerChatsPorUsuarioId(int userId)
        {
            
            var chats = await _chatRepository.Consultar(c => c.Idusuario1 == userId || c.Idusuario2 == userId);

           
            return _mapper.Map<IEnumerable<ChatDTO>>(chats);
        }

        public async Task<MensajeDTO> EnviarMensaje(MensajeDTO mensajeDto)
        {
            
            var chat = await _chatRepository.Obtener(c => c.Idchat == mensajeDto.Idchat);
            if (chat == null)
            {
                throw new Exception("El chat no existe.");
            }

            
            if (mensajeDto.Idusuario != chat.Idusuario1 && mensajeDto.Idusuario != chat.Idusuario2)
            {
                throw new Exception("El usuario no es parte de este chat.");
            }

            
            var mensaje = _mapper.Map<Mensaje>(mensajeDto);

            
            await _mensajeRepository.Crear(mensaje);

            return _mapper.Map<MensajeDTO>(mensaje);
        }

        public async Task<IEnumerable<MensajeDTO>> ObtenerMensajesPorChatId(int chatId, int pageNumber, int pageSize)
        {
            
            var mensajes = await _mensajeRepository.Consultar(m => m.Idchat == chatId);

            
            var paginatedMessages = mensajes.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            
            return _mapper.Map<IEnumerable<MensajeDTO>>(paginatedMessages);
        }
    }
}
