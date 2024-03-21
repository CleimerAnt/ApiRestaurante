using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Orders
{
    public class EditOrderViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Table Field is Required")]
        [JsonIgnore]
        public int TableId { get; set; }
        [Required(ErrorMessage = "The Sub Total field is Required")]
        [JsonIgnore]
        public double SubTotal { get; set; }
        [JsonIgnore]
        public string? State { get; set; }
        [Required(ErrorMessage = "The State field is Required")]
        public ICollection<int> DishesAdd { get; set; }
        [JsonIgnore]
        public ICollection<DishesViewModel>? Dishes { get; set; }
        [JsonIgnore]
        public ICollection<DishesOrdersViewModel>? DishesOrders { get; set; }
    }
}
