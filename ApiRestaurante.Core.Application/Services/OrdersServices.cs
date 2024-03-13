using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Services
{
    public class OrdersServices : GenericServices<OrdersViewModel, OrdersSaveViewModel, Orders>, IOrdersServices
    {
        private readonly IOrdersRepository _OrderRepository;
        private readonly IDishesOrdersRepository _DishesOrdersRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;
        public OrdersServices(IOrdersRepository ordersRepository, IMapper mapper,IDishesRepository dishesRepository ,IDishesOrdersRepository dishesOrdersRepository): base(ordersRepository, mapper) 
        {
            _OrderRepository = ordersRepository;
            _mapper = mapper;
            _DishesOrdersRepository = dishesOrdersRepository;
            _dishesRepository = dishesRepository;
        }

       public async Task<List<OrdersViewModel>> GetAllLINQ()
       {
            var dishesList = await _dishesRepository.GetAll();
            var ordersList = await _OrderRepository.GetAll();
            var dihesOrderList = await _DishesOrdersRepository.GetAll();

            var orders = from o in ordersList
                         select new OrdersViewModel
                         {
                             Id = o.Id,
                             State = o.State,
                             SubTotal = o.SubTotal,
                             TableId = o.TableId,
                             Dishes = (from DO in dihesOrderList
                                       join d in dishesList on DO.DishesID equals d.Id
                                       where o.Id == DO.OrdersId
                                       select new DishesViewModel
                                       {
                                           Name = d.Name,
                                       }).ToList()
                         };

            return orders.ToList();

       }
       
    }
}
