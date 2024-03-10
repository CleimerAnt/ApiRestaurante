using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class DishesIngredients
    {
        public int DishesId { get; set; }   
        public Dishes Dishes { get; set; }
        public int IngredientId { get; set; }   
        public Ingredients Ingredients { get; set; }    
    }
}
