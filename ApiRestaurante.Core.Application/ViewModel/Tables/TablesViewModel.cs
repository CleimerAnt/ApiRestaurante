using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Tables
{
    public class TablesViewModel
    {
        public int Id { get; set; }
        public int NumberOfPeoplePerTable { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        [JsonIgnore]
        public ICollection<OrdersViewModel> Orders { get; set; }
    }
}
