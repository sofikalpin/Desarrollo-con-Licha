using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaApoyo.Bll.Servicios
{
    public class ExamenService : IExamenService
    {

        private readonly IGenericRepository<Examen> _examenRepositorio;
        private readonly IMapper _mapper;

        public ExamenService(IGenericRepository<Examen> examenRepositorio, IMapper mapper)
        {
            _examenRepositorio = examenRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ExamenDTO>> ListaExamen()
        {
            try
            {
                var listaExamen = await _examenRepositorio.Consultar();
                return _mapper.Map<List<ExamenDTO>>(listaExamen.ToList());

            }
            catch
            {
                throw;

            }
        }

        public async Task<List<ExamenDTO>> ExamenNombre(string Nombre, int Id)
        {
            IQueryable<Examen> query = await _examenRepositorio.Consultar();
            var listaResultado = new List<Examen>();

            try
            {
                if (!string.IsNullOrEmpty(Nombre))
                {
                    listaResultado = await query.Where(v => v.Titulo == Nombre || v.Idexamen == Id).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return _mapper.Map<List<ExamenDTO>>(listaResultado);
        }
        public async Task<ExamenDTO> ObtenerExamenAsync(int id)
        {
            try
            {

                var examen = await _examenRepositorio.Obtener(e => e.Idexamen == id);


                if (examen == null)
                {
                    return null;
                }


                return _mapper.Map<ExamenDTO>(examen);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


    }
}