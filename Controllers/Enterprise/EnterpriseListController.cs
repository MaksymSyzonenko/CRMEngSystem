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
using CRMEngSystem.Data.Context;
using Microsoft.AspNetCore.Identity;
using CRMEngSystem.Data.Entities.User;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class EnterpriseListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly CRMEngSystemDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        public EnterpriseListController(IMapper mapper, IRepositoryFactory repositoryFactory, CRMEngSystemDbContext context, UserManager<UserEntity> userManager)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> EnterpriseList(EnterpriseListViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var codes = _context.EnterpriseSelects
                                .Where(select => select.UserId == userId)
                                .Select(select => select.EnterpriseId)
                                .ToHashSet();

            model.NumberItemsSelectedEnterprises = codes.Count;

            var entities = _repositoryFactory.Instantiate<EnterpriseEntity>()
                .GetAllEntitiesAsQueryable(new EnterpriseDataLoader(true, false, false, false));
            model.NumberItemsEnterprises = entities.Count();

            if (model.IsSelectedEnterprises)
            {
                entities = entities.Where(entity => codes.Contains(entity.EnterpriseId));
            }

            var sortService = new EnterpriseSortService(model.SortEnterpriseId, model.SortAlphabetStreet, model.SortAlphabetRegion, model.SortAlphabetCity);
            entities = sortService.Sort(entities);

            var searchService = new EnterpriseSearchService(model.SearchGeneral, model.SearchName, model.SearchStreet, model.SearchRegion, model.SearchCity);
            entities = searchService.Search(entities);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);

            var enterprises = _mapper.Map<IEnumerable<EnterpriseListItemDto>>(await entities.ToListAsync());

            foreach (var enterprise in enterprises)
            {
                enterprise.IsSelected = codes.Contains(enterprise.EnterpriseId);
            }

            model.Entities = enterprises;

            return View(model);
        }

    }
}
