using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        public UserAuthenticationService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<bool> Register(AccessLevel accessLevel, int contactId, string userName, string password)
        {
            var user = new UserEntity
            {
                UserName = userName,
                AccessLevel = accessLevel,
                ContactId = contactId
            };
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}
