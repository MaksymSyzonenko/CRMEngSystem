using AutoMapper;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Catalog;
using CRMEngSystem.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Catalog
{
    [Authorize]
    public class CatalogDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public CatalogDetailsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> CatalogDetails(int EntityId)
        {
            var equipment = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().GetEntityAsync(new EquipmentCatalogPositionDataLoader(true, false, false), equipment => equipment.EquipmentCatalogPositionId, EntityId);
            return View(new CatalogDetailsViewModel
            {
                EntityId = EntityId,
                CatalogPosition = _mapper.Map<CatalogPositionDto>(equipment)
            });
        }
    }
}
