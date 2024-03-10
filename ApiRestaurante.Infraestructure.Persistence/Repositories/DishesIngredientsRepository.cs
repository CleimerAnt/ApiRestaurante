using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class DishesIngredientsRepository : GenericRepository<DishesIngredients>, IDishesIngredientsRepository
    {
        private readonly ApplicationContext _Context;
        
        public DishesIngredientsRepository(ApplicationContext context) : base(context)
        {
            _Context = context;
        }

        public async Task Update(int Id, List<Ingredients> ingredientList)
        {
            var dishesIngredients = await _Context.DishesIngredients.FirstOrDefaultAsync(di => di.DishesId == Id);
            

            if (dishesIngredients != null)
            {
                _Context.DishesIngredients.Remove(dishesIngredients);
                _Context.SaveChanges();
                

                foreach(var ingredient in ingredientList)
                {

                    var ingredientId = await _Context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredient.Name);

                    var DishesIngredients = new DishesIngredients
                    {
                        DishesId = Id,
                        IngredientId = ingredientId.Id
                    };

                 await _Context.Set<DishesIngredients>().AddAsync(DishesIngredients);
                  await  _Context.SaveChangesAsync();    
                }
            
            }
        }
    }
}
