using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DTO;
using SistemaApoyo.Model;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.BLL.Servicios
{
    public class ProfesorArticuloService : IProfesorArticulo
    {
        public readonly IGenericRepository<Articulo> articuloRepositorio;
        private readonly IMapper mapper;



        public ProfesorArticuloService(IGenericRepository<Articulo> _articuloRepositorio, IMapper _mapper)
        {
            articuloRepositorio = _articuloRepositorio;
            mapper = _mapper;
        }

        public async Task<ArticuloDTO> Crear(ArticuloDTO modelo)
        {
            try
            {
                var articuloCreado = await articuloRepositorio.Crear(mapper.Map<Articulo>(modelo));

                if (articuloCreado.Idarticulo == 0)
                    throw new TaskCanceledException("No se puede crear");

                var query = await articuloRepositorio.Consultar(a => a.Idarticulo == articuloCreado.Idarticulo);

                articuloCreado = query.Include(a => a.Idarticulo).First();

                return mapper.Map<ArticuloDTO>(articuloCreado);
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<ArticuloDTO>> Lista(int idArticulo)
        {
            try
            {
                var queryExamen = await articuloRepositorio.Consultar(a => a.Idarticulo == idArticulo);
                var listaArticulo = queryExamen.ToList();
                if (!listaArticulo.Any())
                    throw new TaskCanceledException("El articulo no existe");

                return mapper.Map<List<ArticuloDTO>>(listaArticulo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var articuloEncontrado = await articuloRepositorio.Obtener(a => a.Idarticulo == id);

                if (articuloEncontrado == null)
                    throw new TaskCanceledException("El articulo no existe");

                bool respuesta = await articuloRepositorio.Eliminar(articuloEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se puede eliminar");
                return respuesta;

            }
            catch
            {

                throw;
            }
        }

        public async Task<List<ArticuloDTO>> Lista()
        {
            try
            {
                var queryArticulo = await articuloRepositorio.Consultar();
                var listaArticulo = queryArticulo.ToList();
                return mapper.Map<List<ArticuloDTO>>(listaArticulo);
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> Editar(ArticuloDTO modelo)
        {
            try
            {
                var articuloModelo = mapper.Map<ArticuloDTO>(modelo);

                var articuloEncontrado = await articuloRepositorio.Obtener(a => a.Idarticulo == articuloModelo.Idarticulo);

                if (articuloEncontrado == null)
                    throw new NotImplementedException("El articulo no existe");

                articuloEncontrado.Titulo = articuloModelo.Titulo;
                articuloEncontrado.Descripcion = articuloModelo.Descripcion;
                articuloEncontrado.UrlImagen = articuloModelo.UrlImagen;

                bool respuesta = await articuloRepositorio.Editar(articuloEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se puede editar");
                return respuesta;

            }
            catch
            {

                throw;
            }
        }
    }


}