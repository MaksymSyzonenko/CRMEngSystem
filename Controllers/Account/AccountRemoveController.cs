using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.User;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Account
{
    public class AccountRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public AccountRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public IActionResult AccountRemove(int ContactId)
        {
            TempData["ContactId"] = ContactId;
            TempData["ConfirmModal"] = true;
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
        public async Task<IActionResult> ConfirmModal(int ContactId)
        {
            if((await _userManager.GetUserAsync(User)).AccessLevel == Data.Enums.AccessLevel.High)
            {
                var repository = _repositoryFactory.Instantiate<UserEntity>();
                var user = await repository.GetAllEntitiesAsQueryable(new UserDataLoader(true, false, false)).FirstOrDefaultAsync(user => user.ContactId == ContactId);
                await repository.RemoveEntityAsync(user);
            }   
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
