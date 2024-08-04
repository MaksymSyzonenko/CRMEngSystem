using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.User;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Account
{
    public class AccountChangeAccessLevelController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public AccountChangeAccessLevelController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> AccountChangeAccessLevel(string AccessLevel, int ContactId)
        {
            AccessLevel accessLevel = Data.Enums.AccessLevel.Low;
            switch (AccessLevel)
            {
                case "1 рівень (Максимальний)":
                    accessLevel = Data.Enums.AccessLevel.High;
                    break;
                case "2 рівень (Середній)":
                    accessLevel = Data.Enums.AccessLevel.Medium;
                    break;
            }
            var repository = _repositoryFactory.Instantiate<UserEntity>();
            var user = await repository.GetAllEntitiesAsQueryable(new UserDataLoader(true, false, false)).FirstOrDefaultAsync(user => user.ContactId == ContactId);
            user.AccessLevel = accessLevel;
            await repository.UpdateEntityAsync(user.Id, user);
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
