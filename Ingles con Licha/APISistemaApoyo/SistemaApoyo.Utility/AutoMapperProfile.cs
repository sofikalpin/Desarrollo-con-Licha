﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaApoyo.DTO;
using SistemaApoyo.Model.Models;

namespace SistemaApoyo.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Actividad
            CreateMap<Actividad, ActividadDTO>().ReverseMap();
            #endregion Actividad

            #region Articulo
            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
            #endregion Articulo

            #region Chat
            CreateMap<Chat, ChatDTO>()
                .ForMember(dest => dest.Mensajes,
                           opt => opt.MapFrom(src => src.Mensajes))
                .ReverseMap();
            #endregion Chat

            #region Mensaje
            CreateMap<Mensaje, MensajeDTO>().ReverseMap();
            #endregion Mensaje

            #region Consulta
            CreateMap<Consulta, ConsultaDTO>().ReverseMap();
            #endregion Consulta

            #region Examen
            CreateMap<Examen, ExamenDTO>().ReverseMap();
            #endregion Examen

            #region Foro
            CreateMap<Foro, ForoDTO>().ReverseMap();
            #endregion Foro

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region Nivel
            CreateMap<Nivel, NivelDTO>().ReverseMap();
            #endregion Nivel

            #region Respuesta
            CreateMap<Respuesta, RespuestaDTO>().ReverseMap();
            #endregion Respuesta

            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>(); 
            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino => destino.RolDescripcion,
                           opt => opt.MapFrom(origen => origen.IdrolNavigation.Nombre)); 
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino => destino.IdrolNavigation,
                           opt => opt.Ignore()); 
            #endregion Usuario
        }
    }
}
