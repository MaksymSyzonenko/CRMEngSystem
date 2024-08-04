using AutoMapper;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.WareHouse;
using CRMEngSystem.Models.ViewModels.WareHouse;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseDetailsController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public WareHouseDetailsController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> WareHouseDetails(WareHouseDetailsViewModel model)
        {
            var wareHouse = await _repositoryFactory
                .Instantiate<WareHouseEntity>()
                .GetEntityAsync(new WareHouseDataLoader(true), wareHouse => wareHouse.WareHouseId, model.WareHouseId);

            var entities = wareHouse!.EquipmentWareHousePositions!.AsEnumerable();
            int totalQuantity = entities.Sum(equipment => equipment.Quantity);
            var numberedEntities = entities.Select((entity, index) => new
            {
                Entity = entity,
                Number = index + 1 
            }).ToList();

            // Применение фильтрации
            if (!string.IsNullOrEmpty(model.SearchGeneral))
            {
                numberedEntities = numberedEntities.Where(item =>
                    item.Entity.EquipmentCatalogPosition.NameUA.ToLower().Contains(model.SearchGeneral.ToLower()) ||
                    item.Entity.EquipmentCatalogPosition.NameEN.ToLower().Contains(model.SearchGeneral.ToLower()) ||
                    item.Entity.EquipmentCatalogPosition.Country.ToLower().Contains(model.SearchGeneral.ToLower()) ||
                    item.Entity.EquipmentCatalogPosition.EquipmentCode == model.SearchGeneral ||
                    item.Entity.EquipmentCatalogPosition.Producer.ToLower().Contains(model.SearchGeneral.ToLower()) ||
                    item.Entity.EquipmentCatalogPosition.Type.ToString().ToLower().Contains(model.SearchGeneral.ToLower())
                ).ToList();
            }

            model.TotalPageCount = (int)Math.Ceiling((decimal)numberedEntities.Count / model.NumberItemsPerPage);

            numberedEntities = numberedEntities
                .Skip((model.CurrentPage - 1) * model.NumberItemsPerPage)
                .Take(model.NumberItemsPerPage)
                .ToList();

            model.EquipmentWareHousePositions = numberedEntities.Select(item => new EquipmentWareHousePositionDto
            {
                EquipmentCatalogPositionId = item.Entity.EquipmentCatalogPositionId,
                EquipmentWareHousePositionId = item.Entity.EquipmentWareHousePositionId,
                EquipmentCode = item.Entity.EquipmentCatalogPosition.EquipmentCode,
                NameUA = item.Entity.EquipmentCatalogPosition.NameUA,
                Weight = item.Entity.EquipmentCatalogPosition.Weight,
                Volume = item.Entity.EquipmentCatalogPosition.Volume,
                BasePrice = item.Entity.EquipmentCatalogPosition.BasePrice,
                Quantity = item.Entity.Quantity,
                Number = item.Number
            });

            model.WareHouseName = wareHouse.Name;
            model.Coordinate = wareHouse.Coordinate;
            model.Country = wareHouse.Country;
            model.City = wareHouse.City;
            model.PostalCode = wareHouse.PostalCode;
            model.Region = wareHouse.Region;
            model.Street = wareHouse.Street;
            model.TotalQuantity = totalQuantity;
            model.AccessLevel = (await _userManager.GetUserAsync(User)!).AccessLevel;

            return View(model);
        }
    }
}
