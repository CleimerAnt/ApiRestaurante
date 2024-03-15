using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Services
{
    public class DishesOrdersServices : GenericServices <DishesOrdersViewModel, DishesOrderSaveViewModel, DishesOrders>, IDishesOrderServices
    {
        private readonly IDishesOrdersRepository _dishesordersRepository;
        private readonly IMapper _mapper;
        public DishesOrdersServices(IDishesOrdersRepository dishesOrdersRepository, IMapper mapper) : base(dishesOrdersRepository, mapper)
        {
            _dishesordersRepository = dishesOrdersRepository;
            _mapper = mapper;
        }

        public async Task Remove(int Id)
        {
          await  _dishesordersRepository.Remove(Id);
        }

        public async Task Update(int id, List<SaveDishesForOrder> dishesListVm)
        {
            List<Dishes> ingredients = _mapper.Map<List<Dishes>>(dishesListVm);

            await _dishesordersRepository.Update(id, ingredients);
        }
    }
}
