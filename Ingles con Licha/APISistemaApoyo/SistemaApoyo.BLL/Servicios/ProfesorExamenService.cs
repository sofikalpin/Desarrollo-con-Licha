using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.Servicios
{
    public class ProfesorExamenService : IProfesorExamen
    {
        public readonly IGenericRepository<Examen> examenRepositorio;
        private readonly IMapper mapper;

        public ProfesorExamenService(IGenericRepository<Examen> _examenRepositorio, IMapper _mapper)
        {
            examenRepositorio = _examenRepositorio;
            mapper = _mapper;
        }

        public async Task<List<ExamenDTO>> Lista(int idExamen)
        {
            try
            {
                var queryExamen = await examenRepositorio.Consultar(e => e.Idexamen == idExamen);
                var listaExamen = queryExamen.ToList();
                if (!listaExamen.Any())
                    throw new TaskCanceledException("El examen no existe");

                return mapper.Map<List<ExamenDTO>>(listaExamen);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ExamenDTO> Crear(ExamenDTO modelo)
        {
            try
            {

                var examenCreado = await examenRepositorio.Crear(mapper.Map<Examen>(modelo));

                if (examenCreado.Idexamen == 0)
                    throw new TaskCanceledException("No se puede crear");

                var query = await examenRepositorio.Consultar(e => e.Idexamen == examenCreado.Idexamen);

                examenCreado = query.Include(e => e.Idexamen).First();

                return mapper.Map<ExamenDTO>(examenCreado);

            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> Editar(ExamenDTO modelo)
        {
            try
            {
                var examenModelo = mapper.Map<Examen>(modelo);
                var examenEncontrado = await examenRepositorio.Obtener(e => e.Idexamen == examenModelo.Idexamen);

                if (examenEncontrado == null)
                    throw new TaskCanceledException("El examen no existe");

                examenEncontrado.Duracion = examenModelo.Duracion;
                examenEncontrado.Titulo = examenModelo.Titulo;
                examenEncontrado.Calificacion = examenModelo.Calificacion;
                examenEncontrado.HoraIni = examenModelo.HoraIni;
                examenEncontrado.HoraFin = examenModelo.HoraFin;

                bool respuesta = await examenRepositorio.Editar(examenEncontrado);

                if (!respuesta)

                    throw new TaskCanceledException("No se puede editar");

                return respuesta;

            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var examenEncontrado = await examenRepositorio.Obtener(e => e.Idexamen == id);

                if (examenEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await examenRepositorio.Eliminar(examenEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se puede eliminar");

                return respuesta;
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<ExamenDTO>> Lista()
        {
            try
            {
                var queryExamen = await examenRepositorio.Consultar();
                var listaExamen = queryExamen.ToList();

                return mapper.Map<List<ExamenDTO>>(listaExamen);

            }
            catch
            {

                throw;
            }
        }
    }


}