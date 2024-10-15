using SistemaApoyo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface INivelService
    {
        Task<List<NivelDTO>> ListaNivel();
        Task<NivelDTO> NivelNombre(int id);
        Task<NivelDTO> CrearNivel(NivelDTO nivelDto);
        Task<bool> ActualizarNivel(NivelDTO nivelDto);
        Task<bool> EliminarNivel(int id);
    }
}