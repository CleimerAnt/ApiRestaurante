using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Application.ViewModel.Tables;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Services
{
    public class TablesServices : GenericServices<TablesViewModel, TablesSaveViewModel, Tables>, ITablesServices
    {
        private readonly ITablesRepository _tablesRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IDishesOrdersRepository _dishesOrdenRepository;
        private readonly IDishesIngredientsRepository _dishhesIngredientesRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMapper _mapper;
        public TablesServices(ITablesRepository tablesRepository, IMapper mapper,IDishesRepository dishesRepository 
            ,IOrdersRepository ordersRepository, IDishesIngredientsRepository dishesIngredientsRepository,
            IDishesOrdersRepository dishesOrdersRepository, IIngredientsRepository ingredientsRepository): base(tablesRepository, mapper)
        {
            _tablesRepository = tablesRepository;
            _mapper = mapper;
            _ordersRepository = ordersRepository;   
            _dishesOrdenRepository = dishesOrdersRepository;
            _dishesRepository = dishesRepository;
            _dishhesIngredientesRepository = dishesIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<List<TablesViewModel>> GetByOrder(int Id)
        {
            var tablesList = await _tablesRepository.GetAll();  
            var orderList = await _ordersRepository.GetAll();

            var table = from o in orderList
                        join t in tablesList
                        on o.TableId equals t.Id
                        where o.State == "In Progress"
                        where t.Id == Id
                        select new TablesViewModel
                        {
                            
                        };

            return table.ToList();  
        }

        public async Task<List<OrdersViewModel>> GetTableOrden(int Id)
        {
            var tablesList = await _tablesRepository.GetAll();
            var orderList = await _ordersRepository.GetAll();
            var dishesOrdenList = await _dishesOrdenRepository.GetAll();
            var dishesIngredientsList = await _dishhesIngredientesRepository.GetAll();
            var dishesList = await _dishesRepository.GetAll();  
            var ingredients = await _ingredientsRepository.GetAll();

            var orden = from o in orderList
                        join t in tablesList on o.TableId equals t.Id
                        where t.Id == Id
                        select new OrdersViewModel
                        {
                            Id = o.Id,
                            State = o.State,
                            SubTotal = o.SubTotal,
                            TableId = o.TableId,
                            Dishes = (from di in dishesOrdenList
                                      join o2 in orderList on di.OrdersId equals o2.Id
                                      join d in dishesList on di.DishesID equals d.Id
                                      where o2.State == "In Progress"
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

            return orden.ToList();  
        }
    }
}
