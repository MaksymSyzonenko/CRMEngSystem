using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.Settings;
using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRMEngSystem.Controllers.Settings
{
    [ApiController]
    [Route("Settings/[controller]")]
    public class ColumnResizeSettingsController : ControllerBase
    {
        private readonly CRMEngSystemDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public ColumnResizeSettingsController(CRMEngSystemDbContext context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetColumnSizes")]
        public async Task<IActionResult> GetColumnSizes(string pageIdentifier)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var settings = await _context.ColumnResizeSettings
                .FirstOrDefaultAsync(s => s.PageIdentifier == pageIdentifier && s.UserId == userId);

            if (settings == null)
            {
                return NotFound();
            }

            return Ok(new { columnSizes = settings.ColumnSizes });
        }

        [HttpPost("SaveColumnSizes")]
        public async Task<IActionResult> SaveColumnSizes([FromBody] ColumnResizeSettingsDto dto)
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;

            var settings = await _context.ColumnResizeSettings
                .FirstOrDefaultAsync(s => s.PageIdentifier == dto.PageIdentifier && s.UserId == userId);

            if (settings == null)
            {
                await _context.ColumnResizeSettings.AddAsync(new ColumnResizeSettingsEntity
                {
                    PageIdentifier = dto.PageIdentifier,
                    ColumnSizes = dto.ColumnSizes,
                    UserId = userId
                });
            }
            else
            {
                settings.ColumnSizes = dto.ColumnSizes;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
    public class ColumnResizeSettingsDto
    {
        public string PageIdentifier { get; set; } = default!;
        public string ColumnSizes { get; set; } = default!;
    }

}
