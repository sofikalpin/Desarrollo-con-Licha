using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.Servicios
{
    public class ActividadService : IActividadService
    {
        private readonly IGenericRepository<Actividad> _actividadRepositorio;
        private readonly IMapper _mapper;

        public ActividadService(IGenericRepository<Actividad> actividadRepositorio, IMapper mapper)
        {
            _actividadRepositorio = actividadRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ActividadDTO>> ListaActividad()
        {
            try
            { 
                var listaActividad = await (await _actividadRepositorio.Consultar()).ToListAsync();
                return _mapper.Map<List<ActividadDTO>>(listaActividad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la lista de actividades", ex);
            }
        }

        public async Task<List<ActividadDTO>> ActividadNombre(string nombre, int id)
        {
            try
            {
                var query = await _actividadRepositorio.Consultar(); 
                var listaResultado = await query
                    .Where(v => (nombre != null && v.Nombre == nombre) || v.Idactividad == id)
                    .ToListAsync();

                return _mapper.Map<List<ActividadDTO>>(listaResultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la actividad por nombre o ID", ex);
            }
        }

        public async Task<ActividadDTO> ObtenerActividadAsync(int id)
        {
            try
            {
                var actividad = await _actividadRepositorio.Obtener(a => a.Idactividad == id);
                return _mapper.Map<ActividadDTO>(actividad);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la actividad por ID", ex);
            }
        }
    }
}
