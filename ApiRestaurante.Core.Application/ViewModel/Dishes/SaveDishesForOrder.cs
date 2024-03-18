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
        public int Id { get; set; }
       
        public string Name { get; set; }    
    }
}
