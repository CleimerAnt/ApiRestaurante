using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class TablesRepository : GenericRepository<Tables>, ITablesRepository
    {
        private readonly ApplicationContext _Context;
        public TablesRepository(ApplicationContext context) : base(context)
        {
            _Context = context;
        }
    }
}
