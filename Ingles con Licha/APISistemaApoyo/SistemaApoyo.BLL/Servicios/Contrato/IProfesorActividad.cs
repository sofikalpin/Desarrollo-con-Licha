using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaApoyo.DTO;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IProfesorActividad
    { 
        Task<ActividadDTO> ObtenerPorId(int idActividad); 
        Task<ActividadDTO> Crear(ActividadDTO modelo); 
        Task<bool> Editar(ActividadDTO modelo); 
        Task<bool> Eliminar(int id); 
        Task<List<ActividadDTO>> Lista();
    }
}
