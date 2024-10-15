using SistemaApoyo.Model.Models;
using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SistemaApoyo.DTO;

namespace SistemaApoyo.BLL.servicios
{
    public class ForoService : IForoService
    {
        private readonly IGenericRepository<Foro> _foroRepositorio;
        private readonly IMapper _mapper;

        public ForoService(IGenericRepository<Foro> foroRepositorio, IMapper mapper)
        {
            _foroRepositorio = foroRepositorio;
            _mapper = mapper;
        }

        public async Task<ForoDTO> CrearForo(ForoDTO foroDTO)
        {
            try
            {
                if (foroDTO == null)
                    throw new ArgumentNullException(nameof(foroDTO), "El foro no puede ser nulo.");

                var foro = _mapper.Map<Foro>(foroDTO);
                await _foroRepositorio.Insertar(foro);
                return _mapper.Map<ForoDTO>(foro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el foro", ex);
            }
        }

        public async Task<List<ForoDTO>> ObtenerForos(Expression<Func<Foro, bool>> filtro = null)
        {
            try
            {
                var foros = await _foroRepositorio.Consultar(filtro);
                return _mapper.Map<List<ForoDTO>>(foros.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los foros", ex);
            }
        }

        public async Task<ForoDTO> ObtenerForoPorId(int id)
        {
            try
            {
                var foro = await _foroRepositorio.Obtener(f => f.Idforo == id);
                if (foro == null)
                    throw new KeyNotFoundException("Foro no encontrado.");

                return _mapper.Map<ForoDTO>(foro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el foro por ID", ex);
            }
        }

        public async Task ActualizarForo(ForoDTO foroDTO)
        {
            try
            {
                if (foroDTO == null)
                    throw new ArgumentNullException(nameof(foroDTO), "El foro no puede ser nulo.");

                var foroExistente = await _foroRepositorio.Obtener(f => f.Idforo == foroDTO.IdForo);
                if (foroExistente == null)
                    throw new KeyNotFoundException("Foro no encontrado.");

                _mapper.Map(foroDTO, foroExistente);
                await _foroRepositorio.Actualizar(foroExistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el foro", ex);
            }
        }

        public async Task EliminarForo(int id)
        {
            try
            {
                var foro = await _foroRepositorio.Obtener(f => f.Idforo == id);
                if (foro == null)
                    throw new KeyNotFoundException("Foro no encontrado.");

                await _foroRepositorio.Eliminar(foro);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el foro", ex);
            }
        }
    }
}
