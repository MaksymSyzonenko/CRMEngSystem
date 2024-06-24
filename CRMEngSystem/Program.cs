using CRM_EngSystem_DataBase.Extensions;
using CRMEngSystem.AutoMapper.Account;
using CRMEngSystem.AutoMapper.Catalog;
using CRMEngSystem.AutoMapper.Comment;
using CRMEngSystem.AutoMapper.Contact;
using CRMEngSystem.AutoMapper.Enterprise;
using CRMEngSystem.AutoMapper.Order;
using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Extensions;
using CRMEngSystem.Services.Authentication;
using CRMEngSystem.Services.Cache.User;
using Microsoft.AspNetCore.Identity;

namespace CRMEngSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            //builder.Services.AddMSSqlServerDataBase(configuration.GetConnectionString("MSSQLServerConnectionString")!);

            builder.Services.AddPostgreSQLDataBase(configuration.GetConnectionString("PostgreSQLConnectionString")!);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            ConfigurationSettings.CurrencyCoefficient = configuration.GetValue<decimal>("CurrencyCoefficient");
            ConfigurationSettings.ShippingRatePerKg = configuration.GetValue<decimal>("ShippingRatePerKg");
            ConfigurationSettings.ShippingRatePerCubicMeter = configuration.GetValue<decimal>("ShippingRatePerCubicMeter");

            builder.Services
                .AddDataRepositories()
                .AddAutoMapper(typeof(EnterpriseProfile), typeof(OrderProfile), typeof(ContactProfile), typeof(CatalogProfile), typeof(CommentProfile), typeof(AccountProfile));

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
            builder.Services.AddScoped<UserCacheService>();

            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            builder.Services
                .AddMemoryCache()
                .AddAuthentication();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/AccountLogin/AccountLogin";
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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