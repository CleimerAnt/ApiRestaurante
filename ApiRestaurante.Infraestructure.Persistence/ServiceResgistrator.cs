using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.ViewModel.Orders;
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

            #region "Services"
            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            services.AddTransient<IIngredientsRepository, IngredientsRepository>();

            services.AddTransient<IDishesRepository, DishesRepository>();

            services.AddTransient<IDishesIngredientsRepository, DishesIngredientsRepository>();

            services.AddTransient<ITablesRepository, TablesRepository>();

            services.AddTransient<IOrdersRepository, OrdersRepository>();

            services.AddTransient<IDishesOrdersRepository, DishesOrdersRepository>();
            #endregion
        }
    }
}
