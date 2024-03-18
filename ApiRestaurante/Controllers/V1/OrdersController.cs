using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace ApiRestaurante.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Waiter")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersServices _orderServices;
        private readonly IDishesServices _dishesServices;
        private readonly IDishesOrderServices _dishesordersServices;
        public OrdersController(IOrdersServices ordersServices, IDishesServices dishesServices, IDishesOrderServices dishesOrderServices)
        {
            _orderServices = ordersServices;
            _dishesServices = dishesServices;
            _dishesordersServices = dishesOrderServices;

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(OrdersSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

             var orderAdd =  await _orderServices.AddAsync(vm);

                foreach (var dishes in vm.DishesAdd)
                {
                    var disheId = await _dishesServices.ConfirnDishe(dishes);

                    DishesOrderSaveViewModel dishesIngredientsVm = new();
                    dishesIngredientsVm.DishesID = disheId.Id;
                    dishesIngredientsVm.OrdersId = orderAdd.Id;

                    await  _dishesordersServices.AddAsync(dishesIngredientsVm);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        public async Task<IActionResult> Update(int Id, OrdersSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var ConfirnOrder = await _orderServices.GetById(Id);

                if (ConfirnOrder == null)
                {
                    ModelState.AddModelError("Confirn Order", "Order not Found");

                    return BadRequest(ModelState);
                }

             
                  vm.Id = Id;
                  await _orderServices.Editar(vm, Id);


                var order = await _orderServices.GetById(Id);

                var dishes = new List<SaveDishesForOrder>();

                foreach (var dis in vm.DishesAdd) 
                {
                    dishes = await _dishesordersServices.GetListDishesId(dis);
                }

                List<SaveDishesForOrder> dishesList = dishes.ToList();



                await _dishesordersServices.Update(order.Id, dishesList);

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        public async Task<IActionResult> Get()
        {
            try
            {
                var Orders = await _orderServices.GetAllLINQ();

                if (Orders == null || Orders.Count == 0)
                {
                    return NoContent();
                }

                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
       
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Orders = await _dishesServices.GetByIdAsync(Id);

                if (Orders == null)
                {
                    return NoContent();
                }

                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _orderServices.Eliminar(Id);

                var order = await _orderServices.GetById(Id);

                await _dishesordersServices.Remove(Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
