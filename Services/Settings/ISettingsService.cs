namespace CRMEngSystem.Services.Settings
{
    public interface ISettingsService
    {
        Task UpdateSettingAsync(string key, decimal value);
        Task<Dictionary<string, string>> GetSettingsAsync();
    }
}
