using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class GenericRepository <Entity>: IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Entity> AddEntity(Entity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Entity>> GetAll()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        public async Task<Entity> GetById(int id)
        {
            return await _context.Set<Entity>().FindAsync(id);
        }

        public async Task Remove(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Entity entity, int Id)
        {
            Entity entry = await _context.Set<Entity>().FindAsync(Id);

            _context.Entry(entry).CurrentValues.SetValues(entity);

            _context.SaveChanges();
        }
    }
}
