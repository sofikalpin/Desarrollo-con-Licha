using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SistemaApoyo.DTO;
using SistemaApoyo.Model;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.servicios.contrato
{
    public interface IForoService
    {
        Task<ForoDTO> CrearForo(ForoDTO foroDTO);

        Task<List<ForoDTO>> ObtenerForos(Expression<Func<Foro, bool>> filtro = null);

        Task<ForoDTO> ObtenerForoPorId(int id);

        Task ActualizarForo(ForoDTO foroDTO);

        Task EliminarForo(int id);


    }

}
