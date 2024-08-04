using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Account;
using CRMEngSystem.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Account
{
    [Authorize]
    public class AccountDetailsController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public AccountDetailsController(UserManager<UserEntity> userManager, IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> AccountDetails()
        {
            var user = (await _userManager.GetUserAsync(User))!;
            var entity = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(false, false, false, true, true), contact => contact.ContactId, user.ContactId);
            return View(new AccountDetailsViewModel
            {
                EntityId = entity!.ContactId,
                Account = _mapper.Map<AccountDto>(user),
                ActiveTab = "Account",
                NumberOrders = entity!.ContactOrders!.Count,
                NumberComments = entity.Comments!.Count
            });
        }
    }
}
