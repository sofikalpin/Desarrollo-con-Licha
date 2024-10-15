using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaApoyo.DTO;
using SistemaApoyo.Model;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.servicios.contrato
{
    public interface IRespuestaService
    {
        void CrearRespuesta(Respuesta respuesta);
        List<Respuesta> ObtenerRespuestas();
        Respuesta ObtenerRespuestaPorId(int id);
        void ActualizarRespuesta(Respuesta respuesta);
        void EliminarRespuesta(int id);
    }
}