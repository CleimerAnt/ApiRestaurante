using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Interfaces.Services
{
    public interface IOrdersServices : IGenericService<OrdersViewModel, OrdersSaveViewModel, Orders>
    {
        Task<List<OrdersViewModel>> GetAllLINQ();
    }
}
