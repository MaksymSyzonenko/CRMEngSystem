using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Services.Analyze;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseDetailsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseDetails(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, true, true, true), entity => entity.EnterpriseId, EntityId);
            
            var analyzer = new OrderAnalyzer(entity.Orders.ToList());
            var (months, orderCounts, averageOrderAmounts) = analyzer.AnalyzeLast12Months();

            return View(new EnterpriseDetailsViewModel
            {
                Enterprise = _mapper.Map<EnterpriseDto>(entity),
                NumberOrdersPerMonth = orderCounts,
                AverangeSumsPerMonthOrders = averageOrderAmounts,
                Months = months,
                EntityId = entity!.EnterpriseId,
                ActiveTab = "Details",
                NumberOrders = entity.Orders != null ? entity.Orders.Count : 0,
                NumberContacts = entity.Contacts != null ? entity.Contacts.Count : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
        public IActionResult CloseModal(int EntityId)
        {
            TempData["NotifyModal"] = false;
            TempData["ErorrNotifyModal"] = false;
            return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
        }
    }
}
