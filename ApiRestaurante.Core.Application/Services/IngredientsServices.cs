using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
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
    public class IngredientsServices : GenericServices<IngredientsViewModel, IngredientsSaveViewModel, Ingredients>, IIngredientsServices
    {
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMapper _mapper;
        public IngredientsServices(IIngredientsRepository ingredientsRepository, IMapper mapper) : base(ingredientsRepository, mapper)
        {
            _ingredientsRepository = ingredientsRepository;        
            _mapper = mapper;
        }
        public async Task<IngredientsViewModel> ConfirnIngrediente(string name)
        {
            var ingrediente = await _ingredientsRepository.ConfirnIngrediente(name);

            IngredientsViewModel ingredienteVm = _mapper.Map<IngredientsViewModel>(ingrediente);

            return ingredienteVm;
        }
    }
}
