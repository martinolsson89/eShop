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
                app.AddEndpoint<Color, ColorPostDTO, ColorPutDTO, ColorGetDTO>();
                app.AddEndpoint<Brand, BrandPostDTO, BrandPutDTO, BrandGetDTO>();
                app.AddEndpoint<Fuel, FuelPostDTO, FuelPutDTO, FuelGetDTO>();
                app.AddEndpoint<ProductCategory, ProductCategoryPostDTO, ProductCategoryDeleteDTO>();
                app.AddEndpoint<ProductColor, ProductColorPostDTO, ProductColorDeleteDTO>();
                app.AddEndpoint<ProductFuel, ProductFuelPostDTO, ProductFuelDeleteDTO>();
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
                    cfg.CreateMap<Color, ColorPostDTO>().ReverseMap();
                    cfg.CreateMap<Color, ColorPutDTO>().ReverseMap();
                    cfg.CreateMap<Color, ColorGetDTO>().ReverseMap();
                    cfg.CreateMap<Brand, BrandPostDTO>().ReverseMap();
                    cfg.CreateMap<Brand, BrandPutDTO>().ReverseMap();
                    cfg.CreateMap<Brand, BrandGetDTO>().ReverseMap();
                    cfg.CreateMap<Fuel, FuelPostDTO>().ReverseMap();
                    cfg.CreateMap<Fuel, FuelPutDTO>().ReverseMap();
                    cfg.CreateMap<Fuel, FuelGetDTO>().ReverseMap();
                    cfg.CreateMap<ProductCategory, ProductCategoryPostDTO>().ReverseMap();
                    cfg.CreateMap<ProductCategory, ProductCategoryDeleteDTO>().ReverseMap();
                    cfg.CreateMap<ProductColor, ProductColorPostDTO>().ReverseMap();
                    cfg.CreateMap<ProductColor, ProductColorDeleteDTO>().ReverseMap();
                    cfg.CreateMap<ProductFuel, ProductFuelPostDTO>().ReverseMap();
                    cfg.CreateMap<ProductFuel, ProductFuelDeleteDTO>().ReverseMap();

                    
                });
                var mapper = config.CreateMapper();
                builder.Services.AddSingleton(mapper);
            }
        }
    }
}
