using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Services
{
    public class DishesServices : GenericServices<DishesViewModel, DishesSaveViewModel, Dishes>, IDishesServices
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;
        public DishesServices(IDishesRepository dishesRepository, IMapper mapper, IIngredientsRepository ingredientsRepository, IDishesIngredientsRepository dishesIngredientsRepository) : base(dishesRepository, mapper)
        {
            _dishesRepository = dishesRepository;
            _ingredientsRepository = ingredientsRepository;
            _mapper = mapper;  
            _dishesIngredientsRepository = dishesIngredientsRepository;
        }
        public override Task<DishesSaveViewModel> AddAsync(DishesSaveViewModel vm)
        {
            List<DishesIngredientsViewModel> ingredientsAndDishes = vm.ingredients.Select(ingredientesId => new DishesIngredientsViewModel 
            { 
                IngredientId = ingredientesId
                
              
            }).ToList();

            DishesSaveViewModel dishes = new()
            {
                Name = vm.Name, 
                Price = vm.Price,   
                NumberOfPerson = vm.NumberOfPerson, 
                DishCategory = vm.DishCategory, 
            };

            return base.AddAsync(vm);
        }

        public async Task<List<DishesViewModel>> GetAll()
        {
             var ingredients = await _ingredientsRepository.GetAll();
             var dishes = await _dishesRepository.GetAll();
            var dishesIngredients = await _dishesIngredientsRepository.GetAll();

            var List = from d in dishes
                       select new DishesViewModel
                       {
                           Id = d.Id,
                           Name = d.Name,
                           Price = d.Price,
                           NumberOfPerson = d.NumberOfPerson,
                           DishCategory = d.DishCategory,
                           ingredients = (from di in dishesIngredients
                                          join d2 in dishes on di.DishesId equals d2.Id
                                          join i in ingredients on di.IngredientId equals i.Id
                                          where d.Id == di.DishesId
                                          select new IngredientsViewModel
                                          {
                                              Id = i.Id,
                                              Name = i.Name
                                          }).ToList()

                        };
             return List.ToList();
        }

        public async Task<List<DishesViewModel>> GetByIdAsync(int Id)
        {
            var ingredients = await _ingredientsRepository.GetAll();
            var dishes = await _dishesRepository.GetAll();
            var dishesIngredients = await _dishesIngredientsRepository.GetAll();

            var List = from d in dishes
                       where d.Id == Id
                       select new DishesViewModel
                       {
                           Id = d.Id,
                           Name = d.Name,
                           Price = d.Price,
                           NumberOfPerson = d.NumberOfPerson,
                           DishCategory = d.DishCategory,
                           ingredients = (from di in dishesIngredients
                                          join d2 in dishes on di.DishesId equals d2.Id
                                          join i in ingredients on di.IngredientId equals i.Id
                                          where d.Id == di.DishesId
                                          select new IngredientsViewModel
                                          {
                                              Id = i.Id,
                                              Name = i.Name
                                          }).ToList()

                       };
            return List.ToList();
        }
        public async Task<DishesViewModel> GetByName(string name)
        {
            var dishes = await _dishesRepository.GetAll();

            var dishe =  dishes.FirstOrDefault(d => d.Name == name);

            DishesViewModel disheVm = _mapper.Map<DishesViewModel>(dishe);

            return disheVm;
        }
        public async Task<DishesViewModel> ConfirnDishe(int Id)
        {
            var dishe = await _dishesRepository.ConfirnDishe(Id);

            DishesViewModel disheVm = _mapper.Map<DishesViewModel>(dishe);

            return disheVm;
        }

      
    }
}

