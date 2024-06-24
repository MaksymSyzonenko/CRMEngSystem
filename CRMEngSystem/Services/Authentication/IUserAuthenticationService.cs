using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        Task<bool> Register(AccessLevel accessLevel, int contactId, string userName, string password);
    }
}
