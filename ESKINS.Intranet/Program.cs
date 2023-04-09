using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Services;
using ESKINS.Intranet.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.Intranet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // this is for logging exceptions and messages.
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            // Dependency injection here
            builder.Services.AddScoped<IPaymentMethodsServices, PaymentMethodServices>();
            builder.Services.AddScoped<IErrorLogsServices, ErrorLogsServices>();
            builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
            builder.Services.AddScoped<IExteriorsServices, ExteriorsServices>();
            builder.Services.AddScoped<IItemCollectionsServices, ItemCollectionsServices>();
            builder.Services.AddScoped<IItemLocationsServices, ItemLocationsServices>();
            builder.Services.AddScoped<IPhasesServices, PhasesServices>();
            builder.Services.AddScoped<IQualitiesServices, QualitiesServices>();
            builder.Services.AddScoped<IItemsServices, ItemsServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}