using System.Threading.Tasks;
using System.Collections.Generic;
using SistemaApoyo.DTO;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IActividadService
    {
        Task<List<ActividadDTO>> ListaActividad();
        Task<List<ActividadDTO>> ActividadNombre(string Nombre, int Id);
        Task<ActividadDTO> ObtenerActividadAsync(int id);
    }
}
