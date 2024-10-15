using SistemaApoyo.DTO;


namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IExamenService
    {
        Task<List<ExamenDTO>> ListaExamen();
        Task<List<ExamenDTO>> ExamenNombre(string Nombre, int Id);

        Task<ExamenDTO> ObtenerExamenAsync(int id);
    }
}
