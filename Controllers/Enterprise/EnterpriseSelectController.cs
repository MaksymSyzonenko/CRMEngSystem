using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Enterprise
{
    public class EnterpriseSelectController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly CRMEngSystemDbContext _context;
        public EnterpriseSelectController(UserManager<UserEntity> userManager, CRMEngSystemDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpPost]
        public async Task UnSelectEnterprise(int EnterpriseId)
        {
            string userId = _userManager.GetUserId(User);
            var table = _context.EnterpriseSelects;
            var selectedEnterprise = await table.FirstOrDefaultAsync(select => select.EnterpriseId == EnterpriseId && select.UserId == userId);
            if (selectedEnterprise != null)
                table.Remove(selectedEnterprise);
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task SelectEnterprise(int EnterpriseId)
        {
            string userId = _userManager.GetUserId(User);
            var table = _context.EnterpriseSelects;
            await table.AddAsync(new EnterpriseSelectEntity
            {
                EnterpriseId = EnterpriseId,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }
    }
}
