using ApiRestaurante.Core.Domain.Commont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class Ingredients : AuditableBaseEntity
    {
        public string Name { get; set; }

        public ICollection<DishesIngredients> DishesIngredients { get; set; }
    }
}
