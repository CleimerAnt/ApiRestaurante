using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository <Entity> where Entity : class
    {
        Task<Entity> AddEntity(Entity entity);
        Task<List<Entity>> GetAll();
        Task<Entity> GetById(int id);
        Task Remove(Entity entity);
        Task Update(Entity entity, int Id);
    }
}
