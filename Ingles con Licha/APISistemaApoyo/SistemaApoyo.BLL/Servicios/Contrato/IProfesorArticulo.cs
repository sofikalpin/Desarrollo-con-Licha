using SistemaApoyo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaApoyo.Model;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IProfesorArticulo
    {
        Task<List<ArticuloDTO>> Lista();
        Task<ArticuloDTO> Crear(ArticuloDTO modelo);

        Task<bool> Editar(ArticuloDTO modelo);

        Task<bool> Eliminar(int id);

        Task<List<ArticuloDTO>> Lista(int idArticulo);
    }

}