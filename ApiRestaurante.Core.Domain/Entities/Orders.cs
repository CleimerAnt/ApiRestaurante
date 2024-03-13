using ApiRestaurante.Core.Domain.Commont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class Orders : AuditableBaseEntity 
    {
        public int TableId { get; set; }
        public double SubTotal { get; set; }    
        public string State { get; set; }
        public Tables Tables { get; set; }  
        public ICollection<Dishes> Dishes { get; set; }
        public ICollection<DishesOrders> DishesOrders { get; set; }
    }
}
