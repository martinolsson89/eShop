using AutoMapper;
using eShop.API.DTO;
using eShop.API.Extensions.Extensions;
using eShop.Data.Contexts;
using eShop.Data.Entities;
using eShop.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace eShop.API.Filter
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

            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("CorsAllAccessPolicy", opt =>
                    opt.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            RegisterServices(builder.Services);

            var app = builder.Build();

            RegisterEndpoints(app);

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


            app.Run();

            void RegisterServices(IServiceCollection services)
            {
                ConfigureAutoMapper(builder.Services);
                services.AddScoped<IDbService, FilterDbService>();
            }

            void RegisterEndpoints(WebApplication app)
            {
                app.AddEndpoint<Data.Entities.Filter, FilterPostDTO, FilterPutDTO, FilterGetDTO>();
                app.AddEndpoint<CategoryFilter, CategoryFilterPostDTO, CategoryFilterDeleteDTO>();
                //app.AddEndpoint<FilterType, FilterTypePostDTO, FilterTypePutDTO, FilterTypeGetDTO>();
                //app.AddEndpoint<Option, OptionPostDTO, OptionPutDTO, OptionGetDTO>();

                /*app.MapPost("/api/filterproducts", async (List<FilterRequestDTO> filterDTOs, IFilterService filterService) =>
                {
                    try
                    {
                        // Assuming filterService.ProcessFiltering() is your method to apply filters
                        var filterDTOs = filterService.ProcessFiltering(filterRequest);
                        return Results.Ok(filterDTOs);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details and return an appropriate error response
                        return Results.Problem(ex.Message);
                    }
                });*/
                app.MapPost("/api/filterproducts", async (List<FilterRequestDTO> filterDTOs, IDbService db) =>
                {
                    try
                    {
                        return Results.Ok(await ((FilterDbService)db).FilterProducts(filterDTOs));
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details and return an appropriate error response
                        return Results.Problem(ex.Message);
                    }
                });

            }

            void ConfigureAutoMapper(IServiceCollection services)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Data.Entities.Filter, FilterPostDTO>().ReverseMap();
                    cfg.CreateMap<Data.Entities.Filter, FilterPutDTO>().ReverseMap();
                    cfg.CreateMap<Data.Entities.Filter, FilterGetDTO>().ReverseMap();
                    cfg.CreateMap<Product, ProductGetDTO>().ReverseMap();
                    cfg.CreateMap<Fuel, FuelGetDTO>().ReverseMap();
                    cfg.CreateMap<Color, ColorGetDTO>().ReverseMap();
                    cfg.CreateMap<Brand, BrandGetDTO>().ReverseMap();
                    /*cfg.CreateMap<FilterType, FilterTypeGetDTO>().ReverseMap();
                    cfg.CreateMap<Option, OptionPostDTO>().ReverseMap();
                    cfg.CreateMap<Option, OptionPutDTO>().ReverseMap();
                    cfg.CreateMap<Option, OptionGetDTO>().ReverseMap();*/
                    cfg.CreateMap<CategoryFilter, CategoryFilterPostDTO>().ReverseMap();
                    cfg.CreateMap<CategoryFilter, CategoryFilterDeleteDTO>().ReverseMap();
                });
                var mapper = config.CreateMapper();
                services.AddSingleton(mapper);

            }
        }
    }
}
