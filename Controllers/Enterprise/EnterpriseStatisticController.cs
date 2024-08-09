using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Enterprise;
using CRMEngSystem.Services.Analyze;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    public class EnterpriseStatisticController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseStatisticController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> EnterpriseStatistic(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, true, true, true), entity => entity.EnterpriseId, EntityId);
            var analyzer = new OrderAnalyzer(entity.Orders.ToList());
            var (months, orderCounts, totalOrderAmounts) = analyzer.AnalyzeLast12Months();

            return View(new EnterpriseStatisticViewModel
            {
                NumberOrdersPerMonth = orderCounts,
                TotalOrderAmounts = totalOrderAmounts,
                Months = months,
                EntityId = entity!.EnterpriseId,
                ActiveTab = "Statistic",
                NumberOrders = entity.Orders != null ? entity.Orders.Count : 0,
                NumberContacts = entity.Contacts != null ? entity.Contacts.Count : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0,
                NumberHighPriorityOrders = entity.Orders.Count(order => order.Priority == Data.Enums.PriorityValue.High),
                NumberMediumPriorityOrders = entity.Orders.Count(order => order.Priority == Data.Enums.PriorityValue.Medium),
                NumberLowPriorityOrders = entity.Orders.Count(order => order.Priority == Data.Enums.PriorityValue.Low),
                TotalOrdersNumber = entity.Orders.Count
            });
        }
    }
}
