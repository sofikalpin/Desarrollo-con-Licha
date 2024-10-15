using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model;
using AutoMapper; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.Servicios
{
    public class NivelService : INivelService
    {
        private readonly IGenericRepository<Nivel> _nivelRepository;
        private readonly IMapper _mapper;

        public NivelService(IGenericRepository<Nivel> nivelRepository, IMapper mapper)
        {
            _nivelRepository = nivelRepository;
            _mapper = mapper;
        }

        
        public async Task<List<NivelDTO>> ListaNivel()
        {
            var niveles = await _nivelRepository.Consultar();
            return _mapper.Map<List<NivelDTO>>(niveles.ToList());
        }

        
        public async Task<NivelDTO> CrearNivel(NivelDTO nivelDto)
        {
            var nivel = _mapper.Map<Nivel>(nivelDto);
            var nuevoNivel = await _nivelRepository.Crear(nivel);
            return _mapper.Map<NivelDTO>(nuevoNivel);
        }

        
        public async Task<bool> ActualizarNivel(NivelDTO nivelDto)
        {
            var nivel = _mapper.Map<Nivel>(nivelDto);
            return await _nivelRepository.Actualizar(nivel);
        }

        
        public async Task<NivelDTO> NivelNombre(int id)
        {
            var nivel = await _nivelRepository.Obtener(n => n.Idnivel == id);
            return _mapper.Map<NivelDTO>(nivel);
        }

       
        public async Task<bool> EliminarNivel(int id)
        {
            var nivel = await _nivelRepository.Obtener(n => n.Idnivel == id);
            if (nivel != null)
            {
                return await _nivelRepository.Eliminar(nivel);
            }
            return false;
        }
    }
}