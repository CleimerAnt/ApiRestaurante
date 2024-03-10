using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Repositories
{
    public interface IDishesIngredientsRepository : IGenericRepository<DishesIngredients>
    {
        Task Update(int Id, List<Ingredients> ingredientList);
    }
}
