﻿using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace ApiRestaurante.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class DishesController : BaseApiController
    {
        private readonly IDishesServices _dishesServices;
        private readonly IIngredientsServices _ingredientsServices;
        private readonly IDishesIngredientesServices _dishesIngredientsServices;
        private readonly IMapper _mapper;
        public DishesController(IDishesServices dishesServices, IIngredientsServices ingredientsServices, IDishesIngredientesServices dishesIngredientesServices, IMapper mapper)
        {
            _dishesServices = dishesServices; 
            _ingredientsServices = ingredientsServices; 
            _dishesIngredientsServices = dishesIngredientesServices;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(DishesSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (vm.DishCategory.ToUpper() != "Entrance".ToUpper() && vm.DishCategory.ToUpper() != "Main Course".ToUpper()
                    && vm.DishCategory.ToUpper() != "Dessert".ToUpper() && vm.DishCategory.ToUpper() != "Drink".ToUpper())
                {
                    ModelState.AddModelError("Category Not Available", $"This Category {vm.DishCategory} is not Available");

                    return BadRequest(ModelState);
                }


                await _dishesServices.AddAsync(vm);

                foreach(var ingredient in vm.ingredients)
                {

                   var dishe = await _dishesServices.GetByName(vm.Name);

                    DishesIngredientsSaveViewModel dishesIngredientsVm = new();
                    dishesIngredientsVm.DishesId = dishe.Id;    
                    dishesIngredientsVm.IngredientId = ingredient;

                    await _dishesIngredientsServices.AddAsync(dishesIngredientsVm);
                   
                }
                return CreatedAtAction(nameof(Create), new { id = vm.Id, vm }); 
            }
            catch
            (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int Id, DishesSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var ConfirmDishet = await _dishesServices.GetById(Id);

                if (ConfirmDishet == null)
                {
                    ModelState.AddModelError("Confirn User", "User not Found");
                    return BadRequest(ModelState);
                }

                vm.Id = Id;
                await _dishesServices.Editar(vm, Id);

                var ingredients = new List<IngredientsSaveViewModel>(); 

                foreach (var iten in vm.ingredients)
                {
                     ingredients = await _ingredientsServices.GetListIngredientsById(iten);

                }
               
                
                var dishe = await _dishesServices.GetByName(vm.Name);
                List<IngredientsSaveViewModel> ingredientList = ingredients;

                await _dishesIngredientsServices.Update(dishe.Id, ingredientList);

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientsViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Ingredients = await _dishesServices.GetAll();

                if (Ingredients == null || Ingredients.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Ingredients = await _dishesServices.GetByIdAsync(Id);

                if (Ingredients == null)
                {
                    return NoContent();
                }

                return Ok(Ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
    }
}
