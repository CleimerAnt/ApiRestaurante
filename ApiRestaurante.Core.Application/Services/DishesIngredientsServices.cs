using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Services
{
    public class DishesIngredientsServices : GenericServices<DishesIngredientsViewModel, DishesIngredientsSaveViewModel, DishesIngredients>, IDishesIngredientesServices
    {
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;
        private readonly IMapper _mapper;

        public DishesIngredientsServices(IDishesIngredientsRepository dishesIngredientsRepository, IMapper mapper): base(dishesIngredientsRepository, mapper)
        {
            _dishesIngredientsRepository = dishesIngredientsRepository;
            _mapper = mapper;
        }

        public async Task Update(int id, List<IngredientsViewModel> ingredienteListVm)
        {
            List<Ingredients> ingredients = _mapper.Map<List<Ingredients>>(ingredienteListVm);

            await _dishesIngredientsRepository.Update(id, ingredients);
        }
    }
}
