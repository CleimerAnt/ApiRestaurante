using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class IngredientsRepository : GenericRepository<Ingredients>, IIngredientsRepository
    {
        private readonly ApplicationContext _Context;

        public IngredientsRepository(ApplicationContext context) : base(context)
        {
            _Context = context; 
        }

        public async Task<Ingredients> ConfirnIngrediente(int Id)
        {
            var ingredient = await _Context.Set<Ingredients>().FirstOrDefaultAsync(i => i.Id == Id);

            return ingredient;
        }
    }
}
