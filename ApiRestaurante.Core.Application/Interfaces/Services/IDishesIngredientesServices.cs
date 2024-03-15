using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IDishesIngredientesServices : IGenericService<DishesIngredientsViewModel, DishesIngredientsSaveViewModel, DishesIngredients>
    {
        Task Update(int id, List<IngredientsSaveViewModel> ingredienteListVm);
    }
}
