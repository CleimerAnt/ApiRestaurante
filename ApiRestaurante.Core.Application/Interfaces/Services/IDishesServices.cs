using ApiRestaurante.Core.Application.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IDishesServices : IGenericService<DishesViewModel, DishesSaveViewModel, Dishes>
    {
        Task<List<DishesViewModel>> GetAll();
        Task<DishesViewModel> GetByName(string name);
        Task<List<DishesViewModel>> GetByIdAsync(int Id);
        Task<DishesViewModel> ConfirnDishe(string name);
    }
}
