using AutoMapper;
using CRMEngSystem.Attributes.Cache;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.WareHouse;
using CRMEngSystem.Models.ViewModels.WareHouse;
using CRMEngSystem.Services.Search.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class WareHouseListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseListController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> WareHouseList(WareHouseListViewModel model)
        {
            GC.Collect(); // Принудительная сборка мусора
            GC.WaitForPendingFinalizers(); // Ожидание завершения
            Console.WriteLine("Clean success!");

            var entities = _repositoryFactory.Instantiate<WareHouseEntity>().GetAllEntitiesAsQueryable(new WareHouseDataLoader(false));

            var searchService = new WareHouseSearchService(model.SearchGeneral);
            entities = searchService.Search(entities);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);
            model.Entities = _mapper.Map<IEnumerable<WareHouseListItemDto>>(await entities.ToListAsync());

            return View(model);
        }
    }
}
