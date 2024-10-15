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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioReposiorio;
        private readonly IGenericRepository<Menurol> _menuRolReposiorio;
        private readonly IGenericRepository<Menu> _menuReposiorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioReposiorio,
            IGenericRepository<Menurol> menuRolReposiorio,
            IGenericRepository<Menu> menuReposiorio,
            IMapper mapper)
        {
            _usuarioReposiorio = usuarioReposiorio;
            _menuRolReposiorio = menuRolReposiorio;
            _menuReposiorio = menuReposiorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            try
            {

                var usuario = await _usuarioReposiorio.Consultar(u => u.Idusuario == idUsuario);
                if (usuario == null || !usuario.Any())
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }


                var rolId = usuario.First().IdrolNavigation.Idrol;


                var menuRoles = await _menuRolReposiorio.Consultar(mr => mr.Idmenurol == rolId);
                if (menuRoles == null || !menuRoles.Any())
                {
                    throw new InvalidOperationException("No se encontraron roles de menú para el usuario.");
                }


                var menuIds = menuRoles.Select(mr => mr.IdrolNavigation.Idrol).ToList();
                var menus = await _menuReposiorio.Consultar(m => menuIds.Contains(m.Idmenu));


                return _mapper.Map<List<MenuDTO>>(menus.ToList());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocurrió un error al obtener la lista de menús", ex);
            }
        }
    }
}
