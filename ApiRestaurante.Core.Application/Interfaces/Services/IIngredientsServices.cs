using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IIngredientsServices : IGenericService<IngredientsViewModel, IngredientsSaveViewModel, Ingredients>
    {
        Task<IngredientsViewModel> ConfirnIngrediente(int Id);
        Task<List<IngredientsSaveViewModel>> GetListIngredientsById(int Id);
    }
}
