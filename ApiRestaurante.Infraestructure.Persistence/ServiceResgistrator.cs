using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Infraestructure.Persistence.Context;
using ApiRestaurante.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence
{
    public static class ServiceResgistrator
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conexion"), m =>
                m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });

            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            services.AddTransient<IIngredientsRepository, IngredientsRepository>();

            services.AddTransient<IDishesRepository, DishesRepository>();

            services.AddTransient<IDishesIngredientsRepository, DishesIngredientsRepository>();
        }
    }
}
