using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Domain.Entities;
using ApiRestaurante.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Infraestructure.Persistence.Repositories
{
    public class DishesOrdersRepository : GenericRepository<DishesOrders>, IDishesOrdersRepository
    {
        private readonly ApplicationContext _context;
        public DishesOrdersRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task Remove(int Id)
        {
            var dishesOrder = await _context.DishesOrders.FirstOrDefaultAsync( d => d.OrdersId == Id);

            if (dishesOrder != null) 
            {
                _context.DishesOrders.Remove(dishesOrder);
                _context.SaveChanges();
            }
        }

        public async Task Update(int Id, List<Dishes> dishesList)
        {
            var dishesOrders = await _context.DishesOrders.FirstOrDefaultAsync(d => d.OrdersId == Id);


            if (dishesOrders != null)
            {
                _context.DishesOrders.Remove(dishesOrders);
                _context.SaveChanges();


                foreach (var dishes in dishesList)
                {

                    var dishesId = await _context.Dishes.FirstOrDefaultAsync(i => i.Id == dishes.Id);

                    var DishesOrders = new DishesOrders
                    {
                        OrdersId = Id,
                        DishesID = dishesId.Id
                    };

                    await _context.Set<DishesOrders>().AddAsync(DishesOrders);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
