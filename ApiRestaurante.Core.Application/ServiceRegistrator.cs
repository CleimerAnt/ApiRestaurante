using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application
{
    public static class ServicesRegistrator
    {
        public static void AddAplicationLayer(this IServiceCollection services )
        {
            #region "Services"
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IIngredientsServices, IngredientsServices>();

            services.AddTransient<IDishesServices, DishesServices>();

            services.AddTransient<IDishesIngredientesServices, DishesIngredientsServices>();

            services.AddTransient<ITablesServices, TablesServices>();

            services.AddTransient<IOrdersServices, OrdersServices>();

            services.AddTransient<IDishesOrderServices, DishesOrdersServices>();
            #endregion
        }
    }
}
