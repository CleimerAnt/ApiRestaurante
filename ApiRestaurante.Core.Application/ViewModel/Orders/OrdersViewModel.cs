using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Orders
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public double SubTotal { get; set; }
        [DataType(DataType.Text)]
        public string State { get; set; }
        public ICollection<DishesViewModel> Dishes { get; set; }
        public ICollection<DishesOrdersViewModel> DishesOrders { get; set; }
    }
}
