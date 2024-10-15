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
    public class ArticuloService : IArticuloService
    {
        private readonly IGenericRepository<Articulo> _articuloRepository;
        private readonly IMapper _mapper;

        public ArticuloService(IGenericRepository<Articulo> articuloRepository, IMapper mapper)
        {
            _articuloRepository = articuloRepository;
            _mapper = mapper;
        }

        public async Task<List<ArticuloDTO>> ListaArticulo()
        {
            try
            {
                var listaArticulo = await _articuloRepository.Consultar();
                return _mapper.Map<List<ArticuloDTO>>(listaArticulo.ToList());

            }
            catch
            {
                throw;

            }
        }

        public async Task<List<ArticuloDTO>> ArticuloNombre(string Nombre, int Id)
        {
            IQueryable<Articulo> query = await _articuloRepository.Consultar();
            var listaResultado = new List<Articulo>();

            try
            {
                if (!string.IsNullOrEmpty(Nombre))
                {
                    listaResultado = await query.Where(v => v.Titulo == Nombre || v.Idarticulo == Id).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return _mapper.Map<List<ArticuloDTO>>(listaResultado);
        }

        public async Task<ArticuloDTO> ObtenerArticuloAsync(int id)
        {
            var articulo = await _articuloRepository.Obtener(a => a.Idarticulo == id);
            return _mapper.Map<ArticuloDTO>(articulo);
        }

    }
}