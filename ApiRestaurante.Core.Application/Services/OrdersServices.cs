using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
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
        private readonly IDishesIngredientsRepository _dishesIngredientsRepository;
        private readonly IDishesOrdersRepository _DishesOrdersRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;
        private readonly IIngredientsRepository _ingredientsRepository;  
        public OrdersServices(IOrdersRepository ordersRepository, IMapper mapper,IDishesRepository dishesRepository ,IDishesOrdersRepository dishesOrdersRepository, IDishesIngredientsRepository dishesIngredientsRepository, IIngredientsRepository ingredientsRepository): base(ordersRepository, mapper) 
        {
            _OrderRepository = ordersRepository;
            _mapper = mapper;
            _DishesOrdersRepository = dishesOrdersRepository;
            _dishesRepository = dishesRepository;
            _dishesIngredientsRepository = dishesIngredientsRepository;
            _ingredientsRepository = ingredientsRepository; 
        }

       public async Task<List<OrdersViewModel>> GetAllLINQ()
       {
            var dishesList = await _dishesRepository.GetAll();
            var ordersList = await _OrderRepository.GetAll();
            var dihesOrderList = await _DishesOrdersRepository.GetAll();
            var dishesIngredientsList = await _dishesIngredientsRepository.GetAll();
            var ingredients = await _ingredientsRepository.GetAll();

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
                                           Price = d.Price,
                                           NumberOfPerson = d.NumberOfPerson,
                                           DishCategory = d.DishCategory,
                                           ingredients = (from di in dishesIngredientsList
                                                          join d2 in dishesList on di.DishesId equals d2.Id
                                                          join i in ingredients on di.IngredientId equals i.Id
                                                          where d.Id == di.DishesId
                                                          select new IngredientsViewModel
                                                          {
                                                              Id = i.Id,
                                                              Name = i.Name
                                                          }).ToList()
                                       }).ToList()
                         };

            return orders.ToList();

       }
       
    }
}
