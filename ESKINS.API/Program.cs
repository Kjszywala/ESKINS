using ESKINS.API.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //==== Add this for versioning.====
            builder.Services.AddControllers(); 
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint));
            });
            //=================================
            builder.Services.AddDbContext<DatabaseContext>(
                options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EskinsDbContext"))
                );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}