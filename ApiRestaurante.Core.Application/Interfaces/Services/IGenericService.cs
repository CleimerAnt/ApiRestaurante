using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, postViewModel, Entity> where ViewModel : class
       where postViewModel : class
       where Entity : class
    {
        Task<postViewModel> AddAsync(postViewModel vm);
        Task Editar(postViewModel vm, int ID);
        Task Eliminar(int Id);
        Task<List<ViewModel>> GetAllAsync();
        Task<postViewModel> GetById(int Id);
    }
}
