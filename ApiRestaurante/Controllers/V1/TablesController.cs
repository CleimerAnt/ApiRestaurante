using ApiRestaurante.Core.Application.Enums;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.Services;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Application.ViewModel.Tables;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class TablesController : BaseApiController
    {
        private readonly ITablesServices _tablesServices;
        private readonly IMapper _mapper;
        public TablesController(ITablesServices tablesServices, IMapper mapper)
        {
            _tablesServices = tablesServices;
            _mapper = mapper;   
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Create(TablesSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                vm.State = "Available";

                await _tablesServices.AddAsync(vm);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TablesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int Id, TablesSaveViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                vm.Id = Id;
                vm.State = "Available";

                await _tablesServices.Editar(vm, Id);

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TablesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Waiter, Admin")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Ingredients = await _tablesServices.GetAllAsync();

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TablesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Waiter, Admin")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Orders = await _tablesServices.GetById(Id);
                TablesViewModel TableVm = _mapper.Map<TablesViewModel>(Orders);

                if (Orders == null)
                {
                    return NoContent();
                }

                return Ok(TableVm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("table/{tableOrdenId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Waiter")]
        public async Task<IActionResult> GetTableOrden(int tableOrdenId)
        {
            try
            {
                var Orders = await _tablesServices.GetTableOrden(tableOrdenId);

                if (Orders.Count == 0)
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

        [HttpPut("table/{changestatusId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeStatusTableViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Waiter")]
        public async Task<IActionResult> ChangeStatus(int changestatusId, ChangeStatusTableViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (vm.State.ToUpper() != "available".ToUpper() && vm.State.ToUpper() != "in the process of care".ToUpper()
                   && vm.State.ToUpper() != "attended".ToUpper())
                {
                    ModelState.AddModelError("Status not found", $"this status {vm.State} is not available");

                    return BadRequest(ModelState);
                }

                vm.Id = changestatusId;
                var table = await _tablesServices.GetById(changestatusId);
                table.State = vm.State;

                TablesSaveViewModel SaveVm = _mapper.Map<TablesSaveViewModel>(table);
            
                await _tablesServices.Editar(SaveVm, changestatusId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
