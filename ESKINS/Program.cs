using ESKINS.BusinessLogic.BusinessLogic;
using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESKINS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DatabaseContext>();
            builder.Services.AddControllersWithViews();

			builder.Services.AddScoped<IPaymentMethodsServices, PaymentMethodServices>();
            builder.Services.AddScoped<IErrorLogsServices, ErrorLogsServices>();
            builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
            builder.Services.AddScoped<IExteriorsServices, ExteriorsServices>();
            builder.Services.AddScoped<IItemCollectionsServices, ItemCollectionsServices>();
            builder.Services.AddScoped<IItemLocationsServices, ItemLocationsServices>();
            builder.Services.AddScoped<IPhasesServices, PhasesServices>();
            builder.Services.AddScoped<IQualitiesServices, QualitiesServices>();
            builder.Services.AddScoped<IItemsServices, ItemsServices>();
            builder.Services.AddScoped<IUsersServices, UsersServices>();
            builder.Services.AddScoped<IItemLogsServices, ItemLogsServices>();
            builder.Services.AddScoped<IItemLogic, ItemLogic>();
            builder.Services.AddScoped<ISendEmailServiceLogic, SendEmailServiceLogic>();
            builder.Services.AddScoped<ISellersServices, SellersServices>();
            builder.Services.AddScoped<IItemLogsServices, ItemLogsServices>();
            builder.Services.AddScoped<ICustomersServices, CustomersServices>();
            builder.Services.AddScoped<IOrdersServices, OrdersServices>();
            builder.Services.AddScoped<IItemPriceHistoriesServices, ItemPriceHistoriesServices>();
            builder.Services.AddScoped<ITargetsServices, TargetsServices>();
            builder.Services.AddScoped<IInvoicesServices, InvoicesServices>();
			builder.Services.AddScoped<ISaleCartServices, SaleCartServices>();
			builder.Services.AddScoped<ISaleCartLogic, SaleCartLogic>();
			builder.Services.AddScoped<ICartServices, CartServices>();
            builder.Services.AddScoped<ICartLogic, CartLogic>();
            builder.Services.AddSession();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<CardSessionLogic>();

            var app = builder.Build();
            app.UseSession();
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