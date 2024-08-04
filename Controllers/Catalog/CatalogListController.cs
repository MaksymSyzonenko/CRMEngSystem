using AutoMapper;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Catalog;
using CRMEngSystem.Services.Filter.Catalog;
using CRMEngSystem.Services.Sort;
using CRMEngSystem.Services.Search;
using CRMEngSystem.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Services.Sort.Catalog;
using CRMEngSystem.Services.Search.Catalog;
using Microsoft.AspNetCore.Authorization;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Catalog
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class CatalogListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public CatalogListController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }

        [HttpGet]
        public async Task<IActionResult> CatalogList(CatalogListViewModel model)
        {
            var entities = _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().GetAllEntitiesAsQueryable(new EquipmentCatalogPositionDataLoader(false, false, false));

            var filterService = new CatalogFilterService(model.FilterType, model.FilterMinBasePrice, model.FilterMaxBasePrice, model.FilterMinWeight, model.FilterMaxWeight, model.FilterMinVolume, model.FilterMaxVolume);
            entities = filterService.Filter(entities);

            var sortService = new CatalogSortService(model.SortCode, model.SortAlphabetNameEN, model.SortPrice, model.SortWeight, model.SortVolume);
            entities = sortService.Sort(entities);

            var searchService = new CatalogSearchService(model.SearchGeneral, model.SearchEquipmentCode, model.SearchNameEN);
            entities = searchService.Search(entities);


            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);

            model.Entities = _mapper.Map<IEnumerable<CatalogListItemDto>>(await entities.ToListAsync());

            return View(model);
        }
    }
}
