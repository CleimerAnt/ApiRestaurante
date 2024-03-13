using ApiRestaurante.Core.Application.Interfaces.Repositories;
using ApiRestaurante.Core.Application.Interfaces.Services;
using ApiRestaurante.Core.Application.ViewModel.Tables;
using ApiRestaurante.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private readonly IMapper _mapper;
        public TablesServices(ITablesRepository tablesRepository, IMapper mapper, IOrdersRepository ordersRepository): base(tablesRepository, mapper)
        {
            _tablesRepository = tablesRepository;
            _mapper = mapper;
            _ordersRepository = ordersRepository;   
        }

        public async Task<List<TablesViewModel>> GetByOrder(int Id)
        {
            var tablesList = await _tablesRepository.GetAll();  
            var orderList = await _ordersRepository.GetAll();

            var table = from o in orderList
                        join t in tablesList
                        on o.TableId equals t.Id
                        where o.State == "En proceso"
                        where t.Id == Id
                        select new TablesViewModel
                        {
                            
                        };

            return table.ToList();  
        }
    }
}
