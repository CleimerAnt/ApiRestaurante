using ApiRestaurante.Core.Application.ViewModel.Tables;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface ITablesServices : IGenericService<TablesViewModel, TablesSaveViewModel, Tables>
    {
        Task<List<TablesViewModel>> GetByOrder(int Id);
    }
}
