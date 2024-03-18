using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class DishesRepository : GenericRepository<Dishes>, IDishesRepository
    {
        private readonly ApplicationContext _context;
        public DishesRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Dishes> ConfirnDishe(int Id)
        {
            var ingredient = await _context.Set<Dishes>().FirstOrDefaultAsync(d => d.Id == Id);

            return ingredient;
        }
    }
}
