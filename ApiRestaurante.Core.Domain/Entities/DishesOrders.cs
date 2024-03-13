using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class DishesOrders
    {
        public int DishesID { get; set; }   
        public Dishes Dishes { get; set; }  

        public int OrdersId { get; set; }   
        public Orders Orders { get; set; }
        
    }
}
