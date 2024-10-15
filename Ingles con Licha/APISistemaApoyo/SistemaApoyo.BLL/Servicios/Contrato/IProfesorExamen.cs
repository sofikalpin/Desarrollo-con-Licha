using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaApoyo.BLL.Servicios.Contrato
{
    public interface IProfesorExamen
    {
        Task<List<ExamenDTO>> Lista(int idExamen);
        Task<List<ExamenDTO>> Lista();
        Task<ExamenDTO> Crear(ExamenDTO modelo);

        Task<bool> Editar(ExamenDTO modelo);

        Task<bool> Eliminar(int id);

    }
}