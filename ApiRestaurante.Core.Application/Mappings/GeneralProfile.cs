using ApiRestaurante.Core.Application.ViewModel.Dishes;
using ApiRestaurante.Core.Application.ViewModel.DishesIngredients;
using ApiRestaurante.Core.Application.ViewModel.DishesOrders;
using ApiRestaurante.Core.Application.ViewModel.Ingredients;
using ApiRestaurante.Core.Application.ViewModel.Orders;
using ApiRestaurante.Core.Application.ViewModel.Tables;
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

            #region"Tables"
            CreateMap<Tables, TablesViewModel>().ReverseMap();
            CreateMap<Tables, TablesSaveViewModel>().ReverseMap();
            #endregion

            #region "Orders"
            CreateMap<Orders, OrdersViewModel>().ReverseMap();
            CreateMap<Orders, OrdersSaveViewModel>()
                .ForMember(opt => opt.DishesAdd, i => i.Ignore())
                .ReverseMap();
            #endregion

            #region "DishesOrders"
            CreateMap<DishesOrders, DishesOrdersViewModel>().ReverseMap();
            CreateMap<DishesOrders, DishesOrderSaveViewModel>().ReverseMap();
            #endregion
        }
    }
}
