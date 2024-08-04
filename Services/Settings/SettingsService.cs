using CRMEngSystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CRMEngSystem.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly CRMEngSystemDbContext _context;

        public SettingsService(CRMEngSystemDbContext context)
        {
            _context = context;
        }
        public async Task UpdateSettingAsync(string key, decimal value)
        {
            var setting = await _context.Settings.SingleOrDefaultAsync(s => s.Key == key);
            if (setting != null)
            {
                setting.Value = value.ToString("0.00", CultureInfo.InvariantCulture);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            var settings = await _context.Settings.ToListAsync();
            var result = new Dictionary<string, string>();
            settings.ForEach(setting => result.Add(setting.Key, setting.Value));
            return result;
        }

    }
}
