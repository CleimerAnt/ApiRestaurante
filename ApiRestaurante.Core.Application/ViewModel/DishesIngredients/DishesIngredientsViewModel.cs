using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.DishesIngredients
{
    public class DishesIngredientsViewModel
    {
        public int DishesId { get; set; }
        public DishesViewModel Dishes { get; set; }

        public int IngredientId { get; set; }
        public IngredientsViewModel ingredients { get; set; }
    }
}
