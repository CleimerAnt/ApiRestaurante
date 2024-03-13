using ApiRestaurante.Core.Domain.Commont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class Dishes : AuditableBaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int NumberOfPerson {  get; set; }
        public string DishCategory { get; set; }
        public ICollection<DishesIngredients> DishesIngredients { get; set; }
        public ICollection<DishesOrders> DishesOrders { get; set; }
    }
}
