using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Dishes
{
    public class DishesForOrderViewModel
    {
        [Required(ErrorMessage = "The Name Field is Required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
