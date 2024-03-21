using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace ApiRestaurante.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class IngredientsController : BaseApiController
    {
        private readonly IIngredientsServices _ingredientsServices;
        public IngredientsController(IIngredientsServices ingredientsServices)
        {
            _ingredientsServices = ingredientsServices;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(IngredientsSaveViewModel vm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();  
                }

                await _ingredientsServices.AddAsync(vm);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientsViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(int Id, IngredientsSaveViewModel vm)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                vm.Id = Id;
                 await _ingredientsServices.Editar(vm, Id);

                return Ok(vm);    
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IngredientsViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Ingredients = await _ingredientsServices.GetAllAsync(); 

               if(Ingredients == null || Ingredients.Count == 0)
               {
                    return NoContent(); 
               }

                return Ok(Ingredients);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientsViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Ingredients = await _ingredientsServices.GetById(Id);

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
