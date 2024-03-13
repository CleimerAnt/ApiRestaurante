using ApiRestaurante.Core.Domain.Commont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Domain.Entities
{
    public class Tables : AuditableBaseEntity
    {
        public int NumberOfPeoplePerTable { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
