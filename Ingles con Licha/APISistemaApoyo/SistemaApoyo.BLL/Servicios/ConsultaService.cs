using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaApoyo.BLL.Servicios
{
    public class ConsultaService : IConsultaService
    {
        private readonly IGenericRepository<Consulta> _consultaRepositorio;
        private readonly IMapper _mapper;

        public ConsultaService(IGenericRepository<Consulta> consultaRepositorio, IMapper mapper)
        {
            _consultaRepositorio = consultaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ConsultaDTO>> ListaConsultas()
        {
            try
            {
                var listaConsultas = await (await _consultaRepositorio.Consultar()).ToListAsync();
                return _mapper.Map<List<ConsultaDTO>>(listaConsultas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la lista de consultas", ex);
            }
        }

        public async Task<List<ConsultaDTO>> ConsultaPorTitulo(string titulo, int id)
        {
            try
            {
                var query = await _consultaRepositorio.Consultar();
                var listaResultado = await query
                    .Where(c => (titulo != null && c.Titulo == titulo) || c.Idconsulta == id)
                    .ToListAsync();

                return _mapper.Map<List<ConsultaDTO>>(listaResultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la consulta por título o ID", ex);
            }
        }

        public async Task<ConsultaDTO> ObtenerConsultaAsync(int id)
        {
            try
            {
                var consulta = await _consultaRepositorio.Obtener(c => c.Idconsulta == id);
                return _mapper.Map<ConsultaDTO>(consulta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al obtener la consulta por ID", ex);
            }
        }

        public async Task<bool> CrearConsulta(ConsultaDTO consultaDto)
        {
            try
            {
                var consulta = _mapper.Map<Consulta>(consultaDto);
                await _consultaRepositorio.Insertar(consulta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al crear la consulta", ex);
            }
        }

        public async Task<bool> ActualizarConsulta(ConsultaDTO consultaDto)
        {
            try
            {
                var consultaExistente = await _consultaRepositorio.Obtener(c => c.Idconsulta == consultaDto.Idconsulta);
                if (consultaExistente == null)
                {
                    throw new InvalidOperationException("Consulta no encontrada.");
                }

                consultaExistente = _mapper.Map(consultaDto, consultaExistente);
                await _consultaRepositorio.Actualizar(consultaExistente);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al actualizar la consulta", ex);
            }
        }

        public async Task<bool> EliminarConsulta(int id)
        {
            try
            {
                var consulta = await _consultaRepositorio.Obtener(c => c.Idconsulta == id);
                if (consulta == null)
                {
                    throw new InvalidOperationException("Consulta no encontrada.");
                }

                await _consultaRepositorio.Eliminar(consulta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("Error al eliminar la consulta", ex);
            }
        }
    }
}
