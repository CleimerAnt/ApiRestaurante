using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Dishes
{
    public class DishesViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; } 
        public double Price { get; set; }
        public int NumberOfPerson { get; set; }
        [DataType(DataType.Text)]
        public string DishCategory { get; set; }
        public ICollection<IngredientsViewModel> ingredients { get; set; }
    }
}
