using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.ViewModel.Dishes
{
    public class SaveDishesForOrder
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name Field is Required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
       

    }
}
