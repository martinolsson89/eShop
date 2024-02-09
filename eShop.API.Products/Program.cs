using eShop.API.DTO;
using eShop.API.Extensions.Extensions;
using eShop.Data.Contexts;
using eShop.Data.Entities;
using eShop.Data.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

            RegisterServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            RegisterEndpoints();

            // Configure CORS
            app.UseCors("CorsAllAccessPolicy");

            app.Run();

            

            void RegisterEndpoints()
            {
                app.AddEndpoint<Product, ProductPostDTO, ProductPutDTO, ProductGetDTO>();
                app.MapGet($"/api/productsbycategory/" + "{categoryId}", async (IDbService db, int categoryId) =>
                {
                    try
                    {
                        var result = await ((ProductDbService)db).GetProductsByCategoryAsync(categoryId);
                        return Results.Ok(result);
                    }
                    catch
                    {
                    }

                    return Results.BadRequest($"Couldn't get the requested products of type {typeof(Product).Name}.");
                });

            }

            void RegisterServices()
            {
                ConfigureAutoMapper();
                builder.Services.AddScoped<IDbService, ProductDbService>();
            }

            void ConfigureAutoMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Product, ProductPostDTO>().ReverseMap();
                    cfg.CreateMap<Product, ProductPutDTO>().ReverseMap();
                    cfg.CreateMap<Product, ProductGetDTO>().ReverseMap();
                    cfg.CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
                });
                var mapper = config.CreateMapper();
                builder.Services.AddSingleton(mapper);
            }
        }
    }
}
