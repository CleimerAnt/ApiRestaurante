using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Orders
{
    public class OrdersSaveViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required (ErrorMessage = "The Table Field is Required")]
        public int TableId { get; set; }
        [Required (ErrorMessage = "The Sub Total field is Required")]
        public double SubTotal { get; set; }
        [Required(ErrorMessage = "The State field is Required")]
        [DataType(DataType.Text)]
        public string State { get; set; }
        [Required(ErrorMessage = "The State field is Required")]
        public ICollection<DishesViewModel> DishesAdd { get; set; }
        [JsonIgnore]
        public ICollection<DishesViewModel>? Dishes { get; set; }
        [JsonIgnore]
        public ICollection<DishesOrdersViewModel>? DishesOrders { get; set; }
    }
}
