using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region "Ingredients"
            CreateMap<Ingredients, IngredientsViewModel>().ReverseMap();
            CreateMap<Ingredients, IngredientsSaveViewModel>().ReverseMap();
            #endregion

            #region "Dishes"
            CreateMap<Dishes, DishesViewModel>().ReverseMap();
            CreateMap<Dishes, DishesSaveViewModel>().ReverseMap();
            #endregion

            #region "DishesIngredients"
            CreateMap<DishesIngredients, DishesIngredientsViewModel>().ReverseMap();
            CreateMap<DishesIngredients, DishesIngredientsSaveViewModel>().ReverseMap();
            #endregion
        }
    }
}
