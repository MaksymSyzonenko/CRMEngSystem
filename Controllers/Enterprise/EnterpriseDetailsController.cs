using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Services.Analyze;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRMEngSystem.Attributes.Cache;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Dto.Contact;
using Microsoft.AspNetCore.Identity;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Context;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class EnterpriseDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        private readonly CRMEngSystemDbContext _context;
        public EnterpriseDetailsController(IMapper mapper, IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager, CRMEngSystemDbContext context)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseDetails(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, true, true, true), entity => entity.EnterpriseId, EntityId);
            
            var analyzer = new OrderAnalyzer(entity.Orders.ToList());
            var (months, orderCounts, totalOrderAmounts) = analyzer.AnalyzeLast12Months();

            string userId = _userManager.GetUserId(User);
            var isEnterpriseSelected = _context.EnterpriseSelects
                .Any(select => select.EnterpriseId == EntityId && select.UserId == userId);

            return View(new EnterpriseDetailsViewModel
            {
                Enterprise = _mapper.Map<EnterpriseDto>(entity),
                LastOrders = _mapper.Map<IEnumerable<EnterpriseOrderDto>>(entity.Orders.OrderByDescending(order => order.DateTimeCreate).Take(5).ToList()),
                Contacts = _mapper.Map<IEnumerable<ContactListItemDto>>(entity!.Contacts!.OrderByDescending(entity => entity.DateTimeCreate)),
                EntityId = entity!.EnterpriseId,
                ActiveTab = "Details",
                NumberOrders = entity.Orders != null ? entity.Orders.Count : 0,
                NumberContacts = entity.Contacts != null ? entity.Contacts.Count : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0,
                IsSelected = isEnterpriseSelected
            });
        }
    }
}
