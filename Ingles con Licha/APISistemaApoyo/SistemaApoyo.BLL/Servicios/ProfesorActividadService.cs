using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.Servicios
{
    public class ProfesorActividadService : IProfesorActividad
    {
        private readonly IGenericRepository<Actividad> actividadRepositorio;
        private readonly IMapper mapper;

        public ProfesorActividadService(IGenericRepository<Actividad> _actividadRepositorio, IMapper _mapper)
        {
            actividadRepositorio = _actividadRepositorio;
            mapper = _mapper;
        }

        public async Task<List<ActividadDTO>> Lista()
        {
            try
            {
                var actividades = await actividadRepositorio.Consultar();
                var listaActividad = actividades.ToList();
                return mapper.Map<List<ActividadDTO>>(listaActividad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de actividades: " + ex.Message, ex);
            }
        }

        public async Task<ActividadDTO> ObtenerPorId(int idActividad)
        {
            try
            {
                var actividad = await actividadRepositorio.Obtener(a => a.Idactividad == idActividad);
                if (actividad == null)
                    throw new KeyNotFoundException("La actividad no existe.");

                return mapper.Map<ActividadDTO>(actividad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener actividad por ID: " + ex.Message, ex);
            }
        }

        public async Task<ActividadDTO> Crear(ActividadDTO modelo)
        {
            try
            {
                var actividadCreada = await actividadRepositorio.Crear(mapper.Map<Actividad>(modelo));
                if (actividadCreada == null || actividadCreada.Idactividad == 0)
                    throw new InvalidOperationException("No se pudo crear la actividad.");

                return mapper.Map<ActividadDTO>(actividadCreada);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear actividad: " + ex.Message, ex);
            }
        }

        public async Task<bool> Editar(ActividadDTO modelo)
        {
            try
            {
                var actividadEncontrada = await actividadRepositorio.Obtener(a => a.Idactividad == modelo.Idactividad);
                if (actividadEncontrada == null)
                    throw new KeyNotFoundException("La actividad no existe.");

                // Actualiza los campos necesarios
                actividadEncontrada.Descripcion = modelo.Descripcion;
                actividadEncontrada.Nombre = modelo.Nombre;
                actividadEncontrada.Fecha = modelo.Fecha;

                return await actividadRepositorio.Editar(actividadEncontrada);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar la actividad: " + ex.Message, ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var actividadEncontrada = await actividadRepositorio.Obtener(a => a.Idactividad == id);
                if (actividadEncontrada == null)
                    throw new KeyNotFoundException("La actividad no encontrada.");

                return await actividadRepositorio.Eliminar(actividadEncontrada);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la actividad: " + ex.Message, ex);
            }
        }
    }
}
