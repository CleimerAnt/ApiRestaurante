using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.DishesOrders
{
    public class DishesOrdersViewModel
    {
        public int DishesID { get; set; }
        public DishesViewModel Dishes { get; set; }

        public int OrdersId { get; set; }
        public OrdersViewModel Orders { get; set; }
    }
}
