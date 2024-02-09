using eShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eShop.API.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // SQL Server Service Registration
            builder.Services.AddDbContext<EShopContext>(
                options =>
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("ElectronicShopConnection")));

//CORS (Cross Origin Resource Sharing)
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("CorsAllAccessPolicy", opt =>
                    opt.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Configure CORS
            app.UseCors("CorsAllAccessPolicy");

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.Run();
        }
    }
}
