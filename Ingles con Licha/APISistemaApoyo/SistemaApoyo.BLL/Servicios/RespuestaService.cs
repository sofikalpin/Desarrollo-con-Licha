using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.servicios
{
    public class RespuestaService : IRespuestaService
    {
        private readonly List<Respuesta> _respuestas;

        public RespuestaService()
        {
            _respuestas = new List<Respuesta>();
        }

        public void CrearRespuesta(Respuesta respuesta)
        {
            _respuestas.Add(respuesta);
        }

        public List<Respuesta> ObtenerRespuestas()
        {
            return _respuestas;
        }

        public Respuesta ObtenerRespuestaPorId(int id)
        {
            return _respuestas.FirstOrDefault(r => r.Idrespuesta == id);
        }

        public void ActualizarRespuesta(Respuesta respuesta)
        {
            var respuestaExistente = ObtenerRespuestaPorId(respuesta.Idrespuesta);
            if (respuestaExistente != null)
            {
                respuestaExistente.Contenido = respuesta.Contenido;
                respuestaExistente.Fecha = respuesta.Fecha;
                respuestaExistente.Hora = respuesta.Hora;
                respuestaExistente.Idconsulta = respuesta.Idconsulta;
                respuestaExistente.Idusuario = respuesta.Idusuario;
                respuestaExistente.IdconsultaNavigation = respuesta.IdconsultaNavigation;
                respuestaExistente.IdusuarioNavigation =respuesta.IdusuarioNavigation;  
            }
        }

        public void EliminarRespuesta(int id)
        {
            var respuesta = ObtenerRespuestaPorId(id);
            if (respuesta != null)
            {
                _respuestas.Remove(respuesta);
            }
        }
    }
}
