using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaApoyo.DTO;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IArticuloService
    {
        Task<List<ArticuloDTO>> ListaArticulo();
        Task<List<ArticuloDTO>> ArticuloNombre(string Nombre, int Id);

        Task<ArticuloDTO> ObtenerArticuloAsync(int id);
    }
}
