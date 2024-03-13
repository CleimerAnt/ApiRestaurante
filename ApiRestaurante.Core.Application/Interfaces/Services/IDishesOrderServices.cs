using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IDishesOrderServices : IGenericService<DishesOrdersViewModel, DishesOrderSaveViewModel, DishesOrders>
    {
        Task Update(int id, List<DishesViewModel> dishesListVm);
        Task Remove(int Id);
    }
}
