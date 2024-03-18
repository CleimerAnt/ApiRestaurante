using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Dishes
{
    public class DishesSaveViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required (ErrorMessage = "The Name Field is Required")]
        [DataType (DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Price Filed is Required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "The Number of Person Filed is Required")]
        public int NumberOfPerson { get; set; }
        [Required(ErrorMessage = "The Dish Category Filed is Required")]
        public string DishCategory { get; set; }
        public ICollection<int> ingredients { get; set; }
        [JsonIgnore]
        public ICollection<DishesOrdersViewModel>? dishesOrdersViewModels { get; set; }

    }
}
