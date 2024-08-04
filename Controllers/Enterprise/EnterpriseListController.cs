using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Services.Search.Enterprise;
using CRMEngSystem.Services.Sort.Enterprise;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class EnterpriseListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseListController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }

        [HttpGet]
        public async Task<IActionResult> EnterpriseList(EnterpriseListViewModel model)
        {
            var entities = _repositoryFactory.Instantiate<EnterpriseEntity>().GetAllEntitiesAsQueryable(new EnterpriseDataLoader(true, false, false, false));

            var sortService = new EnterpriseSortService(model.SortEnterpriseId, model.SortAlphabetCountry, model.SortAlphabetRegion, model.SortAlphabetCity);
            entities = sortService.Sort(entities);

            var searchService = new EnterpriseSearchService(model.SearchGeneral, model.SearchName, model.SearchCountry, model.SearchRegion, model.SearchCity);
            entities = searchService.Search(entities);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);
            model.Entities = _mapper.Map<IEnumerable<EnterpriseListItemDto>>(await entities.ToListAsync());

            return View(model);
        }
    }
}
