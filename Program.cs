using CRMEngSystem.Data.Extensions;
using CRMEngSystem.AutoMapper.Account;
using CRMEngSystem.AutoMapper.Catalog;
using CRMEngSystem.AutoMapper.Comment;
using CRMEngSystem.AutoMapper.Contact;
using CRMEngSystem.AutoMapper.Enterprise;
using CRMEngSystem.AutoMapper.Order;
using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using CRMEngSystem.AutoMapper.WareHouse;
using CRMEngSystem.Attributes.Cache;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using CRMEngSystem.Services.Settings;
using Microsoft.AspNetCore.HttpOverrides;

namespace CRMEngSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
            builder.WebHost.UseUrls($"http://*:{port}");

            builder.Services.AddControllersWithViews();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            builder.Services.AddPostgreSQLDataBase(configuration.GetConnectionString("PostgreSQLConnectionString")!);

            ////builder.Services.AddPostgreSQLDataBase(configuration.GetConnectionString("PostgreSQLConnectionStringLocal")!);

            builder.Services.AddScoped<ISettingsService, SettingsService>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var templates = configuration.GetSection("Templates");
            ConfigurationSettings.WordTemplate = templates["WordTemplate"]!;
            ConfigurationSettings.ExcelTemplate = templates["ExcelTemplate"]!;

            builder.Services
                .AddDataRepositories()
                .AddAutoMapper(
                typeof(EnterpriseProfile),
                typeof(OrderProfile),
                typeof(ContactProfile),
                typeof(CatalogProfile),
                typeof(CommentProfile),
                typeof(AccountProfile),
                typeof(WareHouseProfile));

            builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

            }).AddEntityFrameworkStores<CRMEngSystemDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<UserEntity>>();

            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            builder.Services
                .AddMemoryCache()
                .AddAuthentication();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(90);
                options.LoginPath = $"/AccountLogin/AccountLogin";
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(90);
                options.SlidingExpiration = true;
            });


            builder.Services.AddScoped<ClearCacheAttribute>();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var defaultCulture = new CultureInfo("uk-UA");
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = new List<CultureInfo> { defaultCulture };
                options.SupportedUICultures = new List<CultureInfo> { defaultCulture };
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var settingsService = scope.ServiceProvider.GetRequiredService<ISettingsService>();
                var settings = await settingsService.GetSettingsAsync();
                if (settings.TryGetValue("CurrencyCoefficient", out var currencyCoefficientStr) &&
                    decimal.TryParse(currencyCoefficientStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var currencyCoefficient))
                {
                    ConfigurationSettings.CurrencyCoefficient = currencyCoefficient;
                }

                if (settings.TryGetValue("CurrencyCoefficient_UAH_EUR", out var currencyCoefficientUAHEURStr) &&
                    decimal.TryParse(currencyCoefficientUAHEURStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var currencyCoefficientUAHEUR))
                {
                    ConfigurationSettings.CurrencyCoefficient_UAH_EUR = currencyCoefficientUAHEUR;
                }

                if (settings.TryGetValue("ShippingRatePerKg", out var shippingRatePerKgStr) &&
                    decimal.TryParse(shippingRatePerKgStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var shippingRatePerKg))
                {
                    ConfigurationSettings.ShippingRatePerKg = shippingRatePerKg;
                }

                if (settings.TryGetValue("ShippingRatePerCubicMeter", out var shippingRatePerCubicMeterStr) &&
                    decimal.TryParse(shippingRatePerCubicMeterStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var shippingRatePerCubicMeter))
                {
                    ConfigurationSettings.ShippingRatePerCubicMeter = shippingRatePerCubicMeter;
                }
            }

            var supportedCultures = new[] { new CultureInfo("uk-UA")};
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("uk-UA"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AccountLogin}/{action=AccountLogin}");

            app.Run();
        }
    }
}
