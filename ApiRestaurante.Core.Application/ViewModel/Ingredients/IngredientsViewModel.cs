using ApiRestaurante.Core.Application.ViewModel.Dishes;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Ingredients
{
    public class IngredientsViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<DishesViewModel>? Dishes { get; set; }
    }
}
