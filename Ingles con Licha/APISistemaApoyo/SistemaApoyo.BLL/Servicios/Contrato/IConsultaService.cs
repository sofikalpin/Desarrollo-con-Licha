using SistemaApoyo.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IConsultaService
    {
        Task<List<ConsultaDTO>> ListaConsultas();
        Task<List<ConsultaDTO>> ConsultaPorTitulo(string titulo, int id);
        Task<ConsultaDTO> ObtenerConsultaAsync(int id);
        Task<bool> CrearConsulta(ConsultaDTO consultaDto);
        Task<bool> ActualizarConsulta(ConsultaDTO consultaDto);
        Task<bool> EliminarConsulta(int id);
    }


}
