using ApiRestaurante.Core.Application.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Tables
{
    public class ChangeStatusTableViewModel
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [JsonIgnore]
        public int? NumberOfPeoplePerTable { get; set; }
        [JsonIgnore]
        public string? Description { get; set; }
        [JsonRequired]
        public string State { get; set; }
        [JsonIgnore]
        public ICollection<OrdersViewModel>? Orders { get; set; }
    }
}
