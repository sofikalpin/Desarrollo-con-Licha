using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaApoyo.DAL.Repositorios.Contrato;
using SistemaApoyo.DAL.Repositorios;
using SistemaApoyo.Utility;
using SistemaApoyo.BLL.Servicios.Contrato;
using SistemaApoyo.BLL.Servicios;
using SistemaApoyo.BLL.servicios.contrato;
using SistemaApoyo.Bll.Servicios;
using SistemaApoyo.BLL.servicios;
using SistemaApoyo.DAL.DBContext;
using SistemaApoyo.Services;

namespace SistemaApoyo.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<S31Grupo2AprendizajeYApoyoDeInglesContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("cadenaPostGreSQL");
                options.UseNpgsql(connectionString);
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));

           
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IForoService, ForoService>();
            services.AddScoped<IRespuestaService, RespuestaService>();
            services.AddScoped<IActividadService, ActividadService>();
            services.AddScoped<IArticuloService, ArticuloService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<INivelService, NivelService>();
            services.AddScoped<IExamenService, ExamenService>();
            services.AddScoped<IProfesorActividad, ProfesorActividadService>();
            services.AddScoped<IProfesorExamen, ProfesorExamenService>();
            services.AddScoped<IProfesorArticulo, ProfesorArticuloService>();

           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
